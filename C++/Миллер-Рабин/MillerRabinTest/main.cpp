
//#pragma comment (lib,"mpfr.lib")
//#pragma comment (lib,"mpir.lib")

#include <iostream>
#include <stdio.h>
#include <fstream>
#include <conio.h>
#include <string>
#include <time.h>
#include <thread>
#include <chrono>
#include <future>
#include <mutex>


#include<boost\multiprecision\cpp_int.hpp>
#include<boost\any.hpp>
using namespace boost::multiprecision;

//#include "mpreal.h"
#include "functions.h"
#include "miller-rabin.h"
#include"primegen.h"
#include"example1.h"
#include"example2.h"

// Максимальное uint1024_t: 2*179769313486231590772930519078902473361797697894230657273430081157732
//675805500963132708477322407536021120113879871393357658789768814416622492
//847430639474124377767893424865485276302219601246094119453082952085005768
//838150682342462881473913110540827237163350510684586298239947245938479716
//304835356329624224137216

// Максимальное uint128_t: 2*340282366920938463463374607431768211456

// Максимальное uint64_t: 2*18446744073709551616

using namespace std;



// Используемое количество потоков. Будет равно числу ядер в компьютере
int THREADS_COUNT = 0;

// Максимальное количество допустимых активных потоков
const int MAX_THREADS_COUNT = 100;

// Потоки
thread THREADS[MAX_THREADS_COUNT];


int main() {
	
	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке
	// const int digits = 50;// 4096;
	// mpreal::set_default_prec(mpfr::digits2bits(digits));
	// mpf_set_default_prec(4096);

	// Количество ядер
	int NUM_CORES = thread::hardware_concurrency();
	// Количество потоков
	THREADS_COUNT = NUM_CORES;
	
	// Выводные файлы(в которые выводится результат)
	FILE* FOUT_FILES[MAX_THREADS_COUNT];
	// Инициализируем файлы
	initFiles(FOUT_FILES, THREADS_COUNT);
	
	// Массив булевых значений - для контроля работы потоков(освобождения и заполнения)
	atomic<bool> COMPLETED_THREADS[MAX_THREADS_COUNT];

	// Говорим, что потоки ещё свободны
	for (int j = 0; j < THREADS_COUNT; j++) {
		COMPLETED_THREADS[j] = true;
	}

	fprintf(FOUT_FILES[THREADS_COUNT], "Количество ядер %d\nКоличество потоков: %d \n", NUM_CORES, THREADS_COUNT);
	
	// Начало работы
	clock_t start_at = clock();

	// Запускается функция, которая проверяет псевдопростые числа: числа, которые являются произведением двух простых чисел 'p' и 'q', но проходят тест Миллера-Рабина по 	базе 'A'
	//run1(FOUT_FILES,COMPLETED_THREADS, THREADS_COUNT, THREADS);
	run2(FOUT_FILES, COMPLETED_THREADS, THREADS, THREADS_COUNT);
	
	// Конец работы
	clock_t finish_at = clock();

	// Выводим результат
	float diff((float)finish_at - (float)start_at);

	fprintf(FOUT_FILES[THREADS_COUNT], "Time: %f milliseconds\n", diff);

	// Закрываем файлы
	closeFiles(FOUT_FILES, THREADS_COUNT);

	printf("\n\nКОНЕЦ");

	getch();
	return 0;
}

