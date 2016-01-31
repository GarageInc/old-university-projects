using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nokolaeva_Masha_2
{
    class Program
    {

        public static void ReplaceCol(int j, int x, string name)
        {
            //Поток для считывания с двоичного файла
            BinaryReader inBin = new BinaryReader(File.Open(name, FileMode.Open));
            //Сначала выводим высоту и ширину
            int h, w;
            h = inBin.ReadInt32();
            w = inBin.ReadInt32();

            Console.WriteLine("СТРОК: "+h);
            Console.WriteLine("СТОЛБЦОВ: "+w);

            //Затем - считываем из файла данные в массив
            double[,] mass = new double[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int jj = 0; jj < w; jj++)
                {
                    mass[i,jj]=inBin.ReadDouble();
                }
            }

            //Заменяем столбец на число
            for (int i = 0; i < h; i++)
            {
                mass[i,j ] = x;
            }

            //Печатаем на экран результат
            Console.WriteLine("RESULT:");
            for (int i = 0; i < h; i++)
            {
                for (int jj = 0; jj < w; jj++)
                {
                    Console.Write(mass[i, jj]+" ");
                }
                Console.WriteLine();//Переход на новую строку
            }

            //Теперь - все обратно записываем в этот же файл, закрыв поток для чтения
            inBin.Close();

            //Поток для записи
            BinaryWriter outBin = new BinaryWriter(File.Open(name, FileMode.Create));//Create - значит, что файл будет перезаписан
            //Записываем в файл данные
            //Сначала вводим высоту и ширину
            outBin.Write(h );//печатаем и переходим на новую строку
            outBin.Write(w );

            
            //Вводим в файл данные(перезаписываем) - таблицу с уже измененным столбцом
            for (int i = 0; i < h; i++)
            {
                for (int jj = 0; jj < w; jj++)
                {
                    outBin.Write(mass[i,jj]);
                }
                //На новую строку:
                //outBin.Write();
            }

            
            outBin.Close();
        
        
        }

        static void Main(string[] args)
        {
            //Имя файла
            Console.WriteLine("Введите имя файла, который вы хотите открыть:");
            string name;
            name=Console.ReadLine();
            name += ".bin";

            /*
            //Поток для записи
            BinaryWriter outBin = new BinaryWriter(File.Open(s, FileMode.Create));
            //Записываем в файл данные
            //Сначала вводим высоту и ширину
            int h, w;
            Console.WriteLine("Введите высоту = ");
            h = int.Parse(Console.ReadLine());
            outBin.Write(h);
            
            Console.WriteLine("Введите ширину = ");
            w = int.Parse(Console.ReadLine());
            outBin.Write(w);

            
            //Вводим в файл данные
            int x;
            Console.WriteLine
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    outBin.Write(
                }

            }

            outBin.Close();
            */

            int x=0, j=0;
            Console.WriteLine("Введите число, на которое нужно произвести замену в файле:");
            x = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите номер столбца(НОЛЬ - не номер столбца!) :");
            j = int.Parse(Console.ReadLine());

            //Функция изменения столбца таблицы
            //Из-за отчета от нуля, а не от единицы - номер столбца сдвигаем!
            ReplaceCol(j-1, x, name);
            
            
            Console.ReadKey();
        }
    }
}
