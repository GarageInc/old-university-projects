#ifndef	_PIC_H_
#define	_PIC_H_

#ifndef _HTC_H_
#include <htc.h>
#endif

#if defined(_16F1937)
    #include <pic16f1937.h>
#endif

#if defined(_16LF1937)
    #include <pic16lf1937.h>
#endif

#if defined(_16F1936)
    #include <pic16f1936.h>
#endif

#if defined(_16LF1936)
    #include <pic16lf1936.h>
#endif

#if defined(_16F1934)
    #include <pic16f1934.h>
#endif

#if defined(_16LF1934)
    #include <pic16lf1934.h>
#endif

#if defined(_16F1933)
    #include <pic16f1933.h>
#endif

#if defined(_16LF1933)
    #include <pic16lf1933.h>
#endif

#if defined(_16F1826) || defined(_16LF1826)
    #include <pic16f1826.h>
#endif

#if defined(_16F1827) || defined(_16LF1827)
    #include <pic16f1827.h>
#endif


#if defined(_10F200)	|| defined(_10F202)	||\
    defined(_10F204)	|| defined(_10F206)
	#include	<pic10f20x.h>
#endif
#if defined(_10F220)	|| defined(_10F222)
	#include	<pic10f22x.h>
#endif
#if defined(_12C508)	|| defined(_12C509)	||\
    defined(_12F508)	|| defined(_12F509)	||\
    defined(_12C508A)	|| defined(_12C509A)	||\
    defined(_12CE518)	|| defined(_12CE519)	||\
    defined(_12C509AG)	|| defined(_12C509AF)	||\
    defined(_12CR509A)	|| defined(_RF509AG)	||\
    defined(_RF509AF)
	#include	<pic125xx.h>
#endif
#if defined(_12F510)
	#include	<pic12f510.h>
#endif
#if defined(_12F519)
	#include	<pic12f519.h>
#endif
#if defined(_16C432)	|| defined(_16C433)
	#include	<pic1643x.h>
#endif
#if defined(_16C52)	|| defined(_16C54)	|| defined(_16C54A)	||\
    defined(_16C54B)	|| defined(_16C54C)	|| defined(_16CR54A)	||\
    defined(_16CR54B)	|| defined(_16CR54C)	|| defined(_16C55)	||\
    defined(_16C55A)	|| defined(_16C56)	|| defined(_16C56A)	||\
    defined(_16CR56A)	|| defined(_16C57)	|| defined(_16C57C)	||\
    defined(_16CR57B)	|| defined(_16CR57C)	|| defined(_16C58A)	||\
    defined(_16C58B)	|| defined(_16CR58A)	|| defined(_16CR58B)	||\
    defined(_16C58)	|| defined(_16HV540)
	#include	<pic165x.h>
#endif
#if defined(_16F54)	|| defined(_16F57)	|| defined(_16F59)
	#include	<pic16f5x.h>
#endif
#if defined(_12F609)	|| defined(_12HV609)	||\
    defined(_12F615)	|| defined(_12HV615)
	#include	<pic12f615.h>
#endif
#if defined(_12C671)	|| defined(_12C672)	||\
    defined(_12CE673)	|| defined(_12CE674)
	#include	<pic1267x.h>
#endif
#if	defined(_12F629)	|| defined(_12F675)
	#include	<pic12f6x.h>
#endif
#if	defined(_12F683)
	#include	<pic12f683.h>
#endif
#if	defined(_12F675F)	|| defined(_12F675H)	|| defined(_12F675K)
	#include	<pic12rf675.h>
#endif
#if defined(_16C505) || defined(_16F505)
	#include	<pic16505.h>
#endif
#if defined(_16F506)
	#include	<pic16f506.h>
#endif
#if defined(_16F526)
	#include	<pic16f526.h>
#endif
#ifdef	_14000
	#include	<pic14000.h>
#endif
#if defined(_16C554)	|| defined(_16C556)	|| defined(_16C557) ||	\
	defined(_16C558) || defined(_16C554A)   || defined(_16C556A) || \
	defined(_16C558A)
	#include	<pic1655x.h>
#endif
#ifdef	_16C61
	#include	<pic1661.h>
#endif
#if defined(_16C62)	|| defined(_16C62A)	|| defined(_16CR62)	||\
    defined(_16C62B)
	#include	<pic1662.h>
#endif
#if defined(_16C620)	|| defined(_16C621)	|| defined(_16C622)	||\
    defined(_16C620A)   || defined(_16C621A)    || defined(_16C622A)	||\
    defined(_16CE623)	|| defined(_16CE624)	|| defined(_16CE625)	||\
    defined(_16CR620A)
	#include	<pic1662x.h>
