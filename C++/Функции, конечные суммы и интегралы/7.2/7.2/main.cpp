#include <stdio.h>//Основная библиотека ввода и вывода.
#include <conio.h>//Библиотека задержки экрана после выполнения программы
#include <cstring>//Библиотека работы со строками

struct  Dates//Структура - Даты
{                 
	/* начало блока структуры*/  
	int day;     /* поле дня - числа от 1 до 31*/
	char month[3];  /* месяц: из-за лексикографического сравнения(неверного при сортировке) названий месяцев - месяцы будут 01, 02 и т.д.*/
	int year;     /*год, он может быть любым, хоть миллионным со времен динозавров */
}; 

//Функция обмена местами
void Swap(Dates mass[], int i, int j)
{
    Dates temp = mass[i];
    mass[i] = mass[j];
    mass[j] = temp;
}

int main()
{
	//Инициализируем пять произвольных дат. Можно было бы сделать в цикле с вводим scanf пользователем - но от нас требуется только сортировка дат.
	struct Dates dates[5]={{12,"12",2321},{23,"12",2321},{31,"06",2321},{12,"12",2321},{16,"09",2321}};
	int n=5;//Всего 5 дат
	//Печатаем даты до сортировки
	printf("Before sorting:\n");
	for(int i=0; i<n; i++)
	{
		printf("%i.%s.%i.\n",dates[i].day,dates[i].month,dates[i].year);
	}
	printf("\n\n\n");

	//Сортируем - метод "пузырька" - самые "легкие-молодые" по годам - уходят в начало массива         
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n-1; j++)
                {
					if (dates[j].year>=dates[j + 1].year)
                    {	
						Swap(dates, j, j + 1);
                    }
                }
            }
			//Сортируем по месяцам c одинаковыми годами
		if (n>1)
		{
			for(int x=1; x<n; x++)
			{
				int y=1;//Считаем ряд одинаковых лет-годов
				while(dates[x].year==dates[x-1].year)
				{
					y++;
					x++;
				}
				//Если мы дошли до конца такого цикла, то сортируем его
				if (y>1)
				{
					for (int i = x-y; i < x; i++)
					{
						for (int j = x-y; j < x-1; j++)
						{
							if (strcmp(dates[j].month,dates[j + 1].month)>=0)
							{	
								Swap(dates, j, j + 1);
							}
						}
					}
				}
			}

			//Теперь сортируем по месяцам
			for(int x=1; x<n; x++)
			{
				int y=1;//Считаем ряд одинаковых лет-годов с одинаковыми месяцами(но разными днями!)
				while(dates[x].year==dates[x-1].year && (strcmp(dates[x].month,dates[x-1].month)==0))
				{
					y++;
					x++;
				}
				//Если мы дошли до конца такого цикла, то сортируем его
				if (y>1)
				{
					for (int i = x-y; i < x; i++)
					{
						for (int j = x-y; j < x-1; j++)
						{
							if (dates[j].day>=dates[j + 1].day)
							{	
								Swap(dates, j, j + 1);
							}
						}
					}
				}
			}
		}
		else;//Тупиковая часть условия.
	
	//Печать результатов после сортировки
	printf("After sorting:\n");
	for(int i=0; i<n; i++)
	{
		printf("%i.%s.%i.\n",dates[i].day,dates[i].month,dates[i].year);
	}
	//Пауза - задержка
	getch();
	return 0;
}