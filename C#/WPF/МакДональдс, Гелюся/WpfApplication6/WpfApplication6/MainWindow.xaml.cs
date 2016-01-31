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
    
    public partial class MainWindow : Window
    {
        // В этот список будем складывать наши заказы
        List<Заказ> СписокЗаказов = new List<Заказ>();

        public MainWindow()
        {
            InitializeComponent();
            ОкноВывода.IsEnabled = false;
        }

        // Напечатаем окно заказа:
        private void НапечататьЗаказы(object sender, RoutedEventArgs e)
        {
            // Очистим окно и заново добавим,указав для каждого продукта:сколько его заказали
            if (СписокЗаказов.Count>0)
            {
                var новыеЗаказы = new List<Заказ>();

                foreach (var заказ in СписокЗаказов)
                {
                    if (новыеЗаказы.Count(x => x == заказ) == 0)
                        новыеЗаказы.Add(заказ);
                }

                ОкноВывода.Items.Clear();
                int sum = 0;
                foreach (var заказ in новыеЗаказы)
                {
                    var количетсво = СписокЗаказов.Count(x => x == заказ);
                    sum += заказ.цена * количетсво;
                    ОкноВывода.Items.Add(" " + заказ.продукт + "(" + заказ.цена + "р.): " + количетсво + "штук(и) = " + заказ.цена * количетсво);
                }
                ОкноВывода.Items.Add("Сумма: " + sum + "р.");
            }
            else
            {
                    ОкноВывода.Items.Add("Заказов нет!");
            }
        }

        // Кнопки для каждого окна
        private void Сэндвичи_Click(object sender, RoutedEventArgs e)
        {
            ОкноСендвичей w = new ОкноСендвичей(ref СписокЗаказов);
            w.Show();
        }

        private void Картофель_Click(object sender, RoutedEventArgs e)
        {
            ОкноКартофеля w = new ОкноКартофеля(ref СписокЗаказов);
            w.Show();
        }

        private void Десерты_Click(object sender, RoutedEventArgs e)
        {
            ОкноДесертов w = new ОкноДесертов(ref СписокЗаказов);
            w.Show();
        }

        private void Напитки_Click(object sender, RoutedEventArgs e)
        {
            ОкноНапитков w = new ОкноНапитков(ref СписокЗаказов);
            w.Show();
        }

        // Отменить весь заказ
        private void ОтменитьВесьЗаказ_Click(object sender, RoutedEventArgs e)
        {
            // просто очищается список
            СписокЗаказов.Clear();
            ОкноВывода.Items.Add("Список заказов пуст");
        }
    }
}
