#include <iostream>//Разложить матрицу по змейке.
//ВОЗМОЖЕН КАК РУЧНОЙ ВВОД ТАК И РАНДОМНЫЙ! (для проверки)
#include <conio.h>
#include <cstdlib>
#include <ctime>
#include <iomanip>
using namespace std;

int main()
{
	 srand(time(NULL));
	const int N=100;
	const int M=100;
	 int a[N][M];
	 int n,m;
   
	cout<<"Chislo n=";
	 cin>>n;
	 
	
	cout<<endl;

	cout<<"Massiv A: ";//Вывод-вывод массива A.
	cout<<endl;
		for (int i=0;i<n;i++)
		{
			for (int j=0;j<n;j++)
			{
			a[i][j]=rand()%10; 
			cout<<setw(3)<<a[i][j]<<" ";
			}
			cout<<endl;
		}
	cout<<endl;
	m=n*n;
		cout<<"Zmeika:";//Разложение по змейке
	cout<<endl;

	int k, x, u;
	u=n/2;
	x=n;
	k=0;

	if (n%2==0)
	{
	u=n/2;
	for (int e=0; e<u; e++)
		{
		for (int i=k, s=1; s>0 ; s-- )//Первая строка.
		{
			for (int j=k;j<x;j++)
				cout<<a[i][j]<<", ";
		}
		for (int j=x-1, s=1; s>0 ; s-- )//Последний столбец.
		{
			for (int i=k+1;i<x;i++)
				cout<<a[i][j]<<", ";
		}
		for (int i=x-1, s=1; s>0 ; s-- )//Последняя строка.
		{
			for (int j=x-2;j>=k;j--)
				cout<<a[i][j]<<", ";
		}
		for (int j=k, s=1; s>0 ; s-- )//Первый столбец.
		{
			for (int i=x-2;i>k;i--)
				cout<<a[i][j]<<", ";
		}
		k++;
		x--;
		cout<<endl;
		}	
	}
	if ((n%2)!=0)
	{
	u=(n/2)+1;
	for (int e=0; e<u; e++)
		{
		for (int i=k, s=1; s>0 ; s-- )//Первая строка.
		{
			for (int j=k;j<x;j++)
				cout<<a[i][j];
		}
		for (int j=x-1, s=1; s>0 ; s-- )//Последний столбец столбец.
		{
			for (int i=k+1;i<x;i++)
				cout<<a[i][j];
		}
		for (int i=x-1, s=1; s>0 ; s-- )//Последняя строка.
		{
			for (int j=x-2;j>=k;j--)
				cout<<a[i][j];
		}
		for (int j=k, s=1; s>0 ; s-- )//Первый столбец столбец.
		{
			for (int i=x-2;i>k;i--)
				cout<<a[i][j];
		}
		k++;
		x--;
		cout<<endl;
		}	
	}
	getch();
}