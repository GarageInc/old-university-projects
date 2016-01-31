using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication6
{
    public partial class Window1 : Window
    {
        List<ПродуктЦена>Заказы = new List<ПродуктЦена>();

        // Передаем заказы
        public Window1(ref List<ПродуктЦена> Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }

        // Добавление заказов
        private void БигМак_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res="Биг Мак "+Объем.SelectionBoxItem;
            if(Соус.IsChecked==true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        // Добавим из окна вывода в заказы
        private void Ок_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ОкноВывода.Items)
            {
                Заказы.Add(new ПродуктЦена(item.ToString(),100));
            }
            // Закрываем окно
            this.Close();
        }

        // Отмена заказа
        private void Отмена_Click(object sender, RoutedEventArgs e)
        {
            if (ОкноВывода.Items.Count>0)
                ОкноВывода.Items.Remove(ОкноВывода.Items[ОкноВывода.Items.Count - 1]);
        }

        private void РоялЧизбургер_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Роял Чизбургер " + Объем.SelectionBoxItem;
            if (Соус.IsChecked == true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void ЧикенБекон_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Чикен Бекон " + Объем.SelectionBoxItem;
            if (Соус.IsChecked == true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void БифРолл_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Биф Ролл " + Объем.SelectionBoxItem;
            if (Соус.IsChecked == true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        
    }
}
