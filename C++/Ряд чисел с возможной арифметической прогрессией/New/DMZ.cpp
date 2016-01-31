#include <iostream>
#include <conio.h>
using namespace std;


int main ()
{
	int a,b,d,n;
cin >> n >> a >> b;//Вводим данные по условию задачи, для упрощения вводится 4,2,4
	d=(b-a);
	
	n=n-2;//Данное действие исключает уже отобранные a&b

	while (n>0 && (b-a)==d)//Условие
{
	a=b;
	cin >> b;
	n=(n-1);//Сокращение n вследствие	уменьшения количества элементов
}
if (n==0) 
cout << "YES";
else cout << "NO";

return 0;

}