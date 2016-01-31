
#include <iostream>
#include <conio.h>
#include <fstream>//Поток для ссчитывания
#include <math.h>//Для математической функции - извлечение корня
#include <iomanip>// Манипуляторы - для грамотного вывода.
using namespace std;

void BiggerThen(int chislo)
	{
	//1.
		//Ссчитаем количество элементов с файла input.txt
		//Открываем файл
		ifstream in("input.txt");
		int kol=0;
		int safe[100];
		while (!in.eof()) 
		{
			in>>safe[kol];
			kol++;
		}
		in.close();//Закрываем открытый файл
		
		cout << "Kolichestvo chisel = "<<kol<<"\n"; 

	//2.
		//Строим наш квадратный массив
		int n = sqrt((double)kol);
		int mass[100][100];

		//Считывам из файла массив
		int schet=0;
		for(int j=0; j<n; j++)
		{
			int i=0;
			while (i<n)
			{
				mass[j][i]=safe[schet];
				schet++;
				i++;
			}    
		}

		//Выведем в консоль наш массив!
		cout<<"\n Vidod massiva: \n";
		for(int i=0; i<n; i++)
		{
			for(int j=0; j<n; j++)
			{			
				cout<<setw (3)<<mass[i][j]<<" ";
			}
			cout<<"\n";
		}

		//Считаем среднее арифметическое
		//Для записи в файл!
		ofstream out_file("output.txt");

		int sred=0;//ДЛя подсчета среднего арифметического.
		
			for(int i=0; i<n; i++)
			{
				int sum=0;
				for(int j=0; j<n; j++)
				{			
					sum=sum+mass[i][j];
				}
				sred=sum/n;
				if (sred>chislo) {out_file << i <<" ";} ;//Записали индекс строки. И т.д.
			}
		


		out_file.close();//Закрываем открытый файл
	};

int main()// Задачу решаю "с конца".
{
	

	//Вызываем процедуру - индексы строк, которые больше нашего числа - 15, по условию задачи
	BiggerThen(15);




	
	_getch();
	return 0;
}