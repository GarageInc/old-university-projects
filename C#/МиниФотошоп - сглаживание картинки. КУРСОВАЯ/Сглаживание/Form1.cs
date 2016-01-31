using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Сглаживание
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Для рисования прямоугольника
        Rectangle _rect;//..рамку храним(экранные координаты - для клиентских нужно отдельную переменную)
        bool _drawFrame = false;//рисуем мы ее или нет
        Point p1, p2;
        Panel panel;
        private void Form1_Load(object sender, EventArgs e)
        {
            //для ясности - какие обработчики нужны
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
            pictureBox1.MouseLeave += new EventHandler(pictureBox1_MouseLeave);
            AutoScroll = true;//Полосы прокрутки
        }

        //стираем рамку и снимаем флаг что мы ее рисуем
        void EndPaintFrame()
        {
            if (_drawFrame)
            {
                _drawFrame = false;
                ControlPaint.DrawReversibleFrame(_rect, Color.Black, FrameStyle.Dashed);//стираем последний раз
            }
        }
        
        //Выход за пределы
        void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            EndPaintFrame();
        }

        //В момент нажатия кнопки мыши
        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = ClientToScreen(e.X, e.Y);
            p1 = new Point(e.X, e.Y);
             _rect = new Rectangle(p.X, p.Y, 0, 0);
            _drawFrame = true;
        }

        //В момент отпускания
        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            p2 = new Point(e.X, e.Y);
            EndPaintFrame();
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_drawFrame)
            {
                //затираем старую рамку
                ControlPaint.DrawReversibleFrame(_rect, Color.Black, FrameStyle.Dashed);
                Point p = ClientToScreen(e.X, e.Y);

                _rect = new Rectangle(_rect.X, _rect.Y, p.X - _rect.X, p.Y - _rect.Y);
                //рисуем новую
                ControlPaint.DrawReversibleFrame(_rect, Color.Black, FrameStyle.Dashed);
            }
        }

        // перевод координат Пикчур бокса размещенного на форме в экранные координаты
        Point ClientToScreen(int ClientX, int ClientY)
        {
            return pictureBox1.PointToScreen(new Point(ClientX, ClientY));
        }

        //Cглаживание
        private void button1_Click(object sender, EventArgs e)
        {            
                Graphics g = pictureBox1.CreateGraphics();//Создаем графику - возможность рисования

                int x2 = max (p1.X,p2.X), y2 = max (p1.Y, p2.Y);//Берем точки, ДО КОТОРЫХ рисовать прямоугольник сжатий
                
              
                // Create a Bitmap object from an image file.
                Bitmap myBitmap = new Bitmap(pictureBox1.Image);//Создаем bmp-объект из picturebox
                
            
                
                    for (int x1 = min(p1.X,p2.X); x1 < x2; x1++)
                    {
                        
                        for (int y1 = min(p1.Y,p2.Y); y1 < y2; y1++)
                        {
                            Point p = new Point(x1, y1);
                            
                            Color pixelColor = getDominantColor(myBitmap, p.X, p.Y);

                            // Fill a rectangle with pixelColor.
                            SolidBrush pixelBrush = new SolidBrush(pixelColor);//Создаем кисть, использующую данный цвет
                           
                            Rectangle rec = new Rectangle(p.X, p.Y, 1, 1);//На заданных координатах создаем прямоугольник размером с пиксель
                            
                            g.FillRectangle(pixelBrush, rec);//Закрашиваем это регион созданной кистью. Во время закрашивания - изображение размывается
                           
                        }
                    }
                    
                
        }

        //Получаем среднее значение цвета по окрестностям пикселя
        public static Color getDominantColor(Bitmap bmp,int x, int y)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            //Ставим границы слева и справа
            int leftX=0,rightX=0,deepY=0,hightY=0;
            if (x - 1 <= 0) leftX = x; else leftX = x - 1;
            if (x + 1 >= bmp.Width) rightX = x; else rightX = x + 1;
            if (y - 1 <= 0) hightY = y; else hightY = y - 1;
            if (y + 1 >= bmp.Height) deepY = y; else deepY = y + 1;



            int x1=leftX;
            
                    //Идем по окрестностям
            for(; x1<=rightX; x1++)
                for (int y1= hightY; y1 <= deepY; y1++)
                {
                    Color clr = bmp.GetPixel(x1, y1);
                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }

        public int max(int x, int y)
        {
            if (x > y) return x;
            else return y;
        }

        public int min(int x, int y)
        {
            if (x < y) return x;
            else return y;
        }

        //Сохранение рисунки
        int k = 0;//Переменная для нумерации сохраненных объектов
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("image_"+(k++)+".bmp");
            MessageBox.Show("Рисунок сохранен!");
        }

        //Загрузка картинки по нажатию кнопки
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "bmp files (*.bmp)|*.bmp";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(dlg.FileName);
            }

            dlg.Dispose();
        }


        

    }
}
