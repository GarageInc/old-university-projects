
#include <stdlib.h>
#include <iostream>

using namespace std;

/*СТРУКТУРА УЗЛА СПИСКА*/
struct Node
{
	int element; //Элемент
	Node *Next; //Адрес на следующий элемент

public:
	//Конструктор по умолчанию для пустого узла списка
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


// Дублирование второй подпоследовательности в первой
void Duble(Node**top, Node **top2, int count)
{

	Node *temp = *top;
	Node *temp2 = *top2;
	// Идем по первому списку и ищем совпадения
	while (temp != NULL && temp2 != NULL && temp->Next != NULL)
	{
		Node* temp3 = new Node();
		temp2 = *top2;
		int k = 0;
		// Ищем первое совпадение
		while (temp->Next != NULL)
		{
			if (temp->element == temp2->element)
			{
				k++;
				temp3 = temp;
				temp = temp->Next;
				temp2 = temp2->Next;
				break;
			}

			temp = temp->Next;
		}

		// После нахождения - новый цикл на идентичность кусочков - остановка после прихода на последний элемент
		while (temp != NULL && temp2 != NULL)
		{
			if (temp->element == temp2->element)
				k++;
			temp3 = temp3->Next;
			temp = temp->Next;
			temp2 = temp2->Next;
		}

		// Если нашелся участок, полностью совпавший - то вставляем элементы второго списка
		if (k == count)
		{
			temp2 = *top2;
			while (temp2 != NULL)
			{
				if (temp != NULL)
				{
					Node*t = new Node(temp2->element);
					t->Next = temp3->Next;
					temp3->Next = t;

					temp = temp->Next;
					temp2 = temp2->Next;
					temp3 = temp3->Next;
				}
				else
				{
					Node*t = new Node(temp2->element);
					t->Next = temp3->Next;
					temp3->Next = t;

					temp2 = temp2->Next;
					temp3 = temp3->Next;
				}
			}
		}
	}
}

// ФУНКЦИЯ ДУБЛИРОВАНИЯ КАЖДОГО ПЯТОГО ЭЛЕМЕНТА
void DUBLE5(Node** top)
{
	Node *temp = *top; //Объявляем указатель и изначально он указывает на начало

	while (temp->Next != NULL) //Пока по адресу на начало хоть что-то есть
	{
		int i;
		for (i = 1; i < 5 && temp->Next != NULL; i++)
		{
			temp = temp->Next;
		}
		if (i == 5)
		{
			// Дублируем пятый элемент
			Node *temp2 = new Node(temp->element);
			temp2->Next = temp->Next;
			temp->Next = temp2;
			if (temp->Next != NULL)
				temp = temp->Next;
			if (temp->Next != NULL)
				temp = temp->Next;
		}
	}
}

/*ФУНКЦИЯ КЛАССА LIST ДОБАВЛЕНИЯ НОВОГО ЭЛЕМЕНТА В СПИСОК В СПИСОК*/
void AddToList(Node** top, int a)
{
	//На случай, если в списке нет ни одного элемента ещё
	if (*top == NULL)
	{
		*top = new Node(a);
		return;//Закрываем функцию, дело сделано
	}
	else
	{
		//Создаем узел
		Node *newtop = *top;

		while (newtop->Next != NULL)
			newtop = newtop->Next;
		newtop->Next = new Node;
		newtop->Next->element = a;
		newtop->Next->Next = NULL;
	}
}

/*ФУНКЦИЯ КЛАССА LIST ДЛЯ ВЫВОДА СПИСКА НА ЭКРАН*/
void Show(Node** top)
{
	Node *temp = *top; //Объявляем указатель и изначально он указывает на начало

	while (temp != NULL) //Пока по адресу на начало хоть что-то есть
	{
		//Выводим все элементы структуры
		cout << temp->element << " ";
		temp = temp->Next; //Указываем на следующий адрес из списка
	}
	cout << endl;

}

int main()
{
	setlocale(LC_ALL, "Russian");

	Node *list = NULL;//Инициализируем - создаем новый список
	Node *list2 = NULL;//Инициализируем - создаем новый список

	int n;//Переменнаяя для считывания
	cout << "Введите количество чисел в первом списке: ";
	cin >> n;
	int k;

	//Добавление в список 
	for (int i = 0; i < n; i++)
	{
		cout << "Ввод " << i + 1 << " элемента: ";
		cin >> k;
		AddToList(&list, k);
	}

	// Печать списка
	Show(&list);

	// Теперь продублируем каждый 5ый элемент
		DUBLE5(&list);
		cout << "Продублирован каждый пятый элемент:";
		Show(&list);

	// Вторая подпоследовательность
	cout << "Введите количество чисел во втором списке: ";
	cin >> n;

	//Добавление в список 
	for (int i = 0; i < n; i++)
	{
		cout << "Ввод " << i + 1 << " элемента: ";
		cin >> k;
		AddToList(&list2,k);
	}
	Show(&list2);

	// Теперь продублируем все элементы второго списка в первом списке(если они там есть по порядку
	Duble(&list, &list2,n);
	cout << "Результат дублирования элементов второго списка в первом: ";
	Show(&list);

	system("pause");//Для задержки экрана
	return 0;
}




