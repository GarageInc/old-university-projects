using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rusha_AnalizT
{
    class S
    {
        public static void Str()
        {
            //Поток для вывода
            StreamWriter sw = new StreamWriter("t_strings.txt");

            //Сортировка 50 элементов-строк
            sw.WriteLine("Сортировка массива СТРОК 50 элементов: 1-2 строки  - 1ая строка обычный массив, вторая строка - обратно отсортированный массив");
            sw.WriteLine("Сортировка массива СТРОК 500 элементов. 3 и 4 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива СТРОК 5000 элементов. 5 и 6 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива СТРОК 50000 элементов. 7 и 8 строка - то же самое соответственно");

            sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20}", "Пузырьком", "Вставками", "Выбором", "Слиянием", "Шелла", "Быстрая", "Деревом-пирамидкой");

            int n;//Количество элементов
            int m = 50;//Стандарт
            int y = 400;//Количество созданий массивов(количество проверок)

            int stepen = 4;

            for (int i = 0; i < stepen; i++)
            {
                y = y / 4;
                n = m * (int)Math.Pow(10, i);

                double s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, s7 = 0;
                double sr1 = 0, sr2 = 0, sr3 = 0, sr4 = 0, sr5 = 0, sr6 = 0, sr7 = 0;
                double t1, t2, t3, t4, t5, t6, t7;
                double tr1, tr2, tr3, tr4, tr5, tr6, tr7;
                //Сортировки

                for (int j = 0; j < y; j++)
                {
                    string[] m1 = S_String.RandomMass(n);
                    string[] m2 = (string[])m1.Clone();
                    string[] m3 = (string[])m1.Clone();
                    string[] m4 = (string[])m1.Clone();
                    string[] m5 = (string[])m1.Clone();
                    string[] m6 = (string[])m1.Clone();
                    string[] m7 = (string[])m1.Clone();

                    string[] mr1 = S_String.HardMass(n);//Массив из полностью обратной сортировки элементов
                    string[] mr2 = (string[])mr1.Clone();
                    string[] mr3 = (string[])mr1.Clone();
                    string[] mr4 = (string[])mr1.Clone();
                    string[] mr5 = (string[])mr1.Clone();
                    string[] mr6 = (string[])mr1.Clone();
                    string[] mr7 = (string[])mr1.Clone();

                    t1 = S_String.TimeOfBubleSort(m1);//Считаем время рандомно сгенерированного массива
                    tr1 = S_String.TimeOfBubleSort(mr1);//Считаем время - обратно отсортированного массива

                    t2 = S_String.TimeOfInsertSort(m2);//Считаем время рандомно сгенерированного массива
                    tr2 = S_String.TimeOfInsertSort(mr2);//Считаем время - обратно отсортированного массива

                    t3 = S_String.TimeOfSelectionSort(m3);//Считаем время рандомно сгенерированного массива
                    tr3 = S_String.TimeOfSelectionSort(mr3);//Считаем время - обратно отсортированного массива

                    t4 = S_String.TimeOfMergeSort(m4);//Считаем время рандомно сгенерированного массива
                    tr4 = S_String.TimeOfMergeSort(mr4);//Считаем время - обратно отсортированного массива

                    t5 = S_String.TimeOfShellSort(m5);//Считаем время рандомно сгенерированного массива
                    tr5 = S_String.TimeOfShellSort(mr5);//Считаем время - обратно отсортированного массива

                    t6 = S_String.TimeOfQuickSort(m6);//Считаем время рандомно сгенерированного массива
                    tr6 = S_String.TimeOfQuickSort(mr6);//Считаем время - обратно отсортированного массива

                    t7 = S_String.TimeOfHeapSort(m7);//Считаем время рандомно сгенерированного массива
                    tr7 = S_String.TimeOfHeapSort(mr7);//Считаем время - обратно отсортированного массива


                    s1 = s1 + t1;
                    s2 = s3 + t2;
                    s3 = s3 + t3;
                    s4 = s4 + t4;
                    s5 = s5 + t5;
                    s6 = s6 + t6;
                    s7 = s7 + t7;

                    sr1 = sr1 + tr1;
                    sr2 = sr3 + tr2;
                    sr3 = sr3 + tr3;
                    sr4 = sr4 + tr4;
                    sr5 = sr5 + tr5;
                    sr6 = sr6 + tr6;
                    sr7 = sr7 + tr7;
                }
                s1 = s1 / y;//Среднее время сортировки обычного массива
                s2 = s2 / y;
                s3 = s3 / y;
                s4 = s4 / y;
                s5 = s5 / y;
                s6 = s6 / y;
                s7 = s7 / y;

                sr1 = sr1 / y;//Среднее время сортировки рандомного массива
                sr2 = sr2 / y;
                sr3 = sr3 / y;
                sr4 = sr4 / y;
                sr5 = sr5 / y;
                sr6 = sr6 / y;
                sr7 = sr7 / y;

                //Выводим результаты

                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20}", s1, s2, s3, s4, s5, s5, s6, s7);
                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20}", sr1, sr2, sr3, sr4, sr5, sr5, sr6, sr7);
                sw.WriteLine();


            }
            sw.Close();
        }


        public static void Date()
        {
            //Поток для вывода
            StreamWriter sw = new StreamWriter("t_dates.txt");

            //Сортировка 50 элементов-строк
            sw.WriteLine("Сортировка массива ДАТ 50 элементов: 1-2 строки  - 1ая строка обычный массив, вторая строка - обратно отсортированный массив");
            sw.WriteLine("Сортировка массива ДАТ 500 элементов. 3 и 4 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива ДАТ 5000 элементов. 5 и 6 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива ДАТ 50000 элементов. 7 и 8 строка - то же самое соответственно");

            sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20}", "Пузырьком", "Вставками", "Выбором", "Слиянием", "Шелла", "Быстрая", "Деревом-пирамидкой");

            int n;//Количество элементов
            int m = 50;//Стандарт
            int y = 400;//Количество созданий массивов(количество проверок)

            int stepen = 4;

            for (int i = 0; i < stepen; i++)
            {
                y = y / 4;
                n = m * (int)Math.Pow(10, i);

                double s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, s7 = 0;
                double sr1 = 0, sr2 = 0, sr3 = 0, sr4 = 0, sr5 = 0, sr6 = 0, sr7 = 0;
                double t1, t2, t3, t4, t5, t6, t7;
                double tr1, tr2, tr3, tr4, tr5, tr6, tr7;
                //Сортировки

                for (int j = 0; j < y; j++)
                {
                    DateTime[] m1 = S_DateTime.RandomMass(n);
                    DateTime[] m2 = (DateTime[])m1.Clone();
                    DateTime[] m3 = (DateTime[])m1.Clone();
                    DateTime[] m4 = (DateTime[])m1.Clone();
                    DateTime[] m5 = (DateTime[])m1.Clone();
                    DateTime[] m6 = (DateTime[])m1.Clone();
                    DateTime[] m7 = (DateTime[])m1.Clone();

                    DateTime[] mr1 = S_DateTime.HardMass(n);//Массив из полностью обратной сортировки элементов
                    DateTime[] mr2 = (DateTime[])mr1.Clone();
                    DateTime[] mr3 = (DateTime[])mr1.Clone();
                    DateTime[] mr4 = (DateTime[])mr1.Clone();
                    DateTime[] mr5 = (DateTime[])mr1.Clone();
                    DateTime[] mr6 = (DateTime[])mr1.Clone();
                    DateTime[] mr7 = (DateTime[])mr1.Clone();

                    t1 = S_DateTime.TimeOfBubleSort(m1);//Считаем время рандомно сгенерированного массива
                    tr1 = S_DateTime.TimeOfBubleSort(mr1);//Считаем время - обратно отсортированного массива

                    t2 = S_DateTime.TimeOfInsertSort(m2);//Считаем время рандомно сгенерированного массива
                    tr2 = S_DateTime.TimeOfInsertSort(mr2);//Считаем время - обратно отсортированного массива

                    t3 = S_DateTime.TimeOfSelectionSort(m3);//Считаем время рандомно сгенерированного массива
                    tr3 = S_DateTime.TimeOfSelectionSort(mr3);//Считаем время - обратно отсортированного массива

                    t4 = S_DateTime.TimeOfMergeSort(m4);//Считаем время рандомно сгенерированного массива
                    tr4 = S_DateTime.TimeOfMergeSort(mr4);//Считаем время - обратно отсортированного массива

                    t5 = S_DateTime.TimeOfShellSort(m5);//Считаем время рандомно сгенерированного массива
                    tr5 = S_DateTime.TimeOfShellSort(mr5);//Считаем время - обратно отсортированного массива

                    t6 = S_DateTime.TimeOfQuickSort(m6);//Считаем время рандомно сгенерированного массива
                    tr6 = S_DateTime.TimeOfQuickSort(mr6);//Считаем время - обратно отсортированного массива

                    t7 = S_DateTime.TimeOfHeapSort(m7);//Считаем время рандомно сгенерированного массива
                    tr7 = S_DateTime.TimeOfHeapSort(mr7);//Считаем время - обратно отсортированного массива


                    s1 = s1 + t1;
                    s2 = s3 + t2;
                    s3 = s3 + t3;
                    s4 = s4 + t4;
                    s5 = s5 + t5;
                    s6 = s6 + t6;
                    s7 = s7 + t7;

                    sr1 = sr1 + tr1;
                    sr2 = sr3 + tr2;
                    sr3 = sr3 + tr3;
                    sr4 = sr4 + tr4;
                    sr5 = sr5 + tr5;
                    sr6 = sr6 + tr6;
                    sr7 = sr7 + tr7;
                }
                s1 = s1 / y;//Среднее время сортировки обычного массива
                s2 = s2 / y;
                s3 = s3 / y;
                s4 = s4 / y;
                s5 = s5 / y;
                s6 = s6 / y;
                s7 = s7 / y;

                sr1 = sr1 / y;//Среднее время сортировки рандомного массива
                sr2 = sr2 / y;
                sr3 = sr3 / y;
                sr4 = sr4 / y;
                sr5 = sr5 / y;
                sr6 = sr6 / y;
                sr7 = sr7 / y;

                //Выводим результаты

                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20}", s1, s2, s3, s4, s5, s5, s6, s7);
                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20}", sr1, sr2, sr3, sr4, sr5, sr5, sr6, sr7);
                sw.WriteLine();


            }
            sw.Close();
        }


        public static void Numbers()
        {
            //Поток для вывода
            StreamWriter sw = new StreamWriter("t_numbers.txt");

            //Сортировка 50 элементов-строк
            sw.WriteLine("Сортировка массива ЦИФР(0-9) 50 элементов: 1-2 строки  - 1ая строка обычный массив, вторая строка - обратно отсортированный массив");
            sw.WriteLine("Сортировка массива ЦИФР(0-9) 500 элементов. 3 и 4 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива ЦИФР(0-9) 5000 элементов. 5 и 6 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива ЦИФР(0-9) 50000 элементов. 7 и 8 строка - то же самое соответственно");

            sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20} {7,-20}", "Пузырьком", "Вставками", "Выбором", "Слиянием", "Шелла", "Быстрая", "Деревом-пирамидкой", "Поразрядная");

            int n;//Количество элементов
            int m = 50;//Стандарт
            int y = 400;//Количество созданий массивов(количество проверок)

            int stepen = 4;

            for (int i = 0; i < stepen; i++)
            {
                y = y / 4;
                n = m * (int)Math.Pow(10, i);

                double s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, s7 = 0, s8=0;
                double sr1 = 0, sr2 = 0, sr3 = 0, sr4 = 0, sr5 = 0, sr6 = 0, sr7 = 0, sr8=0;
                double t1, t2, t3, t4, t5, t6, t7, t8;
                double tr1, tr2, tr3, tr4, tr5, tr6, tr7, tr8;
                //Сортировки

                for (int j = 0; j < y; j++)
                {
                    int[] m1 = S_Numbers.RandomMass(n);
                    int[] m2 = (int[])m1.Clone();
                    int[] m3 = (int[])m1.Clone();
                    int[] m4 = (int[])m1.Clone();
                    int[] m5 = (int[])m1.Clone();
                    int[] m6 = (int[])m1.Clone();
                    int[] m7 = (int[])m1.Clone();
                    int[] m8 = (int[])m1.Clone();

                    int[] mr1 = S_Numbers.HardMass(n);//Массив из полностью обратной сортировки элементов
                    int[] mr2 = (int[])mr1.Clone();
                    int[] mr3 = (int[])mr1.Clone();
                    int[] mr4 = (int[])mr1.Clone();
                    int[] mr5 = (int[])mr1.Clone();
                    int[] mr6 = (int[])mr1.Clone();
                    int[] mr7 = (int[])mr1.Clone();
                    int[] mr8 = (int[])mr1.Clone();

                    t1 = S_Numbers.TimeOfBubleSort(m1);//Считаем время рандомно сгенерированного массива
                    tr1 = S_Numbers.TimeOfBubleSort(mr1);//Считаем время - обратно отсортированного массива

                    t2 = S_Numbers.TimeOfInsertSort(m2);//Считаем время рандомно сгенерированного массива
                    tr2 = S_Numbers.TimeOfInsertSort(mr2);//Считаем время - обратно отсортированного массива

                    t3 = S_Numbers.TimeOfSelectionSort(m3);//Считаем время рандомно сгенерированного массива
                    tr3 = S_Numbers.TimeOfSelectionSort(mr3);//Считаем время - обратно отсортированного массива

                    t4 = S_Numbers.TimeOfMergeSort(m4);//Считаем время рандомно сгенерированного массива
                    tr4 = S_Numbers.TimeOfMergeSort(mr4);//Считаем время - обратно отсортированного массива

                    t5 = S_Numbers.TimeOfShellSort(m5);//Считаем время рандомно сгенерированного массива
                    tr5 = S_Numbers.TimeOfShellSort(mr5);//Считаем время - обратно отсортированного массива

                    t6 = S_Numbers.TimeOfQuickSort(m6);//Считаем время рандомно сгенерированного массива
                    tr6 = S_Numbers.TimeOfQuickSort(mr6);//Считаем время - обратно отсортированного массива

                    t7 = S_Numbers.TimeOfHeapSort(m7);//Считаем время рандомно сгенерированного массива
                    tr7 = S_Numbers.TimeOfHeapSort(mr7);//Считаем время - обратно отсортированного массива

                    t8 = S_Numbers.TimeOfRadixSort(m8);//Считаем время рандомно сгенерированного массива
                    tr8 = S_Numbers.TimeOfRadixSort(mr8);//Считаем время - обратно отсортированного массива

                    s1 = s1 + t1;
                    s2 = s3 + t2;
                    s3 = s3 + t3;
                    s4 = s4 + t4;
                    s5 = s5 + t5;
                    s6 = s6 + t6;
                    s7 = s7 + t7;
                    s8 = s8 + t8;

                    sr1 = sr1 + tr1;
                    sr2 = sr3 + tr2;
                    sr3 = sr3 + tr3;
                    sr4 = sr4 + tr4;
                    sr5 = sr5 + tr5;
                    sr6 = sr6 + tr6;
                    sr7 = sr7 + tr7;
                    sr8 = sr8 + tr8;
                }
                s1 = s1 / y;//Среднее время сортировки обычного массива
                s2 = s2 / y;
                s3 = s3 / y;
                s4 = s4 / y;
                s5 = s5 / y;
                s6 = s6 / y;
                s7 = s7 / y;
                s8 = s8 / y;

                sr1 = sr1 / y;//Среднее время сортировки рандомного массива
                sr2 = sr2 / y;
                sr3 = sr3 / y;
                sr4 = sr4 / y;
                sr5 = sr5 / y;
                sr6 = sr6 / y;
                sr7 = sr7 / y;
                sr8 = sr8 / y;

                //Выводим результаты

                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20} {7,-20}", s1, s2, s3, s4, s5, s5, s6, s7, s8);
                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20} {7,-20}", sr1, sr2, sr3, sr4, sr5, sr5, sr6, sr7, sr8);
                sw.WriteLine();
            }
            sw.Close();
        }


        public static void Integers()
        {
            //Поток для вывода
            StreamWriter sw = new StreamWriter("t_integers.txt");

            //Сортировка 50 элементов-строк
            sw.WriteLine("Сортировка массива ЧИСЕЛ(0-1млн) 50 элементов: 1-2 строки  - 1ая строка обычный массив, вторая строка - обратно отсортированный массив");
            sw.WriteLine("Сортировка массива ЧИСЕЛ(0-1млн) 500 элементов. 3 и 4 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива ЧИСЕЛ(0-1млн) 5000 элементов. 5 и 6 строка - то же самое соответственно");
            sw.WriteLine("Сортировка массива ЧИСЕЛ(0-1млн) 50000 элементов. 7 и 8 строка - то же самое соответственно");

            sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20} {7,-20}", "Пузырьком", "Вставками", "Выбором", "Слиянием", "Шелла", "Быстрая", "Деревом-пирамидкой", "Поразрядная");

            int n;//Количество элементов
            int m = 50;//Стандарт
            int y = 400;//Количество созданий массивов(количество проверок)

            int stepen = 4;

            for (int i = 0; i < stepen; i++)
            {
                y = y / 4;
                n = m * (int)Math.Pow(10, i);

                double s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, s7 = 0, s8 = 0;
                double sr1 = 0, sr2 = 0, sr3 = 0, sr4 = 0, sr5 = 0, sr6 = 0, sr7 = 0, sr8 = 0;
                double t1, t2, t3, t4, t5, t6, t7, t8;
                double tr1, tr2, tr3, tr4, tr5, tr6, tr7, tr8;
                //Сортировки

                for (int j = 0; j < y; j++)
                {
                    int[] m1 = S_Integers.RandomMass(n);
                    int[] m2 = (int[])m1.Clone();
                    int[] m3 = (int[])m1.Clone();
                    int[] m4 = (int[])m1.Clone();
                    int[] m5 = (int[])m1.Clone();
                    int[] m6 = (int[])m1.Clone();
                    int[] m7 = (int[])m1.Clone();
                    int[] m8 = (int[])m1.Clone();

                    int[] mr1 = S_Integers.HardMass(n);//Массив из полностью обратной сортировки элементов
                    int[] mr2 = (int[])mr1.Clone();
                    int[] mr3 = (int[])mr1.Clone();
                    int[] mr4 = (int[])mr1.Clone();
                    int[] mr5 = (int[])mr1.Clone();
                    int[] mr6 = (int[])mr1.Clone();
                    int[] mr7 = (int[])mr1.Clone();
                    int[] mr8 = (int[])mr1.Clone();

                    t1 = S_Integers.TimeOfBubleSort(m1);//Считаем время рандомно сгенерированного массива
                    tr1 = S_Integers.TimeOfBubleSort(mr1);//Считаем время - обратно отсортированного массива

                    t2 = S_Integers.TimeOfInsertSort(m2);//Считаем время рандомно сгенерированного массива
                    tr2 = S_Integers.TimeOfInsertSort(mr2);//Считаем время - обратно отсортированного массива

                    t3 = S_Integers.TimeOfSelectionSort(m3);//Считаем время рандомно сгенерированного массива
                    tr3 = S_Integers.TimeOfSelectionSort(mr3);//Считаем время - обратно отсортированного массива

                    t4 = S_Integers.TimeOfMergeSort(m4);//Считаем время рандомно сгенерированного массива
                    tr4 = S_Integers.TimeOfMergeSort(mr4);//Считаем время - обратно отсортированного массива

                    t5 = S_Integers.TimeOfShellSort(m5);//Считаем время рандомно сгенерированного массива
                    tr5 = S_Integers.TimeOfShellSort(mr5);//Считаем время - обратно отсортированного массива

                    t6 = S_Integers.TimeOfQuickSort(m6);//Считаем время рандомно сгенерированного массива
                    tr6 = S_Integers.TimeOfQuickSort(mr6);//Считаем время - обратно отсортированного массива

                    t7 = S_Integers.TimeOfHeapSort(m7);//Считаем время рандомно сгенерированного массива
                    tr7 = S_Integers.TimeOfHeapSort(mr7);//Считаем время - обратно отсортированного массива

                    t8 = S_Integers.TimeOfRadixSort(m8);//Считаем время рандомно сгенерированного массива
                    tr8 = S_Integers.TimeOfRadixSort(mr8);//Считаем время - обратно отсортированного массива

                    s1 = s1 + t1;
                    s2 = s3 + t2;
                    s3 = s3 + t3;
                    s4 = s4 + t4;
                    s5 = s5 + t5;
                    s6 = s6 + t6;
                    s7 = s7 + t7;
                    s8 = s8 + t8;

                    sr1 = sr1 + tr1;
                    sr2 = sr3 + tr2;
                    sr3 = sr3 + tr3;
                    sr4 = sr4 + tr4;
                    sr5 = sr5 + tr5;
                    sr6 = sr6 + tr6;
                    sr7 = sr7 + tr7;
                    sr8 = sr8 + tr8;
                }
                s1 = s1 / y;//Среднее время сортировки обычного массива
                s2 = s2 / y;
                s3 = s3 / y;
                s4 = s4 / y;
                s5 = s5 / y;
                s6 = s6 / y;
                s7 = s7 / y;
                s8 = s8 / y;

                sr1 = sr1 / y;//Среднее время сортировки рандомного массива
                sr2 = sr2 / y;
                sr3 = sr3 / y;
                sr4 = sr4 / y;
                sr5 = sr5 / y;
                sr6 = sr6 / y;
                sr7 = sr7 / y;
                sr8 = sr8 / y;

                //Выводим результаты

                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20} {7,-20}", s1, s2, s3, s4, s5, s5, s6, s7, s8);
                sw.WriteLine("{0,-20}  {1,-20} {2,-20} {3,-20} {4,-20} {5,-20} {6,-20} {7,-20}", sr1, sr2, sr3, sr4, sr5, sr5, sr6, sr7, sr8);
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}
