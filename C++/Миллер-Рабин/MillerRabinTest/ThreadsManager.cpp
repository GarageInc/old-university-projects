
#include "ThreadsManager.h"

ThreadsManager::ThreadsManager(){
	FOUT_FILE = fopen("results.txt", "w");
}

ThreadsManager::~ThreadsManager(){
	delete[] THREADS;
	delete[] COMPLETED_THREADS;
	fclose( FOUT_FILE );
}

void ThreadsManager::wait_for_end() {

	printf("\nОжидание последовательного завершения ещё работающих потоков\n");

	for (int j = 0; j < THREADS_COUNT; j++) {

		if ( THREADS[j].joinable() ) {

			THREADS[j].join();
		}

		printf("Завершен поток %d\n", j);
	}
}

void ThreadsManager::parallel_by_cores(std::atomic<bool> *temp_completed_threads, FILE*file_ptr, uint64_t max_count, uint64_t *numbers, int step, int step_low_border,  callback func, int sub_threads_count) {

	STEP = step;
	STEP_LOW_BORDER = step_low_border;

	if (sub_threads_count != 0) {

		THREADS_COUNT = std::thread::hardware_concurrency() / ( sub_threads_count + 1 ) + 1;
	}
	else {

		THREADS_COUNT = std::thread::hardware_concurrency();;
	}
	THREADS = new std::thread[THREADS_COUNT];

	fprintf(FOUT_FILE, "Количество потоков: %d\n", THREADS_COUNT);

	// Массив булевых значений - для контроля работы потоков(освобождения и заполнения)
	COMPLETED_THREADS = new std::atomic<bool>[THREADS_COUNT];
	temp_completed_threads = COMPLETED_THREADS;

	for (int i = 0; i < THREADS_COUNT; i++) {
		temp_completed_threads[i] = true;
	}

	// ссылки на объекты класса
	mutex *locker_ptr = &locker;
	file_ptr = FOUT_FILE;

	double koef = 1 + 1 / (double)(THREADS_COUNT * 1);
	uint64_t j = 0;

	vector<uint128_t> spps{};

	for (uint64_t i = 1; i < max_count; ) {

		// Цикл просматривания потоков. Если поток освободился - то загружаем его работой по рассмотрению нового промежутка
		for (j = 0; j < THREADS_COUNT && i < max_count; j++) {

			if ( temp_completed_threads[j] == true ) {
				temp_completed_threads[j] = false;

				if (i + STEP > max_count) {
					STEP = max_count - i;
				}

				if (THREADS[j].joinable()) {
					THREADS[j].join();
				}
				
				THREADS[j] = std::thread([&temp_completed_threads, &numbers, max_count, i, step, j, locker_ptr, file_ptr, func, &spps] {
					printf("Запущен %lld поток на промежутке [%lld - %lld)\n", j, i, i + step);

					func(i, i + step, max_count, numbers, locker_ptr, file_ptr, &spps);
					
					printf(" => Завершил работу %d\n", j);

					locker_ptr->lock();
					fflush( file_ptr );
					locker_ptr->unlock();

					temp_completed_threads[ j ] = true;
				});

				i += STEP;

				// Т.к. подсчет занимает тем большее время, чем больше рассматриваемые числа - уменьшаем переменную step, чтобы и другие потоки смогли "прийти на помощь"
				if ( STEP > STEP_LOW_BORDER ) {
					STEP = (int)(STEP) / (koef);
				}
			}
		}
	}

	wait_for_end();
}