using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormCharpWebCam
{
    class Helper
    {

        public static void SaveImageCapture(System.Drawing.Image image)
        {
            try
            {
                if (image != null)
                {
                    image.Save("D:\\Image.jpg");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem saving the file." +
                    "Check the file permissions.");
            }
        }
        
        //Для сохранения ряда картинок
        public static void SaveImageCapture(System.Drawing.Image image, int i)
        {
            try
            {
                if (image != null)
                {
                    image.Save("D:\\Image"+i+".jpg");
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem saving the file." +
                    "Check the file permissions.");
            }
        }
    }
}
