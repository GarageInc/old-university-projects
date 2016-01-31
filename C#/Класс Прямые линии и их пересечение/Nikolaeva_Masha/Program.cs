using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nikolaeva_Masha
{
    class Line
    {
        public int a, b, c;//Приватные коэффициенты уравнения линии a*x+b*y+c=0

        //Конструктор по умолчанию
        public Line(int a = 0, int b = 0, int c = 0)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        //Отображение в требуемом виде
        public void Display()
        {
            string s1 = "", s2 = "", s3 = "";

            //Регулируем отображение плюсиков + :

            if (a != 0) s1 += a + "*x"; ;

            if (a != 0 && b != 0) s2 += "+" + b + "*y";
            else
            s2 += b + "*y"; ;

            if ((a != 0 || b != 0) && c != 0) s3 += "+" + c; 
            else
            s3 += "+" + c; ;

            Console.WriteLine(s1 + s2 + s3 + "=0");
        }

        //Перегрузка * - на пересечение линий
        //false - не пересекаются
        public static  bool operator  *(Line L1, Line L2)
        {
            //Прямые - пересекаются, если они - не параллельны. То есть угол наклона к оси X - должен быть разным.
            //Но прямые могут и накладываться друг на друга!
            //ЕСЛИ ПРЯМЫЕ СОВПАДАЮТ - ОНИ ПЕРЕСЕКАЮТСЯ(пересечение=наложению?)

            if ( L1.a / L1.b == L2.a / L2.b)
            {
                if (L1.c == L2.c)//Если накладываются - пересекаются значит
                    return true;
                else
                    return false;
            }
            else return true;
            
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            //Создаем прямые
            Line L1 = new Line(1, 2, 3);
            Line L2 = new Line(1, 2, 3);

            //Отображаем прямые
            L1.Display();
            L2.Display();

            //Выводим результат их пересечения
            Console.WriteLine(L1 * L2);

            Console.ReadKey();
        }
    }
}
