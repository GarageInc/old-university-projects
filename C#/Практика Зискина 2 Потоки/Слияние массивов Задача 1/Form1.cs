using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Слияние_массивов_Задача_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Инициализируем массив случайных чисел от 1 до 10 000(всего 300 чисел)
            Random r = new Random();
            for (int i = 0; i < 3000; i++)
                mass.Add(r.Next(3000));

            richTextBox1.Text = string.Join(", ", mass.ToArray());

            massArray = mass.ToArray();

            textBox9.Text = massArray[10].ToString();
        }

        List<int> mass = new List<int>();
        int[] massArray;

        static List<int> СтепениА = new List<int>();
        static List<int> СтепениБ = new List<int>();
        static List<int> Факториалы = new List<int>();
        static int n = 2;

        // Степени ('a')
        void A_st(object x)
        {
            СтепениА.Add(1);
            int a = (int)x;
            int a_st = a;
            for (int i = 1; i <= n; i++)
            {
                СтепениА.Add(a_st);
                a_st = a_st * a;
            }
        }
        // Степени ('b')
        void B_st(object x)
        {
            СтепениБ.Add(1);
            int b = (int)x;
            int b_st = b;
            for (int i = 1; i <= n; i++)
            {
                СтепениБ.Add(b_st);
                b_st = b_st * b;
            }
        }
        // Факториалы
        void Fact(object x)
        {
            Факториалы.Add(1);
            var fac = 1;
            int k = (int)x;
            for (int i = 1; i <= k; i++)
            {
                fac = fac * i;
                Факториалы.Add(fac);
            }
        }


        // Кнопка вызова Бинома Ньютона
        private void button1_Click(object sender, EventArgs e)
        {
            СтепениА.Clear();
            СтепениБ.Clear();
            Факториалы.Clear();

            var a = int.Parse(textBox1.Text);
            var b = int.Parse(textBox2.Text);
            n = int.Parse(textBox3.Text);
            int sum=0;

            Thread[] th = new Thread[3];
            // Запускаем потоки для подсчета степеней и факториалов
            th[0]=new Thread(new ParameterizedThreadStart(A_st));
            th[0].Start(a);
            
            th[1]=new Thread(new ParameterizedThreadStart(B_st));
            th[1].Start(b);
            
            th[2]=new Thread(new ParameterizedThreadStart(Fact));
            th[2].Start(n);
            

            // Ждём, когда потоки остановятся
            for (int i = 0; i < 3; i++)
            {
                th[i].Join();
            }

            // Суммируем элементы по формуле
            for (int j = 0; j < n+1; j++)
            {
                sum = sum + СтепениБ[j]*СтепениА[n-j]*(Факториалы[n] /( Факториалы[j] * Факториалы[n-j]));
            }


            // Вывод
            textBox4.Text = sum.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Подсчет синуса
        List<double> факториалы = new List<double>();
        List<double> степениX = new List<double>();
        double x;

        private void button2_Click(object sender, EventArgs e)
        {
            факториалы.Clear();
            степениX.Clear();

            double x = (double.Parse(textBox6.Text)*Math.PI)/180;
            n = int.Parse(textBox5.Text);

            // Запуск потоков
            Thread[] thr = new Thread[2];

            thr[0] = new Thread(ПодсчитатьФакториал);
            thr[0].Start();

            thr[1] = new Thread(new ParameterizedThreadStart(ПодсчитатьСтепени));
            thr[1].Start(x);

            // Дождемся, когда потоки остановятся
            for (int i = 0; i < 2; i++)
                thr[i].Join();

            // Сюда выведем результат - подсчет суммы
            double s = 0;
            for (int j = 0; j < n; j++)
                s += Math.Pow(-1, j) * степениX[j] / факториалы[j];

            // Вывод наш
            textBox7.Text = s.ToString();
            // Вывод подсчета компьютером
            textBox8.Text = Math.Sin(x).ToString();
        }

        // Находится степень числа
        void ПодсчитатьСтепени(object param)
        {
            double x = Convert.ToDouble(param);
            степениX.Add(x);
            // 1,3,5...
            for (int i = 1; i < n; i++)
            {
                степениX.Add(x * x * степениX[i - 1]);
            }
        }

        // Степень
        void ПодсчитатьФакториал()
        {
            факториалы.Add(1);
            int j = 2;
            // 1,1*2*3,1*2*3*4*5
            for (int i = 1; i < n; i++)
            {
                int k = j;
                j++;
                факториалы.Add(k * j * факториалы[i - 1]);
                j++;
            }
        }

        bool найдено = false;
        int число = 0;
        int position = -1;
        int STARTpos, ENDpos;
        string resultText = "";
        // Поиск числа в массиве
        private void button3_Click(object sender, EventArgs e)
        {
            int start = 0;
            int end = massArray.Length-1;

            // Глубина поиска
            n = (int)Math.Log(massArray.Length,2);
            число = int.Parse(textBox9.Text);
            найдено = false;
            richTextBox2.Text = "";
            resultText = "";

            Pair p = new Pair();
            p.start = start;
            p.end = end;

            Thread t = new Thread(new ParameterizedThreadStart(Find));;
            t.Start(p);

            while (!найдено)
            {
            }

            // Вывод
            textBox10.Text = position.ToString();
            richTextBox2.Text = resultText;
        }

        public void Find(object o)
        {
            Pair p=(Pair)o;
            int start1=p.start;
            int end2=p.end;
            int start2 = start1+(end2 - start1) / 2 + 1;
            int end1=start2-1;

            //ChangeShowRichTextBox2(start1 + "-" + end2 + "\n");
            resultText += start1 + "-" + end2 + "\n";
            int j = 0;
            if (!найдено)
            {
                if(end2-start1<=n+1)
                {
                    // Обычный поиск
                    for (j = start1; j <= end2 && !найдено; j++)
                    {
                        if (massArray[j] == число)
                        {
                            resultText += start1 + "-" + end2 + "; НА ПОЗИЦИИ = " + j + "\n";
                            //ChangeShowRichTextBox2(start1 + "-" + end2 + "; НА ПОЗИЦИИ = "+j+"\n");
                            position = j;
                            найдено = true;
                            break;
                        }
                    }
                }
                else
                {
                    Thread t1 = new Thread(new ParameterizedThreadStart(Find));
                    Pair p1=new Pair();
                    p1.start=start1;
                    p1.end=end1;
                    t1.Start(p1);

                    Thread t2 = new Thread(new ParameterizedThreadStart(Find));
                    Pair p2 = new Pair();
                    p2.start = start2;
                    p2.end = end2;
                    t2.Start(p2);
                }

                if(j!=0 && j!=end2+1)
                {
                    resultText += start1 + "-" + end2 + "; ПОТОК ВЫЛЕТЕЛ НА ПОЗИЦИИ = " + j + "\n";
                }
            }
        }

        delegate void text(string text);
        public void ChangeShowRichTextBox2(string text)
        {
            if (richTextBox2.InvokeRequired)
            {
                richTextBox2.Invoke(new text(ChangeShowRichTextBox2), text);
            }
            else
            {
                richTextBox2.Text += (text);
            }
        }

    }

    class Pair
    {
        public int start;
        public int end;
    }
}
