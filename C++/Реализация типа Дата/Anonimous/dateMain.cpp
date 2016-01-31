
#include <iostream>
#include <conio.h>

using namespace std;


class Date
{
private:
	unsigned int day, month, year;
public:

	//Конструктор
	Date(unsigned int Day=0,unsigned int Month=0,unsigned int Year=0)
	{
		this->day=Day;
		this->month=Month;
		this->year=Year;
	}
	//Получить дни
	int GetDays()
	{
		return this->day;
	}
	//Получить месяцы
	int GetMonths()
	{
		return this->month;
	}
	//Получить годы
	int GetYear()
	{
		return this->year;
	}
	
	//Изменить дни
	void SetDays(int i)
	{
		this->day=i;
	}
	//Изменить месяцы
	void SetMonths(int i)
	{
		this->month=i;
	}
	//Изменить годы
	void SetYears(int i)
	{
		this->year=i;
	}

	Date(string s)//Считываем дату из введённой строки формата 2013.12.14 или же формата 14.12.2013
	{
		//Делим решение на 2 части - смотрим, с какой стороны у нас число ГОД. Если с конца - один подсчет, если с начала - другой.
		int y=0;
		while(s[y]!='.')
		{
			y++;
		}
		if (y==4)//Значит формат даты типа 2013.12.14
		{
			int i=0;
			string Year, Month, Day;
			while (s[i]!='.')//Считываем данные года
			{
				Year+=s[i];
				i++;
			}
			i++;

			while (s[i]!='.')//Данные по месяцу
			{
				Month+=s[i];
				i++;
			}
			i++;

			while (i<s.length())//Данные по дню
			{
				Day+=s[i];
				i++;
			}		
		
			day=atoi( Day.c_str() );
			month=atoi( Month.c_str() );
			year=atoi( Year.c_str() );
		}
		else// иначе формат типа 14.12.2013
		{
			int i=0;
			string Year, Month, Day;
			while (s[i]!='.')//Считываем данные года
			{
				Day+=s[i];
				i++;
			}
			i++;

			while (s[i]!='.')//Данные по месяцу
			{
				Month+=s[i];
				i++;
			}
			i++;

			while (i<s.length())//Данные по дню
			{
				Year+=s[i];
				i++;
			}		
		
			day=atoi( Day.c_str() );
			month=atoi( Month.c_str() );
			year=atoi( Year.c_str() );
		}

	}

	//Реализуем сравнение дат: переопределяем операторы
	bool operator == (Date d)
	{
		return (this->day==d.day && this->month==d.month && this->year==year);
	}
	bool operator != (Date d)
	{
		return (this->day!=d.day || this->month!=d.month || this->year!=year);
	}

	//Количество дней между датами
	static int DaysBetween(Date*d1, Date*d2)
	{
		//Считаем количество дней для первого даты
		int kol1=d1->year*365+d1->month*30+d1->day;
		int kol2=d2->year*365+d2->month*30+d2->day;

		return unsigned (kol1-kol2);//Вернём разницу
	}	

	//Печать даты
	void print()
	{
		cout<<day<<"."<<month<<"."<<year<<".";
	}
	
	//Вычитаем количество дней из даты
	void NewDateBeforeDays(unsigned int days)
	{
		//Делим полученное количество дней на годы-дни-месяцы
		int d,m,y;
		y=days/365;//Делим на годы
			days=days-y*365;
		m=days/30;//Делим на месяцы
			days=days-m*30;
		d=days;//Последняя стадия - получили дни.
		
		//Начнём работать с самой крупной единицей - годом.
		this->year=year-y;
		//Затем - с месяцем
		if (this->month<m)//Если Количество вычитаемых месяцев - больше,чем в нашей исходной дате(количеству месяцев в исходной дате)
		{
			this->year--;//Уменьшаем наш исходный год на единицу
			this->month=12-(m-this->month);
		}
		else
		{
			if (this->month==m) //Если Количество вычитаемых месяцев - равно нашей исходной дате
			{
				this->year--;
				this->month=12;
			}
			else
				this->month=this->month-m;
		}

		if (this->day<d)//Если Количество вычитаемых дней меньше, чем в нашей исходной дате
		{
			this->day=30-(d-this->day);
			if (this->month!=1)
				{
					this->year--;
					this->month=12;
				}
			else
				this->month--;
		}
		else
		{
			if (this->day==d)
			{
				this->day=30;
				if (this->month==1)
				{
					this->year--;
					this->month=12;
				}
				else
					this->month--;
			}
			else
				this->day=this->day-d;
		}
	}

	//Деструктор
	~Date()
	{}
};


int main()
{
	int const N=3;
	Date *dates[N];//Создаем массив дат
	dates[0]=new Date(12,2,1933);//Инициализируем первую дату по массиву. По порядку она - нулевая, начальная дата. Самая первая.
	cout<<"Examples:"<<endl;//Печатаем примеры:
	dates[0]->print();//Печатаем для проверки - выводим в консоль
	cout<<endl<<endl;

	dates[1]=new Date("2013.12.14");//Новая, вторая дата в массиве.
	dates[1]->print();//Печатаем для проверки - выводим в консоль
	cout<<endl<<endl;

	dates[2]=new Date("14.12.2013");//Новая, третья дата в массиве.
	dates[2]->print();//Печатаем для проверки - выводим в консоль
	cout<<endl<<endl;
	//Итого показана реализация трех видов ввода даты

	//Вычитаем из даты определённое количество дней - случайно выбрали число -N дней, например. В нашем случае - 1 день и -366 дней
	int d=2;//Количество дней
	cout<<"Old Data: "; dates[2]->print(); cout<<endl;//Выведем сначала старую дату, для сравнения

	dates[2]->NewDateBeforeDays(d);
	cout<<endl<<"   1) New date -"<<d<<" day: "<<endl;
	dates[2]->print();//Печатаем новую дату

	cout<<endl<<endl;

	d=365+1;
	dates[2]->NewDateBeforeDays(d);
	cout<<endl<<"   2) New date -"<<d<<" days: "<<endl;
	dates[2]->print();//Печатаем новую дату

	cout<<endl<<endl;


	//Cравним даты
	//1ая пара
	if (dates[0]==dates[1])
	{
		cout<<"Dates ==: "; dates[0]->print(); cout <<" and "; dates[1]->print(); 
	}
	else
	{
		cout<<"Dates !=: "; dates[0]->print(); cout <<" and "; dates[1]->print();
	}

	cout<<endl<<endl;

	//2ая пара
	dates[3]=dates[1];
	if (dates[1]==dates[3])
	{
		cout<<"Dates ==: "; dates[1]->print(); cout <<" and "; dates[3]->print(); 
	}
	else
	{
		cout<<"Dates !=: "; dates[1]->print(); cout <<" and "; dates[3]->print();
	}

	cout<<endl<<endl;

	int i=Date::DaysBetween(dates[1],dates[2]);//Считаем количество дней между датами
	//Количество дней между датами:
	cout<<"Days between "; 
		dates[1]->print(); 
		cout << " and "; 
		dates[2]->print(); 
		cout << "    = "<<i;



	_getch();
	return 0;
}