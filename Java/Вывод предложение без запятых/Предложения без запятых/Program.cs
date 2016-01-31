using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Предложения_без_запятых
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("text.txt",Encoding.Default);

            string text = sr.ReadToEnd();
            string new_text = "";
            int i = 0;// Счетчик, который проходит по тексту
            for (; i < text.Length; i++)
            {
                string buffer="";
                // Считываем каждое предложение - до точки включительно. 
                // При этом, если в предложении не попадалось нам запятых - выводим
                while (text[i] != '.')
                {
                    if (text[i] == ',')
                    {
                        buffer = "";
                        // Если нашлась запятая - то обнуляем буфер и просто считываем предложение до конца
                        while (text[i] != '.')
                        {
                            i++;
                        }
                        break;
                    }
                    else
                    {
                        buffer = buffer + text[i].ToString();
                    }
                    i++;
                }
                if (buffer != "")
                {
                    new_text = new_text + buffer + ".";
                }
            }

            Console.WriteLine(new_text);// Выводим наш новый текст
            
            sr.Close();
            Console.ReadLine();
        }
    }
}
