using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//Сортировка пузырьком
namespace Sorts
{
    //Класс дерево
    class Tree
    {
        public string value;
        public Tree left;
         public Tree right;
    }
        
    
    class Program
    {

        //Добавление в дерево
        static public Tree Add(Tree root, string new_value)
        {
            if (root == null) // если нет сыновей - создаем новый элемент
            {
                root = new Tree();
                root.value = new_value;
                root.left = null;
                root.right = null;
                return root;
            }
            if (root.value.CompareTo(new_value) < 0)  // добавляем ветвь
            {
                root.right = Add(root.right, new_value);
            }
            else
            {
                root.left = Add(root.left, new_value);
            };

            return root;
        }
        //Перевод в массив
        static public void TreeToNewArray(Tree root, string[] mass, ref int max)
        {
            if (root == null) return;  // условие окончания - нет сыновей
            TreeToNewArray(root.left, mass, ref max);
            mass[max++] = root.value;
            TreeToNewArray(root.right, mass, ref max);
        }
        //"Деревянная" сортировка
        static public void TreeSort(ref string[] mass)
        {
            int max = 0;
            Tree root = null;
            for (int i = 0; i < mass.Length; i++)
            {
                root = Add(root, mass[i]);
            }
            TreeToNewArray(root, mass,ref max);
        }   




        static Random r = new Random(DateTime.Now.Millisecond);
        //Задаем рандомный массив
        static string[] RandomMass(int n)
        {
            string[] mass = new string[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = Word(6);//Слово с длинной в 10 символов
            }

            return mass;
        }

        //Формируем слово определенного размера
        static string Word(int n)
        {
            StringBuilder builder = new StringBuilder();
            char ch;//Используется для генерации буквы
            for (int i = 0; i < n; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32((r.Next(26) + 65)));
                //26 - слов в английском алфавите, 65 - код символа А
                if(r.Next(2)%2==1)//Рандомный выбор - маленькие или большие буквы
                builder.Append(ch.ToString().ToLower());
                builder.Append(ch.ToString().ToUpper());
            }
            return builder.ToString();
        }
        //Рандомное формирование слов для массива
        //Задаем тяжелую форму массива - обратно упорядоченный.
        //Для этого - забиваем случайными числами И обратно их упорядочиваем.
        static string[] HardMass(int n)
        {
            string[] mass = new string[n];
            mass = RandomMass(n);
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length - 1; j++)
                {
                    if (mass[j].CompareTo(mass[j+1])<0)
                    {
                        Swap(mass, j, j + 1);
                    }
                }
            }

            return mass;
        }
        //Функция обмена
        static void Swap(string[] mass, int i, int j)
        {
            string temp = mass[i];
            mass[i] = mass[j];
            mass[j] = temp;
        }
        
        //Печать массива
        static void PrintMass(string[] mass, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(mass[i] + " ");
            }
            Console.WriteLine();
        }
        //Рассчет времени сортировки для рандомного массива
        static double TimeOfSort(string[] mass)
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;//Первая точка

            TreeSort(ref mass);

            date2 = DateTime.Now;//Вторая точка

            return (date2 - date1).TotalMilliseconds;
        }

        //Визуализация сортировки
        static void Visualization()
        {
            int n;//Количество элементов
            int m = 50;//Базовое количество элементов
            int y = 1;//Количество созданий массивов(количество проверок)
            double sumTime_1 = 0, sumTime_2 = 0;

            //Для старой версии - если массив уже упорядочен - сортировка все равно идет до конца
            //50, 500, 5000, 50000
            for (int i = 0; i < 4; i++)
            {
                double time_1 = 0, time_2 = 0;
                n = m * (int)Math.Pow(10, i);
                for (int j = 0; j < y; j++)
                {
                    string[] mass_1 = RandomMass(n);
                    string[] mass_2 = HardMass(n);//Копия
                   
                    time_1 = TimeOfSort(mass_1);//Считаем время рандомно сгенерированного массива//Происходят проверки, но перестановки могут и не быть
                    time_2 = TimeOfSort(mass_2);//Считаем время с учетом возможности его частичной отсортированности
                
                    sumTime_1 = sumTime_1 + time_1;
                    sumTime_2 = sumTime_2 + time_2;
                }
                sumTime_1 = sumTime_1 / y;//Среднее время сортировки
                sumTime_2 = sumTime_2 / y;

                Console.WriteLine(n + " elements time: " + "SIMPLE = " + sumTime_1 + " HARD = " + sumTime_2);
            }
        }

        static void Main(string[] args)
        {
            Visualization();

            Console.ReadKey();
        }
    }
}
