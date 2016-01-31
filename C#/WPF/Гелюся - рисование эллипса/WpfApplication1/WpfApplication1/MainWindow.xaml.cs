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
            // Создадим панель, на которой эллипс разместится
           // StackPanel myStackPanel = new StackPanel();
            
            // Создадим элллипс
            Ellipse myEllipse = new Ellipse();

            // Создадим SolidColorBrush, который заполнит эллипс
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Зададим цвет  rgb, с четырьмя параметрами
            // каждое значение от 0 до 255
            Random r = new Random();
            mySolidColorBrush.Color = Color.FromArgb((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255), 0);// Заполняем случайным цветом
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;// Толщина линии
            myEllipse.Stroke = Brushes.Black;// Цвет линии

            // Получим позицию
            var pos = (e.GetPosition(null));//new TranslateTransform { X = e.GetPosition(this).X, Y = e.GetPosition(this).Y };
            
            int width = r.Next(10,400);
            int height = r.Next(10, 400);

            double X = pos.X;
            double Y = pos.Y;

            Console.WriteLine("X="+X+" Y="+Y);
            // Установим ширину и высоту эллипса
            myEllipse.Width = width;
            myEllipse.Height = height;
            myEllipse.Margin = new Thickness(X-width/2,Y-height/2,0,0);

            canv.Children.Add(myEllipse);
        }
    }
}


//Некоторые элементы, которые должны присутствовать в программе:
//Color.FromRgb
// радиус (10-50 Random)
//MouseDown (не MouseClick) че то там (Event Args, e)  и e.GetPozition(this)
