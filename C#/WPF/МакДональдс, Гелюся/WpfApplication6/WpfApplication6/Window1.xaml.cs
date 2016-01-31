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
    public partial class ОкноСендвичей : Window
    {
        // В этот список будут ложиться все заказы
        List<Заказ>Заказы = new List<Заказ>();

        // Передаем заказы
        public ОкноСендвичей(ref List<Заказ> Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }

        // Добавление заказов
        private void F1(object sender, RoutedEventArgs e)
        {
            string res = "";
            res="Большой_Бутерброд "+Объем.SelectionBoxItem;
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
                Заказы.Add(new Заказ(item.ToString(),150));
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

        private void F2(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Бутерброд_с_колбасой " + Объем.SelectionBoxItem;
            if (Соус.IsChecked == true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void F3(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Бутерброд_с_сыром " + Объем.SelectionBoxItem;
            if (Соус.IsChecked == true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        private void F4(object sender, RoutedEventArgs e)
        {
            string res = "";
            res = "Сендвич_английский " + Объем.SelectionBoxItem;
            if (Соус.IsChecked == true)
            {
                res += " с соусом";
            }
            ОкноВывода.Items.Add(res.ToString());
        }

        
    }
}
