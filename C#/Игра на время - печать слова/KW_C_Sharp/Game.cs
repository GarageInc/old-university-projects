using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace Game_of_Words
{
    //Для считывания участника в top10.txt
    public struct Member
    {
        public string FIO;//ФИО участника
        public int time;//Результативное время участника

        public Member(string fio, int t)
        {
            this.FIO = fio;
            this.time = t;
        }
    };

    class Game
    {
        //Переводим из temp всех участников-победителей в массив структуры Members
        Member[] members;
        //Инициализируем переменную по умолчанию
        int memLength;

        //Конструктор
        public Game()
        {
            this.members = new Member[11];
            this.memLength = 0;
        }

        //Старт игры
        public void Start()
        {
            Console.WriteLine("   GAME WITH WORDS!   \n");
            string[] text;
            string[] example;
            //Читаем из файлов
            //Проверим наличие файлов
            if (!System.IO.File.Exists("text.txt") || !System.IO.File.Exists("top10.txt"))
            {
                Console.WriteLine("Требуемые файлы text.txt или top10.txt отсутствуют!");
                return;
            }; ;

            //Потоки для чтения
            //Текст с примерами строк: text.txt
            StreamReader sr1 = new StreamReader("text.txt", Encoding.GetEncoding(1251));//Encoding - для корректного считывания с файла
            //Текст top10.txt
            StreamReader sr2 = new StreamReader("top10.txt", Encoding.GetEncoding(1251));//Encoding - для корректного считывания с файла


            //Считываем всё
            text = sr1.ReadToEnd().Split('\n');//Считываем все числа в массив строк. Как разделитель - берется знак пробела ' ';
            example = sr2.ReadToEnd().Split('\n');
            //Файлы ссчитали, закрываем файлы для чтения
            //Закрываем потоки.
            sr1.Close();
            sr2.Close();
            //Переводим из temp всех участников-победителей в массив структуры Members
            Member[] members = new Member[11];
            //Инициализируем переменную по умолчанию
            int memLength = 0;

            //Преобразуем в данные об участнике для структуры Members 
            for (int i = 0; i < example.Length && i < 10; i++)
            {
                if (example[i].CompareTo("") == 0) break;
                string[] m = example[i].Split(' ');
                members[i] = new Member(m[0], int.Parse(m[1]));
                memLength++;
            }

            //Запускаем игру
            ToGame(members, text, memLength);
        }


        //Функция проверки на продолжение игры
        private static bool ContinueGame(Member[] members, int memLength)
        {
            string word;
            Console.WriteLine("\n ПРОДОЛЖАТЬ ИГРУ? (Введите: Да = 1, Нет = 0) \n");
            //Флаг правильного ввода 0 или 1
            bool b = true;
            while (b)
            {
                word = Console.ReadLine();

                if (word.CompareTo("0") == 0)
                {
                    Console.WriteLine(" ВЫХОД! Нажмите Enter.. \n");
                    b = false;

                    //Реализуем выход из программы и запись в top10.txt
                    StreamWriter sw = new StreamWriter("top10.txt", false, Encoding.GetEncoding(1251));//false - перезапись, Encoding - для корректного считывания с файла

                    for (int i = 0; i < 10 && i < memLength; i++)
                    {
                        sw.WriteLine(members[i].FIO + " " + members[i].time);
                    }

                    sw.Close();
                    //Выходим
                    Console.ReadKey();
                    return false;

                }
                else if (word.CompareTo("1") == 0)
                {
                    Console.WriteLine(" ПРОДОЛЖАЕМ ИГРУ! \n");
                    b = false;
                }
                else
                {
                    Console.WriteLine(" Введите или 0=Нет, или 1=Да \n");
                }

            }

            return true;
        }

       
        //Функция самой игры
        private static void ToGame(Member[] members, string[] text, int memLength)
        {
            Random r = new Random();
            //Бесконечный цикл с уловием выхода
            while (true)
            {

                //Просим ввести слово и считаем время написания слова
                Console.WriteLine("\n ВВЕДИТЕ ПРИВЕДЕННЫЙ ТЕКСТ через Enter:");
                string word_result = text[r.Next(text.Length - 1)];
                Console.WriteLine(word_result);  //Рандомно выдергиваем строку из нашего текста

                DateTime start = new DateTime();
                start = DateTime.Now;

                string word = Console.ReadLine();

                DateTime finish = new DateTime();
                finish = DateTime.Now;

                int result = (finish - start).Milliseconds;//Переводим в миллисекунды
                //Если слово введено верно - проводим проверку в массиве структур Members

                word_result = word_result.Remove(word_result.Length - 2);
                if ((word_result).CompareTo(word) == 0)
                {
                    //Проверка - есть ли элементы больше заданного и и если есть - то добавляем человека в список
                    bool proverka = false;//Проверка наличия в списке больших результатов
                    //Сначала отсортируем наш список, если в нем элементов больше 1
                    if (memLength > 1)
                    {
                        for (int i = 0; i < 10 - 1 && i < memLength - 1; i++)
                            for (int j = 0; j < 10 - i - 1 && j < memLength - i - 1; j++)
                            {
                                if (members[j].time > members[j + 1].time)
                                {
                                    Member tmp = members[j];
                                    members[j] = members[j + 1];
                                    members[j + 1] = tmp;
                                }
                            }
                    }; ;
                    //Ищем результат меньше нашего или же пустое место, если оно существует
                    int poz = 0;
                    for (; poz < memLength && poz < 10; poz++)
                    {
                        if (members[poz].time >= result)
                        {
                            proverka = true; break;//Выходим из цикла, если нашли
                        }
                    }
                    if (poz < 10) proverka = true; ;
                    //Если можно записать результат - то записываем!
                    if (proverka == true)
                    {
                        //Сначала найдем элемент, который нужно "потеснить" и вставляем наш результат. Т.е. 3 этапа
                        //Вставляем результат
                        Console.WriteLine("Вы вошли в ТОП10! Введите своё ФИО в формате ФАМИЛИЯ_ИМЯ:");
                        string fio = Console.ReadLine();

                        //Проверим на правильность ФАМИЛИЯ_ИМЯ, если с пробелом - это ошибка
                        while ((fio.Contains(' ') == true))
                        {
                            Console.WriteLine("Неправильный ввод фамилии с пробелом! Введите идентификатор формата ФАМИЛИЯ_ИМЯ!");
                            fio = Console.ReadLine();
                        }

                        //Записываем пользователя и сохраняем того пользователя, на место которого его записали
                        Member tmp = members[poz];
                        members[poz] = new Member(fio, result);
                        memLength++;


                        //Cдвигаем
                        for (int j = poz + 1; j < 10 && j < memLength; j++)
                        {
                            Member tmp2 = members[j];
                            members[j] = tmp;
                            tmp = tmp2;
                        }

                        //    }
                        //}
                    }
                }
                else
                    Console.WriteLine("Неверный ввод! \n");
                //Иначе - спрашиваем, продолжать или нет игру?
                //Спрашиваем о продолжении игры

                if (ContinueGame(members, memLength) == false) return;//Выходим, если игру не нужно продолжать

            }

        }

    }
}