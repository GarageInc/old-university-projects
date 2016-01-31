using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;

namespace Diana
{
    //Класс прямоугольник
    class Rect
    {
        Point piLB, piRB;//Координаты
        //Конструктор
        public Rect(Point p1, Point p2)
        {
            this.piLB = p1;
            this.piRB = p2;
        }

        public void Display()
        {
            Console.WriteLine(("[(" + piLB.X + "," + piLB.Y + "),(" + piRB.X + "," + piRB.Y + ")]"));
        }

        //Переопределеяем сложение
        public static Rect operator +(Rect r1, Rect r2)
        {
            //x1,x2, y1,y2 - для первого, остальное - для второго из складываемых прямоугольников
            int x11 = max(min(r1.piLB.X,r1.piRB.X),min(r2.piLB.X,r2.piRB.X));
            int x12 = min(max(r1.piLB.X,r1.piRB.X),max(r2.piLB.X,r2.piRB.X));
            int y11 = max(min(r1.piLB.Y,r1.piRB.Y),min(r2.piLB.Y,r2.piRB.Y));
            int y12 = min(max(r1.piLB.Y,r1.piRB.Y),max(r2.piLB.Y,r2.piRB.Y));

            Rect r = new Rect(new Point(x11, y11), new Point(x12, y12));
            return r;
        }

        //Максимум из двух чисел
        public static int max(int x, int y)
        {
            if (x > y) return x; else return y;
        }
        //Минимум
        public static int min(int x, int y)
        {
            if (x > y) return y; else return x;
        }
    }
    class Program
    {
       

        static void Main(string[] args)
        {
            Rect r1 = new Rect(new Point(0, 0), new Point(10, 10));
            Rect r2 = new Rect(new Point(5, 5), new Point(30, 30));
            
            //Выводим

            Console.WriteLine("Первый прямоугольник с координатами: ");
            r1.Display();

            Console.WriteLine("Второй прямоугольник с координатами: ");
            r2.Display();

            //Складываем
            //Выводим
            Console.WriteLine("Result of + :");
            (r1 + r2).Display();

            //Делаем задержку
            Console.ReadKey();

        }
    }
}
