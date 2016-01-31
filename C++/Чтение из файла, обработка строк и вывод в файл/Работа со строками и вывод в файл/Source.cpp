#include<iostream>
#define _CRT_SECURE_NO_WARNINGS

#include<fstream>
#include<string.h>

using namespace std;

char* MyInsert(char *myString, char *subString, int N)
{
	// 1) Выделяем память размером strlen(myString) + strlen(subString) + 1
	int Size, Size3;
	Size = strlen(myString) + strlen(subString) + 1;
	Size3 = strlen(myString) - N;
	char *returnChar = new char[Size];
	char *temp = new char[Size3];

	// 2) Копируем туда часть строки myString до символа n - 1
	strncpy(returnChar, myString + 0, N);
	returnChar[N] = '\0';

	// 3) Копируем туда subString
	strcat(returnChar, subString);

	// 4) Копируем туда часть myString от символа n до конца
	strncpy(temp, myString + N, strlen(myString) - N);
	temp[strlen(myString) - N] = '\0';
	strcat(returnChar, temp);

	return returnChar;
}


int main()
{
	setlocale(LC_ALL, "rus"); // корректное отображение Кириллицы

	// создаём объект для записи в файл
	ofstream fout; /*имя объекта*/; // объект класса ofstream	
	// Объект для считывания
	ifstream fin;

	// открываем файл для чтения и прочитываем текст
	fin.open("text.txt");
	int length = 1000;
	
	// Выводим в файл результат
	fout.open("result.txt");

	// Просим ввести длину слова
	int k = 0;
	cout << "Введите количество гласных в слове:" << endl;
	cin >> k;

	char* str = new char[length]; // буфер промежуточного хранения считываемого из файла текста
	
	// Считываем по-строчно без файл
	while (!fin.eof())
	{
		fin.getline(str, length);// Считываем текст
		
		// Запускаем поиск слов с количеством гласных "K", и если это так - то вставляем после слова ALSOU 
		char *s = "AEIOUYaeiouy";// Все гласные
		char *stroka = "ALSOU";
		int n = 0;// Счетчик количества гласных в слове
		int i = 0;
		while (str[i] != '\0')
		{
			// Игнорируем пробелы идем по каждому слову(по условию задачи слово - символы между пробелами)
			n = 0;
			while (str[i] != '\0' && str[i] != '\n' && str[i] != ' ')
			{
				// Проверяем - глассная ли это?
				for (int j = 0; j<12; j++)
				{
					if (str[i] == s[j])
						n++;
				}

				i++;
			}

			// Количество гласных равно "K" - то вставляем ALSOU
			if (n == k)
			{
				str = MyInsert(str, stroka, i);
				i += 5;// Увеличим счетчик на 5(длина добавленных символов)
			}

			i++;
		}

		i = 0;
		while (str[i] != '\0')
		{
			// Выводим в консоль и в файл
			cout << str[i];
			fout << str[i];
			i++;
		}
		cout << endl;
		fout << endl;
	}

	fout.close();
	fin.close();

	cout << endl << "Результат в файле result.txt"<<endl;

	system("pause");
	return 0;
}