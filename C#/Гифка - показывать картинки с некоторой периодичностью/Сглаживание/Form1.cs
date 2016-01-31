using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Сглаживание
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string[] Файлы;
        private void Form1_Load(object sender, EventArgs e)
        {
            Файлы = System.IO.Directory.GetFiles("D:/Картинки");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            timer1.Interval = 500;
            timer1.Start();

        }

        int g = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (g != Файлы.Length)
            {
                pictureBox1.Image = Image.FromFile(Файлы[g]);
                g += 1;
            }
            else
            {
                timer1.Stop();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "bmp files (*.bmp)|*.bmp";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(dlg.FileName);
            }

            dlg.Dispose();
            */
           
        }
        

    }
}
