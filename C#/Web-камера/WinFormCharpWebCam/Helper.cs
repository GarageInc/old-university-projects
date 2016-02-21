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
        //Для сохранения ряда картинок
        public static void SaveImageCapture(System.Drawing.Image image, int i=0)
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
                MessageBox.Show("Были проблемы с сохранением файла." +
                    "Проверьте права доступа.");
            }
        }
    }
}
