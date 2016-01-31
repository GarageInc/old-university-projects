using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Калькулятор
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            CurrentState = state.nonstate;
        }
        string buff;//для операции +-*/
        state CurrentState;//текушее состояние
        string memory = "";//для операций в памяти
        string memory2 = "";//tool strip memory operations
        enum state// структура - текущее состояние
        {
            substraction,// вычитание
            addiction,// добавление
            multiplication,// умножение
            divide,// деление
            nonstate// никакого статус(никакая операция не выполняется)
        }
        //запрет на ввод букв(только цифры и знаки) + только один раз запытая вводится
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!(Char.IsDigit(e.KeyChar)))
                {
                    if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
                    {
                        if (e.KeyChar == (char)Keys.Back)
                        {
                            
                        }
                        else 
                            if (textBox1.Text.Contains(",") || textBox1.Text.Contains("."))
                            {
                                    e.Handled = true;
                            }
                    }
                    else
                    {
                        e.Handled = true;
                    }
            }
        }

        // Кнопка "=" - считает результат по тому статусу, который выполняется
        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                // если текущий статус деление
                if (CurrentState == state.divide)
                {
                    CurrentState = state.nonstate;
                    double buff2 = double.Parse(textBox1.Text);
                    textBox1.Text = (double.Parse(buff) / buff2).ToString();
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
            catch(Exception)
            {
                MessageBox.Show("Введены некорректные данные");
            }
        }

        // Кнопка умножения
        private void multiplicationButton_Click(object sender, EventArgs e)
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
        private void subtractionButton_Click(object sender, EventArgs e)
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
        private void divideButton_Click(object sender, EventArgs e)
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
        private void additionButton_Click(object sender, EventArgs e)
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
        private void ClearAllButton_Click(object sender, EventArgs e)//reset all
        {
            textBox1.Clear();
            CurrentState = state.nonstate;
            buff = "";
        }
        // Кнопка стирания введенного символа
        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            catch (Exception) { }
        }

        
        // Кнопки ввода цифр и запятой
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
            textBox1.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
            textBox1.Focus();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
            textBox1.Focus();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
            textBox1.Focus();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
            textBox1.Focus();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
            textBox1.Focus();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
            textBox1.Focus();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
            textBox1.Focus();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
            textBox1.Focus();
        }
        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
            textBox1.Focus();
        }
        private void buttonOEMComma_Click(object sender, EventArgs e)
        {
            textBox1.Text += ",";
            textBox1.Focus();
        }



        
        

    }
}
