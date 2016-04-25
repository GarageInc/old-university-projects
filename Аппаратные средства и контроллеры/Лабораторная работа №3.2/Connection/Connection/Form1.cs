using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Connection
{
    public partial class Form1 : Form
    {
        public bool b=true;
        public Thread myThread;
        public String s1;
        public String s2;
        public char c0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Open();
                myThread = new Thread(this.ThreadProc);
                myThread.Start();
            }
            catch
            {
                MessageBox.Show("COM-порт не подключен! Подключите порт и перезапустите программу!");
                b = false;
            }
        }

        public void ThreadProc()
        {
            while (true)
            {
                s1 = "";
                while ((c0 = (char)serialPort1.ReadChar()) != '\r')
                {
                    s1 += c0;
                }
                textBox1.Text = s1;

                //Сюда добавляем - изменение цвета
                ChangePicture(s1);

                s2 = "#010" + '\r';
                //s2 = s1 + '\r';

                //serialPort1.Write(s2);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (b)
            {
                myThread.Abort();
                serialPort1.Close();
            }
        }

        private void ChangePicture(string s)
        {
            switch (s)
            {
                case "1":
                    {
                        if (pictureBox1.BackColor == Color.Red)
                            pictureBox1.BackColor = Color.Green;
                        else
                            pictureBox1.BackColor = Color.Red;
                    }
                    break;
                case "2":
                    {
                        if (pictureBox2.BackColor == Color.Red)
                            pictureBox2.BackColor = Color.Green;
                        else
                            pictureBox2.BackColor = Color.Red;
                    }
                    break;
                case "3":
                    {
                        if (pictureBox3.BackColor == Color.Red)
                            pictureBox3.BackColor = Color.Green;
                        else
                            pictureBox3.BackColor = Color.Red;
                    }
                    break;
                case "4":
                    {
                        if (pictureBox4.BackColor == Color.Red)
                            pictureBox4.BackColor = Color.Green;
                        else
                            pictureBox4.BackColor = Color.Red;
                    }
                    break;
                case "5":
                    {
                        if (pictureBox5.BackColor == Color.Red)
                            pictureBox5.BackColor = Color.Green;
                        else
                            pictureBox5.BackColor = Color.Red;
                    }
                    break;
                case "6":
                    {
                        if (pictureBox6.BackColor == Color.Red)
                            pictureBox6.BackColor = Color.Green;
                        else
                            pictureBox6.BackColor = Color.Red;
                    }
                    break;
                case "7":
                    {
                        if (pictureBox7.BackColor == Color.Red)
                            pictureBox7.BackColor = Color.Green;
                        else
                            pictureBox7.BackColor = Color.Red;
                    }
                    break;
                case "8":
                    {
                        if (pictureBox8.BackColor == Color.Red)
                            pictureBox8.BackColor = Color.Green;
                        else
                            pictureBox8.BackColor = Color.Red;
                    }
                    break;
                case "9":
                    {
                        if (pictureBox9.BackColor == Color.Red)
                            pictureBox9.BackColor = Color.Green;
                        else
                            pictureBox9.BackColor = Color.Red;
                    }
                    break;
                case "10":
                    {
                        if (pictureBox10.BackColor == Color.Red)
                            pictureBox10.BackColor = Color.Green;
                        else
                            pictureBox10.BackColor = Color.Red;
                    }
                    break;
                case "11":
                    {
                        if (pictureBox11.BackColor == Color.Red)
                            pictureBox11.BackColor = Color.Green;
                        else
                            pictureBox11.BackColor = Color.Red;
                    }
                    break;
                case "12":
                    {
                        if (pictureBox12.BackColor == Color.Red)
                            pictureBox12.BackColor = Color.Green;
                        else
                            pictureBox12.BackColor = Color.Red;
                    }
                    break;
                case "13":
                    {
                        if (pictureBox13.BackColor == Color.Red)
                            pictureBox13.BackColor = Color.Green;
                        else
                            pictureBox13.BackColor = Color.Red;
                    }
                    break;
                case "14":
                    {
                        if (pictureBox14.BackColor == Color.Red)
                            pictureBox14.BackColor = Color.Green;
                        else
                            pictureBox14.BackColor = Color.Red;
                    }
                    break;
                case "15":
                    {
                        if (pictureBox15.BackColor == Color.Red)
                            pictureBox15.BackColor = Color.Green;
                        else
                            pictureBox15.BackColor = Color.Red;
                    }
                    break;
                case "16":
                    {
                        if (pictureBox16.BackColor == Color.Red)
                            pictureBox16.BackColor = Color.Green;
                        else
                            pictureBox16.BackColor = Color.Red;
                    }
                    break;
                case "17":
                    {
                        if (pictureBox17.BackColor == Color.Red)
                            pictureBox17.BackColor = Color.Green;
                        else
                            pictureBox17.BackColor = Color.Red;
                    }
                    break;
                case "18":
                    {
                        if (pictureBox18.BackColor == Color.Red)
                            pictureBox18.BackColor = Color.Green;
                        else
                            pictureBox18.BackColor = Color.Red;
                    }
                    break;
                default:
                    serialPort1.Write("1234567890");
                    break;
            }
        }

      



    }
}
