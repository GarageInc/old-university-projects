#include <iostream>
#include <math.h>
#include <stdio.h>
#include <string>

using namespace std;

// Проверка на деление числа на все его составные части( от 2 до корня от этого числа).
// Если делится - то точно простое число. НО! Не работает на четных числах, на вход подаются только нечетные числа с шагом 2: 1,3,5,7,9 и т.д.
bool ПровереноМодифицированнымПростымДелением(uint64_t *number) {
	uint64_t s = sqrt(*number) + 1;

	// нет смысла рассматривать четные числа, т.к. они не делятся на два
	for (uint64_t i = 3; i <= s; i += 2) {
		if ((*number) % i == 0) {
			return false;
		}
	}

	return true;
}

// Заполняет входной массив simples всеми простыми числами между start и finish - границами. Функция явно возвращает количество простых чисел и неявно - все найденные простые числа в массиве simples
uint64_t getCountSimples(uint64_t start, uint64_t finish, uint64_t *simples) {
	uint64_t index = 0;

	for (uint64_t j = start; j < finish; j += 2) {
		if (j % 2 != 0) {

			if (ПровереноМодифицированнымПростымДелением(&j)) {
				simples[index] = j;
				index = index + 1;
			}
		}
	}

	return index;
}


// Вывод значения в файл
void printValue(uint64_t *i, FILE *fout) {

	fprintf(fout, "%lld \n", *i);
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