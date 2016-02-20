#pragma once
// next_prime(uint64_t x) функция для получения следующего простого числа решетом Эратосфена
// возвращает 0 если рассчитать не удалось
// для освобождения памяти next_prime(0)
// использует 64кб памяти под решето и от 16 кб под кэш для инициализации. 16 кб хватает до ~10^10

// при многопоточном использовании:
// сначала инициализировать кэш максимальным значением:
// next_prime(MAX);
// в каждом потоке создавать отдельную биткарту:
// prime_bitmap_t pb = {0};
// x = next_prime(x, &pb);

#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>  
#include <string.h>

#define PRIME_BUF 1048576 // Размер буфера для окна решета (обязательно кратно степеням 2-ки, при увеличении пересчитать init[])

typedef struct {
	uint32_t bitmap[PRIME_BUF / 16]; // биткарта
	uint64_t limit; // до скольки заполнено решето (конец буфера)
} prime_bitmap_t;


uint64_t next_prime(uint64_t x, prime_bitmap_t* pb)
{
	static uint8_t* cache = NULL; // кэш разниц всех простых для инициализации решета
	static uint8_t* cache_next = NULL; // следующий свободный
	static uint8_t* cache_end = NULL; // конец

	if (!cache && x != 0) { // первоначальное выделение памяти под кэш
		uint32_t size = 16384;
		cache = (uint8_t*)malloc(size);
		if (!cache) {
			printf("No %u memory for cache\n", size);
			return 0;
		}
		cache_end = cache + size;
		// инициализация кэша для заполнения первого решета размером PRIME_BUF
		uint8_t init[] = { 1,2,1,2,1,2,3,1,3,2,1,2,3,3,1,3,2,1,3,2,3,4,2,1,2,1,2,7,2,3,1,5,1,3,3,2,3,3,1,5,1,2,1,6,6,2,1,2,3,1,5,3,3,3,1,3,2,1,5,7,2,1,2,7,3,5,1,2,3,4,3,3,2,3,4,2,4,5,1,5,1,3,2,3,4,2,1,2,6,4,2,4,2,3,6,1,9,3,5,3,3,1,3,5,3,3,1,3,3,2,1,6,5,1,2,3,3,1,6,2,3,4,5,4,5,4,3,3,2,4,3,2,4,2,7,5,6,1,5,1,2,1,5,7,2,1,2,7,2,1,2,10,2,4,5,4,2,3,3,7,2,3,3,4,3,6,2,3,1,5 };
		memcpy(cache, init, sizeof(init));
		cache_next = cache + sizeof(init);
	}

	if (x < 5) {
		if (x == 0) { // освобождение памяти
			if (cache) {
				free(cache);
				cache = NULL;
			}
			return 0;
		}
		else {
			return (x < 2) ? 2 : ((x < 3) ? 3 : 5);
		}
	}
	else {
		uint32_t* bitmap = pb->bitmap;
		x = ((x - 1) | 1);
		uint32_t step = ((x % 3) ^ 3) << 1;
		if (step == 6) { // x кратно 3
			step = 4;
			x += 2;
		}
		else {
			x += step;
		}
		if (x >= pb->limit || x < pb->limit - PRIME_BUF) pb->limit = 0; // x отсутствует в текущем буфере
		uint32_t next = x & (PRIME_BUF - 1);

		while (true) {
			if (pb->limit) { // есть биткарта на нужный диапазон
							 // поиск следующего после x
				if (!step) {
					step = ((pb->limit - PRIME_BUF + next) % 3) << 1;
					if (!step) {
						step = 4;
						next += 2;
					}
				}
				for (; next < PRIME_BUF; next += step) {
					step ^= 6;
					if ((bitmap[next >> 6] & (1 << ((next >> 1) & 31))) == 0) {
						return pb->limit + next - PRIME_BUF;
					}
				}
				next = 1;
				step = 0;
			}
			// нет или нехватило биткарты, инициализация следующего буфера
			if (!pb->limit) pb->limit = x - (x & (PRIME_BUF - 1));
			uint64_t start = pb->limit;
			pb->limit += PRIME_BUF;
			if (pb->limit < start) { // превышение разрядности
				printf("No more primes :(\n");
				return 0;
			}
			memset(bitmap, 0, PRIME_BUF / 16);
			uint8_t* cur = cache;
			uint64_t prime_next = 5; // первое простое для заполнения решета
			while (true) {
				uint64_t j = prime_next * prime_next;
				if (j >= pb->limit) break; // дальше заполнять не надо
				if (prime_next > 0xFFFFFFFF) { // превышение разрядности j
					printf("No more primes :(\n");
					return 0;
				}
				uint32_t skip3 = prime_next % 3; // для пропуска кратных 3
				if (j < start) { // выравнивание под prime_next*prime_next+2N*prime_next
					uint64_t block = prime_next * 6;
					uint64_t start_block = start - (start - j) % block;
					if (start_block + (prime_next << (skip3 ^ 3)) < start) {
						j = start_block + block;
					}
					else {
						skip3 ^= 3;
						j = start_block + (prime_next << skip3);
					}
				}
				if (j < pb->limit) {
					uint32_t step;
					if (prime_next > PRIME_BUF) {
						step = PRIME_BUF * 4; // защита от превышения разрядности
					}
					else {
						step = ((uint32_t)prime_next) << skip3;
					}
					for (uint32_t i = (j & (PRIME_BUF - 1)); i < PRIME_BUF; i += step) { // Вычеркиваем кратные prime_next
						if (skip3 & 1) { // следующее кратно 3
							step <<= 1;
						}
						else {
							step >>= 1;
						}
						skip3 ^= 3;
						bitmap[i >> 6] |= (1 << ((i >> 1) & 31));
					}
				}
				if (cur < cache_next) { // следущее простое из кэша
					prime_next += ((uint32_t)*cur) << 1;
					cur++;
				}
				else {  // следующего нет в кэше
					if (cache_next == cache_end) {
						// нет места в кэше, выделение памяти
						uint32_t size = 0;
						if (x > 1.8E19) {
							size = 198 * 1048576;
						}
						else if (x >= 1E19) {
							size = 145 * 1048576;
						}
						else if (x > 1E18) {
							size = 48 * 1048576;
						}
						else if (x > 1E17) {
							size = 17 * 1048576;
						}
						else if (x > 1E16) {
							size = 6 * 1048576;
						}
						else if (x > 1E14) {
							size = 2 * 1048576;
						}
						else if (size < 131072) {
							size = 131072; // 128K
						}
						if (size < (uint32_t)(cache_end - cache) * 5 / 4) size = (uint32_t)(cache_end - cache) * 5 / 4; // +20%
						if (size > 194 * 1048576) size = 194 * 1048576;

						uint8_t* p = (uint8_t*)realloc(cache, size);
						if (!p) {
							printf("No memory for cache. New size %d byte\n", size);
							return 0;
						}
						printf("reallocate cache to %uKb\n", size / 1024);
						cache_end = p + size;
						cache_next = p + (cache_next - cache);
						cache = p;
					}
					// запись в кэш следущее простое
					prime_bitmap_t pb2 = { 0 };
					uint64_t p = next_prime(prime_next, &pb2);
					if (p == 0) return 0;
					if (p - prime_next >= 512) {
						printf("Error cache: %llu - %llu = %llu > 512\n", p, prime_next, p - prime_next);
						return 0;
					}
					*cache_next = (uint8_t)((p - prime_next) >> 1);
					cache_next++;
					prime_next = p;
					cur = cache_next;
					// все необходимые для расчета до x
					while (cache_next != cache_end && prime_next * prime_next < x) {
						uint64_t prime = next_prime(p, &pb2);
						if (p == 0) return 0;
						if (prime - p >= 512) {
							printf("Error cache: %llu - %llu = %llu > 512\n", prime, p, prime - p);
							return 0;
						}
						*cache_next = (uint8_t)((prime - p) >> 1);
						cache_next++;
						p = prime;
					}
					// считывание остатков из биткарты pb2
					uint32_t step = ((p % 3) ^ 3) << 1;
					uint32_t* bitmap2 = pb2.bitmap;
					for (uint32_t i = (p + step) & (PRIME_BUF - 1); i < PRIME_BUF; i += step) { // заполнение кэша из pb2
						step ^= 6;
						if ((bitmap2[i >> 6] & (1 << ((i >> 1) & 31))) == 0) {
							if (cache_next == cache_end) break;
							uint64_t prime = pb2.limit - PRIME_BUF + i;
							if (prime - p >= 512) {
								printf("Error cache: %llu - %llu = %llu > 512\n", prime, p, prime - p);
								return 0;
							}
							*cache_next = (uint8_t)((prime - p) >> 1);
							cache_next++;
							p = prime;
						}
					}
					//printf("fill cache to %llu\n", p);
				}
			}
		}
	}
	return 0;
}

// однопоточный вариант
uint64_t next_prime(uint64_t x)
{
	static prime_bitmap_t* pb = NULL;
	if (x == 0) {
		if (pb) {
			free(pb);
			pb = NULL;
		}
	}
	else if (!pb) {
		pb = (prime_bitmap_t*)calloc(1, sizeof(prime_bitmap_t));
		if (!pb) {
			printf("No memory for prime bitmap\n");
			x = 0;
		}
	}
	return next_prime(x, pb);
}