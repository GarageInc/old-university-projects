using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Сортировка пузырьком
namespace Sorts
{           
    class Program
    {
        //Гибридная сортировка
        static void GybridSort(ref int[] mass, int start, int end, ref int twoLogn)
        {
            //Если длина меньше/равно 16 - то сортировка пузырьком.
            if (end - start <= 16)
            {
                for (int i = start; i < end; i++)
                {
                    for (int j = start; j < end-i; j++)
                    {
                        if (mass[j].CompareTo(mass[j + 1]) > 0)
                        {
                            Swap(mass, j, j + 1);
                        }
                    }
                }
            }
            else
            {
                //Проводим Быструю сортировку, до глубины 2*log(n)
                //А затем - сортировку деревом
                if (twoLogn < 2 * Math.Log(mass.Length, 2))
                {
                    int i = start, j = end;
                    //Опорный элемент:
                    int x = mass[(start + end) / 2];

                    do
                    {
                        //Двигаем два курсора
                        while (mass[i].CompareTo(x) < 0) i++;
                        while (mass[j].CompareTo(x) > 0) j--;

                        if (i <= j)
                        {
                            if (i < j) Swap(mass, i, j);
                            i++;
                            j--;
                        }
                    } while (i <= j);

                    twoLogn++;
                    if (i < end)
                        GybridSort(ref mass, i, end, ref twoLogn); ;
                    if (start < j)
                        GybridSort(ref mass, start, j, ref twoLogn);
                }
                else
                {
                    //Дерево
                    for (int i = end - start / 2; i >= start + 16; i--)
                    {
                        Push(mass, i, end - 1);
                    }
                    for (int i = end-start - 1; i > start+16; i--)
                    {
                        Swap(mass, start, i);
                        Push(mass, start, i - 1);
                    }
                    //"Добиваем" через сортировку пузырьком
                    for (int i = start; i < start+16; i++)
                    {
                        for (int j = start; j < start+16-i; j++)
                        {
                            if (mass[j].CompareTo(mass[j + 1]) > 0)
                            {
                                Swap(mass, j, j + 1);
                            }
                        }
                    }
                }
            }
        }

        //Меняет местами
        static void Push(int[] a, int i, int last)
        {
            int k = i;
            if ((2 * i + 1) <= last)
            {
                if ((a[2 * i + 1] > a[i]))

                    k = 2 * i + 1;
                if ((2 * i + 2) <= last && a[2 * i + 2] > a[k])
                    k = 2 * i + 2;
                if (k > i)
                {
                    Swap(a, i, k);
                    Push(a, k, last);
                }
            }
        }

        static Random r = new Random(DateTime.Now.Millisecond);
        //Задаем рандомный массив
        static int[] RandomMass(int n)
        {
            int[] mass = new int[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = r.Next(1000000);
            }

            return mass;
        }
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        static int[] HardMass(int n)
        {
            int[] mass = new int[n];
            mass = RandomMass(n);
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length - 1; j++)
                {
                    if (mass[j].CompareTo(mass[j + 1]) < 0)
                    {
                        Swap(mass, j, j + 1);
                    }
                }
            }
            return mass;
        }
        //Функция обмена
        static void Swap(int[] mass, int i, int j)
        {
            int temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }

        //Быстрая сортировка
        static void QS(int[] s_arr, int start, int end)
        {
            int i = start, j = end;
            int x = s_arr[(start + end) / 2];

            do
            {
                while (s_arr[i].CompareTo(x) < 0) i++;
                while (s_arr[j].CompareTo(x) > 0) j--;

                if (i <= j)
                {
                    if (i < j) Swap(s_arr, i, j);
                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < end)
                QS(s_arr, i, end);
            if (start < j)
                QS(s_arr, start, j);
        }
        //Печать массива
        static void PrintMass(int[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i] + " ");
            }
            Console.WriteLine();
        }
        //Рассчет времени сортировки для рандомного массива
        static double TimeOfSort(int[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            QS(mass,0,mass.Length-1);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //Визуализация сортировки
        static void Visualization()
        {
            int n;//Количество элементов
            int m = 50;//Базовое количество элементов
            int y = 3;//Количество созданий массивов(количество проверок)
            double sumTime_1 = 0, sumTime_2 = 0;

            //Для старой версии - если массив уже упорядочен - сортировка все равно идет до конца
            //50, 500, 5000, 50000
            for (int i = 0; i < 4; i++)
            {
                double time_1 = 0, time_2 = 0;
                n = m * (int)Math.Pow(10, i);
                for (int j = 0; j < y; j++)
                {

                    int[] mass_1 = RandomMass(n);
                    int[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " elements time: " + "SIMPLE = " + sumTime_1 + " HARD = " + sumTime_2);
            }
        }

        static void Main(string[] args)
        {
            
            Visualization();
            /*
            int n = 100000;
            int[] mass = RandomMass(n);
            Console.WriteLine(TimeOfSort(mass));

            for (int i = 0; i < n-1; i++)
            {
                if (mass[i].CompareTo(mass[i + 1]) > 0)
                    Console.WriteLine(i + " ");

            }
            */

            Console.ReadKey();
        }
    }
}