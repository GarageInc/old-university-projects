using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Diana
{
    class Instruments//Список основных функций для работы с Kolco-м
    {

        public static Kolco Coding(StreamReader rd)//Считывание с файла
        {
            Kolco k = new Kolco();//Переменная для результата
            string str="";//Считываем пол
            int vozr=0;//Считываем возраст

            while(!rd.EndOfStream)
            {
                    //Считываем данные
                    str = rd.ReadLine().ToString();
                    vozr = int.Parse(rd.ReadLine().ToString());
                    //Создаем человека
                    
                    //Добавляем в кольцо
                    k.people.Add(new Chelovek(str.ToString(), vozr));
            }
            return k;
        }

        public static string Decoding(Kolco k)//Построение вывода числа
        {
            return k.ToString();
        }

        public static void Delete(ref Kolco sc)//Уничтожение списков:
        {//sc - Освобождаемый класс
            sc.people.Clear();
            sc.people = null;
        }

        //Сортировка кольца
        public static void Sorting(ref Kolco k)
        {
            k.people.Sort();


            //Работа делегата реализована на примере http://msdn.microsoft.com/ru-ru/library/b0zbh7b6.aspx) - для работы сортировки кольца на основании возраста
            k.people.Sort(delegate(Chelovek x, Chelovek y)
            {
                if (x.vozrast == 0 && y.vozrast == 0) return 0;
                else if (x.vozrast == 0) return -1;
                else if (y.vozrast == 0) return 1;
                else return x.vozrast.CompareTo(y.vozrast);
            });
        }

        //Удаление каждого к-го элемента
        public static void Deleting_k(ref Kolco kolco, int k)
        {
            //Резервируем переменную
            int kol;//Для переменной к
            int z = 0;
            //Пока не останется один элемент - удаляем по 1 штуке за прогон по кольцу
            while (kolco.people.Count() > 1)
            {
                kol = k+z;
                //Предупреждаем вариант, когда k больше количества элементов в списке
                while (kol >= kolco.people.Count())
                    kol = kol - kolco.people.Count();
                kolco.people.RemoveAt(kol);
                z = kol;//Чтобы начать отсчет с номера того элемента, который удалили
            }
        }

        //Слияние двух колец в одно
        public static Kolco Merge(Kolco k, Kolco k2)
        {
            //Считываем со второго кольца и добавляем к первому
            foreach(Chelovek chel in k2.people)
                k.people.Add(chel);

            return k;
        }

        //Деление на 2 кольца мужчин и женщин
        public static void Separe(Kolco M, Kolco W, Kolco k)
        {
            //Считываем с базового кольца и распределяем по спискам
            foreach (Chelovek chel in k.people)
            {
                if (chel.s.CompareTo("M") == 0)
                //Если мужчина - в список мужчин
                    M.people.Add(chel);
                //иначе - в список женщин
                else
                    W.people.Add(chel);
            }
        }


    }
}
