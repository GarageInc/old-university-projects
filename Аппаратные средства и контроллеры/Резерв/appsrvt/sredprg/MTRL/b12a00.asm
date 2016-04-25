$Include 'gpgtregs.inc'

RAMStart     EQU  $0040

RomStart     EQU  $E000     ; This is valid ROM on the GP32, GT16, and GT8

VectorStart  EQU  $FFDC

         org RamStart
i        ds  1
flags    ds  1
time0    ds  1
time1    ds  1
time2    ds  1
timer    ds  1
temprd   ds  2
c0       db  '0123456789ABCDEF'


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
start:
         rsp
         clra                ; Initialize A,X so that interrupt
         clrx                ; processing doesn't stop with
                             ; uninitialized register warning
                             ; when push A,X on the stack
         bsr  Init_SCI       ; Initialize peripherals
         clr  PORTA
         mov  #$0e,DDRA
         mov  #$f1,PTAPUE
         clr  PORTB
         clr  DDRB
         clr  PORTC
         mov  #$1f,DDRC
         clr  PORTD
         mov  #$03,DDRD
         mov  #$3c,PTDPUE
         mov  #$36,T1SC     ;Tim1  Stopped i Prescaler.
         bclr 5,T1SC        ;zapustili
         clr  i
         clr  flags
         clr  time0
         clr  time1
         clr  time2
         clr  timer

         cli                ; Allow interrupts to happen
         mov  #$1,CONFIG2    ; For GP32 - SCI clock source is Bus

update:


         bra  update
**************************************************************
* T1_ISR - obrabotka Overflow Timera 1                       *
**************************************************************
t1_isr:
         pshh
         inc  time0
         lda  #100t
         cbeq time0,mt100
         bra  l22
mt100:
         clr  time0
         inc  time1
         inc  time2
         lda  #187t
         cbeq time1,mt101
         bra  l18
mt101:
         clr  time1
l18:
         lda  #3t
         cbeq time2,mt102
         bra  l22
mt102:
         clr  time2
         bset 5,flags
l22:
*zdes rabota s LCD

iret:
         pulh
         rti

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
         dw  dummy_isr    ; ADC Conversion Complete
         dw  dummy_isr    ; Keyboard Vector
         dw  dummy_isr    ; SCI Transmit Vector
         dw  dummy_isr    ; SCI Receive Vector
         dw  dummy_isr    ; SCI Error Vector
         dw  dummy_isr    ; SPI Transmit Vector
         dw  dummy_isr    ; SPI Receive Vector
         dw  dummy_isr    ; TIM2 Overflow Vector
         dw  dummy_isr    ; TIM2 Channel 1 Vector
         dw  dummy_isr    ; TIM2 Channel 0 Vector
         dw  t1_isr       ; TIM1 Overflow Vector
         dw  dummy_isr    ; TIM1 Channel 1 Vector
         dw  dummy_isr    ; TIM1 Channel 0 Vector
         dw  dummy_isr    ; ICG/CGM Vector
         dw  dummy_isr    ; ~IRQ1 Vector
         dw  dummy_isr    ; SWI Vector
         dw  start        ; Reset Vector
;programma kursovoj raboti

