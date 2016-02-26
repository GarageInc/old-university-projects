
#include <thread>

// Функция №1: если число проходит проверку на тест Миллера-Рабина, но не является простым(не ПровереноМодифицированнымПростымДелением) - то выводим его в файл
void threadFunctionRun1(uint64_t  start, uint64_t  finish, FILE *fout)//atomic<bool>& ab)
{
	if (start % 2 == 0) 
		start++;

	for (uint64_t  i = start; i < finish; i+=2) {

		if (ПровереноТестомМиллераРабина(&i) && !ПровереноМодифицированнымПростымДелением(&i)) {

			printValue(&i, fout);
		}
		else {

			//pass	
		}
	}
}

// Функция, которая проверяет ВСЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1 
void run1(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, int THREADS_COUNT, thread * THREADS) {
	// Переменные границ и шага, для работы потоков
	uint64_t  start = 3;
	uint64_t  finish = pow(10, 6) + 700000;//12 - рассматриваем верхнюю границу в  1трлн чисел
	uint64_t  step = pow(10, 8)/4;//9

	fprintf(FOUT_FILES[THREADS_COUNT], "%lld to %lld by %lld\n", start, finish, step);

	double koef = 1 + 1 / ((double)THREADS_COUNT*2);
	int j = 0;

	for (uint64_t  i = start; i < finish; ) {

		for (j = 0; j < THREADS_COUNT && i < finish; j++) {

			if (COMPLETED_THREADS[j] == true) {
				if (i + step > finish) {
					step = finish - i ;
				}

				COMPLETED_THREADS[j] = false;
				fflush(FOUT_FILES[j]);

				if (THREADS[j].joinable()) {
					THREADS[j].join();
				}

				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES, finish, i, step, j] {
					threadFunctionRun1(i, i + step, FOUT_FILES[j]);
					COMPLETED_THREADS[j] = true;
				});

				printf("Запущен %d поток на промежутке [%lld - %lld)\n", j, i, i + step);
				i += step;

				if (step > 1000000) {
					step = (step) / (koef);
				}
			}
		}
	}
}
