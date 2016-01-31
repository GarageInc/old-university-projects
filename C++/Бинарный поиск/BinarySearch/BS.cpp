#include<iostream>//ƒвоичный поиск с функцией.
#include<conio.h>

using namespace std;
# define N 10000

//Function
int BinarySearch (int *x, int iskch, int n)
{
	int left=0, right=n-1;
	int centr=(right+left)/2;
	int nomer;

	while ((right-left)<=1)
	{
		centr=(right+left)/2;
		{if (x[centr]==iskch) nomer=centr;};
		{if (x[left]==iskch) nomer=left;};
		{if (x[right]==iskch) nomer=right;};

		if (iskch<x[centr])
			right=centr;
				else 
				left=centr;
	}
	return nomer;
}

int main()
{
	int x[N],n,iskch;//ѕеременные дл€ массива.
	
	cout<<"Vvedite kolichestvo chisel <100 =  ";
	cin>>n;

	//¬вод массива
	for(int i=0;i<n;i++)
		cin>>x[i];

	//¬вод числа
	cout<<"Vvedite iskomoe chislo X=";
	cin>>iskch;

	cout<<BinarySearch(x, iskch, n);
	
	getch();
	return 0;
}
