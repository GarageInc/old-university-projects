#pragma once
#include <iostream>
using namespace std;

class Date
{
protected:
	unsigned int day, month, year;

public:
	//Конструктор
	Date(unsigned int Day, unsigned int Month, unsigned int Year);

	//Получить дни
	int GetDays();

	//Получить месяцы
	int GetMonths();

	//Получить годы
	int GetYear();

	//Изменить дни
	void SetDays(int i);

	//Изменить месяцы
	void SetMonths(int i);

	//Изменить годы
	void SetYears(int i);

	Date(string s);//Считываем дату из введённой строки формата 2013.12.14 или же формата 14.12.2013

	//Реализуем сравнение дат: переопределяем операторы
	bool operator == (Date d);

	bool operator != (Date d);
	//Сравниваем - меньше дата или больше(до или после)
	bool operator < (Date d);

	bool operator > (Date d);

	//Количество дней между датами
	static int DaysBetween(Date*d1, Date*d2);

	//Високосный год или нет
	bool VisokosYear();

	//Печать даты
	void print();

	//Вычитаем количество дней из даты
	void NewDateBeforeDays(unsigned int days);

	//Добавляем в дату N количество дней
	void NewDateAfterDays(unsigned int days);

	//Деструктор
	~Date();
};
