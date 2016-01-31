#include <iostream>
#include <algorithm>
#include <math.h>

#include "complex.cpp"

using namespace std;



Complex sum(Complex a, Complex b);//Получаем сумму. Реализация - ниже main
Complex dif(Complex a, Complex b);//Получаем разность
Complex mult(Complex a, Complex b);//Получаем перемножение

int main()
{
	setlocale(LC_ALL, "RUSSIAN");//Чтобы можно было использовать русский язык


/////////Работаем с комплексным числом:
	cout<<"РАБОТА С КОМПЛЕКСНЫМ ЧИСЛОМ:"<<endl;
	Complex a, b, c;
	char d;
	cout << "Введите два числа:" << endl;
	a.read();
	b.read();

	cout << "Введите знак действия:" << endl;
	cin >> d;//Вводишь знак + или - или *
	switch (d)
	{
	case '+':
	{
				c = sum(a, b);
				c.output();
	}
		break;
	case '-':
	{
				c = dif(a, b);
				c.output();
	}
		break;
	case '*':
	{
				c = mult(a, b);
				c.output();
	}
		break;
	}






	system("PAUSE");
	return 0;
	
}

