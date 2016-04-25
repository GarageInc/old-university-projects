***********************************************
* TEST3.ASM- Perform input and time normalization
* Added interrupt processing, error and on leds
* Added compare routine
* Copyright (c) 1995 by Brad Stewart
* Raw data is stored in EEPROM loc 0 to 128, bank 0
* where [0..127] is high frequency and
* [128..255] is low frequency data
*
***************************************************
$SET debug1     ;toggles sampling led
$SET debug
$SETNOT debug
$SET timer
;$SETNOT timer
RAMStart EQU $0040
ROMStart EQU $E000

        org  RAMStart

#include        'brad.h'

        org  ROMStart

start:
        ;setup data direction registers

        lda #$f0
        sta porta
        sta ddra
        lda #$ff
        sta portb
        lda #%00101110
        sta ddrb

;put in pull down definitions here????

        lda #0
        sta led_count
        tax             ;initialize x register

again:
        ;turn off all status leds
        bset sample_led,portb
        bset irqr,iscr

************************************
* wait for interrupt               *
************************************

        stop

        ;check for type of interrupt which is stored in irq_type
        sei
        lda irq_type

;        cmp #4          ;speech signal?
;        beq again       ;yes

        bset error_led,portb   ;turn off error led

        cmp #1          ;select ?
        beq again       ;yes

        cmp #2          ;recognize button?
        bne main15
        lda #$f0
        sta porta       ;turn off template lights
        bra main10

main15:
        cmp #3           ;train mode?
        bne again
        bclr sample_led,portb

main10:
       ;make sure irqs do not happen

        bclr on_led,portb
        bset error_led,portb    ;turn off led

        jsr input
        ;now check if there was an error
        cmp #0          ; if acc = 0, then good sample
        bne main20
        jsr time_norm   ;results of template are in ram loc normal,normal1

        ;traing mode?
        ;if yes, then store template

        lda irq_type
        cmp #3
        bne main30

        ;store the template in eerom
        jsr a2404_start

        ;first, compute start address
        lda led_count
        and #0f

        tax
        lda addresses,x   ;get effective address
        sta addr
        cpx #5
        bls main35
        ;address start is now in bank1

        lda #W_B1               ;put data in bank zero
        bra main40
main35:
        lda #W_B0
main40:
        jsr a2404_write         ;write control byte

        lda addr                ;write address
        jsr a2404_write         ;address 0


        ldx #0
        stx ctr
main45:
        lda normal,x          ;now write data to ROM
                              ;address is auto-incremented
        jsr a2404_write
        inc addr
        bne main50      ;have we overflowed address?
                        ;will only occur with template #5
        ;yes
        ;reset to bank1 and set addr to zero
        jsr a2404_stop
        jsr a2404_start
        lda #W_B1
        jsr a2404_write      ;write control byte
        lda #0
        sta addr
        jsr a2404_write      ;set address to 0

main50:
        inc ctr
        ldx ctr
        cpx #24t
        bne main45

main30:
        ;do pattern match
        jsr compare
        ;test if lowest value is less than threshold
        lda min0        ;subtract and test
        sub #150t
        lda min0+1
        sbc #0
        bmi main32
        ;minimum value not low enough!
        lda #no_recog
        sta porta
        bclr error_led,portb
        bra main25
main32:
        ; now display minimum value
        lda imin0
        coma
        lsla
        lsla
        lsla
        lsla
        sta porta
        bra main25

main20:
        bclr error_led,portb ;turn on error led
        dec led_count        ;decrement count so as to repeat
main25:
        jsr a2404_stop  ;turn off eeprom
        bset on_led,portb ;turn off on indicator

       jmp again


#include        'div16_8.sub'
#include        'eeprom.sub'
#include        'time_nor.sub'
#include        'input.sub'
#include        'delayms.sub'
#include        'irq.sub'
#include        'compare.sub'

***************************
* rom constants
***************************

addresses:
        db      128t    ;0
        db      152t    ;1
        db      176t    ;2
        db      200t    ;3
        db      224t    ;4
        db      248t    ;5
        db       16t    ;6
        db       40t    ;7
        db       64t    ;8
        db       88t    ;9
        db      112t    ;10
        db      136t    ;11
        db      160t    ;12
        db      184t    ;13
        db      208t    ;14
        db      232t    ;15

;    org MOR
;    db $24           ;MOR enable PORT A LEVEL ints and osc resistor

   org $07f8
   dw start
   dw irq_int
   dw start
   dw start
