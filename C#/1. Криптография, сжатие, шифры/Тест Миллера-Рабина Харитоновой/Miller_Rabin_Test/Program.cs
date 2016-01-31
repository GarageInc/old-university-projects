using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Miller_Rabin_Test
{
    class Program
    {
        // Реализация теста
        // Передается исследуемое число 'n'  и основание проверка 'a'
        // Тест Миллера-Рабина
        static bool MillerRabinTest(long n, long a)
        {
            // Отсеиваем тривиальные случаи(самые простые)
            if (n <= 1)
                return false;
            if (n == 2)
                return true;
            if (n % 2 == 0)
                return false;
            long s = 0, d = n - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            // Реализация проверок по возведению в степень по модулю 'n'
            long r = 1;
            long x = (long)BigInteger.ModPow(a, d, n);
            if (x == 1 || x == n - 1)
                return true;

            x = (long)BigInteger.ModPow((long)Math.Pow(a, d), (long)Math.Pow(2, r), n);

            if (x == 1)
                return false;

            if (x != n - 1)
                return false;

            return true;
        }

      
        static void Main(string[] args)
        {
            // Проверка чисел от 3 до 'n'
            int n=500;

            // Массив проверок наиболее частых ошибок числа 'a'
            int[] massA = new int[11];
            // ПРОВЕРКА НЕ НА 2х ОСНОВАНИЯХ 'a', а на 10! Т.е. чем больше оснований - тем точнее проверка числа на простоту int[] massA = new int [11];
            // Берем числа в диапазон от 3 до n
            for (int i = 3; i <= n; i++)
            {
                int schetchik = 0;// Счетчик свидетелей простоты
                // Разные основания 'а' от 2 до 10
                for (int a = 2; a<i && a < 11; a++)
                {
                    // Запускаем тест
                    bool b = MillerRabinTest(i, a);// Передаем число, 
                    // Теперь проверим - верно ли что число простое, если тест сказал, что оно простое?
                    if (b)
                    {
                        schetchik++;
                        // Проверим на делимость ручным способом
                        for (int j = 2; j <= Math.Sqrt((double)i); j++)
                        {
                            if (i % j == 0)
                            {
                                massA[a]++;
                                Console.WriteLine("Ошибка для числа: "+i+" при проверке 'r'=1 и при 'a'= "+a);
                                Console.WriteLine("   Вероятность ошибки не более "+100*(1-Math.Pow(0.25,schetchik))+"%\n");
                                break;
                            }
                        }

                    }
                }
                
            }

            // Массив ошибок 'a'
            for (int i = 2; i < massA.Length; i++)
            {
                Console.WriteLine("a="+i+" => "+massA[i]+" ошибок; ");
            }
            // Выведем максимальную ошибку
            int min = n;
            int index = 0;
            for (int i = 2; i < massA.Length; i++)
            {
                if (massA[i] < min)
                {
                    min = massA[i];
                    index = i;
                }
            }
        
            Console.WriteLine("Наименьшее число ошибок = "+min + " при 'a'="+index);

            // ЧТобы экран не гас быстро
            Console.ReadKey();
        }
    }
}
