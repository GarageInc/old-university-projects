
// на вход  простые числа!
void thread_function_official_pq(uint64_t  leftBorder, uint64_t  rightBorder, uint64_t  maxCount, uint64_t  * simples, mutex *locker, FILE*file, std::vector<uint128_t> *spps )
{
	int print_count = 3;
	uint128_t * print_values = new uint128_t[ print_count ];

	uint128_t q;
	uint128_t n;

	uint128_t u;
	uint128_t *ords = new uint128_t[ A_LENGTH  + 1 ];

	int j;
	int koef;

	cpp_int d;
	cpp_int sqrt_d;

	uint128_t k = 1;
	cpp_int d1, d2;

	for ( uint64_t i = leftBorder; i < rightBorder && i < maxCount; i++ ) {

		// Получим ord по каждой базе
		for ( j = 0; j < A_LENGTH; j++ ) {
			ords[ j ] = getOrd( simples[ i ], A_uint128_t[ j ] );
		}
		ords[ j ] = 0;

		// получим НОК по всем ord
		u =  getNOK(ords);

		if ( u % 2 != 0 ) {

			koef = 2;
		}
		else {

			koef = 1;
		}

		// распараллеливаем подсчет степеней
		thread t1([ &d1, simples, i ] {
			d1 = pow((cpp_int)2, simples[ i ] - 1) - 1;
		});
		thread t2([ &d2, simples, i ] {
			d2 = pow((cpp_int)3, simples[ i ] - 1) - 1;
		});

		t1.join();
		t2.join();

		d = gcd( d1, d2 );

		sqrt_d = sqrt( d ) + 1;

		for (k = 1, q = 1; q < sqrt_d ; k++) {

			q = koef * k * u + 1;

			if ( d % q == 0 ) {

				n = q * simples[ i ];

				if ( LABS_TEST_MILLER_RABIN_uint128_t( &n, A_LENGTH) ) {

					locker->lock();

					if ( std::find(spps->begin(), spps->end(), n) == spps->end()) {
						spps->push_back(n);

						print_values[0] = n;;
						print_values[1] = simples[i];
						print_values[2] = q;

						printValues(print_values, &print_count, file);
					}

					locker->unlock();
				}// pass
				
			}
			else {
				// pass
			}
		}
	}

	delete[] ords;
}



// Функция, которая проверяет все  числа в промежутке от start до finish с помощью функции threadFunctionRun3 
void official_algorithm_run_pq() {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 1000000;//+00;// корень из 10в16ой, кубический корень = 2.2 * 10в5ой 
	uint64_t  *simples = new uint64_t[ max_count_simples ];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes( simples, &count_simples, 0, max_count_simples, 1 );

	uint64_t  step = 40;// count_simples / 25;

	std::atomic<bool> * is_completed_threads = NULL;
	FILE *f = NULL;

	ThreadsManager 	example;
	fprintf(example.FOUT_FILE, "Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	printf("Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);

	// запускаем!
	example.parallel_by_cores(is_completed_threads, f, count_simples, simples, step, 40, thread_function_official_pq, 2);
}


// на вход  простые числа!
void thread_function_official_pqr(uint64_t  leftBorder, uint64_t  rightBorder, uint64_t  maxCount, uint64_t  * simples, mutex *locker, FILE*file, std::vector<uint128_t> *spps)
{
	int print_count = 4;
	uint128_t * print_values = new uint128_t[ print_count ];

	uint128_t q;
	uint128_t r;
	uint128_t n;

	uint128_t n_tmp;

	uint128_t u;
	uint128_t *ords = new uint128_t[A_LENGTH + 1];

	int j;
	int koef;

	cpp_int d;
	cpp_int sqrt_d;

	uint128_t k = 1;
	cpp_int d1, d2;

	uint64_t index_r = 0;

	for (uint64_t i = leftBorder; i < rightBorder && i < maxCount; i++) {

		// Получим ord по каждой базе
		for (j = 0; j < A_LENGTH; j++) {
			ords[j] = getOrd(simples[i], A_uint128_t[j]);
		}
		ords[j] = 0;

		// получим НОК по всем ord
		u = getNOK(ords);

		if (u % 2 != 0) {

			koef = 2;
		}
		else {

			koef = 1;
		}

		// распараллеливаем подсчет степеней
		thread t1([&d1, simples, i] {
			d1 = pow((cpp_int)2, simples[i] - 1) - 1;
		});
		thread t2([&d2, simples, i] {
			d2 = pow((cpp_int)3, simples[i] - 1) - 1;
		});

		t1.join();
		t2.join();

		d = gcd(d1, d2);

		sqrt_d = sqrt(d) + 1;

		for (k = 1, q = 1; q < sqrt_d; k++) {

			q = koef * k * u + 1;

			if ( d % q == 0 ) {

				n_tmp = q*simples[i];

				// ТЕПЕРЬ НУЖНО ПОДОБРАТЬ 'r'
				// БЕРЕТСЯ 'r' из входных простых чисел, НО, только r > q и r < max( simples ). По методичке.
				index_r = i;

				while ( index_r < maxCount && q >= simples[ i ] ) {
					index_r++;
				}
				
				while ( index_r < maxCount ) {
					r = simples[ i ];

					n = n_tmp * r;

					if ( LABS_TEST_MILLER_RABIN_uint128_t( &n, A_LENGTH) ) {

						locker->lock();

						if (std::find(spps->begin(), spps->end(), n) == spps->end()) {
							spps->push_back(n);
							print_values[0] = n;;
							print_values[1] = simples[i];
							print_values[2] = q;
							print_values[3] = r;

							printValues( print_values, &print_count, file );
						}

						locker->unlock();
					}// pass
					

					index_r++;
				}
			}
			else {
				// pass
			}
		}
	}

	delete[] ords;
	delete[] print_values;
}

void official_algorithm_run_pqr() {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 220000;// кубический корень из 10в16ой = 2.2 * 10в5ой 
	uint64_t  *simples = new uint64_t[max_count_simples];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes(simples, &count_simples, 0, max_count_simples, 1);

	uint64_t  step = 40;// count_simples / 25;

	std::atomic<bool> * is_completed_threads = NULL;
	FILE *f = NULL;

	ThreadsManager 	example;
	fprintf(example.FOUT_FILE, "Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	printf("Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);

	// запускаем!
	example.parallel_by_cores(is_completed_threads, f, count_simples, simples, step, 40, thread_function_official_pqr, 2);
}

