#include <iostream>
#include <fstream>
#include <string>
#include <conio.h>//Для последующей задержки экрана. См в конце getch()
using namespace std;

/*
структура
-Название пункта назначения
-номер рейса
-тип самолета

-ввод из файла в массив структур
-вводится название пункта назначения и вывод номеров рейсов и типов самолетов(совпавших)
-если такого нет - вывести соответствующее сообщение;
*/


int main()
{
	//Чтобы выводился русский язык:
	setlocale(LC_ALL, "Russian");

	const int length=10;//Длина для символьных строк

	//Переменные для буферной строки(для чтения):
	const int l_buf=3*length;

	struct AEROFLOT
	{
		char p_nazn[length+1];//Название пункта назначения - символьная строка
		int nomer_reys;//Номер рейса - целое число
		char type_reys[length+1];//Тип самолета - симв. строка
	};
	
	const int l_aeroflot=100;//Для массива структур,читаемых с файла

	AEROFLOT aeroflot[l_aeroflot];

	//Буферные строки для чтения:
	char buf[l_buf+1];
	char p_nazn[length+1];

	//Поток для чтения с файла:
	ifstream fin("aeroflot.txt");

	//Если файла не существует:
	if(!fin){cout<<"ФАЙЛ НЕ СУЩЕСТВУЕТ!"; return 0;};;

	int i=0;//Счетчик количества рейсов
	//Считываем данные из файла в массив структур:
	while(fin.getline(buf, l_buf))//Пока можно проводить считывание
	{
		if(i>=l_aeroflot){cout<<"Слишком много рейсов. Увеличьте значение переменной l_aeroflot!"<<endl; return 1;};;
		
		strncpy(aeroflot[i].p_nazn,buf,length);
		aeroflot[i].p_nazn[length]='\0';
		
		aeroflot[i].nomer_reys=atoi(&buf[length]);//Переводим из массива char в целое число int

		strncpy(aeroflot[i].type_reys,buf+2*length,length);//Копируем с номера позиции 2*length на длину length
		aeroflot[i].type_reys[length]='\0';
		
		i++;
	}

	//Вечный цикл обработки
	while(true)
	{
		cout<<"Введите пункт назначения или end(для завершения): ";
		gets(p_nazn);
		int kol=1;//Счетчик для грамотного вывода на экран рейсов
		if (strcmp(p_nazn,"end")==0) break;//Выход из цикла еслии введен "end"

		int not_reys=1;//1 - рейса нет, 0 - рейс существует(есть)
		
		
		for(int j=0; j<i; j++)
		{
			//Если строка есть в другой строке
			if(strstr(aeroflot[j].p_nazn,p_nazn))
			{
				//Если после пункта назначения в одной строке - пробел, значит, этот рейс находится в этой строке
				if (aeroflot[j].p_nazn[strlen(p_nazn)]==' ')
				{
					cout<<kol<<") "<<aeroflot[j].p_nazn<<" "<<aeroflot[j].nomer_reys<<" "<<aeroflot[j].type_reys<<endl;
						kol++;
					//Если рейс есть - отметим, что он существует
					not_reys=0;
				}
			}

		}
		//Выводим "Данный рейс отсутствует", если в массиве рейсов его не нашли
		if(not_reys)
			cout<<"Рейс отсутствует!"<<endl;
	}
	

	getch();//Для задержки
	return 0;
}