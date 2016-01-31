#include<iostream>
#include<conio.h>
#include<fstream>

using namespace std;

//Структура - список(элементы, который "цепляются" друг за друга адресами next)
struct List
{
	int info;//Число, которое в элементе списка сохраняется
	List *next;//Ссылка на следующий элемент
	List (int x=0, List*p=NULL)//Конструктор по умолчанию для создания списка
		{info=x; next=p;}
};

void Print(List*p)//Обычная функция печати списка.
{
	for (List*q=p; q!=NULL; q=q->next)
	{
		cout<<q->info<<" ";
	}
}

//Функция ввода В КОНЕЦ линейного списка.
void AddEnd(List *q, int x) //Так как р - меняется.
{
		for (;q->next!=NULL; q=q->next)//Идем по списку до конца
			;
		q->next=new List(x);//Вставляем элемент
}

//Функция удаления последнего элемента
void DeleteEnd(List*&p)//p-ссылка на список
{
	if (p!=NULL && p->next!=NULL)
	{
		List*q=p;//Чтобы не "потерялся" адрес первого элемента
	
		while(q->next->next!=NULL)
			q=q->next;

		List*r=q->next;

		delete r;
		
		q->next=NULL;
	}
	else//Случай - когда список пустой или состоит из одного элемента
		{
			List*q=p;

			delete q;

			p=NULL;
		}
	

}

int main()
{
	
    setlocale(LC_ALL, "Russian");//Чтобы воспринимался русский текст

	int n=5;//Количество элементов для списка

	List *p=NULL;//Инициализируем - создаем новый список
	
	int k;//Переменнаяя для считывания
	
	cout<<"Введите 5 элементов по порядку:"<<endl;
	cout<<1<<" = ";
	cin>>k;
	p=new List(k);
	for(int i=0; i<n-1; i++)
	{
		cout<<i+2<<" = ";
		cin>>k;
		AddEnd(p,k);
	}
	
	//Печатаем список
	cout<<"Новый введенный список: ";
	Print(p);
	
	//Удаляем последний элемент из списка
	DeleteEnd(p);

	//Печатаем новый список
	cout<<endl<<"Список без последнего элемента: ";
	Print(p);

	_getch();//Для задержки экрана
	return 0;
}