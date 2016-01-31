#include<iostream>
#include<conio.h>
#include<stack>
using namespace std;
typedef pair <int,int> Pair;
/*
Известно, что дерево строить не нужно. Оно нам задаётся в текстовом формате.
1) Первоначально - сосчитать максимальную глубину. Число с максимальной глубиной и есть корень дерева! Явно - использование стека. Перевод в двоичный массив.
2) Использовать стек с параметром пар. 
Берётся исходное число-родитель. Оно удаляется

*/
//Двоичное дерево
struct Node
{
	int info;
	Node *left, *right;
	Node (int x)
	{
		info=x; left=NULL; right=NULL;
	}
};

//Печать двоичного дерева.
void PrintR(Node *r)
{
	if (r==NULL) return;
	else 
	{
		cout<<r->info<<" ";
		PrintR(r->left);
		PrintR(r->right);
	}
}

//Добавление элемента. Создание левого поддерева.
void Add (Node *&p, int x)
{
	for(;p!=NULL;p=p->left)
		;
	p=new Node(x);
}

void DeleteInTree(Node* &p)
{
	Node *q=p;
	for(;p!=NULL;)
	{
		q=p;
		p=p->left;
		delete q;
	}
	delete p;
}
/*
void Delete(stack <Node*> &p,int chislo)
{
	for(stack <Node*> q=p; q!=NULL;q=q->next)
	{
		if(q->next->info==chislo && q->next->next!=NULL)
		{
			Node *g=q->next->next;
			delete q->next;
			q->next=g;
		}
		else 
		{
			Node *g=NULL;
			delete q->next;
			q->next=g;
		}
	}
}
*/
int main()
{
	int const N=100;
	Pair a[N];
	int kol, ut[N];
	freopen("input.txt","r",stdin);
	freopen("output.txt","w",stdout);

	cin>>kol;
	kol=kol;
	cout<<kol<<endl;
	for(int i=0;i<kol; i++)
	{
		cin>>a[i].first; cin>>a[i].second;
		cout<<a[i].first<<"_"<<a[i].second<<endl;
	}
	//Найдем корень.
	//Найдём корневой элемент.
	/*Известно при анализе таблицы, что корневой элемент не является потомком какого-либо! Значит - он не должен повториться во втором
	столбце.
	*/
	int podschet;
	for (int i=0; i<kol; i++)
	{
		podschet=0;
		for (int j=0; j<kol; j++)
		{
			if (a[i].first==a[j].second) podschet++;
		}
		if (podschet==0) {podschet=a[i].first; break;}
	}

	//Сначала создадим "кучу" деревьев.
	stack <Node*>tree;
	Node* inTree=NULL;
	int kolTrees=0, b;//Считаем количество поддеревьев.
	for(int i=0;i<kol;i++)
	{
		if (a[i].first!=NULL && a[i].second!=NULL)
		{	
			Add(inTree, a[i].first);//Запоминаем заголовочное число-подкорень
			Add(inTree, a[i].second);
			b=a[i].first;
			a[i].first=NULL;
			for(int j=0; j<kol; j++)
			{
				if (a[j].first!=NULL && a[j].first==b)
				{
					cout<<"a";
					Add(inTree, a[j].second);
					a[j].first=NULL;
				}
			}
			//Заносим заголовок в специальный стек, в котором запоминются адреса заголовков-подкорней
			ut[kolTrees]=a[i].first;
			kolTrees++;
		}
	}
	for(int i=0; i<kolTrees; i++)
	{
		cout<<ut[i]<<" ";
	}
	/*
	//Собственно начинаем строить дерево.
	//Ищем поддерево с главным корнем. Запоминаем его и удаляем!
	/*
	Node *root=NULL;
	root=new Node(3);
	*//*
	stack <Node*>q=tree;
	Node *root=NULL, *p=NULL;
	for(int i=0; i<kolTrees; i++)
	{
		p=q.top();
		if (p->info==podschet) break; else q.pop();
	}
	root=p;//Запомнили.
	Delete(tree,p->info);//Удалили p из стека.
	//Запуск функции
	*/
	

	Node *z=tree.top();
	PrintR(z);
	getch();
	return 0;
}