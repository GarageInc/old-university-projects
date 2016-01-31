#include <string.h>
#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <conio.h>

using namespace std;

/*СТРУКТУРА УЗЛА СПИСКА*/
struct Node
{
	int element; //Элемент
	Node *Next; //Адрес на следующий элемент

	//Конструктор по умолчанию для пустого узла списка
public:
	Node()
	{
		element = 0;
		Next = NULL;
	}
	Node(int a)
	{
		element = a;
		Next = NULL;
	}
};

//Наш список
class List
{
protected:
	Node *top; //Указатель на начало списка
	Node *marker; //Указатель на текущий элемент спика
	Node *pred;//Указатель на предыдущий элемент списка. Совпадает с top, если в списке 1н элемент
public:
	List() :top(NULL){}; //Конструктор по умолчанию (top=NULL) - создает пустой список
	void Reset();//Установить маркер на начало
	void Move();//Сдвиг по списку на один шаг
	bool EoList();//Проверка - не находится ли маркер на конце списка.Конец списка - это самый последний существующий элемент
	void AddToList(int a);//Добавление в список перед текущим после предыдущего
	void Show();//Показать список
	void Del();
	Node * TakeTop()//Получение ссылки на корневой список
	{
		Node *temp=this->top;
		return temp;
	}
	
};

//Наследующий класс упорядоченный список UporList от List
class UporList : List
{
public:
	UporList()
	{
		top = NULL;
		marker=NULL;
		pred=NULL;
	}; //Конструктор по умолчанию (top=NULL) - создает пустой список

	//Конструктор - принимает список - ссылку на список
	UporList(List *new_list)
	{
		this->top = new_list->TakeTop();

		//Сортируем "пузырьком"
		Node * list = top; // связанный список
		Node * node, * node2;
 
		for( node = list; node; node = node->Next )
			for( node2 = list; node2; node2 = node2->Next )
				if( node->element < node2->element ){ // если число из node меньше числа из node2 то переставляем их
					int i = node->element;
				    node->element = node2->element;
					node2->element = i;
            }
	}
	//Функция объединения двух упорядоченных списков - метод слияния, однопроходный алгоритм.
	//Объединяется первый список со вторым
	void Union(UporList *t,UporList *list)
	{
		//Создаем копии узлов корней наших списков(адреса, указывающие на корни)
		Node * asda = t->TakeTop(), *qwe = top;
		//list.AddToList(12);
		//Идем по адресам наших спискам, пока они не пустые
		for (; qwe || asda;)
		{
			if (qwe && asda)
			{
				if (qwe->element > asda->element)
				{
					list->AddToList(asda->element);
					asda = asda->Next;
				}
				else //if int go for
				{
					list->AddToList(qwe->element);
					qwe = qwe->Next;
				}
			}
			else
				if (asda)
				{
					list->AddToList(asda->element);
					asda = asda->Next;
				}
				;
					if (qwe)
					{
						list->AddToList(qwe->element);
						qwe = qwe->Next;
					}
				
		}
		//UporList list2(&list);
		//list2.UporListShow();
		//list.Show();
		//list2.UporListShow();
		//return (list2);
	}

	UporList& operator + ( UporList d) 
	{
		//void Union(UporList *t,UporList *list)
		static UporList ul;
	//Создаем копии узлов корней наших списков(адреса, указывающие на корни)
		Node * asda = d.TakeTop(), *qwe = top;
		//list.AddToList(12);
		//Идем по адресам наших спискам, пока они не пустые
		for (; qwe || asda;)
		{
			if (qwe && asda)
			{
				if (qwe->element > asda->element)
				{
					ul.AddToUporList(asda->element);
					asda = asda->Next;
				}
				else //if int go for
				{
					ul.AddToUporList(qwe->element);
					qwe = qwe->Next;
				}
			}
			else
				if (asda)
				{
					ul.AddToUporList(asda->element);
					asda = asda->Next;
				}
				;
					if (qwe)
					{
						ul.AddToUporList(qwe->element);
						qwe = qwe->Next;
					}
				
		}
		return ul;

	}


