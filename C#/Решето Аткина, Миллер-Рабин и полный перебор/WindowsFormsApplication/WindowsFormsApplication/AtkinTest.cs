using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication
{
    class AtkinTest
    {
        public int limit { get; set; }

        public AtkinTest(int limit)
        {
            this.limit = limit;
        }

        public void getResult()
        {
            
        }

        public void start()
        {
            int sqr_lim;
            bool[] is_prime = new bool[10002];
            int x2, y2;
            int i, j;
            int n;

            sqr_lim = (int)Math.Sqrt(limit);

            for (i = 0; i <= limit; i++)
                is_prime[i] = false; // Инициализация решета

            is_prime[2] = true;
            is_prime[3] = true;
            
            // Предположительно простые - это целые с нечетным числом
            // представлений в данных квадратных формах.
            // x2 и y2 - это квадраты i и j (оптимизация).
            x2 = 0;
            for (i = 1; i <= sqr_lim; i++)
            {
                x2 += 2 * i - 1;
                y2 = 0;
                for (j = 1; j <= sqr_lim; j++)
                {
                    y2 += 2 * j - 1;
                    n = 4 * x2 + y2;
                    if ((n <= limit) && (n % 12 == 1 || n % 12 == 5))
                        is_prime[n] = !is_prime[n];
                    // n = 3 * x2 + y2; 
                    n -= x2; // Оптимизация
                    if ((n <= limit) && (n % 12 == 7))
                        is_prime[n] = !is_prime[n];
                    // n = 3 * x2 - y2;
                    n -= 2 * y2; // Оптимизация
                    if ((i > j) && (n <= limit) && (n % 12 == 11))
                        is_prime[n] = !is_prime[n];
                }
            }
            // Отсеиваем кратные квадратам простых чисел в интервале [5, sqrt(limit)].
            // (основной этап не может их отсеять)
            for (i = 5; i <= sqr_lim; i++)
            {
                if (is_prime[i])
                {
                    n = i * i;
                    for (j = n; j <= limit; j += n)
                    {
                        is_prime[j] = false;
                    }
                }
            }
            // Вывод списка простых чисел в консоль.
            Console.Write("2 3 5 ");
            int q = 0;
            for (i = 6; i <= limit; i++) // проверка делимости на 3 и 5
            {
                if ((is_prime[i]) && (i % 3 != 0) && (i % 5 != 0))
                {
                    Console.Write(i + " ");
                    q++;
                    if (q % 15 == 0)
                        Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
    }
}
