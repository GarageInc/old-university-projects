using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GraphGen
{
    class Program
    {
     
        public static int [,] Create(int n)
        {
            int [,] a = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        a[i, j] = -1;
                    else
                        a[i, j] = 0;
                }
            return a;
        }
        public static void random1(int [,] a,int n)
        {
            Random rnd = new Random();
            for (int i =0;i<n;i++)
                for (int j=i+1;j<n;j++)
                    a[i,j] = rnd.Next(0, 2);
        }
        public static void Prov(int[,] a, int n, int k)
        {
            int kol = 0;
            for (int i = k + 1; i < n; i++)
                if (a[k, i] != 0)
                    kol++;
            if (kol == 0)
            {
                Random rnt = new Random();
                int p = rnt.Next(k+1, n);
                a[k, p] = 1;
            }

        }
        public static void Prov2(int[,] a, int n, int k)
        {
            int kol = 0;
            for (int i = 0; i < k; i++)
                if (a[i, k] != 0)
                    kol++;
            if (kol == 0)
            {
                Random rnt = new Random();
                int p = rnt.Next(0, k);
                a[p, k] = 1;
            }

        }
        public static void randomen(int [,] a,int n)
        {
            random1(a, n);
            for (int i = 0; i < n - 1; i++)
            {
                Prov(a, n, i);
            }
            for (int j = 1; j < n; j++)
                Prov2(a, n, j);
        }
        public static void duplicate(int[,] a, int n)
        {
            for (int i=0;i<n;i++)
                for (int j=0;j<n;j++)
                    if (a[i,j]==1)
                        a[j,i]=1;
        }
        public static void RandomAll(int[,] a, int n)
        {
            randomen(a, n);
            duplicate(a, n);
            Random rnd2 = new Random();
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (a[i, j] == 1)
                    {
                        a[i, j] = rnd2.Next(1, 20);
                        a[j, i] = a[i, j];
                    }
                   
                }
         
       
        }
        public static int Rebra(int[,] a, int n)
        {
            int rebra = 0;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    if (a[i, j] != 0)
                        rebra++;
            return rebra;
        }
        public static int[,] Spisok(int[,] a, int n)
        {
            int rebra = Rebra(a, n);
            int[,] b = new int[3, rebra];
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                    if (a[i, j] != 0)
                    {
                        b[0, k] = a[i, j];
                        b[1, k] = i;
                        b[2, k] = j;
                        k++;
                    }
            }
            return b;
        }
        public static void GraphSort(int[,] a, int n)
        {
            int a1=0;
            int a2=0;
            int a3=0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (a[0, j] > a[0, j + 1])
                    {
                        a1 = a[0, j];
                        a2 = a[1, j];
                        a3 = a[2, j];
                        a[0, j] = a[0, j + 1];
                        a[1, j] = a[1, j + 1];
                        a[2, j] = a[2, j + 1];
                        a[0, j + 1] = a1;
                        a[1, j + 1] = a2;
                        a[2, j + 1] = a3;
                    }
                }
            }

                    
        }
        public static void Craskall(int[,] a, int n,int reb)
        {
            GraphSort(a, reb);
            int cost = 0;
            int[,] res = new int[2, reb];
            int[] tree_id = new int[reb];
            for (int i = 0; i < reb; ++i)
                tree_id[i] = i;
            for (int i=0; i<reb; ++i)
            {
                int k1=a[1,i];
                int k2=a[2,i];
                int k3=a[0,i];
                if (tree_id[k1]!=tree_id[k2])
                {
                    cost=cost+k3;
                    res[0,i]=k1;
                    res[1,i]=k2;
                    int old_id = tree_id[k2],  new_id = tree_id[k1];
                    for (int j=0; j<reb; ++j)
			            if (tree_id[j] == old_id)
				         tree_id[j] = new_id;
                }
            }
           // Console.WriteLine(cost);
        }

        public static int min(int a, int b)
        {
            if (a>b)
                return b;
            else
                return a;
        }
        public static void Prim(int[,] graph, int n)
        {
            int INF = 10000;
            bool[] used = new bool[n];
            int[] dist = new int[n];
            for (int i = 0; i < n; i++)
            {
                dist[i] = INF;
                used[i] = false;
            }
            dist[0] = 0;
            for (; ; )
            {
                int v = -1;
                for (int nv = 0; nv < n; nv++)
                    if (!used[nv] && dist[nv] < INF && (v == -1 || dist[v] > dist[nv]))
                         v = nv;
                if (v == -1)
                    break; 
               used[v] = true;
                for (int nv = 0; nv < n; nv++)
                 if (!used[nv] && graph[v,nv] < INF  && graph[v,nv]!=0)
                     dist[nv] = min(dist[nv], graph[v,nv]); 
            }
            int ret = 0;
       for (int v = 0; v < n; v++)
             ret =ret+ dist[v];
      // Console.WriteLine(ret);
            }
        public static void Craskallanalitics(int a, int b)
        {
            int[,] c;
            int[,] d;
            for (int i = a; i < b + 1; i++)
            {
                Stopwatch watch = new Stopwatch();
                for (int k = 0; k < 100; k++)
                {
                    c = Create(i);
                    RandomAll(c, i);
                    d = Spisok(c, i);
                    watch.Start();
                    Craskall(d, i, Rebra(c,i));
                    watch.Stop();
                }
                TimeSpan ts = watch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours , ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
                Console.WriteLine("Для 100 сегенерированных графов с количеством вершин равным "  + i+" Среднее время работы алгоритма Краскала  равно "+elapsedTime);

            }
        }
        public static void Primanalitics(int a, int b)
        {
            int[,] c;
            int[,] d;
            for (int i = a; i < b + 1; i++)
            {
                Stopwatch watch = new Stopwatch();
                for (int k = 0; k < 100; k++)
                {
                    c = Create(i);
                    RandomAll(c, i);
                    watch.Start();
                    Prim(c, i);
                    watch.Stop();
                }
                TimeSpan ts = watch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
                Console.WriteLine("Для 100 сегенерированных графов с количеством вершин равным " + i + " Среднее время работы алгоритма Прима равно " + elapsedTime);

            }
        }
        static void Main(string[] args)
        {
           /* int[,] a;
            int[,] b;
            int n = 8;
            a= Create(n);
            RandomAll(a, n);
            int reb = Rebra(a, n);
            b = Spisok(a, n);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < reb; j++)
                    Console.Write(b[i, j] + "   ");
                Console.WriteLine();
            }
            Prim(a, n);
            Craskall(b, n,reb);
           
            */

            Craskallanalitics(50, 200); // запускаем и анализируем графы с количеством вершин от 50 до 200 , по 100 графов для каждого вида. Круска
            //Primanalitics(50, 200);// алгоритм Прима
            Console.ReadLine();
        }
    }
}
