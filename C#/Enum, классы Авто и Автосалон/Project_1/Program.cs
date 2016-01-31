using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_1
{
    //Перечисляемые объекты:
    enum Manufacturer{Honda, Peugeot, Ford, Mersedes}
    enum Color {Red, Green, Blue, White}

    //Класс авто
    class Auto
    {
        public int price;//Цена
        public string manufacturer;//Производитель
        public string color;//Цвет
        //Конструктор по умолчанию - цена, производитель и цвет
        public Auto(int price, Manufacturer manuf, Color col)
        {
            this.price = price;
            this.manufacturer = manuf.ToString();
            this.color = col.ToString();
        }

        //Отображение
        public void Dispay()
        {
            Console.WriteLine("Auto: price {0}, manufacturer {1}, color {2}", price, manufacturer, color);
        }

        //Сравнение двух авто
        public static bool operator ==(Auto a1, Auto a2)
        {
            //Сравнение по всем параметрам - по цене, производителю и цвету
            if (a1.price == a2.price && a1.color.CompareTo(a2.color) == 0 && a2.manufacturer.CompareTo(a1.manufacturer) == 0)
                return true;
            else return false;
        }
        //Для оператора == должен быть определ оператор !=
        public static bool operator !=(Auto a1, Auto a2)
        {
            //Сравнение по всем параметрам - по цене, производителю и цвету
            if (a1.price != a2.price || a1.color.CompareTo(a2.color) != 0 || a2.manufacturer.CompareTo(a1.manufacturer) != 0)
                return true;
            else return false;
        }
    }

    //Класс автосалон
    class AutoSalon
    {
        //Массив автомобилей. Лучше было бы определить - список List
        public Auto[] auto;

        //Конструктор без параметров
        public AutoSalon()
        {
            //Первоначально - в автосалоне нет авто - то есть их 0
            auto = new Auto[0];
        }

        //Метод добавления нового авто(сведений о нём) в автосалон
        public void Additions(Auto a)
        {
            //Т.к. старый массив - это n элементов, то новый будет n+1. Переписываем!
            Auto[] auto2=new Auto[auto.Length+1];

            //Копируем из старого массива в новый
            int i = 0;
            for (; i < auto.Length; i++)
                auto2[i] = auto[i];
            //Добавляем ещё один элемент-тот автомобиль, который нужно ещё добавить
            auto2[i] = a;
            //Переводим ссылку с нового массива на старый:
            auto = auto2;
            //Выполнено
        }

        //Метод Test - проверка наличия авто в автосалоне
        //Основан - на методе сравнения == из Auto
        public bool Test(Auto a)//Передаем авто
        {
            //Если авто нашлось - сразу возвращаем True
            //Если нет - в конце False
            foreach (Auto am in auto)
            {
                if (am == a) return true;
            }
            return false;
        }

        //Метод отображения всех авто в автосалоне
        public void Display()
        {
            foreach (Auto a in auto)
            {
                a.Dispay();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Вузализируем работу:

            //Создаем автосалон
            AutoSalon autosalon = new AutoSalon();
            
            //Добавляем авто!
            autosalon.Additions(new Auto(1000000, Manufacturer.Ford, Color.Blue));
            autosalon.Additions(new Auto(2000000, Manufacturer.Honda, Color.Green));
            autosalon.Additions(new Auto(3000000, Manufacturer.Mersedes, Color.Red));
            autosalon.Additions(new Auto(4000000, Manufacturer.Peugeot, Color.White));

            //Отображаем информацию обо всех авто:
            Console.WriteLine("AUTOSALON:");
            autosalon.Display();
            
            //Проверим - есть ли такие авто в автосалоне? False - нет авто, True - есть авто 
            Console.WriteLine();
            Auto a1=new Auto(2000000, Manufacturer.Peugeot, Color.Red);
            a1.Dispay();
            Console.WriteLine(autosalon.Test(a1));
            
            Auto a2=new Auto(4000000, Manufacturer.Peugeot, Color.White);
            a2.Dispay();
            Console.WriteLine(autosalon.Test(a2));




            Console.ReadLine();
        }
    }
}
