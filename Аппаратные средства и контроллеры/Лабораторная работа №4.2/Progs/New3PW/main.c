#include <pic.h>
#include <pic165x>
#include "delay.c"

void main(void)
{
TRISA = 0;
PORTA = 0;
TRISB = 0;
PORTB = 0;

while(1)
	{
	DelayMs(100); // Сделаем паузу в полсекунды
	DelayMs(100);
	DelayMs(100);
	DelayMs(100);
	DelayMs(100);

	RB0 ^= 1; // инвертируем вывод 
	}
}