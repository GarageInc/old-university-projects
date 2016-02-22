
// База для теста Миллера-Рабина
int A[] = { 2};
const int A_LENGTH = 1;

// Функция для умножения двух чисел x,y по модулю m
uint64_t mulmod(uint64_t *x, uint64_t *y, uint64_t *m)
{
	return ((*x)*(*y)) % (*m);
}

// Функция возведения числа x в степен а по модулю n
uint64_t powmod(uint64_t x, int a, uint64_t *m)
{
	uint64_t r = 1;

	while (a > 0)
	{
		if (a % 2 != 0)
			r = mulmod(&r, &x, m);
		a = a >> 1;
		x = mulmod(&x, &x, m);
	}

	return r;
}

// Функция теста Миллера-Рабина
bool test_Miller_Rabin(uint64_t *m, int *a) {

	uint64_t s = 0;
	uint64_t t = *m - 1;
	uint64_t x = 0;
	uint64_t y = 0;

	// Считаем количество степени двойки
	while (t != 0 && t % 2 == 0) {
		s++;
		t = t >> 1;
	}

	x = powmod(*a, t, m);

	if (x == 1 || x == *m - 1)
		return true;

	// цикл B: s-1 раз
	for (int j = 0; j<s - 1; j++) {
		x = powmod(x, 2, m);

		if (x == 1)
			return false;

		if (x == *m - 1)
			return true;
	}

	return false;
}


// Функция проверки, использует тест Миллера-Рабина
bool ПровереноТестомМиллераРабина(uint64_t *number)
{
	for (int i = 0; i < A_LENGTH; i++)
	{
		// Запускаем тест
		if (!test_Miller_Rabin(number, &A[i]))
		{
			return false;
		}
	}
	return true;
}