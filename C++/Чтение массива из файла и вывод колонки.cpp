#include <stdio.h>
#include <math.h>
#include <fstream>
#include <conio.h>


void GetColumn(int i)
{
	FILE *f = fopen("file.txt", "r"); 
	int h=0,w=0;// Высота и ширина массива
	fscanf(f,"%d",&h);//Cчитываем высоту и ширину int
	fscanf(f,"%d",&w);

	//Создаем и считываем наш двумерный массив по-строчно
	double ** mass=new double*[h];
	for(int i=0; i<h; i++)
	{
		mass[i]=new double[w];
		for(int j=0; j<w; j++)
			fscanf(f,"%f",&mass[i][j]);
	}

	if (i>w) // если указан номер колонки, которой нет в массиве
	{
		printf("Не верно указан номер колонки!");
		return;
	}
	else
	{
	//Теперь обрабатываем - выведем нашу колонну
	for(int j=0; j<h; j++)
		printf("%f",mass[j][i]);
	}
}

int main()
{
	setlocale(LC_ALL, "Russian");// Чтобы выводился русский язык в консоли

	
	int n=0;// номер колонки
	printf("Введите номер колонки в массиве:\n");
	scanf("%i",&n);
	
	//Вызываем нашу функцию
	GetColumn(n);

	_getch();
	return 0;
	
}