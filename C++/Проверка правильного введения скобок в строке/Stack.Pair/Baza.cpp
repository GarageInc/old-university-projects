#include<iostream>
#include<conio.h>
#include<stack>
#include<queue>
#include<algorithm>

using namespace std;
int const N=5;
typedef pair<int,int> Pair;

void Skobki()
{
	int const N=100;
	Pair p;
	stack <Pair> st;
	char com[N];
		cin.getline(com,100);
	int z=0;
	bool ok=true;
	if (com[0]=='(' || com[0]=='[')
		for (int i=0; com[i];)
		{
			if (com[i] && com[i]=='(')
			{
				while (com[i] && com[i]=='(')
					{z++;i++;}
				p.first='('; p.second=z; z=0; st.push(p);
			}
			else
			{
				if (com[i] && com[i]=='[')
				{
					while (com[i] && com[i]=='[')
						{z++; i++;}
					p.first='['; p.second=z; z=0; st.push(p);
				}
			}

			if (com[i]==')' || com[i]==']')
				if (st.empty()==false)
				{
					if (com[i]==')')
					{
						while (com [i] && com[i]==')')
							{z++;i++;}
						{
							if (st.top().first=='(' && z==st.top().second)
								st.pop(); 
							else {ok=false; break;}
						}
						z=0;
					}

					else
					if (com[i]==']')
					{
						while (com[i] && com[i]==']')
							{z++;i++;}
						{
							if (st.top().first=='[' && z==st.top().second)
								st.pop(); 
							else {ok=false; break;}
						}
						z=0;
					}
					z=0;
				}
				else {ok=false; break;}
			else ;
		}
	else ok=false;
	if (ok==true) cout<<"All right!"; else cout<<"Error!";
}

int main()
{
	
	/*Pair Olymp[N];

	for(int i=0; i<N; i++)
	{
		Olymp[i].second=i+1;
		cin>>Olymp[i].first;
	}
	sort(Olymp, Olymp+N);

	for(int i=0; i<N; i++)
	{
		cout<<endl<<Olymp[i].first<<" "<<Olymp[i].second;
	}*/
	/*Pair prov;
	stack <Pair>st;
	queue <Pair>q;
	for(int i=0, j=10; i<3; i++, j=j+3)//Ввод
	{
		prov.first=i; prov.second=j;
		st.push(prov);
		q.push(prov);
	}
	cout<<"Stack': ";
	for(; !st.empty(); )//Печать
	{
		cout>>st.top()<<" ";
		st.pop();
	}
	cout<<endl<<"Ochered': ";
	for(; !st.empty() ; )//Печать
	{
		cout>>q.front ()<<" ";
		q.pop();
	}*/


	Skobki();

	getch();
	return 0;
}
