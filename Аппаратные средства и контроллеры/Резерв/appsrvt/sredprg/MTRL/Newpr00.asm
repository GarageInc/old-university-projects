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
rab00    ds  1
i2       ds  1
r17      ds  1
r18      ds  1
r25      ds  1
r26      ds  1
row      ds  1
col      ds  1
ftim     ds  1
ktim     ds  1
keybl    ds  1
lastrow  ds  1
lastcol  ds  1
rab01    ds  1
rab02    ds  1
tblkey   db  '147.2580369#'
         org RomStart

**************************************************************
* Init_SCI - Turns on the asyncronous communications port    *
*            for "transmitting only" at 9600 baud N81.       *
**************************************************************
Init_SCI:
         mov  #$04,SCBR       ; Baud Rate = 9600
         mov  #$40,SCC1       ; Enable the SCI peripheral
         mov  #$08,SCC2       ; Enable the SCI transmitter
         rts
init_lcd:
         mov  #$20,r25
         jsr  i_com
         jsr  del
         mov  #$f,r25
         jsr  i_com
         jsr  del
         mov  #$20,r25
         jsr  i_com
         jsr  del
         mov  #$1,r25
         jsr  i_com
         jsr  delay
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
         mov  #$0b,DDRD
         mov  #$34,PTDPUE
         clr  i
         clr  flags
;         jsr  start_ds
         clr  time0
         clr  time1
         clr  time2
         clr  timer
         clr  rab02
         clr  ftim
         clr  ktim
         clr  keybl
         clr  lastrow
         clr  lastcol
         bsr  init_lcd
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
;         beq  mg00
;         jsr  start_ds
         bclr 5,flags
         lda  #$20
mg01:
;         bit  flags
;         beq  mg01
update:
;         brclr 0,flags,mg02
         bclr 0,flags
*pereslat v kachestve granici
mg02:
*dobavit sravnenie i vkljuchenie vixoda
         inc  rab02
         bne  up00
         lda  PORTD
         eor  #$8
         sta  PORTD
up00:
         bclr 2,flags
         lda  #$8
         bit  flags
         beq  mg03
         bset 7,flags
         jsr  conv_t
         bclr 7,flags
         bclr 3,flags
mg03:
         lda  #$20
         bit  flags
         beq  update
         bclr 5,flags
         bset 3,flags
         jsr  read_t
;         jsr  start_ds
         and  #$ff
         bne  update
         mov  #6t,i1
         lda  #$2d
         clrh
         ldx  #c0
         bset 7,flags
mg04:
         sta  ,x
         incx
         dbnz i1,mg04
         bclr 7,flags
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
*zdes rabota s LCD i skanirov.klaviaturi
         brclr 7,flags,key15
         jmp  iret
key15:
         clra
         cbeq ktim,key00
         bra  key01
key00:
         clr  row
         clr  col
key01:
         cbeq ftim,key02
         bra  key03
key02:
         clra
         cbeq ktim,key04
         inca
         cbeq ktim,key06
         lda  #$8
         bra  key05
key06:
         lda  #$4
         bra  key05
key04:
         lda  #$2
key05:
         sta  PORTA
         inc  ftim
         bra  lcd00
key03:
         clr  ftim
         lda  PORTA
         and  #$f0
         beq  key07
         sta  row
         mov  ktim,col
key07:
         inc  ktim
         lda  #$3
         cbeq ktim,key08
         bra  lcd00
key08:
         clr  ktim
         clra
         cbeq row,key09
         lda  #$6
         cbeq keybl,key10
         lda  row
         cbeq lastrow,key11
         bra  key12
key11:
         lda  col
         cbeq lastcol,key13
         bra  key12
key13:
         clra
         bra  lcd00
key12:
         mov  col,lastcol
         mov  row,lastrow
         lda  row
         clr  rab01
key14:
         inc  rab01
         rola
         bcc  key14
         dec  rab01
         lda  col
         asla
         asla
         ora  rab01
         cmp  #$b
         beq  key10
         clrh
         tax
         lda  tblkey,x
         ldx  keybl
         sta  c0,x
         inc  keybl
         bra  lcd00
