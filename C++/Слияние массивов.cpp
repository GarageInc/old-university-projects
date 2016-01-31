#include <stdio.h>
//#include <math.h>
#include <fstream>
#include <conio.h>

// размеры массивов - константы, чтобы
// можно было создать статические массивы
const int nA = 3;
const int nB = 3;

int* Merger(int *A, int *B, int* C) {
	for (int i = 0, j = 0; i < nA || j < nB;) {
		// если один из массивов пройден до конца - идём только по другому
		if (i == nA) {
			C[i+j] = B[j];
			j++;
			continue;
		}
		else if (j == nB) {
			C[i+j] = A[i];
			i++;
			continue;
		}

		// если элементы совпадают, записываем оба в C последовательно
		if (A[i] == B[j]) {
			C[i+j] = A[i];
			i++;
			C[i+j] = B[j];
			j++;
		}
		else if (A[i] > B[j]) {
			C[i+j] = B[j];
			j++;
		}
		else {
			C[i+j] = A[i];
			i++;
		}
	}

	return C;
}

int main() {
	setlocale(LC_ALL, "Russian");// Чтобы выводился русский язык в консоли

	// неубывающие массивы
	int A[nA];
	int B[nB];
	int C[nA+nB]; // сюда запишутся результат слияния

	// считываем массивы
	printf("Введите массив A из 3 элементов:\n");
	for (int i = 0; i < nA; i++)
		scanf("%i", &A[i]);

	printf("Введите массив B из 3 элементов:\n");
	for (int i = 0; i < nB; i++)
		scanf("%i", &B[i]);

	// находим слияние
	int* Cnew = Merger(A, B, C); 
	printf("Резулттат слияния:\n");
	for (int i = 0; i < nA+nB; i++) {
		printf("%d ", Cnew[i]);
	}

	_getch();
	return 0;
}