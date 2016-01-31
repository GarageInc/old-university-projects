using System;
using System.Collections.Generic;
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

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         
        public MainWindow()
        {
            InitializeComponent();
            
        }
       

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Создадим элллипс
            Ellipse эллипс = new Ellipse();

            // Создадим SolidColorBrush, который заполнит эллипс
            SolidColorBrush цветЭллипса = new SolidColorBrush();

            // Задаётся цвет случайными значениями от 100
            Random r = new Random();
            цветЭллипса.Color = Color.FromArgb((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255), 0);// Заполняем случайным цветом
            эллипс.Fill = цветЭллипса;
            эллипс.StrokeThickness = 1;// Толщина линии
            эллипс.Stroke = Brushes.Red;// Цвет линии красный

            // Установим ширину и высоту эллипса - случайные числа от 50 до 700
            int ширина = r.Next(50,700);
            int высота = r.Next(50, 700);

            // Установим координаты центра - это те координаты, которые получились в результате клика мышью
            double X = (e.GetPosition(null)).X;
            double Y = (e.GetPosition(null)).Y;

            // Установим ширину и высоту эллипса
            эллипс.Width = ширина;
            эллипс.Height = высота;
            // И поставим эллипс в координаты
            эллипс.Margin = new Thickness(X-ширина/2,Y-высота/2,0,0);

            // На нашу канву добавим новый созданный эллипс
            canv.Children.Add(эллипс);
        }
    }
}


//Некоторые элементы, которые должны присутствовать в программе:
//Color.FromRgb
// радиус (10-50 Random)
//MouseDown (не MouseClick) че то там (Event Args, e)  и e.GetPozition(this)
