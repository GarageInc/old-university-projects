

using System;

namespace Antivirus
{
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    public class ScanSystem
    {
        public  long numberofFile = 0;
        public  long numberofPEFile = 0;
        public  long numberofNotOpenedFile = 0;
        public  long numberofVirusFile = 0;
        public  List<FileInfo> virusFiles = new List<FileInfo>();
        public  VirusLibrary virusLibrary = new VirusLibrary();
        private  long i = 0;
        public  long procent = 0;

        private ProgressBar progressBar;
        private RichTextBox richTextBoxResult;

        // Конструктор
        public ScanSystem(ref ProgressBar pB,ref RichTextBox rtB)
        {
            numberofFile = 0;
            numberofVirusFile = 0;
            numberofNotOpenedFile = 0;
            numberofPEFile = 0;

            progressBar = pB;
            richTextBoxResult = rtB;

            virusFiles.Clear();
            i = 0;

            // Загружаем библиотеку вирусов
            virusLibrary.LoadLibrary();
        }

        public  void Scanning(string s)
        {
            // Подсчет количества всех файлов
            GetCountOfFiles(s);
            // Собственно, запуск сканирования
            run(s);
        }

        public  void ScanFile(FileInfo f)
        {
            i++;
            if (numberofFile != 0)
            {
                procent = (i * 100) / numberofFile;
            }

            //// Вывод процентов
            progressBar.Value = (int) procent;
            //// Вывод названия сканируемого файла
            richTextBoxResult.Text += '\n'+f.Name;

            try
            {
                // Парсер - получаем сегменты кода проверяемого файла
                Parser p = new Parser(f.FullName);
                if (p.doIt() != 1)
                {
                    if (virusLibrary.check(ref p))
                    {
                        virusFiles.Add(f);
                        numberofVirusFile++;
                    }
                    numberofPEFile++;
                }
            }
            catch (FileNotFoundException exception)
            {
                numberofNotOpenedFile++;
                richTextBoxResult.Text += '\n' + exception.Message+ '\n';
            }
            catch (IOException exception)
            {
                numberofNotOpenedFile++;
                richTextBoxResult.Text += '\n' + exception.Message + '\n';
            }
        }

        // Подсчитывает, сколько всего файлов в каталоге
        private void run(object Dir)
        {
            DirectoryInfo DI = new DirectoryInfo((string)Dir);
            DirectoryInfo[] SubDir = null;
            try
            {
                // Получили все подкаталоги
                SubDir = DI.GetDirectories();
                // Получили все файлы
                FileInfo[] FI = DI.GetFiles();
                foreach (var file in FI)
                {
                    ScanFile(file);
                }
                // Продолжаем обход
                foreach (DirectoryInfo t in SubDir)
                {
                    this.run(t.FullName);
                }
            }
            catch (Exception exception)
            {
                richTextBoxResult.Text += '\n' + exception.Message + '\n';
                return;
            }
        }
        
        // Подсчитывает, сколько всего файлов в каталоге
        private void GetCountOfFiles(object Dir)
        {
            DirectoryInfo DI = new DirectoryInfo((string)Dir);
            DirectoryInfo[] SubDir = null;
            try
            {
                // Получили все подкаталоги
                SubDir = DI.GetDirectories();
                // Получили все файлы
                FileInfo[] FI = DI.GetFiles();

                numberofFile += FI.Count();

                // Продолжаем обход
                for(long i = 0; i < SubDir.Length; ++i)
                {
                    this.GetCountOfFiles(SubDir[i].FullName);
                }
            }
            catch (Exception exception)
            {
                richTextBoxResult.Text += '\n' + exception.Message + '\n';
                return;
            }
        }

        /// <summary>
        /// Напечатаем зараженные
        /// </summary>
        public void ShowResult()
        {
            richTextBoxResult.Text += "\n ЗАРАЖЕНЫ:";
            if (virusFiles.Count > 0)
            {
                foreach (var virusFile in virusFiles)
                {
                    richTextBoxResult.Text += '\n' + virusFile.FullName;
                }
            }
            else
            {
                richTextBoxResult.Text += '\n' + "нет зараженных файлов";
            }
        }
    }
    
}
