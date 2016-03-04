
#include <thread>

// Функция №1: если число проходит проверку на тест Миллера-Рабина, но не является простым(не ПровереноМодифицированнымПростымДелением) - то выводим его в файл
void threadFunctionRun1(uint128_t  start, uint128_t  finish, FILE *fout, int index_j)//atomic<bool>& ab)
{
	if (start % 2 == 0) 
		start = start + 1;

	for (uint128_t  i = start; i < finish; i+=2) {

		if ( LABS_TEST_MILLER_RABIN_uint128_t( &i, A_LENGTH ) ) {

			if( !LABS_TEST_MILLER_RABIN_uint128_t( &i, 9 ) )
				printValue(&i, fout);
		}
		else {

			//pass	
		}
	}
}

// Функция, которая проверяет ВСЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1 
void exhaustive_search_run(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, int THREADS_COUNT, thread * THREADS) {
	// Переменные границ и шага, для работы потоков
	uint64_t  start = 3;
	uint64_t  finish = pow(10, 6);//12 - рассматриваем верхнюю границу в  1трлн чисел
	uint64_t step = pow(10, 5)/2;//9

	fprintf(FOUT_FILES[THREADS_COUNT], "%lld to %lld by %lld\n", start, finish, step);

	double koef = 1 + 1 / ((double)THREADS_COUNT*2);
	int j = 0;

	for (uint64_t  i = start; i < finish; ) {

		for (j = 0; j < THREADS_COUNT && i < finish; j++) {

			if (COMPLETED_THREADS[j] == true) {
				COMPLETED_THREADS[j] = false;

				if (i + step > finish) {
					step = finish - i;
				}

				if (THREADS[j].joinable()) {
					THREADS[j].join();
				}

				uint64_t index_i = i;
				int index_j = j;

				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES, index_i, step, index_j] {
					printf("Запущен %d поток на промежутке [%lld - %lld)\n", index_j, index_i, index_i + step);

					threadFunctionRun1(index_i, index_i + step, FOUT_FILES[index_j], index_j);

					printf(" => Завершил работу %d\n", index_j);
					fflush(FOUT_FILES[index_j]);
					COMPLETED_THREADS[index_j] = true;
				});

				i += step;
				if (step > 1000000) {
					step = (step) / (koef);
				}
			}
		}
	}


	printf("\nОжидание завершения ещё работающих потоков\n");
	for (int j = 0; j < THREADS_COUNT; j++) {

		if (THREADS[j].joinable()) {

			THREADS[j].join();
		}

		printf("Завершен поток %d\n", j);
	}
}
