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
    public partial class Window3 : Window
    {
        List<ПродуктЦена> Заказы = new List<ПродуктЦена>();

        // Передаем заказы
        public Window3(ref List<ПродуктЦена> Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }

        // Добавление продукции
        private void МШоколадное_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Мороженое шоколадное");
        }

        private void МКарамельное_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Мороженое карамельное");
        }

        private void МКлубничное_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Мороженое клубничное");
        }

        private void ВРожок_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Вафельный рожок");
        }

        private void ВПирожок_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Вафельный пирожок");
        }

        private void МсШоколадом_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Маффин с шоколадом");
        }

        // Добавление из ОкнаВывода в Заказы
        private void Ок_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ОкноВывода.Items)
            {
                Заказы.Add(new ПродуктЦена(item.ToString(), 120));
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
