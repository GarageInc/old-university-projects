using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Сортировка пузырьком
namespace Sorts
{
    //using Integer = System.Int32; 

    class Program
    {
        
        static Random r = new Random(DateTime.Now.Millisecond);
        //Задаем рандомный массив
        static DateTime[] RandomMass(int n)
        {
            DateTime[] mass = new DateTime[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = Date();//Дата
            }

            return mass;
        }

        //Формируем дату определенного размера
        static DateTime Date()
        {
            DateTime start = new DateTime();
            start=DateTime.Now;
            int range = 2013 * 365;
            int c = r.Next(0, range);
            return start.AddDays((-1)*r.Next(0,range));
        }
        //Рандомное формирование слов для массива
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        static DateTime[] HardMass(int n)
        {
            DateTime[] mass = new DateTime[n];
            mass = RandomMass(n);
            //Сначала сортируем
            //Потом переворачиваем
            Array.Sort<DateTime>(mass);
            Array.Reverse(mass);

            return mass;
        }
        //Функция обмена
        static void Swap(DateTime[] mass, int i, int j)
        {
            DateTime temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }
        //Сортировка выбором
        static void SelectionSort(DateTime[] arr)
        {
            int min;
            DateTime temp;
            int length = arr.Length;

            for (int i = 0; i < length - 1; i++)
            {
                min = i;

                for (int j = i + 1; j < length; j++)
                {
                    if (arr[j].CompareTo(arr[min])<0)
                    {
                        min = j;
                    }
                }

                temp = arr[i];
                arr[i] = arr[min];
                arr[min] = temp;
            }
        }

        //Печать массива
        static void PrintMass(DateTime[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i].ToShortDateString() + " ");
            }
            Console.WriteLine();
        }
        //Рассчет времени сортировки для рандомного массива
        static double TimeOfSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            SelectionSort(mass);

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

                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_3 = HardMass(n);//Массив из полностью обратной сортировки элементов

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

        static void Main(String[] args)
        {
            Visualization();
            /*
            DateTime[] mass_1 = RandomMass(10);
            PrintMass(mass_1,10);
            SelectionSort(mass_1);
            PrintMass(mass_1,10);
            */


            /*
        DateTime[] mass_1 = RandomMass(15);

        PrintMass(mass_1, 15);
        BubleSort(mass_1);
        Console.WriteLine();
        PrintMass(mass_1, 15);
         * */
            Console.ReadKey();
        }
    }
}
