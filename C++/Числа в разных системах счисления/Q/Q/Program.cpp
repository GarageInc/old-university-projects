#include<fstream>
#include<stdio.h>
#include<iostream>
#include<conio.h>
#include<vector>
#include<string>
#include<fstream>
#include<algorithm>
#include"ScalenumFunctions.h"
;
#include"Visualisation.h"


;
int main()
{
			
	setlocale(LC_ALL, "Russian");//Чтобы воспринимался русский текст

			ifstream rd("numbers.txt");
			ofstream wr("result.txt");

            int q = Initialize();//Выбираем систему счисления с которой работаем
            Scalenum scl(q);
            Scalenum scl1(q);
            int n = 0;
            while (n != 4)
            {
                n = Choice();//Выбираем выполняемое действие
                if (n != 5)//Если не выбрали выход
                {
                    Read( scl, rd);
                    Read( scl1, rd);
                    
					wr<<"Первое число:"<<endl;
                    Write(scl, wr);
                    
					wr<<"Второе число:"<<endl;
                    Write(scl1, wr);
                    
					switch(n)//Выполняем требуемое действие
                    {
                        case 1:
                        {
                            Scalenum s;
							s= scl+scl1;
                            wr<<endl<<"Результат:"<<endl;
                            Write(s, wr);
                        }
                        break;
                        case 2:
                        {
                            Scalenum s = scl*scl1;
                            wr<<endl<<"Результат:"<<endl;
                            Write(s, wr);
                        }
                        break;
                        case 3:
                        {
                            Scalenum s = scl%scl1;
                            wr<<endl<<"Результат:"<<endl;
                            Write(s, wr);
                        }
                        break;
                    }
                }
            }
			//Закрываем потоки
			wr.close();
			rd.close();
	return 0;
}
