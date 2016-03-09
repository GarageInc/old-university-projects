
// на вход  простые числа!
// Функция №1: если число проходит проверку на тест Миллера-Рабина, но не является простым - то выводим его в файл
void threadFunctionRun3(uint64_t  leftBorder, uint64_t  rightBorder, uint64_t  maxCount, uint64_t  * simples, FILE *fout )//atomic<bool>& ab) 
{
	uint128_t q;
	uint128_t n;

	uint128_t u;
	uint128_t ords[A_LENGTH];// = new uint128_t[];

	int j;
	int koef;

	cpp_int d;
	cpp_int sqrt_d;

	uint128_t k = 1;

	for (uint64_t i = leftBorder; i < rightBorder && i < maxCount; i++ ) {

		// Получим ord по каждой базе
		for ( j = 0; j < A_LENGTH; j++ ) {
			ords[j] = getOrd( simples[ i ], A_uint128_t[ j ] );
		}
		ords[j] = 0;

		// получим НОК по всем ord
		u = getNOK(ords);

		if ( u % 2 != 0 ) {

			koef = 2;
		}
		else {

			koef = 1;
		}

		d = gcd( pow((cpp_int)2, simples[i] - 1) - 1, pow((cpp_int)3, simples[i] - 1) - 1 );

		sqrt_d = sqrt( d ) + 1;

		for (k = 1, q = 1; q < sqrt_d ; k++) {

			q = koef * k * u + 1;

			if ( d % q == 0 ) {

				n = q * simples[ i ];

				if ( LABS_TEST_MILLER_RABIN_uint128_t( &n, 2 ) ) {

					cout << n << endl;
					printValues( &n, simples[i], &q, fout );
				}
				else {
					// pass
				}
			}
			else {
				// pass
			}
		}

	}
}

// Функция, которая проверяет все  числа в промежутке от start до finish с помощью функции threadFunctionRun3 
void official_algorithm_run(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, int THREADS_COUNT, thread * THREADS) {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 1000000;
	uint64_t  *simples = new uint64_t[max_count_simples];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes(simples, &count_simples, 0, max_count_simples, THREADS_COUNT);

	uint64_t  step = count_simples / 30;

	double koef = 1 + 1 / (double)(THREADS_COUNT * 1);
	int j = 0;

	printf("Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	

	fprintf(FOUT_FILES[THREADS_COUNT], "Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	THREADS_COUNT = 1;
	for (uint64_t i = 1; i < count_simples; ) {

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
				THREADS[j] = thread([&COMPLETED_THREADS, &FOUT_FILES, &simples, count_simples, index_i, step, index_j] {
					printf( "Запущен %d поток на промежутке [%lld - %lld)\n", index_j, index_i, index_i + step );

					threadFunctionRun3(index_i, index_i + step, count_simples, simples, FOUT_FILES[index_j]);

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
