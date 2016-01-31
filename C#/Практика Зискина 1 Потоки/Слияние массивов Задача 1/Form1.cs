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
        }

        int[] A;
        int[] B;
        int[] C;
        int[] was;
        // Кнопка сортирования
        private void button1_Click(object sender, EventArgs e)
        {
            var a = textBox1.Text.Split(',');
            var b = textBox2.Text.Split(',');

            A = new int[a.Length];
            B = new int[b.Length];

            // Перевод в массивы
            int i = 0;
            foreach(var t in a)
            {
                A[i] = int.Parse(t);
                i++;
            }
            i = 0;
            foreach (var t in b)
            {
                B[i] = int.Parse(t);
                i++;
            }

            C = new int[A.Length + B.Length];
            //flag = C.Length;
            was = new int[A.Length + B.Length];

            for (int j = 0; j < C.Length; j++)
                C[j] = 999999;

            for (int j = 0; j < was.Length; j++)
                was[j] = 0;


            // Cортировка в потоке
            Merger(A, B, ref C);

            
            // Вывод
            textBox3.Text = string.Join(",",C);
        }

        public void Merger(int[] A, int[] B, ref int[] C)
        {
            for (int i = 0; i < A.Length; i++)
            {
                Thread t = new Thread(this.GetPosition);
                t.IsBackground = true;
                t.Start((object)new MyClass(A, i, B));
                try
                {
                    t.Join();
                    //while (t.IsAlive)
                    //    ; 
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
                }
            }
            for (int i = 0; i < B.Length; i++)
            {
                Thread t = new Thread(this.GetPosition);
                t.IsBackground = true;
                t.Start((object)new MyClass(B, i, A));
                
                //    t.Join();
                while (t.IsAlive)
                    ; 
            }
        }

        // Функция, подсчитывающая позицию в массиве
        public void GetPosition(object ob)
        {
            MyClass o = (MyClass)ob;

            int[] AA = (int[])o.A;
            int indexindex = (int)o.Index;;

            int[] BB = (int[])o.B;
            int i = 0;
            while (i < BB.Length && (AA[indexindex] > BB[i]))
            {
                i++;
            }

            while(was[i+indexindex]==1)
            {
                i++;
            }

            // Вставляем
            C[i + indexindex] = AA[indexindex];
            was[i + indexindex] = 1;
            // flag--;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = textBox5.Text.Split(',');

            A = new int[a.Length];

            // Перевод в массивы
            int i = 0;
            foreach (var t in a)
            {
                A[i] = int.Parse(t);
                i++;
            }

            // Cортировка в потоке
            BubleSort();

            // Вывод
            textBox4.Text = string.Join(",", A);
        }

        int yes = 1;
        public void BubleSort()
        {
            // Итерации - пока нет изменений
            int j=0;
            int n = A.Length;
            Thread[] потоки = new Thread[n / 2];
            while(yes>0)
            {
                yes = 0;
                for(int i=j, k=0; i < n-1; i=i+2,k++)
                {
                        потоки[k] = new Thread(Swap);
                        MyClass2 m = new MyClass2(i, i + 1);
                        потоки[k].Start(m);
                }

                // Ждём, когда закончатся потоки
                foreach(var п in потоки)
                {
                    п.Join();
                }
                
                j = (j+1) % 2;
            }
        }

        public void Swap(object o)
        {
            int i = ((MyClass2)o).I;
            int j = ((MyClass2)o).J;

            if(A[i]>A[j])
            {
                int a = A[i];
                A[i] = A[j];
                A[j] = a;

                yes++;
            }
        }

        // 3
        int flag = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            var a = textBox7.Text.Split(',');
            var b = textBox8.Text.Split(',');

            A = new int[a.Length];
            B = new int[b.Length];

            // Перевод в массивы
            int i = 0;
            foreach (var t in a)
            {
                A[i] = int.Parse(t);
                i++;
            }
            i = 0;
            foreach (var t in b)
            {
                B[i] = int.Parse(t);
                i++;
            }

            int n, m;
            n = A.Length;
            m = b.Length;
            
            Thread[] потоки = new Thread[n + m - 1];

            C = new int[n + m - 1];
            flag = n + m - 1;
            
            for (i = 0; i < n + m - 1; i++)
            {
                потоки[i] = new Thread(new ParameterizedThreadStart(count));
                потоки[i].Start(i);
                //потоки[i].Join();
                while (потоки[i].IsAlive)
                    ;
            }

            // Вывод результатов
            textBox6.Text = string.Join(",", C);
        
        }


        void count(object param)
        {
            int data = Convert.ToInt32(param);
            for (int i = Math.Max(0, data - B.Length + 1); i < Math.Min(A.Length, data + 1); i++)
                C[data] += A[i] * B[data - i];
            //flag--;
        }

    }

 

    class MyClass
    {
        int[] a;
        int[] b;
        int index;
        public MyClass (int[] a1, int i1, int[] b1)
        {
            this.a = a1;
            this.b = b1;
            this.index = i1;
        }

        public int[] A    // the Name property
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }
        public int[] B    // the Name property
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }
        public int Index    // the Name property
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
    }

    class MyClass2
    {
        int i, j;
        bool swap;

        public MyClass2(int i1, int i2)
        {
            this.i = i1;
            this.j = i2;
            this.swap = false;
        }

        public bool SWAP    // the Name property
        {
            get
            {
                return swap;
            }
            set
            {
                swap = value;
            }
        }
        
        public int J    // the Name property
        {
            get
            {
                return j;
            }
            set
            {
                j = value;
            }
        }
        public int I    // the Name property
        {
            get
            {
                return i;
            }
            set
            {
                i = value;
            }
        }

        

    }

}
