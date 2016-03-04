
#include <boost/math/special_functions/pow.hpp>
#include <boost/multiprecision/cpp_dec_float.hpp>

cpp_int customPow(int base, uint128_t stepen ) {
	cpp_int saved = base;
	cpp_int new_base = base;

	while ( stepen != 0 ) {
		new_base *= saved;
		stepen--;
	}

	return new_base;
}


// Функция №1: если число проходит проверку на тест Миллера-Рабина, но не является простым(не ПровереноМодифицированнымПростымДелением) - то выводим его в файл
void threadFunctionRun3(uint64_t  start, uint64_t  finish, FILE *fout, int index_j)//atomic<bool>& ab)
{
	if ( start % 2 == 0 )
		start = start + 1;

	uint128_t q;
	uint128_t u;
	uint128_t* ords = new uint128_t[A_LENGTH];;
	int length = 0;
	int j = 0;
	int koef = 1;
	cpp_int d;
	uint128_t k = 1;

	for (uint128_t i = start; i < finish; i += 2) {

		// Получим ord по каждой базе
		length = 0;

		for (j = 0; j < A_LENGTH; j++) {
			ords[j] = ord( &i, A_int[j] );
			length++;
		}

		// получим НОК по всем ord
		u = getNOK(ords, length);

		koef = 1;
		if (u % 2 != 0) {
			koef = 2;
		}

		d = gcd(customPow(2, i - 1) - 1, customPow(3, i - 1) - 1);

		for (k = 1; ; k++) {
			q = koef*k*u + 1;

			if (d % q == 0) {
				if (TEST_MILLER_RABIN_uint128_t(&i, &index_j)) {
					printValue(&i, fout);
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

// Функция, которая проверяет ВСЕ числа в промежутке от start до finish с помощью функции threadFunctionRun1 
void official_algorithm_run(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, int THREADS_COUNT, thread * THREADS) {

	uint128_t p = 49;
	IsSimple(&p);

	return;
	// Переменные границ и шага, для работы потоков
	uint64_t  start = 3;
	uint64_t  finish = pow(10, 4);//12 - рассматриваем верхнюю границу в  1трлн чисел
	uint64_t step = pow(10, 3);//9

	fprintf(FOUT_FILES[THREADS_COUNT], "От %lld до %lld с шагом %lld\n", start, finish, step);

	double koef = 1 + 1 / ((double)THREADS_COUNT * 2);
	int j = 0;

	for (uint64_t i = start; i < finish; ) {

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

					threadFunctionRun3(index_i, index_i + step, FOUT_FILES[index_j], index_j);

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
