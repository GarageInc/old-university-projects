using System;
using System.Windows.Forms;
using System.IO;

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
            // Сначала очистим элементы, в которые запишем результаты
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();

            // Создадим строки, в которые будем записывать результат
            var baseText = richTextBox1.Text;
            var codeText = "";

            var lzw = new LZW();
            // Сжимаем введенное сообщение
            lzw.Code(baseText, ref codeText);

            // Указываем полученно-измененные данные
            richTextBox2.Text = codeText;

            // Выведем словарь
            string result = lzw.GetString();
            richTextBox4.Text = result;
            
            // Декодируем
            var deCodeText = "";
            lzw.Decode(baseText,ref deCodeText);

            // Указываем полученное-измененные данные
            richTextBox3.Text = deCodeText;

            var finishDate = new DateTime();

            // Выведем результаты
            richTextBox5.Text = "Время работы = " + (finishDate - startDate).TotalMilliseconds + " миллисекунд";
            richTextBox5.Text += "\nИсходный размер: " + richTextBox1.Text.Length + " символов";
            richTextBox5.Text += "\nСжатый размер: " + richTextBox2.Text.Length + " символов";
            richTextBox5.Text += "\nКПД: " + 100 * richTextBox2.Text.Length / richTextBox1.Text.Length + "%";
        }

        // Сжатие
        private void LZWButton(object sender, EventArgs e)
        {
            // Запустим работу
            DoWork();
        }

        // Загрузить текст из файла
        private void DownloadFileButton(object sender, EventArgs e)
        {
            var saveFileDialog1 = new OpenFileDialog();
            saveFileDialog1.Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                var sr = new StreamReader(filename);
                richTextBox1.Clear();
                richTextBox1.Text=sr.ReadToEnd();

                // Запустим работу
                DoWork();

                var sw = new StreamWriter("result.txt");
                sw.Write(richTextBox2.Text);
                
                sr.Close();
                sw.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
