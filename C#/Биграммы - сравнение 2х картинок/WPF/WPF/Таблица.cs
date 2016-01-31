using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF
{
    class Таблица : MainWindow
    {
        private int i;
        private int j;
        private int z;
        private char[] a = new char[2];
        private string[] abc = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
        private Int16[,] tabl = new Int16[33, 33];

        public void Биграмм(string s)
        {
            for (z = 0; z < Convert.ToInt32(s.Length.ToString()) - 1; z++)
            {
                s.CopyTo(z, a, 0, 1);
                s.CopyTo(z + 1, a, 1, 1);

                for (j = 0; j < 33; j++)
                {
                    if (Convert.ToString(a[0]) == abc[j] || Convert.ToString(a[0]) == abc[j].ToUpper())
                    {
                        for (i = 0; i < 33; i++)
                        {
                            if (Convert.ToString(a[1]) == abc[i])
                            {
                                tabl[j, i]++;
                                i = 33;
                                //0.00000000023283064365386962890625
                                //0.000000059604644775390625

                            }
                        }
                        j = 33;
                    }
                }
            }
        }
    }
}
