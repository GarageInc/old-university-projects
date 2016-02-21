using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Timers;


namespace WinFormCharpWebCam
{
    public partial class mainWinForm : Form
    {
        public mainWinForm()
        {
            InitializeComponent();
        }

        // Создаем объект - камера
        WebCam webcam;

        private void mainWinForm_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
        }

        private void bntStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, EventArgs e)
        {
            webcam.Continue();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
            imgCapture1.Image = imgVideo.Image;//Фиксируем картинку     
        }


        private void bntSave_Click(object sender, EventArgs e)
        {
            //Отправляем зафиксированную картинку в функцию её сохранения
            Helper.SaveImageCapture(imgCapture1.Image);
        }

        private void bntVideoFormat_Click(object sender, EventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void bntVideoSource_Click(object sender, EventArgs e)
        {
            webcam.AdvanceSetting();
        }

        //Инициализируем переменную, которую будет использовать
        //таймер как счетчик
        int timeLeft = 6;

        //Нужно зафиксировать 5 камшотов и сохранить их в файл
        private void bntCamTimer_Click(object sender, EventArgs e)
        {
            bntCamTimer.Enabled = false;//Запускаем таймер
            timer1.Start();           
        }

        //Переходы между картинками. Для выбора самой подходящей и её сохранения.
        private void imgCapture2_Click(object sender, EventArgs e)
        {
            Image newImage = imgCapture2.Image;
            imgCapture1.Image = imgCapture2.Image;
            imgCapture2.Image = newImage;
        }

        private void imgCapture3_Click(object sender, EventArgs e)
        {
            Image newImage = imgCapture3.Image;
            imgCapture1.Image = imgCapture3.Image;
            imgCapture3.Image = newImage;
        }

        private void imgCapture4_Click(object sender, EventArgs e)
        {
            Image newImage = imgCapture4.Image;
            imgCapture1.Image = imgCapture4.Image;
            imgCapture4.Image = newImage;
        }

        private void imgCapture5_Click(object sender, EventArgs e)
        {
            Image newImage = imgCapture5.Image;
            imgCapture1.Image = imgCapture5.Image;
            imgCapture5.Image = newImage;
        }

        private void SavePhotos_Click(object sender, EventArgs e)
        {           
            Helper.SaveImageCapture(imgCapture1.Image, 1);//Сохраняем фото
            Helper.SaveImageCapture(imgCapture2.Image, 2);//Сохраняем    
            Helper.SaveImageCapture(imgCapture3.Image, 3);//Сохраняем 
            Helper.SaveImageCapture(imgCapture4.Image, 4);//Сохраняем 
            Helper.SaveImageCapture(imgCapture5.Image, 5);//Сохраняем 
        }

        public void timer1_Tick_1(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " секунд";
                switch(timeLeft)
                {
                    case 5: 
                        imgCapture1.Image = imgVideo.Image; break;
                    case 4:
                        imgCapture2.Image = imgVideo.Image; break;//Фиксируем картинку 
                    case 3:
                        imgCapture3.Image = imgVideo.Image; break;//Фиксируем картинку
                    case 2:
                        imgCapture4.Image = imgVideo.Image; break;//Фиксируем картинку
                    case 1:
                        imgCapture5.Image = imgVideo.Image; break;//Фиксируем картинку
                }
               
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Отсчет закончен!";
                bntCamTimer.Enabled = true;
            }
        }      
    }
}
