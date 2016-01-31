#include <iostream>
#include <conio.h>
#include <cmath>
using namespace std;


struct Point
{
	int x,y;
	Point(){x=0; y=0;}
	Point(int a,int b){x=a;y=b;}
	void input();
	void print();
	double dlina(Point );
};

void Point::input()
{
	cout<<"Vvod koordinat: ";
	cin>>x>>y;
}

void Point::print()
{
	cout<<"Vivod koordinat tochki "<<"X= "<<x<<" & "<<"Y="<<y<<endl;
}

double Point::dlina(Point p)
{
	return sqrt ( (double)(x-p.x)*(x-p.x) + (y-p.y)*(y-p.y) );
}

double modul(double s)
{
	if (s<0) return s*(-1);
		else return s;
}

//Ôóíêöèÿ ïëîùàäè:
void Analiz()
{
	int n,a,b;
	int const N=100;
	double s=0; //S - ïëîùàäü
	Point vershinaA, vershinaB;

	cout<<"Vvedite kolichestvo vershin N=";
		cin>>n;
	cout<<endl<<"Koordinati:"<<endl<<endl;

	vershinaA.input();
	//Ñîõðàíèì ïåðâóþ ââåäåííóþ âåðøèíó N-óãîëüíèêà:
	a=vershinaA.x;
	b=vershinaA.y;
	
	int schetA=0,schetB,q=n-1,lenght[N], ugol[N];

	for (int i=0; i<n-1; i++ )
	{
		vershinaB.input();
		s=s+(vershinaA.x * vershinaB.y - vershinaA.y * vershinaB.x);
		
		lenght[i]= pow (((vershinaA.x-vershinaB.x)*(vershinaA.x-vershinaB.x) + (vershinaA.y-vershinaB.y)*(vershinaA.y-vershinaB.y)),0.5);//Äëèíû âãîíÿþòñÿ â ìàññèâ.

		ugol[i]=	//Óãëû â ðàäèàíàõ âãîíÿþòñÿ â ìàññèâ.


		vershinaA.x=vershinaB.x;
		vershinaA.y=vershinaB.y;
	}
	
	lenght[n-1]=pow (((vershinaA.x-a)*(vershinaA.x-a) + (vershinaA.y-b)*(vershinaA.y-b)),0.5);
	ugol[n-1]=	;

	for(int i=0;i<n-1;i++)
	{

	}

	s=(s+(vershinaA.x * b - vershinaA.y * a))/2;
	cout<<endl<<"Ploshad figuri = "<<modul(s);






}


//Íàïèñàòü ïðîâåðêó ïðàâèëüíîñòè ìíîãîóãîëüíèêà.
int main()
{
	//ÏÐÎÂÅÐÊÀ ÍÀÕÎÆÄÅÍÈß ÄËÈÍÛ ÌÅÆÄÓ ÄÂÓÌß ÒÎ×ÊÀÌÈ:
	/*Point p,p1;
	
	cout<<"Nahodim dlinu mezhdu dvum'a tochkami: "<<endl;
	p.input();
	p.print();
	
	p1.input();
	p1.print();

	cout << "Dlina = " << p.dlina (p1)<< endl<<endl;*/

	//ÍÀÉÒÈ ÏËÎÙÀÄÜ N-ÓÃÎËÜÍÈÊÀ:
	Analiz();

	getch();
	return 0;
}