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
Timeout1  ds 1   ; Allows three timeout routines to be called each of which
Timeout2  ds 1   ; can run for up to ~ 1/2 second.
Timeout3  ds 1


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
;programma kursovoj raboti

