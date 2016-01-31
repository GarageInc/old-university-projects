using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Straights
{
    //Класс, описывающий свойства и методы объекта ПРЯМАЯ типа a*x+b*y=c
    class Straight
    {
        //Параметры прямой
        private double a, b, c;//Могут быть и не целочисленные значения, поэтому - double
        private bool flag = false;//Флаг для вырожденного случая, когда прямая - неизвестна, т.е. a=0, b=0!

        //Все возможные конструкторы, которые могут приниматься при создании объекта типа ПРЯМАЯ:
        public Straight(double a = 0, double b = 0, double c = 0)//Заранее присваиваются нулевые значения, т.к. часть данных пользователем может быть упущена
        {
            this.a = a;
            this.b = b;
            this.c = c;

            if (a == 0 && b == 0)
                flag = true;//Значит, что прямая вырожденная
        }

        //Переобределяем класс ToString для печати данных о прямой. Делаем его public, чтобы можно было вызывать
        //Подумать над правильным выводом при отрицательных величинах!
        public override string  ToString()
        {        
            string str1="", str2="", str3="", pl1="+", pl2="+";//Для корректировки отрицательных и нулевых членов
            
            if (b < 0) pl1 = ""; ;//Если отрицательное число - знак плюс нам не нужен.
            if (c < 0) pl2 = ""; ;
            if (a == 0) pl1 = ""; ;

            if (a != 0) str1 = a + "*x"; ;
            if (b != 0) str2 = pl1 + b + "*y"; ;
            if (c != 0) str3 = pl2 + c; 
            // проверим - не вырожденная ли прямая?
            if (this.flag==true)
            {
                return "Вы не ввели параметры прямой";
            }
            else
                return "Прямая:   "+ str1 + str2 + str3 + " = 0";
        }

        //Создаём метод - определение пересечений с осями координат:
        public void IntersectionAxes()
        {
            double x = 0, y = 0;
            if (flag == false)
            {
                if (a != 0)
                {
                    if (b != 0)
                    {
                        x = (-1) * c / this.a;
                        Console.WriteLine("  Пересечения с осью 0X в точке x = " + x);
                    }
                    else
                    {
                        if (c != 0)
                        {
                            x = (-1) * c / this.a;
                            Console.WriteLine("  Пересечения с осью 0X в точке x = " + x);
                        }
                        else
                            Console.WriteLine("  Пересечения с осью 0X нет. ");
                    }
                }
                else
                {
                    Console.WriteLine("  Прямая параллельна оси 0Х. ");
                }

                if (b != 0)
                {
                    if (a != 0)
                    {
                        y = (-1) * c / this.b;
                        Console.WriteLine("  Пересечения с осью 0Y в точке y = " + y);                       
                    }
                    else
                    {
                        if (c != 0)
                        {
                            y = (-1) * c / this.b;
                            Console.WriteLine("  Пересечения с осью 0Y в точке y = " + y);
                        }
                        else
                            Console.WriteLine("  Пересечения с осью 0Y нет. ");

                    }
                }
                else
                {
                    Console.WriteLine("  Прямая параллельна оси 0Y.");
                }
            }
            else
                Console.WriteLine("  Пересечений с осями нет. ");
        }
        
        //Проверяем на перпендикулярность. Возвращаем FALSE, если НЕ перпендикулярны, иначе вернём TRUE
        //Статический метод, для удобства использования.
        public static bool Perpendicularity(Straight L1, Straight L2)
        {
            if (L1.flag != true && L2.flag != true)//Проверим на вырожденность - сушествуют ли обе прямые?
            {
                //Для большего упрощения задачи проверим перпендикулярность относитель оси координат 0X.
                double uL1, uL2, u;//Углы для первой и второй прямой. И разница углов.
                //Найдём угол для L1
                uL1 = Math.Abs(Math.Atan((-1) * L1.a / L1.b) * 180 / Math.PI);
                //Найдём угол для L2
                uL2 = Math.Abs(Math.Atan((-1) * L2.a / L2.b) * 180 / Math.PI);
                //Вычтем углы и найдем общий угол

                if (Math.Abs(uL1 - uL2) == 90)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        //Найдём угол между прямыми. Вернём его в градусах по тангенсу
        //Если прямые не перпендикулярны, то можно найти угол, иначе - в знаменателе формулы будет ноль. То есть угол = 90%
        //Статический метод, для удобства вызова и использования
        public static double AngleBetween(Straight L1, Straight L2)
        {
           if (L1.flag != true && L2.flag != true)
            {
                double uL1, uL2, u;//Углы для первой и второй прямой. И разница углов.
                uL1 = Math.Abs(Math.Atan((-1) * L1.a / L1.b) * 180 / Math.PI);
                uL2 = Math.Abs(Math.Atan((-1) * L2.a / L2.b) * 180 / Math.PI);

                return u = Math.Abs(uL1 - uL2);
            }
           return 0;//Иначе угла нет - возвращаем ноль. Т.к. в этом случае прямые не существуют.
         }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //Создаём первую прямую и выводим её в консоль - проверяем правильность
            Straight L1 = new Straight(-1, 2, -3);
            Console.WriteLine(L1);
            L1.IntersectionAxes();

Console.WriteLine();

            //Создаём вторую прямую и выводим её в консоль - проверяем правильность
            Straight L2 = new Straight(5,-4,6);
            Console.WriteLine(L2);
            L2.IntersectionAxes();

Console.WriteLine();

            //Проверим на перпендикулярность
            if (Straight.Perpendicularity(L1, L2) == false)
                Console.WriteLine("Прямые НЕ перпендикулярны. ");
            else
                Console.WriteLine("Прямые перпендикулярны. ");

Console.WriteLine();

            //Найдём угол между прямыми:
            double ugol;
            ugol = Straight.AngleBetween(L1, L2);
            Console.WriteLine("Угол между прямыми = "+ugol+" градусов");

Console.WriteLine("_______________________");
Console.WriteLine();
Console.WriteLine();
            //Создаём первую прямую и выводим её в консоль - проверяем правильность
            Straight L3 = new Straight(1, 2);
            Console.WriteLine(L3);
            L3.IntersectionAxes();

Console.WriteLine();

            //Создаём вторую прямую и выводим её в консоль - проверяем правильность
            Straight L4 = new Straight(5,0,6);
            Console.WriteLine(L4);
            L4.IntersectionAxes();

Console.WriteLine();

            //Проверим на перпендикулярность
            if (Straight.Perpendicularity(L3, L4) == false)
                Console.WriteLine("Прямые НЕ перпендикулярны. ");
            else
                Console.WriteLine("Прямые перпендикулярны. ");

Console.WriteLine();

            //Найдём угол между прямыми:
            ugol = Straight.AngleBetween(L3, L4);
            Console.WriteLine("Угол между прямыми = "+ugol+" градусов");

Console.WriteLine("_______________________");
Console.WriteLine();
Console.WriteLine();
            //Создаём первую прямую и выводим её в консоль - проверяем правильность
            Straight L5 = new Straight(-1, -2);
            Console.WriteLine(L5);
            L5.IntersectionAxes();

Console.WriteLine();

            //Создаём вторую прямую и выводим её в консоль - проверяем правильность
            Straight L6 = new Straight(0, 10, -6);
            Console.WriteLine(L6);
            L6.IntersectionAxes();

Console.WriteLine();

            //Проверим на перпендикулярность
            if (Straight.Perpendicularity(L5, L6) == false)
                Console.WriteLine("Прямые НЕ перпендикулярны. ");
            else
                Console.WriteLine("Прямые перпендикулярны. ");

Console.WriteLine();

            //Найдём угол между прямыми:
            ugol = Straight.AngleBetween(L5, L6);
            Console.WriteLine("Угол между прямыми = " + ugol + " градусов");


Console.WriteLine("_______________________");
Console.WriteLine();
Console.WriteLine();

            //Создаём первую прямую и выводим её в консоль - проверяем правильность
            Straight L7 = new Straight(-1, 0, 4);
            Console.WriteLine(L7);
            L7.IntersectionAxes();

Console.WriteLine();

            //Создаём вторую прямую и выводим её в консоль - проверяем правильность
            Straight L8 = new Straight(0, 10, -6);
            Console.WriteLine(L8);
            L8.IntersectionAxes();

Console.WriteLine();

            //Проверим на перпендикулярность
            if (Straight.Perpendicularity(L7, L8) == false)
                Console.WriteLine("Прямые НЕ перпендикулярны. ");
            else
                Console.WriteLine("Прямые перпендикулярны. ");

Console.WriteLine();

            //Найдём угол между прямыми:
            ugol = Straight.AngleBetween(L7, L8);
            Console.WriteLine("Угол между прямыми = " + ugol + " градусов");






            Console.ReadKey();
        }
    }
}
