#include <iostream>
#include <stdio.h>
#include <math.h>
#include <fstream>
#include <conio.h>

#include <time.h>

#include <thread>
#include <chrono>
#include <future>
#include <mutex>

using namespace std;

mutex g_lock;
int a[] = { 2, 3 };
ofstream fout;

int countThreads = 16;
std::thread myThreads[100];

// Функция для умножения двух чисел x,y по модулю m
long long mulmod(long long *x, long long *y, long long *m)
{
	return ((*x)*(*y)) % (*m);
}

// Функция возведения числа x в степен а по модулю n
long long powmod(long long x, int a, long long *m)
{
	long long r = 1;

	while (a > 0)
	{
		if (a % 2 != 0)
			r = mulmod(&r, &x, m);
		a = a >> 1;
		x = mulmod(&x, &x, m);
	}

	return r;
}

// Функция теста Миллера-Рабина
bool test_Miller_Rabin(long long *m, int *a) {
	if (*m == 2 || *m == 3)
		return true;

	if (*m % 2 == 0 || *m == 1) {
		return false;
	}
	// Сначала мы отсеяли самые простые случаи

	long long s = 0;
	long long t = *m - 1;
	long long x = 0;
	long long y = 0;

	// Считаем количество степени двойки
	while (t != 0 && t % 2 == 0) {
		s++;
		t = t >> 1;
	}

	// Реализация проверок по возведению в степень по модулю 'n'
	long r = 1;

	x = powmod(*a, t, m);

	if (x == 1 || x == *m - 1)
		return true;

	x = powmod(
		(long)pow(*a, t),
		(long)pow(2.0, r),
		m);

	if (x == 1)
		return false;

	if (x != *m - 1)
		return false;

	return true;
}


// Функция проверки, использует тест Миллера-Рабина
bool ПровереноТестомМиллераРабина(long long *number)
{
	for (int i = 0; i < 2; i++)
	{
		// Запускаем тест
		if (!test_Miller_Rabin(number, &a[i]))
		{
			return false;
		}
	}
	return true;
}

bool ПровереноПростымДелением(long long *number) {
	long long s = sqrt(*number) + 1;


	// нет смысла рассматривать четные числа, т.к. они не делятся на два
	for (long long i = 3; i <= s; i += 2) {
		if ((*number) % i == 0) {
			return false;
		}
	}


	return true;
}

void printValue(long long *i) {
	g_lock.lock();

	//cout << *i << endl;
	fout << *i << endl;

	g_lock.unlock();
}

void threadFunction( long long start, long long finish)//atomic<bool>& ab)
{
	if (start % 2 == 0) start++;

	for (long long i = start; i < finish; i++) {

		if (ПровереноТестомМиллераРабина(&i) && !ПровереноПростымДелением(&i)) {
			
			printValue( &i );
		}
		else {

			//pass	
		}
	}
}



int main()
{
	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке

	fout.open("mytest.txt", ios::out);

	int num_cores = ::thread::hardware_concurrency();
	fout << endl << "Количество ядер: " << num_cores << endl << endl;

	countThreads = num_cores;
	fout << endl << "Количество потоков: " << countThreads << endl << endl;
	
	long long start = 3;
	long long finish = pow(10, 12);
	long long step = pow(10,9);

	fout << start << " to "<< finish << " by step " << step << endl << endl;

	int j = 0;

	atomic<bool> doned[100];//;(false); // Use an atomic flag.

	for (j = 0; j < countThreads; j++) {
		doned[j] = false;// = std::atomic<bool>(false);
	}

	double koef = 1 + 1 / (double)countThreads;
	cout << koef;

	clock_t t1, t2;
	t1 = clock();
	
	for (long long i = start; i < finish; ) {

		for (j = 0; j < countThreads && i + step <= finish; j++ ) {
			
			if ( doned[j] = true ) {
				doned[j] = false;
				myThreads[j] = std::thread([&doned, i, step, j] {threadFunction(i + 1, i + step);  doned[j] = true; });
				myThreads[j].detach();

				cout << "Запущен: " << j << " на промежутке [" << (i+1) << " - " << (i + step) <<"]"<< endl;
				i += step;

				if (step > 10000000) {
					step = step / ( koef );
				}
			}
		}
	}

	for (j = 0; j < countThreads; j++) {
		while( !doned[j] ) {
			this_thread::sleep_for( 1ms );
			//cout <<"Не спит: "<< j<< endl;
			//myThreads[j].join();
		}
	}

	t2 = clock();
	float diff((float)t2 - (float)t1);
	fout << "Time:" << diff << " milliseconds"<< endl;
		
	fout.close();

	cout << endl << "END";

	getch();
	return 0;
}

