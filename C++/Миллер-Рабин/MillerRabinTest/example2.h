
#include <thread>

// Функция №2 - проверяем произведения всех простых чисел из поданного массива simples.
// например, массив содержит все простые числа от 3 до 1млн - но функция обрабатывает определенные промежуток между leftBorder и rightBorder(т.к. в потоке) - 
// и выводит прошедшие проверку тестом Миллера-Рабина числа в файл. Ведь они составные, а проходят проверку!
void threadFunctionRun2(uint64_t  leftBorder, uint64_t  rightBorder, uint64_t  maxCount, uint64_t  * simples, FILE *fout)
{
	uint64_t  multResult = 0;
	uint64_t  j = 0;
	uint64_t  p = 0;
	uint64_t  q = 0;

	for (uint64_t  i = leftBorder; i < rightBorder && i < maxCount; i++) {

		p = simples[i];

		for (j = leftBorder; j < maxCount; j++) {
			q = simples[j];

			multResult = p*q;

			if (ПровереноТестомМиллераРабина(&multResult)) {
				printValue(&multResult, fout);
			}
		}
	}
}


// Функция, которая проверяет ВСЕ ПРОСТЫЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1
void run2(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, thread * THREADS, int THREADS_COUNT) {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 100; // 10000000 => 1400156 простых чисел
	uint64_t  *simples = new uint64_t [max_count_simples];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes(simples, &count_simples, 1, max_count_simples, THREADS_COUNT);
	
	uint64_t  step = count_simples / 20;
	
	double koef = 1 + 1 / (double)(THREADS_COUNT * 3);
	int j = 0;

	printf("Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	fprintf(FOUT_FILES[THREADS_COUNT], "Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);

	for (uint64_t  i = 0; i < count_simples; ) {

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
					threadFunctionRun2(i, i + step, count_simples, simples, FOUT_FILES[j]);
					COMPLETED_THREADS[j] = true;
				});

				printf("Запущен %d поток на промежутке [%lld - %lld)\n", j, i, i + step);
				i += step;

				// Т.к. подсчет занимает тем большее время, чем больше рассматриваемые числа - уменьшаем переменную step, чтобы и другие потоки смогли "прийти на помощь"
				if (step > 3000) {
					step = (step) / (koef);
				}
			}
			else {

			}
		}
	}
}

