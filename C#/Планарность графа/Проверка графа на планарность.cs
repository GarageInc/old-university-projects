using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication2
{
    class Program
    {
        public static int[,] Create(int n) //Создаем граф с Н вершинами , задаем матрицей смежности размером Н на Н,заполняем матрицу нулями , а по главной диагонали -1 
        {
            int[,] a = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        a[i, j] = -1;
                    else
                        a[i, j] = 0;
                }
            return a;
        }
        public static void random1(int[,] a, int n) // Справа от главной диагонали в трегольной матрице генерируем числа ноль или один
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    a[i, j] = rnd.Next(0, 2);
        }
        public static void Prov(int[,] a, int n, int k)// Проверяем есть ли в трегольной матрице справа , как минимум одна единица в строке, если нет то генерируем единицу в этой строке
        {
            int kol = 0;
            for (int i = k + 1; i < n; i++)
                if (a[k, i] != 0)
                    kol++;
            if (kol == 0)
            {
                Random rnt = new Random();
                int p = rnt.Next(k + 1, n);
                a[k, p] = 1;
            }

        }
        public static void Prov2(int[,] a, int n, int k)// Проверяем есть ли в треугольной матрице справа, как минимум одна единица в столбце, если нет то генерируем единицу в этом столбце
        {
            int kol = 0;
            for (int i = 0; i < k; i++)
                if (a[i, k] != 0)
                    kol++;
            if (kol == 0)
            {
                Random rnt = new Random();
                int p = rnt.Next(0, k);
                a[p, k] = 1;
            }

        }
        public static void randomen(int[,] a, int n)// Использвуем две проверки , описанные выше
        {
            random1(a, n);
            for (int i = 0; i < n - 1; i++)
            {
                Prov(a, n, i);
            }
            for (int j = 1; j < n; j++)
                Prov2(a, n, j);
        }
        public static void duplicate(int[,] a, int n)// Отображаем правую трегольную матрицу влево от главной диагонали
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (a[i, j] == 1)
                        a[j, i] = 1;
        }
        public static void RandomAll(int[,] a, int n)// Там где единицы в треугольной матрице генерируем случайные числа, это путь из вершины i в вершину j. Сгенерированный граф готов
        {
            randomen(a, n);
            duplicate(a, n);
            Random rnd2 = new Random();
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (a[i, j] == 1)
                    {
                        a[i, j] = rnd2.Next(1, 20);
                        a[j, i] = a[i, j];
                    }

                }


        }
        public static bool k5(int[,] a, int k1,int k2)// Проверка подграфа на граф k5 .к1 - Индекс строки, к2 - индекс столбца
        {
            bool b = false;
            int kol1=0;
            int kol2=0;
            for (int i = k1; i < (k1+5); i++)
            {
                kol1 = 0;
                for (int j = k2; j <(k2+5); j++)
                    if (a[i, j] != 0 && a[i, j] != -1)
                        kol1++;
                if (kol1 == 4)
                    kol2++;
            }
            if (kol2 == 5)
                b = true;
            return b;

        }
        public static bool k33(int[,] a, int k1, int k2)// Проверка подграфа на граф k33 .к1 - Индекс строки, к2 - индекс столбца
        {
            bool b = false;
            int kol1 = 0;
            int kol2 = 0;
            for (int i = k1; i < (k1 + 6); i++)
            {
                kol1 = 0;
                for (int j = k2; j < (k2 + 6); j++)
                    if (a[i, j] != 0 && a[i, j] != -1)
                        kol1++;
                if (kol1 == 3)
                    kol2++;
            }
            if (kol2 == 6)
                b = true;
            return b;

        }
        public static bool k5full(int[,] a, int n)// Полная проверка всевозможных подграфов ,сгенерированного графа на к5 
        {
            bool b=false;
            for (int i = 0; i < (n - 5); i++)
            {
                for (int j = 0; j < (n - 5); j++)

                    if (k5(a, i, j) == true)
                    {

                        b = true;
                        break;
                    }
                if (b == true)
                    break;
            }
            return b;
        }
        public static bool k33full(int[,] a, int n)// Полная проверка всевозможных подграфов ,сгенерированного графа на к33
        {
            bool b = false;
            for (int i = 0; i < (n - 5); i++)
            {
                for (int j = 0; j < (n - 5); j++)
                    if (k33(a, i, j) == true)
                    {
                        b = true;
                        break;
                    }
                if (b == true)
                    break;
            }
            return b;
        }
        public static void Planar(int[,] a, int n)// Проверка на принадлежность к к33 и к5. Если принадлежит хоть одному из типов, граф не планарный
        {
            if (k5full(a, n) == true || k33full(a, n) == true)
                Console.WriteLine("Граф не является планарным");
            else
                Console.WriteLine("Граф  является планарным");
        }
        static void Main(string[] args)
        {
            int n = 200;
            int[,] a;
            a = Create(n);
            RandomAll(a, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(a[i, j] + "   ");
                Console.WriteLine();
            }
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Planar(a, n);
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds );
            Console.WriteLine("Время проверки на планарность равно" + elapsedTime);
            Console.ReadLine();
        }
    }
}
