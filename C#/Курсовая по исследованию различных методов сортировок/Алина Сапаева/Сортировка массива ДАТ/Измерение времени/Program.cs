using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

//Сортировка пузырьком
namespace Sorts
{
    class Program
    {
        //Быстрая сортировка
        static void QS(DateTime[] s_arr, int start, int end)
        {
            int i = start, j = end;
            DateTime x = s_arr[(start + end) / 2];

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

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfQuickSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            QS(mass, 0, mass.Length - 1);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________
        //Сортировка деревом-пирамидкой
        //Меняет местами
        static void Push(DateTime[] a, int i, int last)
        {
            int k = i;
            if ((2 * i + 1) <= last)
            {
                if (a[2 * i + 1].CompareTo(a[i]) > 0)

                    k = 2 * i + 1;
                if ((2 * i + 2) <= last && a[2 * i + 2].CompareTo(a[k]) > 0)
                    k = 2 * i + 2;
                if (k > i)
                {
                    Swap(a, i, k);
                    Push(a, k, last);
                }
            }
        }

        static void HeapSort(DateTime[] mass)
        {
            //Дерево
            for (int i = mass.Length / 2; i >= 0; i--)
            {
                Push(mass, i, mass.Length - 1);
            }

            for (int i = mass.Length - 1; i > 0; i--)
            {
                Swap(mass, 0, i);
                Push(mass, 0, i - 1);
            }

        }

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfHeapSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            HeapSort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________
        //Сортировка Шелла
        static void ShellSort(DateTime[] mass)
        {
            int i, j, d;
            for (d = mass.Length / 2; d != 0; d /= 2)
            {
                for (i = d; i < mass.Length; i++)
                {
                    for (j = i; j >= d; j -= d)
                    {
                        if (mass[j - d].CompareTo(mass[j]) > 0)
                        {
                            Swap(mass, j, j - d);
                        }
                    }
                }
            }
        }

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfShellSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            ShellSort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________
        //Сортировка слиянием
        static DateTime[] MergeSort(DateTime[] massive)
        {
            if (massive.Length == 1)
                return massive;
            int mid_point = massive.Length / 2;
            return Merge(MergeSort(massive.Take(mid_point).ToArray()), MergeSort(massive.Skip(mid_point).ToArray()));
        }
        //Слияние
        static DateTime[] Merge(DateTime[] mass1, DateTime[] mass2)
        {
            Int32 a = 0, b = 0;
            DateTime[] merged = new DateTime[mass1.Length + mass2.Length];
            for (Int32 i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                    if (mass1[a].CompareTo(mass2[b]) > 0)
                        merged[i] = mass2[b++];
                    else //if int go for
                        merged[i] = mass1[a++];
                else
                    if (b.CompareTo(mass2.Length) < 0)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
            }
            return merged;
        }

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfMergeSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            mass = MergeSort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________
        //Сортировка пузырьком - старая версия, не оптимизированная
        static void BubleSort(DateTime[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length - 1; j++)
                {
                    if (mass[j].CompareTo(mass[j + 1]) > 0)
                    {
                        Swap(mass, j, j + 1);
                    }
                }
            }
        }

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfBubleSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            BubleSort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________
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
                    if (arr[j].CompareTo(arr[min]) < 0)
                    {
                        min = j;
                    }
                }

                temp = arr[i];
                arr[i] = arr[min];
                arr[min] = temp;
            }
        }

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfSelectionSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            SelectionSort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________
        //Сортировка вставками
        static void Insert(DateTime[] mass)
        {
            int i, j;
            DateTime temp;
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

        //Рассчет времени сортировки для рандомного массива
        static double TimeOfInsertSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            Insert(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________

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
        //Печать массива
        static void PrintMass(DateTime[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i].ToShortDateString() + " ");
            }
            Console.WriteLine();
        }
        
        

        //Вывод времени сортировок
        static void SortingOfArrays()
        {
            int n;//Количество элементов
            int m = 50;//Базовое количество элементов
            int y = 2;//Количество созданий массивов(количество проверок)
            double sumTime_1 = 0, sumTime_2 = 0;
            double time_1, time_2;
            //Сортировки
            //50, 500, 5000, 50000
            Console.WriteLine("Сортировка Пузырьком:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);
            
                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfBubleSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfBubleSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " эл-ов: " + "ПРОСТОЙ_МАССИВ = " + sumTime_1 + " ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
            }

            Console.WriteLine();
            Console.WriteLine("Сортировка Выбором:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);

                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfSelectionSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfSelectionSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine("{0,-40},  {1,-40}, {2,-40} ", n + "эл-ов:", "ПРОСТОЙ_МАССИВ = " + sumTime_1, "ОБРАТНО_СОРТИРОВАННЫЙ = "+sumTime_2);
            }

            Console.WriteLine();
            Console.WriteLine("Сортировка Вставками:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);

                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfInsertSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfInsertSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " эл-ов: " + "ПРОСТОЙ_МАССИВ = " + sumTime_1 + " ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
            }

            Console.WriteLine();
            Console.WriteLine("Сортировка Слиянием:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);

                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfMergeSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfMergeSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " эл-ов: " + "ПРОСТОЙ_МАССИВ = " + sumTime_1 + " ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
            }

            Console.WriteLine();
            Console.WriteLine("Сортировка Шелла:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);

                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfShellSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfShellSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " эл-ов: " + "ПРОСТОЙ_МАССИВ = " + sumTime_1 + " ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
            }
            Console.WriteLine();
            Console.WriteLine("Сортировка Быстрая:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);

                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfQuickSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfQuickSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " эл-ов: " + "ПРОСТОЙ_МАССИВ = " + sumTime_1 + " ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
            }
            Console.WriteLine();
            Console.WriteLine("Сортировка Деревом-пирамидкой:");
            for (int i = 0; i < 4; i++)
            {
                time_1 = 0; time_2 = 0;
                sumTime_1 = 0; sumTime_2 = 0;

                n = m * (int)Math.Pow(10, i);

                for (int j = 0; j < y; j++)
                {
                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfHeapSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfHeapSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " эл-ов: " + "ПРОСТОЙ_МАССИВ = " + sumTime_1 + " ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
            }
        }



        static void Main(String[] args)
        {
            SortingOfArrays();

            Console.ReadKey();
        }
    }
}
