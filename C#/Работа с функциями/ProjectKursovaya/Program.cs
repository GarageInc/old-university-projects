using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKursovaya
{
    class Program
    {
        // Основная функция под номером "З"
        static double MyFunction(double x, double a)
        {
            if (a < 0)// Основной критерий - позиция числа 'a' относительно нуля, в зависимости от этого и возвращаем результат
            {
                return Math.Pow(Math.Sin(a + x),2);//Math.Pow(x,y) - функция, которая возводит число 'x' в степень числа 'y' 
            }
            else
            {
                return Math.Sin(1 + a * x + Math.Pow(x, 2));//Math.Sin(число) - функция, которая берет синус числа
            }
        }

        static void GetResult(double b, double h, int n, double[] a)
        {

            // Создадим точки, в которых ищем значения функции. По условию каждое Xj=b+j*h, где 'j' от 1 до 'n'
            double[] x = new double[n];
            for (int j = 0; j < n; j++)
            {
                x[j] = b + (j + 1) * n;// j+1, т.к. сдвиг от нуля на единицу должен быть
            }

            // Считаем каждую серию(их у нас три - по количеству чисел 'a')
            for (int i = 0; i < a.Length; i++)
            {
                // Создадим наш массив 'y', куда будем записывать результаты
                double[] y = new double[n];

                for (int j = 0; j < n; j++)
                {
                    // Считаем результат по серии для заданных параметров 'x' и 'a'
                    y[j] = MyFunction(x[j], a[i]);
                }
                // Теперь, получив результаты, нужно рассчитать значение двух функций
                // 'f', 'g', где 'f'- равна произведению всех Yj, 'g'- равна модулю суммы всех Yj
                double f = 1, g = 0;// f=1, т.к. первоначально нужно умножать, g=0, т.к. нужно суммировать
                for (int j = 0; j < n; j++)
                {
                    f = f * y[j];
                    g = g + y[j];
                }
                g = Math.Abs(g);// Математическая функция, которая возвращает модуль от переданного числа

                // Выводим результат
                Console.WriteLine("Результаты в итерации №" + (i + 1) + " равны:");
                Console.WriteLine("      f=" + f + ";   g=" + g);
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            string vvod = "";
            // Вводимые значения переменных
            double h = 0.556;
            double b = -2;
            int n = 7;
            double[] a = new double[3];
            a[0] = -5;
            a[1] = 21.4;
            a[2] = 6;

            // Выводим значимую информацию и спрашиваем - какие данные использовать
            Console.WriteLine(string.Format("Значение переменных равны по умолчанию h={0}, b={1}, n={2}, a1={3}, a2={4}, a3={5}",h,b,n,a[0],a[1],a[2]));
            Console.WriteLine("Вы хотите ввести новые переменные(значения) или продолжить работу?(0=нет/1=да)\n");// \n - знак перехода на новую строку
            vvod=Console.ReadLine();
            
            // Цикл, который прервется только тогда, когда пользователь введет "да", "1", "нет", "0"
            while (true)
            {
                // Если пользователь ввел 1 - это значит, что он хочет ввести новые значения переменных
                if (vvod == "1" || vvod == "да")
                {
                    Console.WriteLine("Введите значение 'h'=");
                    h = double.Parse(Console.ReadLine());

                    Console.WriteLine("Введите значение 'b'=");
                    b = double.Parse(Console.ReadLine());

                    Console.WriteLine("Введите значение 'n'=");
                    h = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите значение 'a1'=");
                    a[0] = double.Parse(Console.ReadLine());

                    Console.WriteLine("Введите значение 'a2'=");
                    a[1] = double.Parse(Console.ReadLine());

                    Console.WriteLine("Введите значение 'a2'=");
                    a[2] = double.Parse(Console.ReadLine());

                    Console.WriteLine();
                    break;// Выходим из цикла
                }
                else
                {
                    if (vvod == "0" || vvod == "нет")
                    {
                        break;// Выходим из цикла
                    }
                    else// Если же пользователь ввел не то, что нам нужно - снова спрашиваем
                    {
                        Console.WriteLine("Вы хотите ввести новые переменные(значения) или продолжить работу?(0=нет/1=да)\n");
                        vvod = Console.ReadLine();
                    }
                }
            }

            // Запускаем основную функцию, в которой происходят подсчеты(передаем введенные или измененные параметры)
            GetResult(b, h, n, a);


            // Чтобы консоль не закрывалась
            Console.ReadKey();
        }
    }
}
