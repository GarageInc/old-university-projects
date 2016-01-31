using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;



namespace LSD
{
    class Program
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        //Задаем рандомный массив

        static int[] RandomMass(int n)
        {
            int[] mass = new int[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = r.Next(50)-r.Next(50);
            }

            return mass;
        }


        //Печать массива
        static void PrintMass(int[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                Console.Write(mass[i] + " ");
            }
            Console.WriteLine();
        }



        static void LSD(int[] mass)
        {
            //Массив адресов
            int[] adr = new int[mass.Length];
            //Найди максимум разрядов
            int r=KolRazr(Max(mass));

            List<int>[] lists = new List<int>[20];
            //Инициализируем
            for (int i = 0; i < 20; i++)
                lists[i] = new List<int>();

            //Идем по рязрядам, начиная с младшего
            for (int i = 0; i < r; i++)
            {
                int razrjad;
                //Идем по старому массиву
                for (int j = 0; j < mass.Length; j++)
                {
                    razrjad = mass[j];//Берем элемент
                    //Вычисляем разряд
                    for (int y = 0; y < i; y++)
                    {
                        razrjad = razrjad / 10;//Отрезаем с конца десятичный кусок
                    }
                    razrjad = Math.Abs(razrjad % 10);
                    //Добавляем в список по индексам
                    if (mass[j] >= 0)
                        lists[razrjad].Add(mass[j]);
                    else
                        lists[razrjad].Insert(0,mass[j]);
                }
                //Слияние в прежний массив
                int kol = 0;
                for (int y = 0; y < 20; y++)
                {
                    foreach (int value in lists[y])
                    {
                        mass[kol++] = value;
                    }
                }
                //Очищаем списки
                for (int y = 0; y < 20; y++)
                {
                    lists[y].Clear();
                }
            }
        }

        //Максимальное число в массиве
        static int Max(int[] mass)
        {
            int max = mass[0];
            for (int i = 1; i < mass.Length; i++)
            {
                if (mass[i] > max) max = mass[i];
            }
            return max;
        }
        //Количество разрядов
        static int KolRazr(int x)
        {
            int kol = 0;
            while (x != 0)
            {
                x = x / 10;
                kol++;
            }
            return kol;
        }


        static void Main(string[] args)
        {
            int n = 15;
            int[] mass = RandomMass(n);
            PrintMass(mass);

            LSD(mass);

            PrintMass(mass);
            Console.ReadKey();
        }
    }
}
