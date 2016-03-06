
// Проверка на деление числа на все его составные части( от 2 до корня от этого числа).
// Если делится - то точно простое число. НО! Не работает на четных числах, на вход подаются только нечетные числа с шагом 2: 1,3,5,7,9 и т.д.
bool ПровереноМодифицированнымПростымДелением(uint128_t *number) {
	uint128_t s = sqrt(*number) + 1;// sqrt(*number) + 1;

	// нет смысла рассматривать четные числа, т.к. они не делятся на два
	for (uint128_t i = 3; i <= s; i += 2) {
		if (*number % i == 0 ) {
			return false;
		}
	}

	return true;
}


// Заполняет входной массив simples всеми простыми числами между start и finish - границами. Функция явно возвращает количество простых чисел и неявно - все найденные простые числа в массиве simples
/*int1024_t getCountSimples(int1024_t start, int1024_t finish, int1024_t *simples) {
	int1024_t index = 0;

	for (int1024_t j = start; j < finish; j += 2) {
		if (j % 2 != 0) {

			if (ПровереноМодифицированнымПростымДелением(&j)) {
				simples[index] = j;
				index = index + 1;
			}
		}
	}

	return index;
}
*/

// Вывод значения в файл
void printValue(uint128_t *i, FILE *fout) {
	
	fprintf(fout, "%s\n", boost::lexical_cast<std::string>(*i).c_str());
}

// Вывод значений в файл
void printValues(uint128_t *n, uint128_t *p, uint128_t *q, FILE *fout) {
	uint128_t a = 132;
	
	auto str = boost::lexical_cast<std::string>(a);

	return;

	const char* n_str = (*n).convert_to<string>().c_str();
	const char* p_str = (*p).convert_to<string>().c_str();
	const char* q_str = (*q).convert_to<string>().c_str();
	
	fprintf(fout, "%s = %s * %s\n", n_str, p_str, q_str);
}

// Закрытие файлов, вызывает flush для выгрузки в них невыгруженного вывода. Если он есть, конечно.
void closeFiles(FILE **FOUT_FILES, int THREADS_COUNT) {

	for (int j = 0; j <= THREADS_COUNT; j++) {
		fclose(FOUT_FILES[j]);
	};
}

// Инициализируем файлы для вывода информации. Каждое имя файла - равно номеру потока. Чтобы не создавать конкурентных ресурсов.
// Более правильно было бы использовать один файл для всех потоков, но работа с мьютексами отнимает очень много системных-временных ресурсов. Главное - скорость.
// Обработку данных можно оставить для другой программы.
void initFiles(FILE **FOUT_FILES, int THREADS_COUNT) {

	char tmp[20];

	for (int j = 0; j < THREADS_COUNT; j++) {

		string name = std::to_string(j) + ".txt";
		strcpy(tmp, name.c_str());

		FOUT_FILES[j] = fopen(tmp, "w");
	}

	FOUT_FILES[THREADS_COUNT] = fopen("stats.txt", "w");
}


// Получение НОК массива чисел
uint128_t getNOK(uint128_t *array, int length, int index = 0) {

	uint128_t lcm = (array[index] * array[index + 1]) / gcd(array[index], array[index + 1]);

	if (index + 2  < length) {

		array[index + 1] = lcm;
		return getNOK(array, index + 1);
	}
	else {
		
		return lcm;
	}
}

// Получение ord числа
uint128_t ord(uint64_t p, uint128_t a) {
	
	int* H = new int[(int)boost::math::log1p(33)];// Массив для делителей p−1. Лаб.работа: числа 10в16ой, но т.к. рассматриваются только нечетные числа, то количество делителей будет не более чем log(3,10в16ой)
													
	uint128_t pp = p - 1;
	int k = 0, i = 2;
	
	for (; i <= pp && pp; i++) {
		while (pp % i == 0) {
			H[k] = i;
			k++;
			pp = pp / i;
		}
	}

	uint128_t u = p - 1;
	bool isChanged = false;

	uint128_t b;
	uint128_t c;

	for (i = 0; i < k; i++) {
		c = u / H[i];
		b = (powm(a, c, p));// % (*p);

		if (b == 1) {
			u = u / H[i];
			isChanged = true;
		}
		else {
			// pass
		}
	}

	// delete[] H;

	if (isChanged)
		return u;
	else
		return (p - 1);
}

