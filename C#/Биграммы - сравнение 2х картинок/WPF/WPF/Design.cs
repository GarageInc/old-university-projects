using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections;

namespace WPF
{
    class Design
    {
        private const int ТаблЗум = 15;
        private const int количество_цветов = 1536;
        
        Grid Окно_шкалы = new Grid();
        Grid Градусник = new Grid();

        /// <summary>
        /// Длина стороны блока таблицы.
        /// </summary>
        public int Размер_сторон_блока_таблицы
        {
            get { return ТаблЗум; }
        }
        /// <summary>
        /// Значение 11 битого цвета.
        /// </summary>
        public int Фиолетовый_цвет
        {
            get { return количество_цветов; }
        }
        /// <summary>
        /// Количество используемых цветов.
        /// </summary>
        public int Количество_цветов
        {
            get { return количество_цветов; }
        }

        /// <summary>
        /// Создание блока для таблицы.
        /// </summary>
        /// <param name="горизонталь">Столбец по счету.</param>
        /// <param name="вертикаль">Строка по счету.</param>
        /// <param name="значение_цвета">Цвет блока, из расчета один из 65536 цветов.</param>
        /// <returns>Блок таблицы.</returns>
        public Rectangle Табличный_блок(int горизонталь, int вертикаль, int значение_цвета)
        {
            Rectangle rc = new Rectangle();
            rc.Width = ТаблЗум;
            rc.Height = ТаблЗум;
            rc.HorizontalAlignment = HorizontalAlignment.Left;
            rc.VerticalAlignment = VerticalAlignment.Top;
            rc.Fill = ColorRGB(значение_цвета);
            rc.Margin = new Thickness(ТаблЗум * горизонталь, ТаблЗум * вертикаль, 0, 0);
            return rc;
        }

        /// <summary>
        /// Создание текста для блока таблицы.
        /// </summary>
        /// <param name="горизонталь">Столбец по счету.</param>
        /// <param name="вертикаль">Строка по счету.</param>
        /// <param name="Текст">Текст, который будет находится в блоке таблицы.</param>
        /// <returns>Текст блока таблицы.</returns>
        public TextBlock Табличный_текст(int горизонталь, int вертикаль, char Текст)
        {            
            TextBlock tb = new TextBlock();
            tb.Name = "Label";
            tb.Text = "" + Текст;
            tb.Width = ТаблЗум;
            tb.Height = ТаблЗум;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.VerticalAlignment = VerticalAlignment.Top;
            tb.Padding = new Thickness(0);
            tb.TextAlignment = TextAlignment.Center;
            tb.FontSize = ТаблЗум - 3;
            tb.FontFamily = new FontFamily("Сonsolas");
            //tb.Background = Brushes.Violet; //New LinearGradientBrush(Colors.LightBlue, Colors.SlateBlue, 90);
            // tb.Background.Opacity = 0.7; 
            tb.Margin = new Thickness(ТаблЗум * горизонталь, ТаблЗум * вертикаль, 0, 0);
            return tb;
        }


        /// <summary>
        /// Определение цвета по числовому значение одного из 1536.
        /// </summary>
        /// <param name="a">Значение цвета.</param>
        /// <returns>Цветовая настройка для Fill.</returns>
        private SolidColorBrush ColorRGB(int a)
        {
            SolidColorBrush slb = new SolidColorBrush();
            switch (a / 256)
            {
                case 0: slb.Color = Color.FromArgb(255, В_RGB(a, false), В_RGB(a, false), 255); break;
                case 1: slb.Color = Color.FromArgb(255, 0, В_RGB(a, true), 255); break;
                case 2: slb.Color = Color.FromArgb(255, 0, 255, В_RGB(a, false)); break;
                case 3: slb.Color = Color.FromArgb(255, В_RGB(a, true), 255, 0); break;
                case 4: slb.Color = Color.FromArgb(255, 255, В_RGB(a, false), 0); break;
                case 5: slb.Color = Color.FromArgb(255, 255, 0, В_RGB(a, true)); break;
                default: slb.Color = Color.FromArgb(255, 255, 0, 255); break;
            }
            return slb;
        }
        /// <summary>
        /// Расчет порядкого номера цвета.
        /// </summary>
        /// <param name="переводимое_число">Значение, переводимое в цвет.</param>
        /// <param name="увеличение">Относительно позиции в 6 цветах.</param>
        /// <returns>Значение цвета в 256 цветовой гамме.</returns>
        private byte В_RGB(int переводимое_число, bool увеличение)
        {
            int RGB;
            if (увеличение)
            { RGB = переводимое_число - (переводимое_число / 256) * 256; }
            else
            { RGB = 255 - (переводимое_число - (переводимое_число / 256) * 256); }
            return Convert.ToByte(RGB);
        }

        private void Вывод_градусника(double Коэфициент, double Количество)
        {
            Окно_шкалы.Children.Clear();
            Окно_шкалы.Margin = new Thickness(0, 0, 20, 0);
            Окно_шкалы.Width = 30;
            Окно_шкалы.Height = 522;
            Окно_шкалы.HorizontalAlignment = HorizontalAlignment.Right;
            Окно_шкалы.VerticalAlignment = VerticalAlignment.Top;
            Градусник.Margin = new Thickness(0, 10, 40, 0);
            Градусник.Width = 43;
            Градусник.Height = 511;
            Градусник.HorizontalAlignment = HorizontalAlignment.Right;
            Градусник.VerticalAlignment = VerticalAlignment.Top;
            for (int вертикаль = 1; вертикаль < 511; вертикаль++)
            {
                int a = Convert.ToInt32((Convert.ToDouble(вертикаль) / 511) * количество_цветов);
                Rectangle rc = new Rectangle();
                rc.Height = 1;
                rc.HorizontalAlignment = HorizontalAlignment.Left;
                rc.VerticalAlignment = VerticalAlignment.Bottom;
                SolidColorBrush slb = new SolidColorBrush();

                if (Convert.ToDouble(вертикаль) % Convert.ToDouble(51) == 0)
                {
                    slb.Color = Color.FromArgb(255, 0, 0, 0);
                    rc.Width = 25;
                    TextBlock Шкала = new TextBlock();
                    Шкала.HorizontalAlignment = HorizontalAlignment.Left;
                    Шкала.VerticalAlignment = VerticalAlignment.Bottom;
                    Шкала.Name = "Label" + вертикаль;
                    double Значение_шкалы = Math.Round(((Convert.ToDouble(вертикаль) / Convert.ToDouble(51)) * 0.1) / Коэфициент, 4) * Количество;
                    Шкала.Text = "" + Значение_шкалы;
                    Шкала.Width = 60;
                    Шкала.Height = 15;
                    Шкала.Padding = new Thickness(0);
                    Шкала.TextAlignment = TextAlignment.Left;
                    Шкала.FontSize = 12;
                    Шкала.FontFamily = new FontFamily("Сonsolas");
                    Шкала.Margin = new Thickness(0, 0, 0, вертикаль - 8);
                    Окно_шкалы.Children.Add(Шкала);

                }
                else
                {
                    rc.Width = 19;
                    slb = ColorRGB(a);
                }
                rc.Fill = slb;
                rc.Margin = new Thickness(0, 0, 0, 1 * вертикаль);
                Градусник.Children.Add(rc);
            }
        }



        
    }
}
