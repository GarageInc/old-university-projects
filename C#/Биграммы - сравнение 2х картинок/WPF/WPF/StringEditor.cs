using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WPF
{
    class StringEditor
    {
        private int k;
        private char[] a = new char[2];
        //private string[] mas=new string[100];
        private bool ВСЕ;
        private ArrayList arr = new ArrayList(2);

        /// <summary>
        /// Полученное после методов расщепления текста.
        /// </summary>
        public ArrayList Массив
        {
            get { return arr; }
        }
        
        /// <summary>
        /// Расщепление текста с разделителями ";", на цифры.
        /// </summary>
        public void Расщепление_строковых_с_разделителем_точка_запятая(string текст)
        {
            arr.Clear();
            k = 0;
            ВСЕ = true;
            while(ВСЕ)
            {                
                arr.Add(Расщепитель(текст));
                if (k == текст.Length) { ВСЕ = false; }
            }
        }
        
        /// <summary>
        /// Расщепления цифры из текста от k до ";" или до последнего символа.
        /// </summary>
        private string Расщепитель(string текст)
        {
            int l = 0;
            string число = "";
            int количество_букв = текст.Length;
            while (число == "")
            {
                for (l = k; l < количество_букв; l++)
                {
                    текст.CopyTo(l, a, 0, 1);
                    if (a[0] != Convert.ToChar(";") && (a[0] == Convert.ToChar("0") || a[0] == Convert.ToChar("1") || a[0] == Convert.ToChar("2") || a[0] == Convert.ToChar("3") || a[0] == Convert.ToChar("4") || a[0] == Convert.ToChar("5") || a[0] == Convert.ToChar("6") || a[0] == Convert.ToChar("7") || a[0] == Convert.ToChar("8") || a[0] == Convert.ToChar("9")))
                    {
                        число += Convert.ToString(a[0]);
                        k = l;
                    }
                    else
                    {
                        
                        l = количество_букв;                        
                    }
                    k++;
                }
            }

            return число;
        }
        
        /// <summary>
        /// Расщепление текста без раздилителей на символы.
        /// </summary>
        /// <param name="текст">переменная string</param>
        public void Расщепление_строковых_без_разделителей(string текст) 
        {
            int количество_букв = текст.Length;
            arr.Clear();
            for (int i = 0; i < количество_букв; i++)
            {
                текст.CopyTo(i, a, 0, 1);
                arr.Add(a[0]);
            }            
        }
    }
}
