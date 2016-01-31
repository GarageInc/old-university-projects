using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Regina_2
{
    class Program
    {
        //Наша функция записи в файл
        public static void TableToFile(double[,] a,int h, int w, string name_file)
        {
            StreamWriter sw = new StreamWriter(name_file);
            //Пишем - количество строк и столбцов в массиве - высота h и ширина w
            sw.WriteLine(h);
            sw.WriteLine(w);

            //Записываем всю таблицу
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                    sw.Write("{0,-10} ",a[i,j]);//Форматируемый вывод дробных чисел - каждому числу дается пространство в -10 символов. Чтобы таблица не вышла "косой";
                sw.WriteLine();//Переход на новую строку
            }

                    //Закрываем поток-сохраняем данные
                    sw.Close();
            //Результат - см в файле
        }





        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя файла с расширением (file.txt): ");
            string s = Console.ReadLine();//Имя файла

            //Высота и ширина массива
            int w,h;
            Console.WriteLine("Введите высоту:");
            h=int.Parse(Console.ReadLine());
            Console.WriteLine("Введите ширину:");
            w=int.Parse(Console.ReadLine());

            

            double[,] a = new double[h,w];

            Console.WriteLine("Начните ввод данных(чисел) в массив дробных чисел - через Enter :"); 
            //Вводим данные в массив
            for (int i = 0; i < h; i++)//Идем по высоте
                for (int j = 0; j < w; j++)//Идем по ширине
                    a[i, j] = double.Parse(Console.ReadLine());

            //Вызываем функцию добавления в файл
                    TableToFile(a,h,w,s);//Передаем - массив, его длину и ширину, имя файла(в который запишем данные из массива)


                    Console.WriteLine("Программа завершила работу. Результат - смотрите в Вашем файле");
            Console.ReadKey();
        }
    }
}
