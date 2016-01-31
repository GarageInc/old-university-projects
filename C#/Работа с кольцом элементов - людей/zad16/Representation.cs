using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;


namespace Diana
{
    class Visualisation//Список основных функций для работы с вводом-выводом
    {
        public static int Choice()//Выбор действия:
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. СОРТИРОВКА И ВЫВОД РЕЗУЛЬТАТА;");
            Console.WriteLine("2. УДАЛЕНИЕ КАЖДОГО К-го С ВЫВОДОМ РЕЗУЛЬТАТА;");
            Console.WriteLine("3. ПОСТРОИТЬ НОВЫЙ СПИСОК ИЗ ДВУХ ИМЕЮЩИХСЯ;");
            Console.WriteLine("4. ИЗ СПИСКА УЧАСТНИКОВ ПОСТРОИТЬ ДВА СПИСКА - СООТЕТСТВЕННО МУЖЧИН И ЖЕНЩИН;");
            Console.WriteLine("5. ВЫХОД.");
            return int.Parse(Console.ReadLine());
        }

        public static void Read(ref Kolco k, StreamReader rd)//Считывание числа из файла:
        {
            k = Instruments.Coding(rd);
        }

        public static void Write(Kolco k, StreamWriter wr)//Вывод данных списка 
        {//sc - список, wr - поток для печати
            wr.WriteLine("Печать полученного списка: " + Instruments.Decoding(k));
            wr.WriteLine();
        }
    }
}
