
#include <thread>

// Функция №2 - проверяем произведения всех простых чисел из поданного массива simples.
// например, массив содержит все простые числа от 3 до 1млн - но функция обрабатывает определенные промежуток между leftBorder и rightBorder(т.к. в потоке) - 
// и выводит прошедшие проверку тестом Миллера-Рабина числа в файл. Ведь они составные, а проходят проверку!
void threadFunctionRun2(uint64_t  leftBorder, uint64_t  rightBorder, uint64_t  maxCount, uint64_t  * simples, FILE *fout, int index_j)
{
	uint128_t  multResult = 0;
	uint64_t  j = 0;

	for (uint64_t  i = leftBorder; i < rightBorder && i < maxCount; i++) {
		
		for (j = leftBorder; j < maxCount; j++) {
			multResult = simples[i] * simples[j];// делаем составное число

			if (TEST_MILLER_RABIN_uint128_t( &multResult, &index_j) ) {

				// Если составное число прошло проверку - оно выводится в файл
				printValue( &multResult, fout );
			}
		}
	}
}


// Функция, которая проверяет ВСЕ ПРОСТЫЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1
void mult_simples_pq_run(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, thread * THREADS, int THREADS_COUNT) {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 100000;
	uint64_t  *simples = new uint64_t[max_count_simples];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes(simples, &count_simples, 0, max_count_simples, THREADS_COUNT);
	
	uint64_t  step = count_simples / 30;

	double koef = 1 + 1 / (double)(THREADS_COUNT * 1);
	int j = 0;

	printf("Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	fprintf(FOUT_FILES[THREADS_COUNT], "Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);

	for (uint64_t  i = 1; i < count_simples; ) {

		// Цикл просматривания потоков. Если поток освободился - то загружаем его работой по рассмотрению нового промежутка
		for (j = 0; j < THREADS_COUNT && i < count_simples; j++) {

			if (COMPLETED_THREADS[j] == true) {
				COMPLETED_THREADS[j] = false;

				if (i + step > count_simples) {
					step = count_simples - i;
				}

				if (THREADS[j].joinable()) {
					THREADS[j].join();
				}

				uint64_t index_i = i;
				int index_j = j;
				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES,  &simples, count_simples, index_i, step, index_j] {
					printf("Запущен %d поток на промежутке [%lld - %lld)\n", index_j, index_i, index_i + step, index_j);

					threadFunctionRun2(index_i, index_i + step, count_simples, simples, FOUT_FILES[index_j], index_j );
					
					printf(" => Завершил работу %d\n", index_j);
					fflush(FOUT_FILES[index_j]);
					COMPLETED_THREADS[index_j] = true;
				});

				i += step;

				// Т.к. подсчет занимает тем большее время, чем больше рассматриваемые числа - уменьшаем переменную step, чтобы и другие потоки смогли "прийти на помощь"
				if (step > 3000) {
					step = (uint16_t)(step) / (koef);
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

