#include <iostream>//Проверка строки на полиндромность при наличии неучитывыемых при анализе строки пробелов. Ручной ввод через гетлайн.
#include <conio.h>
#include <iomanip>
#include <cstdlib>
#include <ctime>
using namespace std;

char s[100];
int main()
{
	srand(time(NULL));
	int n,k=0,m=0,l;

	cout<<"Vvedite stroku: "<<endl<<endl;
	cin.getline(s,100);//Вводим строку
	n = strlen (s);//Узнаем её длину.
	
	cout<<"Vivod stroki: "<<endl;//Для визуальности выведем строку.
	for (int i=0; i<n; i++)
	{
		cout<<s[i];		
	}
	cout<<endl;

	for (int i=0; i<n; i++)// Сосчитаем количество пробелов.
	{
		if (s[i]==' ') m++;		
	}

	l=n-m;

	for (int i=0, j=n-1; i<=j; i++, j--)//Проверка на палиндромность.
	{
		if (s[i]==' ')
		{
			while (s[i]==' ')
				{
					i++;		
				}
		}
		if (s[j]==' ')
		{
			while (s[j]==' ')
				{
					j--;		
				}
		}
		if (s[i]==s[j]) k++;
	}
	cout<<endl;

	//Вывод о палиндромности:
	if (n%2==0)
	{
		if ((l/2)==k) cout<<"Stroka palindrom";
			else cout<<"Stroka NE palindrom";
	}
	else 
	{
		if (n%2==1)
		{
			if (((l/2)+1)==k) cout<<"Stroka palindrom";
			else cout<<"Stroka NE palindrom";
		}
	}
	getch();
	return 0;
}