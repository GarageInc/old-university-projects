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
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        List<ПродуктЦена> Заказы = new List<ПродуктЦена>();

        // Передаем заказы
        public Window4(ref List<ПродуктЦена> Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }
        
        // Перевод из ОкнаВывода в Заказы
        private void Ок_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ОкноВывода.Items)
            {
                Заказы.Add(new ПродуктЦена(item.ToString(), 50));
            }
            // Закрываем окно
            this.Close();
        }

        // Отмена заказа
        private void Отмена_Click(object sender, RoutedEventArgs e)
        {
            if (ОкноВывода.Items.Count > 0)
                ОкноВывода.Items.Remove(ОкноВывода.Items[ОкноВывода.Items.Count - 1]);
        }

        // Добавление напитков в заказы
        private void Кока_кола_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Кока-кола " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void Фанта_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Фанта " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void Спрайт_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Спрайт " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void Чай_Click(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Чай " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }


    }
}
