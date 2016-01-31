#include <iostream>//‘”Ќ ÷»я - €вл€етс€ ли одна строка частью другой.
#include <conio.h>
#include <iomanip>
#include <cstring>
using namespace std;

int Find(char *s, char *t)//явл€етс€ ли втора€ строка частью первой.
{
	int l1=strlen(s); int l2=strlen(t), sum=0, nomer=(-1);
	for (int i=0; i<(l1-l2); i++)
	{
		sum=0;
		for (int j=0; j<l2; j++)
		{
			if (t[j]==s[i+j])
				sum++;
		}
		{if (sum==l2) nomer=1;}
		{if (sum==l2) break;}
	}
	return (nomer);
}


int main()//√лавна€ функци€, присутствие еЄ ќЅя«ј“≈Ћ№Ќќ.
{
	char x[1000], y[500];

	//¬вод первой строки
	cout<<"Vvedite 1 stroku: "<<endl;
	cin.getline(x,1000);
	cout<<endl;

	//¬вод второй строки
	cout<<"Vvedite 2 stroku"<<endl;
	cin.getline(y,500);	

	if(Find(x,y)==(-1))//¬ызов функции
		cout<<"NO!";
		else cout<<"YES!";

getch();
return 0;
}


