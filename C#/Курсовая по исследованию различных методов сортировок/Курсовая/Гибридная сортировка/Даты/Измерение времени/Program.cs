using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sorts
{
            
    class Program
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        
        //Формируем дату определенного размера
        static DateTime Date()
        {
            DateTime start = new DateTime();
            start = DateTime.Now;
            int range = 2013 * 365;
            int c = r.Next(0, range);
            return start.AddDays((-1) * r.Next(0, range));
        }
        //Рандомное формирование слов для массива
        //Задаем рандомный массив
        static DateTime[] RandomMass(int n)
        {
            DateTime[] mass = new DateTime[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = Date();//Слово с длинной в 10 символов
            }

            return mass;
        }


        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        static DateTime[] HardMass(int n)
        {
            DateTime[] mass = new DateTime[n];
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
        static void Swap(DateTime[] mass, int i, int j)
        {
            DateTime temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }

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
            int n=0;
            GybridSort(ref mass,0,mass.Length-1,ref n);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        static double Time(DateTime[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка
            
            Array.Sort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //Визуализация сортировки
        static void Visualization()
        {
            int n;//Количество элементов
            int m = 50;//Базовое количество элементов
            int y = 1;//Количество созданий массивов(количество проверок)
            double sumTime_1 = 0, sumTime_2 = 0, sumTime_3=0;

            //Для старой версии - если массив уже упорядочен - сортировка все равно идет до конца
            //50, 500, 5000, 50000
            for (int i = 0; i < 4; i++)
            {
                double time_1 = 0, time_2 = 0, time_3 = 0 ;
                n = m * (int)Math.Pow(10, i);
                for (int j = 0; j < y; j++)
                {

                    DateTime[] mass_1 = RandomMass(n);
                    DateTime[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов
                    
                    time_1 = TimeOfSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfSort(mass_2);//Считаем время "плохого случая" обратной сортировки массива//Происходит не только проверка, но и постоянные перестановки
                    time_3 = Time((DateTime[])mass_1.Clone());

                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                    sumTime_3 = sumTime_3 + time_3;
                    //Console.WriteLine("Time №"+j+" = "+t);
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;
                sumTime_3 = sumTime_3 / y;
                Console.WriteLine(n + " elements time: " + "SIMPLE = " + sumTime_1 + " HARD = " + sumTime_2+"   STANDART:   "+sumTime_3);
            }
        }

        static void Main(String[] args)
        {

            Visualization();

            Console.ReadKey();
        }
    }
}
