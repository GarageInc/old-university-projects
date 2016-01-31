using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TripleDES_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "qweqweqweqweqweqweqweqweqwe";
        }

        private void ButtonToEncode(object sender, EventArgs e)
        {
            DateTime d1 = DateTime.Now;

            string key = textBox1.Text;
            var code = EncryptTripleDES(richTextBox1.Text, key);
            richTextBox2.Text = code;

            richTextBox3.Text = DecryptTripleDES(code, key);

            DateTime d2 = DateTime.Now;
            textBox2.Text = (d2 - d1).Ticks.ToString();
        }


        // Шифрование
        public static string EncryptTripleDES(string toEncrypt, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray;

            GetTextArrayAndKeyHash(toEncrypt, out toEncryptArray, key, out keyArray);
            var tdes = GetParametresTripleDES(keyArray);

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            // возвращаем расшифрованный текст
            return Encoding.Default.GetString(resultArray);
        }
        
        // Дешифрование
        public static string DecryptTripleDES(string cipherString, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray;
            // получаем байтовый код из шифрованной строки
            GetTextArrayAndKeyHash(cipherString, out toEncryptArray, key, out keyArray);

            var tdes = GetParametresTripleDES(keyArray);
            
            // основные операции
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            // расшифровка
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            // освобождаем ресурсы 
            tdes.Clear();
            // возвращаем расшифрованный текст
            return Encoding.Default.GetString(resultArray);
        }

        // получение параметров шифра
        public static TripleDESCryptoServiceProvider GetParametresTripleDES(byte[] keyArray)
        {
            // 3-DES алгоритм
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            // установим его секретный ключи
            tdes.Key = keyArray;
            // режим шифрования
            tdes.Mode = CipherMode.ECB;
            // режим заполнения дополнительных байтов
            tdes.Padding = PaddingMode.PKCS7;

            return tdes;
        }

        // Формируем данные
        public static void GetTextArrayAndKeyHash(string cipherString, out byte[] toEncryptArray, string key, out byte[] keyArray)
        {
            toEncryptArray = Encoding.Default.GetBytes(cipherString.ToString());

            // получаем хэш нашего секретного ключа
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            
            // удаляем использованные ресурсы для безопасности
            hashmd5.Clear();
        }

        // Шифруем шифром Вернама
        private void ButtonEncryptToVerrnameCipher(object sender, EventArgs e)
        {
            DateTime d1= DateTime.Now;

            string key = textBox1.Text;
            string text = richTextBox1.Text;
            // TODO: выпилено для более точного подсчета времени
            //MyDES des = new MyDES();
            
            // Попарно шифруем ключ текстом и текст ключом - получаем не одинаковые блоки, кратные 8
            //var bytesText = des.Encryption(text, key);
            //var bytesKey = des.Encryption(key, text);
            var bytesText = new byte[0];
            var bytesKey = new byte[0];
            GetTextArrayAndKeyHash(text, out bytesText, key,out bytesKey);;
            #region Шифрование и дешифрование
                // Создаем объект и инициализируем случайный массив перестановок
                Vernam ver = new Vernam(bytesText.Length);
                
                // Шифруем
                var code = ver.EncodeVernamCipher(bytesText, bytesKey);
                richTextBox2.Text = Encoding.Default.GetString(code);

                // Дешифруем
                var decode = ver.DecodeVernamCipher(code, bytesKey);

                //bytesText = des.Decryption(Encoding.Default.GetString(decode), key);
                var decoded = Encoding.Default.GetString(decode);
                //richTextBox3.Text = Encoding.Default.GetString(bytesText);
                richTextBox3.Text = decoded;
            #endregion

            DateTime d2 = DateTime.Now;
            textBox4.Text = (d2 - d1).Ticks.ToString();
        }


        // MyDes
        private void MyDesButton(object sender, EventArgs e)
        {
            DateTime d1 = DateTime.Now;

            string text = richTextBox1.Text;
            string key = textBox1.Text;

            var bytesText = new byte[0];
            var bytesKey = new byte[0];
            GetTextArrayAndKeyHash(text, out bytesText, key, out bytesKey); ;

            var toEncrypt = Encoding.Default.GetString(bytesText);
            var keyString = Encoding.Default.GetString(bytesKey);

            // Вывод шифровки
            MyDES des = new MyDES();
            var coded=Encoding.Default.GetString(des.Encryption(toEncrypt, keyString));
            richTextBox2.Text = coded;
            
            // Вывод расшифровки
            var deCoded = Encoding.Default.GetString(des.Decryption(coded, keyString));
            richTextBox3.Text = deCoded;

            DateTime d2 = DateTime.Now;
            textBox3.Text = (d2 - d1).Ticks.ToString();
        }

        // Зашифровка RC2
        private void ExampleButton(object sender, EventArgs e)
        {
            DateTime d1 = DateTime.Now;
            string key = textBox1.Text;
            var code = EncryptRC2(richTextBox1.Text, key);
            richTextBox2.Text = code;

            richTextBox3.Text = DecryptRC2(code, key);

            DateTime d2 = DateTime.Now;
            textBox5.Text = (d2 - d1).Ticks.ToString();
        }

        // Шифрование
        public static string EncryptRC2(string toEncrypt, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray;

            GetTextArrayAndKeyHash(toEncrypt, out toEncryptArray, key, out keyArray);
            var tdes = GetParametresRC2(keyArray);

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            // возвращаем расшифрованный текст
            return Encoding.Default.GetString(resultArray);
        }

        // Дешифрование
        public static string DecryptRC2(string cipherString, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray;
            // получаем байтовый код из шифрованной строки
            GetTextArrayAndKeyHash(cipherString, out toEncryptArray, key, out keyArray);

            var tdes = GetParametresRC2(keyArray);

            // основные операции
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            // расшифровка
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            // освобождаем ресурсы 
            tdes.Clear();
            // возвращаем расшифрованный текст
            return Encoding.Default.GetString(resultArray);
        }

        // получение параметров шифра
        public static RC2CryptoServiceProvider GetParametresRC2(byte[] keyArray)
        {
            // 3-DES алгоритм
            RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
            // установим его секретный ключи
            rc2.Key = keyArray;
            // режим шифрования
            rc2.Mode = CipherMode.ECB;
            // режим заполнения дополнительных байтов
            rc2.Padding = PaddingMode.PKCS7;

            return rc2;
        }
    }
}
