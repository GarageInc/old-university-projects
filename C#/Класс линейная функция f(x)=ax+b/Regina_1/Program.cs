using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regina_1
{
    //Class f(x)=a*x+b;
    class AffineF
    {
        private double a, b;
        public AffineF(double a = 0, double b = 0)
        {
            this.a = a;
            this.b = b;
        }
        public double A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }
        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }


        public double Calcul(double x)
        {
            return (x * this.a + this.b);
        }

        public void Display()
        {
            string s = "f(x) = ";
            string s1 = "", s2 = "";
            bool tf=true;
            if (this.a == 0) {s1 = "";tf=false;}
            else s1 += (a + "*x");

            if ((this.b != 0) && tf) s2 += "+"; ;
            if (this.b != 0) s2 += b.ToString(); else ;
Console.WriteLine(s+s1+s2);
        }


        public static AffineF operator +(AffineF f1, AffineF f2)
        {
            return new AffineF(f1.A + f2.A, f1.B + f2.B);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            AffineF f1 = new AffineF(20, 30);
            AffineF f2 = new AffineF(13, 45);
            f1.Display();
            f2.Display();

            Console.WriteLine("В точках x=10 : ");
            Console.WriteLine(f1.Calcul(10));
            Console.WriteLine(f2.Calcul(10));


            Console.WriteLine("Summa = ");
            (f1 + f2).Display();

            Console.ReadKey();
        }
    }
}
