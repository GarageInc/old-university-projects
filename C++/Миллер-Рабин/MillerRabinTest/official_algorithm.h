
// на вход  простые числа!

void thread_function_official(uint64_t  leftBorder, uint64_t  rightBorder, uint64_t  maxCount, uint64_t  * simples, mutex *locker, FILE*file )
{
	uint128_t q;
	uint128_t n;

	uint128_t u;
	uint128_t ords[A_LENGTH];

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

					locker->lock();
					printValues(&n, simples[i], &q, file);
					locker->unlock();
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
void official_algorithm_run() {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 1000000;
	uint64_t  *simples = new uint64_t[max_count_simples];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes(simples, &count_simples, 0, max_count_simples, 1);

	uint64_t  step = count_simples / 30;

	//fprintf(FOUT_FILES[THREADS_COUNT], "Промежуток: до %lld, простых чисел всего: %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	std::atomic<bool> * is_completed_threads = NULL;
	FILE *f = NULL;

	ThreadsManager 	example;
	fprintf(example.FOUT_FILE, "Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	example.parallel(is_completed_threads, f, count_simples, simples, step, 3000, thread_function_official);
}
