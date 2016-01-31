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
    
    public partial class ОкноКартофеля : Window
    {
        List<Заказ>Заказы = new List<Заказ>();

        // Передаем заказы
        public ОкноКартофеля(ref List<Заказ>Заказы)
        {
            InitializeComponent();
            this.Заказы = Заказы;
        }

        // Добавление нового заказа в окно вывода:
        private void F1(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картошка_по_деревенски(большая порция)");
        }

        private void F2(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картошка_по_деревенски(маленькая порция)");
        }

        private void F3(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картошка_по_деревенски(средняя порция)");
        }

        private void F4(object sender, RoutedEventArgs e)
        {
            ОкноВывода.Items.Add("Картофель_в_мундире");
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
                Заказы.Add(new Заказ(item.ToString(), 75));
            }
            // Закрываем окно
            this.Close();
        }
    }
}
