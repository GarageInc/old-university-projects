using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace prim01
{
    public partial class Form1 : Form
    {
        int timeLeft = 120;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timeLabel.Text = "TIMER";
            button1.Enabled = false;//Запускаем таймер
            timer1.Start();   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
                String w1, w2, w3;
                String s1, s2, s3;
                char c1,c2,c3;
                //
                w1 = "#010" + '\r';
                serialPort1.Write(w1);
                s1 = "";
                while ((c1 = (char)serialPort1.ReadChar()) != '\r')
                {
                    if (c1 == '.')
                        s1 += ",";
                    else
                        if (c1 >= '1' && c1 <= '9')
                            s1 += c1;
                }
                //
                w2 = "#011" + '\r';
                serialPort1.Write(w2);
                s2 = "";
                while ((c2 = (char)serialPort1.ReadChar()) != '\r')
                {
                    if (c2 == '.')
                        s2 += ",";
                    else
                        if (c2 >= '1' && c2 <= '9')
                            s2 += c2;
                }
                //
                w3 = "#012" + '\r';
                serialPort1.Write(w3);
                s3 = "";
                while ((c3 = (char)serialPort1.ReadChar()) != '\r')
                {
                    if (c3 == '.')
                        s3 += ",";
                    else
                        if (c3 >= '1' && c3 <= '9')
                            s3 += c3;
                }

                label1.Text = s1;
                    label12.Text=(double.Parse(s1)+double.Parse(textBox2.Text.ToString())*double.Parse(textBox1.Text.ToString())).ToString();
                label2.Text = s2;
                    label22.Text = (double.Parse(s2) + double.Parse(textBox3.Text.ToString()) * double.Parse(textBox1.Text.ToString())).ToString();
                label3.Text = s3;
                    label32.Text = (double.Parse(s3) + double.Parse(textBox4.Text.ToString()) * double.Parse(textBox1.Text.ToString())).ToString();
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                button1.Enabled = true;
            }
        }


    }
}