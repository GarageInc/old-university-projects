#include <iostream>
#include <stdio.h>
#include <math.h>
#include <fstream>
#include <conio.h>
#include <string>

#include <time.h>

#include <thread>
#include <chrono>
#include <future>
#include <mutex>

using namespace std;

int A[] = { 2, 3 };

int THREADS_COUNT = 0;
const int MAX_THREADS_COUNT = 100;

thread THREADS[ MAX_THREADS_COUNT ];


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
		if (!test_Miller_Rabin(number, &A[i]))
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

void printValue(long long *i, FILE *fout) {

	fprintf(fout, "%d \n", i);
}

void threadFunction( long long start, long long finish, FILE *fout)//atomic<bool>& ab)
{
	if (start % 2 == 0) start++;

	for (long long i = start; i < finish; i++) {

		if (ПровереноТестомМиллераРабина(&i) && !ПровереноПростымДелением(&i)) {
			
			printValue( &i, fout);
		}
		else {

			//pass	
		}
	}
}


void run(long long *start, long long *finish, long long *step, FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS) {
	double koef = 1 + 1 / (double)THREADS_COUNT;
	int j = 0;

	for (long long i = *start; i < *finish; ) {

		for (j = 0; j < THREADS_COUNT && i <= *finish; j++) {

			if (COMPLETED_THREADS[j] = true) {
				COMPLETED_THREADS[j] = false;
				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES, i, &step, j] {threadFunction(i + 1, i + *step, FOUT_FILES[j]);  COMPLETED_THREADS[j] = true; });
				THREADS[j].detach();

				//cout << "Запущен: " << j << " на промежутке [" << (i+1) << " - " << (i + step) <<"]"<< endl;
				i += *step;

				if (*step > 10000000) {
					*step = (*step) / (koef);
				}
			}
		}
	}
}

void waitEnding( atomic<bool> *COMPLETED_THREADS) {

	for (int j = 0; j < THREADS_COUNT; j++) {

		while (!COMPLETED_THREADS[j]) {
			this_thread::sleep_for(1ms);
		}
	}
}

void initFiles( FILE **FOUT_FILES) {

	char tmp[20];

	for (int j = 0; j < THREADS_COUNT; j++) {

		string name = std::to_string(j) + ".txt";
		strcpy(tmp, name.c_str());

		FOUT_FILES[j] = fopen(tmp, "w");
	}

	FOUT_FILES[THREADS_COUNT] = fopen("stats.txt", "w");

}

void closeFiles( FILE **FOUT_FILES ) {

	for (int j = 0; j <= THREADS_COUNT; j++) {
		fclose(FOUT_FILES[j]);
	};
}

int main(){

	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке

	int NUM_CORES = thread::hardware_concurrency();
	THREADS_COUNT = NUM_CORES;

	FILE* FOUT_FILES[MAX_THREADS_COUNT];
	atomic<bool> COMPLETED_THREADS[ MAX_THREADS_COUNT ];

	for (int j = 0; j < THREADS_COUNT; j++) {
		COMPLETED_THREADS[j] = false;
	}
	
	initFiles(FOUT_FILES);

	fprintf(FOUT_FILES[THREADS_COUNT], "Количество ядер %d \n Количество потоков: %d \n", NUM_CORES, THREADS_COUNT);

	long long leftBorder = 3;
	long long rightBorder = pow(10, 6);//12
	long long step = pow(10, 5);//9

	fprintf(FOUT_FILES[THREADS_COUNT], "%d to %d by %d\n", leftBorder, rightBorder, step);
	
	clock_t start_at = clock();

	run(&leftBorder,&rightBorder,&step,FOUT_FILES,COMPLETED_THREADS);

	waitEnding(COMPLETED_THREADS);

	clock_t finish_at = clock();

	float diff((float)finish_at - (float)start_at);

	fprintf(FOUT_FILES[THREADS_COUNT], "Time: %d milliseconds\n", diff);

	closeFiles(FOUT_FILES);

	return 0;
}

