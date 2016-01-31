using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Khasanov_Raikhan
{
    class Program
    {


        static void Main(string[] args)
        {
            //Поток для вывода
            StreamWriter sw = new StreamWriter("vectors.txt");

            
            int n = 0;//Длина отдельного вектора
            double k = 0;//Отдельная координата вектора

            //Вводим векторы в файл
            Console.WriteLine("Введите количество векторов: ");
            int kol;//Количество векторов?
            kol = int.Parse(Console.ReadLine());

            Console.WriteLine("Ввод векторов: ");
            Console.WriteLine();

            //Ввод в файл
            for (int i = 0; i < kol; i++)
            {
                Console.WriteLine("  Длина вектора №"+(i+1));
                n=int.Parse(Console.ReadLine());
                sw.Write(n + " ");//Записываем в файл

                Console.WriteLine("    Введите координаты ветора(через Enter) вектора №"+(i+1));
                for (int j = 0; j < n; j++)
                {
                    k = double.Parse(Console.ReadLine());
                    //Записываем в файл
                    sw.Write(k + " ");
                }
            }
            //Закрываем поток
            sw.Close();

            //Поток для чтения
            StreamReader sr = new StreamReader("vectors.txt", Encoding.GetEncoding(1251));//Encoding - для корректного считывания с файла
            

            //Считываем всё
            string[] str = sr.ReadLine().Split(' ');//Считываем все числа в массив строк. Как разделитель - берется знак пробела ' ';

            int kol_vectors = 0;//Количество векторов

            for (int i = 0; i < str.Length; i++)
            {
                //Если мы дошли до конца и прочитали пробел - это выход из цикла
                if (str[i].CompareTo(" ") == 0) break; ;
                n = int.Parse(str[i]);//Переводим в число
                i++;
                
                for (int j = 0; j < n; j++, i++)//Идем по координатам вектора
                {
                    k = double.Parse(str[j]);//Эту строку можно убрать даже.
                    //Тут ничего не происходит. Просто проходим по вектору
                }
                
                //Прочитали вектор->увеличиваем счетчик на единицу
                kol_vectors++;
            }

            

            //Закрываем поток чтения
            sr.Close();

            //Выводим результат
            Console.WriteLine("Количество векторов = "+kol_vectors);
            //Делаем задержку
            Console.ReadKey();

        }
    }
}
