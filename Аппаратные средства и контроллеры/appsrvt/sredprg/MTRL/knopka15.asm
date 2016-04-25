;prog 09.04.05 knopka15.asm
RAMStart      EQU  $0040
ROMStart      EQU  $8000
VectorStart   EQU  $FFDC

$Include 'gpgtregs.inc'

LCD_CTRL      EQU  PORTC
LCD_DATA      EQU  PORTC
LCD_DATA_MASK EQU  $0F
LCD_RS        EQU  1
LCD_RW        EQU  0
LCD_E         EQU  4
LCD_CTRU      EQU  PORTD


        org   RAMStart

delayReg1 ds 1
delayReg2 ds 1
delayReg3 ds 1
lcddata   ds 1
time      ds 1
i1        ds 1
i2        ds 1
i3        ds 1
c0        ds 32
cr        ds 1


      org   ROMStart

Var_Delay:
       lda   #33T
L6:    deca
       bne   L6
       dec   time
       bne   Var_Delay
       rts


LCD_Delay_40us:
       psha        ;2 cycles
       lda   #14T  ;2 cycles
L5:    deca        ;1 cycle
       bne   L5    ;3 cycles
       pula        ;2 cycles
       rts         ;4 cycles

LCD_Write_Nibble:
       psha
       and   #$0F
       psha
       sei
       lda   LCD_DATA
       and   #$F0
       ora   1,SP
       sta   LCD_DATA
       cli
       bset  LCD_E,LCD_CTRL
       bclr  LCD_E,LCD_CTRL
       bsr   LCD_Delay_40us
       pula
       pula
       rts


LCD_Write:
       nsa
       bsr   LCD_Write_Nibble
       nsa
       bsr   LCD_Write_Nibble
       rts

LCD_Init:
*** Wait for 15ms
       lda   #150T
       sta   time
       jsr   Var_Delay
*** Send Init Command
       lda   #$02
       jsr   LCD_Write_Nibble
*** Wait for 4.1ms
       lda   #41T
       sta   time
       jsr   Var_Delay
*** Send Init Command
       lda   #$08
       jsr   LCD_Write_Nibble
*** Wait for 100 us
       lda   #1
       sta   time
       jsr   Var_Delay
*** Send Init Command
    ;   lda   #$00
    ;   jsr   LCD_Write_Nibble
*** Send Function Set Command (4 bit bus, 2 rows, 5x7 dots)
   ;    lda   #$0F
   ;    jsr   LCD_Write_Nibble
       lda   #$0F
       jsr   LCD_Write
*** Send Display Ctrl Command (display on, cursor off, no blinking)
       lda   #$28
       jsr   LCD_Write
*** Send Clear Display Command (clear display, cursor addr = 0)
       lda   #$01
       jsr   LCD_Write
       lda   #200T
       sta   time
       jsr   Var_Delay
*** Send Entry Mode Command (increment, no display shift)
;       lda   #$04
;       jsr   LCD_Write
       bset  LCD_RS,LCD_CTRU
       rts


Main_Init:
       rsp
       lda   CONFIG1
       ora   #1
       sta   CONFIG1
       mov   #$0E,DDRA
       mov   #$F0,PTAPUE
       mov   #$1F,DDRC
       mov   #$00,PORTC
       mov   #$0B,DDRD
       mov   #$00,PORTD
       jsr   LCD_Init
       clrx
       clrh
OPROS:
       lda   #$30
       sta   cr
       lda   #32T
       sta   i1
       clrh
       clrx
L10:
       lda   cr
       sta   c0,x
       inca
       sta   cr
       incx
       dbnz  i1,L10
;Clear Display i ustanov kursora na 0
       bclr  LCD_RS,LCD_CTRU
       lda   #$01
       jsr   LCD_Write
       lda   #200T
       sta   time
       jsr   Var_Delay
;Delay 0,5 sek
loop:
     lda #$FF
     sta i1
L8:
     lda #50T
     sta time
     jsr Var_Delay
     dbnz i1,L8
     lda   #34T
     sta   i2
     clrh
     clrx
L9:
     lda i2
     cmp #34T
     beq lm01
     cmp #17T
     beq lm03
     bset LCD_RS,LCD_CTRU
     lda c0,x
     jsr LCD_Write
     lda #100T
     sta time
     jsr Var_Delay
     incx
     bra lm00
lm01:
     lda #$80
     bra lm02
lm03:
     lda #$A8
lm02:
     bclr LCD_RS,LCD_CTRU
     jsr LCD_Write
     lda #100T
     sta time
     jsr Var_Delay
lm00:
     dbnz i2,L9
     clrh
     clrx
     lda  #31T
     sta  i1
     mov  c0,cr
lm04:
;     lda  c0+1,x
 ;    sta  c0,x
     incx
     dbnz i1,lm04
     lda  cr
  ;   sta  c0,x
     lda  PORTD
     eor  #$08
     sta  PORTD
     bit  #$08
     beq  lm05
     lda  #$20
     bra  lm07
lm05:
     lda  #$51
lm07:
     sta  c0+3            
     bra  loop
PROVERKA:
     jsr PRB4
     add #3T
     jsr PRB5
     add #3T
     jsr PRB6
     add #3T
     jsr PRB7
     rts


PRB4:
     brclr 4,PTA,L1
     jmp K1
L1:  jsr LCD_Write
B1:  brclr 4,PTA,B1
K1:  rts

PRB5:
     brclr 5,PTA,L2
     jmp K2
L2:  jsr LCD_Write
B2:  brclr 5,PTA,B2
K2:  rts

PRB6:
     brclr 6,PTA,L3
     jmp K3
L3:  jsr LCD_Write
B3:  brclr 6,PTA,B3
K3:  rts

PRB7:
     brclr 7,PTA,L4
     jmp K5
L4:  cmp #$3A
     beq E1
     cmp #$3B
     beq E2
     cmp #$3C
     beq E3
     jmp K4
E1:  lda #1T
     jsr comand
     jmp K4
E2:  lda #$30
     jsr LCD_Write
     jmp K4
E3:  lda #2T
     jsr comand
K4:  brclr 7,PTA,K4
K5:  rts


comand:
     bclr LCD_RS,LCD_CTRU
     jsr  LCD_Write
     bset LCD_RS,LCD_CTRU
     rts



Dummy_Isr:
       rti              ; Return


*******************************************************************************
* Vectors section                                                             *
*******************************************************************************
       org  VectorStart

       dw  Dummy_Isr    ; Time Base Vector
       dw  Dummy_Isr    ; ADC Conversion Complete
       dw  Dummy_Isr    ; Keyboard Vector
       dw  Dummy_Isr    ; SCI Transmit Vector
       dw  Dummy_Isr    ; SCI Receive Vector
       dw  Dummy_Isr    ; SCI Error Vector
       dw  Dummy_Isr    ; SPI Transmit Vector
       dw  Dummy_Isr    ; SPI Receive Vector
       dw  Dummy_Isr    ; TIM2 Overflow Vector
       dw  Dummy_Isr    ; TIM2 Channel 1 Vector
       dw  Dummy_Isr    ; TIM2 Channel 0 Vector
       dw  Dummy_Isr    ; TIM1 Overflow Vector
       dw  Dummy_Isr    ; TIM1 Channel 1 Vector
       dw  Dummy_Isr    ; TIM1 Channel 0 Vector
       dw  Dummy_Isr    ; PLL Vector
       dw  Dummy_Isr    ; ~IRQ1 Vector
       dw  Dummy_Isr    ; SWI Vector
       dw  Main_Init    ; Reset Vector


