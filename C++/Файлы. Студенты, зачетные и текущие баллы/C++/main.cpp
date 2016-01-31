#include <stdio.h>
#include <string.h>
#include <conio.h>
#include <stdlib.h>
#include <time.h>
#include <iostream>

using namespace std;

const int N=100;//Количество студентов максимально

#define DL_FIO 20 /* длина поля ФИО в файле */
#define DL_GROUP 10 /* длина поля  предмета */

struct students2 /* структура СТУДЕНТЫ для записи входного файла */
{	
	char fio[DL_FIO]; /* фамилия и инициалы студента*/
    unsigned balls1; /*  баллы за предмет №1 по списку*/
};

struct students1 /* структура СТУДЕНТЫ для записи входного файла */
{	
	char fio[DL_FIO]; /* фамилия и инициалы студента*/
	char predmet[DL_FIO]; /* фамилия и инициалы студента*/
    unsigned balls1; /*  баллы за предмет №1 по списку*/
};

struct students1 stud1[N];
struct students2 stud2[N];

/* прототипы функций */
void Reading(FILE *f1, FILE *f2);
void SortingByBalls(students1 stud[N],int start, int end);
void SortingByGroup(students1 stud[N],int start, int end);
void SumBalls(students1 stud_1[N], int n, students1 stud_2[N], int m);//Передаются массивы, которые будут обрабатываться. Это студенты с текущими и зачетными баллами соответствнно с их количеством(n и m).
//Т.к. это база данных, приме по умолчанию, что студент в файле не дублируется. Но он может отсутствовать в списке студентов с зачетными баллами, т.к.
//это может быть случай, когда он не сдал зачет. Или был пропущен на сдаче.

/*----------------------*/
/* главная функция */
/*----------------------*/
int main()
{
	setlocale(LC_ALL, "Russian");//Русский язык чтобы воспринимался
    FILE *f1, *f2; /* ссылка на входной файл */
    
    f1 = fopen("file1.txt", "r");
	f2 = fopen("file2.txt", "r");

    if (f1==NULL)
    {
        puts ("Файл file1.txt не найден! Создайте файл!");
        getch();
        return 1;
    };;
	if (f2==NULL)
    {
        puts ("Файл file2.txt не найден! Создайте файл!");
        getch();
        return 1;
    };;

	//Считываем данные из файлов и запускаем всю работу. См эту функцию
	Reading(f1,f2);

	//Прекращаем работу с файлами
    fclose(f1);
	fclose(f2);

	system("pause");//Пауза
    return 0;
}
/*--------------------------------------*/
/*Чтение из файлов
/*--------------------------------------*/
void Reading(FILE *f1, FILE *f2)
{
    int n = 0, m = 0; /*будем считать количество студентов с текущими баллами и зачетными*/
    
	//Записываем в массивы студентов stud1, stud2 записи из файлов(считывая по строке)	
	cout<<"Первый файл с текущими баллами:"<<endl;
	while (fscanf (f1, "%s %s %u", stud1[n].fio, stud1[n].group, &(stud1[n].balls)) != EOF) {
		cout << n+1 << ". " << stud1[n].fio<<" "<< stud1[n].group << " " <<stud1[n].balls<<endl;
		n++;
	}
	cout<<endl<<"Второй файл с зачетными баллами:"<<endl;
	while (fscanf (f2, "%s %s %u", stud2[m].fio, stud2[m].group, &(stud2[m].balls)) != EOF) {
		cout << m+1 << ". " << stud2[m].fio<<" "<< stud2[m].group << " " <<stud2[m].balls<<endl;	
		m++;
	}
	//Сортируем содержимое обеих файлов по группаm(1ый этап) и по баллам(2ой этап)
	//1ый этап
	SortingByBalls(stud1,0,n);//Передаем элементы, с какого по какой сортировать.
	SortingByBalls(stud2,0,m);//Передаем элементы, с какого по какой сортировать.
	
	//Выводим результат
	cout<<endl<<endl<<endl<<"Отсортированный 1ый файл только по баллам:"<<endl;
	for(int i=0; i<n; i++)
		cout << i+1 << ". " << stud1[i].fio<<" "<< stud1[i].group << " " <<stud1[i].balls<<endl;	

	//2ой этап
	//Теперь сортируем по группам внутри одинаковых баллов
	for(int i=0; i<n; i++)
	{
		//Идем по людям, которые в одной группе и сортируем внутри группы
		int start=i;//Начало группы
		int end=i+1;
		while(end<n && stud1[start].balls==stud1[end].balls)
			end++;
		if (end-start==1) continue;//Не имеет смысла сортировать, если 1 человек
		i=end;//Фиксируем конец группы
		//Запускаем функцию сортировки внутри баллов по группе:
		SortingByGroup(stud1,start,end);
	}

	cout<<endl<<endl<<"Отсортированный 1ый файл по баллам И ПО ГРУППАМ ТАК ЖЕ(если одинаковые баллы - сортировка по группам):"<<endl;
	for(int i=0; i<n; i++)
		cout << i+1 << ". " << stud1[i].fio<<" "<< stud1[i].group << " " <<stud1[i].balls<<endl;

	//Теперь реализуем поиск людей во втором файле и суммирование зачетных и текущих баллов, ПРИ СОВПАДЕНИИ ГРУППЫ И ФАМИЛИИ!
	//Итог выводится в файл file3.txt
	SumBalls(stud1,n,stud2,m);

}