	UporList& operator - ( UporList d) 
	{
		static UporList inters;
		
		//Создаем копии узлов корней наших списков(адреса, указывающие на корни)
		Node * asda = d.TakeTop(), *qwe = top;
		
		//Идем по адресам наших спискам, пока они не пустые
		for (; qwe && asda;)
		{
			if (qwe->element==asda->element)
			{
				inters.AddToUporList(qwe->element);
				qwe=qwe->Next;
				asda=asda->Next;
			}
			else
			{
				if(qwe->element>asda->element)
				{
					asda=asda->Next;
				}
				else
				{
					qwe=qwe->Next;
				}
			}
		}
		return inters;
	}


	//Функция пересечения двух упорядоченных списков, однопроходный алгоритм
	void Intersection(UporList *t,UporList *list)
	{
		//Создаем копии узлов корней наших списков(адреса, указывающие на корни)
		Node * asda = t->TakeTop(), *qwe = top;

		//Идем по адресам наших спискам, пока они не пустые
		for (; qwe && asda;)
		{
			if (qwe->element==asda->element)
			{
				list->AddToList(qwe->element);
				qwe=qwe->Next;
				asda=asda->Next;
			}
			else
			{
				if(qwe->element>asda->element)
				{
					asda=asda->Next;
				}
				else
				{
					qwe=qwe->Next;
				}
			}
		}
	}

	//Печать в файл
	void ToFile()
	{
		//Выводим в файл
		ofstream file("result.txt");
		file.clear();//Очищаем для новой записи
		if (file.fail())
		{
			cout<<"Файл rezult.txt не существует!";
			getch();
			return;//Задержка экрана
		}
		else;

		Node * temp = top; //Объявляем указатель и изначально он указывает на начало

		while (temp != NULL) //Пока по адресу на начало хоть что-то есть
		{
			//Выводим все элементы структуры
			cout<<" "<<temp->element;
			file<<temp->element<<endl;
			temp = temp->Next; //Указываем на следующий адрес из списка
		}

		file.close();
	}

	//Деструктор - вызываем из родительского класс
	~UporList()
	{
		//while (top != NULL) //Пока по адресу есть хоть что-то
		//{
		//	Node *temp = top->Next; //Сразу запоминаем указатель на адрес следующего элемента структуры
		//	delete top; //Освобождаем память по месту начала списка
		//	top = temp; //Меняем адрес начала списка
		//}
		while (top != NULL)
		 {
			Node *next = top->Next;
			delete top;
			top = next;
		}
	}
	
	void  UporListShow()
	{
		Node *temp = top; //Объявляем указатель и изначально он указывает на начало

		while (temp != NULL) //Пока по адресу на начало хоть что-то есть
		{
			//Выводим все элементы структуры
			cout<<temp->element<<" ";
			temp = temp->Next; //Указываем на следующий адрес из списка
		}
		cout<<endl;
	}

	//Добавление в упорядоченный список
	void AddToUporList(int a)
	{
		Node* temp3=top;
		if(temp3)//Проверим - а не пустой ли у нас список вообще?
		{
		//Ищем подходящий узел
		while(temp3->Next && temp3->element<a)
		{
			temp3=temp3->Next;
		}
		
		//Теперь добавляем - c учетом - какой у нас последний элемент
		if (temp3->element>=a)
		{
			Node* temp2=temp3->Next;//Cохраняем кусочек оборванной цепи списка
			temp3->Next=new Node;
			temp3->Next->element=temp3->element;//Меняем местами(по порядку чтобы было)
			temp3->element=a;

			temp3=temp3->Next;
			//Связываем 
			temp3->Next=temp2;
		}
		else
		{
			temp3->Next= new Node(a);
		}
		}
		else
			top=new Node(a);
	}
};

