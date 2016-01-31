using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;



namespace PictureToByteCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите название картинки(например example.jpg");
            string path = Console.ReadLine();
            
            Image image = Image.FromFile(path);// Считали
            MemoryStream memoryStream = new MemoryStream();// Поток для считывания
            image.Save(memoryStream, ImageFormat.Jpeg);// Сохраняем картинку из файла в поток

            // Переводим в байт код
            byte[] b = memoryStream.ToArray();

            // Выводим в строку
            string stroka = Encoding.Default.GetString(b);
            Console.WriteLine(stroka);

            // Поток для записи
            MemoryStream memoryStream1 = new MemoryStream();
            foreach (byte b1 in b) // Записываем в поток байты
                memoryStream1.WriteByte(b1);
            Image image1 = Image.FromStream(memoryStream1);// Создаем картинку
            image1.Save("newImage.jpeg", ImageFormat.Jpeg);// Сохраняем

            Console.WriteLine("Сохраненая новая картинка newImage.jpg");
            Console.ReadLine();
        }
    }
}
