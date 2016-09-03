#define _CRT_SECURE_NO_WARNINGS


#include "CLogReader.h"
#include <fstream>



// ВАЛИДАЦИЯ:
// правильные маски: abc, *abc, ?abc, abc*, abc?, *abc*, ?abc?, *abc?, ?abc*. 
// Причем количество символов может быть абсолютно любым
// Фильтр типа abc???? - означает, что должны быть отобраны строки, начинающиеся с abc и заказнчивающиеся любым дополнительным символом(условие неисключаемое)

// ФИЛЬТРОВАНИЕ СТРОКИ:
// Считаю, что * - любая последовательность символов(вплоть до пустой последовательности)
// Считаю, что ? - один единиченый символ
// При этом семантически * === ***...** и ? === ?????...?

// Файл считывается заданными блоками по 2кб


const int LENGTH = 100;


void Start()
{
	CLogReader 	sfText;
	bool		bReturn = false;
	
	char filter[ LENGTH];
		
	printf( "Enter filter for search:\n");
	scanf( "%100s", &filter);

	if ( sfText.SetFilter( filter))
	{
		char path[LENGTH];

		printf("Path to file:\n");
		scanf("%100s", &path);

		if ( sfText.Open( path))
		{
			std::ofstream fout;
			fout.open( "result.txt");

			printf( "Result:\n");
			printf( "\n------------------------------------\n");
			
			while ( sfText.GetNextLine() != 0)
			{
				if ( sfText.FilterLine())
				{
					fout << sfText.szLine << "\n";
					printf( "%s\n", sfText.szLine);
				} // else pass
			}

			printf( "\n------------------------------------\n");

			printf( "\n[Result in file result.txt]\n");

			sfText.Close(); 

			fout.close();
		}
		else
		{
			printf( "\n[Invalid file name!]\n");
		}
	}
	else
	{
		printf( "[Invalid filter]!\n");
	}

	
}


int main()
{
	while ( true)
	{
		printf("\nAre you want to filter?\n(input 1 to YES, 0(or other symbol) for NO)\n");

		char tmp;
		scanf("%c", &tmp);
				
		if ( tmp == '1')
			Start();
		else
			break;
	}
	
	system( "pause");

	return 0;
}