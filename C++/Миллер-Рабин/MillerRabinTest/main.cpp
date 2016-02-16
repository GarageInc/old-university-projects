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

// База для теста Миллера-Рабина
int A[] = { 2,3,5 };

// Используемое количество потоков. Равно числу ядер в компьютере
int THREADS_COUNT = 0;

// Максимальное количество допустимых активных потоков
const int MAX_THREADS_COUNT = 100;

// Потоки
thread THREADS[MAX_THREADS_COUNT];


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

	x = powmod(*a, t, m);

	if (x == 1 || x == *m - 1)
		return true;

	// цикл B: s-1 раз
	for (int j = 0; j<s - 1; j++) {
		x = powmod(x, 2, m);

		if (x == 1)
			return false;

		if (x == *m - 1)
			return true;
	}

	return false;
}



// Функция проверки, использует тест Миллера-Рабина
bool ПровереноТестомМиллераРабина(long long *number)
{
	for (int i = 0; i < 3; i++)
	{
		// Запускаем тест
		if (!test_Miller_Rabin(number, &A[i]))
		{
			return false;
		}
	}
	return true;
}

// Проверка на деление числа на все его составные части( от 2 до корня от этого числа).
// Если делится - то точно простое число. НО! Не работает на четных числах, на вход подаются только нечетные числа с шагом 2: 1,3,5,7,9 и т.д.
bool ПровереноМодифицированнымПростымДелением(long long *number) {
	long long s = sqrt(*number) + 1;

	// нет смысла рассматривать четные числа, т.к. они не делятся на два
	for (long long i = 3; i <= s; i += 2) {
		if ((*number) % i == 0) {
			return false;
		}
	}

	return true;
}

// Вывод значения в файл
void printValue(long long *i, FILE *fout) {

	fprintf(fout, "%lld \n", *i);
}

// Собственно функции, которые проверяют промежуток чисел между start и finish. С выводом в файл

// Функция №1: если число проходит проверку на тест Миллера-Рабина, но не является простым(не ПровереноМодифицированнымПростымДелением) - то выводим его в файл
void threadFunctionRun1(long long start, long long finish, FILE *fout)//atomic<bool>& ab)
{
	if (start % 2 == 0) start++;

	for (long long i = start; i < finish; i++) {

		if (ПровереноТестомМиллераРабина(&i) && !ПровереноМодифицированнымПростымДелением(&i)) {

			printValue(&i, fout);
		}
		else {

			//pass	
		}
	}
}

// Функция №2 - проверяем произведения всех простых чисел из поданного массива simples.
// например, массив содержит все простые числа от 3 до 1млн - но функция обрабатывает определенные промежуток между leftBorder и rightBorder(т.к. в потоке) - 
// и выводит прошедшие проверку тестом Миллера-Рабина числа в файл. Ведь они составные, а проходят проверку!
void threadFunctionRun2(long long leftBorder, long long rightBorder, long long maxCount, long long * simples, FILE *fout)//atomic<bool>& ab)
{
	long long multResult = 0;
	long long j = 0;
	long long p = 0;
	long long q = 0;

	for (long long i = leftBorder; i < rightBorder && i < maxCount; i++) {

		p = simples[i];

		if (p > 0) {

			for (j = leftBorder; j < maxCount; j++) {
				q = simples[j];

				if (q > 0) {
					multResult = p*q;

					if (ПровереноТестомМиллераРабина(&multResult)) {
						printValue(&multResult, fout);
						simples[j] = -1;
					}
				}				
			}
		}
		
	}
}

// Функция, которая проверяет ВСЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1 
void run1(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS) {
	// Переменные границ и шага, для работы потоков
	long long start = 3;
	long long finish = pow(10, 12);//12 - рассматриваем верхнюю границу в  1трлн чисел
	long long step = pow(10, 9);//9

	fprintf(FOUT_FILES[THREADS_COUNT], "%lld to %lld by %lld\n", start, finish, step);

	double koef = 1 + 1 / (double)THREADS_COUNT;
	int j = 0;

	for (long long i = start; i < finish; ) {

		for (j = 0; j < THREADS_COUNT && i <= finish; j++) {

			if (COMPLETED_THREADS[j] == true) {

				COMPLETED_THREADS[j] = false;
				fflush(FOUT_FILES[j]);

				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES, i, &step, j] {threadFunctionRun1(i + 1, i + step, FOUT_FILES[j]);  COMPLETED_THREADS[j]

					= true; });
				THREADS[j].detach();

				//cout << "Запущен: " << j << " на промежутке [" << (i+1) << " - " << (i + step) <<"]"<< endl;
				i += step;

				if (step > 10000000) {
					step = (step) / (koef);
				}
			}
		}
	}
}

// Заполняет входной массив simples всеми простыми числами между start и finish - границами. Функция явно возвращает количество простых чисел и неявно - все найденные простые числа в массиве simples
long long getCountSimples(long long start, long long finish, long long *simples) {
	long long index = 0;

	for (long long j = start; j < finish; j += 2) {
		if (j % 2 != 0) {

			if (ПровереноМодифицированнымПростымДелением(&j)) {
				simples[index] = j;
				index = index + 1;
			}
		}
	}

	return index;
}

