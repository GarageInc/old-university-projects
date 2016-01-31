
#include <iostream>
#include <conio.h>
#include <math.h>//Для математической функции - извлечение корня и т.д.
#include <stdio.h>

using namespace std;
 

class Root//Виртуальный класс Корень
{
public:
	virtual void Roots() = 0;//Виртуальные методы. Для дальнейшего переопределения
	virtual void print() = 0;//Для печати
}
;

class Linear: public Root//Наследник - линейное уравнение
{
private:
	double _a,_b;//Переменные для линейного уравнения
public:
		//y=a*x+c
	 void print()//Для печати
	 {
		 cout <<"y = "<<_a<<"*x"<<" + "<<_b<<endl;
	 }

	 //Строим конструктор:
	 Linear(double a=0, double b=0): _a(a), _b(b)
	 {	 
	 }

	 void Roots()//Для дальнейшего переопределения
	 {		 
		 //Решаем уравнение и возвращаем корни
		 cout<<"Koren lineinogo uravnenija = "<<(-1)*_b/_a<<endl;
	 }

	~Linear () {};//Деструктор - очистка

}
;

class Binary: public Root//Наследник - линейное уравнение
{
private:
	double _a,_b,_c;//Переменные для линейного уравнения
public:
		//a*x^2+b*y^2+c=0
	 void print()//Для печати
	 {
		 cout <<_a<<"*x^2 + "<<_b<<"*x + "<<_c<<" = 0"<<endl;
	 }

	 //Строим конструктор:
	 Binary(double a=0, double b=0, double c=0): _a(a), _b(b), _c(c)
	 {	 
	 }

	 void Roots( )//Для дальнейшего переопределения - поиск корней
	 {		 
		 //Решаем уравнение и возвращаем корни
		double korenDiscr=pow(_b*_b-4*_a*_c,1/2);//Считаем корень из дискриминанта.
		if ((_b*_b-4*_a*_c)>0)
		{
			double x1=((-1)*_b-korenDiscr)/(2*_a);//Первый корень.
			double x2=((-1)*_b+korenDiscr)/(2*_a);//Второй корень.
			cout<<"Korni binarnogo uravnenija: x1 = "<<x1<<", x2 = "<<x2<<endl;
		}
		else
			if ((_b*_b-4*_a*_c)==0)//Значит - один корень
			{
				double x=((-1)*_b)/(2*_a);//Один корень
				cout<<"1 koren: x = "<<x;
			}
			else//Нет корней
			cout<<"Korney net!"<<endl;
	 }

	~Binary () {};//Деструктор - очистка
}
;

int main()// Задачу решаю "с конца".
{
	Root *Uravnenie[1];//Массив уравнений
	Uravnenie[0] = new Linear(1,2);//Линейное уравнение в массиве уравнение - самое первое
	//Выводим на экран:
	Uravnenie[0]->print();
	//Выведем на экран корень
	Uravnenie[0]->Roots();

	cout<<endl;

	Uravnenie[1] = new Binary(1,4,4);//Линейное уравнение в массиве уравнение - второе
	//Выводим на экран:
	Uravnenie[1]->print();
	//Выведем на экран корень-корни
	Uravnenie[1]->Roots();

	_getch();
	return 0;
}