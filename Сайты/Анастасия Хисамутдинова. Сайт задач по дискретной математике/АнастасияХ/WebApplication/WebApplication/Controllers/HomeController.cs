using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.WebPages;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Возвращает страничку тренажера
        public ActionResult Trenager()
        {
            ViewBag.Message = "Тренажер";

            return View();
        }
        
        // Генерируется задача в зависимости от присланного номера
        public JsonResult GetRequest(int id)
        {
            string result = "";
            switch (id)
            {
                case 0:
                    {
                        result = GenerateString01();
                        break;
                    }
                case 1:
                    {
                        result = GenerateString02(1);
                        break;
                    }
                case 2:
                    {
                        result = GenerateString02(2);
                        break;
                    }
                case 3:
                    {
                        result = GenerateString03();
                        break;
                    }
                case 4:
                    {
                        result = GenerateString04();
                        break;
                    }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Функция, которая генерирует случайную последовательность из нулей и единиц
        public static string GenerateString01()
        {
            Random r = new Random();
            List<string> res=new List<string>();

            // Количество последовательностей
            var count = r.Next(3, 14);

            // Генерируем последовательности
            int i = 0;
            for (i = 0; i < count; i++)
            {
                string s = "";
                var length = r.Next(1, 10);
                for (int j = 0; j < length; j++)
                {
                    s += r.Next(1, 100)%2;
                }
                res.Add(s);
            }

            string result = "";
            // Теперь склеиваем их в результат - кодовая строка типа 00110 000 1110 11 0 00101 0....
            for (i = 0; i < count - 1; i++)
            {
                result += res[i] + " ";
            }

            result += res[i];

            return result;
        }

        // Расшифровать, декодируется ли однозначно данная последовательность заданным кодом
        public static string GenerateString02(int level)
        {
            Random r = new Random();
            List<string> res = new List<string>();

            // Количество последовательностей
            int count;
            if (level == 1)
                count = r.Next(2, 6);
            else
                count = r.Next(6, 10);

            // Генерируем последовательности
            int i = 0;
            for (i = 0; i < count; i++)
            {
                int length;

                if (level == 1)
                    length = r.Next(2, 4);
                else
                    length = r.Next(5, 8);

                res.Add(length.ToString());
            }

            string result = "L={";
            // Теперь склеиваем их в результат - кодовая строка типа 00110 000 1110 11 0 00101 0....
            for (i = 0; i < count - 1; i++)
            {
                result += res[i] + ", ";
            }

            result += res[i];

            return result+"}";
        }

        // Функция, которая генерирует декодируюмую строку и код для неё
        public static string GenerateString03()
        {
            Random r = new Random();
            List<string> res = new List<string>();

            // Количество последовательностей
            var count = r.Next(3, 14);

            // Генерируем последовательности
            int i = 0;
            for (i = 0; i < count; i++)
            {
                string s = "";
                var length = r.Next(1, 10);
                for (int j = 0; j < length; j++)
                {
                    s += r.Next(1, 100) % 2;
                }
                res.Add(s);
            }

            string result = "";

            // Теперь склеиваем их в результат - кодовая строка типа 00110 000 1110 11 0 00101 0....
            for (i = 0; i < count - 1; i++)
            {
                result += res[i] + " ";
            }

            result += res[i];
            result += " / ";

            // Теперь сгенерируем код
            count = r.Next(3, 10);
            for (i=0; i< count; i++)
            {
                result += res[r.Next(0, res.Count)];
            }

            return result;
        }

        // Функция, которая генерирует последовательность цифр от 0 до 9
        public static string GenerateString04()
        {
            Random r = new Random();
            List<string> res = new List<string>();

            // Количество цифр
            var count = r.Next(3, 14);

            // Генерируем последовательности
            int i = 0;
            for (i = 0; i < count; i++)
            {
                var length = r.Next(0, 10).ToString();
                res.Add(length);
            }

            string result = "";

            // Теперь склеиваем их в результат - кодовая строка типа 00110 000 1110 11 0 00101 0....
            for (i = 0; i < count; i++)
            {
                result += res[i];
            }
            
            return result;
        }

        // Проверка правильности строки
        public static string CheckString01(string reply, string generated)
        {
            // Проверим - нет ли в строке суффиксного/префиксного кода?
            // Если есть - вернём ошибку
            reply = reply.ToLowerInvariant();

            if (reply == "да" || reply == "нет")
            {
                bool yes = true;
                string error = "";
                List<string> res = generated.Split(' ').ToList();
                for (int i = 0; i < res.Count; i++)
                {
                    for (int j = 0; j < res.Count; j++)
                    {
                        if (j != i)
                        {
                            // Если какая-то строка является окончанием другой - это не хорошо
                            if (res[i].EndsWith(res[j]))
                            {
                                error = "[" + res[i] + "]" + " & " + "[" + res[j] + "]";
                                yes = false;
                                break;
                            }
                        }
                    }
                    if (!yes)
                        break;
                }

                if (reply == "да" && yes)
                    return "Абсолютно верное решение! Строка однозначно декодируется";
                if (reply == "нет" && !yes)
                    return "Абсолютно верное решение! Строка неоднозначно декодируется";
                if (reply == "да" && !yes)
                    return "Нет, строка неоднозначно декодируема. Не удовлетворяют суффиксному коду: " + error;
                if (reply == "нет" && yes)
                    return "Нет, строка однозначно декодируема!";
            }

            return "Ваш ответ не подходит под условие";
        }

        // Проверка правильности строки
        public static string CheckString02(string reply, string generated)
        {
            try
            {
                bool yes = true;
                string error = "";
                // Если есть - вернём ошибку
                var res = reply.Split(' ').ToList();
                for (int i = 0; i < res.Count && yes; i++)
                {
                    for (int j = 0; j < res.Count && yes; j++)
                    {
                        if (j != i)
                        {
                            // Если какая-то строка является окончанием другой - это не хорошо
                            if (res[i].EndsWith(res[j]))
                            {
                                error = "[" + res[i] + "]" + " & " + "[" + res[j] + "]";
                                yes = false;
                            }
                        }
                    }
                }

                if (yes)
                    return "Абсолютно верное решение! Строка однозначно декодируется";

                return "Нет, строка неоднозначно декодируема. Не удовлетворяют свойству Фано: " + error;

            }
            catch(Exception e)
            {
                return "Ошибка ввода! "+e.Message;
            }
        }

        // Проверка правильности строки для кода Хаффмана
        public static string CheckString04(string reply, string generated)
        {
            try
            {
                bool yes = true;

                string input = generated;

                if (input.CompareTo("") == 0)
                {
                    return "Вы не ввели текст!";
                }

                HuffmanTree huffmanTree = new HuffmanTree();

                // Строим дерево Хаффмана по весам слов
                huffmanTree.Build(input);

                // Кодируем
                BitArray encoded = (BitArray)huffmanTree.Encode(input);

                if (huffmanTree.str != reply)
                    yes = false;


                if (yes)
                    return "Абсолютно верное решение! ";

                return "Это не оптимальный код Хаффмана. Вот он: " + huffmanTree.str;

            }
            catch (Exception e)
            {
                return "Ошибка ввода! " + e.Message;
            }
        }

        // Проверяется задача
        public JsonResult CheckRequest(int id, string reply, string generated)
        {
            string result = "yes!";
            switch (id)
            {
                case 0:
                    {
                        result = CheckString01(reply,generated);
                        break;
                    }
                case 1:
                    {
                        result = CheckString02(reply, generated);
                        break;
                    }
                case 2:
                    {
                        result = CheckString02(reply, generated);
                        break;
                    }
                case 3:
                    {
                        // Получим только первую часть(т.к. можно просто проверить на правило суффиксного/префиксного кода Фано, независимо от того, что сгенерировано справа)
                        var sub = generated.Split(new string[] { " / " }, StringSplitOptions.None);
                        result = CheckString01(reply, sub[0]);
                        break;
                    }
                case 4:
                    {
                        result = CheckString04(reply, generated);
                        break;
                    }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}