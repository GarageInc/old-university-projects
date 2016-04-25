#include<iostream>
#include<conio.h>
#include<stack>
#include<queue>
using namespace std;
/*
* УСЛОВИЕ ЗАДАЧИ: написать класс дерево. Реализовать его функции, какие можно. 
*
*
**/
struct Node//Узел дерева
{
	int info;
	Node *left, *right;
	Node (int x=0, Node*leftL=NULL, Node* rightR=NULL)//Конструктор. Задаём либо по умолчанию, либо по выбору пользователя.
	{
		info=x; left=leftL; right=rightR;
	}
};

class Tree//Класс для создания дерева упорядоченных цифр.
{
private:
	Node* root;//Корень. Основной элемент.
public:
	Node* uzel;
	Tree() {root=NULL;};//КОНСТРУКТОР. Определение элементов класса.
	void Push();//Вкладываем в дерево элементы, сначала задаём их количество.
	bool Empty() {return root==NULL;}//Проверка дерева на пустоту.
	void Print();// Печать дерева.
	void SravnTrees(Tree tr2);//Сравнение двух деревьев по их структуре. Можно сравнивать и по содержанию, но код аналогичен, с мин.изменением.
	void Delete ();//Удаление дерева. Внутри деструктора либо извне. Перегрузка функций.
	void Delete (Node * uzel);//Удаление узла. Используется внутри деструктора либо вызывается извне. Для его вызова используем следующую функцию:
	void Delete (int x);//Удаление заданного элемента со всеми его поддеревьями.
	queue <Node*> Ochered();//Функция, которая формирует очередь, содержащую в себе по-уровневые адреса всех корней-веток дерева, 
	//переданного функции.
	int Pop();//Убираем из дерева заданный узел. Со всеми его поддеревьями.
	~Tree();//ДЕСТРУКТОР. Очищаем дерево полностью.
	void NewNode (int x);//Ещё одна функция добавления в дерево элементов. Написана Тагировым Равилем Рафкатовичем.
};

Tree::~Tree()//Деструктор. Если root не пустое и оформленное дерево - то такой код удалит абсолютно всё дерево?
{
	cout<<endl<<"Дерево удалено. "<<endl;
	if (root==NULL)
		{cout<<"..было передано уже пустое дерево!"; return;}
	else
	{
    Delete (root);
	root=NULL;
	}
}

void Tree::Delete ()
{
   Delete (root);
   root = NULL;
}

void Tree::Delete (Node * uzel)//Удаление узла(поддерева)
{
	if ( (uzel) != NULL )
	{
		Delete ((uzel)->left);
		Delete ((uzel)->right);
		delete (uzel);
	}
}

void Tree::Delete (int x)
{
	Node * * res = &root;
    while ( (* res)->info!=x )
       if ( x < (* res)->info )
          res = &((* res)->left);
       else
          res = &((* res)->right);
    Delete(*res);
	//cout<<(*res)->info;
	*res=NULL;	
}

//Cоздать ради интереса ДОБАВЛЕНИЕ в дерево и ПЕЧАТЬ:*/
void Tree::Push ()//Добавление в дерево элементов. Основа - создание сортированного дерева.
{
	int kol, x;
	cout<<endl<<"Введите количество чисел: "; cin>>kol;
	for (int i=0; i<kol; i++)
	{
		cout<<endl<<"Введите "<<i+1<<") ";
			cin>>x;
		Node* *res=&root;
		//Берём адрес корня. Чтобы изменять структуру создаваемого дерева без изменения корневого элемента.
		while(*res!=NULL)//Ищем место для присоединения новой ветки.
		{
			if (x==(*res)->info)
				{cout<<"Ошибка! Этот элемент уже был добавлен! "; return;}
			else if (x<(*res)->info)
				{res=&((*res)->left);}
			else if (x>(*res)->info)
				{res=&((*res)->right);}
		}
		*res=new Node(x); //-Создали узел.
	}
}

//Вывод дерева.
void Tree::Print()
{
	cout<<endl<<"Печать в ширину(по уровням): "<<endl;//Печать в ширину, что ли.

	if ( root == NULL )
		{ cout<<"Пустое дерево! "; return; }

	Node *&b=root;//Печать через очередь по уровням-ярусам! Запоминаем для дальнейшего присвоения.
	queue <Node *> q;
    q.push (root);
    while ( ! q.empty () )
	{
        Node * curr = q.front (); 
		q.pop ();
        cout << curr->info << " ";//Далее создаём в очереди новый поддуровень. А цикл пока обрабатывает элементы предыдущего!
        if ( curr->left != NULL )
            q.push (curr->left);
        if ( curr->right != NULL )
            q.push (curr->right);
    }
}

