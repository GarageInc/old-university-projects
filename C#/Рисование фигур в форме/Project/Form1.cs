using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Рисуем-обрабатываем
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Добавляем фигуры
            List<Figure> figures = new List<Figure>();

            Point point = new Point(100, 50);
            Segment segment = new Segment(new Point(30, 100), new Point(100, 100));
            Circle circle = new Circle(new Point(60, 60), 40);
            Rectangle rectangle = new Rectangle(new Point(120, 120), new Point(150, 300));
            Triangle triangle = new Triangle(new Point(10, 10), new Point(200, 200), new Point(350, 10));

            figures.Add(point);
            figures.Add(segment);
            figures.Add(circle);
            figures.Add(rectangle);
            figures.Add(triangle);

            //Печатаем их данные в текстовом окне
            listBox1.Items.Clear();//Очищаем текстовое окно
            //Выводим информацию о созданных фигурах
            foreach (Figure f in figures)
                listBox1.Items.Add(f.ToString());

            //РИСУЕМ ФИГУРЫ
            Pen myPen;
            //Линия
            myPen = new Pen(System.Drawing.Color.Red);//Кисть-ручка, которой рисуем
            e.Graphics.DrawLine(myPen, new PointF(0, 0), new PointF(point.X, point.Y));
            //Сегмент
            myPen = new Pen(System.Drawing.Color.Chocolate);//Кисть-ручка, которой рисуем
            e.Graphics.DrawLine(myPen, new PointF(segment.p1.X,segment.p1.Y),new PointF(segment.p2.X,segment.p2.Y));
            //треугольник
            myPen = new Pen(System.Drawing.Color.DarkKhaki);//Кисть-ручка, которой рисуем
            e.Graphics.DrawPolygon(myPen, new PointF[3]{new PointF(triangle.p1.X, triangle.p1.Y),new PointF(triangle.p2.X, triangle.p2.Y),new PointF(triangle.p3.X, triangle.p3.Y),});
            //Прямоугольник
            myPen = new Pen(System.Drawing.Color.DodgerBlue);//Кисть-ручка, которой рисуем
            e.Graphics.DrawRectangle(myPen, new System.Drawing.Rectangle(rectangle.p1.X,rectangle.p1.Y, rectangle.p2.X-rectangle.p1.X,rectangle.p2.Y-rectangle.p1.Y));
            //Круг
            myPen = new Pen(System.Drawing.Color.DarkRed);//Кисть-ручка, которой рисуем
            e.Graphics.DrawEllipse(myPen, new System.Drawing.Rectangle(circle.p.X-circle.radius, circle.p.Y-circle.radius, circle.radius*2, circle.radius*2));
        }

        

    }


    class Figure
    {
        //Наследуются:
        public virtual void Length() { }//Длина
    }

    //Наследники для точки и т.д.
    class Figure0D:Figure
    {
    }
    //Класс линии
    class Point : Figure0D
    {
        public int X, Y;//Координаты точки

        public Point(int x = 0, int y = 0)//Конструктор задающий координаты
        {
            this.X = x;
            this.Y = y;
        }

        //Для определения длины
        public override void Length()
        {
            this.ToString();
        }

        public override string ToString()
        {
            string s = "Длина прямой = " + (Math.Pow((Math.Pow(X, 2) + Math.Pow(Y, 2)), 0.5));
            return s;
        }
    }

    //Класс куска фигуры - линии
    class Segment : Figure
    {
        //Две точки с координатами
        public Point p1, p2;
        public Segment(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        //Длина этого сегмента
        public override void Length()
        {
            this.ToString();
        }


        public override string ToString()
        {
            string s = "Длина сегмента = " + (Math.Pow((Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)), 0.5));
            return s;
        }
    }

    //Класс для двумерных фигур
    class Figure2D : Figure
    {

        public virtual void Square(){}//Площадь
    }

    //Класс для круга
    class Circle : Figure2D
    {
        //Координаты центра
        public Point p;
        //Радиус
        public int radius;
        public Circle(Point p, int radius = 0)//Конструктор
        {
            this.p = p;
            this.radius = radius;
        }

        //Площадь этого круга
        public override void Square()
        {
            this.ToString();
        }

        public override string ToString()
        {
            string s = "Площадь круга = " + (Math.PI * Math.Pow(radius, 2));
            return s;
        }
    }

    //Класс прямоугольника
    class Rectangle : Figure2D
    {
        //Начальные координаты и конечные
        public Point p1, p2;
        //Конструктор
        public Rectangle(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        //Площадь этого прямоугольника
        public override void Square()
        {
            this.ToString();
        }

        public override string ToString()
        {
            string s = "Площадь прямоугольника = " + (Math.Abs(p1.X - p2.X) * Math.Abs(p1.Y - p2.Y));
            return s;
        }
    }

    //Класс треугольника
    class Triangle : Figure2D
    {
        //Координаты вершин
        public Point p1, p2, p3;
        //Конструктор
        public Triangle(Point p1, Point p2, Point p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
        //Площадь этого треугольника
        public override void Square()
        {
            this.ToString();
        }

        public override string ToString()
        {
            //Площадь - как модуль половинного произведения двух векторов
            string s = "Площадь треугольника = " + Math.Abs( 0.5*( (p1.X-p3.X)*(p2.Y-p3.Y)-(p2.X-p3.X)*(p1.Y-p3.Y)) );
            return s;
        }
    }
}