key10:
         clr  keybl
         bset 0,flags
         bra  lcd00
key09:
         clr  lastrow
lcd00:
         bset 1,PORTD
         clrh
         ldx  i
         lda  c0,x
         brclr 7,SCS1,$
         sta  SCDR
         nsa
         and  #$f
         ora  #$10
         sta  PORTC
         bclr 4,PORTC
         lda  c0,x
         and  #$f
         ora  #$10
         sta  PORTC
         bclr 4,PORTC
         inc  i
         lda  #$10
         cbeq i,lcd01
         bra  iret
lcd01:
         clr  i
iret:
         pulh
         rti
*podprogrammi
convert:
         sta  numlh+1
         ora  #$f0
         sta  thou
         add  thou
         mov  thou,rab00
         sta  thou
         lda  rab00
         add  #$e2
         sta  hund
         add  #$32
         sta  ones
         lda  numlh+1
         and  #$f
         sta  rab00
         add  hund
         add  hund
         sta  hund
         lda  rab00
         add  ones
         add  #$e9
         sta  rab00
         sta  tens
         add  tens
         add  tens
         sta  tens
         lda  numlh
         and  #$f
         sta  numlh
         add  tens
         sta  tens
         lda  numlh
         add  ones
         sta  ones
         rol  tens
         rol  ones
         com  ones
         rol  ones
         lda  rab00
         and  #$f
         add  ones
         sta  ones
         rol  thou
         mov  #$a,rab00
         lda  ones
mcv02:
         add  rab00
         dec  tens
         bcc  mcv02
         sta  ones
         lda  tens
mcv03:
         add  rab00
         dec  hund
         bcc  mcv03
         sta  tens
         lda  hund
mcv04:
         add  rab00
         dec  thou
         bcc  mcv04
         sta  hund
         lda  thou
mcv05:
         add  rab00
         bcc  mcv05
         sta  thou
         mov  #3t,i2
         clrh
         ldx  #thou
mcv00:
         lda  ,x
         and  #$f
         bne  mcv01
         lda  #$a
         sta  ,x
         incx
         dbnz i2,mcv00
mcv01:
         rts
conv_t:
         rts
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
         brclr 7,temprd+1,mcnv08
mcnv11:
         lda  #99t
         sub  th
         bcc  mcnv09
         lda  th
         mov  th,numlh
         clr  numlh+1
         jsr  convert
         mov  #$20,c0
         mov  #$30,c0+5
         mov  #$2e,c0+4
         lda  ones
         add  #$30
         sta  c0+3
         lda  tens
         add  #$30
         sta  c0+2
         lda  hund
         add  #$30
         sta  c0+1
         rts
mcnv09:
         lda  tl
         mov  tl,numlh
         clr  numlh+1
         jsr  convert
         mov  #$2e,c0+4
         lda  ones
         add  #$30
         sta  c0+5
         mov  th,numlh
         clr  numlh+1
         jsr  convert
         lda  ones
         add  #$30
         sta  c0+3
         lda  tens
         add  #$30
         sta  c0+2
         mov  #$20,c0+1
         mov  #$20,c0
         brclr 7,temprd+1,mcnv10
         mov  #$2d,c0
mcnv10:
         rts
mcnv08:
         lda  #19t
         sub  th
         bcc  mcnv11
         lda  th
         mov  th,numlh
         clr  numlh+1
         jsr  convert
         mov  #$30,c0+5
         mov  #$2e,c0+4
         mov  #$2d,c0
         lda  ones
         add  #$30
         sta  c0+2
         lda  tens
         add  #$30
         sta  c0+1
         rts
read_t:
         rts
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
del:
         mov  #50t,r18
mmdl00:
         dbnz r18,mmdl00
         rts
delay:
         mov  #9t,r17
mlay00:
         mov  #255t,r18
mlay01:
         dbnz r18,mlay01
         dbnz r17,mlay00
         rts
i_com:
         bclr 0,PORTD
         bclr 1,PORTD
         lda  r25
         nsa
         and  #$f
         ora  #$10
         sta  PORTC
         bclr 4,PORTC
         lda  r25
         and  #$f
         ora  #$10
         sta  PORTC
         bclr 4,PORTC
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

