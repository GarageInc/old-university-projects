#include <htc.h>
#define _XTAL_FREQ 4000000 


__CONFIG(MCLREN & UNPROTECT & WDTDIS);

unsigned char i;  

void main() {
    TRISB = 0x00; 
    PORTB = 0x00; 
        for (;;)
        {    

            RB0 = 0;
            RB1 = 0;
            RB2 = 0;
            RB3 = 0;

            __delay_ms(100); 
            __delay_ms(100);
                  for ( i = 0; i < 8; i++) {
                  __delay_ms(100);
                  }
            RB3 = 1;
                  for ( i = 0; i < 8; i++) {
                  __delay_ms(100);
                  }
            RB3=0;
            RB2=1;
                  for ( i = 0; i < 8; i++) {
                  __delay_ms(100);
                  }
            RB2=1;
                  for ( i = 0; i < 8; i++) {
                  __delay_ms(100);
                  }
        }

}