using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EK_5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Вводим игрек y
            Console.WriteLine("Введите число 'y'= ");
            double y = double.Parse(Console.ReadLine());//Считываем с ввода тип double. //Чтобы не было конфликта с Math, т.к. она требует значение double. И может быть дробное значение в результате вычислений

            //Выбор действия по условиям
            if (y < 0)
            {                
                y = Math.Sqrt(Math.Abs(Math.Sin(3.1415926 * y)));//Приближенное значение, т.к. Math.Sin(y*Math.PI) - даёт весьма странный результат
            }
            else
            {
                if (y > 3)
                {
                    y = Math.Pow(y, 4) + 1;
                }
                else
                {
                    y=1+(Math.Abs(y+1))/(y+2);//Если y=[0,3] можно было не брать модуль. Но модуль есть в условии задачи и он взят.
                }
            }

            Console.WriteLine("Ответ: y = " + y);
            Console.ReadKey();
        }
    }
}
