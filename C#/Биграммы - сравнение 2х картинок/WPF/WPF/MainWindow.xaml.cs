using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// для изображения .jpg - т.е. для тех, которые 256 rgb
    /// </summary>
    public partial class MainWindow : Window
    {
        int z;
        int i;
        int j;
        double[,] tabl = new double[33, 33];
        double m = 1;
        int Количество_символов;
        private string jpg1_str = "";
        private string jpg2_str = "";

        // Создаем алфавит
        StringEditor Алфавит1 = new StringEditor();

        // Создаем сам метод исследования
        N_gramm Ngramm1 = new N_gramm();
        N_gramm Ngramm2 = new N_gramm();


        public MainWindow()
        {
            InitializeComponent();
            Iz_file.Content = "И\nз\n\nф\nа\nй\nл\nа";
            Iz_file_Copy.Content = "И\nз\n\nф\nа\nй\nл\nа";
        }
        
        /// <summary>
        /// Считывание изображения №1 с внешних текстовых данных в кодировке Unicode.
        /// </summary>
        private string Из_файла_1()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            string filename = ofd.FileName;

            if (filename!="")
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(filename);// Считали изображение
                MemoryStream memoryStream = new MemoryStream();// Поток для считывания
                image.Save(memoryStream, ImageFormat.Jpeg);// Сохраняем картинку из файла в поток

                // Переводим в байт код первое изображение
                byte[]  jpg1 = memoryStream.ToArray();
                jpg1_str = Encoding.Default.GetString(jpg1);

                // Создадим алфавит
                byte[] словарь = jpg1.Distinct().ToArray();

                // Теперь создаем алфавит
                string алфавит = Encoding.Default.GetString(словарь);
                Алфавит1.Расщепление_строковых_без_разделителей(алфавит);

                // Вернём строку - как мы считали изображение
                return Encoding.Default.GetString(jpg1);
            }

            return "";
        }

        /// <summary>
        /// Считывание изображения №2 с внешних текстовых данных в кодировке Unicode.
        /// </summary>
        private string Из_файла_2()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            string filename = ofd.FileName;

            if (filename != "")
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(filename);// Считали изображение
                MemoryStream memoryStream = new MemoryStream();// Поток для считывания
                image.Save(memoryStream, ImageFormat.Jpeg);// Сохраняем картинку из файла в поток

                // Переводим в байт код первое изображение
                byte[] jpg2 = memoryStream.ToArray();
                jpg2_str = Encoding.Default.GetString(jpg2);

                // Создадим алфавит
                byte[] словарь = jpg2.Distinct().ToArray();
                
                // Теперь создаем алфавит
                string алфавит = Encoding.Default.GetString(словарь);
                Алфавит1.Расщепление_строковых_без_разделителей(алфавит);

                // Вернём строку - как мы считали изображение
                return Encoding.Default.GetString(jpg2);
            }

            return "";
        }

        /// <summary>
        /// Кнопка анализа первого изображения.
        /// </summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (jpg1_str.Length < Convert.ToInt32(textBox_Количество_символов.Text))
            {
                Текст_Ошибка.Content = "Исследуемое количество символов\nпревышает количество символов в тексте.\nВ тексте " + Convert.ToInt32(textBox1.Text.Length) + " символов.";
            }
            else
            {
                Текст_Ошибка.Content = "В изображении " + Convert.ToInt32(jpg1_str.Length) + " символов.";
                
                Ngramm1.Биграмм(jpg1_str, Convert.ToInt32(textBox_Количество_символов.Text), Алфавит1.Массив);

                Window_N_gram PrintNgramm = new Window_N_gram(Алфавит1.Массив, Ngramm1.Значения_биграм, Convert.ToInt32(textBox_Коэффициент.Text), Convert.ToDouble(textBox_Количество_символов.Text));
            }
        }
        /// <summary>
        /// Кнопка анализа второго изображения.
        /// </summary>
        private void button1_Click_Copy(object sender, RoutedEventArgs e)
        {
            if (jpg2_str.Length < Convert.ToInt32(textBox_Количество_символов_Copy.Text))
            {
                Текст_Ошибка_Copy.Content = "Исследуемое количество символов\nпревышает количества символов в тексте.\nВ тексте " + Convert.ToInt32(textBox1.Text.Length) + " символов.";
            }
            else
            {
                Текст_Ошибка_Copy.Content = "В изображении " + Convert.ToInt32(jpg2_str.Length) + " символов.";
                
                Ngramm2.Биграмм(jpg2_str, Convert.ToInt32(textBox_Количество_символов_Copy.Text), Алфавит1.Массив);
                
                Window_N_gram PrintNgramm = new Window_N_gram(Алфавит1.Массив, Ngramm2.Значения_биграм, Convert.ToInt32(textBox_Коэффициент_Copy.Text), Convert.ToDouble(textBox_Количество_символов_Copy.Text));
                
            }
        }
        
        /// <summary>
        /// Кнопка чтения изображения 1 из файла.
        /// </summary>
        private void Iz_file_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = Из_файла_1();
        }

        /// <summary>
        /// Кнопка чтения изображения 2 из файла.
        /// </summary>
        private void Iz_file_Copy_Click(object sender, RoutedEventArgs e)
        {
            textBox1_Copy.Text = Из_файла_2();
        }
        
        /// <summary>
        /// Кнопка сравнения двух биграмм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ngramm1.Биграмм(jpg1_str, Convert.ToInt32(textBox_Коэффициент_Copy1.Text), Алфавит1.Массив);
                Ngramm2.Биграмм(jpg2_str, Convert.ToInt32(textBox_Коэффициент_Copy1.Text), Алфавит1.Массив);

                // Получим таблицы для сравнения
                Window_N_gram w1 = new Window_N_gram();
                int[,] mass1 = w1.ПолучитьТаблицуДляСравнения(Алфавит1.Массив.Count, Ngramm1.Значения_биграм, Convert.ToInt32(textBox_Коэффициент_Copy1.Text), Convert.ToDouble(textBox_Количество_символов_Copy1.Text));

                Window_N_gram w2 = new Window_N_gram();
                int[,] mass2 = w2.ПолучитьТаблицуДляСравнения(Алфавит1.Массив.Count, Ngramm2.Значения_биграм, Convert.ToInt32(textBox_Коэффициент_Copy1.Text), Convert.ToDouble(textBox_Количество_символов_Copy1.Text));


                // Ищем несовпадения - минимальной высоте/ширине
                int minWidth = Math.Min(mass1.GetLength(0), mass2.GetLength(0));// Число строк
                int minHight = Math.Min(mass1.GetLength(1), mass2.GetLength(1));// Число столбцов

                double несовпадения = 0;
                double совпадения = 0;
                for (int i = 0; i < minHight; i++)
                    for (int j = 0; j < minWidth; j++)
                    {
                        if (mass1[i, j] != 0 && 0 != mass2[i, j])
                        {
                            if (mass1[i, j] == mass2[i, j])
                                совпадения++;
                            else
                                несовпадения++;
                        }
                    }

                // Считаем процент:
                if (совпадения + несовпадения == 0)
                {
                    label.Content = "Изменить интервалы, совпадений/несовпадений нет";
                    return;
                }

                double процент = 100 * совпадения / (совпадения + несовпадения);
                label.Content = процент.ToString()+"%";
            }
            catch(Exception er)
            { }
        }
    }
}
