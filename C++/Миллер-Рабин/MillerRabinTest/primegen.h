// √енератор простых чисел в заданном диапазоне до 2^64
#include <time.h>

#ifdef WIN32
#define strtoull(X,Y,Z) _strtoui64(X,Y,Z)
#pragma warning(disable: 4996) // fopen()
#endif

#include "next_prime64.h" 

//***************************************************************************************
// ћногопоточный расчет
#include <thread>
#include <mutex>  
#include <vector>

// состо€ни€ обработки
#define ST_INIT 1 // готов к обсчету
#define ST_CALC 2 // обсчитрываетс€
#define ST_END  3 // готово
#define ST_ERR  4 // были ошибки

// задание дл€ обсчета интервала
typedef struct {
	uint64_t from; // начало интервала
	uint64_t to; // конец интервала
	int cnt; // кол-во простых
	int state; // состо€ние
	uint64_t *block_simples;
} interval_t;

std::vector<interval_t> res; // интервалы для обсчета и результаты
std::mutex res_change; // синхронизация доступа к res

// поток расчета
void prime_thread(int block)
{
	prime_bitmap_t pb = { 0 }; // биткарта текущего потока
	bool calc_up = true; // считаем последовательно вверх

	while (block >= 0) {
		// обсчет res[block]
		interval_t* v = &res[block];

		uint64_t x = next_prime(v->from, &pb);

		while (x <= v->to) {
			printf("%lld\n", x);
			v->block_simples[v->cnt] = x;
			v->cnt++;

			x = next_prime(x, &pb);
		}

		// выбор очередного блока
		res_change.lock();
		v->state = (x == 0) ? ST_ERR : ST_END;

		if (calc_up) {
			block++; //следующий в своем интервале

			if (block != res.size() && res[block].state == ST_INIT) {
				res[block].state = ST_CALC;
			}
			else {
				calc_up = false;
			}
		}
		else {
			// pass
		}

		if (!calc_up) { // поиск необсчитанных в чужих интервалах
			for (block = (int)res.size() - 1; block >= 0; block--) {
				if (res[block].state == ST_INIT) {
					res[block].state = ST_CALC;
					break;
				}
			}
		}
		else {
			// pass
		}
		res_change.unlock();


	}
}

// поток инициализации кэша next_prime()
void prime_init(uint64_t max)
{
	next_prime(max);
}

// запуск потоков расчета и сбор результатов
void prime_calc_mt(uint64_t * simples, uint64_t * count_simples, uint64_t from, uint64_t to, int width, int thread_cnt)
{
	if (from & 1) 
		from--; // делаем четным чтобы не пропустить простое from
	if (from <= 2) 
		from = 1;

	std::thread th_init(prime_init, to);

	// формирование заданий блоками кратными PRIME_BUF (окно решета)
	for (uint64_t i = from; i < to; i += PRIME_BUF - (i & (PRIME_BUF - 1))) {
		interval_t v = { 0 };

		v.from = i;
		v.to = i + PRIME_BUF - (i & (PRIME_BUF - 1)) - 1;

		if (v.to > to)
			v.to = to;

		v.block_simples = new uint64_t[v.to - v.from];

		v.state = ST_INIT;

		res.push_back(v);
	}

	// инициализаци€ кэша
	th_init.join();

	// запуск потоков расчета
	std::vector<std::thread> th;

	for (int i = 0; i < thread_cnt; i++) {
		int first = (int)res.size() * i / thread_cnt; // номер первого блока дл€ потока
		
		res_change.lock();
		res[ first ].state = ST_CALC;
		res_change.unlock();
		
		th.push_back(std::thread(prime_thread, first));
	}
	
	// oжидание завершени€ потоков
	for (int i = 0; i < thread_cnt; i++)
		th[i].join();
	
	// вывод результатов
	(*count_simples) = 0;

	for (int i = 0; i != res.size(); i++) {
		
		if (res[i].state != ST_END) {
			(*count_simples) = -1;
			break;
		}

		(*count_simples) += res[i].cnt;
	}

	next_prime(0); // освобождение пам€ти
}

//***************************************************************************************
// расчет в одном потоке, возвращает количество найденых простых
void prime_calc(uint64_t * simples, uint64_t * count_simples, uint64_t from, uint64_t to, int width)
{
	if (from & 1) 
		from--; // делаем четным чтобы не пропустить простое from
	if (from <= 2) 
		from = 1;

	*count_simples = 0;
	
	uint64_t x = next_prime(from);

	while (x && x < to) {

		simples[*count_simples] = x;
		(*count_simples)++;

		x = next_prime(x);
	}

	next_prime(0); // освобождение пам€ти

	if (x == 0) 
		*count_simples = -1; // ошибка в next_prime()
}


//****************************************************************************************
int getPrimes(uint64_t * simples, uint64_t * count_simples, uint64_t from = 0, uint64_t to = 0, int th_cnt = 0)
{
	const char* file = NULL;
	int width = 0;

	if (to <= from) {

		printf("\nPrime generator to 2^64\n\nprimegen [from] to [-t:threads] [-f:filename [-w:width]]\n\nfrom - start interval\nto - end interval\nthreads - threads count\nfilename - file to output primes\nwidth - minimum simbols at line\n");
		printf("\nExample:\nprimegen 10000 -t:4\nprimegen 5000 -f:result.txt\nprimegen 5000 10000 -f:result.txt -w:80\n");
	}
	else {

		printf("Подсчет простых чисел на интервале: %llu..%llu\n", from, to);
		int cnt = 0;

		//if (th_cnt > 1) {
		//	prime_calc_mt(simples, count_simples, from, to, width, th_cnt);
		//}
		//else {
			prime_calc(simples, count_simples, from, to, width);
		//}
	}

	return 0;
}