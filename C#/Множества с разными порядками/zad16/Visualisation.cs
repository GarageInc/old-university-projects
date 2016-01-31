using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Регина
{
    class Visualisation
    {
        public static int Choice()
        {
            Console.WriteLine("Введите номер для действия:");
            Console.WriteLine("1 - для объединения множеств с частичным порядком;");
            Console.WriteLine("2 - для пересечения множеств с частичным порядком;");
            Console.WriteLine("3 - для разности множеств с частичным порядком;");
            Console.WriteLine("4 - для объединения множеств с линейным порядком;");
            Console.WriteLine("5 - для пересечения множеств с линейным порядком;");
            Console.WriteLine("6 - для разности множеств с линейным порядком;");
            Console.WriteLine("7 - для выхода.");
            return int.Parse(Console.ReadLine());
        }

        public static void Read(ref Set sc, StreamReader rd)
        {
            string s = rd.ReadToEnd();
            string[] t = s.Split(' ');
            for (int i = 0; i < t.Length; i++)
                sc.Add(int.Parse(t[i]));
        }

        public static void Write(Set sc, StreamWriter wr) 
        {
            wr.WriteLine("Elements of set:\r\n" + sc.ToString());
            sc.Elements.Clear();
        }
    }
}
