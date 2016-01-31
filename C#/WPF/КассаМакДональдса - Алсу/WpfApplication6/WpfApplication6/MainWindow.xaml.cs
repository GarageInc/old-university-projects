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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication6
{
    // Основной класс
    public class ПродуктЦена
    {
        public int цена;
        public string product;

        // Конструктор
        public ПродуктЦена(string прод, int ц)
        {
            this.product = прод;
            this.цена = ц;
        }
    }

    public partial class MainWindow : Window
    {
        // В этот список будем складывать наши заказы
        List<ПродуктЦена> Заказы = new List<ПродуктЦена>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Напечатаем окно заказа:
        private void Итого_Click(object sender, RoutedEventArgs e)
        {
            // Очистим окно и заново добавим,указав для каждого продукта:сколько его заказали
            if (Заказы.Count>0)
            {
                var новыеЗаказы = new List<ПродуктЦена>();

                foreach (var заказ in Заказы)
                {
                    if (новыеЗаказы.Count(x => x == заказ) == 0)
                        новыеЗаказы.Add(заказ);
                }

                ОкноВывода.Items.Clear();
                int sum = 0;
                foreach (var заказ in новыеЗаказы)
                {
                    var количетсво = Заказы.Count(x => x == заказ);
                    sum += заказ.цена * количетсво;
                    ОкноВывода.Items.Add("-> " + заказ.product + "(" + заказ.цена + "р.): " + количетсво + "штук(и) = " + заказ.цена * количетсво);
                }
                ОкноВывода.Items.Add("Сумма к оплате: " + sum + "р.");
            }
            else
            {
                    ОкноВывода.Items.Add("Заказы отсутствуют");
            }
        }

        // Кнопки для каждого окна
        private void Сэндвичи_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1(ref Заказы);
            w.Show();
        }

        private void Картофель_Click(object sender, RoutedEventArgs e)
        {
            Window2 w = new Window2(ref Заказы);
            w.Show();
        }

        private void Десерты_Click(object sender, RoutedEventArgs e)
        {
            Window3 w = new Window3(ref Заказы);
            w.Show();
        }

        private void Напитки_Click(object sender, RoutedEventArgs e)
        {
            Window4 w = new Window4(ref Заказы);
            w.Show();
        }

        // Отменить весь заказ
        private void ОтменитьВесьЗаказ_Click(object sender, RoutedEventArgs e)
        {
            Заказы.Clear();
        }
    }
}
