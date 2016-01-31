#include <stdio.h>
#include <fstream>
#include <conio.h>

int const N=1000; // Константа для статических массивов
	
// Слияние последовательностей - массивов
int* Merger(int *A, int na, int *B, int nb)
{
	static int C[N];
	//Выполняем слияние
	for(int i=0, j=0; i<=na && j<=nb;)
	{
		while(A[i]<=B[j] && i<=na)
		{
			C[i+j]=A[i];
			i++;
		}
		while(B[j]<=A[i] && j<=nb)
		{
			C[i+j]=B[j];
			j++;
		}
	}

	return C;
}

int main()
{
	// Для чтения русского языка в консоли
	setlocale(LC_ALL, "Russian");
	
	//Создаем массивы наших последовательностей
	int A[N], B[N];
	int na=0,nb=0;

	//Ввод данных - про наши последовательности
	printf("Введите длину 1ой последовательности: \n");
	scanf("%i", &na);
	printf("Введите первую НЕУБЫВАЮЩУЮ последовательность через Enter: ");
	for(int i=0; i<na; i++)
	{
		scanf("%i",&A[i]);
	}

	printf("Введите длину 2ой последовательности: \n");
	scanf("%i", &nb);
	printf("Введите вторую НЕУБЫВАЮЩУЮ последовательность через Enter: ");
	for(int i=0; i<nb; i++)
	{
		scanf("%i",&B[i]);
	}

	int * C=Merger(A,na,B,nb);
	printf("Результат слияния:\n");
	for(int i=0; i<nb+na; i++)
	{
		printf("%i, ",C[i]);
	}

	_getch();// Пауза экрана - чтобы он не гас
	return 0;
	
}