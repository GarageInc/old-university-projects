#include<iostream>
#include<conio.h>
#include<stack>
#include<queue>
using namespace std;

struct Node
{
	int info;
	Node *left, *right;
	Node (int x)
	{
		info=x; left=NULL; right=NULL;
	}
};


//Печать всех элементов, рекурсивный метод.
void PrintR(Node *r)
{
	if (r==NULL) return;
	else 
	{
		PrintR(r->left);
		cout<<r->info<<" ";
		PrintR(r->right);
	}
}

//Вывод без рекурсии.
void PrintUnR(Node *d)
{
	stack <Node*> q;
	Node *r=d;
L1:	if (r!=NULL) 
	{
		q.push(r);
		r=r->right;
		goto L1;
	}
	else
	{
		//r==NULL
		if (q.empty()==false) {r=q.top(); q.pop(); cout<<r->info<<" "; r=r->left; goto L1;} 
		else goto L2;
	}

L2:
	return;
}

bool SravnVetok(Node *p1, Node *p2)
{
	bool ok=true;
	if (p1==NULL && p2==NULL) return ok;
	else if (p1!=NULL && p2!=NULL)
	{
		if (p1->left==NULL && p2->left==NULL) return ok;
		else if (p1->left!=NULL && p2->left!=NULL)
			SravnVetok(p1->left, p2->left);
		else {ok=false; return ok;}

		if (p1->right==NULL && p2->right==NULL) return ok;
		else if (p1->right!=NULL && p2->right!=NULL)
			SravnVetok(p1->right, p2->right);
		else {ok=false; return ok;}
	}
	else	{ok=false; return ok;}
}

queue <Node*> q;

void PrintLevel1(Node *root)//Забьём все адреса в очередь.
{
	if (root!=NULL)
	{
		q.push(root);
		PrintLevel1(root->left);
		PrintLevel1(root->right);
	}
	else return;
}
//Печатаем адреса!
void PrintLevel2(queue <Node*> q, Node* root)
{
D1:
	if (q.empty()==false)
	{
		cout<<q.front()->info<<" ";
		q.pop();
		goto D1;
	}
	else
		return;

}

void PrintLevel3(Node *root)
{
	if (!root)
        return;
    q.push(root);
    while (!q.empty()) 
	{
        Node *curr = q.front(); 
		q.pop();
        cout<< curr->info<<" ";//Далее создаём в очереди новый поддуровень. А цикл пока обрабатывает элементы предыдущего!
        if (curr->left)
            q.push(curr->left);;
        if (curr->right)
            q.push(curr->right);;
    }
}

//Добавление элемента по возрастанию.
void Add (Node *&p, int x)
{
	if (p==NULL)
			p=new Node(x);
	else if (p->info==x)
		{cout<<"ERROR! This element was declared! :  "; return;}
	else if (p->info>x)
		Add(p->left, x);
	else Add(p->right, x);
}

int main()
{	
	//Ввод дерева. Внесение в него определенного элемента по возрастанию.
	Node *root=NULL;
	root=new Node(10);
	root->left=new Node(21);
	root->left->left=new Node(31);
	root->left->right=new Node(32);
	root->right=new Node(22);
	root->right->left=new Node(33);
	root->right->right=new Node(34);
	/*PrintR(root); cout<<endl<<endl;
	int x; cout<<"Vvedite element dl'a vstavki:"; cin>>x;
	Add(root, x);
	PrintR(root);*/
	
	/*cout<<endl;
	PrintUnR(root);*/
	//Сравнение двух деревьев. Попытка не пытка.

	/*Node *p1=NULL;
	p1=new Node(3);
	p1->left=new Node(1);
	p1->right=new Node(17);
	p1->right->left=new Node(5);

	Node *p2=NULL;
	p2=new Node(3);
	p2->left=new Node(1);
	p2->right=new Node(17);
	p2->right->right=new Node(20);
	p2->right->left=new Node(5);

	if (SravnVetok(p1,p2)==true) cout<<"TRUE"; else cout<<"FALSE";*/
	
	/*cout<<endl<<"By recursia: ";
		PrintR(root);
	cout<<endl<<"By stek: ";
		PrintUnR(root);*/
	PrintR(root);
	
	cout<<endl<<"Level:"<<endl;
	/*PrintLevel1(root);
	PrintLevel2(q, root); cout<<end;*/

	PrintLevel3(root);

	getch();
	return 0;
}