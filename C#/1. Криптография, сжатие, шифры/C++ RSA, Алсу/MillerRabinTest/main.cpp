#include<iostream>
#include<conio.h>
#include<stdio.h>
#include<math.h>
#include <fstream>
#include <iomanip>

using namespace std;


        // Расширенный алгоритм Евклида
        void РасширенныйАлгоритмЕвклида(long a, long b, long *x, long *y, long *d)
        {
            long q, r, x1, x2, y1, y2;

            if (b == 0)
            {
                *d = a;
                *x = 1;
                *y = 0;
                return;
            }

            x2 = 1;
            x1 = 0;
            y2 = 0;
            y1 = 1;

            while (b > 0)
            {
                q = a/b;
                r = a - q*b;
                *x = x2 - q*x1;
                *y = y2 - q*y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = *x;
                y2 = y1;
                y1 = *y;
            }

            *d = a;
            *x = x2;
            *y = y2;
        }

        long НайтиОбратныйПоМодулю(long a, long n)
        {
            long x, y, d;
            РасширенныйАлгоритмЕвклида(a, n,&x, &y, &d);

            if (d == 1) return x;

            return 0;

        }

// Функция для умножения двух чисел x,y по модулю m
long long mulmod(long long x,long long y, long long m)
{
    return (x*y)%m;
}

// Функция возведения числа x в степен а по модулю n
long long powmod(long long x,long long a,long long m)
{
        long r=1;
        while(a>0)
        {
                if(a%2!=0) 
                   r=mulmod(r,x,m);
                a=a>>1; 
                x=mulmod(x,x,m);
        }
        return r;
}

// Функция теста Миллера-Рабина
bool test_Miller_Rabin(long long m, long long a) {		
       if(m==2 || m==3)
             return true;

       if(m % 2 == 0 || m == 1){
             return false;
       }
	   // Сначала мы отсеяли самые простые случаи

       long long s = 0;
       long long t = m-1;
       long long x=0;
	   long long y=0;

	   // Считаем количество степени двойки
       while(t!=0 && t % 2 == 0){
             s++;
             t/=2;
       }

				// Реализация проверок по возведению в степень по модулю 'n'
                long r = 1;
                x = (long)powmod(a, t, m);
                if (x == 1 || x == m - 1)
                    return true;

                x = (long)powmod((long)pow(a,(double)t), (long)pow(2.0, r), m);

                if (x == 1)
                    return false;
            
                if (x != m - 1)
                    return false;
            
            return true;
}

        // Функция проверки, использует тест Миллера-Рабина
        bool ПроверитьЧислоТестомМиллераРабина(long i)
        {
            // ПРОВЕРКА НЕ НА 2х ОСНОВАНИЯХ 'a', а на 10! Т.е. чем больше оснований - тем точнее проверка числа на простоту long[] massA = new long [11];
            // Разные основания 'а' от 2 до 10
            for (long a = 2; a < i && a < 11; a++)
            {
                // Запускаем тест
                bool b = test_Miller_Rabin((long)i, (long)a);// Передаем число и проверяем
                if (!b)
                {
                    return false;
                }
            }
            return true;
        }

		        // Получить простое число
        long ПолучитьПростоеЧисло()
        {
            long число=0;
            bool isCorrect = false;
            // Ищем первое просто число
            while (!isCorrect)
            {
				cout<<"Введите простое число:";
				cin>>число;
                //число = (long)rand();
                if (ПроверитьЧислоТестомМиллераРабина(число))
                {
					break;
                }
				else
					cout<<"Введенное число не простое!"<<endl;
            }
            return число;
        }


		        // Имитация шифра RSA
        void ШифрRSA(long p, long q, long *code, long *decoded)
        {
            // Попросим ввести зашифровываемое сообщение 'm'
            cout<<"Введите сообщение = "<<endl;
			char message[1000];
            cin>>message;

            // Установим константу 'e'
			int a[3];
			a[0]=17;
			a[1]=257;
			a[2]=65537;//{ 17, 257, 65537 }
			long e = a[rand()%3];// Одно из 3х простых чисел Ферма

            // Зашифруем число 'm' закрытым ключом Алисы
			// Зашифруем сообщение:
			string result="";
			long n = p * q;
			for(int i=0; message[i]; i++)
			{
				result +=(char) powmod((int)message[i], e, n);
			}
			printf("Зашифрованное сообщение = %s",result);
            /*long n = p * q;
            *code = powmod(m, e, n);
            *//*cout<<"\nШифр числа 'm' закрытым ключом Алисы(e,n) = " << *code<<endl;
*/

            // Расшифруем
            // Найдем сначала константу 'd' - закрытый ключ Боба
            //long fn = (p - 1) * (q - 1);
            //long d = НайтиОбратныйПоМодулю(e, fn);
            //if (d < 0) d = fn + d;
            //*decoded = powmod(*code, d, n);
            //cout<<"Расшифровка числа 'm' закрытым ключом Боба(d,n) = " << *decoded<<endl;

            //// Сравним и выведем результат
            //if (*decoded == m)
            //    cout<<"\nЧисла после шифровки и расшифровки совпали!"<<endl;
            //else
            //{
            //    cout<<"\nНеверная расшифровка!"<<endl;
            //}
        }


int main()
{
	setlocale( LC_ALL,"Russian" );// Чтобы выводился текст на русском языке
	ofstream fout("mytest.txt",ios::out);

			 // 2 простых числа в диапазоне от 1 до 10 млн
            long p=0, q=0;

            // Ищем первое простое число
            p = ПолучитьПростоеЧисло();
            // Ищем второе простое число
            q = p;
            while(q==p)
                q = ПолучитьПростоеЧисло();

            // Выведем полученный результат - 2 простых числа. Если они действительно простые, то всё должно сработать
            cout<<"ПРОСТЫЕ ЧИСЛА: 'p' = "<<p<<"; 'q'="<<q<<endl;

            long code=0;
            long decoded=0;
            ШифрRSA(p,q,&code, &decoded);
            

			getch();

			return 0;			
}