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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class ОкноДесертов : Window
    {
        List<Заказ> Заказы = new List<Заказ>();

        // Передаем заказы
        public ОкноДесертов(ref List<Заказ> Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }

        // Добавление продукции
        private void Конфеты_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Конфеты");
        }

        private void Мороженое_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Мороженое");
        }

        private void МорожСАнанасами_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Мороженое_с_ананасами");
        }

        private void Вафли_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Вафли");
        }

        private void ПирожокСПов_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Пирожок_с_повидлом");
        }

        private void ПирСКремом_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Пирожок_с_кремом");
        }

        // Добавление из ОкнаВывода в Заказы
        private void Ок_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ОкноВывода.Items)
            {
                Заказы.Add(new Заказ(item.ToString(), 60));
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



    }
}
