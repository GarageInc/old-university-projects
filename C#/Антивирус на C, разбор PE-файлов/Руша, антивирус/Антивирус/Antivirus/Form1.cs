using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace Antivirus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Visible = true;
            db = new Model2();
            //db.Signatures.Load();

            label.Text = "Количество сигнатур:" + db.Signatures.Count();
        }

        private Model2 db ;
        
        // Открытие файла
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";//выбор элементов любых типов
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            // Если выбран файл
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {// Пишем имя файла в первое окошко
                            textBox1.Text = openFileDialog1.FileName;
                            textBox2.Text = String.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: прочитать файл не получилось: " + ex.Message);
                }
            }
        }

        // Открытие папки
        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult dialogresult = folderBrowserDialog1.ShowDialog();

            //Надпись выше окна контрола
            folderBrowserDialog1.Description = "Поиск папки";
            //string folderName=null;
            //string filename=null;
            if (dialogresult == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
                textBox1.Text = String.Empty;
            }
        }

        // Кнопка сканировать
        private void ScanButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.Enabled = false;
            progressBar1.Value = 0;
            
            // Собственно, запускаем процесс сканирования
            if (textBox1.Text != "")
            {
                ScanSystem s = new ScanSystem(ref progressBar1,ref richTextBox1);
                s.ScanFile(new FileInfo(textBox1.Text));
                richTextBox1.Text += (("\n\nВсего: " + s.numberofFile+"\n"
                    + " - Исполняемых: " + s.numberofPEFile + "\n"
                    + " - Неоткрывшихся: " + s.numberofNotOpenedFile + "\n"
                    + " - Зараженных: " + s.numberofVirusFile) + "\n");

                s.ShowResult();
                // Оживляется кнопка
                btn.Enabled = true;
            }


            if (textBox2.Text!="")
            {
                ScanSystem s = new ScanSystem(ref progressBar1, ref richTextBox1);
                s.Scanning(textBox2.Text);

                richTextBox1.Text += (("\n\nВсего: " + s.numberofFile + "\n"
                    + " - Исполняемых: " + s.numberofPEFile + "\n"
                    + " - Неоткрывшихся: " + s.numberofNotOpenedFile + "\n"
                    + " - Зараженных: " + s.numberofVirusFile) + "\n");

                s.ShowResult();
                // Оживляется кнопка
                btn.Enabled = true;
            }
        }

        // Генерируем простую сигнуру типа
        // Console.WriteLine("Virus!");
        // Thread.Sleep(1000);
        private void GeneratSignatureButton_Click(object sender, EventArgs e)
        {
            // Прочитаем "зараженные" файлы(сегменты кода) и найдем их общую часть
            Parser p1 = new Parser("Virus1.exe");
            p1.doIt();
            Parser p2 = new Parser("Virus2.exe");
            p2.doIt();
            Parser p3 = new Parser("MiniVirus.exe");
            p3.doIt();
            Parser p4 = new Parser("NotVirus.exe");
            p4.doIt();

            var segm1 = p1.AllSegments.First().CodeBytes;
            var segm2 = p2.AllSegments.First().CodeBytes;
            var segm3 = p3.AllSegments.First().CodeBytes;

            // Найдем пересечение общих участков кода - это и будет сигнатура! ищем данный участок: он отсутствует у p4
            // Console.Writeline("VIRUS!";
            //  Thread.Sleep(1000);
            var a7 = LCS(Encoding.Default.GetString(segm1), Encoding.Default.GetString(segm2));
            var a8 = LCS(Encoding.Default.GetString(segm3), Encoding.Default.GetString(segm2));
            var a9 = LCS(a7, a8);

            // Методом тыка пришёл к получению данной сигнатуры:
            //Добавляем сигнатуру в базу данных
            Addsignature(Encoding.Default.GetBytes(a9));

            this.progressBar1.Value = 0;
        }


        // Добавление в Signatures
        private void Addsignature(byte[] Mass)
        {
            try
            {
                var count = db.Signatures.Count();
                db.Signatures.Add(new Signatures
                {
                    Id = count + 1,
                    Length = Mass.Length,
                    Mass = Mass
                });

                db.SaveChanges();

                label.Text = "Количество сигнатур: "+ db.Signatures.Count();
            }
            catch (Exception ex)
            {
                // если мы ввели неверные данные или неправильно вставили - получаем сообщение с текстом ошибки
                MessageBox.Show(ex.Message);
            }
        }


        // Нахождение наибольшей общей подстроки в двух строках
        public string LCS(string s1, string s2)
        {
            var a = new int[s1.Length + 1, s2.Length + 1];
            int u = 0, v = 0;

            for (var i = 0; i < s1.Length; i++)
                for (var j = 0; j < s2.Length; j++)
                    if (s1[i] == s2[j])
                    {
                        a[i + 1, j + 1] = a[i, j] + 1;
                        if (a[i + 1, j + 1] > a[u, v])
                        {
                            u = i + 1;
                            v = j + 1;
                        }
                    }

            return s1.Substring(u - a[u, v], a[u, v]);
        }
    }
}