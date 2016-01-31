#include <iostream>
#include <conio.h>
using namespace std;

int main()
{
int k;
double t,s,l,e,x;
cout<<"Chislo x=";
	cin>>x;
cout<<"Chislo e=";
	cin>>e;

k=1;
t=x/k;
l=t;
{
if (t<0)
	l=t*(-1);
}
s=t;

while (l>e)
{
	k=k+2;
	t=t*((-1)*x*x)/(k*(k-1));
	l=t;
	{
		if (t<0)
				l=(-1)*t;
	}
	s=s+t;
}

cout<<"Summa(sinus) = "<<s<<endl;
_getch();
return 0;
}