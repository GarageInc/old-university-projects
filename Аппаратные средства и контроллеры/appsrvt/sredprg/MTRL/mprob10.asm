$Include 'gpgtregs.inc'

RamStart     EQU  $0040

RomStart     EQU  $E000

VectorStart  EQU  $FFDC

         org RamStart
d1nl     ds  1
d1nm     ds  1
         org RomStart

**************************************************************
* Init_SCI - Turns on the asyncronous communications port    *
*            for "transmitting only" at 9600 baud N81.       *
**************************************************************
start:
         rsp
         clra                ; Initialize A,X so that interrupt
         clrx                ; processing doesn't stop with
                             ; uninitialized register warning
                             ; when push A,X on the stack
         clr  PORTA
         mov  #$0e,DDRA
         mov  #$f1,PTAPUE
         clr  PORTB
         clr  DDRB
         clr  PORTC
         mov  #$1f,DDRC
         clr  PORTD
         mov  #$3b,DDRD
         mov  #$4,PTDPUE
         bset 3,PORTD
         bclr 4,PORTD
         bset 5,PORTD
         mov  #$36,T1SC     ;Tim1  Stopped i Prescaler.
         bclr 5,T1SC        ;zapustili
                            ;Disable interrupts to happen
         mov  #$1,CONFIG2    ; For GP32 - SCI clock source is Bus
         clr  d1nl
         clr  d1nm
update:
         inc  d1nl
         clra
         cbeq d1nl,upd01
         bra  update
upd00:   inc  d1nm
         lda  #$20
         cbeq d1nm,upd01
         bra  update
upd01:   clr  d1nm
         lda  PORTD
         eor  #$8
         sta  PORTD
         bra  update
*podprogrammi
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
         dw  dummy_isr       ; TIM1 Overflow Vector
         dw  dummy_isr    ; TIM1 Channel 1 Vector
         dw  dummy_isr    ; TIM1 Channel 0 Vector
         dw  dummy_isr    ; ICG/CGM Vector
         dw  dummy_isr    ; ~IRQ1 Vector
         dw  dummy_isr    ; SWI Vector
         dw  start        ; Reset Vector
;programma kursovoj raboti

