using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace HaffmaneCode_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();           
        }

        public void DoWork()
        {
            var startDate = new DateTime();

            string input = richTextBox1.Text;

            if (input.CompareTo("") == 0)
            {
                MessageBox.Show("Вы не ввели текст!");
                return;
            }
            HuffmanTree huffmanTree = new HuffmanTree();

            // Строим дерево Хаффмана по весам слов
            huffmanTree.Build(input);

            // Кодируем
            BitArray encoded = (BitArray)huffmanTree.Encode(input);
            richTextBox2.Clear();
            // Выводим результат кодирования            
            foreach (bool bit in encoded)
            {
                //richTextBox2.Text += ((bit ? 1 : 0) + "");
            }
            richTextBox2.Text = huffmanTree.str;
            // Декодируем
            string decoded = huffmanTree.Decode(encoded);
            richTextBox3.Clear();
            richTextBox3.Text = decoded;

            
            var finishDate = new DateTime();

            // Выведем результаты
            richTextBox4.Text = "Время работы = " + (finishDate - startDate).TotalMilliseconds + " миллисекунд";
            richTextBox4.Text += "\nИсходный размер: " + richTextBox1.Text.Length + " символов";
            richTextBox4.Text += "\nСжатый размер: " + richTextBox2.Text.Length + " символов";
            richTextBox4.Text += "\nКПД: " + 100 * richTextBox2.Text.Length / richTextBox1.Text.Length + "%";
        }

        // Проверка
        private void CheckButton(object sender, EventArgs e)
        {
            DoWork();
        }

        // Проверка в файле
        private void CheckFileButton(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;
            fileDialog1.DefaultExt = ".txt";

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = fileDialog1.FileName;
                StreamReader sr = new StreamReader(filename);
                richTextBox1.Clear();
                richTextBox1.Text=sr.ReadToEnd();
                sr.Close();

                DoWork();

                StreamWriter sw = new StreamWriter("result.txt");
                sw.Write(richTextBox2.Text);
                sw.Close();
            }
        }
    }
}
