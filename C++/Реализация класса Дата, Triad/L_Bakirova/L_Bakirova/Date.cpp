#include "Date.h"

//Конструктор
Date::Date(unsigned int Day, unsigned int Month, unsigned int Year)
{
	this->day = Day;
	this->month = Month;
	this->year = Year;
}
//Получить дни
int Date::GetDays()
{
	return this->day;
}
//Получить месяцы
int Date::GetMonths()
{
	return this->month;
}
//Получить годы
int Date::GetYear()
{
	return this->year;
}

//Изменить дни
void Date::SetDays(int i)
{
	this->day = i;
}
//Изменить месяцы
void Date::SetMonths(int i)
{
	this->month = i;
}
//Изменить годы
void Date::SetYears(int i)
{
	this->year = i;
}

Date::Date(string s)//Считываем дату из введённой строки формата 2013.12.14 или же формата 14.12.2013
{
	//Делим решение на 2 части - смотрим, с какой стороны у нас число ГОД. Если с конца - один подсчет, если с начала - другой.
	int y = 0;
	while (s[y] != '.')
	{
		y++;
	}
	if (y == 4)//Значит формат даты типа 2013.12.14
	{
		unsigned int i = 0;
		string Year, Month, Day;
		while (s[i] != '.')//Считываем данные года
		{
			Year += s[i];
			i++;
		}
		i++;

		while (s[i] != '.')//Данные по месяцу
		{
			Month += s[i];
			i++;
		}
		i++;

		while (i<s.length())//Данные по дню
		{
			Day += s[i];
			i++;
		}

		this->day = atoi(Day.c_str());
		this->month = atoi(Month.c_str());
		this->year = atoi(Year.c_str());
	}
	else// иначе формат типа 14.12.2013
	{
		unsigned int i = 0;
		string Year, Month, Day;
		while (s[i] != '.')//Считываем данные года
		{
			Day += s[i];
			i++;
		}
		i++;

		while (s[i] != '.')//Данные по месяцу
		{
			Month += s[i];
			i++;
		}
		i++;

		while (i<(unsigned int)s.length())//Данные по дню
		{
			Year += s[i];
			i++;
		}

		this->day = atoi(Day.c_str());
		this->month = atoi(Month.c_str());
		this->year = atoi(Year.c_str());
	}

}

//Реализуем сравнение дат: переопределяем операторы
bool Date::operator == (Date d)
{
	return (this->day == d.day && this->month == d.month && this->year == year);
}
bool Date::operator != (Date d)
{
	return (this->day != d.day || this->month != d.month || this->year != year);
}
//Сравниваем - меньше дата или больше(до или после)
bool Date::operator < (Date d)
{
	return (this->day + this->month * 30 + this->year * 365 < d.day + d.month * 30 + d.year * 365);
}
bool Date::operator > (Date d)
{
	return (this->day + this->month * 30 + this->year * 365 > d.day + d.month * 30 + d.year * 365);
}

//Количество дней между датами
int Date::DaysBetween(Date*d1, Date*d2)
{
	//Считаем количество дней для первого даты
	int kol1 = d1->year * 365 + d1->month * 30 + d1->day;
	int kol2 = d2->year * 365 + d2->month * 30 + d2->day;

	return unsigned(kol1 - kol2);//Вернём разницу
}

//Високосный год или нет
bool Date::VisokosYear()
{
	bool y = false;
	if (this->year % 4 == 0) y = true;;
	if (this->year >= 400 && this->year % 100 == 0)
	{
		if (this->year % 400 != 0)
			y = false;;
	};;

	return y;
}

//Печать даты
void Date::print()
{
	cout << day << "." << month << "." << year << ".";
}

//Вычитаем количество дней из даты
void Date::NewDateBeforeDays(unsigned int days)
{
	unsigned int safeYear1 = this->year;
	//Делим полученное количество дней на годы-дни-месяцы
	unsigned int d, m, y, saveYear = this->year;
	y = days / 365;//Делим на годы
	days = days - y * 365;
	m = days / 30;//Делим на месяцы
	days = days - m * 30;
	d = days;//Последняя стадия - получили дни.

	//Начнём работать с самой крупной единицей - годом.
	this->year = year - y;
	//Затем - с месяцем
	if (this->month<m)//Если Количество вычитаемых месяцев - больше,чем в нашей исходной дате(количеству месяцев в исходной дате)
	{
		this->year--;//Уменьшаем наш исходный год на единицу
		this->month = 12 - (m - this->month);
	}
	else
	{
		if (this->month == m) //Если Количество вычитаемых месяцев - равно нашей исходной дате
		{
			this->year--;
			this->month = 12;
		}
		else
			this->month = this->month - m;
	}

	if (this->day<d)//Если Количество вычитаемых дней меньше, чем в нашей исходной дате
	{
		this->day = 30 - (d - this->day);
		if (this->month != 1)
		{
			this->year--;
			this->month = 12;
		}
		else
			this->month--;
	}
	else
	{
		if (this->day == d)
		{
			this->day = 30;
			if (this->month == 1)
			{
				this->year--;
				this->month = 12;
			}
			else
				this->month--;
		}
		else
			this->day = this->day - d;
	}
	//Теперь устроим провекру - а не вычитали ли мы среди вычитаемых лет високосные года? Если да - то прибавим +1 день  за каждый високосный год
	unsigned int safeYear2 = this->year;

	while (safeYear2<safeYear1)//Проверка на високосность и соответственно - прибавление +1 дня при положительной високосности года
	{
		bool y = false;
		if (safeYear2 % 4 == 0) y = true;;
		if (safeYear2 >= 400 && safeYear2 % 100 == 0)
		{
			if (safeYear2 % 400 != 0)
				y = false;;
		};;

		safeYear2++;
	}


}

//Добавляем в дату N-ое количество дней
void Date::NewDateAfterDays(unsigned int days)
{
	int safeYear1 = this->year;

	int kol = this->year * 365 + this->month * 30 + this->day;

	kol = kol + days;
	this->year = kol / 365;
	kol = kol - this->year * 365;
	this->month = kol / 30;
	kol = kol - this->month * 30; if (this->month == 0) this->month++;;
	this->day = kol; if (this->day == 0) this->day++;;

	//Попробуем поработать с високосными годами, который могу находится среди добавленных годов. Если они есть - нужно убавить на единицу
	int safeYear2 = this->year;

	while (safeYear1<safeYear2)
	{
		bool y = false;
		if (safeYear1 % 4 == 0) y = true;;
		if (safeYear1 >= 400 && safeYear1 % 100 == 0)
		{
			if (safeYear1 % 400 != 0)
				y = false;;
		};;


		if (y == true) NewDateBeforeDays(1);;

		safeYear1++;
	}
}


//Деструктор
Date::~Date()
{};
