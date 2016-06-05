// Решето Аткина.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>
#include <time.h>
using namespace std;

void createArr( static bool arr[], const unsigned long long int size ){
	for (long long int k = 2; k < size; k++)
		arr[k] = false;
	arr[2] = true;
	arr[3] = true;
}

void Print(bool arr[], const int size){
	int j = 0;
	for( int i = 2; i < size; i++ )
		if(arr[i] == true){
			//cout << i << endl;
			j++;
		}
	cout<< endl<<"Количество простых чисел  "<< j << endl;
}

void main(){
	setlocale(LC_CTYPE,"Russian");
	const unsigned long long int size = 1024*1024*8;
	unsigned long long int x2, y2, i, j, n;
	long double t1,t2;
	static bool arr[size];
 
	// Инициализация решета
	createArr(arr,size);
 
  t1=clock();
	// Предположительно простые - это целые с нечетным числом
	// представлений в данных квадратных формах.
	// x2 и y2 - это квадраты i и j (оптимизация).
	x2 = 0;
	for ( i = 1; i*i < size; i++ ){
		x2 += 2*i-1;
		y2 = 0;
		for (j = 1; j*j < size; j++){
			y2 += 2*j-1;
 
			n = 4 * x2 + y2;
			if ((n < size) && (n % 12 == 1 || n % 12 == 5))
				arr[n] = !arr[n];
 
			// n = 3 * x2 + y2; 
			n -= x2; // Оптимизация
			if ((n < size) && (n % 12 == 7))
				arr[n] = !arr[n];
 
			// n = 3 * x2 - y2;
			n -= 2 * y2; // Оптимизация
			if ((i > j) && (n < size) && (n % 12 == 11))
				arr[n] = !arr[n];
		}
	}
	// Отсеиваем кратные квадратам простых чисел в интервале [5, sqrt(size)].
	// (основной этап не может их отсеять)
	for (i = 5; i*i < size; i++){
		if (arr[i]){
			n = i*i;
			for (j = n; j < size; j += n){
				arr[j] = false;
			}
		}
	}
  t2=clock();

	// Вывод списка простых чисел в консоль.
	Print(arr,size);
	// Время выполнения поиска простых чисел
	cout << "Время " << (t2-t1)/CLOCKS_PER_SEC << "sec"<< endl;
	
	cin.get();
}