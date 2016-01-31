#include<iostream>//Условия задач для группы "Информационная безопасность".
#include<conio.h>
#include<string>
using namespace std;
/*
* Задача №1. Заполнить кинотеатр местами. Подаются запросы на количество мест и ряд. (гр."Информационная безопасность")
*
* Задача №2. Подаётся строка цифр. За один алгоритм - перевернуть строку, прикрепить её ко всей строке и удалить n-количество элементов с конца строки
* согласно номеру шага шагу.
* Количество задаваемых алгоритвом - 8(шаг=алгоритм). В функции предусматривается самостоятельное задание количества алгоритмов.
*
*/
int const N=1000;
int mass[N][N];//Универсальный массив как для кинотеатра, так и для любой другой задачи.
char stroka[N];
//Функции для задачи "КИНОТЕАТР":
bool ProvPustota(int n, int m)//Проверяем пустоту кинотеатра,a - ряд, x-место.
{
	bool ok=false;
	for(int y=0; y<n; y++)
		for(int b=0; b<m; b++)
		{
			if (mass[y][b]==0) {ok=true; return ok;};
		}
	return ok;
}
void Prov(int a, int x, int m)//Печатаем возможность бронирования мест. Либо невозможность. a-ряд,x-количество мест,m-ширина ряда(кинотеатра).
{
	//Ищем первое пустое место.
	bool ok=false;
	int mesto1, schet=0, mesto2;
	for(int q=0; q<m; q++)
	{
		if (q==m-1) {cout<<"NO places!"<<endl; return;}//Если весь ряд уже занят - то печать.
		else if (mass[a][q]==0) {mesto1=q; break;}
	}
	//Теперь проверяем на возможность заполнения.
	for(int q=mesto1; q<m; q++)
	{
		//Можно ли заполнить?
		if (mass[a][q]==0) schet++; else {cout<<"NO places!"<<endl; return;}
		if (schet==x) mesto2=q;
	}
	//Заполняем
	if (schet<x)  {cout<<"NO places!"<<endl; return;}
	schet=mesto1;
	while (mesto1<=mesto2)
	{
		mass[a][mesto1]=1;
		mesto1++;
	}
	cout<<"Yes! "<<"Places from "<<schet+1<<" to "<<mesto2+1<<endl;
}
void Kinoteatr()//Основная функция задания задач кинотеатра.
{
	int n,m;
	cout<<"Vvedite n=";
		cin>>n;
	cout<<"Vvedite m=";
		cin>>m;
	//Забьём нулями места в кинотеатре.
	for (int i=0; i<n; i++)
		for(int j=0; j<m; j++)
			mass[i][j]=0;
	//Подача запросов.
	int zaprosRjad, zaprosMest;
	for(int i=0; i<n*m; i++)
	{
	L1:
		{if (ProvPustota(n,m)==false) {cout<<"Ok! Polniy kinoteatr! NO PLACES. "<<endl; return;}}//Проверяем кинотеатр на заполненность.
		cout<<endl<<"Vvedite rjad="; cin>>zaprosRjad;
		cout<<"Vvedite kol.mest="; cin>>zaprosMest;
		if (zaprosRjad>n || zaprosMest>m) {cout<<"Error! Povtorite popitku!"; goto L1;}
		Prov(zaprosRjad-1,zaprosMest, m);//Проверяем и вбиваем если можно. Если Да - то печатаем начало-конец. Иначе - NO. 
	}
}

//Функции для задачи "СТРОКА ЧИСЕЛ"
void Algorithm(int i)
{
	//Копируем часть
	int m=strlen(stroka);
	int z=m-i;
	for(int j=1; j<=z; j++)//Одновременно с этим вводим ограничение на отрубание последних элементов
	{
		stroka[m+j-1]=stroka[m-j];
	}
}
void StrokaChisel()//Основная функция по реализации "Строки из чисел".
{
	//Вводим строку.
	cout<<endl<<"Vvedite stroku: ";
	cin.getline(stroka, 100);
	int n;//Количество алгоритмов.
	cout<<"Vvedite kolichestvo algorithmov= "; cin>>n;
	
	for (int i=0; i<8; i++)
	{
		Algorithm(i+1);//Передаём в алгоритм номер шага.
		cout<<endl<<stroka<<endl;//Для визуальности выводим строку.
	}
	cout<<"Element pod nomerom 102: "<<stroka[101]<<endl;
		cout<<"Element pod nomerom 302: "<<stroka[301]<<endl;
			cout<<"Element pod nomerom 502: "<<stroka[501]<<endl;

}

//Функция для задачи про три логические переменные a,b,c.
bool TrueFalse(bool a, bool b, bool c)//Условие задачи немного недопонял. А именно - как именно комбинируются a.b.c. Булевы функции либо независимо от взаимодействий между собой?
{
	/*
	* Есть три комбинации "ложь" - если b "ложь" либо с "ложь".
	* Но условие истинности а стоит первее, т.е. задаёт истинность изначально. Иначе - проверка b и c.
	*
	*
	*
	*/
	if (a==1) return 1; 
	else if (b==0) return 0;
	else if (c==0) return 0;
	else return 1;
	//Какие 8 комбинаций всё-таки имелись в виду? Такие?
	/*
	*abc
	*000|0or1or0->  0
	*001|0or1->		0
	*010|0->		0
	*011|1->		1
	*100|1or0or0->	1
	*101|1or1->		1
	*110|1or0->		1
	*111|1->		1
	*/
}

int main()
{
	/*cout<<"Zadacha kinoteatr: "<<endl;
		Kinoteatr();

	cout<<endl<<"Zadacha o stroke iz chisel: "<<endl;
		StrokaChisel();
		*/
	cout<<endl<<"Logicheskaya zadacha: "<<endl<<"Vvedite logicheskie peremennie: ";
	int a,b,c;
	for(int i=0; i<8; i++)//Ьногократное повторение для выявления правильности решения.
	{
		cout<<endl<<"Vvod: ";
		cin>>a>>b>>c;
		cout<<"Vivod: "<<TrueFalse(a,b,c)<<endl;
	}

	getch();
	return 0;
}