#endif
#if defined(_16C64)	|| defined(_16C64A)	|| defined(_16CR64)
	#include	<pic1664.h>
#endif
#if defined(_16C641)	|| defined(_16C642)	||\
    defined(_16C661)	|| defined(_16C662)
	#include	<pic166xx.h>
#endif
#if defined(_16C65)	|| defined(_16C65A)	|| defined(_16CR65)
	#include	<pic1665.h>
#endif
#if defined(_16C66)	|| defined(_16C67)
	#include	<pic166x.h>
#endif
#if defined(_16C71)	|| defined(_16C710)	|| defined(_16C711)
	#include	<pic1671x.h>
#endif
#if defined(_16C712)    || defined(_16C715)     || defined(_16C716)
	#include	<pic16715.h>
#endif
#if defined(_16C72)     || defined(_16C72A)	|| defined(_16CR72)
	#include	<pic1672.h>
#endif
#if defined(_16C73)	|| defined(_16C73A)	|| defined(_16CR73) || \
    defined(_16C74)	|| defined(_16C74A)	|| defined(_16CR74) || \
    defined(_16C63)     || defined(_16CR63)     || defined(_16C63A)     ||\
    defined(_16C65B)    || defined(_16C73B)     || defined(_16C74B)	||\
    defined(_16LC74B)
	#include	<pic1674.h>
#endif
#if defined(_16C76)	|| defined(_16C77) || \
	defined(_16CR76) || defined(_16CR77)
	#include	<pic1677.h>
#endif
#if defined(_16C773)	|| defined(_16C774)	|| defined(_16C770)	||\
    defined(_16C771)	|| defined(_16C717)	|| defined(_16C745)	||\
    defined(_16C765)
	#include	<pic1677x.h>
#endif
#if defined(_16C781)	|| defined(_16C782)
	#include	<pic1678x.h>
#endif
#if defined(_16F610)	|| defined(_16HV610)	|| defined(_16F616)	||\
    defined(_16HV616)
	#include	<pic16f616.h>
#endif
#if defined(_16F627)	|| defined(_16F628)
	#include	<pic16f6x.h>
#endif
#if defined(_16F627A)	|| defined(_16F628A)	|| defined(_16F648A)
	#include	<pic16f62xa.h>
#endif
#if defined(_16F630)	|| defined(_16F676)
	#include	<pic16630.h>
#endif
#if defined(_12F635)	|| defined(_16F636)	|| defined(_16F639)
	#include	<pic16f636.h>
#endif
#if defined(_16F684)
	#include	<pic16f684.h>
#endif
#if defined(_16F631)	|| defined(_16F677)	|| defined(_16F685)	||\
    defined(_16F687)	|| defined(_16F689)	|| defined(_16F690)
	#include	<pic16f685.h>
#endif
#if defined(_16F688)
	#include	<pic16f688.h>
#endif
#if defined(_16F83)	|| defined(_16CR83)	|| defined(_16C84)	|| \
    defined(_16F84)	|| defined(_16F84A)	|| defined(_16CR84)
	#include	<pic1684.h>
#endif
#if defined(_16F87)	|| defined(_16F88)
	#include <pic16f87.h>
#endif
#if defined(_16F873)	|| defined(_16F874)	||\
    defined(_16F876)	|| defined(_16F877)	||\
    defined(_16F872)	|| defined(_16F871)	||\
    defined(_16F870)
	#include	<pic1687x.h>
#endif
#if defined(_16F873A)	|| defined(_16F874A)	||\
    defined(_16F876A)	|| defined(_16F877A)
	#include	<pic168xa.h>
#endif
#if defined(_16F882)	|| defined(_16F883)	|| defined(_16F884)	||\
    defined(_16F886)	|| defined(_16F887)
	#include	<pic16f887.h>
#endif
#if	defined(_16F72)	||\
	defined(_16F73)	|| defined(_16F74)	||\
	defined(_16F76) || defined(_16F77)
	#include	<pic16f7x.h>
#endif
#if defined(_16F716)
	#include	<pic16f716.h>
#endif
#if	defined(_16F722) || defined(_16F723) || defined(_16F724) ||\
	defined(_16F726) || defined(_16F727) ||\
	defined(_16LF722) || defined(_16LF723) || defined(_16LF724) ||\
	defined(_16LF726) || defined(_16LF727)
	#include	<pic16f72x.h>
#endif
#if defined(_16F785) 	|| defined(_16HV785)
	#include	<pic16f785.h>