// Функция, которая проверяет ВСЕ ПРОСТЫЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1
void run2(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS) {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	long long max_count_simples = 100000; // 10000000 => 1400156 простых чисел
	long long *simples = new long long[max_count_simples];

	// Получим количество простых чисел
	long long count_simples = getCountSimples(3, max_count_simples, simples);
	long long step = count_simples / 20;

	for (int i = 0; i < count_simples; i++) {
		long long a = simples[i];
	}

	double koef = 1 + 1 / (double)(THREADS_COUNT * 3);
	int j = 0;

	printf("Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	fprintf(FOUT_FILES[THREADS_COUNT], "Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);

	for (long long i = 0; i < count_simples; ) {

		// Цикл просматривания потоков. Если поток освободился - то загружаем его работой по рассмотрению нового промежутка
		for (j = 0; j < THREADS_COUNT && i < count_simples; j++) {

			if (COMPLETED_THREADS[j] == true) {

				if (i + step > count_simples) {
					step = count_simples - i;
				}

				COMPLETED_THREADS[j] = false;
				fflush(FOUT_FILES[j]);

				if (THREADS[j].joinable()) {
					THREADS[j].join();
				}

				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES, count_simples, &simples, i, step, j] {
					threadFunctionRun2(i, i + step, count_simples,simples, FOUT_FILES[j]); 
					COMPLETED_THREADS[j] = true; 
				});

				printf("Запущен %d поток на промежутке [%lld - %lld)\n", j, i, i + step);
				i += step;

				// Т.к. подсчет занимает тем большее время, чем больше рассматриваемые числа - уменьшаем переменную step, чтобы и другие потоки смогли "прийти 			на помощь"
				if (step > 3000) {
					step = (step) / (koef);
				}
			}
			else {

			}
		}
	}
}

// Функция ожидания окончания работы потоков
void waitEnding(atomic<bool> *COMPLETED_THREADS) {

	printf("\n\nОжидание завершения ещё работающих потоков");
	for (int j = 0; j < THREADS_COUNT; j++) {

		//while (!COMPLETED_THREADS[j]) {
		//this_thread::sleep_for(1ms);
		//}
		
		THREADS[j].join();
		printf("\nЗавершен поток %d", j);
	}
}

// Инициализируем файлы для вывода информации. Каждое имя файла - равно номеру потока. Чтобы не создавать конкурентных ресурсов.
// Более правильно было бы использовать один файл для всех потоков, но работа с мьютексами отнимает очень много системных-временных ресурсов. Главное - скорость.
// Обработку данных можно оставить для другой программы.
void initFiles(FILE **FOUT_FILES) {

	char tmp[20];

	for (int j = 0; j < THREADS_COUNT; j++) {

		string name = std::to_string(j) + ".txt";
		strcpy(tmp, name.c_str());

		FOUT_FILES[j] = fopen(tmp, "w");
	}

	FOUT_FILES[THREADS_COUNT] = fopen("stats.txt", "w");
}

// Закрытие файлов, вызывает flush для выгрузки в них невыгруженного вывода. Если он есть, конечно.
void closeFiles(FILE **FOUT_FILES) {

	for (int j = 0; j <= THREADS_COUNT; j++) {
		fclose(FOUT_FILES[j]);
	};
}

int main() {

	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке

								 // Количество ядер
	int NUM_CORES = thread::hardware_concurrency();
	// Количество потоков
	THREADS_COUNT =  NUM_CORES;

	// Выводные файлы(в которые выводится результат)
	FILE* FOUT_FILES[MAX_THREADS_COUNT];

	// Массив булевых значений - для контроля работы потоков(освобождения и заполнения)
	atomic<bool> COMPLETED_THREADS[MAX_THREADS_COUNT];

	// Говорим, что потоки ещё свободны
	for (int j = 0; j < THREADS_COUNT; j++) {
		COMPLETED_THREADS[j] = true;
	}

	// Инициализируем файлы
	initFiles(FOUT_FILES);

	fprintf(FOUT_FILES[THREADS_COUNT], "Количество ядер %d\nКоличество потоков: %d \n", NUM_CORES, THREADS_COUNT);


	// Начало работы
	clock_t start_at = clock();

	//run1(FOUT_FILES,COMPLETED_THREADS);
	// Запускается функция, которая проверяет псевдопростые числа: числа, которые являются произведением двух простых чисел 'p' и 'q', но проходят тест Миллера-Рабина по 	базе 'A'
		run2(FOUT_FILES, COMPLETED_THREADS);

	// Ждем окончания работы всех потоков
	waitEnding(COMPLETED_THREADS);

	// Конец работы
	clock_t finish_at = clock();

	// Выводим результат
	float diff((float)finish_at - (float)start_at);

	fprintf(FOUT_FILES[THREADS_COUNT], "Time: %f milliseconds\n", diff);

	// Закрываем файлы
	closeFiles(FOUT_FILES);

	printf("\n\nКОНЕЦ");

	getch();
	return 0;
}

