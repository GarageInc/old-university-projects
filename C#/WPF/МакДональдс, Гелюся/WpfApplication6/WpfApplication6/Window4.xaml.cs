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
    public partial class ОкноНапитков : Window
    {
        List<Заказ> Заказы = new List<Заказ>();

        // Передаем заказы
        public ОкноНапитков(ref List<Заказ> Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }
        
        // Перевод из ОкнаВывода в Заказы
        private void Ок_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ОкноВывода.Items)
            {
                Заказы.Add(new Заказ(item.ToString(), 50));
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
        private void F1(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "КокаКола " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void F2(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Фанта " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void F3(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Спрайт " + Объем.SelectionBoxItem;
            if (Лёд.IsChecked == true)
            {
                res += " со льдом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void F4(object sender, RoutedEventArgs e)
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
