#include "stdafx.h"
#include <windows.h>
#include <tchar.h>
#include <strsafe.h>
#include <iostream>
#include <algorithm>
#include <stdio.h>

using namespace std;

HANDLE *Mutex = new HANDLE;
HANDLE Semaphore;

void Philosophy(int kol_fil, int kol_it);
DWORD WINAPI Philosopher(LPVOID a);

typedef struct Data {
	int fil_num;
	int kol_fil;
	int kol_it;
} DATA, *PDATA;

int _tmain()
{

	setlocale(LC_ALL, "Russian");

	int kol_fil = 0, kol_it = 0;

	cout << "Введите количество философов: " ;
	cin >> kol_fil;
	cout << "Введите количество обедов: ";
	cin >> kol_it;
	printf("\n");

	Philosophy(kol_fil, kol_it);

	printf("\n Обеды завершены.");

	cin >> kol_fil;

	return 0;
}


void Philosophy(int kol_fil, int kol_it)
{

	PDATA *Data = new PDATA[kol_fil];
	HANDLE  *Philosophers = new HANDLE[kol_fil];

	Semaphore = CreateSemaphore(0, kol_fil - 1, kol_fil - 1, 0);

	for (int i = 0; i < kol_fil; i++)
		Mutex[i] = CreateMutex(NULL, FALSE, NULL);

	for (int i = 0; i < kol_fil; i++)
	{
		Data[i] = new DATA;
		Data[i]->fil_num = i;
		Data[i]->kol_fil = kol_fil;
		Data[i]->kol_it = kol_it;

		Philosophers[i] = CreateThread(NULL, 0, Philosopher, Data[i], 0, NULL);
	}

	WaitForMultipleObjects(kol_fil, Philosophers, TRUE, INFINITE);

	for (int i = 0; i < kol_fil; i++)
	{
		CloseHandle(Philosophers[i]);
		CloseHandle(Mutex[i]);
		delete Data[i];
	}

	CloseHandle(Semaphore);

	delete[] Philosophers;
}

DWORD WINAPI Philosopher(LPVOID a)
{
	PDATA p;

	p = (PDATA)a;
	int launch_time = 1000 * (rand() % 3 + 1);
	int thinking_time = 1000 * (rand() % 5 + 1);

	for (int i = 0; i < p->kol_it; i++)
	{	
		srand(GetTickCount());
		printf("Философ %d размышляет.\n", p->fil_num);
		Sleep(thinking_time);
		WaitForSingleObject(Semaphore, INFINITE);
		WaitForSingleObject(Mutex[p->fil_num], INFINITE);
		printf("Философ %d взял правую палочку.\n", p->fil_num);
		WaitForSingleObject(Mutex[(p->fil_num + 1) % p->kol_fil], INFINITE);
		printf("Философ %d взял левую палочку.\n", p->fil_num);
		printf("Философ %d обедает.\n", p->fil_num);
		Sleep(launch_time);
		printf("Философ %d закончил обед.\n", p->fil_num);
		ReleaseMutex(Mutex[p->fil_num]);
		ReleaseMutex(Mutex[(p->fil_num + 1) % p->kol_fil]);
		ReleaseSemaphore(Semaphore, 1, 0);
	}

	return TRUE;
}
