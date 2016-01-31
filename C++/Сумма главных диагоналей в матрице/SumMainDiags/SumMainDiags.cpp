#include <iostream>//Выписать суммы главных диагоналей.
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
	 int n;
   
	cout<<"Chislo n=";
	 cin>>n;

	cout<<endl;
	cout<<endl;

	for (int i=0;i<n;i++)//Ввод массива A.
		for (int j=0;j<n;j++)
			a[i][j]=rand()%10;

	cout<<"Massiv A: ";//Вывод массива A.
	cout<<endl;
		for (int i=0;i<n;i++)
		{
			for (int j=0;j<n;j++)
			cout<<a[i][j]<<" ";
			cout<<endl;
		}
	cout<<endl;

	int q,k,s,y,u,w=0,r;//y=i,u=j,
	q=n+n-1;
	k=0;

	for (int h=0; h<n; h++)
	{
		w++;
		s=0;
		
		k++;
		r=k;
		
		y=0;
		u=n-k;
		while (r>=1)
		{
			s=s+a[y][u];
			u++;
			y++;
			r=r-1;
		}
		cout<<"Summa glavnoi diagonali nomer "<<w<<" ravna "<<s;
		cout<<endl;
	}
	
	k=0;
		for (int h=0; h<n-1; h++)
	{
		w++;
		s=0;
		
		k++;
		r=k;
		
		y=0;
		u=k;
		while (u<=n-1)
		{
			s=s+a[u][y];
			u++;
			y++;
			r=r-1;
		}
		cout<<"Summa glavnoi diagonali nomer "<<w<<" ravna "<<s;
		cout<<endl;
	}

	getch();
return 0;
}