#include <iostream>//Проверить, не является ли матрица - "магической".
//ВОЗМОЖЕН КАК РУЧНОЙ ВВОД ТАК И РАНДОМНЫЙ! (для проверки)
#include <conio.h>
#include <cstdlib>
#include <ctime>

using namespace std;

int main()
{
	 srand(time(NULL));
	const int N=20;
	const int M=20;
	 int a[N][M];
	 int n,m,r,e;
   
	cout<<"Chislo n=";
	 cin>>n;

	cout<<endl;
	//RANDOM:
	  for (int i=0;i<n;i++)//Ввод массива A.
		for (int j=0;j<n;j++)
			a[i][j]=rand()%10; 

	//WRITING:
	/*for (int i=0;i<n;i++)//Ввод массива A.
		for (int j=0;j<n;j++)
			cin>>a[i][j];*/

	cout<<endl;
	cout<<"Massiv A: ";//Вывод массива A.
	cout<<endl;
		for (int i=0;i<n;i++)
		{
			for (int j=0;j<n;j++)
			cout<<a[i][j]<<" ";
			cout<<endl;
		}
	cout<<endl;

	int s=0,t=0;

	//Сосчитаем сумму для главной и побочной диагонали.
	for (int i=0;i<n;i++)
		{
			for (int j=0;j<n;j++)
			{
				if (i==j)
					s=s+a[i][j];
				if (j==(n-i-1))
					t=t+a[i][j];
			}
		}


	if (s==t)
	{//Для проверки строк.
		e=0;
		for (int i=0;i<n;i++)
		{
			r=0;
			for (int j=0;j<n;j++)
			{
				r=r+a[i][j];
			}
			if (r==s)
			{
				e++;
			}
		}
		//Для проверки столбцов.
		r=0;
		for (int j=0;j<n;j++)
		{
			r=0;
			for (int i=0;i<n;i++)
			{
				r=r+a[i][j];
			}
			if (r==s)
			{
				e++;
			}
		}
		if (e!=(2*n))
				{
					cout<<"Matrica A NE magicheskaya po stolbcam & strokam";
					cout<<endl;
				}
		else cout<<"Matrica A magicheskaya!";
	}
	else cout<<"Matrica A NE magicheskaya po glavnim diagonal'am!";
	cout<<endl;

	getch();
return 0;
}