int main()
{
	setlocale(LC_ALL, "Russian");

	ifstream f("data1.txt");
	ifstream f2("data2.txt");
	ifstream f3("data3.txt");
	//Проверим на существование файл
	if (f.fail() || f2.fail() || f3.fail())
	{
		cout<<"Один из файлов не существует! Проверьте их наличие";
		getch();
		return 0;//Задержка экрана
	}
	else;

	List list;//Инициализируем - создаем новый список
	List list2;//Инициализируем - создаем новый список
	List list3;//Инициализируем - создаем новый список

	int k;//Переменнаяя для считывания
	//Добавление в список - считываем из файла.
	//Считываем из файлов
	while (!f.eof())
	{
		f>>k;
		list.AddToList(k);
		;
	}
	while (!f2.eof())
	{
		f2>>k;
		list2.AddToList(k);
		;
	}
	while (!f3.eof())
	{
		f3>>k;
		list3.AddToList(k);
		;
	}

	//Создаем упорядоченные списки из прочитанных списков
	UporList l(&list);
	UporList l2(&list2);
	UporList l3(&list3);

	//Печатаем в файл
	UporList union_list;//Для получения объединения списков
	UporList intersection;
	
	//ПЕРЕСЕЧЕНИЕ ДВУХ ОБЪЕДИНЕНИЙ

	union_list=l+l2;
	intersection=union_list-l3;

	//Печатаем список
	cout<<"Результат пересечения 3го списка с объединеним 1го и 2го списков - в файле result.txt и в консоли: "<<endl;
	intersection.UporListShow();
	//Добавление новых элементов в наш упорядоченный список и печатаем результаты
	/*intersection.AddToUporList(12);
	cout<<endl<<"Добавили число 12:"<<endl;
	intersection.UporListShow();

	intersection.AddToUporList(10);
	cout<<endl<<"Добавили число 10:"<<endl;
	intersection.UporListShow();
*/

	//intersection.ToFile();
	f.close();//Закрываем наш файл
	f2.close();//Закрываем наш файл
	f3.close();//Закрываем наш файл

	system("pause");//Для задержки экрана
	return 0;
}



/*ФУНКЦИЯ КЛАССА LIST ДОБАВЛЕНИЯ НОВОГО ЭЛЕМЕНТА В СПИСОК В СПИСОК*/
void List::AddToList(int a)
{
	
	//На случай, если в списке нет ни одного элемента ещё
	if (top == NULL)
	{
		top = new Node;
		top->element = a;

		//Устанавливаем маркер на текущий элемент - т.е. на последний добавленный
		marker = top->Next;

		//Предыдущий элемент - устанавливаем на top
		pred = top;
		return;//Закрываем функцию, дело сделано

	}
	else
	{
		//Создаем узел
		pred->Next = new Node;
		pred->Next->element = a;

		//Передвинули указатель с нового элемента на текущий(соединили список)
			pred->Next->Next = marker;

		//Самое важное - передвинули предыдущий элемент на +1
		pred = pred->Next;
	}
}

/*ФУНКЦИЯ КЛАССА LIST УСТАНОВКИ МАРКЕРА НА НАЧАЛО СПИСКА*/
void List::Reset()
{
	marker = top;
}

/*ФУНКЦИЯ КЛАССА LIST ДЛЯ ВЫВОДА СПИСКА НА ЭКРАН*/
void List::Show()
{
	Node *temp = top; //Объявляем указатель и изначально он указывает на начало

	while (temp != NULL) //Пока по адресу на начало хоть что-то есть
	{
		//Выводим все элементы структуры
		cout<<temp->element<<" ";
		temp = temp->Next; //Указываем на следующий адрес из списка
	}
	cout<<endl;
	
}

/*ФУНКЦИЯ КЛАССА LIST ПРОВЕРКИ КОНЦА СПИСКА*/
bool List::EoList()
{
	if (marker->Next == NULL)
		return 1;
	else return 0;
}

/*ФУНКЦИЯ КЛАССА LIST ДЛЯ ВЫВОДА СДВИГА НА 1 ШАГ*/
void List::Move()
{
	marker = marker->Next;
}

/*ФУНКЦИЯ КЛАССА LIST УДАЛЕНИЯ ТЕКУЩЕГО ЭЛЕМЕНТА*/
void List::Del()
{
	Node *temp=marker;

	marker=marker->Next;
	pred->Next=marker;

	//Удаляем неиспользуемый элемент
	delete temp;
}
