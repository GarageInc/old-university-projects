#include<iostream>
#include<conio.h>
#define __RRR__
/*
*	УСЛОВИЕ ЗАДАЧИ:
*		Найти мосты в графе. Даже оставшаяся точка - уже граф (с одной вершиной).
*	Суть задачи-строим поиск в глубину из двух точек. ЕСЛИ в обоих поисках произойдёт совпадение вершин ->не мост. Или мост:)
*	Для удобства - считывание со входного файла. 
*   1ая строка - количествоРёбер и количествоВершин. Остальные строки - ребра, соединенные между собой.
*	Граф неориентированный по условию задачи.
*
*	РЕШЕНИЕ: бесхитростное. Берём две вершины-концы ребра. Для каждой из двух точек - по динамич.массиву посещений. В результате, если после рекурсивного
*	обхода графа какия-то вершина в массиве посещенных отмеченна(в обоих!), то отсюда следует, что ребро ТОЧНО уже НЕ мост. Иначе - если вершины дважды в
*	массивах visitedA и visitedB не отмечены - то ребро является мостом!
*	//попробовать сделать прерывание уже в процессе рекурсивного поиска - сравнение двух массивов уже в рекурсии! Как следствие сократится используемые
*	память и время.
*/
using namespace std;

class Graph
{
private:
	int n;
	int **e;//Массив ребёр. И веса связей тоже.
public:
	Graph();
	Graph(int);
	Graph(int, int**);
	Graph(Graph&);
	void addEdge(int i, int j);
	void printMatrica();
	void clearMatrica();//Обнуляем матрицу для нового графа.
	void Brightes(int a, int b);
	void recSearchA(int i, int *visited);
	void recSearchB(int i, int *visited);
};
void Graph::clearMatrica()//Чистим матрицу графа. В задаче не используется, для интереса.
{
	for(int i=0; i<n; i++)
		/*for(int j=0; j<n; j++)
			e[i][j]=0;*/
			delete e[i];

}

void Graph::Brightes(int a, int b)
{
	//Самое трудное - писать на угад первые строки кода. А затем - их же шлифовать/подгонять.
	//Забьем массивы проверок нулями.
	int *visitedA;//Массив проверок для точки A
		visitedA=new int[n];
	int *visitedB;//Массив проверок для точки Б
		visitedB=new int[n];

	for(int i=0; i<n; i++)
	{
		visitedA[i]=0; visitedB[i]=0;
	}

	visitedA[a]=1;//Отметим эти точки как уже посещенные.
	visitedB[b]=1;

	//Отправляем в поиск по вершине А.
	for(int i=0; i<n; i++)
	{
		if (e[a][i]==1 && i!=b && visitedA[i]==0)//Чтобы из нашей вершины А не перейти случайно на вершину Б
		{
			visitedA[i]=1;
			recSearchA(i, visitedA);
		}
	}

	//Отправляем в поиск по вершине Б.
	for(int i=0; i<n; i++)
	{
		if (e[b][i]==1 && i!=a && visitedB[i]==0)//Чтобы из нашей вершины А не перейти случайно на вершину Б
		{
			visitedB[i]=1;
			recSearchB(i, visitedB);
		}
	}
	//Сравниваем и печатаем результат.
	bool ok=true;
	for(int i=0; i<n; i++)
	{
		if (visitedA[i]==1 && visitedB[i]==1) {ok=false; break;} //Если ==1 оба, то - вершина дваждый посещалась, значит, есть ещё один путь в граф.
	}
	if (ok) 
	{cout<<"Вершина с индексами "<<a<<" и "<<b<<" -мост;"<<endl;
	/*
	//Ради интереса напечатаем обе получившиеся матрицы
	for (int i=0; i<n; i++)
		cout<<visitedA[i]<<" ";
	cout<<endl;
	for (int i=0; i<n; i++)
		cout<<visitedA[i]<<" ";
	cout<<endl<<endl;*/
	}
	else /*cout<<a<<" "<<b<<" -НЕмост;"<<endl*/;
	delete []visitedA;//Удаляем созданные объекты.
	delete []visitedB;
}

//Рекурсивная функция поиска места для элемента. Обход графа.
void Graph::recSearchA(int j, int *visitedA)
{
	for(int i=0; i<n; i++)//Перебираем соседние вершины.
	{
		if (e[j][i]==1 && visitedA[i]==0)//Нашли соседнюю точку. 
		{	
			visitedA[i]=1;
			recSearchA(i, visitedA);
		}

	}
}

void Graph::recSearchB(int j, int *visitedB)
{
	for(int i=0; i<n; i++)//Перебираем соседние вершины.
	{
		if (e[j][i]==1 && visitedB[i]==0)//Нашли соседнюю точку. 
		{	
			visitedB[i]=1;
			recSearchB(i, visitedB);
		}
	}
}

Graph::Graph(int n)
{
	//Создаём матрицу графа.
	this->n=n;
	e=new int*[n];
	for(int i=0; i<n; i++)
	{
		e[i]=new int[n];
		for(int j=0; j<n; j++)
			e[i][j]=0;
	}
};

void Graph::addEdge(int i, int j)
{
		e[i][j]=1;
		e[j][i]=1;
}

void Graph::printMatrica()
{
	for(int i=0; i<n; i++)
	{
		for(int j=0; j<n; j++)
			cout<<e[i][j]<<" ";
		cout<<endl;
	}
}

int main()
{
	#ifdef __RRR__
	setlocale (LC_ALL, "Russian");
	freopen("Musor.txt","r",stdin);//Выводим в консоль.
	//freopen("Output for Sample Input.txt","w",stdout);
	#endif

	int kolReber, kolVershin, i, j;
	cin>>kolReber;	cin>>kolVershin;//Считываем соответственно количество рёбер и вершин в графе.
	Graph gr(kolVershin);//Создаём граф.
	typedef pair <int, int> Pair;//Новый тип для массива хранения ребёр.
	Pair *ver;//Создаём динамический массивы для хранения рёбер.

	//Создали два динамических массива для хранения рёбер.
	ver=new Pair[kolReber];
	for(int t=0; t<kolReber; t++)//Обрабатываем входной файл.
	{
		cin>>i; cin>>j;//Считываем ребро
		ver[t].first=i; ver[t].second=j; // Запоминаем параметры ребра.
		gr.addEdge(i,j);
	}
	//gr.printMatrica(); //Печать матрицы. Для проверки правильности ввода.
	//Исследуем граф на наличие мостов.
	for(int t=0; t<kolReber; t++)
	{
		//Передаём в функцию ребро для определения - мост ли это?
		gr.Brightes(ver[t].first, ver[t].second);
	}

	//И не забыть удалить созданные элементы:
	delete []ver;

	getch();
	return 0;
}