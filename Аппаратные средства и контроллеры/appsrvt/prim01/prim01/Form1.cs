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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s1="#010"+'\r';
            serialPort1.Write(s1);
            s2="";
            while ((c0 =(char)serialPort1.ReadChar()) != '\r')
            {
                s2 += c0;
            }
            label1.Text = s2;
        }
    }
}