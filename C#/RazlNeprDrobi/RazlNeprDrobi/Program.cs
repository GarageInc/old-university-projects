using System;
using System.Numerics;


namespace RazlNeprDrobi
{
    public static class BigIntegerExtension
    {
        public static BigInteger Sqrt(this BigInteger bInt)
        {
            if (bInt.IsZero)
                return 0;
            if (bInt == 1)
                return 1;
            bool flag = false;
            BigInteger x = BigInteger.Divide(bInt, 2);
            BigInteger y = 0;

            while (true)
            {
                y = (x + BigInteger.Divide(bInt, x)) / 2;
                if (x == y || y < x && flag)
                    return y;
                else
                    flag = y > x;
                x = y;
            }
        }
    }

    class Programк
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите искомое число: ");
            BigInteger n = BigInteger.Parse(Console.ReadLine());
            
            //Используем одноименные массивы для поиска.
            BigInteger B = new BigInteger(0);
            BigInteger[] P = new BigInteger[1000000];
            BigInteger[] Q = new BigInteger[1000000];
            BigInteger[] r = new BigInteger[1000000];
            BigInteger[] c = new BigInteger[1000000];
            BigInteger[] d = new BigInteger[1000000];
            BigInteger[] S = new BigInteger[1000000];

           
            //Проводим первичные вычисления
            P[0] = 0;
            Q[0] = 1;
            //r[0] = (BigInteger)Math.Sqrt((int)n);
            r[0] =BigIntegerExtension.Sqrt(n);
            c[0] = r[0];
            d[0] = 1;

            P[1] = BigIntegerExtension.Sqrt(n);
            Q[1] = n - BigInteger.Pow(r[0], 2);
            r[1] = (BigInteger)((r[0] + P[1]) / Q[1]);
            c[1] = 1 + r[0] * r[1];
            d[1] = r[1];

            int k=0;;
            
            if (n > 2)//Проверяем верный ввод числа
            {
                //На всякий случай, если будет деление на ноль, либо бесконечный перебор
                for (k = 2; k < 1000000; k++)
                {
                l1:
                    P[k] = r[k - 1] * Q[k - 1] - P[k - 1];
                    Q[k] = Q[k - 2] + r[k - 1] * (P[k - 1] - P[k]);
                    r[k] = (BigInteger)((r[0] + P[k]) / Q[k]);
                    c[k] = r[k] * c[k - 1] + c[k - 2];
                    d[k] = r[k] * d[k - 1] + d[k - 2];

                    S[k] = BigInteger.Pow(c[k], 2) - BigInteger.Pow(d[k], 2) * n;

                    if (S[k] >= 2)//Является ли наше число квадратом другого
                    {
                        
                        //double a = Math.Sqrt((int)S[k]);
                        BigInteger a=new BigInteger();
                        a=BigIntegerExtension.Sqrt(S[k]);
                        if (S[k] == BigInteger.Pow(a,2))
                        {
                            B = (BigInteger)a;
                            
                            //Находим НОД.
                            Console.WriteLine();
                            Console.Write("i");
                            Console.Write("     P");
                            Console.Write("     Q");
                            Console.Write("     r");
                            Console.Write("      c");
                            Console.Write("      d");
                            Console.Write("          S");
                            Console.WriteLine();
                            for (int i = 0; i < k + 1; i++)//Строим таблицу.
                            {
                                Console.Write("{0:D2} ", i);
                                Console.Write("{0:D5} ", P[i]);
                                Console.Write("{0:D5} ", Q[i]);
                                Console.Write("{0:D5} ", r[i]);
                                Console.Write("{0:D6} ", c[i]);
                                Console.Write("{0:D6} ", d[i]);
                                Console.Write("{0:D10} ", S[i]);
                                Console.WriteLine();

                            }

                            Console.WriteLine("B= " + B);
                            //Проверяем - не единица ли у нас?
                            //Теперь найдем НОД

                            BigInteger p = BigInteger.GreatestCommonDivisor(n, c[k] - B);
                            BigInteger q = BigInteger.GreatestCommonDivisor(n, c[k] + B);

                            if (p != 1 && q != 1)
                            {
                                Console.WriteLine("Искомые множители:  " + p + " и  " + q);
                                break;
                            }
                            else
                            {
                                if (k < 100000)
                                {
                                    goto l1;
                                }
                                else
                                {
                                    Console.WriteLine("Вы ввели простое число!");
                                }
                            }
                        }
                    }
                }
            }
            else Console.WriteLine("Неверный ввод числа! Введите число >=2:");

            Console.ReadKey();
        }
    }
}
