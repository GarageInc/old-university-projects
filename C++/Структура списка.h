struct E
{
	int info;
	E *next;
	E(int x=0,E *p=NULL)
	{
		info=x;
		next=p;
	}

};

void addEnd(E *&p,int x)
{
	E *q=p;
	while(q->next!=NULL)
		q=q->next;
	q->next=new E;
	q->next->info=x;
	q->next->next=NULL;
}
void addBegin(E *&p,int x)
{
	E *q=p;
	p=new E;
	p->info=x;
	p->next=q;
}
void addInsert(E *&p,int x,int pos)
{
	E *q=p;
	int i=pos;
	while(--i>1)
		q=q->next;
	E *g=q->next;

	E *chislo;
	chislo=new E;
	chislo->info=x;
	chislo->next=NULL;

	q->next=chislo;
	q->next->next=g;
}

void delEnd(E *&p)
{
	E *q=p;
	while(q->next->next!=NULL)
		q=q->next;
	delete q->next->next;
	q->next=NULL;
}

void delBegin(E *&p)
{
	E *q=p;
	p=p->next;
	delete q;
}

void delInsert(E *&p,int pos)
{
	E *q=p;
	int i=pos;
	while(--i>1)
		q=q->next;
	E *g=q->next->next;
	delete q->next;
	q->next=g;
}

void addNext(E *&p, int x, int chislo)
{
	E *q=p;
	while(q->info!=chislo)
		q=q->next;
	E *c=new E;
	c->info=x;
	E *g=q->next;
	q->next=c;
	q->next->next=g;
}

void addUp(E *&p,int x)
{
	E *q=p;
	while(q->next->info<=x)
		q=q->next;
	E *g=new E(x,q->next);
	q->next=g;
}

void delObject(E *&p,int chislo)
{
	for(E *q=p;q!=NULL;q=q->next)
	{
		if(q->next->info==chislo && q->next->next!=NULL)
		{
			E *g=q->next->next;
			delete q->next;
			q->next=g;
		}
		else 
		{
			E *g=NULL;
			delete q->next;
			q->next=g;
		}
		}
	}
}

int print(E *p)
{
	int k=0;
	E *q=p;
	while(q!=NULL)
	{
		k++;
		cout<<q->info<<" ";
		q=q->next;
	}
	cout<<endl;
	return k;
}


void inputAll(E *&p,int n)
{
	int x;
	cin>>x;
	p=new E;
	p->info=x;
	p->next=NULL;
	int k=n-1;
	E *q=p;
	while(k>0)
	{
		k--;
		cin>>x;
		q->next=new E;
		q->next->info=x;
		q->next->next=NULL;
		q=q->next;
	}
}

int searchBig(E *p)
{
	E *q=p;
	int max=p->info;
	while(q!=NULL)
	{
		if(max<q->info)
			max=q->info;
		q=q->next;
	}
	return max;
}

int sumObject(E *p)
{
	int sum=0;
	for(E *q=p;q!=NULL;q=q->next)
		sum+=q->info;
	return sum;
}

void DeleteTree(Node *)