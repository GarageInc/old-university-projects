using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    abstract class Partner<T>
    {
        public abstract T Debt(); // задолженность

        protected string _name; // имя партнёра

        public string Name
        {
            get { return _name; } 
            set { _name = value; }
        }

        // переопределение операторов сравнения
        public static bool operator <(Partner<T> l, Partner<T> r)
        {
            return (double)(object)l.Debt() < (double)(object)r.Debt();
        }

        public static bool operator >(Partner<T> l, Partner<T> r)
        {
            return (double)(object)l.Debt() > (double)(object)r.Debt();
        }
    }

    // с фикс. ставкой
    class PartnerFixed<T> : Partner<T>
    {
        private T _rate; // ставка
        public T Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        public PartnerFixed()
        {
        }

        public PartnerFixed(string name, T rate)
        {
            Name = name;
            Rate = rate;
        }

        public override T Debt()
        {
            return Rate;
        }
    }

    // часовая ставка
    class PartnerHour<T> : Partner<T>
    {
        private T _rate; // ставка
        public T Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        private int _hours; // отработанные часы
        public int Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }

        public PartnerHour()
        {
        }

        public PartnerHour(string name, T rate, int hours)
        {
            Name = name;
            Rate = rate;
            Hours = hours;
        }

        public override T Debt()
        {
            // если тип T - int
            if (typeof(T).ToString().Equals("System.Int32"))
            {
                int intRate =  (int)(object)Rate; // преобразование T -> object -> int 
                T result = (T)(object)(intRate*Hours); // преобразование int -> object -> T    
                return result;
            }
            else if (typeof(T).ToString().Equals("System.Double"))
            {
                double intRate =  (double)(object)Rate; 
                T result = (T)(object)(intRate*Hours);
                return result;    
            }
            else
            {
                throw new Exception("Неверный тип!");
            }
        }
    }

    // класс, хранящий и считывающий из файла список партнёров
    class PartnerList<T>
    {
        private List<Partner<T>> _partners = new List<Partner<T>>();  // список партнёров
        public List<Partner<T>> Partners { get { return _partners; } }

        public void ReadPartners(String fileName)
        {
            StreamReader reader = new StreamReader(fileName); // открытие потока чтения
            String line = reader.ReadLine();
            while (line != null)                // считывание до конца файла
            {
                String[] values = line.Split(' '); // разделение одной строки на массив строк по пробелам
                if (values[1].Equals("Тип(1)"))
                {
                    // Наименование Тип(1) ФиксСтавка
                    string name = values[0];
                    T rate = (T) (object) double.Parse(values[2]); 
                    Partners.Add(new PartnerFixed<T>(name, rate));
                }
                else if (values[1].Equals("Тип(2)"))
                {
                    // Наименование Тип(2) ЧасСтавка КолвоОтрабЧасов
                    string name = values[0];
                    T rate = (T)(object)double.Parse(values[2]);
                    int hours = (int)double.Parse(values[3]); 
                    Partners.Add(new PartnerHour<T>(name, rate, hours));
                }
                else
                {
                    throw new Exception("Некорректная строка!");
                }

                line = reader.ReadLine();
            }
        }

        // Найти в списке максимальный по задолженности элемент.
        public void PrintMax()
        {
            Partner<T> max = Partners[0];
            foreach (var partner in Partners)
            {
                if (max < partner)
                {
                    max = partner;
                }
            }
            Console.WriteLine(max.Name + " " + max.Debt());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PartnerFixed<int> p1 = new PartnerFixed<int>();
            p1.Rate = 10;
            Console.WriteLine(p1.Debt());

            PartnerHour<double> p2 = new PartnerHour<double>();
            p2.Rate = 15.2;
            p2.Hours = 21;
            Console.WriteLine(p2.Debt());

            PartnerList<double> list = new PartnerList<double>();
            list.ReadPartners(@"partners.txt");
            list.PrintMax();

            Console.ReadKey();
        } 
    }
}
