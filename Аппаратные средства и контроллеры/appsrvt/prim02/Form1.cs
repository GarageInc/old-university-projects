using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace prim02
{
    public partial class Form1 : Form
    {
        public Thread myThread;
        public String s1;
        public String s2;
        public char c0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
            myThread = new Thread(this.ThreadProc);
            myThread.Start();
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
                s2 = "#010" + '\r';
                serialPort1.Write(s2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myThread.Abort();
            serialPort1.Close();
        }
    }
}