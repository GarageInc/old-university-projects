using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Калькулятор
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Status = status.нетСтатуса;
            
        }
        string buffer;//для операции +-*/ - сохраняет предыдущее введенное значение
        status Status;//текушее состояние
        
        

        // Кнопка "=" - считает результат по тому статусу, который выполняется
        private void button16_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // если текущий статус деление
                if (Status == status.деление)
                {
                    Status = status.нетСтатуса;
                    double buff2 = double.Parse(окноВывода.Text);
		            if(buff2==0)
                    {
                        MessageBox.Show("Деление на ноль!");
                        return;
                    }

                    окноВывода.Text = (double.Parse(buffer) / buff2).ToString();
                } 
                // если текущий статус умножение
                if (Status == status.умножение)
                {
                    Status = status.нетСтатуса;
                    double buff2 = double.Parse(окноВывода.Text);
                    окноВывода.Text = (double.Parse(buffer) * buff2).ToString();
                }
                // если текущий статус вычитание
                if (Status == status.вычитание)
                {
                    Status = status.нетСтатуса;
                    double buff2 = double.Parse(окноВывода.Text);                    
                    окноВывода.Text = (double.Parse(buffer) - buff2).ToString();
                } 
                // если текущий статус добавление
                if (Status == status.сложение)
                {
                    Status = status.нетСтатуса;
                    double buff2 = double.Parse(окноВывода.Text);
                    окноВывода.Text = (double.Parse(buffer) + buff2).ToString();
                }
            }
            catch 
            {
            }
        }

        // Кнопка умножения
        private void multiplicationButton_Click(object sender, RoutedEventArgs e)
        {
            // Если у нас статуса пока нет - делаем статус "умножение" и сохраняем переменную в буффер
            if (Status == status.нетСтатуса)
            {
                Status = status.умножение;
                buffer = окноВывода.Text;
                окноВывода.Text = "";
                окноВывода.Focus();
            }
        }

        // кнопка вычитания
        private void subtractionButton_Click(object sender, RoutedEventArgs e)
        {
            if (Status == status.нетСтатуса)
            {
                Status = status.вычитание;
                buffer = окноВывода.Text;
                окноВывода.Text = "";
                окноВывода.Focus();
            }
        }

        //деление
        private void divideButton_Click(object sender, RoutedEventArgs e)
        {
            if (Status == status.нетСтатуса)
            {
                Status = status.деление;
                buffer = окноВывода.Text;
                окноВывода.Text = "";
                окноВывода.Focus();
            }
        }

        //сложение
        private void additionButton_Click(object sender, RoutedEventArgs e)
        {
            if (Status == status.нетСтатуса)
            {
                Status = status.сложение;
                buffer = окноВывода.Text;
                окноВывода.Text = "";
                окноВывода.Focus();
            }
        }

        // Кнопка очистки
        private void ClearAllButton_Click(object sender, RoutedEventArgs e)//reset all
        {
            окноВывода.Clear();
            Status = status.нетСтатуса;
            buffer = "";
        }

        // Кнопки ввода цифр и запятой
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "1";
            окноВывода.Focus();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "2";
            окноВывода.Focus();
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "3";
            окноВывода.Focus();
        }
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "4";
            окноВывода.Focus();
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "5";
            окноВывода.Focus();
        }
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "6";
            окноВывода.Focus();
        }
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "7";
            окноВывода.Focus();
        }
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "8";
            окноВывода.Focus();
        }
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (окноВывода.Text == "0")
                окноВывода.Text = "";

            окноВывода.Text += "9";
            окноВывода.Focus();
        }
        private void button0_Click(object sender, RoutedEventArgs e)
        {
            // Если там ещё не введена дробная часть, то можем написать ноль
            if(!(окноВывода.Text.Contains("0,") || окноВывода.Text.Contains("0.")))
                окноВывода.Text += "0";
            окноВывода.Focus();
        }

        // Обратавается ввод с клавиатуры
        private void textBox1_PrewievTextInput(object sender, TextCompositionEventArgs e)
        {
            // Если введено не число или не запятая - то не происходит ввода
            if (!Char.IsDigit(e.Text,0))
            {
                // Если введена запятая
                if((e.Text.ToString()==","))
                {
                    // Если другой запятой нет
                    if(окноВывода.Text.Contains(','))
                    {
                        e.Handled = true;
                    }
                }
                else
                    e.Handled = true;
            }
            else
            {
                // Если введено число и это число ноль, но оно уже есть в окне - то не вводим
                if(e.Text=="0" && окноВывода.Text=="0")
                {
                    e.Handled = true;
                }
            }
        }


        // Автопрокрутка - чтобы курсор всегда справа был
        private void окноВывода_TextChanged(object sender, TextChangedEventArgs e)
        {
            окноВывода.SelectionStart = окноВывода.Text.Length;
            окноВывода.ScrollToEnd();// ScrollToCaret();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Если там ещё нет запятой - то мы её ввести можем.
            if (!окноВывода.Text.Contains(','))
                окноВывода.Text += ",";
            окноВывода.Focus();
        }
    }
    enum status// структура - текущее состояние
    {
        вычитание,
        сложение,
        умножение,
        деление,
        нетСтатуса// никакого статус(никакая операция не выполняется)
    }
}
