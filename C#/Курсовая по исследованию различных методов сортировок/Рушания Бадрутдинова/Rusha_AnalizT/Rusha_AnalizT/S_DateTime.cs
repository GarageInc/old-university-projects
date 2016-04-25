using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Rusha_AnalizT
{
    class S_DateTime
    {
        //Быстрая сортировка
        public static  void QS(DateTime[] s_arr, int start, int end)
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
        public static  double TimeOfQuickSort(DateTime[] mass)
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
        public static  void Push(DateTime[] a, int i, int last)
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

        public static  void HeapSort(DateTime[] mass)
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
        public static  double TimeOfHeapSort(DateTime[] mass)
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
        public static  void ShellSort(DateTime[] mass)
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
        public static  double TimeOfShellSort(DateTime[] mass)
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
        public static  DateTime[] MergeSort(DateTime[] massive)
        {
            if (massive.Length == 1)
                return massive;
            int mid_point = massive.Length / 2;
            return Merge(MergeSort(massive.Take(mid_point).ToArray()), MergeSort(massive.Skip(mid_point).ToArray()));
        }
        //Слияние
        public static  DateTime[] Merge(DateTime[] mass1, DateTime[] mass2)
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
        public static  double TimeOfMergeSort(DateTime[] mass)
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
        public static  void BubleSort(DateTime[] mass)
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
        public static  double TimeOfBubleSort(DateTime[] mass)
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
        public static  void SelectionSort(DateTime[] arr)
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
        public static  double TimeOfSelectionSort(DateTime[] mass)
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
        public static  void Insert(DateTime[] mass)
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
        public static  double TimeOfInsertSort(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            Insert(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //_________________________________________________________________________________

        public static  Random r = new Random(DateTime.Now.Millisecond);
        //Задаем рандомный массив
        public static  DateTime[] RandomMass(int n)
        {
            DateTime[] mass = new DateTime[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = Date();//Дата
            }

            return mass;
        }

        //Формируем дату определенного размера
        public static  DateTime Date()
        {
            DateTime start = new DateTime();
            start = DateTime.Now;
            int range = 2013 * 365;
            int c = r.Next(0, range);
            return start.AddDays((-1) * r.Next(0, range));
        }
        //Рандомное формирование слов для массива
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        public static  DateTime[] HardMass(int n)
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
        public static  void Swap(DateTime[] mass, int i, int j)
        {
            DateTime temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }
        //Печать массива
        public static  void PrintMass(DateTime[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i].ToShortDateString() + " ");
            }
            Console.WriteLine();
        }
    }
}
