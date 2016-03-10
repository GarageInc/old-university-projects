
//#pragma comment (lib,"mpfr.lib")
//#pragma comment (lib,"mpir.lib")

#include <stdio.h>
#include <conio.h>
#include <string>
#include <time.h>

#include <thread>
#include <chrono>
#include <future>
#include <mutex>

#include <iostream>
#include <fstream>
#include <math.h>

#include <boost/math/special_functions/log1p.hpp>
#include <boost/math/special_functions/pow.hpp>
#include<boost\multiprecision\cpp_int.hpp>
#include<boost\any.hpp>

using namespace boost::multiprecision;
using namespace std;

#include "functions.h"
#include "miller-rabin.h"

#include "ThreadsManager.h"
#include "TimeManager.h"

#include "official_algorithm.h"
#include "exhaustive_search.h"
#include "mult_simples_pq.h"



// Максимальное uint1024_t: 2*179769313486231590772930519078902473361797697894230657273430081157732
//675805500963132708477322407536021120113879871393357658789768814416622492
//847430639474124377767893424865485276302219601246094119453082952085005768
//838150682342462881473913110540827237163350510684586298239947245938479716
//304835356329624224137216

// Максимальное uint128_t: 2*340282366920938463463374607431768211456

// Максимальное uint64_t: 2*18446744073709551616

void some_function() {

	srand(_Xtime_get_ticks());

	std::this_thread::sleep_for(std::chrono::seconds(rand() % 10));
}

int main() {
	
	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке

	// Запускается функция, которая проверяет псевдопростые числа: числа, которые являются произведением двух простых чисел 'p' и 'q', но проходят тест Миллера-Рабина по 	базе 'A'
	// exhaustive_search_run(FOUT_FILES, COMPLETED_THREADS, THREADS_COUNT, THREADS);// полный перебор
	// mult_simples_pq_run(FOUT_FILES, COMPLETED_THREADS, THREADS, THREADS_COUNT); // Почти полный перебор, поиск по произведениям простых чисел
	

	TimeManager example;
	example.run_simples( official_algorithm_run );// Официальный алгоритм, по методичке
	

	printf("\n\nКОНЕЦ");

	getch();
	return 0;
}

