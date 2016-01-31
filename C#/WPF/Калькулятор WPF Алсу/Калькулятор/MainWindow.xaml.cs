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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CurrentState = state.nonstate;
            
        }
        string buff;//для операции +-*/ - сохраняет предыдущее введенное значение
        state CurrentState;//текушее состояние
        
        enum state// структура - текущее состояние
        {
            substraction,// вычитание
            addiction,// добавление
            multiplication,// умножение
            divide,// деление
            nonstate// никакого статус(никакая операция не выполняется)
        }

        // Кнопка "=" - считает результат по тому статусу, который выполняется
        private void button16_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // если текущий статус деление
if (Status == status.divide)
{
Status = status.nonstate;
double buff2 = double.Parse(окноВывода.Text);
if(buff2==0)
{
MessageBox.Show("Осторожно, деление на ноль");
return;
}
окноВывода.Text = (double.Parse(buffer) / buff2).ToString();
}                // если текущий статус умножение
                if (CurrentState == state.multiplication)
                {
                    CurrentState = state.nonstate;
                    double buff2 = double.Parse(textBox1.Text);
                    textBox1.Text = (double.Parse(buff) * buff2).ToString();
                }                // если текущий статус деление
                if (CurrentState == state.substraction)
                {
                    CurrentState = state.nonstate;
                    double buff2 = double.Parse(textBox1.Text);
                    textBox1.Text = (double.Parse(buff) - buff2).ToString();
                }                // если текущий статус добавление
                if (CurrentState == state.addiction)
                {
                    CurrentState = state.nonstate;
                    double buff2 = double.Parse(textBox1.Text);
                    textBox1.Text = (double.Parse(buff) + buff2).ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введены некорректные данные");
            }
        }

        // Кнопка умножения
        private void multiplicationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentState == state.nonstate)
            {
                CurrentState = state.multiplication;
                buff = textBox1.Text;
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        // кнопка вычитания
        private void subtractionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentState == state.nonstate)
            {
                CurrentState = state.substraction;
                buff = textBox1.Text;
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        //деление
        private void divideButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentState == state.nonstate)
            {
                CurrentState = state.divide;
                buff = textBox1.Text;
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        //сложение
        private void additionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentState == state.nonstate)
            {
                CurrentState = state.addiction;
                buff = textBox1.Text;
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        // Кнопка очистки
        private void ClearAllButton_Click(object sender, RoutedEventArgs e)//reset all
        {
            textBox1.Clear();
            CurrentState = state.nonstate;
            buff = "";
        }

        // Кнопка стирания введенного символа
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            catch (Exception) { }
        }


        // Кнопки ввода цифр и запятой
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "1";
            textBox1.Focus();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "2";
            textBox1.Focus();
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "3";
            textBox1.Focus();
        }
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "4";
            textBox1.Focus();
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "5";
            textBox1.Focus();
        }
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "6";
            textBox1.Focus();
        }
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "7";
            textBox1.Focus();
        }
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "8";
            textBox1.Focus();
        }
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "0")
                textBox1.Text = "";

            textBox1.Text += "9";
            textBox1.Focus();
        }
        private void button0_Click(object sender, RoutedEventArgs e)
        {
            // Если там ещё не введена дробная часть, то можем написать ноль
            if(!(textBox1.Text.Contains("0,") || textBox1.Text.Contains("0.")))
                textBox1.Text += "0";
            textBox1.Focus();
        }
        private void buttonOEMComma_Click(object sender, RoutedEventArgs e)
        {
            // Если там ещё нет запятой - то мы её ввести можем.
            if (!textBox1.Text.Contains(','))
                textBox1.Text += ",";
            textBox1.Focus();
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
                    if(textBox1.Text.Contains(','))
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
                if(e.Text=="0" && textBox1.Text=="0")
                {
                    e.Handled = true;
                }
            }
        }


    }
}
