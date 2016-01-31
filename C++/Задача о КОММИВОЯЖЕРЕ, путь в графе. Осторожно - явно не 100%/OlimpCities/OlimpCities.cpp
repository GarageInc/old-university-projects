#include <iostream>
#include <fstream>
#include <math.h>
#include <conio.h>

using namespace std;
const int N = 256;

//Проверяем - есть ли в массиве непосещенных вершин непосещенные
bool Proverka(int* TF, int n)
{
	for(int i=0; i<n; i++)
	{
		if (TF[i]==-1) return 0;
	}
	return 1;//Значит - все вершины уже посетили
}

int MIN_path;//Глобальная переменная для получения значений кратчайшего пути

//Основная обработка
void Obrabotka(int* a, int* b, int* c, int* TF, int vershina, int rebra, int sum, int next)
{
	//Рассчитываем каждый раз по новой количество вершин. Это "съедает" дополнительную память, но нам же это не критично :)
	int n;
	n = (1 + sqrt(1.0 + 8 * rebra)) / 2;
	next++;
	//Идем по а[]
	for(int i=0; i<rebra; i++)
	{//Ищем ветви и отмечаем посещенность этой вершины
		if(a[i]==vershina && TF[b[i]-1]<0)//Если этот узел существует и какой-то его сосед ещё не посещён- идем по его соседу
		{
			//Т.к. копируются только ссылки - нужно заново строить массив посещений и его передавать
			int* tf=new int[n];
			for(int i=0; i<n; i++)
				tf[i]=TF[i];

			if (tf[b[i]-1]==1 && !Proverka(tf,n))//Если же вдруг окажется, что соседняя вершина - исходная, а остальные вершины - ещё не посещены, то это есть тупиковый вариант. Тупик рекурсии -> прервать поиск по этой ветви
				continue;
			else
				tf[b[i]-1]=next;//Иначе - отмечаем вершину уже как посещенную
			//Идем дальше по вершине
			
			Obrabotka(a,b,c,tf,b[i],rebra,sum+c[i],next);

			delete[] tf;
		}
	}
	//Идем по b[]
	for(int i=0; i<rebra; i++)
	{//Ищем ветви и отмечаем посещенность этой вершины
		if(b[i]==vershina && TF[a[i]-1]<0)
		{
			int* tf=new int[n+1];
			for(int i=0; i<n+1; i++)
				tf[i]=TF[i];

			if (tf[a[i]-1]==1 && !Proverka(tf,n))
				continue;
			else
				tf[a[i]-1]=next;
			//Идем дальше по вершине
			
			Obrabotka(a,b,c,tf,a[i],rebra,sum+c[i],next);
			
			delete[] tf;
		}
	}	

	//Проверим - а если все вершины кроме исходной уже пройдены?
	if(Proverka(TF,n))
	{
		//cout<<"!!!";
		for(int i=0; i<n; i++)
		{
			if(a[i]==vershina && TF[b[i]-1]==1)
			{/*
				for(int i=0;i<n;i++)
					cout<<TF[i];
				cout<<endl;
				cout<<"c[i]="<<c[i]<<endl;
				cout<<"end "<<sum+c[i]<<endl;;
				*/
				if (MIN_path>sum+c[i])
					MIN_path=sum+c[i];
			}
		}
		for(int i=0; i<n; i++)
		{
			if(b[i]==vershina && TF[a[i]-1]==1)
			{/*
				for(int i=0;i<n;i++)
					cout<<TF[i];
				cout<<endl;
				cout<<"c[i]="<<c[i]<<endl;
				cout<<"end "<<sum+c[i]<<endl<<endl;;
				*/
				if (sum+c[i]<MIN_path)
					MIN_path=sum+c[i];
			}
		}
	}
}



int main()
{
    setlocale(LC_ALL, "Russian");

	int rebra;
	//Считываем
	ifstream f("Sample Input.txt");
	f>>rebra;
	int* a = new int[rebra];
	int* b = new int[rebra];
	int* c = new int[rebra];
	for (int i = 0; i < rebra; i++)
		f >> a[i] >> b[i] >> c[i];
	f.close();
	
	cout << "Ребер в графе = " << rebra << endl;
	int n;
	n = (1 + sqrt(1.0 + 8 * rebra)) / 2;
	cout << "Максимальное число вершин = " << n << endl;
	
	//Записываем-выводим
	int i;
	ofstream f1("Output for Sample Input.txt");

	//Вывод просто для проверки
	//for(int i=0; i<rebra; i++)
	//	f1<<a[i]<<" "<<b[i]<<" "<<c[i]<<endl;

	ofstream f2("Output for Sample Input.txt");
	for(int q=1; q<=n; q++)
	{
		//Инициализируем нашу внешнюю переменную НАИКРАТЧАЙШЕГО пути МАКСИМАЛЬНЫМ значением.
		MIN_path=10000;
		//Заводим булевый массив посещений узлов
		int* TF=new int[n];
		for(int i=0; i<n; i++)
			TF[i]=-1;

		TF[q-1]=1;//Помечаем исходную вершину 
			
		Obrabotka(a,b,c,TF,q,rebra,0,1);//q-исходная вершина
			cout<<endl;

		//cout<<"Кратчайший путь для вершины №"<<q<<" = "<<MIN_path<<endl;
		f2<<"Кратчайший путь для вершины №"<<q<<" = "<<MIN_path;
		delete[] TF;
	}
	f2.close();
	cout<<endl<<"END";

	//Удаляем мусор
	delete[] a;
	delete[] b;
	delete[] c;
	f1.close();
	getch();
	return 0;
}