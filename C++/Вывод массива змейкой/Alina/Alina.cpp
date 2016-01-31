#include <iostream>//Условие задачи Алины. Условие задачи работает, только если N-четное! (рисунок для нечетного чертить и смотреть)
#include <conio.h>
#include <iomanip>
using namespace std;

int main()
{
	const int N=100;
	const int M=100;
	 int a[N][M];
	 int n=4,u;
	/*cout<<"Vvedite chetnuyu razmernost' massiva"<<endl;
		cin>>n;*/
	u=n*n;
	cout<<"Zmeika: vvedite chislo elementov = "<<u<<": "<<endl;//Разложение по Алинкиной формуле ДЛЯ КВАДРАТНОЙ матрицы.
	
	for (int j=0; j==0  ; )//Первый столбец.
	{
		for (int i=0;i<n;i++)
		{
			cin>>a[i][j];
		}
		j++;
	}

	for (int j=1; j<n-1 ; j++)//Остальныe маленькие подстолбики кроме последнего столбца.
		for (int i=1;i<n;i++)
		{
			cin>>a[i][j];
		}

	//Для четного n:
	for (int j=n-1;j==n-1 ; )//Последний столбец.
	{
		for (int i=n-1; i>0; i--)
		{
			cin>>a[i][j];
		}
		j--;
	}

	for (int i=0; i==0 ; )//Первая строка без элемента с адресом a[0][0].
	{
		for (int j=n-1;j>0;j--)
		{
			cin>>a[i][j];
		}
		i++;
	}

	cout<<"Zmeika:"<<endl;
	//Выводим МАССИВ:
	for (int i=0; i<n; i++)
	{
		for (int j=0; j<n; j++)
		{
			cout<<setw(4)<<a[i][j];
		}
		cout<<endl;
	}
	getch();
return 0;
}