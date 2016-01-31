using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Miller_Rabin_Test
{

    class Program
    {


        // Расширенный алгоритм Евклида
        private static void GCD(long a, long b, out long x, out long y, out long d)
        {
            long q, r, x1, x2, y1, y2;

            if (b == 0)
            {
                d = a;
                x = 1;
                y = 0;
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
                x = x2 - q*x1;
                y = y2 - q*y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }

            d = a;
            x = x2;
            y = y2;
        }

        static long inverse(long a, long n)
        {
            long x, y, d;
            GCD(a, n,out x, out y, out d);

            if (d == 1) return x;

            return 0;

        }

        // Тест Миллера-Рабина
        static bool MillerRabinTest2(long n, long a)
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


        // Функция проверки, использует тест Миллера-Рабина
        static bool CheckMR(long i)
        {
            // ПРОВЕРКА НЕ НА 2х ОСНОВАНИЯХ 'a', а на 10! Т.е. чем больше оснований - тем точнее проверка числа на простоту long[] massA = new long [11];
            // Разные основания 'а' от 2 до 10
            for (long a = 2; a < i && a < 11; a++)
            {
                // Запускаем тест
                bool b = MillerRabinTest2((long)i, (long)a);// Передаем число и проверяем
                if (!b)
                {
                    return false;
                }
            }
            return true;
        }
      
        static void Main(string[] args)
        {
            // 2 простых числа в диапазоне от 1 до 10 млн
            long p=0, q=0;
            Random r=new Random();
            bool isCorrect = false;
            // Ищем первое просто число
            while (!isCorrect)
            {
                p = (long) r.Next(100000,10000000);
                if (CheckMR(p))
                {
                    isCorrect = true;
                    for (long i = 2; i < Math.Sqrt(p) + 1; i++)
                    {
                        if (p % i == 0)
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("!");
            // Ищем второе простое число
            isCorrect = false;
            while (!isCorrect)
            {
                q = (long)r.Next(3, 15);
                if (CheckMR(q))
                {
                    isCorrect = true;
                    for (long i = 2; i < Math.Sqrt(q) + 1; i++)
                    {
                        if (q % i == 0)
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                }
            }

            // Выведем полученный результат - 2 простых числа. Если они действительно простые, то всё должно сработать
            Console.WriteLine("ПОЛУЧЕННЫЕ ПРОСТЫЕ ЧИСЛА: 'p' = "+p+"; 'q'="+q);

            // Попросим ввести зашифровываемое сообщение 'm'
            Console.WriteLine("Введите число 'm'(сообщение) от 1 до n = "+ p*q);
            long m = long.Parse(Console.ReadLine());

            // Установим константу 'e'
            long e = new long[] {17, 257, 65537}[r.Next(0, 3)];// Одно из 3х простых чисел Ферма

            // Зашифруем число 'm' закрытым ключом Алисы
            long n = p*q;
            long M = m;
            long code = (long)BigInteger.ModPow(m, e, n);
            Console.WriteLine("Шифр числа 'm' закрытым ключом Алисы(e,n) = " + code);
            
            // Расшифруем
            // Найдем сначала константу 'd' - закрытый ключ Боба
            long fn = (p - 1)*(q - 1);
            long d = inverse(e, fn);
            if (d < 0) d = fn + d;
            long decoded = (long) BigInteger.ModPow(code, d, n);
            Console.WriteLine("Расшифровка числа 'm' закрытым ключом Боба(d,n) = " + decoded);
            
            // Сравним и выведем результат
            if (decoded == M)
                Console.WriteLine("\nАлгоритм RSA работает верно, числа после шифровки и расшифровки совпали!");
            else
            {
                Console.WriteLine("\nНеверная расшифровка!");
            }


            // ЧТобы экран не гас быстро
            Console.ReadKey();
        }
    }
}
