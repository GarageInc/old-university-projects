#define _CRT_SECURE_NO_WARNINGS
#include "CLogReader.h"
#include <fstream>

// Можно было бы и лучше написать, но... Честно признаю, часть кода кроме фильтрации строки и валидации маски
// я раньше для другого проекта использовал и старое, выпилив ненужное и добавив своё. В свою очередь история его появления уходит корнями в гугл.
// Если не сложно - оцените, пожалуйста, и, если есть ещё подобные задачи - пришлите на почту 9-b-rinat@rambler.ru . Любознательность свербит.
//  На неделе попробую сделать так, чтобы уже точно считывание происходил одним разом, без повторного просмотра некоторых строк. Сложность O(n) сделать хотелось бы.
// ПС: printf - это Си, но ускоряет ввод-вывод

// ДОП.ПРАВИЛА, к тем, что в задании
// ВАЛИДАЦИЯ:
// правильные маски: abc, *abc, ?abc, abc*, abc?, *abc*, ?abc?, *abc?, ?abc*. 
// Причем количество символов может быть абсолютно любым
// Фильтр типа abc???? - означает, что должны быть отобраны строки, начинающиеся с abc и заказнчивающиеся любым дополнительным символом(условие неисключаемое)

// ФИЛЬТРОВАНИЕ СТРОКИ:
// Считаю, что * - любая последовательность символов(вплоть до пустой последовательности)
// Считаю, что ? - один единиченый символ
// При этом семантически * === ***..** и ? === ?????..?

// РЕЗУЛЬТАТИВНОСТЬ: парсинг файла в ~40 мб происходит за 21~ (при самом сложном варианте ***Some* или *Some**) - когда порой нужно читать почти всю строку.
// Файл считывается блоками по 2кб

// Функция для чтения
void ReadTextFile()
{
	CLogReader 	sfText;
	std::string	szLine;// = new char[100];
	bool		bReturn = false;
	char mass[101];
	
	printf("Path to file or only name of file:\n");
	scanf("%100s", &mass);
	std::string path = ((std::string)mass).c_str();
	
	// Валидируем маску
	printf("Please, enter filter fo search:\n");
	scanf("%100s", &mass);
	std::string filter = ((std::string)mass).c_str();

	// Если фильтр провалидировался/установился:
	if (sfText.SetFilter(filter))
	{
		// Открываем исходный файл
		if (sfText.Open(path))
		{
			std::ofstream fout;// файл для записи
			fout.open("result.txt"); // связываем объект с файлом

			int i = 0;
			printf("Result of filtering:\n");
			printf("\n------------------------------------\n");
			
			// Считывание всех строк (друг за другом)
			// если получилось получить строку - print :)
			while (sfText.GetNextLine(&szLine) != 0)
			{
				// Проверяем по маске
				if (sfText.FilterLine(szLine))
				{
					fout << szLine.c_str() << "\r\n";	//и распечатка в файл
					printf("%s\r\n",szLine.c_str());	//и распечатка  в консоль
				}

				//printf("%s\r\n",szLine.c_str());	//и распечатка  в консоль
				//printf("%i",i++);
			}
			printf("\n------------------------------------\n");

			printf("\n[Result in file result.txt]\n");
			sfText.Close(); // Закрываем открытый файл
			fout.close();// Закрываем запись в поток
		}
		else
		{
			printf("\n[Invalid file name!]\n");
		}
	}
	else
	{
		printf("[Invalid filter]!\n");
	}

	
}


int main()
{
	std::string str;
	char tmp[101];
	while (true)
	{
		printf("\nAre you want to filter?(input 1 to YES, 0(or other symbol) for NO)\n");

		scanf("%100s", tmp);
		std::string str = tmp;
				
		if (str != "1")
			break;
		else
		{
			// Функция чтения
			ReadTextFile();
		}
	}
	
	system("pause");
	return 0;
}