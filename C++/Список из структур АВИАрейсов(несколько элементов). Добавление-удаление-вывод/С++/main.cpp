#include <iostream>
#include <conio.h>
#include <cstring>
#include <string>

using namespace std;

	typedef struct Zajavka
	{
		string punkt_nazn;
		int  numbe_reys;
		char FIO[100];
		char date[100];
	};
	//Структура - список(элементы, который "цепляются" друг за друга адресами next)
	typedef struct Node {
		Zajavka value;
		struct Node *next;
	} Node;


//Вставка в конец списка
void push_back(Node* &first, Zajavka int1) 
{
	//Если не нулл
if (first==NULL) 
{
   first=new Node;
   first->next=NULL;
   first->value=int1;
   return;
}
else
{
	Node* last=first;

	while (last->next!=NULL) 
		last=last->next;
;
	last->next=new Node;


	last->next->value=int1;
	last->next->next=NULL;
	return;
}
}

//Печать всех рейсов
void print(Node* head)
{
	while(head!=NULL)
	{
		cout<<head->value.FIO<<endl;
		head=head->next;
	}
}

//Вывод по дате-номеру нужных рейсов
void print_data_number(Node* head,char* date, int n)
{
	//Если список не пуст - выводит данные
	while(head!=NULL)
	{
		if( strcmp(date,head->value.date)==0 && n==head->value.numbe_reys) cout<<head->value.FIO<<" "<<head->value.numbe_reys<<endl
			; else ;
		head=head->next;
	}


}


	int Choice()//Выбор действия:
        {
            cout<<"Введите номер для действия:"<<endl;
            cout<<"1 - удалить заявки;"<<endl;
            cout<<"2 - вывести заявку по заданному номеру рейса И дате рейса"<<endl;
            cout<<"3 - вывод всех заявок;"<<endl;
            cout<<"4 - выход."<<endl;
			int t;

			cin>>t;
			//Контролируем вывод
			if(t>0 && t<5)
            return t;
			else return 4;
        };

	//удаление списка
 Node* delete_spisok(Node *&car)
 {
  Node *p=car;
    while (p->next)
  {
    Node *tmp;
       tmp = p;
        p = p->next;
        delete tmp;
  }
  p=p->next;
  delete []p;

  car=NULL;
   cout << endl;
 return car;
  }
	
int main()
{
	setlocale(LC_ALL, "Russian");//Чтобы воспринимался русский текст
	cout<<"Введите количество заявок:"<<endl;
	int kol;
	cin>>kol;
	
	Node *node=NULL;

	//Добавляем в список заявки
	//но сначала формируем заявку
	for(int i=0; i< kol; i++)
	{
		cout<<"Введите заявку номер "<<i+1<<endl;
		string punkt_nazn;
		int  numbe_reys;
		char FIO[100];
		char date[100];
		
		cout<<"Пункт назначения:"<<endl;
		cin>>punkt_nazn;
		cout<<"номер рейса:"<<endl;
		cin>>numbe_reys;
		cout<<"фамилия:"<<endl;
		cin>>FIO;
		cout<<"дата:"<<endl;
		cin>>date;

		Zajavka z;
		z.punkt_nazn=punkt_nazn;
			strcpy(z.FIO,FIO);
			strcpy(z.date,date);
			z.numbe_reys=numbe_reys;

		//Добавляем в cписок
		push_back(node, z);
	}

            int n = 0;
            while (n != 4)
            {
                n = Choice();//Выбираем выполняемое действие
                if (n != 4)//Если не выбрали выход
                {
					switch(n)//Выполняем требуемое действие
                    {
                        case 1:
                        {
							cout<<"Все заявки удалены."<<endl;
							delete_spisok(node);
                        }
                        break;
                        case 2:
                        {
							char date[1000];
							int n;
							cout<<"ПОИСК: введите дату рейса И номер рейса через Enter: "<<endl;
							cin>>date;
							cin>>n;
							print_data_number(node,date,n);
                        }
                        break;
                        case 3:
                        {
							print(node);  
                        }
                        break;
                    }
                }
            }
	return 0;
}