void Tree::NewNode (int x)//Ваша функция добавления элементов в дерево.
{
   Node * * r = &root;
   while ( * r != NULL )
      if ( x == (* r)->info )
         return;
      else if ( x < (* r)->info )
         r = &((* r)->left);
      else
         r = &((* r)->right);
   * r = new Node (x);
}

queue <Node*> Tree::Ochered()//Небольшая функция для разложения второго переданного дерева по списку(всех его веток, заложенных в очередь!).
{
	queue <Node*> q2,p2;
    q2.push(root);
    while (!q2.empty()) 
	{
        Node* curr = q2.front(); 
		p2.push(q2.front());
		q2.pop();
        if (curr->left!=NULL)
            q2.push(curr->left);;
        if (curr->right!=NULL)
            q2.push(curr->right);;
	}
	return p2;
}

void Tree::SravnTrees(Tree tr2)
{
	// В дальнейшем, чуть изменив код, можно производить сравнение деревьев ПО занесенной в них информации(а не только по структуре).
	//Сначала занесем все ветки основного дерева в очередь p. 
	queue <Node*> q1,p1;
    q1.push(root);
    while (!q1.empty()) 
	{
        Node* curr = q1.front(); 
		p1.push(q1.front());
		q1.pop();
        if ((curr)->left!=NULL)
            q1.push((curr)->left);;
        if ((curr)->right!=NULL)
            q1.push((curr)->right);;
	}
	queue <Node*> p2;
	p2=tr2.Ochered();
	int a=0;
	for(; p1.empty()!=true && p2.empty()!=true;p1.pop(), p2.pop())//Нерекурсивная функция сравнения двух очередей. Пока обе очереди не пустые.
	{
		if (p1.front()==NULL && p2.front()==NULL)
			a++;
		else if ((p1.front()!=NULL && p2.front()==NULL) || (p1.front()==NULL && p2.front()!=NULL))
			{cout<<endl<<"Разные деревья по структуре!"<<endl; return;}
		else
		{
			int l1=0, l2=0, r1=0, r2=0;
			/*if (p1.front()->left && p2.front()->left)
			{
				if (p1.front()->right && p2.front()->right)
				a++;
				else {cout<<endl<<"Raznie derevja2!"<<endl; return;}
			}
			else if (p1.front()->right && p2.front()->right)
			{
				if (p1.front()->left && p2.front()->left)
					a++;
				else {cout<<endl<<"Raznie derevja4!"<<endl; return;}
			}
			else {cout<<endl<<"Raznie derevja5!"<<endl; return;}*/
			if (p1.front()->left) l1++;;
			if (p2.front()->left) l2++;;
			if (p1.front()->right) r1++;;
			if (p2.front()->right) r2++;;
			if (l1!=l2 && r1!=r2) {cout<<endl<<"Одинаковые деревья!"<<endl; return;}
		}
	}		

	if (p2.empty()==false || p1.empty()==false)//Если какой-то список адресов остался не пуст.
			{cout<<"Это разные деревья по структуре!"<<endl; return;}
	else {cout<<endl<<"Одинаковые деревья!"<<endl; return;}
}

int main()
{
	Tree tr, tr2;
	int x;
	setlocale (LC_ALL, "Russian");
	//Работа с одним деревом:
	cout<<"Введите 1ое дерево: ";
    /*tr.NewNode (5);
	tr.Print();
    tr.NewNode (3);
	tr.Print();
    tr.NewNode (1);
	tr.Print();
    tr.NewNode (9);*/
	tr.Push();//Заполняем дерево.
	tr.Print(); //Печатаем его.

	if ( tr.Empty () )
       cout << "Пустое дерево!" << endl;
    else
       cout << endl << "Есть элементы в дереве! Не пустое дерево." << endl;  //Проверяем пустоту дерева

	//Удалим заданный элемент со всеми его подэлементами.
	cout<<endl<<"Введите элемента, что удалится со всеми своими полэлементами: ";
		cin>>x;
	tr.Delete(x);
	tr.Print();//Для наглядности выведем результат.


	tr.~Tree();//Деструктор!
    //либо (вместо деструктора): tr.Delete ();
	tr.Print();//Печатаем. Для проверки удаляемости.

	/*cout<<endl<<endl<<"Введите 2ое дерево "<<endl;
	tr2.Push();//Создаём ещё одно дерево. Для последующего сравнения.
	tr2.Print();
	cout<<endl; tr.SravnTrees(tr2);//Сравниваем два дерева.*/

	getch();
	return 0;
}