
const int COUNT_FIRST_SIMPLES = 19;
int FIRST_SIMPLES[COUNT_FIRST_SIMPLES] = { 3, 5,	7,	11,	13,	17,	19,	23,	29,	31,	37,	41,	43,	47,	53,	59,	61,	67,	71 };

// Проверка на деление числа на все его составные части( от 2 до корня от этого числа).
// Если делится - то точно простое число.
bool SimpleDivisionTest(uint64_t *number) {
	if (*number % 2 == 0) 
		return false;
	
	for (int i = 0; i < COUNT_FIRST_SIMPLES; i++ ) {

		if ( (*number) % FIRST_SIMPLES[ i ] == 0  ) {
			
			return true;
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
void printValue_uint64_t(uint64_t *i, FILE *fout) {

	fprintf(fout, "%lld\n", (*i));
}

// Вывод значения в файл
void printValue_uint128_t(uint128_t *i, FILE *fout) {
	
	fprintf(fout, "%s\n", boost::lexical_cast<std::string>(*i).c_str());
}

std::stringstream ss;
// Вывод значений в файл
void printValues( uint128_t *values, int*count, FILE *fout ) {
	
	ss.str(std::string());
	ss.clear();

	ss << boost::lexical_cast<std::string>(values[ 0 ]).c_str() << " = " << boost::lexical_cast<std::string>(values[ 1 ]).c_str();
	
	for (int i = 2; i < *count; i++) {
		//result = sprintf("%s",result.c_str())
		ss << " * " << boost::lexical_cast<std::string>(values[i]).c_str();
	}

	fprintf(fout, "%s\n", ss.str().c_str());// boost::lexical_cast<std::string>(*n).c_str(), boost::lexical_cast<std::string>(p).c_str(), boost::lexical_cast<std::string>(*q).c_str());
}
/*
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
*/

// Получение НОК массива чисел
uint128_t getNOK(uint128_t *array, int index = 0) {

	uint128_t lcm = ( array[ index ] * array[ index + 1 ]) / gcd( array[ index ], array[ index + 1 ] );

	if (array[ index + 1 ] && array[ index + 2 ]) {

		array[ index + 1 ] = lcm;
		return getNOK( array, index + 1 );
	}
	else {
		
		return lcm;
	}
}

// Получение ord числа
uint128_t getOrd(uint128_t p, uint128_t a) {
	
	int* H = new int[MAX_FACTORS_COUNT];// Массив для делителей p−1.
													
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

	delete[] H;

	if ( isChanged )
		return u;
	else
		return (p - 1);
}

