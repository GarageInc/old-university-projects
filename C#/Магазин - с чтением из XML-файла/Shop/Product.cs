using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    // Класс товаров с ценами
    [Serializable]
    public class Product
    {
        // Название
        public string Name { get; set; }

        // Цены за штуки
        public List<Position> Positions { get; set; }

        // Конструктор

        public Product(string name)
        {
            Name = name;

            Positions = new List<Position>();
        }

        public Product()
        {
            Positions = new List<Position>();
        }
    }
}
