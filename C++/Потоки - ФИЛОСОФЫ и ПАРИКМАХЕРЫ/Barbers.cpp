#include "stdafx.h"
#include <windows.h>
#include <tchar.h>
#include <strsafe.h>
#include <iostream>
#include <algorithm>
#include <stdio.h>

using namespace std;

int clients = 0;
HANDLE mutex;
HANDLE client;
HANDLE barber;
HANDLE service;

void Barbershop(int kol_bar, int kol_cl, int kol_it, int kol_ch);
DWORD WINAPI Client(LPVOID a);
DWORD WINAPI Barber(LPVOID a);

typedef struct Data {
	int num;
	int kol_bar;
	int kol_cl;
	int kol_it;
	int kol_ch;
} DATA, *PDATA;

int _tmain()
{
	setlocale(LC_ALL, "Russian");

	int kol_bar = 0, kol_cl = 0, kol_it = 0, kol_ch = 0;

	cout << "Введите количество парикмахеров: ";
	cin >> kol_bar;
	cout << "Введите количество клиентов: ";
	cin >> kol_cl;
	cout << "Введите количество посещений клиентами парикмахерской: ";
	cin >> kol_it;
	cout << "Введите количество кресел в зале ожидания парикмахерской: ";
	cin >> kol_ch;
	printf("\n");

	Barbershop(kol_bar, kol_cl, kol_it, kol_ch);

	printf("\n Стрижки завершены.");

	cin >> kol_cl;

	return 0;
}


void Barbershop(int kol_bar, int kol_cl, int kol_it, int kol_ch)
{
	PDATA *Data = new PDATA[kol_bar+kol_cl];
	HANDLE *Barbers_Clients = new HANDLE[kol_bar+kol_cl];

	mutex = CreateSemaphore(0, 1, 1, 0);
	client = CreateSemaphore(0, 0,kol_ch, 0);
	barber = CreateSemaphore(0, 0, 1, 0);
	service = CreateSemaphore(0, 0, 1, 0);

	for (int i = 0; i < kol_bar; i++)
	{
		Data[i] = new DATA;

		Data[i]->num = i;

		Barbers_Clients[i] = CreateThread(NULL, 0, Barber, Data[i], 0, NULL);
	}
	
	for (int i = 0; i < kol_cl; i++)
	{
		Data[kol_bar+i] = new DATA;

		Data[kol_bar + i]->num = i;
		Data[kol_bar + i]->kol_cl = kol_cl;
		Data[kol_bar + i]->kol_it = kol_it;
		Data[kol_bar + i]->kol_ch = kol_ch;

		Barbers_Clients[kol_bar + i] = CreateThread(NULL, 0, Client, Data[kol_bar + i], 0, NULL);
	}

	WaitForMultipleObjects(kol_bar+kol_cl, Barbers_Clients, TRUE, INFINITE);

	for (int i = 0; i < kol_bar+kol_cl; i++)
	{
		CloseHandle(Barbers_Clients[i]);
		delete Data[i];
	}

	CloseHandle(mutex);
	CloseHandle(client);
	CloseHandle(barber);

	delete[] Barbers_Clients;
}

DWORD WINAPI Client(LPVOID a)
{
	PDATA p;

	p = (PDATA)a;
	int walking_time = 1000 * (rand() % 3 + 1);

	for (int i = 0; i < p->kol_it; i++)
	{
		srand(GetTickCount());

		WaitForSingleObject(mutex, INFINITE);
		if (clients == p->kol_ch)
		{
			printf("Клиент %d уходит, нет свободных мест.\n", p->num);
			Sleep(walking_time);
			ReleaseSemaphore(mutex, 1, 0);
		}
		else
		{
			clients++;
			printf("Клиент %d сел в кресло.\n", p->num);
			ReleaseSemaphore(client, 1, 0);
			ReleaseSemaphore(mutex, 1, 0);
			WaitForSingleObject(barber, INFINITE);
			WaitForSingleObject(service, INFINITE);
		}
		
	}

	return TRUE;
}


DWORD WINAPI Barber(LPVOID a)
{
	PDATA p;

	p = (PDATA)a;
	int haircut_time = 1000 * (rand() % 3 + 1);

	while (true)
	{
		srand(GetTickCount());
		WaitForSingleObject(client, INFINITE);	
		WaitForSingleObject(mutex, INFINITE);
		clients--;
		printf("Парикмахер %d принял клиента.\n", p->num);
		Sleep(haircut_time);
		printf("Парикмахер %d закончил стрижку.\n", p->num);
		ReleaseSemaphore(barber, 1, 0);
		ReleaseSemaphore(mutex, 1, 0);
		ReleaseSemaphore(service, 1, 0);
	}

	return TRUE;
}
