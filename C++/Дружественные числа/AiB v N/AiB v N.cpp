//������� ������: ����� ��� "�������������" ����� � ���� "�" � "�" �� 1 �� ��������� ���������� ����� "n".
// � ������� ������ �������� ����� � ������� skrin.jpeg, ��� ������ �� n=10000 �������� ����� 1 ����.
#include <iostream>
#include <conio.h>
using namespace std;

int main()
{
int n,p,k,t,i,y,r,u=0;
cout<<"Chislo N=";
cin>>n;

for(i=2;i<n;i++)//������� 1 ����� �� ��, ��� ����� ����� i.
{
	k=0;// � ������ ������ ���������� ��������.
	p=0;
	for ( t=1; t<i; t++)//������� ����� ��������� i �����.
		{
			if (i%t==0)
			k=k+t;
		}
	for (y=i+1; y<n; y++)//���� �������� ������������ �����, ������� ������, ��� "i", �.�. ������������� ����� "i" � n.
		{
				p=0;//C������ "p" ���������� � ������ ��������� "y".
				for (r=1; r<y; r++)//������� ����� "p" ��������� ����� �����.
					{
					if (y%r==0)
						p=p+r;
					}
				if ((k==y) && (p==i)) //���� ����� "������������", �� ���������� ����������� ����� �� �� �����.
					cout<<i<<" "<<y<<",  ";
				u++;// �������� �������� ����������� ��������. ��������� ���� ������� �� ���������.
		}
}

cout<<"Chislo podborov ravno= "<<u;
_getch();
return 0;
}