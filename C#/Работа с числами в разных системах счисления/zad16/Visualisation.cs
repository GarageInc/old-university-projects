using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace zad16
{
    class Visualisation//Список основных функций для работы с вводом-выводом
    {
        public static int Initialize()//Задание системы счисления:
        {
            Console.WriteLine("Введите основание системы счисления или пустую строку по умолчанию(10ичная система):");
            string s = Console.ReadLine();
            if(s != "")
                return int.Parse(s);
            return 10;
        }

        public static int Choice()//Выбор действия:
        {
            Console.WriteLine("Введите номер для действия:");
            Console.WriteLine("1 - для суммирования двух чисел;");
            Console.WriteLine("2 - для умножения двух чисел;");
            Console.WriteLine("3 - для нахождения остатка от деления;");
            Console.WriteLine("4 - для вставки одного числа в середину другого;");
            Console.WriteLine("5 - exit(выход).");
            return int.Parse(Console.ReadLine());
        }

        public static void Read(ref Scalenum sc, StreamReader rd)//Считывание числа из файла:
        {//sc - число в которое считываем;rd - файловый поток откуда считываем
            sc = Functions.Coding(int.Parse(rd.ReadLine()), sc.q);
        }

        public static void Write(Scalenum sc, StreamWriter wr)//Вывод данных о числе 
        {//sc - число;wr - файловый поток в который записываем
            wr.WriteLine("Представление числа в десятичной системе:" + Functions.Decoding(sc));//10-ичная система счисления
            wr.WriteLine("Представление числа в q-ичной системе счисления:\r\n" + sc.ToString());//q-ичная система счисления
        }
    }
}