#endif
#if	defined(_16F737) || defined(_16F747)	||\
	defined(_16F767) || defined(_16F777)
	#include	<pic16f7x7.h>
#endif
#if defined(_16F818)	|| defined(_16F819)
	#include	<pic16f81x.h>
#endif
#if defined(_16C923)	|| defined(_16C924)	||\
    defined(_16C925)	|| defined(_16C926) || defined(_16CR926)
	#include	<pic169xx.h>
#endif
#if defined(_16F913)	|| defined(_16F914)	||\
    defined(_16F916)	|| defined(_16F917)
	#include	<pic16f91x.h>
#endif
#if defined(_16F946)
	#include	<pic16f946.h>
#endif
#if	defined(_7C695X)
	#include	<pic7695x.h>
#endif
#if	defined(_16C99) || defined (_16C99C)
	#include        <pic1699.h>
#endif

#if  defined(_17C42) || defined(_17C42A) || defined(_17CR42) ||\
     defined(_17C43) || defined(_17CR43) || defined(_17C44)
	#include        <pic174x.h>
#endif

#if  defined(_17C752)   || defined(_17C756)     || defined(_17C756A)    ||\
     defined(_17C762)   || defined(_17C766)
        #include        <pic177xx.h>
#endif
#if  defined(_MCV08A)
        #include        <mcv08a.h>
#endif
#if  defined(_MCV14A)
        #include        <mcv14a.h>
#endif
#if  defined(_MCV18A) || defined(_MCV28A)
        #include        <mcv28a.h>
#endif
	
#define	CLRWDT()	asm("clrwdt")
#define	SLEEP()		asm("sleep")
#define NOP()		asm("nop")
#ifdef _PIC14E
#define RESET()		asm("reset");
#endif

#define	__CONFIG(x)	asm("\tpsect config,class=CONFIG,delta=2");\
			asm("\tdw "___mkstr(x))

// Programs the lower 4 bits per ID location
#define __IDLOC(w)       asm("\tpsect idloc,class=IDLOC,delta=2");\
			 asm("\tglobal\tidloc_word"); \
			 asm("idloc_word"); \
			 asm("\tirpc\t__arg," ___mkstr(w)); \
			 asm("\tdw 0&__arg&h"); \
			 asm("\tendm")

// Variant of IDLOC for those devices that permit programming of the lower 7 bits per ID location
#define __IDLOC7(a,b,c,d) asm("\tpsect idloc,class=IDLOC,delta=2");\
			 asm("\tglobal\tidloc_word"); \
			 asm("idloc_word"); \
			 asm("\tdw 0x7f&"___mkstr(a)); \
			 asm("\tdw 0x7f&"___mkstr(b)); \
			 asm("\tdw 0x7f&"___mkstr(c)); \
			 asm("\tdw 0x7f&"___mkstr(d))

#if !defined(_PIC14E) && !defined(EEADRL)
#define EEADRL EEADR
#endif
#if	EEPROM_SIZE > 0
#define __EEPROM_DATA(a, b, c, d, e, f, g, h) \
			 asm("\tpsect eeprom_data,class=EEDATA,delta=2,space=2"); \
			 asm("\tdb\t" ___mkstr(a) "," ___mkstr(b) "," ___mkstr(c) "," ___mkstr(d) "," \
				      ___mkstr(e) "," ___mkstr(f) "," ___mkstr(g) "," ___mkstr(h))
#endif

/***********************************************************************
 **** FLASH memory read/write/erase macros and function definitions ****
 ***********************************************************************
 * Notes:
 *	__FLASHTYPE == 0 defined in devices that can only read flash memory - cannot write eg. 16F777
 *	__FLASHTYPE == 1 defined in traditional devices that can write 1 word at a time eg. 16F877
 *	__FLASHTYPE == 2 defined in devices that can only write in 4 word blocks eg. 16F877A
 *	__FLASHTYPE == 3 defined in devices requiring 32-word block erasure before writing eg. 16F87
 *	__FLASHTYPE == undefined if device can neither read nor write program memory
 */
// macro FLASH_READ returns a word stored at a flash address
#if defined(__FLASHTYPE)
extern unsigned int flash_read(unsigned short addr);
#if	EEPROM_SIZE > 0
#define FLASH_READ(addr) \
	(EEADRL=(addr)&0xff,	\
	EEADRH=(addr)>>8,	\
	WREN=0,			\
	EECON1 |= 0x80,		\
	RD=1,			\
	DC=0,			\
	DC=0,			\
	(EEDATH << 8) | EEDATA)
