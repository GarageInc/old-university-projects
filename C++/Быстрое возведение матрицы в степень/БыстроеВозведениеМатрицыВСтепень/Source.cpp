#include <iostream>
#include <fstream>
#include <cmath>
#include<conio.h>
#include <algorithm>

using namespace std;

// Длинные целые числа(для хранения результатов)
long long n, m;
long long result[1000][1000];// для хранения результата перемножения
long long matrix[1000][1000]; // начальная матрица которую нужно возвести в степень
long long answer[1000][1000]; // для конечного ответа

// Печать массивов
void Print(int n)
{
	cout << "Массив-результат:"<<endl;
	for (int i = 0; i<n; i++)
	{
		for (int j = 0; j<n; j++)
			cout << answer[i][j] << " ";
		cout << endl;
	}
}

// Умножением матриц - передаются умножаемые матрицы и размерность
void Mult(long long matrix1[1000][1000], long long matrix2[1000][1000], int n)
{
	for (int i = 0; i<n; i++)
		for (int j = 0; j<n; j++)
		{
			result[i][j] = 0;
			for (int k = 0; k <n; k++)
			{
				result[i][j] += matrix1[i][k] * matrix2[k][j];
			}
		}

	for (int i = 0; i<n; i++)
		for (int j = 0; j<n; j++)
			matrix1[i][j] = result[i][j];
}

// Функция быстрого возведения в степень
void ans(int n, int m)
{
	while (m)// m - своеобразный счетчик
	{
		if (m & 1)
			Mult(answer, matrix, n);

		Mult(matrix, matrix, n);

		m >>= 1;// Сдвигаем счетчик вправо
	}
}

int main()
{
	setlocale(LC_ALL, "Russian");// Вывод русского языка
	//	freopen("input.txt","r",stdin);
	//	freopen("output.txt","w",stdout);
	// n - размерность ( матрица квадратной должна быть)
	// m - степень
	cout << "Введите параметры массива(количество строк/столбцов) и степень:";
	cin >> n >> m;

	cout << "Введите элементы массива";
	
	// Инициализируем промежуточный массив во время ввода основного:
	for (int i = 0; i<n; i++)
		for (int j = 0; j<n; j++)
		{
			cout << "mass[" << i << "][" << j << "] = ";
			cin >> matrix[i][j];
			answer[i][j] = matrix[i][j];

			cout << endl;
		}

	// Возводим в степень:
	ans(n, m - 1);

	// печатаем результат(который в массиве answer)
	Print(n);

	getch();
	
	return 0;
}
