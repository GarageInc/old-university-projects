using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fractions
{
    class Fraction
    {
        //Элементы
        int a;//Числитель
        int b;//Знаменатель

        //Конструктор. Работает и по умолчанию.
        public Fraction(int a=0, int b = 0)
        {
            //Числитель - должен быть целым(может быть отрицательным!)
            //Знаменатель - должен быть натуральным
            this.a = a;
            this.b = b;

            if (b < 0)//Т.к. знаменатель должен
            {
                this.b = (-1) * this.b;
                this.a = this.a * (-1);
            }
            
            //Сокращаем дробь типа (400/400)
            int min = 0;
            //Берем модули, чтобы не учитывать отрицательность возможную от числителя
            if (Math.Abs(this.a) > this.b) min = this.b; else min = Math.Abs(this.a);

            for(int i=min; i>0; i--)
            {
                if ((this.a%i==0) && (this.b%i==0))
                {
                    this.a=this.a/i;
                    this.b = this.b / i;
                }

            }
        }

        //Добавление
        public Fraction Add(Fraction x)
        {
            return new Fraction(a * x.b + b * x.a, b * x.b);
        }

        //Арифметические операции
        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            return new Fraction(f1.a * f2.b + f1.b * f2.a, f1.b * f2.b);
        }
        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            return new Fraction(f1.a *f2.b - f1.b *f2.a, f1.b *f2.b);
        }
        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return new Fraction(f1.a * f2.a,f1.b * f2.b);
        }
        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return new Fraction(f1.a * f2.b, f1.b * f2.a);
        }
        //Сравнение
        public static bool operator ==(Fraction f1, Fraction f2)
        {
            //Числители с общим знаменателем должны быть равны!
            if (f1.a*f2.b == f2.a*f1.b) return true; else return false;
        }
        public static bool operator !=(Fraction f1, Fraction f2)
        {
            if (f1.a * f2.b != f2.a * f1.b) return true; else return false;
        }
        public static bool operator <(Fraction f1, Fraction f2)
        {
            if (f1.a * f2.b < f2.a * f1.b) return true; else return false;
        }
        public static bool operator >(Fraction f1, Fraction f2)
        {
            if (f1.a * f2.b > f2.a * f1.b) return true; else return false;
        }
        public static bool operator <=(Fraction f1, Fraction f2)
        {
            if (f1.a * f2.b <= f2.a * f1.b) return true; else return false;
        }
        public static bool operator >=(Fraction f1, Fraction f2)
        {
            if (f1.a * f2.b >= f2.a * f1.b) return true; else return false;
        }
        //Умножение деление на число
        ////////
        public static Fraction operator /(Fraction f1, int x)
        {
            return new Fraction(f1.a, f1.b*x);
        }
        ///////
        public static Fraction operator *(int x, Fraction f1)
        {
            return new Fraction(f1.a * x, f1.b);
        }


        //Вывод на экран - корректный вывод через Console.WriteLine();
        public override string ToString()
        {
            return ("("+a+"/"+b+")");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(10, 20);
            Fraction b = new Fraction(49, 40);
            
            Console.WriteLine(a);
            Console.WriteLine(b);


            Console.WriteLine("Сложение: " + (a + b));
            Console.WriteLine("Умножение: " + (a * b));
            Console.WriteLine("Деление: " + (a / b));
            Console.WriteLine("Равенство: " + (a == b));
            Console.WriteLine("Сравнение(на неравенство): " + (a != b));
            Console.WriteLine("Сравнение <: " + (a < b));
            
            Console.ReadKey();//"Задержка"
        }
    }
}
