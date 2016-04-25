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
        static string[] HardMass(int n)
        {
            string[] mass = new string[n];
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
        static void Swap(string[] mass, int i, int j)
        {
            string temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }

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


        //Печать массива
        static void PrintMass(string[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i] + " ");
            }
            Console.WriteLine();
        }
        //Рассчет времени сортировки для рандомного массива
        static double TimeOfSort(string[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            ShellSort(mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //Визуализация сортировки
        static void Visualization()
        {
            int n;//Количество элементов
            int m = 50;//Базовое количество элементов
            int y = 1;//Количество созданий массивов(количество проверок)
            double sumTime_1 = 0, sumTime_2 = 0, sumTime_3 = 0;

            //Для старой версии - если массив уже упорядочен - сортировка все равно идет до конца
            //50, 500, 5000, 50000
            for (int i = 0; i < 4; i++)
            {
                double time_1 = 0, time_2 = 0, time_3;
                n = m * (int)Math.Pow(10, i);
                for (int j = 0; j < y; j++)
                {

                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Массив из полностью обратной сортировки элементов

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

        static void Main(String[] args)
        {

            Visualization();

            Console.ReadKey();
        }
    }
}