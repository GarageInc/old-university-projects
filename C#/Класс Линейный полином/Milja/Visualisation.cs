using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Milja
{
    class Visualisation
    {
        public static int Choice()
        {
            Console.WriteLine("Enter number for action:");
            Console.WriteLine("1 - для умножения на линейный полином;");
            Console.WriteLine("2 - for division on linear polynom;");
            Console.WriteLine("3 - for collecting similar terms;");
            Console.WriteLine("4 - for exit.");
            return int.Parse(Console.ReadLine());
        }

        public static void Read(ref Polynom sc, StreamReader rd)
        {
            string s = rd.ReadToEnd();
            string[] t = s.Split(' ');
            for (int i = 0; i < t.Length / 2; i++)
            {
                sc.degrees.Add(int.Parse(t[2 * i]));
                sc.koeff.Add(double.Parse(t[2 * i + 1]));
            }
        }

        public static void Read(ref LinearPolynom sc)
        {
            Console.WriteLine("Enter the first koefficient:");
            sc.koeff.Add(double.Parse(Console.ReadLine()));
            sc.degrees.Add(0);
            Console.WriteLine("Enter the second koefficient:");
            sc.koeff.Add(double.Parse(Console.ReadLine()));
            sc.degrees.Add(1);
        }

        public static void Write(Polynom sc, StreamWriter wr) 
        {
            wr.WriteLine("Koefficients of polynom:\r\n" + sc.ToString());
        }
    }
}
