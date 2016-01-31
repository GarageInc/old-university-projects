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
    
    public partial class Window2 : Window
    {
        List<ПродуктЦена>Заказы = new List<ПродуктЦена>();

        // Передаем заказы
        public Window2(ref List<ПродуктЦена>Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }

        // Добавление нового заказа в окно вывода:
        private void КартФириБПорция_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картофель-фри(большая порция)");
        }

        private void КартФриМалПорция_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картофель-фри(маленькая порция)");
        }

        private void КартФриСредПорц_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картофель-фри(средняя порция)");
        }

        private void КартПоДерев_Click(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картофель по-деревенски");
        }

        // Кнопка отмены
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (ОкноВывода.Items.Count > 0)
                ОкноВывода.Items.Remove(ОкноВывода.Items[ОкноВывода.Items.Count-1]);
        }

        // Добавим из окна вывода в заказы
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in ОкноВывода.Items)
            {
                Заказы.Add(new ПродуктЦена(item.ToString(), 150));
            }
            // Закрываем окно
            this.Close();
        }
    }
}
