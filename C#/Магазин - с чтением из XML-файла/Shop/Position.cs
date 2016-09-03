using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    [Serializable]
    public class Position
    {
        public int Count { get; set; }

        public double Price { get; set; }

        public Position()
        {

        }

        public Position(int count, double price)
        {
            Count = count;
            Price = price;
        }
    }
}
