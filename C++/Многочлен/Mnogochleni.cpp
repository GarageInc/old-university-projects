#include<iostream>//ЗАДАЧА - в линейных списках ввести два многочлена, сложить их и вывести конечный многочлен, как результат сложения. 
#include<conio.h>//Учитывая все возможные варианты степеней, коэффициентов и т.д. Методы использовались топорные, корявые,
using namespace std;//понятые по мере начала изучения лин.списков. Возможно, есть варианты и попроще намного.
//МОЖНО ВВОДИТЬ И НЕПРИВЕДЕННЫЙ МНОГОЧЛЕН. Сосчитает:)

//НАЧАЛО:
//Создание прообраза многочлена с полями коэффициентов и степеней.
struct E
{
	int exp, coef;
	E *next;
	E (int y=0, int x=0, E*p=NULL)
		{coef=y; exp=x; next=p;}
};

//Добавление элементов в ПЕРВЫЙ многочлен. Подфункция для функции InputP.
void AddEndP (E *first, int y, int x) //Так как р - меняется.
{
	for (;first->next!=NULL; first=first->next)
		;
	first->next=new E(y, x);
}
//Функция ввода данных для первого многочлена.
void InputP (E*&p,int MaxExpP)//Обычная функция ввода 1го многочлена.
{
	int k=MaxExpP;
	p=new E;
	cin>>p->coef>>p->exp;
	p->next=NULL;

	int x,y;
	for (int i=1;i<k;i++)
	{
		cin>>y>>x;
		AddEndP(p,y,x);
	}
}

//Добавление элементов во ВТОРОЙ многочлен. Подфункция для функции InputQ.
void AddEndQ (E *second, int y, int x)
{
	for (;second->next!=NULL; second=second->next)
		;
	second->next=new E(y, x);
}
//Функция ввода данных для второго многочлена.
void InputQ (E*&q, int MaxExpQ)//Обычная функция ввода 1го многочлена.
{
	int k=MaxExpQ;
	q=new E;
	cin>>q->coef>>q->exp;
	q->next=NULL;

	int x,y;
	for (int i=1;i<k;i++)
	{
		cin>>y>>x;
		AddEndQ(q,y,x);
	}
}

//Прикладная подфункция функция дла красивой печати(работы функции печати)
void Plus(int k)
{
	if (k!=0)
		cout<<"+";
}
//Обычная функция печати списка.
void Print (E*p)
{
	int k=0;
	for (E*w=p; w!=NULL; w=w->next)
	{
		if (w->coef==1 && w->exp==1)
		{	Plus(k);cout<<"x"; }
		else
			if (w->coef==1 )
			{	Plus(k); cout<<"x^"<<w->exp; }
			else
				if (w->coef!=0 && w->exp==1)
				{	Plus(k);cout<<w->coef<<"*x"; }
				else 
					if (w->coef!=0 && w->coef!=1 && w->exp!=0)
					{	Plus(k);cout<<w->coef<<"*x^"<<w->exp; }
					else 
						if (w->coef!=0 && w->coef==1 && w->exp!=0)
						{	Plus(k);cout<<"x^"<<w->exp; }
						else 
							if (w->coef!=0 && w->exp==0)
							{	Plus(k);cout<<w->coef; }
		k++;
	}
}

void SortP (E*&p)
 {
	 int const N=100;
	 int nomer=0, expB[N], coefB[N];
	 int i=0;
	 for (E*n=p ;	n!=NULL;	i++,nomer++, n=n->next)
	 {
		 expB[i]=n->exp;
		 coefB[i]=n->coef;
	 }
	 //СОРТИВОВКА КОЭФФИЦИЕНТОВ.
	 int bob;
	 for(int y=0;y<nomer;y++ )
	 {
		 for(int j=0;j<(nomer-1);j++)
			 if (expB[j]>expB[j+1])
				bob=expB[j], expB[j]=expB[j+1],expB[j+1]=bob,bob=coefB[j], coefB[j]=coefB[j+1],coefB[j+1]=bob;
	 }
	 E*m=p;
	 for (int j=0;m!=NULL;j++,m=m->next)
	 {
		 m->exp=expB[j];
		 m->coef=coefB[j];
	 }
 }
