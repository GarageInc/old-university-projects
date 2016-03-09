// CLogReader.cpp: implementation of the CLogReader class.
#include "ThreadsMikhail.h"

// Constructor
ThreadsMikhail::ThreadsMikhail()
{
}

// Деструктор
ThreadsMikhail::~ThreadsMikhail()
{
	delete[] THREADS;
}

void ThreadsMikhail::wait_for_end() {

	printf("\nОжидание последовательного завершения ещё работающих потоков\n");

	for (int j = 0; j < THREADS_COUNT; j++) {

		if ( THREADS[j].joinable() ) {

			THREADS[j].join();
		}

		printf("Завершен поток %d\n", j);
	}
}

void ThreadsMikhail::parallel(int threads_count, callback func) {

	THREADS_COUNT = threads_count;
	THREADS = new std::thread[ threads_count ];
	
	for (int j = 0; j < THREADS_COUNT; j++) {
		
		THREADS[j] = std::thread([func, j] {
			printf("Запущен %d поток\n", j);
			
			func();

			printf(" => Завершил работу %d\n", j);
		});
	}
}

void ThreadsMikhail::parallel_by_cores(callback func, bool max) {

	int num_cores = std::thread::hardware_concurrency();
	
	if ( !max && num_cores != 1 ) {
		num_cores = num_cores - 1;
	}

	this->parallel(num_cores, func);
}