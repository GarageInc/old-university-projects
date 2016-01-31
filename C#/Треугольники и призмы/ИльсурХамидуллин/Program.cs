using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ИльсурХамидуллин
{
    class EqTriangle//Класс равносторонний треугольник
    {
        protected double a;//Сторона
        //Конструктор
        public EqTriangle(double a = 0)
        {
            this.a = a;
        }

        public double Perimeter()//Периметр
        {
            return this.a * 3;
        }

        public double Square()//Площадь
        {
            return (Math.Sqrt(3)*this.a*this.a*0.25);
        }
        
        public override string  ToString()
        {
            return "Треугольник со стороной а = "+this.a;
        }
    }

    class EqPrism : EqTriangle//Класс правильная призма
    {
        private double h;
        //Конструктор
        public EqPrism(double side, double height)
        {
            this.a = side;
            this.h = height;
        }

        public double Square()//Площадь поверхности призмы
        {
            return 2 * (Math.Sqrt(3) * this.a * this.a * 0.25) + 3 * (this.h * this.a);
        }

        public double Perimeter()//Периметр призмы
        {
            return this.a * 6 + this.h * 3;
        }

        public double Capacity()//Объем
        {
            return (Math.Sqrt(3) * this.a * this.a * 0.25) * this.h;
        }

        public override string ToString()
        {
            return "Призма со стороной основания = "+this.a +" и с высотой = " + this.h;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            //Треугольник
            EqTriangle T = new EqTriangle(10);
            Console.WriteLine(T);
            Console.Write("   P = "); Console.WriteLine(T.Perimeter());
            Console.Write("   S = ");  Console.WriteLine(T.Square());
            Console.WriteLine(T.GetType());

            Console.WriteLine();

            //Призма
            EqPrism P = new EqPrism(9, 10);
            Console.WriteLine(P);
            Console.WriteLine(P.Perimeter());
            Console.Write("   S = ");  Console.WriteLine(P.Square());
            Console.Write("   V = "); Console.WriteLine(P.Capacity());
            Console.WriteLine(P.GetType());

            Console.WriteLine();


            Console.WriteLine("Работа с треугольниками: ");
            Random r = new Random();


            int N = 5;//Количество треугольников
            double sredSqT = 0;//Среднее арифметическое площади
            int kolSqT = 0;//количество треугольников c площадью, меньше средней
            EqTriangle[] ET = new EqTriangle[N];//Массив треугольников. Забьем его случайными значениями от 1 до 30:
            for (int i = 0; i < N; i++)
            {
                ET[i] = new EqTriangle(r.Next(30)+1);//+1 -чтобы не было стороны или высоты равно 0. Исключим тем самым вырожденные случае призм.
                Console.WriteLine("Площадь треугольника №" + (i + 1) + " = {0:F3}", ET[i].Square());//F3 - даёт редактируемый вывод, 3 знака после запятой
                sredSqT=sredSqT+ET[i].Square();
            }
            sredSqT=sredSqT/N;

            //Считаем и выводим количество треугольников с меньшей площадью:
            for (int i = 0; i < N; i++)
            {
                if (ET[i].Square()<sredSqT) kolSqT++;;
            }
            Console.WriteLine("Количество треугольников с площадью меньше средней площади({0:F3}) = " + kolSqT, sredSqT);//F3 - даёт редактируемый вывод, 3 знака после запятой


            Console.WriteLine();
            Console.WriteLine("Работа с призмами: ");

            int M = 5;//Количество призм
            double maxCap = 0;//Максимальный объем            
            EqPrism[] EP = new EqPrism[M];//Массив призм. Забьем его случайными значениями сторон оснований и высот от 1 до 30:
            for (int i = 0; i < M; i++)
            {
                EP[i] = new EqPrism(r.Next(100), r.Next(30)+1);
                Console.WriteLine("Объем призмы №" + (i + 1) + " = {0:F3}", EP[i].Capacity());//Выводим значения, редактируем его через F3 - 3 знака после запятой
                if (EP[i].Capacity() > maxCap) maxCap = EP[i].Capacity(); ;//Ищем максимальный объем                
            }
                       
            Console.WriteLine("Максимальный объем = {0:F3}",maxCap);




            Console.ReadKey();
        }
    }
}
