#include <iostream>
#include <conio.h>
#include <math.h>
using namespace std;

int main()
{
int n,t2,k,t,i,j,a,o;
j=0;
cout << "	Vvedite chislo ot m= ";
	cin >>o;
cout << "	Vvedite chislo do n= ";
	cin >> n;


a=n;
cout<<"Vse prostie chisla v promezhutke do N : ";

for (t=o; t<=n; t++)
{
	t2=(t/2)+1;
	k=0;
	if (t2>2)
	{
	for (i=2; i<=t2; i++)
		{
			if (t%i==0) 
				k++;
		}
	}
	if (k==0) 
		{
			cout<<t<<", ",
			j++;
		}
}
cout<<" "<<endl;
cout<<"Kolichestvo prostih chisel do N : "<<j<<". "<<endl;
cout<<" "<<endl;

cout<<"Vse sovershennie chisla v promezhutke do N : ";
int p,e,w,q;

q=0;
for (p=o;p<=n;p++)
{
	w=0;
	for (e=1;e<p;e++)
	{
		if (p%e==0) 
			w=w+e;
	}
	if (p==w)
	{
		cout<<w<<", ";
		q++;
	}
}
cout<<" "<<endl;

if (q==0) cout<<" Net sovershennih chisel ";
cout<<"Kolichestvo soverchennih chisel do N : "<<q<<", ";

cout<<" "<<endl;
cout<<" "<<endl;

cout << "	Vvedite chislo ot m= ";
	cin >>o;
cout << "	Vvedite chislo do n= ";
	cin >> n;

cout<<"Vse shastlivie chisla v promezhutke do N : ";

int f,left,right,m,d,v,g,h,z,x;
m=0;
h=0;
left=0;
right=0;

for (f=o;f<=n;f++)
{
	g=f;
	d=4;
    while (d>1)
		{
			z=g/10;
			left=left+z;
			
			x=g%10;
			right=right+x;

			g=g/10;
			g=g%10;

			d--;
		}

	if (left==right) 
		{
			cout<<f<<", ";
			h++;
		}
	left=0;
	right=0;
}
cout<<" "<<endl;
cout<<"Kolichestvo shastlivih chisel : "<<h<<" ";

_getch();
return 0;
}