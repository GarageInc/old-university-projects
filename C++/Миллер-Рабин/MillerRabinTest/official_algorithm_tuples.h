

///////////////////////////////////////////////////////

mutex locker;
FILE *file;
std::vector<uint128_t> spps{};
const int max_threads_count = 5;

// на вход  простые числа!
// проверка кортежа
void check_tuple(uint64_t E, uint64_t  maxCount, uint64_t  * simples, uint64_t last_pi_index, uint64_t*tmp, uint64_t tmp_length)
{
	int count = tmp_length + 2;

	uint128_t * print_values;
	print_values = new uint128_t[count];

	for (int i = 2; i < count; i++) {
		print_values[i] = tmp[i - 2];
	}

	cpp_int d;
	cpp_int d1, d2;

	uint64_t n;

	// распараллеливаем подсчет степеней
	thread t1([&d1, simples, E] {
		d1 = pow((cpp_int)2, E - 1);
	});
	thread t2([&d2, simples, E] {
		d2 = pow((cpp_int)3, E - 1);
	});

	t1.join();
	t2.join();

	d = gcd(d1, d2);

	// Перебираем всевозможные простые p > pk−1, являющиеся делителями d такие, что n = p1 ·p2 ·...·pk ≤ C
	// p - из переданного списка простых чисел
	n = E * simples[last_pi_index];

	while (n <= MAX_BORDER_C && last_pi_index < maxCount) {

		// 5. Проверяем, является ли произведение n СППС(A), проверяя каждое такое n по тесту МР с множеством баз A. 
		// 6. В случае успеха добавляем n к базе данных СППС(A)-чисел. 
		if (LABS_TEST_MILLER_RABIN_uint64_t(&n, 2)) {

			locker.lock();
			if (std::find(spps.begin(), spps.end(), n) == spps.end()) {

				spps.push_back(n);

				print_values[0] = n;
				print_values[0] = simples[last_pi_index];

				printValues(print_values, &count, file);
			}

			locker.unlock();
		}// pass

		last_pi_index++;
		n = E * simples[last_pi_index];
	}

	delete[] print_values;
}


thread THREADS[max_threads_count];
atomic<bool> completed[max_threads_count];

// Функция получения кортежей простых чисел из разных комбинаций, p1<p2<...<pk
// только для отсортированного варианта
void find_tuples(uint64_t index_simples, uint64_t *simples, uint64_t index_tmp, uint64_t *tmp, uint64_t *maxCount, uint64_t last_mult = 1, bool is_incr_tmp = false) {
	
	
	if (index_tmp > 0) {

		if (!is_incr_tmp || last_mult * tmp[index_tmp - 1] >= MAX_BORDER_C) {
			
			// Дальше смысла перебирать нет в этой ветке
			/*
			stringstream ss;
			for (int i = 0; i < index_tmp; i++) {
			ss << tmp[i] << " ";
			}

			printf("\n\nЗА ПРЕДЕЛОМ: %s", ss.str().c_str());
			*/
			return;
		}
		else {

			// запуск по последовательности
			
			// спим, пока какие-то кортежи обрабатываются, но
			// ищем свободный поток
			int completed_index = -1;
			
			while ( completed_index == -1 ) {
				
				for (int i = 0; i < max_threads_count; i++) {
				
					if (completed[i] == true) {
						completed_index = i;
						break;
					}
				}

				this_thread::sleep_for(10ms);
			}

			if ( THREADS[completed_index].joinable() ) {
				THREADS[completed_index].join();
			}
			
			// Как освободились - ищем результат на вывод
			stringstream ss;
			for (int i = 0; i < index_tmp; i++) {
				ss << tmp[i]<<" ";
			}
			
			printf("\n\nОБРАБОКТА КОРТЕЖА: %s", ss.str().c_str());
			
			// скопируем массив и скормим потоку( чтобы он внезапно не изменился внутри потока )
			uint64_t *tmp_tmp = new uint64_t[index_tmp];
			for (int i = 0; i < index_tmp; i++) {
				tmp_tmp[i] = tmp[i];
			}
			
			completed[completed_index] = false;

			THREADS[completed_index] = thread([last_mult, maxCount, &simples, index_simples, tmp_tmp, index_tmp, completed_index] {
				
				printf("\nЗапущен поток %d", completed_index);

				this_thread::sleep_for(1000ms);
				check_tuple(last_mult, *maxCount, simples, index_simples, tmp_tmp, index_tmp);

				printf("\n\n => Завершил работу %d", completed_index);
				
				locker.lock();
				fflush(file);
				locker.unlock();

				completed[completed_index] = true;
			});
			
		}
	}
	else {
		if (last_mult > MAX_BORDER_C) {

			// Дальше смысла перебирать нет, если запоролось на первом значении
			return;
		}// pass
	}

	if (index_simples == *maxCount || index_tmp == *maxCount) {

		return;
	}

	// первая ветка - берем текущий
	tmp[index_tmp] = simples[index_simples];

	if (index_simples + 1 != *maxCount) {

		find_tuples(index_simples + 1, simples, index_tmp + 1, tmp, maxCount, last_mult * simples[index_simples], true);
	}// pass


	 // вторая ветка - пропускаем
	 // tmp[ index_tmp ] = a[ index_a ]; // 
	find_tuples(index_simples + 1, simples, index_tmp, tmp, maxCount, last_mult, false);
}


void official_algorithm_run_simpletuples() {

	// Максимальное количество простых чисел. По умолчанию равно верхней границе рассматриваемого промежутка
	uint64_t  max_count_simples = 1000;//  корень из 10в16ой = 10в8ой 
	uint64_t  *simples = new uint64_t[max_count_simples];

	// Получим количество простых чисел и все простые числа
	uint64_t  count_simples = 0;// getCountSimples(3, max_count_simples, simples);
	getPrimes(simples, &count_simples, 0, max_count_simples, 1);

	uint64_t  step = 40;// count_simples / 25;

	std::atomic<bool> * is_completed_threads = NULL;
	file = fopen("results.txt", "a");

	fprintf( file, "Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);
	printf( "Промежуток: до %lld, простых чисел всего : %lld, максимальное число = %lld\n\n", max_count_simples, count_simples, simples[count_simples - 2]);

	// запускаем поиск кортежей
	// для каждого кортежа - запускаем отдельный поток

	uint64_t *tmp = new uint64_t[MAX_FACTORS_COUNT];

	for (int i = 0; i < max_threads_count; i++) {
		completed[i] = true;
	}

	find_tuples(1, simples, 0, tmp, &max_count_simples);
}