using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Diana
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader rd1 = new StreamReader("spisok1.txt", Encoding.GetEncoding("Windows-1251"));//Файл для считывания данных в первый список
            StreamReader rd2 = new StreamReader("spisok2.txt", Encoding.GetEncoding("Windows-1251"));//Файл для считывания данных во второй список
            StreamWriter wr = new StreamWriter("output.txt");//Файл для записи результатов

            Kolco k = new Kolco();
            
            int n = 0;
            while (n != 5)
            {
                n = Visualisation.Choice();//Выбираем выполняемое действие
                if (n != 5)//Если не выбрали выход
                {
                    //Считываем в список
                    Visualisation.Read(ref k, rd1);

                    Visualisation.Write(k, wr);

                    switch(n)//Выполняем требуемое действие
                    {
                        case 1:
                        {
                            Instruments.Sorting(ref k);
                            wr.WriteLine("РЕЗУЛЬТАТ СОРТИРОВКИ:");
                            Visualisation.Write(k, wr);
                        }
                        break;
                        case 2:
                        {
                            Console.WriteLine("Какой каждый к-ый элемент удалить? Введите число к = ");
                            int el;
                            el=int.Parse(Console.ReadLine());
                            Instruments.Deleting_k(ref k, el);
                            wr.WriteLine("РЕЗУЛЬТАТ УДАЛЕНИЯ КАЖДОГО К-го УЧАСТНИКА:");
                            Visualisation.Write(k, wr);
                        }
                        break;
                        case 3:
                        {
                            Kolco k2 = new Kolco();//Второе кольцо
                            //Считываем во второй список
                            Visualisation.Read(ref k2, rd2);

                            wr.WriteLine("Второй список. ");
                            Visualisation.Write(k2, wr);

                            wr.WriteLine("РЕЗУЛЬТАТ ПОСТРОЕНИЯ НОВОГО СПИСКА ИЗ УЖЕ ИМЕЮЩИХСЯ ДВУХ:");
                            Kolco k3 = Instruments.Merge(k, k2);//Слияние; слияние 1 и 2 ( в первое кольцо добавляются элементы 2го)
                            Instruments.Delete(ref k2);//Удаляем ненужный список
                            
                            Visualisation.Write(k3, wr);
                            
                        }
                        break;
                        case 4:
                        {
                            Kolco M = new Kolco();//Будущий список только мужчин
                            Kolco W = new Kolco();//Будущий список только женщин

                            Instruments.Separe(M, W, k);//Фунция деления на два списка
                            wr.WriteLine("РЕЗУЛЬТАТ ДЕЛЕНИЯ НА ДВА СПИСКА-КОЛЬЦА(по мужчинам и женщинам):");
                            Visualisation.Write(M, wr);
                            Visualisation.Write(W, wr);

                            Instruments.Delete(ref M);//Удаляем списки, т.к. больше не используются
                            Instruments.Delete(ref W);
                        }
                        break;
                    }
                }
                
            }
            Instruments.Delete(ref k);//Удаляем ненужный список
            wr.Close();//Иначе - закрываем потоки
            rd1.Close();
            rd2.Close();
        }
    }
}
