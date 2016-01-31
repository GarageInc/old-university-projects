#pragma once
#include <iostream>
using namespace std;

class Triad
{
protected:
	unsigned int day, month, year;
public:
	Triad(int day = 0, int month = 0, int year = 0)
	{
		this->day = day;
			this->month = month;
		this->year = year;
	};
	//Получить дни
	virtual int GetDays()=0;

	//Получить месяцы
	virtual int GetMonths()=0;

	//Получить годы
	virtual int GetYear()=0;

	//Изменить дни
	virtual void SetDays(int i)=0;

	//Изменить месяцы
	virtual void SetMonths(int i)=0;

	//Изменить годы
	virtual void SetYears(int i)=0;

	//Печать даты
	virtual void print() = 0;

	~Triad() {};
};

class Date: public Triad
{


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
