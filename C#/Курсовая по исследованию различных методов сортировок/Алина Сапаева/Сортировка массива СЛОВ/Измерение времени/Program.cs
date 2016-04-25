using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Сортировка пузырьком
namespace Sorts
{
    class Program
    {
        //Быстрая сортировка
        static void QS(string[] s_arr, int start, int end)
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
        static double TimeOfQuickSort(string[] mass)
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
        static void Push(string[] a, int i, int last)
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

        static void HeapSort(string[] mass)
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
        static double TimeOfHeapSort(string[] mass)
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
        static void ShellSort(string[] mass)
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
        static double TimeOfShellSort(string[] mass)
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
        static string[] MergeSort(string[] massive)
        {
            if (massive.Length == 1)
                return massive;
            int mid_point = massive.Length / 2;
            return Merge(MergeSort(massive.Take(mid_point).ToArray()), MergeSort(massive.Skip(mid_point).ToArray()));
        }
        //Слияние
        static string[] Merge(string[] mass1, string[] mass2)
        {
            Int32 a = 0, b = 0;
            string[] merged = new string[mass1.Length + mass2.Length];
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
        static double TimeOfMergeSort(string[] mass)
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
        static void BubleSort(string[] mass)
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
        static double TimeOfBubleSort(string[] mass)
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
        static void SelectionSort(string[] arr)
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
        static double TimeOfSelectionSort(string[] mass)
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
        static void Insert(string[] mass)
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
        static double TimeOfInsertSort(string[] mass)
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
        static string[] RandomMass(int n)
        {
            string[] mass = new string[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = Word(6);//Слово с длинной в 10 символов
            }

            return mass;
        }

        //Формируем слово определенного размера
        static string Word(int n)
        {
            StringBuilder builder = new StringBuilder();
            char ch;//Используется для генерации буквы
            for (int i = 0; i < n; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32((r.Next(26) + 65)));
                //26 - слов в английском алфавите, 65 - код символа А
                if(r.Next(2)%2==1)//Рандомный выбор - маленькие или большие буквы
                builder.Append(ch.ToString().ToLower());
                builder.Append(ch.ToString().ToUpper());
            }
            return builder.ToString();
        }
        //Рандомное формирование слов для массива
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        static string[] HardMass(int n)
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
        static void Swap(string[] mass, int i, int j)
        {
            string temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }


        //Печать массива
        static void PrintMass(string[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i] + " ");
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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

                    time_1 = TimeOfSelectionSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfSelectionSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine("{0,-40},  {1,-40}, {2,-40} ", n + "эл-ов:", "ПРОСТОЙ_МАССИВ = " + sumTime_1, "ОБРАТНО_СОРТИРОВАННЫЙ = " + sumTime_2);
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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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
