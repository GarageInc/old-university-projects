using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Miller_Rabin_Test
{

    class Program
    {


        // Расширенный алгоритм Евклида
        private static void РасширенныйАлгоритмЕвклида(long a, long b, out long x, out long y, out long d)
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

        static long НайтиОбратныйПоМодулю(long a, long n)
        {
            long x, y, d;
            РасширенныйАлгоритмЕвклида(a, n,out x, out y, out d);

            if (d == 1) return x;

            return 0;

        }

        // Тест Миллера-Рабина
        static bool ТестМиллераРабина(long n, long a)
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
        static bool ПроверитьЧислоТестомМиллераРабина(long i)
        {
            // ПРОВЕРКА НЕ НА 2х ОСНОВАНИЯХ 'a', а на 10! Т.е. чем больше оснований - тем точнее проверка числа на простоту long[] massA = new long [11];
            // Разные основания 'а' от 2 до 10
            for (long a = 2; a < i && a < 11; a++)
            {
                // Запускаем тест
                bool b = ТестМиллераРабина((long)i, (long)a);// Передаем число и проверяем
                if (!b)
                {
                    return false;
                }
            }
            return true;
        }
      
        // Получить простое число
        public static long ПолучитьПростоеЧисло()
        {
            Random r = new Random();
            long число=0;
            bool isCorrect = false;
            // Ищем первое просто число
            while (!isCorrect)
            {
                Console.WriteLine("Введите простое число: ");

                число = int.Parse(Console.ReadLine()); //(long)r.Next(3, 15);
                if (ПроверитьЧислоТестомМиллераРабина(число))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введенное число НЕ простое!");
                }
            }
            return число;
        }

        // Имитация шифра RSA
        private static void ШифрRSA(long p, long q, ref long code, ref long decoded)
        {
            // Попросим ввести зашифровываемое сообщение 'm'
            Console.WriteLine("Введите текст сообщения:");
            string message = Console.ReadLine();

            // Установим константу 'e'
            Random r = new Random();
            long e = new long[] { 17, 257, 65537 }[r.Next(0, 3)];// Одно из 3х простых чисел Ферма

            // Зашифруем число 'm' закрытым ключом Алисы
            long n = p * q;

            char[] mass = message.ToCharArray(0, message.Length);
            string result = "";
            for(int i=0; i<mass.Length; i++)
            {
                result += (char)(int) BigInteger.ModPow((int) mass[i], e, n);
            }
           
            // Выведем результат:
            Console.WriteLine("Зашифрованная строка: "+result);
            //code = (long)BigInteger.ModPow(m, e, n);
            //Console.WriteLine("\nШифр числа 'm' закрытым ключом Алисы(e,n) = " + code);

            // Расшифруем
            // Найдем сначала константу 'd' - закрытый ключ Боба
            //long fn = (p - 1) * (q - 1);
            //long d = НайтиОбратныйПоМодулю(e, fn);
            //if (d < 0) d = fn + d;
            //decoded = (long)BigInteger.ModPow(code, d, n);
            //Console.WriteLine("Расшифровка числа 'm' закрытым ключом Боба(d,n) = " + decoded);

            //// Сравним и выведем результат
            //if (decoded == m)
            //    Console.WriteLine("\nЧисла после шифровки и расшифровки совпали!");
            //else
            //{
            //    Console.WriteLine("\nНеверная расшифровка!");
            //}
        }


        static void Main(string[] args)
        {
            // 2 простых числа в диапазоне от 1 до 10 млн
            long p=0, q=0;

            // Ищем первое простое число
            p = ПолучитьПростоеЧисло();
            // Ищем второе простое число
            q = p;
            while(q==p)
                q = ПолучитьПростоеЧисло();

            // Выведем полученный результат - 2 простых числа. Если они действительно простые, то всё должно сработать
            Console.WriteLine("ПРОСТЫЕ ЧИСЛА: 'p' = "+p+"; 'q'="+q);

            long code=0;
            long decoded=0;

            ШифрRSA(p,q,ref code, ref decoded);
            
            // ЧТобы экран не гас быстро
            Console.ReadKey();
        }

    }
}
