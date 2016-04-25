using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Rusha_AnalizT
{
    class S_String
    {
        //Быстрая сортировка
        public static  void QS(string[] s_arr, int start, int end)
        {
            int i = start, j = end;
            string x = s_arr[(start + end) / 2];

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
        public static  double TimeOfQuickSort(string[] mass)
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
        public static  void Push(string[] a, int i, int last)
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

        public static  void HeapSort(string[] mass)
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
        public static  double TimeOfHeapSort(string[] mass)
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
        public static  void ShellSort(string[] mass)
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
        public static  double TimeOfShellSort(string[] mass)
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
        public static  string[] MergeSort(string[] massive)
        {
            if (massive.Length == 1)
                return massive;
            int mid_point = massive.Length / 2;
            return Merge(MergeSort(massive.Take(mid_point).ToArray()), MergeSort(massive.Skip(mid_point).ToArray()));
        }
        //Слияние
        public static  string[] Merge(string[] mr1, string[] mass2)
        {
            Int32 a = 0, b = 0;
            string[] merged = new string[mr1.Length + mass2.Length];
            for (Int32 i = 0; i < mr1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mr1.Length)
                    if (mr1[a].CompareTo(mass2[b]) > 0)
                        merged[i] = mass2[b++];
                    else //if int go for
                        merged[i] = mr1[a++];
                else
                    if (b.CompareTo(mass2.Length) < 0)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mr1[a++];
            }
            return merged;
        }

        //Рассчет времени сортировки для рандомного массива
        public static  double TimeOfMergeSort(string[] mass)
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
        public static  void BubleSort(string[] mass)
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
        public static  double TimeOfBubleSort(string[] mass)
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
        public static  void SelectionSort(string[] arr)
        {
            int min;
            string temp;
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
        public static  double TimeOfSelectionSort(string[] mass)
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
        public static  void Insert(string[] mass)
        {
            int i, j;
            string temp;
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
        public static  double TimeOfInsertSort(string[] mass)
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
        public static  string[] RandomMass(int n)
        {
            string[] mass = new string[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = Word(4);//Слово с длинной в 4 символов
            }

            return mass;
        }

        //Формируем слово определенного размера
        public static  string Word(int n)
        {
            StringBuilder builder = new StringBuilder();
            char ch;//Используется для генерации буквы
            for (int i = 0; i < n; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32((r.Next(26) + 65)));
                //26 - слов в английском алфавите, 65 - код символа А
                if (r.Next(2) % 2 == 1)//Рандомный выбор - маленькие или большие буквы
                    builder.Append(ch.ToString().ToLower());
                builder.Append(ch.ToString().ToUpper());
            }
            return builder.ToString();
        }
        //Рандомное формирование слов для массива
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        public static  string[] HardMass(int n)
        {
            string[] mass = new string[n];
            mass = RandomMass(n);
            //Сначала сортируем
            //Потом переворачиваем
            Array.Sort<string>(mass);
            Array.Reverse(mass);

            return mass;
        }
        //Функция обмена
        public static  void Swap(string[] mass, int i, int j)
        {
            string temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }


        //Печать массива
        public static  void PrintMass(string[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i] + " ");
            }
            Console.WriteLine();
        }

    }
}