void PlusMinusP(E *&p)
{
	SortP(p);
	int coefM, expM;
	for(E *i=p; i!=NULL; i=i->next)//До предпоследнего слагаемого многочлена
	{
		coefM=i->coef;
		expM=i->exp;
		E *jok=i;
		for(E *j=i->next; j!=NULL;  j=j->next, jok=jok->next)// До последнего слагаемого многочлена
		{
			if ((j->next != NULL) && (j->exp==expM))
			{
				coefM=coefM+j->coef;

				E *g=j->next;
				jok->next=g;
				delete j;
				j=g;
			}
			else
			{
				if((j->next==NULL) && (expM==j->exp))
				{
					coefM=coefM+(j->coef);

					E* b;
					b=j;
					jok->next=NULL;
					j=jok;
					delete b;
				}
			}
		}
		i->coef=coefM;
	}
	E*i=p, *j=p->next;
	for (;i->next->next!=NULL;i=i->next, j=j->next)
		;
	if (i->exp==j->exp) {i->coef=i->coef+j->coef; i->next=NULL; delete j;}
}

void SortQ (E*&q)
 {
	 int const N=100;
	 int nomer=0, expB[N], coefB[N];
	 int i=0;
	 for (E*n=q ;	n!=NULL;	i++,nomer++, n=n->next)
	 {
		 expB[i]=n->exp;
		 coefB[i]=n->coef;
	 }
	 //СОРТИВОВКА КОЭФФИЦИЕНТОВ.
	 int bob;
	 for(int y=0;y<nomer;y++ )
	 {
		 for(int j=0;j<(nomer-1);j++)
			 if (expB[j]>expB[j+1])
				bob=expB[j], expB[j]=expB[j+1],expB[j+1]=bob,bob=coefB[j], coefB[j]=coefB[j+1],coefB[j+1]=bob;
	 }
	 E*m=q;
	 for (int j=0;m!=NULL;j++,m=m->next)
	 {
		 m->exp=expB[j];
		 m->coef=coefB[j];
	 }
 }
void PlusMinusQ(E *&q)
{
	SortP(q);
	int coefM, expM;
	for(E *i=q; i!=NULL; i=i->next)//До предпоследнего слагаемого многочлена
	{
		coefM=i->coef;
		expM=i->exp;
		E *jok=i;
		for(E *j=i->next; j!=NULL;  j=j->next, jok=jok->next)// До последнего слагаемого многочлена
		{
			if ((j->next != NULL) && (j->exp==expM))
			{
				coefM=coefM+j->coef;

				E *g=j->next;
				jok->next=g;
				delete j;
				j=g;
			}
			else
			{
				if((j->next==NULL) && (expM==j->exp))
				{
					coefM=coefM+(j->coef);

					E* b;
					b=j;
					jok->next=NULL;
					j=jok;
					delete b;
				}
			}
		}
		i->coef=coefM;
	}
	E*i=q, *j=q->next;
	for (;i->next->next!=NULL;i=i->next, j=j->next)
		;
	if (i->exp==j->exp) {i->coef=i->coef+j->coef; i->next=NULL; delete j;}
}
//Подфункция для формирования суммы многочленов
void AddEndB (E *trololo, int y, int x)
{
	for (;trololo->next!=NULL; trololo=trololo->next)
		;
	trololo->next=new E(y, x);
}
//Подфункция для сортировки получившегося многочлена.
//Сортируем весь список b.Для красоты и от нечего делать.
void Sort (E*&b)
 {
	 int const N=100;
	 int nomer=0, expB[N], coefB[N];
	 int i=0;
	 for (E*n=b ;	n!=NULL;	i++,nomer++, n=n->next)
	 {
		 expB[i]=n->exp;
		 coefB[i]=n->coef;
	 }
	 //СОРТИВОВКА КОЭФФИЦИЕНТОВ.
	 int bob;
	 for(int y=0;y<nomer;y++ )
	 {
		 for(int j=0;j<(nomer-1);j++)
			 if (expB[j]>expB[j+1])
				bob=expB[j], expB[j]=expB[j+1],expB[j+1]=bob,bob=coefB[j], coefB[j]=coefB[j+1],coefB[j+1]=bob;
	 }
	 E*m=b;
	 for (int j=0;m!=NULL;j++,m=m->next)
	 {
		 m->exp=expB[j];
		 m->coef=coefB[j];
	 }
 }
