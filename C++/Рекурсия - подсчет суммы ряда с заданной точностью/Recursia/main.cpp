#include<conio.h>
#include<iostream>
#include<math.h>

using  namespace std;

// Наша рекурсивная функция
double function(double x, double e, int stepen)
{
	// Считаем основное элемент
	double f=(pow(2.0,stepen)*pow(x,stepen))/(sqrt((1+4*stepen)*pow(5.0,stepen)));

	cout<<f<<endl;// Печатаем промежуточные результаты
	// Условие рекурсии - сравнение с погрешностью
	if (f>e)
	{
		return f+function(x,e,stepen+1);// Считаем новое значение по рекурсии
	}
	else
	{
		return f;// Иначе вернем конечное значение
	}
}

int main()
{
	setlocale(LC_ALL, "Russian");// Для чтения русского языка

	double x=0, e=0; // x - значение переменное, e - вводимая переменная

	cout<<"Введите значение 'x' = ";
	cin>>x;
	
	cout<<"Введите 'e'(погрешность) = ";
	cin>>x;

	cout<<"Результат = "<<function(x,e,0);// Вызываем функцию и печатаем результат. Передаем число, точность и начальную степень

	getch();// Пауза, чтобы консоль не закрылась
}