#include<iostream>
#include<conio.h>
#include<fstream>

using namespace std;

//Структура - список(элементы, который "цепляются" друг за друга адресами next)
struct E
{
	int info;//Число, которое в элементе списка сохраняется
	E *next;//Ссылка на следующий элемент
	//E (int x=0, E*p=NULL)//Конструктор по умолчанию для создания списка
		///{info=x; next=p;}
};

int Print (E*p)//Обычная функция печати списка.
{
	int k=0;
	for (E*q=p; q!=NULL; q=q->next,k++)
	{
		cout<<q->info<<" ";
	}
	return k;
}

//Функция ввода В КОНЕЦ линейного списка.
void AddEnd (E *q, int x) //Так как р - меняется.
{
		for (;q->next!=NULL; q=q->next)//Идем по списку до конца
			;
		q->next=new E;//Вставляем элемент
}

//Вставка в конец списка
void push_back(E* &first, int int1) 
{
	//Если не нулл
if (first==NULL) 
{
   first=new E;
   first->next=NULL;
   first->info=int1;
   return;
}
else
{
	E* last=first;

	while (last->next!=NULL) 
		last=last->next;
;
	last->next=new E;


	last->next->info=int1;
	last->next->next=NULL;
	return;
}
}


//Функция удаление элемента по заданному номеру.
void DeleteUnit(E*&p,int n)//n-номер,p-ссылка на список
{
	if (n!=1)
		{
		E*q=p;
		int i=0;
		for (;i<n-2 && q!=NULL;q=q->next,i++)//Идем до позиции
		{
		}

		if (q->next!=NULL)//Удаляем, если номер позиции элемента - где-то посередине списка
		{
			E *r=q->next;
			q->next=q->next->next;
			delete r;
		}
		else//Иначе - элемент последний
			if (q->next==NULL)
				q=NULL;
		}
	else
		if (n==1)
		{
			E *q=p;
			p=p->next;
			delete q;
		}
}

int main()
{
	
	ifstream f("data.txt");
	//Проверим на существование файл
	if (f.fail())
	{
		cout<"data.txt is not exist!(ne sushestvuet!)";
		getch();
		return 0;//Задержка экрана
	}
	else;

	E *p=NULL;//Инициализируем - создаем новый список
	
	int k;//Переменнаяя для считывания
	//Добавление в список - считываем из файла.
	f>>k;
	//p=new E(k);

	while(!f.eof())
	{
		f>>k;
		push_back(p,k);
	}
	
	//Печатаем список
	cout<<"Old List: ";
	Print(p);
	
	//Удаляем второй элемент из списка
	DeleteUnit(p,2);

	//Печатаем новый список
	cout<<endl<<"New list without second element: ";
	Print(p);


	f.close();
	_getch();//Для задержки экрана
	return 0;
}