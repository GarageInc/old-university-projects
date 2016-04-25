$Include 'gpgtregs.inc'

RAMStart     EQU  $0040

RomStart     EQU  $8000

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
i_byte   ds  1
o_byte   ds  1
_n       ds  1
loop1    ds  1
loop2    ds  1
i1       ds  1
numlh    ds  2
a        ds  1
tl       ds  1
th       ds  1
thou     ds  1
hund     ds  1
tens     ds  1
ones     ds  1

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
         clr  i
         clr  flags
         jsr  start_ds
         clr  time0
         clr  time1
         clr  time2
         clr  timer
         mov  #$36,T1SC     ;Tim1  Stopped i Prescaler.
         bclr 5,T1SC        ;zapustili
         cli                ; Allow interrupts to happen
         bclr 5,flags
         bclr 3,flags
         mov  #$1,CONFIG2    ; For GP32 - SCI clock source is Bus
         jsr  start_ds
         lda  #$20
mg00:
         bit  flags
         beq  mg00
         jsr  start_ds
         bclr 5,flags
         lda  #$20
mg01:
         bit  flags
         beq  mg01
update:



mg02:
         bclr 2,flags
         lda  #$8
         bit  flags
         beq  mg03
         jsr  conv_t
         bclr 3,flags
mg03:
         lda  #$20
         bit  flags
         beq  update
         bclr 5,flags
         bset 3,flags
         jsr  read_t
         jsr  start_ds
         and  #$ff
         bne  update
         mov  #6t,i1
         lda  #$2d
         clrh
         ldx  #c0
mg04:
         sta  ,x
         incx
         dbnz i1,mg04
         bra  update
**************************************************************
* T1_ISR - obrabotka Overflow Timera 1                       *
**************************************************************
t1_isr:
         pshh
         lda  T1SC
         and  #$7f
         sta  T1SC
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
*podprogrammi
convert:
         rts
conv_t:
         mov  temprd,numlh
         mov  temprd+1,numlh+1
         brclr 7,temprd+1,mcnv00
         mov  #$4,a
mcnv01:
         clc
         ror  numlh+1
         ror  numlh
         dbnz a,mcnv01
         lda  #$ff
         sub  numlh
         sta  th
         lda  temprd
         and  #$f
         sta  numlh+1
         lda  #$ff
         sub  numlh+1
         and  #$f
         bra  mcnv03
mcnv00:
         mov  #$4,a
mcnv02:
         clc
         ror  numlh+1
         ror  numlh
         dbnz a,mcnv02
         mov  numlh,th
         lda  temprd
         and  #$f
mcnv03:
         sta  numlh
         clr  numlh+1
         mov  #6t,a
         clra
mcnv04:
         add  numlh
         dbnz a,mcnv04
         sta  numlh
         jsr  convert
         lda  #$a
         cbeq tens,mcnv05
         bra  mcnv06
mcnv05:  clr  tens
mcnv06:  lda  ones
         cmp  #$5
         bcs  mcnv07
         inc  tens
mcnv07:
         mov  tens,tl


         rts
read_t:
         bsr  init
         mov  #$cc,o_byte
         bsr  out_byte
         mov  #$be,o_byte
         bsr  out_byte
         bsr  in_byte
         sta  temprd
         bsr  in_byte
         sta  temprd+1
         bsr  in_byte
         bsr  in_byte
         bsr  in_byte
         bsr  in_byte
         bsr  in_byte
         bsr  in_byte
         bsr  in_byte
         lda  #$ff
         rts
start_ds:
         bsr  init
         mov  #$cc,o_byte
         bsr  out_byte
         mov  #$44,o_byte
         bsr  out_byte
         lda  #$ff
         rts
init:
         bsr  pin_hi
         bsr  pin_lo
         lda  #50t
         bsr  del_10mk
         bsr  pin_hi
         lda  #50t
         bsr  del_10mk
         rts
in_byte:
         mov  #8t,_n
         clr  i_byte
min00
         bsr  pin_lo
         nop
         bsr  pin_hi
         nop
         nop
         nop
         nop
         nop
         nop
         lda  #$4
         bit  PORTD
         clc
         beq  min01
         sec
min01:
         ror  i_byte
         lda  #6t
         bsr  del_10mk
         dbnz _n,min00
         lda  i_byte
         rts
out_byte:
         mov  #8t,_n
mou03:
         ror  o_byte
         bcc  mou00
         bsr  pin_lo
         bsr  pin_hi
         lda  #6t
         bsr  del_10mk
mou02:
         dbnz _n,mou03
         rts
mou00:   bsr  pin_lo
         lda  #6t
         bsr  del_10mk
         bsr  pin_hi
         bra  mou02
pin_hi:
         sei
         bclr 2,DDRD
         cli
         rts
pin_lo:
         sei
         bclr 2,PORTD
         bset 2,DDRD
         cli
         rts
dellng:
         lda  #250t
del_n_ms:
         sta  loop1
outter:
         mov  #110t,loop2
inner:
         nop
         nop
         nop
         nop
         nop
         nop
         dbnz loop2,inner
         dbnz loop1,outter
         rts
del_10mk:
         sta  loop1
mdel00:
         nop
         nop
         nop
         nop
         nop
         nop
         nop
         dbnz loop1,mdel00
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

