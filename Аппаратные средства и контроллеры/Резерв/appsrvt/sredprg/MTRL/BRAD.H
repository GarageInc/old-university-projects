************************************************
* BRAD.H header file for equates
* and RAM definitions
************************************************

;misc. 68hc05 equates
toie    equ     5
rtie    equ     4
irqr    equ     1
iscr    equ     $0a
tcntr   equ     9
copr    equ     $07f0

* 39 bytes of ram used
*****************************************
* ram and equates for time normalization
*****************************************
normal    ds      12t       ;normalized high frequency vector
normal1   ds      12t       ;normalized low freq. vector
working   ds      2         ;16 bit working registers
working1  ds      2
unor      ds      1         ;length of the raw data buffer
                            ;Variables used by the
j       ds      1           ;normalization and must not be used
m       ds      1           ;while in the routine, but available
n       ds      1           ;otherwise.

nor     equ     12t       ;length of normalized vector--a constant

*******************************************************
* ram and equates for the eerom read/write subroutine *
*******************************************************

addr            ds      1           ;eeprom address
ee_data1        db      0
ee_data2        db      0

****************************************
;equates and ram for the input subroutine
*****************************************
; these ram locations can share with the ones
; used by the time normalization

timeout     equ   normal
zcross      equ   normal+1
;bit_count   equ   normal+2
frames      equ   normal+3
silence     equ   normal+4
sound       equ   normal+5
z1          equ   normal+6
freq1       equ   normal+7
freq2       equ   normal+8
sign        equ   normal+9
temp        equ   normal+0a
ticks       equ   normal+0b

z_thresh        equ 5

$IF debug
sound_thresh    equ 2
sil_thresh      equ 2
$ELSEIF
sil_thresh      equ 6
sound_thresh    equ 8 ;was 6
$ENDIF

freq_limit      equ 6

;portb LEDs
sample_led      equ 2
on_led          equ 3
speech          equ 4
error_led       equ 5

;push buttons porta
select          equ 3
recog           equ 2
train           equ 1
untrainb        equ 0

$IF debug
time_out        equ 04 ;time out value
$ELSEIF
time_out        equ 0ff
$ENDIF

to_error       equ $EF
bufull_error   equ $DF
no_recog       equ $CF
to_high        equ $BF

***********************************************
* ram locations used by division routine
***********************************************
dvsor0    ds      1         ;used by div16_8.sub
ctr       ds      1         ; "   "      "

**********************************
* ram for command/template number
**********************************

led_count db 0

***************************
* ram for type of interrupt
***************************

irq_type db 0

****************************
* ram used by main routine
****************************

******************************
* used by compare.sub        *
******************************
nindex          equ      j
tnum            equ      m

error0          ds      2           ;working error value
min0            equ     working     ;lowest error
min1            equ     working1    ;second lowest error
imin0           equ     ee_data1    ;lowest index number
imin1           equ     ee_data2    ;second lowest index number
running         ds      1

