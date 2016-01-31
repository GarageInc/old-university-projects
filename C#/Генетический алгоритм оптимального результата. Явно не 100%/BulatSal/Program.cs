using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulatSal
{
    class Program
    {
        //Алгоритм, генетический. Получаем массив двумерный с данными
        //Итог алгоритма - выбор наиболее оптимального значения - длины выполнения S
        //S - время выполнения
        //P - время простоя
        //N - количество строк
        //M - количество столбцов
        //mass - собственно наш массив
        public static void Search(int[,] mass, int N, int M)//Поиск в заданном массиве минимального Времени выполнения и Времени простоя
        {
            //Получаем массив по всем вызовам комбинаций
            //Передаем: 0 - элемент, от которого считаем, 
            //N-число, до которого считаем
            //x-массив для записи элементов
            //0 - index для заполнения massiv
            //massiv - массив для получения массив с переставленными
            //Получим факториал N - всего элементов в массиве
            int[] x = new int[Fact(N)];
            int[,] massiv = new int[Fact(N), N];
            int index = 0;
            int[,] perestanovki = NumbersOfPerestanovok(0, N, x, ref index, massiv);
            
            //Теперь - каждый раз получая из нашего массива проектов НОВЫЙ массив с перестановками строк по полученной комбинации - ищем оптимальное время выполнения.
            //Реализуем поиск времени выполнения работы и времени простоя
            //Ищем первый элемент для сравнения     
            int S=0, P=0;//Величины, которые нужно найти
            int index_comb=0;//Индекс комбинации в массиве perestanovki
            
            //Т.к. W=0 проходку сделали, начинаем с W=1;
            for (int W = 0; W < index; W++)
            {
                int[,] new_mass=new int[N,M];
                //Передаем исходный массив проектов, его параметры - число СТРОК и СТОЛБЦОВ, комбинацию перестановок по индексу и Индекс(номер по порядку перестановки)/
                new_mass = PerestanovkaColumns(mass, N, M, perestanovki,W);
                int S_new = 0;
                int P_new = 0;
                //Создаем одномерный массив для анализа времени выполнения. Первый столбец - запись времени выполнения, второй столбец - какой этап работы
                int[] time = new int[M];
                
                //Производим обработку - обход массива
                //Идем вниз по таблице
                for (int j = 0; j < N; j++)
                {
                    //Идем вправо по таблице
                    for (int i = 0; i < M; i++)
                    {
                        //Первый(нулевой) элемент - в любом случае нужно записывать
                            if (j != 0)
                            {
                                //Если предыдущий пункт выполнен - то прибавляем. Но и проводим проверку - если сумма предыдущего больше образовавшейся - суммы - то это явно время простоя
                                //Проверка на "простой"(заштрихованная область) работы.
                                    if (i != 0)
                                    {
                                        if (time[i - 1] > time[i])
                                        {
                                            P_new = P_new + (time[i - 1] - time[i]);
                                            time[i] = time[i - 1] + new_mass[j, i];
                                        }
                                        else
                                        {
                                            time[i] = time[i] + new_mass[j, i];
                                        }
                                    }
                                    else
                                    {
                                        time[i] = time[i] + new_mass[j, i];
                                    }
                            }
                            else
                            {
                                //Сохраняем значение первого считывания
                                if (i != 0)
                                {
                                    time[i] = time[i - 1] + new_mass[j, i];
                                }
                                else
                                {
                                    time[i] = new_mass[j, i];
                                }
                            }
                    }
                }
               
                //Ищем минимальное время выполнения
                S_new=Time(time, M);

                //Теперь среди всех наименьших S выберем - с наименьшим "простоем" работы P.
                if(W==0)
                {
                    S = S_new; P = P_new; index_comb = W;
                }
                else
                {
                    if (S_new == S)
                    {
                        if (P_new < P)
                        {
                            P = P_new; index_comb = W;
                        }
                    }
                    else
                    {
                        if (S_new < S)
                        {
                            S = S_new; P = P_new; index_comb = W;
                        }
                    }

                }

            }
            //Печатаем сумму!
            Console.WriteLine(S);
            //Печатаем время простоя!
            Console.WriteLine(P);
            //Печатаем комбинацию. ВНИМАНИЕ! Берется последняя комбинация из множества удачных по S. Т.к. (N! - для 7: N!=7*6*5*4*3*2*1=5040 комбинаций. Среди них - полно удачных версий)
            PrintCombination(perestanovki, index_comb, N);
            //return S;
        }

        //Факториал
        public static int Fact(int N)
        {
            int F = N;
            for (int i = 1; i <= N; i++)
                F = F * i;
            return F;
        }
        //Время выполнения
        public static int Time(int[] time, int M)
        {
            int t=time[0];
            if (M!=1)//Если не одна строчка. Мб и такой вырожденный случай для задачи
            {
                for (int i = 1; i < M; i++)
                    if (t < time[i]) t = time[i];
            }
                else return t;
            return t;
        }

        //Печать комбинации
        public static void PrintCombination(int[,] perestanovki, int w, int N)
        {
            for (int i = 0; i < N; i++)
                Console.Write(perestanovki[w,i]+" ");
            Console.WriteLine();
        }

        //Формирование нового массива по коду перестановок
        public static int[,] PerestanovkaColumns(int[,] mass, int N, int M, int[,] perestanovki, int W)
        {
            int[,] new_mass= new int[N,M];

            //Идем по строкам
            for(int i=0; i<N; i++)
            {
                //Копируем каждую строку
                for(int j=0; j<M; j++)
                {
                    new_mass[i,j]=mass[perestanovki[W,i]-1,j];
                }
            }
            return new_mass;
        }

        //Рекурсивная функция получения комбинаций вызовов
        //Передается число, от которого отсчитывать и число, к которму считать перестановку, например [от 3, до 9]
        //x - массив, в который записываются различные комбинации
        public static int[,] NumbersOfPerestanovok(int k, int n, int[] x,ref int index, int[,] mass)
        {
            if (k == n)
            {
                int schet = 0; //Переменнная, которая подсчитывает количество НЕповторений всех элементов в ряду перестановки.
                int poz = 0;//Переменная, для сравнения с вышеназванной schet. 

                for (int t = 1; t < n; t++)//Подсчитывает все числа от 1 до N - количество именно сравнений на повторяемость элементов. В принципе, можно было вывести цикл
                    //вне процедуры ради экономии времени подсчета, работы рекурсии. Но пришлось бы вводить новые переменные для передачи в процедуру.
                    poz = poz + t;

                for (int w = 0; w < n; w++)//Элементарный цикл подсчета НЕповторяемости элементов в перестановке(массиве)
                    for (int y = w + 1; y < n; y++)
                    {
                        if (x[w] != x[y]) schet++;
                    }

                if (schet == poz)//В зависимости от равенства переменных происходит печать подстановки. 
                //В результате печатаются подстановки, в которых отсутствует повторяемость элементов.
                {
                    for (int i = 0; i < n; i++)
                    {
                        mass[index, i] = x[i];
                    }
                    index++;
                }
                
            }
            else
            {
                for (int r = 0; r < n; r++)
                {
                    x[k] = r + 1;
                    NumbersOfPerestanovok(k + 1, n, x, ref index, mass);
                }
            }
            return mass;
        }

        static void Main(string[] args)
        {
            const int  N=7, M=5;//N-количество строк, M-столбцов
            int[,] mass = new int[N,M] 
            {
                {3, 4, 2, 3, 5},
                {2, 3, 1, 4, 4},
                {2, 2, 1, 5, 4},
                {1, 3, 2, 3, 3},
                {2, 4, 4, 2, 4},
                {3, 3, 5, 1, 4},
                {1, 3, 3, 3, 5},
            };

 
            Search(mass, N, M);

            Console.ReadKey();
        }
    }
}
