using System;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        //создать класс vector для хранения векторов.определить в нем конструктор, 
        //свойства для получения размерности вектора, получения и изменения j-ой координаты.
        
        //перегрузить операции сложения векторов и вычисления скалярного произведения
        public class Vector
        {
            private int[] vector;//Сам вектор

            //Конструктор с параметрами по умолчанию
            public Vector(int[] mass)
            {
                this.vector=mass;
            }

            //Получение размерности вектора
            public int Size
            {
                get { return vector.Length; }
                //Размерность не меняем, поэтому нет свойства set{};
            }

            //Получение и изменение j-ой координаты
            public int Get_j_element(int j=0)
            {
                //Проверяем - не больше ли номер запрашиваемой позиции размера массива. Если нет - вернем требуемый элемент, иначе - вернем 0(false)
                if (j < vector.Length) return vector[j];
                else return 0;
               //Хотя вместо последней строки можно вот так:
                //else throw new Exception("Выход за границы вектора! Неверный ввод!");
            }

            //Изменение j-ой координаты
            public void Set_j_element( int value, int j = 0)
            {
                //Проверяем - не больше ли номер запрашиваемой позиции размера массива. Если нет - вернем требуемый элемент, иначе - вернем 0(false)
                if (j < vector.Length) vector[j]=value;
            }

            //Перегружаем операции сложения и скал.умножение. Проверяем длины!
            public static Vector operator +(Vector a, Vector b)
            {
                if(a.Size>b.Size)
                {
                    //Складываем по первому вектору
                    int[] mass=new int[a.Size];
                    for (int i = 0; i < a.Size; i++)
                    {
                        if (i < b.Size) mass[i] = a.Get_j_element(i) + b.Get_j_element(i);
                        else mass[i] = a.Get_j_element(i);
                    }
                    return new Vector(mass);

                }
                else
                {
                    //Иначе - по второму вектору
                    int[] mass = new int[b.Size];
                    for (int i = 0; i < b.Size; i++)
                    {
                        if (i < a.Size) mass[i] = a.Get_j_element(i) + b.Get_j_element(i);
                        else mass[i] = b.Get_j_element(i);
                    }
                    return new Vector(mass);
                }                 
            }

            public static  double operator *(Vector a, Vector b)
            {
                //Найдем угол между векторами
                double angle = (Poparn_sum(a, b)) / ((Lenght(a) * Lenght(b)));

                //Найдем скалярное произведение

                return Math.Cos(angle) * Lenght(a) * Lenght(b);

            }

            //Функция определяет длину вектора 
            public static double Lenght(Vector a)
            {
                double l=0;

                for (int i = 0; i < a.Size; i++)
                {
                    l += (a.Get_j_element(i) * a.Get_j_element(i));

                }

                //Ура! Нашли длину!
                l = Math.Sqrt(l);

                return l;

            }
            //Попарное суммирование по координатам
            public static double Poparn_sum(Vector a, Vector b)
            {
                double n=0;

                if (a.Size > b.Size)
                {
                    //Складываем по первому вектору
                    for (int i = 0; i < a.Size; i++)
                    {
                        if (i < b.Size) n += (a.Get_j_element(i) + b.Get_j_element(i));
                        else n += a.Get_j_element(i);
                    }

                }
                else
                {
                    //Иначе - по второму вектору
                    for (int i = 0; i < b.Size; i++)
                    {
                        if (i < a.Size) n += (a.Get_j_element(i) + b.Get_j_element(i));
                        else n += b.Get_j_element(i);
                    }

                }      

                return n;
            }
           

            //Печать вектора
            public void Print()
            {
                for (int i = 0; i < vector.Length; i++)
                    Console.Write(vector[i] + " ");
            }
        }

        static void Main(string[] args)
        {
            //Инициализируем векторы
            Vector A = new Vector(new int[] { 1, 2, 3, 4, 5, 6, 7});
            Vector B = new Vector(new int[] { 2, 2, 2, 3});

            //Печатаем векторы
            Console.WriteLine("Векторы: ");
                A.Print();
                Console.WriteLine();
                B.Print();
                Console.WriteLine();

            //Получим элемент первый от первого вектора
            Console.WriteLine("Первый элемент первого вектора: ");
            Console.Write(A.Get_j_element(0));
            Console.WriteLine();

            Console.WriteLine("Сумма произвольных векторов: ");
            (A + B).Print();
            Console.WriteLine();

            Console.WriteLine("Скалярное произведение произвольных векторов(как перемножение их длин на косинус угла между ними) векторов: ");
            Console.Write(A * B);


            // чтобы экран сразу не гас
            Console.ReadKey();
        }
    }

}