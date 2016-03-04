
#include <boost/math/special_functions/pow.hpp>
#include <boost/multiprecision/cpp_dec_float.hpp>

cpp_int customPow(int base, uint64_t stepen ) {
	/*cpp_int saved = base;
	cpp_int new_base = base;

	while ( stepen != 0 ) {
		new_base *= saved;
		stepen--;
	}
	*/
	return boost::multiprecision::pow((cpp_int)base, stepen);
}


// ������� �1: ���� ����� �������� �������� �� ���� �������-������, �� �� �������� ������� - �� ������� ��� � ����
void threadFunctionRun3(uint64_t  start, uint64_t  finish, int index_j)//atomic<bool>& ab) FILE *fout, 
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
	cpp_int sqrt_d;

	uint128_t k = 1;

	for (uint64_t i = start; i < finish; i += 2) {

		// ������� ord �� ������ ����
		length = 0;

		for (j = 0; j < A_LENGTH; j++) {
			ords[j] = ord( &i, A_int[j] );
			length++;
		}

		// ������� ��� �� ���� ord
		u = getNOK(ords, length);

		koef = 1;
		if (u % 2 != 0) {
			koef = 2;
		}
		
		cpp_int q = customPow(2, i - 1) - 1;
		d = gcd(q, customPow(3, i - 1) - 1);
		sqrt_d = sqrt(d) + 1;

		for (k = 1; q < sqrt_d ; k++) {
			q = koef*k*u + 1;

			if (d % q == 0) {
				if (LABS_TEST_MILLER_RABIN_uint64_t(&i, &index_j)) {
					cout << i << endl;
					//printValue(&i, fout);
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

// �������, ������� ��������� ���  ����� � ���������� �� start �� finish � ������� ������� threadFunctionRun3 
void official_algorithm_run(FILE **FOUT_FILES, atomic<bool> *COMPLETED_THREADS, int THREADS_COUNT, thread * THREADS) {

	threadFunctionRun3(1530780, 1530790, 0);

	return;
	// ���������� ������ � ����, ��� ������ �������
	uint64_t  start = 3;
	uint64_t  finish = pow(10, 4);//12 - ������������� ������� ������� �  1���� �����
	uint64_t step = pow(10, 3);//9

	fprintf(FOUT_FILES[THREADS_COUNT], "�� %lld �� %lld � ����� %lld\n", start, finish, step);

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
					printf("������� %d ����� �� ���������� [%lld - %lld)\n", index_j, index_i, index_i + step);

					//threadFunctionRun3(index_i, index_i + step, FOUT_FILES[index_j], index_j);

					printf(" => �������� ������ %d\n", index_j);
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


	printf("\n�������� ���������� ��� ���������� �������\n");
	for (int j = 0; j < THREADS_COUNT; j++) {

		if (THREADS[j].joinable()) {

			THREADS[j].join();
		}

		printf("�������� ����� %d\n", j);
	}
}
