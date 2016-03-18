
// Лаб.работа: числа 10в16ой, но т.к.рассматриваются только нечетные числа, то количество делителей будет не более чем log(3, 10в16ой)
const int MAX_FACTORS_COUNT = 33;
const int MAX_BORDER_C = 10000000000000000;
const int MAX_BORDER_C_SQRT = 100000000;

#include <stdio.h>
#include <conio.h>
#include <string>


#include <iostream>
#include <fstream>
#include <math.h>

#include <boost/math/special_functions/log1p.hpp>
#include <boost/math/special_functions/pow.hpp>
#include<boost\multiprecision\cpp_int.hpp>
#include<boost\any.hpp>
#include"primegen.h"

using namespace boost::multiprecision;
using namespace std;

#include "functions.h"
#include "miller-rabin.h"

#include "ThreadsManager.h"
#include "TimeManager.h"

#include "official_algorithm_tuples.h"
#include "official_algorithm_pq_pqr.h"
#include "exhaustive_search.h"
#include "mult_simples_pq.h"



// Максимальное uint1024_t: 2*179769313486231590772930519078902473361797697894230657273430081157732
//675805500963132708477322407536021120113879871393357658789768814416622492
//847430639474124377767893424865485276302219601246094119453082952085005768
//838150682342462881473913110540827237163350510684586298239947245938479716
//304835356329624224137216

// Максимальное uint128_t: 2*340282366920938463463374607431768211456

// Максимальное uint64_t: 2*18446744073709551616


int main() {
	
	setlocale(LC_ALL, "Russian");// Чтобы выводился текст на русском языке
		
	TimeManager example;// для запуска - передаём запускаемую функцию

	example.run(official_algorithm_run_pq);// Официальный алгоритм, по методичке, проверяет pq - числа
	// example.run( official_algorithm_run_pqr );// Официальный алгоритм, по методичке, проверяет pqr, r>q - числа
	// example.run( exhaustive_search_run );// Полный перебор всех чисел до указанного предела
	// example.run( mult_simples_pq_run ); // Почти полный перебор, поиск по произведениям простых чисел p и q

	// official_algorithm_run_simpletuples();// Перебор кортежей простых чисел, так же распараллелено
	
	printf("\n\nКОНЕЦ");

	getch();
	return 0;
}

