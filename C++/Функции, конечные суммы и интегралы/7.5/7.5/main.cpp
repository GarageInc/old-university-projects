/*
В. Многочлен. Представить многочлен S(x)=52·x40−3·x8+x в виде списка, где каждый элемент указывает на последующий. 
Eсли коэффициент а(i) = 0, то звено не включается в список. 
При создании списка использовать структурные переменные с тремя полями: 
	для хранения соответственно коэффициента, показателя степени и указателя на следующую запись.   
7. Написать функцию equal(), проверяющую на равенство многочлены p и q. Протестировать при p = q.  
*/

#include<conio.h>
#include<fstream>

//Структура - список(элементы, который "цепляются" друг за друга адресами next)
struct E
{
	int koef;//Число, которое в элементе списка сохраняется - кэффициент члена многочлена
	int step;//Степень члена
	E *next;//Ссылка на следующий элемент
	//E (int x=0,int y=0)//Конструктор по умолчанию для создания списка
		//{this->koef=x;  this->step=y;  this->next=NULL;}
};

void Print (E*p)//Обычная функция печати списка.
{
	printf("f(x)=");
	for (E*q=p; q!=NULL; q=q->next)
	{
		if (q->next==NULL)
			printf("%i*x^%i",q->koef,q->step);
		else
			printf("%i*x^%i+",q->koef,q->step);
	}
	printf("\n");
}

//Вставка в конец списка(или объединение с существующим элементом)
void push_back(E* &first, int koef, int step) 
{
	//Если пустой нулл
	if (first==NULL) 
	{
	   first=new E;
	   first->koef=koef;
	   first->step=step;
	   first->next=NULL;
	   return;
	}
	else
	{
		E* last=first;
		E* last2=last;
		int i=0;
		//Вставляем новый элемент согласно степени-"приводим" многочлен
		while (last->next!=NULL && last->step>step) 
		{
			last=last->next;
			if (i>0) last2=last->next;;
			i++;
		}
		/*
		last->next=new E;
		last->next->koef=koef;
		last->next->step=step;
		last->next->next=NULL;
		*/
		if (last->step==step) 
		{
			//Если степени совпали - просто складываем коэффициенты
			last->koef=last->koef+koef;//Но коэффициенты могут быть нулевыми!т.е. если нулевой коэффициент получился - удаляем этот член списка
			if (last->koef==0)
			{
				//Сдвигаем все списки
				while(last->next!=NULL)
				{
					last->koef=last->next->koef;
					last->step=last->next->step;
				}
				//delete last;//Удаляем последний элемент - копию предыдущего.
				last=NULL;//Завершили сдвиг списка
				return;
			}
			else return;//Иначе завершаем работу
		}
		else 
		{
			//Если степень нашего вводимого члена больше степени многочлена, на котором остановились по списку
			if (last->step<step)
			{
				//Если нет - то вставляем на это место новый член
				//Сохраняем ссылку на следующий элемент
				//Вставляем новый элемент и прикрепляем к нему сохраненную ссылку-сцепку
				last2->next=new E;
				last2->next->koef=koef;
				last2->next->step=step;
				last2->next->next=last;
				return;
			}
			else
			{
				
			}
			//Иначе других вариантов нет - это конец списка
		}

		last->next=new E;
		last->next->koef=koef;
		last->next->step=step;
		last->next->next=NULL;
		
	}
}

bool equals(E* p1, E*p2)
{
	bool t=false;//Изначально считаем, что многочлены - уже приведённые(без лишних степеней) и равны друг другу(true)
	//Сравниваем. Если хотя бы один пункт не совпал - возвращаем сразу false
	while(p1!=NULL && p2!=NULL)
	{
		if (p1->koef!=p2->koef && p2->step!=p1->step) return false;;

		//Идем по спискам, до конца обоих или одного из них
		p1=p1->next;
		p2=p2->next;

		if (p1!=NULL || p2!=NULL) 
			return false;//Один многочлен длиннее другого
		else 
			return true;//Иначе - одинаковые. Возвращаем true
	}
}

int main()
{
	
	E *p1=NULL;//Инициализируем - создаем новый список
	E *p2=NULL;
	
	//Добавление в список
	int n=0;
	//Введите количество членов в первом многочлене
	printf("N=");
	scanf("%d",&n);
	printf("\n");

	int koef=0,step=0;//Вводимые элементы
	//Вводим элементы
	for(int i=0; i<n; i++)
	{
		printf("Stepen = ");
		scanf("%d",&step);
		printf("   Koefficient = ");
		scanf("%d",&koef);

		push_back(p1,koef,step);
	}
	
	
	//Введите количество членов в первом многочлене
	printf("N=");
	scanf("%d",&n);
	printf("\n");

	//Вводим элементы
	for(int i=0; i<n; i++)
	{
		printf("Stepen = ");
		scanf("%d",&step);
		printf("   Koefficient = ");
		scanf("%d",&koef);

		push_back(p2,koef,step);
	}
	


	//Печатаем список
	printf("Result:\n");
	Print(p1);
	Print(p2);
	
	//Проверим многочлены на одинаковость
	if (equals(p1,p2)) printf("\n EQUALS.");//Эквивалентны
	else printf("\n NOT EQUALS.");//Не эквивалентны


	getch();//Для задержки экрана
	return 0;
}

