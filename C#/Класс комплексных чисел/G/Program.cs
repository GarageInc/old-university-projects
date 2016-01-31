//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace G
//{
//    /// <summary>
//    /// Класс комплексных чисел
//    /// </summary>
//    public class Complex
//    {
//        private double real = 0.0;
//        private double imag = 0.0;

//        // Конструкторы
//        public Complex()
//        {
//        }

//        public Complex (double re)
//        {
//            real = re;
//        }

//        public Complex (double re, double im)
//        {
//            real = re;
//            imag = im;
//        }

//        public Complex (Complex x)
//        {
//            real = x.Real;
//            imag = x.Imag;
//        }
        
//        // Получение частей числа
//        public double Real
//        {
//            get { return real; }
//            set { real = value; }
//        }

//        public double Imag
//        {
//            get { return imag; }
//            set {imag = value; }
//        }

//        // Модуль комплексного числа
//        public double Abs
//        {
//            get { return Math.Sqrt(imag * imag + real * real); }
//        }

//        // Квадратный корень комплексного числа
//        // Модуль комплексного числа
//        public Complex Sqrt()
//        {
//            return new Complex(Math.Sqrt((Math.Sqrt(imag * imag + real * real)+real)/2),Math.Sign(imag)*Math.Sqrt((Math.Sqrt(imag * imag + real * real)-real)/2));
//        }

//        // Вывод комплексного числа в строку
//        public override string ToString()
//        {
//            string res = "";

//            if (real != 0.0)
//            {
//                res = real.ToString();
//            }

//            if (imag != 0.0)
//            {
//                if (imag > 0)
//                {
//                    res += "+";
//                }

//                res += imag.ToString() + "i";
//            }

//            return res;
//        }


//        // Перегруженный оператор Сложения одного комплексного числа на другое
//        public static Complex operator + (Complex c1, Complex c2)
//        {
//            return new Complex (c1.Real + c2.Real, c1.Imag + c2.Imag);
//        }
        
//        // Перегруженный оператор вычитания одного комплексного числа на другое
//        public static Complex operator - (Complex c1, Complex c2)
//        {
//            return new Complex (c1.Real - c2.Real, c1.Imag - c2.Imag);
//        }


//        // Перегруженный оператор умножения одного комплексного числа на другое
//        public static Complex operator * (Complex c1, Complex c2)
//        {
//            return new Complex (c1.Real * c2.Real - c1.Imag * c2.Imag,
//                c1.Real * c2.Imag + c1.Imag * c2.Real);
//        }



//        // Перегруженный оператор деления одного комплексного числа на другое
//        public static Complex operator / (Complex c1, Complex c2)
//        {
//            double Denominator = c2.Real * c2.Real + c2.Imag * c2.Imag;
//            return new Complex ((c1.Real * c2.Real + c1.Imag * c2.Imag) / Denominator,
//                (c2.Real * c1.Imag - c2.Imag * c1.Real) / Denominator);
//        }


//        public static bool operator == (Complex c1, Complex c2)
//        {
//            return c1.Real == c2.Real && c1.Imag == c2.Imag;
//        }

//        public static bool operator != (Complex c1, Complex c2)
//        {
//            return c1.Real != c2.Real || c1.Imag != c2.Imag;
//        }

//    }
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("КОМПЛЕКСНЫЕ ЧИСЛА:");
//            Complex a = new Complex(2, 3);
//            Complex b = new Complex(5, 7);

//            Console.WriteLine("Умножение " + a.ToString() + " на " + b.ToString() + " = " + (a * b).ToString());
//            Console.WriteLine("Деление " + a.ToString() + " на " + b.ToString() + " = " + (a / b).ToString());
//            Console.WriteLine("Сложение " + a.ToString() + " на " + b.ToString() + " = " + (a + b).ToString());

//            Console.WriteLine("Корень " + a.ToString() + " = (+/-)*(" + (a.Sqrt()).ToString()+")");

//            Console.ReadKey();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drobi
{
    class Program
    {
        static void Main(string[] args)
        {
            //Основная программа
            Дробь a = new Дробь(4, -8);//создание объекта класса Drob
            Дробь b = new Дробь(2, 5);//создание объекта класса Drob

            Дробь c;
            c = a + b;
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine("Проверка на сложение: " + a.ToString() + "+" + b.ToString() + "=" + c.ToString());
            Console.ReadKey();

        }
    }
    class Дробь//Описание класса Drob
    {
        public double числитель = 0;
        public double знаменатель = 0;

        public Дробь(int c, int z)
        {
            this.числитель = c;
            this.знаменатель = z;

        }

        public override string ToString()//cтроковое представление
        {
            return "(" + числитель.ToString() + "/" + знаменатель.ToString() + ")";
        }

        public static Дробь operator +(Дробь a, Дробь b)//сложение дробей
        {
            Дробь t = new Дробь(1, 1);//создание и инициализация новой дроби
            t.числитель = (a.числитель * b.знаменатель + a.знаменатель * b.числитель);//числитель новой дроби
            t.знаменатель = a.знаменатель * b.знаменатель;//знаменатель новой дроби
            Дробь.SetFormat(t);//сокращаем дробь
            return t;//возвращаем результат

        }
        public static Дробь operator -(Дробь a, Дробь b)//вычитание дробей
        {
            Дробь t = new Дробь(1, 1);//создание и инициализация новой дроби
            t.числитель = (a.числитель * b.знаменатель - a.знаменатель * b.числитель);//числитель новой дроби
            t.знаменатель = a.знаменатель * b.знаменатель;//знаменатель новой дроби
            Дробь.SetFormat(t);//сокращаем дробь
            return t;//возвращаем результат

        }
        public static Дробь operator *(Дробь a, Дробь b)//умножение дробей
        {
            Дробь t = new Дробь(1, 1);//создание и инициализация новой дроби
            t.числитель = (a.числитель * b.числитель);//числитель новой дроби
            t.знаменатель = a.знаменатель * b.знаменатель;//знаменатель новой дроби
            Дробь.SetFormat(t);//сокращаем дробь
            return t;//возвращаем результат

        }
        public static Дробь operator /(Дробь a, Дробь b)//деление дробей
        {
            Дробь t = new Дробь(1, 1);//создание и инициализация новой дроби
            t.числитель = (a.числитель / b.числитель);//числитель новой дроби
            t.знаменатель = a.знаменатель / b.знаменатель;//знаменатель новой дроби
            Дробь.SetFormat(t);//сокращаем дробь
            return t;//возвращаем результат
        }
        //процедура по сокращению дроби
        public static Дробь SetFormat(Дробь a)
        {

            double max = 0;

            //выбираем что больше числитель или знаменатель
            if (a.числитель > a.знаменатель)
                max = Math.Abs(a.знаменатель);//ВНИМАНИЕ! берем по модулю, что работало и с отрицательными
            else
                max = Math.Abs(a.числитель);//ВНИМАНИЕ! берем по модулю, что работало и с отрицательными
            //поиск от максимума до 2
            for (double i = max; i >= 2; i--)
            {
                //такого числа, поделив на которое бы делилось без
                //остатка и на числитель и на знаменатель
                if ((a.числитель % i == 0) & (a.знаменатель % i == 0))
                {
                    a.числитель = a.числитель / i;
                    a.знаменатель = a.знаменатель / i;
                }

            }
            //Определяемся со знаком
            //если в знаменателе минус, поднимаем его наверх
            if ((a.знаменатель < 0))
            {
                a.числитель = -1 * (a.числитель);
                a.знаменатель = Math.Abs(a.знаменатель);
            }
            return (a);//возращаем результат
        }
    }
}