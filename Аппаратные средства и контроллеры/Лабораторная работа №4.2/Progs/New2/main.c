#include <pic.h>
#include "delay.c"

void main(void)
{
TRISA = 0;
PORTA = 0;
TRISB = 0;
PORTB = 0;

while(1)
{
DelayMs(250); // Сделаем паузу в полсекунды
DelayMs(250);
RB0 ^= 1; // инвертируем вывод 
}
}