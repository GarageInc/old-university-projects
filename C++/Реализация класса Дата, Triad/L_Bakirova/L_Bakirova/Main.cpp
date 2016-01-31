//Стандартно считаем, что есть високосные и невисокосные года

#include <iostream>
#include <conio.h>
#include "Date.h"


using namespace std;

int main()
{
	int const N = 3;
	Date *dates[N];//Создаем массив дат
	dates[0] = new Date(12, 2, 2012);//Инициализируем первую дату по массиву. По порядку она - нулевая, начальная дата. Самая первая.
	cout << "Examples:" << endl;//Печатаем примеры:
	dates[0]->print();//Печатаем для проверки - выводим в консоль
	cout << endl << endl;

	dates[1] = new Date("2016.12.14");//Новая, вторая дата в массиве.
	dates[1]->print();//Печатаем для проверки - выводим в консоль
	cout << endl << endl;

	dates[2] = new Date("14.12.2013");//Новая, третья дата в массиве.
	dates[2]->print();//Печатаем для проверки - выводим в консоль
	cout << endl;
	//Итого показана реализация трех видов ввода даты
	cout << endl << "1." << endl;

	//Вычитаем из даты определённое количество дней - случайно выбрали число -N дней, например. В нашем случае - 1 день и -366 дней
	int d = 2;//Количество дней
	cout << "Old Data: "; dates[2]->print(); cout << endl;//Выведем сначала старую дату, для сравнения

	dates[2]->NewDateBeforeDays(d);
	cout << endl << "   1) New date -" << d << " day: " << endl;
	dates[2]->print();//Печатаем новую дату

	cout << endl << endl;

	d = 365 + 1;
	dates[2]->NewDateBeforeDays(d);
	cout << endl << "   2) New date -" << d << " days: " << endl;
	dates[2]->print();//Печатаем новую дату
	cout << endl << endl<< "2.";

	//Добавим в дату N дней
	cout << endl << "Old date :"; dates[2]->print();
	dates[2]->NewDateAfterDays(123);
	cout << endl << "   New date after 123 days: "; dates[2]->print();

	cout << endl << endl << "3." << endl;


	//Cравним даты
	//1ая пара
	if (dates[0] == dates[1])
	{
		cout << "Dates ==: "; dates[0]->print(); cout << " and "; dates[1]->print();
	}
	else
	{
		cout << "Dates !=: "; dates[0]->print(); cout << " and "; dates[1]->print();
	}

	cout << endl;

	//2ая пара
	dates[3] = dates[1];//Создали копию даты - для сравнения :)
	if (dates[1] == dates[3])
	{
		cout << "Dates ==: "; dates[1]->print(); cout << " and "; dates[3]->print();
	}
	else
	{
		cout << "Dates !=: "; dates[1]->print(); cout << " and "; dates[3]->print();
	}

	cout << endl << endl<< "4." << endl;


	int i = Date::DaysBetween(dates[1], dates[2]);//Считаем количество дней между датами
	//Количество дней между датами:
	cout << "Days between ";
	dates[1]->print();
	cout << " and ";
	dates[2]->print();
	cout << "    = " << i;

	cout << endl << endl << "5." << endl;

	//Определим високосность дат
	for (int i = 0; i<N; i++)
	{
		if (dates[i]->VisokosYear())
		{
			dates[i]->print(); cout << "is Visokos year." << endl;
		}
		else
		{
			dates[i]->print(); cout << "is NOT Visokos year." << endl;
		}
	}


	//Делаем задержку экрана:
	_getch();
	return 0;
}