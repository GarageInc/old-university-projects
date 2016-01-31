#include <iostream>//Нужно вывести число с наибольшим количеством делителей в заданном диапазоне (само число НЕ делитель!).
#include <conio.h>
using namespace std;


int main()
{
	int n,m,k=0,KolDel=0,chislo,w;

	cout<<"Vvedite diapazon A (nachalo): "<<endl;
		cin>>n;
	cout<<"Vvedite diapazon B (konec): "<<endl;
		cin>>m;
	cout<<endl;

	for (int j=n; j<=m; j++)
	{
		w=j/2+1;
		KolDel=0;
		for(int i=1; i<w; i++)
		{
			if (j%i==0) 
				KolDel++;
		}
		if (KolDel>k) 
		{
			k=KolDel,
			chislo=j;
		}
	}
	cout<<"Chislo iz zadannogo diapazona s MAXkolichestvom deliteley = "<<chislo<<", KolDeliteley = "<<k;
	getch();
	return 0;
}