using System.Collections.Generic;
using System.Windows;

namespace MarkAlgoritm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Rule> _rules;
        private int _step;

        public MainWindow()
        {
            InitializeComponent();
            _step = 0;
        }
        
        public void PasreRules()
        {
            _rules = new List<Rule>();
            var rules = Rules.Text.Split('\n');
            for (var i = 0; i < rules.Length; i++)
            {
                rules[i] = rules[i].Trim();
                rules[i] = rules[i].Replace('\n'.ToString(), "");
                if (rules[i].Contains("->") || rules[i].Contains("=>"))
                {
                    int index;
                    if (rules[i].Contains("->"))
                    {
                        index = rules[i].IndexOf("->");
                    }
                    else
                    {
                        index = rules[i].IndexOf("=>");
                    }
                    var rule = new Rule
                    {
                        First = rules[i].Substring(0, index),
                        Second = rules[i].Substring(index + 2, rules[i].Length - 2 - index)
                    };
                    _rules.Add(rule);
                }
                else
                {
                    Message.Text = "Syntax error" + Message.Text;
                    return;
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PasreRules();

            if (Input.Text.Equals(""))
            {
                Message.Text = "Не задано входное слово!" + Message.Text;
                return;
            }

            int ruleIndex = 0;

            var result = Input.Text;
            while (ruleIndex < _rules.Count && !result.Contains(_rules[ruleIndex].First))
            {
                ruleIndex++;
            }

            if (_rules.Count > ruleIndex && result.Contains(_rules[ruleIndex].First))
            {
                if (_rules[ruleIndex].Second.Equals("."))
                {
                    result = result.Replace(_rules[ruleIndex].First, "");
                    Message.Text = "Step #" + _step + "\n" +
                               "Rule:    " + _rules[ruleIndex].First + " -> " + _rules[ruleIndex].Second + "\n" +
                               "Before: " + Input.Text + "\n" +
                               "After:   " + result + "\n\n" +
                               "Выполнено терминальное правило" +
                               Message.Text;
                    return;
                }
                result = result.Replace(_rules[ruleIndex].First, _rules[ruleIndex].Second);
                Message.Text = "Step #" + _step + "\n" +
                               "Rule:    " + _rules[ruleIndex].First + " -> " + _rules[ruleIndex].Second + "\n" +
                               "Before: " + Input.Text + "\n" +
                               "After:   " + result + "\n\n" +
                               Message.Text;
                Output.Text = result;
                Input.Text = Output.Text;
                _step++;
            }
            else
            {
                Message.Text = "Нет приминимых правил!\n\n" + Message.Text;
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            _step = 0;
            PasreRules();

            if (Input.Text.Equals(""))
            {
                Message.Text = "Не задано входное слово!" + Message.Text;
                return;
            }

            int repeatCount = 0;
            int k = 0;
            while (repeatCount != 500  && k != -1)
            {
                int ruleIndex = 0;
                k = -1;

                var result = Input.Text;
                while (ruleIndex < _rules.Count && !result.Contains(_rules[ruleIndex].First))
                {
                    ruleIndex++;
                }

                if (_rules.Count > ruleIndex && result.Contains(_rules[ruleIndex].First))
                {
                    k = ruleIndex;
                    if (_rules[ruleIndex].Second.Equals("."))
                    {
                        result = result.Replace(_rules[ruleIndex].First, "");
                        Message.Text = "Step #" + _step + "\n" +
                                   "Rule:    " + _rules[ruleIndex].First + " -> " + _rules[ruleIndex].Second + "\n" +
                                   "Before: " + Input.Text + "\n" +
                                   "After:   " + result + "\n\n" +
                                   "Выполнено терминальное правило" +
                                   Message.Text;
                        return;
                    }
                    if (_rules[ruleIndex].First.Equals(""))
                    {
                        result = _rules[ruleIndex].Second + result;
                    }
                    else
                    {
                        if (_rules[ruleIndex].Second.Equals(""))
                        {
                            result = result.Replace(_rules[ruleIndex].First, null);
                        }
                        else
                        {
                            result = result.Replace(_rules[ruleIndex].First, _rules[ruleIndex].Second);
                        }
                    }
                   
                    Message.Text = "Step #" + _step + "\n" +
                                   "Rule:    " + _rules[ruleIndex].First + " -> " + _rules[ruleIndex].Second + "\n" +
                                   "Before: " + Input.Text + "\n" +
                                   "After:   " + result + "\n\n" +
                                   Message.Text;
                    Output.Text = result;
                    Input.Text = Output.Text;
                    _step++;
                }
                else
                {
                    Message.Text = "Нет приминимых правил!\n\n" + Message.Text;
                }
                repeatCount++;
            }
        }
    }
}