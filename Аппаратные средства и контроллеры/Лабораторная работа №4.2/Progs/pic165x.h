
#ifndef	_HTC_H_
#warning Header file pic165x.h included directly. Use #include <htc.h> instead.
#endif

/*
 *	Header file for the Microchip 
 *	PIC 16C52 chip
 *	PIC 16C54 chip
 *	PIC 16C54A chip
 *	PIC 16C54B chip
 *	PIC 16C54C chip
 *	PIC 16CR54A chip
 *	PIC 16CR54B chip
 *	PIC 16CR54C chip
 *	PIC 16C55 chip
 *	PIC 16C55A chip
 *	PIC 16C56 chip
 *	PIC 16C56A chip
 *	PIC 16CR56A chip
 *	PIC 16C57 chip
 *	PIC 16C57C chip
 *	PIC 16CR57B chip 
 *	PIC 16CR57C chip 
 *	PIC 16C58 chip
 *	PIC 16C58A chip
 *	PIC 16C58B chip
 *	PIC 16CR58A chip
 *	PIC 16CR58B chip
 *	PIC 16HV540 chip
 *	Baseline Microcontrollers
 */

volatile unsigned char	INDF	@ 0x00;
volatile unsigned char	TMR0	@ 0x01;
volatile unsigned char	RTCC	@ 0x01;
volatile unsigned char	PCL	@ 0x02;
volatile unsigned char	STATUS	@ 0x03;
		unsigned char	FSR	@ 0x04;
volatile unsigned char	PORTA	@ 0x05;
volatile unsigned char	PORTB	@ 0x06;

		unsigned char control	OPTION	@ 0x00;
volatile	unsigned char control	TRISA	@ 0x05;
volatile	unsigned char control	TRISB	@ 0x06;
#ifdef _16HV540
		unsigned char control	OPTION2 @ 0x07;
#endif

/*	STATUS bits	*/
volatile bit	PA1	@ (unsigned)&STATUS*8+6;
volatile bit	PA0	@ (unsigned)&STATUS*8+5;
volatile bit	TO	@ (unsigned)&STATUS*8+4;
volatile bit	PD	@ (unsigned)&STATUS*8+3;
volatile bit	ZERO	@ (unsigned)&STATUS*8+2;
volatile bit	DC	@ (unsigned)&STATUS*8+1;
volatile bit	CARRY	@ (unsigned)&STATUS*8+0;

/*	OPTION bits	*/
#define	T0CS	(1<<5)
#define	T0SE	(1<<4)
#define	PSA	(1<<3)
#define	PS2	(1<<2)
#define	PS1	(1<<1)
#define	PS0	(1<<0)

/*	OPTION2 bits	*/
#ifdef _16HV540
#define WPC	(1<<5)
#define SWE	(1<<4)
#define RL	(1<<3)
#define SL	(1<<2)
#define BL	(1<<1)
#define BE	(1<<0)
#endif

/*	PORTA	bits	*/
volatile bit	RA3	@ (unsigned)&PORTA*8+3;
volatile bit	RA2	@ (unsigned)&PORTA*8+2;
volatile bit	RA1	@ (unsigned)&PORTA*8+1;
volatile bit	RA0	@ (unsigned)&PORTA*8+0;

/*	PORTB bits	*/
volatile bit	RB7	@ (unsigned)&PORTB*8+7;
volatile bit	RB6	@ (unsigned)&PORTB*8+6;
volatile bit	RB5	@ (unsigned)&PORTB*8+5;
volatile bit	RB4	@ (unsigned)&PORTB*8+4;
volatile bit	RB3	@ (unsigned)&PORTB*8+3;
volatile bit	RB2	@ (unsigned)&PORTB*8+2;
volatile bit	RB1	@ (unsigned)&PORTB*8+1;
volatile bit	RB0	@ (unsigned)&PORTB*8+0;

#if defined(_16C55) 	|| defined(_16C55A)	|| defined(_16C57) 	||\
    defined(_16C57C)	|| defined(_16CR57B)	|| defined(_16CR57C)

	volatile unsigned char	PORTC	@ 0x07;
	volatile	unsigned char control	TRISC	@ 0x07;

	/*	PORTC bits	*/
	volatile bit 	RC7	@ (unsigned)&PORTC*8+7;
	volatile bit 	RC6	@ (unsigned)&PORTC*8+6;
	volatile bit 	RC5	@ (unsigned)&PORTC*8+5;
	volatile bit 	RC4	@ (unsigned)&PORTC*8+4;
	volatile bit 	RC3	@ (unsigned)&PORTC*8+3;
	volatile bit 	RC2	@ (unsigned)&PORTC*8+2;
	volatile bit 	RC1	@ (unsigned)&PORTC*8+1;
	volatile bit 	RC0	@ (unsigned)&PORTC*8+0;
#endif

#define CONFIG_ADDR	0xFFF

/*watchdog*/
#define WDTEN		0xFFF	// enable watchdog timer
#define WDTDIS		0xFFB	// disable watchdog timer

/*osc configurations*/
#define RC		0xFFF	// resistor/capacitor
#define HS		0xFFE	// high speed crystal/resonator
#define XT		0xFFD	// crystal/resonator
#define LP		0xFFC	// low power crystal/resonator

#if defined(_16C52) || defined(_16C54) || defined(_16C54A) 	||\
    defined(_16C55) || defined(_16C56) || defined(_16C57)	||\
    defined(_16C58A)
#define PROTECT		0xFF7	// protect the code
#else
#define PROTECT		0x007	// protect the code
#endif
#define UNPROTECT	0xFFF	// do not protect the code
