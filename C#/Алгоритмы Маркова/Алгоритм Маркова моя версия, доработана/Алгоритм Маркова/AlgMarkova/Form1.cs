using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace AlgMarkova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //richTextBox1.Text +=
        }

        // Добавлением импликации
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += ("\u001A");
        }

        public void doWork(bool byStep = false)
        {
            try
            {
                string base_string = textBox1.Text.ToString();
                string[] str;
                str = richTextBox1.Text.ToString().Split('\n');//<string>();

                int workingString = 0;// Рабочая строка
                // Проводим замены обычных подстановок
                // Код не оптимизирован - т.к. масс ив прогоняется всё время с начала до конца со всеми комментариями и тп
                for (int i = workingString; i < str.Length;)
                {
                    // Идем по блокам актуальных команд - разделенных пробелом
                    {
                        //if (str[i].CompareTo("") == 0) workingString = i;
                    }
                    //Игнорируем пустые строки и строки без ->, комментарии с #
                    if (str[i].CompareTo("") != 0 && str[i].IndexOf("->") >= 0 && str[i].IndexOf("#") != 0)
                    {
                        string[] mass = new string[2];// str[i].Split('\u001A');
                        int j = str[i].IndexOf("->");
                        mass[0] = str[i].Substring(0, j);
                        mass[1] = str[i].Substring(j + 2, str[i].Length - (j + 2));

                        //Проводим замену
                        //Игнорируем не входящие в базовое слово элементы и терминальные подстановки(которые начинаются с точки)
                        if (mass[1].IndexOf(".") != 0 && base_string.IndexOf(mass[0]) >= 0)
                        {
                            Regex rgx = new Regex(mass[0]);
                            base_string = rgx.Replace(base_string, mass[1], 1).ToString();
                            //Уведомляем - какая замена произведена:
                            ChangeRichTextBox( "\a Замена слова \"" + mass[0] + "\" на слово \"" + mass[1] + "\":\n");
                            ChangeRichTextBox( base_string + '\n');
                            i = workingString;

                            // Пауза!
                            if (byStep)
                            {
                                System.Threading.Thread.Sleep(1000);
                            }

                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }

                // Проводим замены из конечных подстановок
                for (int i = 0; i < str.Length;)
                {
                    //Игнорируем пустые строки и строки без ->, комментарии с #
                    if (str[i].CompareTo("") != 0 && str[i].IndexOf("->") >= 0 && str[i].IndexOf("#") != 0)
                    {
                        string[] mass = new string[2];// str[i].Split('\u001A');
                        int j = str[i].IndexOf("->");
                        mass[0] = str[i].Substring(0, j);
                        mass[1] = str[i].Substring(j + 2, str[i].Length - (j + 2));

                        //Проводим замену
                        //Игнорируем не входящие в базовое слово элементы и терминальные подстановки(которые начинаются с точки)
                        if (mass[1].IndexOf(".") == 0 && base_string.IndexOf(mass[0]) >= 0)
                        {
                            // Обрежем точку - чтобы она не отображалась
                            mass[1] = mass[1].Substring(1, mass[1].Length - 1);
                            Regex rgx = new Regex(mass[0]);
                            base_string = rgx.Replace(base_string, mass[1], 1).ToString();
                            //Уведомляем - какая замена произведена:
                            ChangeRichTextBox( "\n!\a! Конечная замена слова \"" + mass[0] + "\" на слово \"" + mass[1] + "\":\n");
                            ChangeRichTextBox( base_string + '\n');
                            //Замена прошла - выходим из цикла
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }

                // richTextBox3.BackColor = Color.GreenYellow;
                // Конечный результат
                textBox2.Text = base_string;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка ввода правил-подстановок");
                //richTextBox3.BackColor = Color.Red;
            }
        }
        // Запуск алгоритма Маркова
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            richTextBox3.BackColor = Color.White;
            doWork();
        }
        // Очистить окно ввода формул
        private void button1_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        // Очистить окно вывода результата
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            richTextBox3.BackColor = Color.White;
        }

        // Сохранение в файл
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //TODO запись в файл
                string filename = saveFileDialog1.FileName;
                StreamWriter sr = new StreamWriter(filename);
                
                string[] str = richTextBox1.Text.ToString().Split('\n');//<string>();
   
                for (int i = 0; i < str.Length; i++)
                    sr.WriteLine(str[i]);
                sr.WriteLine();
                
                sr.Close();
            }

        }

        // Вывод из файла
        private void button5_Click(object sender, EventArgs e)
        {
            // Сначала очищаем всё на наше поле
            richTextBox1.Clear();
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Rules"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                // TODO чтение из файла
                string filename = dlg.FileName;
                StreamReader sr = new StreamReader(filename);

                string[] readingString = sr.ReadToEnd().Split('\n');

                // Заполняем
                for(int i=0; i< readingString.Length; i++)
                    richTextBox1.Text += readingString[i];

                sr.Close();
            }
        }

        // Сохранение строки
        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //TODO запись в файл
                string filename = saveFileDialog1.FileName;
                StreamWriter sr = new StreamWriter(filename);

                string str = textBox1.Text.ToString();//<string>();
                
                sr.WriteLine(str);
                sr.WriteLine();

                sr.Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "BaseString"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                // TODO чтение из файла
                string filename = dlg.FileName;
                StreamReader sr = new StreamReader(filename);

                string readingString = sr.ReadToEnd();

                // Заполняем
                textBox1.Text = readingString;

                sr.Close();
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void инструкцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("# Введите данные для алгоритма по следующим правилам:\n"+"# Слово->Подстановка\n"+"# Cлово->.КонечнаяПодстановка\n"+"# Группы комманд разделяются пустой строкой\n" + "# v - знак пустого символа\n # число начинается как а123;","Краткая инструкция");
        }

        // Запуск пошагового исполнения
        private void шаг31ЗапускПошаговогоИсполненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            richTextBox3.BackColor = Color.White;
            doWork(true);
        }


        delegate void text(string text, bool? enabled = true);

        public void ChangeRichTextBox(string text, bool? enabled = null)
        {
            if (richTextBox3.InvokeRequired)
            {
                richTextBox3.Invoke(new text(ChangeRichTextBox), text, enabled);
            }
            else
            {
                richTextBox3.Text += (text);
            }
            richTextBox3.Refresh();
        }
    }
}
