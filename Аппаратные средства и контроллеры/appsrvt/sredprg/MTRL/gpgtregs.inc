 ; 68HC908GT16/GP32 Equates

PTA    EQU $0000     ; Ports and data direction
PORTA  EQU $0000
PTB    EQU $0001
PORTB  EQU $0001
PTC    EQU $0002
PORTC  EQU $0002
PTD    EQU $0003
PORTD  EQU $0003
DDRA   EQU $0004
DDRB   EQU $0005
DDRC   EQU $0006
DDRD   EQU $0007
PTE    EQU $0008
PORTE  EQU $0008

SCPSC   EQU $0009    ;GT16 Only - Enhanced SCI
SCIACTL EQU $000A    ;GT16 Only
SCIADAT EQU $000B    ;GT16 Only

DDRE    EQU $000C    ; Ports and data direction

PTAPUE  EQU $000D    ; Port pull-up enables
PTCPUE  EQU $000E
PTDPUE  EQU $000F

SPCR    EQU $0010    ; SPI (Syncronous communications)
SPSCR   EQU $0011
SPDR    EQU $0012

SCC1    EQU $0013    ; SCI (Asyncronous communications)
SCC2    EQU $0014
SCC3    EQU $0015
SCS1    EQU $0016
SCS2    EQU $0017
SCDR    EQU $0018
SCBR    EQU $0019

INTKBSCR EQU $001a   ; Keyboard interrupt control/status
INTKBIER EQU $001b

TBCR    EQU $001c    ; Time base module

INTSCR  EQU $001d    ; IRQ status/control

CONFIG2 EQU $001e    ; System configuration
CONFIG1 EQU $001f

T1SC    EQU $0020    ; Timer 1
T1CNTH  EQU $0021
T1CNTL  EQU $0022
T1MODH  EQU $0023
T1MODL  EQU $0024
T1SC0   EQU $0025
T1CH0H  EQU $0026
T1CH0L  EQU $0027
T1SC1   EQU $0028
T1CH1H  EQU $0029
T1CH1L  EQU $002a

T2SC    EQU $002b    ; Timer 2
T2CNTH  EQU $002c
T2CNTL  EQU $002d
T2MODH  EQU $002e
T2MODL  EQU $002f
T2SC0   EQU $0030
T2CH0H  EQU $0031
T2CH0L  EQU $0032
T2SC1   EQU $0033
T2CH1H  EQU $0034
T2CH1L  EQU $0035

PCTL    EQU $0036    ; GP32 Only - Phase lock loop (for crystals)
PBWC    EQU $0037    ; GP32 Only
PMSH    EQU $0038    ; GP32 Only
PMSL    EQU $0039    ; GP32 Only
PMRS    EQU $003A    ; GP32 Only
PMDS    EQU $003B    ; GP32 Only

ICGCR   EQU $0036    ; GT16 Only - Internal Clock Generator
ICGMR   EQU $0037    ; GT16 Only
ICGTR   EQU $0038    ; GT16 Only
ICGDVR  EQU $0039    ; GT16 Only
ICGDSR  EQU $003A    ; GT16 Only
ICGTR5  EQU $FF80    ; GT16 Only
ICGTR3  EQU $FF81    ; GT16 Only

ADSCR   EQU $003C    ; A to D converter
ADR     EQU $003D
ADCLK   EQU $003E

SBSR    EQU $fe00    ; System integration
SRSR    EQU $fe01
SUBAR   EQU $fe02
SBFCR   EQU $fe03

INT1    EQU $fe04    ; Interrupt status
INT2    EQU $fe05
INT3    EQU $fe06

FLTCR   EQU $fe07    ; Flash test/programming
FLCR    EQU $fe08

BRKH    EQU $fe09    ; Hardware breakpoint
BRKL    EQU $fe0a
BRKSCR  EQU $fe0b

LVISR   EQU $fe0c    ; Low voltage detect

FLBPR   EQU $ff80    ; Flash boot protect

COPCTL  EQU $ffff    ; COP (Computer operating properly) control



; (C)opywrite P&E Microcomputer Systems, 1998, 2002
; http://www.pemicro.com

