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

#include"primegen.h"
#include"example1.h"
#include"example2.h"

using namespace std;


// Используемое количество потоков. Будет равно числу ядер в компьютере
int THREADS_COUNT = 0;

// Максимальное количество допустимых активных потоков
const int MAX_THREADS_COUNT = 100;

// Потоки
thread THREADS[MAX_THREADS_COUNT];

// Функция ожидания окончания работы потоков
void waitEnding(atomic<bool> *COMPLETED_THREADS) {

	printf("\n\nОжидание завершения ещё работающих потоков");
	for (int j = 0; j < THREADS_COUNT; j++) {
			
		if (THREADS[j].joinable()) {

			THREADS[j].join();
			printf("\nЗавершен поток %d", j);
		}
		else {

			printf("\nЗавершен поток %d", j);
		}
	}
}


int main() {
	
	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке
		 
	// Количество ядер
	int NUM_CORES = thread::hardware_concurrency();
	// Количество потоков
	THREADS_COUNT =  NUM_CORES;
	
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

	//run1(FOUT_FILES,COMPLETED_THREADS);
	// Запускается функция, которая проверяет псевдопростые числа: числа, которые являются произведением двух простых чисел 'p' и 'q', но проходят тест Миллера-Рабина по 	базе 'A'
	run2(FOUT_FILES, COMPLETED_THREADS, THREADS, THREADS_COUNT);

	// Ждем окончания работы всех потоков
	waitEnding(COMPLETED_THREADS);

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

