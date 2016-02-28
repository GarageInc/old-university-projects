
// База для теста Миллера-Рабина
int A[] = { 2, 3, 5 };
const int A_LENGTH = 2;

cpp_int FOR_THREADS[100];

// Функция для умножения двух чисел x,y по модулю m
uint128_t mulmod(uint128_t *x, uint128_t *y, uint128_t *m, int *current_index)
{
	FOR_THREADS[*current_index] = (*x);
	FOR_THREADS[*current_index + 1] = (*y);
	FOR_THREADS[*current_index + 2] = FOR_THREADS[*current_index] * FOR_THREADS[*current_index + 1] % (*m);
	return FOR_THREADS[*current_index + 2].convert_to<uint128_t>();;
}

// Функция возведения числа x в степен а по модулю n
uint128_t powmod(uint128_t x, uint128_t a, uint128_t *m, int *index_j)
{
	uint128_t r = 1;
	int current_index = (*index_j) * 3;

	while (a > 0)
	{
		if ( a % 2 != 0 )
			r = mulmod(&r, &x, m, &current_index);
		a = a >> 1;//  1;
		x = mulmod(&x, &x, m, &current_index);
	}
	//printf(" => %lld\n", r);	

	return r;
}

// Функция теста Миллера-Рабина
bool test_Miller_Rabin(uint128_t *m, int *a, int *index_j) {
	
	int s = 0;
	uint128_t t = *m - 1;
	uint128_t x = 0;
	uint128_t two = 2;

	// Считаем количество степени двойки
	while (t != 0 && t % 2 == 0) {
		s = s + 1;
		t = t >> 1;
	}

	x = powmod(*a, t, m, index_j);

	if (x == 1 || x == *m - 1)
		return true;

	// цикл B: s-1 раз
	for (int j = 0; j < s - 1; j++) {
		x = powmod(x, two, m, index_j);

		if (x == 1)
			return false;

		if (x == *m - 1)
			return true;
	}

	return false;
}


// Функция проверки, использует тест Миллера-Рабина
bool ПровереноТестомМиллераРабина(uint128_t *number, int *index_j)
{
	for (int i = 0; i < A_LENGTH; i++)
	{
		// Запускаем тест
		if (!test_Miller_Rabin(number, &A[i], index_j))
		{
			return false;
		}
	}
	return true;
}