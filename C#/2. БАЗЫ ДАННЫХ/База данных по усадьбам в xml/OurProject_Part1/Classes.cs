using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OurProject_Part1
{
    // Класс Дом
    class House
    {
        // Ставка процента - величина налога
        public double величинаНалога;
        
        // Конструктор
        public House(double d=0.0)
        {
            this.величинаНалога = d;
        }

    }

    // Класс Гараж
    class Garage
    {
        // Ставка процента - величина налога
        public double величинаНалога;
        
        // Конструктор
        public Garage(double d=0.0)
        {
            this.величинаНалога = d;
        }
    }
    
    // Класс усадьба
    class Homestead
    {
        // Определим поля по заданию:
        public Garage гараж;
        public House дом;
        public string адрес;
        
        // Конструктор усадьбы по умолчанию
        public Homestead()
        {
            this.гараж = new Garage();
            this.дом = new House();
            this.адрес = "";
        }
        // Конструктор усадьбы с параметрами
        public Homestead(string a, Garage g, House h)
        {
            this.гараж = g;
            this.дом = h;
            this.адрес = a;
        }
        public Homestead(string a, Garage g)
        {
            this.гараж = g;
            this.дом = new House();
            this.адрес = a;
        }
        public Homestead(string a, House h)
        {
            this.гараж = new Garage();
            this.дом = h;
            this.адрес = a;
        }
    }

}
