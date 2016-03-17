#include "TimeManager.h"

TimeManager::TimeManager(){
}


TimeManager::~TimeManager(){
}

void TimeManager::run(callback func)
{
	// Начало работы
	clock_t start_at = clock();

	func();

	// Конец работы
	clock_t finish_at = clock();

	// Выводим результат
	float diff = (float)finish_at - (float)start_at;

	printf("Time: %f milliseconds\n", diff);
}
