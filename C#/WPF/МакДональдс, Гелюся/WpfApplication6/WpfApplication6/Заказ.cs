using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication6
{
    // Основной класс
    public class Заказ
    {
        public int цена;
        public string продукт;

        // Конструктор
        public Заказ(string прод, int ц)
        {
            this.продукт = прод;
            this.цена = ц;
        }
    }

}