#else	// FLASH_READ without EEPROM
#define FLASH_READ(addr) \
	(EEADRL=(addr)&0xff,	\
	EEADRH=(addr)>>8,	\
	RD=1,			\
	DC=0,			\
	DC=0,			\
	(EEDATH << 8) | EEDATA)
#endif
#endif	// end FLASH_READ

// macro FLASH_WRITE used when writing only one word of data
#if	__FLASHTYPE==2 || __FLASHTYPE==3
/*
 * This is not available in this version. Contact HI-TECH support for more information.
#define FLASH_WRITE(addr,data)	\
	do{	\
	unsigned short x=data;	\
	flash_copy((const unsigned short *)&x,1,addr);	\
	}while(0)

extern void flash_copy(const unsigned short * source_addr,unsigned char length,unsigned short dest_addr);
*/
#elif	__FLASHTYPE==1
#define FLASH_WRITE(addr, value) \
	EEADRL=((addr)&0xff);	\
	EEADRH=((addr)>>8);	\
	EEDATH=((value)>>8);	\
	EEDATA=((value)&0xff);	\
	EECON1 |= 0x80;		\
	WREN=1;			\
	EECON2 = 0x55;		\
	EECON2 = 0xaa;		\
	WR=1;			\
	asm("nop");		\
	asm("nop");		\
	WREN=0
//extern void flash_copy(const unsigned short * source_addr,unsigned char length,unsigned short dest_addr);
#endif	// end FLASH_WRITE

// macro FLASH_ERASE used to clear a 32-Byte sector of flash
#if	__FLASHTYPE==3
#define FLASH_ERASE(addr) \
       	while(WR)continue; \
	EEADRL=((addr)&0xFF); \
	EEADRH=((addr>>8)&0xFF); \
	EECON1=0x94; \
	CARRY=0;if(GIE)CARRY=1;GIE=0;\
	EECON2=0x55;EECON2=0xAA;WR=1; \
	asm("\tNOP"); \
	if(CARRY)GIE=1 

	// library function version
extern void flash_erase(unsigned short addr);
#endif	// end FLASH_ERASE

#include <eeprom_routines.h>

#ifdef __PICCPRO__
/****************************************************************/
/* Built-in delay routine					*/
/****************************************************************/
#pragma inline(_delay)
extern void _delay(unsigned long);
// NOTE: To use the macros below, YOU must have previously defined _XTAL_FREQ
#define __delay_us(x) _delay((unsigned long)((x)*(_XTAL_FREQ/4000000.0)))
#define __delay_ms(x) _delay((unsigned long)((x)*(_XTAL_FREQ/4000.0)))
#endif

/****************************************************************/
/****** Global interrupt enable/disable macro definitions *******/
/****************************************************************/
#ifdef _PIC16

#ifndef	ei
#define	ei()	(GLINTD = 0)	// interrupt disable bit
#endif 

#if defined(_17C42)
	#ifndef	di
		#define di()	{ do { GLINTD = 1; } while ( GLINTD == 0 ); }	// disable interrupt bit
	#endif 
#else
	#ifndef	di
		#define	di()	(GLINTD = 1)	// interrupt disable bit
	#endif 
#endif

#elif defined(_PIC14) || defined(_PIC14E)
	
#ifndef	ei
#define	ei()	(GIE = 1)	// interrupt enable bit
#endif  

#if defined(_14000) ||  defined(_16C61) || defined(_16C62) ||\
	defined(_16C63) || defined(_16C63A) || defined(_16C64) ||\
	defined(_16C65) || defined(_16C65B) || defined(_16C71) ||\
	defined(_16C73) || defined(_16C73B) || defined(_16C74) ||\
	defined(_16C74B) || defined(_16C84) || defined(_16C745) ||\
	defined(_16C765) || defined(_16LC74B)
	#ifndef	di
		#define di()	{ do { GIE = 0; } while ( GIE == 1 ); }	// disable interrupt bit
	#endif  
#else
	#ifndef	di
		#define	di()	(GIE = 0)	// interrupt enable bit
	#endif  
#endif

#endif

#ifdef __RESETBITS_ADDR
/* If '--runtime=+resetbits' is specified, these reflect the state
   of TO and PD, respectively, which are trashed by startup code. */
extern unsigned char __resetbits @ __RESETBITS_ADDR;
#ifndef _PIC16
extern bit __powerdown	@ ((unsigned)&__resetbits*8)+3;
extern bit __timeout	@ ((unsigned)&__resetbits*8)+4;
#else
extern bit __powerdown	@ ((unsigned)&__resetbits*8)+2;
extern bit __timeout	@ ((unsigned)&__resetbits*8)+3;
#endif
#endif
#endif	/* _PIC_H */
