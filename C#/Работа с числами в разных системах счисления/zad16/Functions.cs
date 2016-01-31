using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zad16
{
    class Functions//Список основных функций для работы с Scalenum
    {
        public static Scalenum Coding(int n, int q = 10)//Построение числа в q-ичной системе счисления по заданному числу в 10-тичной системе:
        {//n - преобразуемое число;q - основание системы счисления
            Scalenum s = new Scalenum(q);//Переменная для результата
            for (int i = 0; n != 0; i++, n /= q)
            {
                if (n % q != 0)
                {
                    s.digits.Push(n % q);
                    s.degrees.Push(i);
                }
            }
            return s;
        }

        public static int Decoding(Scalenum sc)//Построение числа в 10-ичной системе счисления по заданному числу в q -ичной системе:
        {//sc - преобразуемое число
            int n = 0;//Переменная для результата
            for (int i = 0, j = 0, q = 1; i < sc.degrees.Length; i++)
            {
                for (; j < sc.degrees[i]; j++, q *= sc.q) ;
                n += sc.digits[i] * q;
            }
            return n;
        }

        public static void Delete(Scalenum sc)//Уничтожение списков:
        {//sc - Освобождаемый класс
            //sc.digits.Clear();
            //sc.degrees.Clear();
            sc.degrees = null;
            sc.digits = null;
        }

        public static void QMult(Scalenum sc)//Умножение числа на q:
        {//sc - число которое умножаем
            for (int i = 0; i < sc.degrees.Length; i++)
                sc.degrees[i]++;
        }

        public static void SumConst(ref Scalenum sc, int n)//Увеличения числа на на константу 
        {//sc - число;n - константа
            int m = Decoding(sc) + n;
            sc = Coding(m, sc.q);
        }

        public static int MED(Scalenum sc)//Нахождение цифры, которая встречается в представлении числа максимальное число раз:
        {//sc - число
            int[] a = new int[sc.q];//Количество нахождений цифр в числе
            for (int i = 0; i < sc.digits.Length; i++)
                a[sc.digits[i]]++;
            a[0] = sc.degrees[sc.degrees.Length - 1];
            for(int i = 1;i < sc.q;i++)
                a[0]-= a[i];
            int max = -1, imax = -1;
            for(int i = 0;i < sc.q;i++)
                if (a[i] > max)
                {
                    max = a[i];
                    imax = i;
                }
            return imax;
        }
    }
}
