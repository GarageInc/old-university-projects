using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace zad16
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader rd = new StreamReader("numbers.txt");//Файл для считывания данных
            StreamWriter wr = new StreamWriter("result.txt");//Файл для записи результатов
            int q = Visualisation.Initialize();//Выбираем систему счисления с которой работаем
            Scalenum scl = new Scalenum(q);
            Scalenum scl1 = new Scalenum(q);
            int n = 0;
            while (n != 5)
            {
                n = Visualisation.Choice();//Выбираем выполняемое действие
                if (n != 5)//Если не выбрали выход
                {
                    Visualisation.Read(ref scl, rd);
                    Visualisation.Read(ref scl1, rd);
                    wr.WriteLine("Первое число:");
                    Visualisation.Write(scl, wr);
                    wr.WriteLine("Второе число:");
                    Visualisation.Write(scl1, wr);
                    switch(n)//Выполняем требуемое действие
                    {
                        case 1:
                        {
                            Scalenum s = scl + scl1;
                            wr.WriteLine("Результат:");
                            Visualisation.Write(s, wr);
                        }
                        break;
                        case 2:
                        {
                            Scalenum s = scl * scl1;
                            wr.WriteLine("Результат:");
                            Visualisation.Write(s, wr);
                        }
                        break;
                        case 3:
                        {
                            Scalenum s = scl % scl1;
                            wr.WriteLine("Результат:");
                            Visualisation.Write(s, wr);
                        }
                        break;
                        case 4:
                        {
                            scl.Insert(scl1);
                            wr.WriteLine("Результат:");
                            Visualisation.Write(scl, wr);
                        }
                        break;
                    }
                }
            }
            wr.Close();
        }
    }
}
