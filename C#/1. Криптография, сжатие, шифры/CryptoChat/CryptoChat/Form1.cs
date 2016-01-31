using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace CryptoChat
{
    public partial class Form1 : Form
    {
        // Клиенты
        static private UdpClient udpclient1;
        static private UdpClient udpclient2;
        static private IPAddress multicastaddress; // один из зарезервированных для локальных нужд UDP адресов;
        private IPEndPoint remoteep1;
        private IPEndPoint remoteep2;

        My3DES des = new My3DES();

        public Form1()
        {
            InitializeComponent();
            // Инициализация
            udpclient1 = new UdpClient();
            udpclient2 = new UdpClient();
            multicastaddress = IPAddress.Parse("239.0.0.222"); // один из зарезервированных для локальных нужд UDP адресов;
            
            remoteep1 = new IPEndPoint(multicastaddress, 2222);
            remoteep2 = new IPEndPoint(multicastaddress, 2223);
            
            udpclient1.JoinMulticastGroup(multicastaddress);
            udpclient2.JoinMulticastGroup(multicastaddress);

            // Прослушивание первого клиента
            object name = "USER1";
            Thread ListenThread1 = new Thread(this.Listen1);
            ListenThread1.Start(name);

            // Прослушивание второго клиента
            name = "USER2";
            Thread ListenThread2 = new Thread(this.Listen2);
            ListenThread2.Start(name);
        }
            
        public void SendMessage1(string data)
        {
            string text = firstMessageRichTextBox.Text;
            string key = secretKeyBox.Text;

            var bytesText = new byte[0];
            var bytesKey = new byte[0];
            GetTextArrayAndKeyHash(text, out bytesText, key, out bytesKey); ;

            var toEncrypt = Encoding.Default.GetString(bytesText);
            var keyString = Encoding.Default.GetString(bytesKey);

            // Вывод шифровки
            var coded = des.Encryption(toEncrypt, keyString);

            decyptedMessageTextBox.Text = "USER1: "+Encoding.Default.GetString(coded);
            udpclient2.Send(coded, coded.Length, remoteep1);
        }

        public void SendMessage2(string data)
        {
            string text = secondMessageRichTextBox.Text;
            string key = secretKeyBox.Text;

            var bytesText = new byte[0];
            var bytesKey = new byte[0];
            GetTextArrayAndKeyHash(text, out bytesText, key, out bytesKey); ;

            var toEncrypt = Encoding.Default.GetString(bytesText);
            var keyString = Encoding.Default.GetString(bytesKey);

            // Вывод шифровки
            var coded = des.Encryption(toEncrypt, keyString);
            decyptedMessageTextBox.Text = "USER2 " + Encoding.Default.GetString(coded);
            
            udpclient1.Send(coded, coded.Length, remoteep2);
        }

        public void Listen1(object user)
        {
            UdpClient client = new UdpClient();

            client.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);

            ChangeText("\tПрослушивание для "+(string)user+" началось\n");

            while (true)
            {
                Thread.Sleep(1000);
                Byte[] data = client.Receive(ref localEp);

                var keyString = secretKeyBox.Text;

                // Вывод расшифровки
                var deCoded = Encoding.Default.GetString(des.Decryption(Encoding.Default.GetString(data), keyString));

                ChangeText("\nПришло сообщение от " + (string)user + " : " + deCoded);
            }
        }

        public void Listen2(object user)
        {
            UdpClient client = new UdpClient();

            client.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2223);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);

            ChangeText("\tПрослушивание для " + (string)user + " началось\n");

            while (true)
            {
                Thread.Sleep(1000);
                Byte[] data = client.Receive(ref localEp);
                
                var keyString = secretKeyBox.Text;

                // Вывод расшифровки
                var deCoded = Encoding.Default.GetString(des.Decryption(Encoding.Default.GetString(data), keyString));

                ChangeText("\nПришло сообщение от " + (string)user + " : " + deCoded);
            }
        }


        private void ButtonNewMessageAndListen_User1(object sender, EventArgs e)
        {
            // Сообщение, которое отправляем
            string data = firstMessageRichTextBox.Text;
            SendMessage1(data);
        }

        private void ButtonNewMessageAndListen_User2(object sender, EventArgs e)
        {
            // Сообщение, которое отправляем
            string data = secondMessageRichTextBox.Text;
            SendMessage2(data);
        }

        /// <summary>
        /// Изменение richTextBox'a
        /// </summary>
        /// <param name="text"></param>
        public void ChangeText(string text)
        {
            if (showMessagesRichTextBox.InvokeRequired)
            {
                showMessagesRichTextBox.Invoke(new text(ChangeText), text);
            }
            else
            {
                showMessagesRichTextBox.Text += (text);
            }
        }
        
        delegate void text(string text);

        // MyDes
        private void MyDesButton(object sender, EventArgs e)
        {
            string text = showMessagesRichTextBox.Text;
            string key = keyBox.Text;

            var bytesText = new byte[0];
            var bytesKey = new byte[0];
            GetTextArrayAndKeyHash(text, out bytesText, key, out bytesKey); ;

            var toEncrypt = Encoding.Default.GetString(bytesText);
            var keyString = Encoding.Default.GetString(bytesKey);

            // Вывод шифровки
            My3DES des = new My3DES();
            var coded = Encoding.Default.GetString(des.Encryption(toEncrypt, keyString));
            firstMessageRichTextBox.Text = coded;

            // Вывод расшифровки
            var deCoded = Encoding.Default.GetString(des.Decryption(coded, keyString));
            secondMessageRichTextBox.Text = deCoded;

        }

        // Формируем данные
        public static void GetTextArrayAndKeyHash(string cipherString, out byte[] toEncryptArray, string key, out byte[] keyArray)
        {
            toEncryptArray = Encoding.Default.GetBytes(cipherString.ToString());

            // получаем хэш нашего секретного ключа
            keyArray = Encoding.UTF8.GetBytes(key);
        }

        string secretKey1 = "";
        string secretKey2 = "";
        // Генерация секретного ключа
        private void buttonGenerateSecretKey_Click(object sender, EventArgs e)
        {
            int a1 = 7, a2 = 11;
            int p=a1*a2;
            string simplekey = keyBox.Text;

            char[] keybytes = simplekey.ToCharArray();
            var result1 = "";
            var result2 = "";
            // Отправка первого
            foreach(var k in keybytes)
            {
                result1 += (char)(int)BigInteger.ModPow((int)k, a1, p);
            }
            SendMessage1(result1);
            // Отправка второго
            // Отправка первого
            foreach (var k in keybytes)
            {
                result2 += (char)(int)BigInteger.ModPow((int)k, a2, p);
            }
            SendMessage2(result2);
        
            // Выведем теперь общий ключ шифрования(на стороне первого клиента)
            secretKey1 = "";
            foreach (var k in result2.ToCharArray())
            {
                secretKey1 += (char)(int)BigInteger.ModPow((int)k, a1, p);
            }
            // Выведем теперь общий ключ шифрования(на стороне второго клиента)
            secretKey2 = "";
            foreach (var k in result1.ToCharArray())
            {
                secretKey2 += (char)(int)BigInteger.ModPow((int)k, a2, p);
            }

            if (secretKey1 == secretKey2)
                secretKeyBox.Text = secretKey1;
            else
                secretKeyBox.Text = "Пр.Д-Х.не ср-л";
        }
    }
}
