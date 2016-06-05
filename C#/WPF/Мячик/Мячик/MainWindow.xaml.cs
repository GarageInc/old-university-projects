using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_GameBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Thickness стартМяча = new Thickness(116, 160, 0, 0);
        Thickness текущаяПозицияМяча;
        Thickness следующаяПозицияМяча = new Thickness();

        Thickness стартоваяПозицияПлатформы = new Thickness(200, 390, 0, 0);
        Thickness текущаяПозицияПлатформы;

        ThicknessAnimation анимацияДвиженияМяча;
        ThicknessAnimation анимацияДвиженияПлатформы;

        Storyboard анимацияМяча = new Storyboard();
        Storyboard анимацияПлатформы;

        private void НачатьИгру(object sender, RoutedEventArgs e)
        {
            текущаяПозицияМяча = стартМяча;
            следующаяПозицияМяча.Left = полеИгры.Width;
            следующаяПозицияМяча.Top = полеИгры.Height;
            ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);

            полеИгры.KeyDown += new KeyEventHandler(полеИгры_KeyDown);
            мяч.LayoutUpdated += new EventHandler(мяч_LayoutUpdated);
        }

        void мяч_LayoutUpdated(object sender, EventArgs e)
        {
            текущаяПозицияМяча = мяч.Margin;

            if (((мяч.Margin.Top - мяч.Height + 20) >= платформа.Margin.Top) && мяч.Margin.Left >= (платформа.Margin.Left - 30) && мяч.Margin.Left <= (платформа.Margin.Left + 30))
            {
                следующаяПозицияМяча.Top = 0;
                следующаяПозицияМяча.Left = текущаяПозицияМяча.Left - 200;
                ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);
            }
            else if (((мяч.Margin.Top - мяч.Height + 20) >= платформа.Margin.Top) && мяч.Margin.Left >= (платформа.Margin.Left + 30) && мяч.Margin.Left <= (платформа.Margin.Left + 60))
            {
                следующаяПозицияМяча.Top = 0;
                ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);
            }
            else if (((мяч.Margin.Top - мяч.Height + 20) >= платформа.Margin.Top) && мяч.Margin.Left >= (платформа.Margin.Left + 60) && мяч.Margin.Left <= (платформа.Margin.Left + 100))
            {
                следующаяПозицияМяча.Top = 0;
                следующаяПозицияМяча.Left = текущаяПозицияМяча.Left + 200;
                ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);
            }
            else if (мяч.Margin.Top <= 25)
            {
                следующаяПозицияМяча.Top = полеИгры.Height;
                ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);
            }
            else if (мяч.Margin.Left <= 0)
            {
                следующаяПозицияМяча.Left = полеИгры.Width;
                ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);
            }
            else if (мяч.Margin.Left >= полеИгры.Width - 50)
            {
                следующаяПозицияМяча.Left = 0;
                ЗапускАнимацииМяча(следующаяПозицияМяча, текущаяПозицияМяча);
            }
            else if (мяч.Margin.Top >= платформа.Margin.Top + мяч.Height)
            {
                MessageBox.Show("Игра проиграна!");
                Application.Current.Shutdown();
            }
        }

        void ЗапускАнимацииМяча(Thickness next, Thickness current)
        {
            анимацияДвиженияМяча = new ThicknessAnimation();
            анимацияДвиженияМяча.From = current;
            анимацияДвиженияМяча.To = next;
            анимацияДвиженияМяча.Duration = new Duration(TimeSpan.FromSeconds(3));
            Storyboard.SetTargetName(анимацияДвиженияМяча, "мяч");
            Storyboard.SetTargetProperty(анимацияДвиженияМяча, new PropertyPath(Rectangle.MarginProperty));

            if (анимацияМяча.Children.Count > 0)
                анимацияМяча.Children.RemoveAt(0);
            анимацияМяча.Children.Add(анимацияДвиженияМяча);
            анимацияМяча.Begin(this, true);
        }

        void полеИгры_KeyDown(object sender, KeyEventArgs e)
        {
            текущаяПозицияПлатформы = платформа.Margin;
            double сдвиг = 0;
            if (e.Key == Key.Left)
                if (платформа.Margin.Left > -100)
                    сдвиг = -50;
            if (e.Key == Key.Right)
                if (платформа.Margin.Left <= (полеИгры.Width - платформа.Width))
                    сдвиг = 50;

            ЗапускАнимацииПлатформы(сдвиг);
        }

        void ЗапускАнимацииПлатформы(double x)
        {
            анимацияДвиженияПлатформы = new ThicknessAnimation();
            анимацияДвиженияПлатформы.From = текущаяПозицияПлатформы;
            анимацияДвиженияПлатформы.To = new Thickness(платформа.Margin.Left + x, платформа.Margin.Top, 0, 0);
            анимацияДвиженияПлатформы.Duration = new Duration(TimeSpan.FromSeconds(0));
            Storyboard.SetTargetName(анимацияДвиженияПлатформы, "платформа");
            Storyboard.SetTargetProperty(анимацияДвиженияПлатформы, new PropertyPath(Rectangle.MarginProperty));

            анимацияПлатформы = new Storyboard();
            if (анимацияПлатформы.Children.Count > 0)
                анимацияПлатформы.Children.RemoveAt(0);
            анимацияПлатформы.Children.Add(анимацияДвиженияПлатформы);
            анимацияПлатформы.Begin(this, true);
        }
        


        private void ЗавершитьИгру(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
