using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace КОД_ХЭММИНГ 
{
    public partial class Form1 : Form
    {
        public static Random Rand = new Random();
       // public static int[] code;
       // public static int controlBits;
       // public static int err;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();// очищение

            int length = Convert.ToInt32(textBox1.Text);
            int controlBits = Convert.ToInt32(Math.Ceiling(Math.Log(length, 2))) + 1;// (log2n) + 1
            int totalBits = length + controlBits;

            int[] array = new int[totalBits + 1]; // массив всех бит
            int[] array_Error = new int[totalBits + 1];// массив для генерации ошибки
            textBox2.Text = controlBits.ToString();
            textBox3.Text = totalBits.ToString();
            
                //int k;
                //генерация сообщений
                gen_message(array, totalBits);

                // устанавливаем контрольные биты на нужные позиции
                double l = 0;// степень двойки
                int k = 0;
                // Отсчет от 1 и по всем битам
                for (int j = 1; j < totalBits + 1; j++)
                {
                    k = Convert.ToInt32(Math.Pow(2, l));
                    if (j == k)
                    {
                        l++;
                        continue;
                    }
                    else richTextBox1.Text += array[j];// сдвиг дальше, пока не дойдем до контрольного
                }
                richTextBox1.Text += "\n";

                zeroing(array, totalBits);// обнуляем контрольные биты

                //вычисление контрольных битов
                l = 0;
                for (int j = 1; j < totalBits + 1; j++)
                {
                    k = Convert.ToInt32(Math.Pow(2, l));
                    if (j == k)
                    {
                        array[j] = getControlBits(array, k, totalBits);
                        l++;
                    }
                    array_Error[j] = array[j];
                    richTextBox1.Text += array[j];
                }
                richTextBox1.Text += @"	Нахождение значений контрольных бит
";

                //генерируем ошибку в случайном месте
                int error = 0;

                gen_error(array_Error, totalBits, ref error);

                if (array[error] == 0)
                    array[error] = 1;
                else
                    array[error] = 0;

                for (int j = 1; j < totalBits + 1; j++)
                    richTextBox1.Text += array[j];

                richTextBox1.Text += "\tСгенерирована ошибка в " + error + " бите\n";

                //исправление ошибки
                //заново обнуление контрольных битов
                zeroing(array_Error, totalBits);

                l = 0;
                //вычисление новых бит
                for (int j = 1; j < totalBits + 1; j++)
                {
                    k = Convert.ToInt32(Math.Pow(2, l));
                    if (j == k)
                    {
                        array_Error[j] = getControlBits(array_Error, k, totalBits);
                        l++;
                    }
                    richTextBox1.Text += array_Error[j];
                }
                richTextBox1.Text += "\tCнова находим значения контрольных бит\n";

                int corr = 0;
                for (int j = 1; j < totalBits + 1; j++)
                {
                    if (j == error) // нашли ошибку
                        break;
                    if (array[j] != array_Error[j])
                        corr += j;
                }

                if (array[corr] == 0) // исправляем 
                    array[corr] = 1;
                else
                    array[corr] = 0;

                for (int j = 1; j < totalBits + 1; j++)
                {
                    richTextBox1.Text += array[j];
                }
                richTextBox1.Text += "\tПолучаем исправленное сообщение\n";


                l = 0;
                for (int j = 1; j < totalBits + 1; j++)
                {
                    k = Convert.ToInt32(Math.Pow(2, l));
                    if (j == k)
                    {
                        l++;
                        continue;
                    }
                    else richTextBox1.Text += array[j];
                }

                richTextBox1.Text += "\n\n\n";
            
        }
        // функция генерации сообщения
        public static void gen_message(int[] array, int TotalBits)
        {
            int n;

            for (int i = 1; i < TotalBits + 1; i++)
            {
                n = Rand.Next() % 2;
                array[i] = n;
            }
        }

        // функция обнуления контрольных бит
        public static void zeroing(int[] array, int TotalBits)
        {
            double l = 0;
            int k;
            for (int j = 1; j < TotalBits + 1; j++)
            {
                k = Convert.ToInt32(Math.Pow(2, l));
                if (j == k)
                {
                    array[j] = 0;
                    l++;
                }
            }
        }
        // функция вычисления контрольных бит
        public static int getControlBits(int[] array, int k, int TotalBits)
        {
            int value = 0;// кол-во единиц
            int kol = 0;// контролирует длину каждого блока
            int i = k;// позиция поиска, с какого места считаем
            while (i < TotalBits + 1)// для поиска от позиции и до конца
            {
                while (kol < k && i < TotalBits + 1)//контролирует, чтобы прошел блок от контрольного бита и не вышел за границу массива
                {
                    if (array[i] == 1)// подсчет кол-ва единиц в блоке
                        value++;
                    kol++;
                    i++;
                }
                i = i + k;// перескакивает ненужные биты
                kol = 0;// обнуляется длина блока, снова подсчет единиц в новом блоке
            }
            if (value % 2 == 0)// если четное число единиц
                return 0;
            return 1;
        }
        // функция генерации ошибки
        public static void gen_error(int[] array_error, int TotalBits, ref int error)
        {
            int i = Rand.Next(1, TotalBits + 1);
            while (i == 1 || i == 2 || i == 4 || i == 8 || i == 16 || i == 32 || i == 64 || i == 128)
            {
                i = Rand.Next(1, TotalBits + 1);
            }
            if (array_error[i] == 0)
                array_error[i] = 1;
            else array_error[i] = 0;
            error = i;
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
