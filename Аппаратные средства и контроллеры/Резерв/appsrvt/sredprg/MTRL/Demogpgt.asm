; This application is meant to demonstrate a framework for an
; application running on the 68HC908GT8, 68HC908GT16 or 68HC908GP32 MCU. It
; demonstrates interrupts from the timer and the ADC, uses general
; purpose timeouts, and transmits information out the serial port.

; Application Information:

; (1) An output compare channel of the timer is used to create a time
;     base for the application. Output compare intervals happen every
;     2ms, and is continually set up in the timer interrupt service
;     routine. The internal Bus clock is 2.4576 MHZ.
; (2) Provides three general purpose timeouts, each with a resolution of
;     2ms and a maximum timeout of 510 ms (1-255). When a timeout value
;     is zero, it is ignored. When a timeout value is non-zero, it is
;     decremented by the timer interrupt approximately every two milli-
;     seconds. When a timeout value goes to zero, the timeout event
;     handler is called. The "timeout 1" value is initially set to 3,
;     so the "timeout 1" event handler will be run after 6ms. The event
;     handler sets up the ADC converter to do a single conversion and
;     cause an interrupt. After starting the conversion, the "timeout 1"
;     event schedules another "timeout 1" event in 6ms (timeout=3).
; (3) When the ADC conversion is complete, an interrupt is generated.
;     In the interrupt routine, the ADC input value is read and transmitted
;     out the TXD line of the serial port (PRTE bit 0) at 9600 baud.
;     Note that if you are running this ICS in "Simulation Only" mode,
;     you have to use the ADDI command prior to the conversion becoming
;     complete. Otherwise the ADR value will not be set and the simulator
;     will give an error when the simulator tries to load the uninitialized
;     value in ADR.
; (4) Since the GT16/GT8 parts included with the ICS kits runs in monitor mode
;     at at bus clock of 2.4576, this program simulates the GT16/GT8 switching
;     to external clock mode so it matches the ICS kit. This can be changed
;     so the GT16/GT8 runs off of the internal clock. The simulator fully simulates
;     the ICG module including the trim functionality.

; To run this application, you first need to compile it pushing the F4
; key on your keyboard. Note that once you do this, it will say "Successful
; Assembly" on the status bar at the bottom of this application.

; To simulate this application in the ICS08GPGTZ in-circuit simulator,
; simply run the ICS08GPGTZ and type "LOAD DEMOGPGT" in the status window.
; The PC will automatically be loaded with the reset vector. Use the
; BR command to set breakpoints in the ADC interrupt routine and the
; timer routine (or in the debugger double click the red dot next to any
; valid source line). Use the cycles counter to verify the time between
; timer interrupts when you run code using the GO command. The CAPTURE
; command is very useful for tracking when and if a memory location's
; value changes. If you capture the ADR (ADC Data Register), the
; capturefile will contain the cycle count of each time a conversion is
; complete.

; To run this application realtime, you will have to use the PROG08SZ
; application to program the S19 into the GT16/GT8/GP32's on-chip flash block.
; Once you have done this, run the ICD08SZ in-circuit debugger. The
; PC does not get set to the reset vector automatically (this is a
; monitor mode limitation). First, you should load the debug information
; for the application now programmed into the flash. Type
; "LOADMAP DEMOGPGT.MAP" in the status window. Use the "PC MAIN_INIT"
; to set the PC to the start of the application. Only the first breakpoint
; set is valid in flash. To reset the first breakpoint, use the NOBR command
; before you set the breakpoint. If you do a GO, the part will be running
; in realtime. You can see characters coming out the porte pin every 6ms.

; Here is the sample application...

$Include 'gpgtregs.inc'

RAMStart     EQU  $0040

RomStart     EQU  $E000         ; This is valid ROM on the GP32, GT16, and GT8

VectorStart  EQU  $FFDC
ADC_Channel  EQU  5t
ADC_ENABLE_INT EQU 01000000q    ; Bit mask for interrupt enable bit
                                ; in the ADC status/control register


    org RamStart

temp_long ds 4
temp_word ds 2
temp_byte ds 1
Timeout1 ds 1   ; Allows three timeout routines to be called each of which
Timeout2 ds 1   ; can run for up to ~ 1/2 second.
Timeout3 ds 1


    org RomStart


**************************************************************
* Init_SCI - Turns on the asyncronous communications port    *
*            for "transmitting only" at 9600 baud N81.       *
**************************************************************
Init_SCI:
       mov   #$04,SCBR       ; Baud Rate = 9600
       mov   #$40,SCC1       ; Enable the SCI peripheral
       mov   #$08,SCC2       ; Enable the SCI transmitter
       rts


**************************************************************
* Init_AtoD - Sets up the AtoD clock + turns it on           *
**************************************************************
Init_AtoD:
       mov   #$30,ADCLK          ; Bus Clock / 2
       mov   #ADC_CHANNEL,ADSCR  ; Enable the ADC peripheral
       rts


**************************************************************
* Init_Timer - Turns on timer 1 channel 0 for an Output      *
*              Compare in approximately 2ms. The timer       *
*              interrupt service routine continually sets    *
*              the next interrupt to occur 2ms later.        *
**************************************************************
Init_Timer:
       mov   #$36,T1SC     ; Timer 1 - Cleared + Stopped.
                           ;    Clicks once every 64 BUS Cycles
                           ;    77t Clicks ~ 2ms

       mov   #$0,T1CH0H    ; Set Output Compare to happen 77T clicks
       mov   #77T,T1CH0L   ; after we start the timer. (~2ms). The
                           ; timer interrupt will set OC for another ~2ms.

       mov   #$54,T1SC0    ; Timer 1 Channel 0
                           ; Set for Output Compare operation.

       mov   #$06,T1SC     ; Start the timer
       rts


