#ifndef _HTC_H_
#define _HTC_H_


/* Definitions for _HTC_EDITION_ values */
#define __LITE__ 0
#define __STD__ 1
#define __PRO__ 2

/* common definitions */

#define	___mkstr1(x)	#x
#define	___mkstr(x)	___mkstr1(x)



/* HI-TECH PICC / PICC-Lite compiler */
#if	defined(__PICC__) || defined(__PICCLITE__)
#include <pic.h>
#endif

/* HI-TECH PICC-18 compiler */
#if	defined(__PICC18__)
#include <pic18.h>
#endif

/* HI-TECH dsPICC compiler */
#if	defined(__DSPICC__)
#include <dspic.h>
#endif

/* HI-TECH HTKC compiler for Holtek MCUs */
#if	defined(_HTKC_)
#include <htk.h>
#endif

/* HI-TECH C for 8051 */
#if	defined(__HTC8051__)
#include <8051.h>
#endif

/* HI-TECH C for MSP430 */
#if	defined(__MSP430C__)
#include <msp430.h>
#endif

/* HI-TECH C for ARM */
#if	defined(__ARMC__)
#include <arm.h>
#endif

/* HI-TECH HTFSC compiler for Fortune MCUs */
#if	defined(__HTFSC__) || defined(_HTFSC_)
#include <htfsc.h>
#endif

/* HI-TECH C for PIC32 */
#if defined(__PICC32__)
#include <pic32.h>
#endif

/* HI-TECH C for PSOC */
#if defined(__HCPSOC__)
#include <psoc.h>
#endif

#endif
