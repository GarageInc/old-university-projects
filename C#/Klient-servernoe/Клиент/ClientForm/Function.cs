using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace ClientForm
{
    class Function
    {
        static public Random rand = new Random();

        // Получение MD5 хэша из входящей строки
        public static string calcHash(string input)
        {
            MD5 md5Hash = MD5.Create();
            //посчитали мд5 хэш, получили массив байт
            byte[] data = md5Hash.ComputeHash(getBytesFromStr(input));
            //переводим массив в строку
            return getStrFromBytes(data);
        }

        // Возведение в степень
        static public BigInteger PowMod(BigInteger a, BigInteger b, BigInteger n)
        {
            BigInteger temp;
            if (b == 1) { return a % n; }
            if (b == 0) { return 1; }

            temp = PowMod(a, b / 2, n);
            temp *= temp;
            temp %= n;
            if (b % 2 == 1)
            {
                temp *= a;
            }
            return temp % n;
        }

        // Проверка - является ли число простым, по методу Миллера-Рабина
        private static bool is_prime_MR(BigInteger R, int num)
        {
            Random random = new Random();
            BigInteger s = 0;
            BigInteger t = R - 1;

            while (t % 2 == 0)
            {
                t = t / 2;
                s++;
            }

            String st = "";
            int p;
            int q = Int32.MaxValue;
            BigInteger A;
            BigInteger b;
            BigInteger i = 0;

            for (int j = 1; j <= num; j++)
            {
                p = random.Next(10);

                if (p % 2 == 0)
                    p = p + 1;

                st = p.ToString();

                while (st.Length <= R.ToString().Length - q.ToString().Length)
                {
                    st = random.Next(q) + st;
                }
                try
                {
                    st = random.Next(10 ^ (R.ToString().Length - st.Length - 2)) + st;
                }
                catch { }

                A = BigInteger.Parse(st);

                if (A.IsOne)
                    A++;

                if ((R % A).IsZero)
                    return false;

                b = BigInteger.ModPow(A, t, R);

                if (b.IsOne)
                    continue;
                i = 0;

                do
                {
                    b = BigInteger.ModPow(b, BigInteger.One + BigInteger.One, R);
                    i++;
                }

                while (!(b.IsOne || b.ToString() == (R - 1).ToString() || i > s));

                if (i > s)
                    return false;
            }

            return true;
        }

        public static BigInteger num_gen(int size)
        {
            String tick = DateTime.Now.Ticks.ToString();
            tick = tick.Substring(tick.Length - 9, 9);

            Random random = new Random(Convert.ToInt32(tick));

            int p = random.Next(10);
            int q = Int32.MaxValue;

            String st = "";

            while (st.Length <= size - q.ToString().Length)
            {
                st = st + random.Next(q).ToString();
            }

            if (size - st.Length != 0)
            {
                int min = Convert.ToInt32(Math.Pow(10, size - st.Length - 1) - 1);
                int max = Convert.ToInt32(Math.Pow(10, size - st.Length) - 1);
                st = st + random.Next(min, max).ToString();
            }

            if (p % 2 == 0)
                p = p + 1;

            int len = st.Length;

            st = st.Remove(len - 1);
            st = st + p.ToString();

            BigInteger a = BigInteger.Parse(st);

            while (true)
            {
                if (is_prime_MR(a, 20)) break;
                a = a + 2;
            }

            return a;
        }


        // Шифрование по типу RC4
        public static string RC4(string input, string key)
        {
            StringBuilder result = new StringBuilder();

            int x, y, j = 0;
            int[] box = new int[256];

            for (int i = 0; i < 256; i++)
            {
                box[i] = i;
            }

            for (int i = 0; i < 256; i++)
            {
                j = (key[i % key.Length] + box[i] + j) % 256;
                x = box[i];
                box[i] = box[j];
                box[j] = x;
            }

            for (int i = 0; i < input.Length; i++)
            {
                y = i % 256;
                j = (box[y] + j) % 256;
                x = box[y];
                box[y] = box[j];
                box[j] = x;

                result.Append((char)(input[i] ^ box[(box[y] + box[j]) % 256]));
            }

            return result.ToString();
        }

        // Получение массива байт из строки
        public static byte[] getBytesFromStr(string s)
        {
            return Encoding.GetEncoding(1251).GetBytes(s);
        }

        // Получение строки из байтов
        public static string getStrFromBytes(byte[] b)
        {
            return Encoding.GetEncoding(1251).GetString(b);
        }
    }
}
