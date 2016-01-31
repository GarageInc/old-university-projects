#include <iostream>
#include <conio.h>//Представить число из 10ой в 2ичную систему и наоборот из заданной строки нулей и единиц.
#include <cstring>
#include <iomanip>// Для манипулятора - выравнивателя.
using namespace std;

void ConvertToBinary (int a)
{
	int en[32],j=0;
	while (a>1)
	{
		en[j]=a%2;
		j++;
		a=a/2;
	}
	en[j]=a;
	//Vivod:
	for (; j>=0; j--)
		cout<<en[j];
}

void ConvertToDecimal (char *s)
{
	int m=0;
	for (; *s; s++)
	{
		m=m*2+*s-'0';
	}
	cout<<m;
}

int main()
{
	int a;
	cout<<"Vvedite 10-ichnoe chislo = ";
		cin>>a;
	cout<<"Dvoichnoe predstavlenie = ";
	ConvertToBinary(a);
	cout<<endl<<endl;

	//Для строки:
	int const N=100;
	char s[N];
	int n=0;
	cout<<"Vvedite stroku-kod chisla v 2-oi sisteme: "<<endl;
		cin.getline(s,N);
	cout<<"Vivod v 10-om vide: ";
	ConvertToDecimal(s);


	getch();
	return 0;
}