//Сортировка по баллам (СОРТИРОВКА МЕТОДОМ ПУЗЫРЬКА)
void SortingByBalls(students1 *stud, int start, int end)
{
    for(int i = start; i < end - 1; i++)
    for(int j = 0; j < end - i - 1; j++)
		if(stud[j].balls > stud[j + 1].balls)
		{
            students1 tmp = stud[j];
			stud[j] = stud[j + 1];
			stud[j + 1] = tmp;
		}
}

//Сортировка по группам
void SortingByGroup(students1 *stud, int start, int end)
{
    for(int i = start; i < end - 1; i++)
    for(int j = 0; j < end - i - 1; j++)
		if(strcmp(stud[j].group,stud[j + 1].group)>0)
		{
            students1 tmp = stud[j];
			stud[j] = stud[j + 1];
			stud[j + 1] = tmp;
		}
}

//Суммирование текущих и зачетных баллов, их вывод в консоль(результат) и в файл file3.txt
void SumBalls(students1 *stud_1, int n, students1 *stud_2, int m)
{
	//Можно было провести предварительную сортировку, но т.к. студенты могут отсутствовать во втором списке с зачетными баллами - это приведет к ошибке
	struct students1 stud3[N];//Создаем студентов с суммированием баллов
	int kol_stud=0;//Будем считать количество студентов, которые есть в обоих списках и у которых суммируются баллы

	for(int i=0; i<n; i++)
		for(int j=0; j<m; j++)
		{
			//Если мы нашли студента по ФИО и группе, то суммируем баллы
			//Студент не может дублироваться в одном файле! Это нарушает принцип построения Баз данных, т.к. у каждого студента должен быть свой уникальный ID, не может быть двух сущностей с полностью одинаковыми свойствами
			if(strcmp(stud1[i].fio,stud2[j].fio)==0 && strcmp(stud1[i].group,stud2[j].group)==0)
			{
				//Суммируем баллы
				stud3[kol_stud]=stud1[i];
				stud3[kol_stud].balls+=stud2[j].balls;
				kol_stud++;//Мы добавили студента -> увеличили счетчик
				break;//Дальше искать смысла нет,т.к. студент в файле не должен дублироваться - его "клона" нет
			}
		}

	//Выводим результат по порядку(т.к. основной выбор шел по уже отсортированным данным первого файла
	cout<<endl<<endl<<"Новый список студентов с суммированием по баллам(текущие баллы+зачетные баллы) /n суммирование проводилось по тем студентам, которые есть в обеих группах:"<<endl;
	//Создаем файл для записи
	FILE *f; /* ссылка на входной файл */
    
    f = fopen("file3.txt", "w");
	
    if (f==NULL)
    {
        puts ("Файл file3.txt не найден! Создайте файл!");
        getch();
        return;
    };;
	for(int i=0; i<kol_stud; i++)
	{
		//Печатаем на экран
		cout << i+1 << ". " << stud3[i].fio<<" "<< stud3[i].group << " " <<stud3[i].balls<<endl;
		//Параллельно выводим в файл
		fprintf (f, "%s %s %i\n", stud3[i].fio, stud3[i].group, stud3[i].balls);
	}

	
	fclose(f);
}