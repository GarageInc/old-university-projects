using System.Collections.Generic;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            int MaxExecution = 20;
            int ExecutionCount = 0;
            ErrorMsg.Text = string.Empty;

            while (RunOnce() != 0 && (ExecutionCount < MaxExecution))
            {
                ExecutionCount++;
            }
            if (ExecutionCount == MaxExecution)
            {
                ErrorMsg.Text = "Привышено  время выполнения. \n" + ErrorMsg.Text;
            }
            else
            {
                ErrorMsg.Text = "Выполнение завершено.\n" + ErrorMsg.Text;
            }
        }


        /*Собственно самая главная функция тут. Здесь просиходит магия разделения правил не отдельные 
        правила. И здесь же эти правила применяются. 
            */
        private int RunOnce()
        {
            var RulesMaxQty = 250; // количесвто циклов выполнения
            var Executed = 0;      // ноль, если правило не пременялось
            var Code = CodeArea.Text.Split('\n'); // вытаскиваем из входного поля все правила и разбиваем по отдельности
            var Word = InputWord.Text;
            var Rules = new List<string>();

            var c = string.Empty;
            var Rule = string.Empty;

            //Здесь продолжаем обработывать правила. Смотрим. не пропущены ли знаки правила. Убираем лишние символы.
            for (int i = 0; i < Code.Length & i < RulesMaxQty; i++) 
            {
                var tmp = Code[i];
                Rule = string.Empty;
                for (int j = 0; j < tmp.Length; j++)
                {
                    c = tmp.Substring(j, 1);
                    if (!c.Equals(" ") && !c.Equals("\r"))
                    {
                        Rule += c;
                    }
                }

                if (0 < Rule.Length)
                {
                    if ((Rule.Contains("->")) && (Rule.Contains("=>")))
                    {
                        ErrorMsg.Text = i + 1 + ": пропущен знак правила.\n" + ErrorMsg.Text;
                    }
                    else
                    {
                        Rules.Add(Rule);
                    }
                }
                else
                {
                    ErrorMsg.Text = i + 1 + ": пустая строка.\n" + ErrorMsg.Text;
                }
            }

            var k = 0;
            var TotalRules = Rules.Count <= 20 ? Rules.Count : 20; // если количесвто правил больше 20, то оставляем только 20 из них
            var TerminalRule = false;

            Rule = string.Empty;
            //Тут правила как раз приминяются. Пока не применим одно из правил или количесвто циклов не привысит допустимо возможное или не найдем терминальное правило
            while (Executed == 0 && k < TotalRules && !TerminalRule)
            {
                var alpha = string.Empty;
                var betha = string.Empty;
                TerminalRule = false;
                Rule = Rules[k]; //берем правило из списка

                //разделяем его на две части: первая часть это то, что мы должны менять, а второе на что менять
                int pos;
                if (Rule.Contains("->"))
                {
                    pos = Rule.IndexOf("->");
                }
                else
                {
                    pos = Rule.IndexOf("=>");
                }
                alpha = Rule.Substring(0, pos);
                betha = Rule.Substring(pos + 2, Rule.Length - pos - 2);

                if (!betha.Equals(".")) // не терминальная ли подстановка? 
                {
                    if (0 <= Word.IndexOf(alpha))
                    {
                        if (betha.Equals("")) // если правая часть пустая, то это значит, что мы просто это слово удаляем из рабочего слова
                        {
                            InputWord.Text = Word.Replace(alpha, null);
                        }
                        if (alpha.Equals("")) // если левая часть пустая, то тогда мы ставим слово-замену в начала рабочего слова
                        {
                            InputWord.Text = betha + Word;
                        }
                        else // иначе удаляем вхождение слова образца на слов-замену из рабочего слова
                        {
                            InputWord.Text = Word.Replace(alpha, betha);
                        }
                        Executed = k + 1; // мы сообщаем, что какое то правило применилось 
                    }
                }
                else //при нахождении терминальной подстановки, удаляем слова образец из рабочего слова и выхоит из функции
                {
                    if (0 <= Word.IndexOfAny(alpha.ToCharArray()))
                    {
                        InputWord.Text = Word.Replace(alpha, null);
                        TerminalRule = true;
                        Executed = k + 1;
                    }
                }

                if (Executed != 0)
                {
                    HighLightCode(Rules, k); //Подкрашиваем приминевшуюся правило
                }
                k++; // переход на след правило
            }

            if (Executed != 0 && !TerminalRule) //Если какое либо правило применилось или найдена терминальная подстановка, то выходим из функции
            {
                return Executed;
            }
            return 0;
        }

        private void HighLightCode(List<string> Code, int Line)
        {
            var Alpha = string.Empty;
            var Betha = string.Empty;
            var Arrow = string.Empty;
            var Dummy = string.Empty;
            Dummy = "<!DOCTYPE html ><html><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'><head></head><body><table width=146 cellspacing=0>";
            for (int i = 0; i < Code.Count; ++i)
            {
                var Rule = Code[i];
                var sk = ("->").ToCharArray();
                var pos = Rule.IndexOfAny(sk);
                if (pos < 0) pos = Rule.IndexOf("=>");
                if (0 <= pos)
                {
                    try
                    {
                        Alpha = Rule.Substring(0, pos);
                    }
                    catch (System.Exception)
                    {
                        Alpha = string.Empty;
                    }
                    try
                    {
                        Arrow = Rule.Substring(pos, 2);
                    }
                    catch (System.Exception)
                    {
                        Alpha = string.Empty;
                    }
                    if (Arrow == "->")
                    {
                        Arrow = "&#8594";
                    }
                    else
                    {
                        Arrow = "&#8614;";
                    }
                    try
                    {
                        Betha = Rule.Substring(pos + 2, Rule.Length - pos - 2);
                    }
                    catch (System.Exception)
                    {
                        Betha = string.Empty;
                    }
                }
                Dummy += "<tr valign=top";
                if (i == Line)
                {
                    Dummy += " bgcolor=#e0f0e0";
                }
                Dummy += "><td align=right width=25>" + (i + 1) + ".&nbsp;</td><td align=right>" + Alpha + "</td><td align=center style=\"font-face:times new roman,serif;\">" + Arrow + "</td><td align=left>" + Betha + "&nbsp;&nbsp;</td></tr>";
            }
            Dummy += "</table></body></html>";
            ParceCode.NavigateToString(Dummy);
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            var r = RunOnce(); 
            if (r != 0)
            {
                ErrorMsg.Text = "Правило " + r + " применимо.\n" + ErrorMsg.Text;
            }
            else
            {
                ErrorMsg.Text = "Ни одно правило не применимо, либо выполнено терминальное правило.\n";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            InputWord.Text = "abbbcaa";
            CodeArea.Text = "aa->a\nbb->b\ncc->c";
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            InputWord.Text = "xx";
            CodeArea.Text = "*x->xx*\n *=>\n ->*";
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            InputWord.Text = "cbabca";
            CodeArea.Text = "ba->ab\nca->ac\ncb->bc";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            InputWord.Text = "";
            CodeArea.Text = "";
            ParceCode.NavigateToString("<!DOCTYPE html ><html><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'><head></head><body></body></html>");
            ErrorMsg.Text = "";
        }
    }
}
