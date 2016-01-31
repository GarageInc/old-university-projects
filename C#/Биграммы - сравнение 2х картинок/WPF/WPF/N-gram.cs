using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WPF
{
    class N_gramm
    {
        private int kABC; //Количество_символов_в_алфавите
        private char[] a = new char[2];
        private int[,] tabl;

        /// <summary>
        /// Передает массив со значениями, подсчитанного методом "Биграмм".
        /// </summary>
        public int[,] Значения_биграм
        {
            get { return tabl; }
        }
        


        /// <summary>
        /// Подсчет Би-грамм.
        /// </summary>
        /// <param name="текст">Текст для анализа</param>
        /// <param name="Количество_символов">Количество символов требуемое для анализа текста</param>
        /// <param name="Алфавит">Набор букв, по которому проводить анализ</param>
        public void Биграмм(string текст, int Количество_символов, ArrayList Алфавит)
        {
            kABC = Алфавит.Count;
            tabl = new int[kABC, kABC];
            
            for (int z = 0; z < Количество_символов - 1; z++)
            {
                текст.CopyTo(z, a, 0, 1);
                текст.CopyTo(z + 1, a, 1, 1);
                int j=0;
                foreach (char Первая_буква in Алфавит)
                {
                    if (a[0] == Первая_буква)// || a[0] == Первая_буква.ToUpper())
                    {
                        int i = 0;
                        foreach (char Вторая_буква in Алфавит)
                        {
                            if (a[1] == Вторая_буква) //|| Convert.ToString(a[1]) == Вторая_буква.ToUpper())
                            {
                                tabl[j, i]++;
                                i = kABC;
                            }
                            i++;
                        }
                        j = kABC;
                    }
                    j++;
                }
            }
            
            
        }

        

        
    }
}