**************************************************************
* Main_Init - This is the point where code starts executing  *
*             after a RESET.                                 *
**************************************************************
Main_Init:
       rsp
       clra                ; Initialize A,X so that interrupt
       clrx                ; processing doesn't stop with
                           ; uninitialized register warning
                           ; when push A,X on the stack
       bsr Init_SCI        ; Initialize peripherals
       bsr Init_AtoD
       bsr Init_Timer
       clr timeout1        ; Initialize timeouts (0=off)
       clr timeout2
       clr timeout3
       cli                 ; Allow interrupts to happen

       mov #$29,config2    ; For GT16 - turn on external clock
                           ; For GP32 - SCI clock source is Bus
                           ;            (Automatic for GT16)
       mov #$0A,icgcr      ; For GT16 - enable external clock

       mov #3,timeout1     ; Start an AtoD conversion in 3*2ms=6ms

main_loop:
       brclr 0,icgcr,fixcop
       bset  4,icgcr       ; For GT16 - switch to external clock
                           ;            when valid
fixcop:
       sta copctl          ; Clear COP to prevent watchdog reset
       bra main_loop

**************************************************************
* AtoD_ISR - ADC Conversion Complete Interrupt               *
*            Transmit ADC value out serial port (PTE0)       *
**************************************************************
AtoD_ISR:
!
       lda  adr            ; Get the converted value
       brclr 7,scs1,$       ; wait until xmitter is ready.
       sta  scdr           ; Xmit it our serial port
       sta  temp_byte      ; save it in temp_byte
       rti


**************************************************************
* T_ISR - Timer Interrupt Service Routine.                   *
*             after a RESET.                                 *
**************************************************************
T_ISR:
       pshh
       lda   t1sc0
       and   #$7f
       sta   t1sc0              ; Clear O.C. Flag
       ldhx  t1ch0h
       aix   #77T               ; Setup another interrupt in ~2ms
       sthx  t1ch0h
       pulh

Check_t1:
       tst timeout1
       beq check_t2             ; Is Timeout 1 currently active?
       dec timeout1             ;   yes
       bne check_t2             ; Did it just finish counting down?
       jsr t1_handler           ;   Yes - Execute Timeout 1 handler

Check_t2:
       tst timeout2
       beq check_t3             ; Is Timeout 2 currently active?
       dec timeout2             ;   yes
       bne check_t3             ; Did it just finish counting down?
       jsr t2_handler           ;   Yes - Execute Timeout 2 handler

Check_t3:
       tst timeout3
       beq done_tisr             ; Is Timeout 3 currently active?
       dec timeout3             ;   yes
       bne done_tisr             ; Did it just finish counting down?
       jsr t3_handler           ;   Yes - Execute Timeout 3 handler

done_tisr:
       rti


****************************************************************
* Timeout Handlers - All the user has to do is set one of the  *
*                    timeout variables to any number n (1-255) *
*                    and the timeout handler will be executed  *
*                    in 2*n milliseconds. Setting the timeout  *
*                    variable from within the handler will     *
*                    cause a periodic timeout as shown in      *
*                    timeout 1.                                *
****************************************************************
t1_handler:
       mov   #{ADC_ENABLE_INT | ADC_CHANNEL},ADSCR
                                 ; Start a single ADC conversion
                                 ; which will generate an interrupt.

       mov #3,timeout1     ; Setup next t1_handler event in 3*2ms=6ms
       rts

t2_handler:
       rts

t3_handler:
       rts


**************************************************************
* DUMMY_ISR - Dummy Interrupt Service Routine.               *
*             Just does a return from interrupt.             *
**************************************************************
dummy_isr:

       rti           ; return


**************************************************************
* Vectors - Timer Interrupt Service Routine.                 *
*             after a RESET.                                 *
**************************************************************
   org  VectorStart

        dw  dummy_isr    ; Time Base Vector
        dw  atod_isr     ; ADC Conversion Complete
        dw  dummy_isr    ; Keyboard Vector
        dw  dummy_isr    ; SCI Transmit Vector
        dw  dummy_isr    ; SCI Receive Vector
        dw  dummy_isr    ; SCI Error Vector
        dw  dummy_isr    ; SPI Transmit Vector
        dw  dummy_isr    ; SPI Receive Vector
        dw  dummy_isr    ; TIM2 Overflow Vector
        dw  dummy_isr    ; TIM2 Channel 1 Vector
        dw  dummy_isr    ; TIM2 Channel 0 Vector
        dw  dummy_isr    ; TIM1 Overflow Vector
        dw  dummy_isr    ; TIM1 Channel 1 Vector
        dw  t_isr        ; TIM1 Channel 0 Vector
        dw  dummy_isr    ; ICG/CGM Vector
        dw  dummy_isr    ; ~IRQ1 Vector
        dw  dummy_isr    ; SWI Vector
        dw  main_init    ; Reset Vector


; (C)opywrite P&E Microcomputer Systems, 1998, 2002
; Visit us at http://www.pemicro.com