//Функция подсчета суммы двух многочленов и формирование нового линейного списка.
void Summa (E*&p,E*&q,E*&b)
{
	int expB;
	int coefB;
	int k=1;
	for(E*first=p;  first!=NULL;  first=first->next)
	{
		{if (k==1) b=new E,	b->coef=0,	b->exp=0,	b->next=NULL;}
		k++;
		expB=first->exp;
		coefB=first->coef;
		for(E*second=q;  second!=NULL;  second=second->next)
		{
			{if (first->exp == second->exp) {coefB=coefB+second->coef;}}
			//Написать цикл отбора элементов, не входящих в один из многочленов
		}
		if (coefB!=0)
			AddEndB(b,coefB,expB);
	}

	int coefA;
	int expA;
	for(E*second=q;  second!=NULL;  second=second->next)//Добавление слагаемых из второго многочлена, если 
	//эти слагаемые отсутствуют в первом.
	{
		expA=second->exp;
		coefA=second->coef;
		E*third=b;
		for(;  third!=NULL;  third=third->next)
		{
			if(second->exp==third->exp)
				break;
		}
		if (third==NULL && coefA!=0)
			AddEndB(b,coefA,expA);
	}
	Sort (b);
}


int main()
{
	E *p=NULL, *q=NULL, *b=NULL;//Соответственно первый, второй и конечный многочлены.
	int MaxExpP,MaxExpQ;
	cout<<"Vvedite Max exp 1:";//Максимальная степень первого многочлена.
		cin>>MaxExpP;
	cout<<"Vvedite Max exp 2:";//Максимальная степень второго многочлена.
		cin>>MaxExpQ;

	cout<<endl<<"LinSpisok 1 : vvodite koefficient & stepen' cherez probel & Enter "<<endl;//Ввод первого линейного списка-ПЕРВОГО многочлена.
		InputP(p,MaxExpP);
	cout<<endl<<endl<<"LinSpisok 2: vvodite koefficient & stepen' cherez probel & Enter "<<endl;//Ввод второго линейного списка - ВТОРОГО многочлена.
		InputQ(q,MaxExpQ);
	
	cout<<endl<<"Mnogochleni: "<<endl;//Вывод обоих получившихся многочленов - визуальный ряд.
	cout<<"P(x)= ",Print(p);
	cout<<endl, 
	cout<<"Q(x)= ",Print(q);

	cout<<endl<<endl<<"Privedennie: "<<endl;//Приводим многочлены к нормальному ввиду. Ввод ведь может быть произвольным.
	PlusMinusP(p);//Сортировка 1го многочлена и вывод результата.
	PlusMinusP(p);
	PlusMinusQ(q);
	PlusMinusQ(q);
	cout<<endl<<"P(x)= ",Print(p);
	cout<<endl<<"Q(x)= ",Print(q);

	cout<<endl<<endl<<"Summa f(x)= ";//Запуск функций подсчёта сумм и вывода получившегося результата.
	Summa(p,q,b);
	Print(b);

	_getch();
	return 0;
}