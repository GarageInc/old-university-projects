using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Сортировка пузырьком
namespace Sorts
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
                mass[i] = r.Next(10);
            }

            return mass;
        }
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        static int[] HardMass(int n)
        {
            int[] mass = new int[n];
            mass = RandomMass(n);
            //Сначала сортируем
            //Потом переворачиваем
            Array.Sort<int>(mass);
            Array.Reverse(mass);

            return mass;
        }

        //Функция обмена
        static void Swap(int[] mass, int i, int j)
        {
            int temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }

        //Сортировка вставками
        static void Insert(int[] mass)
        {
            int i, j;
            int temp;
            for (i = 1; i < mass.Length; i++)
            {
                temp = mass[i];
                for (j = i - 1; j >= 0; j--)
                {
                    if (mass[j].CompareTo(temp) < 0)
                        break;

                    mass[j + 1] = mass[j];
                    mass[j] = temp;
                }
            }
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

            Insert(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }
 
        //Визуализация сортировки
        static void Visualization()
        {
            int n;//Количество элементов
            int m = 50;//Базовое количество элементов
            int y = 2;//Количество созданий массивов(количество проверок)
            double sumTime_1 = 0, sumTime_2 = 0, sumTime_3 = 0;

            //Для старой версии - если массив уже упорядочен - сортировка все равно идет до конца
            //50, 500, 5000, 50000
            for (int i = 0; i < 4; i++)
            {
                double time_1 = 0, time_2 = 0, time_3;
                n = m * (int)Math.Pow(10, i);
                for (int j = 0; j < y; j++)
                {

                    int[] mass_1 = RandomMass(n);
                    int[] mass_2 = (int[])mass_1.Clone();//Копия
                    int[] mass_3 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_3 = TimeOfSort(mass_3);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_3 = sumTime_3 + time_3;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;
                sumTime_3 = sumTime_3 / y;

                Console.WriteLine(n + " elements time: " + "SIMPLE = " + sumTime_1 + " HARD = " + sumTime_3);
            }
        }

        static void Main(string[] args)
        {
            
            Visualization();
       
            Console.ReadKey();
        }
    }
}
