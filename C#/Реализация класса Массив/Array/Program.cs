using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Array
{
    // Создаем на класс Array
    class Array
    {
        private int[] ar;

        // Конструктор - инициализация массива
        public Array(int n, Random r)
        {
            ar = new int[n];
            // Заполняем массив случайными числами
            for (int i = 0; i < n; i++)
                ar[i] = r.Next(0, 10);
        }

        // Печать всех возможных подмножеств из множества
        public void PrintPodmnozhestva()
        {
            for (int i = 0; i < ar.Length - 1; i++)
                for (int j = i + 1; j < ar.Length; j++)// i, j - границы
                {
                    Console.Write("[");
                    for (int z = i; z < j; z++)
                    {
                        Console.Write(ar[z] + " ");
                    }
                    Console.Write("]");
                    Console.WriteLine();
                }
        }

        // Функция печати массива на экран
        public void Print()
        {
            for (int i = 0; i < ar.Length; i++)
            {
                Console.Write(ar[i] + " ");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Функция рандома
            Random r = new Random();

            // Создаем наш массив
            Array myArray = new Array(10, r);

            // Печатаем
            myArray.Print();

            // Снова печатаем, чтобы увидеть результат сортировки
            Console.WriteLine();
            Console.WriteLine("Все возможные множества массива:");
            Console.WriteLine();

            // Напечатаем все подмножества
            myArray.PrintPodmnozhestva();

            Console.ReadKey();// ЧТобы консоль не "гасла" быстро
        }
    }
}
