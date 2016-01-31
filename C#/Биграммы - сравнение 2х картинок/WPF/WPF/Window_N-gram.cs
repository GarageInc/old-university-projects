using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WPF
{
    class Window_N_gram
    {
        private Design Дизайн = new Design();
        private int Size;
        private int WidthSize=0;
        private Window win = new Window();
        private Grid div0 = new Grid();
        private Grid divTable = new Grid();
        private Grid divContentTable = new Grid();
        private Grid Окно_шкалы = new Grid();
        private Grid Градусник = new Grid();
        private int[,] ТаблицаДляСравнения;

        private TextBlock Info = new TextBlock();
        StringEditor Мышь = new StringEditor();

        private ArrayList ABCD = new ArrayList(2);
        private int[,] tabl;
        private double m = 1;

        private void ARR(ArrayList Алфавит, Array Значения_Ngramm, double Количество_символов)
        {
            foreach (char ABC in Алфавит)
            {
                ABCD.Add(ABC);
            }
            tabl = new int[Алфавит.Count, Алфавит.Count];
            for (int i = 0; i < Алфавит.Count; i++)
            {
                for (int j = 0; j < Алфавит.Count; j++)
                {
                    tabl[i, j] = (int)Значения_Ngramm.GetValue(i, j);
                }
            }

            m = Количество_символов;
        }

        public Window_N_gram(ArrayList Алфавит, Array Значения_Ngramm, int Коэффициент, double Количество_символов)
        {
            ARR(Алфавит, Значения_Ngramm, Количество_символов);
            Вывод_Info();
            Оглавление_таблицы(Алфавит);
            Значения_таблицы(Алфавит.Count, Значения_Ngramm, Коэффициент, Количество_символов);
             
            divTable.Children.Add(divContentTable);
            div0.Children.Add(Info);
            div0.Children.Add(divTable);
            div0.Children.Add(Окно_шкалы);
            div0.Children.Add(Градусник);

            div0.Width = WidthSize;
            div0.Height = Size;
            win.Width = WidthSize;
            win.Height = Size;
            win.Content = div0;
            
            win.Show();

        }

        public Window_N_gram()
        {
        }

        /// <summary>
        /// Заполнение первоначальных данных таблицы.
        /// </summary>
        /// <param name="Алфавит">Значения первого столбца и строки.</param>
        private void Оглавление_таблицы(ArrayList Алфавит)
        {
            int i = 1;
            foreach (char ABC in Алфавит)
            {
                divTable.Children.Add(Дизайн.Табличный_блок(i, 0, Дизайн.Фиолетовый_цвет));
                divTable.Children.Add(Дизайн.Табличный_текст(i, 0, ABC));
                divTable.Children.Add(Дизайн.Табличный_блок(0, i, Дизайн.Фиолетовый_цвет));
                divTable.Children.Add(Дизайн.Табличный_текст(0, i, ABC));
                i++;
            }
            Size = Дизайн.Размер_сторон_блока_таблицы*i + 45;
            divTable.Margin = new Thickness((int)Info.Width+5, 0, 0, 0);
            WidthSize += Size;
        }

        /// <summary>
        /// Заполнения талицы значениями.
        /// </summary>
        /// <param name="Количество_символов_алфавита">Количество символов алфавита.</param>
        /// <param name="Значения_Ngramm">Массив просчитанных n-грамм значении.</param>
        /// <param name="Коэффициент">Коэффициент, для получения удобного диапазона цветов.</param>
        /// <param name="Количество_символов">Количество символов, для высчитывания значения цвета.</param>
        public void Значения_таблицы(int Количество_символов_алфавита, Array Значения_Ngramm, int Коэффициент, double Количество_символов)
        {
            int Значение_цвета;
            for (int i = 0; i < Количество_символов_алфавита; i++)
            {
                for (int j = 0; j < Количество_символов_алфавита; j++)
                {

                    if ((int)Значения_Ngramm.GetValue(i, j) != 0)
                    {
                        Значение_цвета = Коэффициент * Convert.ToInt32((Convert.ToDouble(Значения_Ngramm.GetValue(i, j)) / Количество_символов) * Дизайн.Количество_цветов);

                        Rectangle RC = new Rectangle();
                        RC = Дизайн.Табличный_блок(i, j, Значение_цвета);
                        RC.MouseMove += new MouseEventHandler(tb_MouseMove);
                        divContentTable.Children.Add(RC);
                    }
                }
            }
            divContentTable.Margin = new Thickness(Дизайн.Размер_сторон_блока_таблицы, Дизайн.Размер_сторон_блока_таблицы, 0, 0);
        }

        public int[,] ПолучитьТаблицуДляСравнения(int Количество_символов_алфавита, Array Значения_Ngramm, int Коэффициент, double Количество_символов)
        {
            ТаблицаДляСравнения=new int[Количество_символов_алфавита, Количество_символов_алфавита];
            int Значение_цвета;
            for (int i = 0; i < Количество_символов_алфавита; i++)
            {
                for (int j = 0; j < Количество_символов_алфавита; j++)
                {

                    if ((int)Значения_Ngramm.GetValue(i, j) != 0)
                    {
                        Значение_цвета = Коэффициент * Convert.ToInt32((Convert.ToDouble(Значения_Ngramm.GetValue(i, j)) / Количество_символов) * 1536);
                        ТаблицаДляСравнения[i, j] = Значение_цвета;
                    }
                }
            }

            return ТаблицаДляСравнения;
        }

        private void Вывод_Info()
        {
            Info.HorizontalAlignment = HorizontalAlignment.Left;
            Info.VerticalAlignment = VerticalAlignment.Top;
            Info.Margin = new Thickness(5, 5, 0, 0);
            Info.Width = 80;
            Info.Height = 100;
            WidthSize += (int)(Info.Width+Info.Margin.Left);
        }

        /// <summary>
        /// Событие, при наведение мыши на блоки таблицы. Блоки содержащие данные.
        /// </summary>
        void tb_MouseMove(object sender, MouseEventArgs e)
        {

            string pos = Convert.ToString(Mouse.GetPosition(divContentTable));
            //Info.Text = "" + pos;
            Info.Text = "";
            Мышь.Расщепление_строковых_с_разделителем_точка_запятая(pos);
            bool первая_цифра = true;
            int позицияАБВгориз = 0;
            int позицияАБВверт = 0;
            foreach (string число in Мышь.Массив)
            {
                if (первая_цифра) { позицияАБВгориз = Convert.ToInt32(число) / 15; первая_цифра = false; } else { позицияАБВверт = Convert.ToInt32(число) / 15; }
            }
            if (позицияАБВверт > 32) { позицияАБВверт--; } else { if (позицияАБВгориз > 32) { позицияАБВгориз--; } }
            Info.Text += "" + ABCD[позицияАБВгориз] + ABCD[позицияАБВверт] + "=" + tabl[позицияАБВгориз, позицияАБВверт] + "\np(" + ABCD[позицияАБВгориз] + ABCD[позицияАБВверт] + ")=" + Convert.ToString(Convert.ToDouble(tabl[позицияАБВгориз, позицияАБВверт]) / m);
            
        }
        
    }
}
