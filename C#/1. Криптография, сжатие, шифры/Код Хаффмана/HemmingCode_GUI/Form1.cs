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
        }
        // Проверка
        private void CheckButton(object sender, EventArgs e)
        {
            DoWork();
        }

        // Проверка в файле
        private void CheckFileButton(object sender, EventArgs e)
        {
            DoWork();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                StreamReader sr = new StreamReader(filename);
                richTextBox1.Clear();
                richTextBox1.Text=sr.ReadToEnd();
                sr.Close();

                StreamWriter sw = new StreamWriter("result.txt");
                sw.Write(richTextBox2.Text);
                sw.Close();
            }
        }
    }
}
