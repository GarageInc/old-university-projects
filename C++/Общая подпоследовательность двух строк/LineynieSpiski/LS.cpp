#include<iostream>
#include<conio.h>
using namespace std;

struct E
{
	int info;
	E *next;
	E (int x=0, E*p=NULL)
		{info=x; next=p;}
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

int PrintR(E*p)//Рекурсивная функция печати списка ПО ПОРЯДКУ.
{
	if (p==NULL) return 0;
	else
	{
		cout<<p->info<<" ";
		return (1+ Print(p->next));
	}
}

int PrintRR(E*p)//Рекурсивная функция печати списка ПО ОБРАТНОМУ ПОРЯДКУ.
{
	if (p==NULL) return 0;
		else
		{
			int k=1+ PrintRR(p->next);
			cout<<p->info<<" ";
			return k;//k-количество.
		}
}

//Функция ввода В НАЧАЛО линейного списка.
void AddBegin (E *&p, int x) //Так как р - меняется.
{
	p=new E(x,p);
};

//Функция ввода В КОНЕЦ линейного списка.
void AddEnd (E *q, int x) //Так как р - меняется.
{
	for (;q->next!=NULL; q=q->next)
		;
	q->next=new E(x);
}

//Функция удаления ЭЛЕМЕНТОВ линейного списка с начала.
void Delete(E*&p)
{
	for (E*q=p,*r;q!=NULL;q=r)
	{
		r=q->next;
		delete q;
	}
	p=NULL;
}

//Функция удаление элемента с заданным значением.
void DeleteUnit(E*&p,int x)
{
	E*q=p, *r;
	for (;q->info!=x;q=q->next)
	{
		;
	}
	if (q->next!=NULL)
	{
		q->next=q->next->next;
		delete q;
	}
}
/*void Insert( , int x, int pos)// pos - позиция, х - элемент.
{
}
*/

int main()
{
	E *p=NULL;

	p=new E;
	p->info=3;
	p->next=NULL;

	p->next=new E;
	p->next->info=7;
	p->next->next=NULL;

	E *q=p;
	p=new E;
	p->info=1;
	p->next=q;
	cout<<endl<<"LinSpisok: "<<endl;
	cout<<"Kol= "<<Print(p)<<endl;
	cout<<endl;

	//Добавление В НАЧАЛО списка.
	int x;
	cout<<"Vvod v nachalo !=0:"<<endl;
	cin>>x;
	for ( ; x!=0; )
	{
		AddBegin(p,x);
		cin>>x;
	}
	cout<<endl<<"LinSpisok: "<<endl;
	cout<<"Kol= "<<Print(p)<<endl;

	//Добавление В КОНЕЦ списка.
	cout<<endl<<"Vvod v konec !=0 :"<<endl;
	cin>>x;
	for ( ; x!=0; )
	{
		AddEnd(p,x);
		cin>>x;
	}
	cout<<endl<<"LinSpisok: "<<endl;
	cout<<"Kol= "<<Print(p)<<endl;

	cout<<endl<<"Delete:";
	//УДАЛЕНИЕ элемента лин.списка С ЗАДАННЫМ значением:
	cout<<endl<<"Vvedite element s zadannim znaheniem: ";
		cin>>x;
	DeleteUnit(p,x);

	//УДАЛЕНИЕ линейного списка:
	Delete(p);
	cout<<endl<<"LinSpisok: "<<endl;
	cout<<"Kol= "<<Print(p)<<endl;
	cout<<"Kol= "<<PrintR(p)<<endl<<"Rekursia - naoborot: "<<endl;
	cout<<"Kol= "<<PrintRR(p);

	_getch();
	return 0;
}