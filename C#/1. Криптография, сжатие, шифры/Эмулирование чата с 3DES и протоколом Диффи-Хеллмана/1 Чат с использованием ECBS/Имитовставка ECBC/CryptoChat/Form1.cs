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
        static private UdpClient клиент;
        static private UdpClient сервер;
        static private IPAddress multicastaddress; // один из зарезервированных для локальных нужд UDP адресов;
        private IPEndPoint remoteep1;
        private IPEndPoint remoteep2;

        My3DES des = new My3DES();

        string Р = "";
        string секретныйКлючКлиента = "";
        string секретныйКлючСервера = "";

        public Form1()
        {
            InitializeComponent();

            // Инициализация
            клиент = new UdpClient();
            сервер = new UdpClient();

            multicastaddress = IPAddress.Parse("239.0.0.222"); // один из зарезервированных для локальных нужд UDP адресов;
            
            remoteep1 = new IPEndPoint(multicastaddress, 2222);
            remoteep2 = new IPEndPoint(multicastaddress, 2223);
            
            клиент.JoinMulticastGroup(multicastaddress);
            сервер.JoinMulticastGroup(multicastaddress);

            // Прослушивание  клиента
            object name = "Client";
            Thread слушаемПоток = new Thread(this.ПрослушиваниеПорта);
            слушаемПоток.Start(name);
            
        }
        
        // Отправка клиентом сообщения
        public void ОтправитьСообщение(string data)
        {
            string text = firstMessageRichTextBox.Text;

            var bytesText = new byte[0];
            var bytesKey = new byte[0];

            ПолучитьМассивТекстаВБайтахИХешКлюча(text, out bytesText, секретныйКлючКлиента, out bytesKey); ;

            var toEncrypt = Encoding.Default.GetString(bytesText);
            var keyString = Encoding.Default.GetString(bytesKey);

            // Вывод шифровки - шифруем сообщение
            var coded = des.Encryption(toEncrypt, keyString);
            
            var message = coded.ToArray();
            cryptedMessageTextBox.Text = "Зашифрованное сообщение от Client: " + Encoding.Default.GetString(coded);
            
             // Отправляем серверу сообщение в чат
            сервер.Send(message, message.Length, remoteep1);
        }
        
        // Прослушиваем. Если получили- то расшифровываем своим ключом
        public void ПрослушиваниеПорта(object user)
        {
            UdpClient клиент = new UdpClient();

            клиент.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);

            клиент.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            клиент.ExclusiveAddressUse = false;

            клиент.Client.Bind(localEp);

            клиент.JoinMulticastGroup(multicastaddress);

            ChangeText("\nServer начала прослушивание для "+(string)user+"\n");

            while (true)
            {
                Thread.Sleep(1000);
                Byte[] data = клиент.Receive(ref localEp);
                
                // Вывод расшифровки
                var deCoded = Encoding.Default.GetString(des.Decryption(Encoding.Default.GetString(data.ToArray()), секретныйКлючКлиента));

                ChangeText("\nПришло сообщение от " + (string)user + " : " +deCoded);
            }
        }

        private void ButtonNewMessageAndListen_User1(object sender, EventArgs e)
        {
            // Сообщение, которое отправляем
            string data = firstMessageRichTextBox.Text;
            ОтправитьСообщение(data);
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
        

        // Формируем данные
        public static void ПолучитьМассивТекстаВБайтахИХешКлюча(string cipherString, out byte[] toEncryptArray, string key, out byte[] keyArray)
        {
            toEncryptArray = Encoding.Default.GetBytes(cipherString.ToString());

            // получаем хэш нашего секретного ключа
            keyArray = Encoding.UTF8.GetBytes(key);
        }

        // Генерация секретного ключа и обмен между сервером и клиентом - по протоколу Диффи-Хеллмана
        private void buttonGenerateSecretKey_Click(object sender, EventArgs e)
        {
            // По 1 простому числу для каждого клиента
            int a1 = 7, a2 = 11;
            int p=a1*a2;
            string simplekey = keyBox.Text;
            char[] keybytes = simplekey.ToCharArray();

            var result1 = "";
            var result2 = "";
            // генерация первого
            foreach(var k in keybytes)
            {
                result1 += (char)(int)BigInteger.ModPow((int)k, a1, p);
            }
            // генерация второго
            foreach (var k in keybytes)
            {
                result2 += (char)(int)BigInteger.ModPow((int)k, a2, p);
            }
        
            // Теперь считаем, что первый клиент отправил result1, второй клиент отправил result2

            // Выведем теперь общий ключ шифрования(на стороне первого клиента)
            секретныйКлючКлиента = "";
            foreach (var k in result2.ToCharArray())
            {
                секретныйКлючКлиента += (char)(int)BigInteger.ModPow((int)k, a1, p);
            }

            // Выведем теперь общий ключ шифрования(на стороне второго клиента)
            секретныйКлючСервера = "";
            foreach (var k in result1.ToCharArray())
            {
                секретныйКлючСервера += (char)(int)BigInteger.ModPow((int)k, a2, p);
            }

            
            if (секретныйКлючКлиента == секретныйКлючСервера)
                // Рукопожатие сработало! Клиенты обменялись секретными ключами!
                secretKeyBox.Text = секретныйКлючКлиента;
            else// Иначе - не получилось обменяться
                secretKeyBox.Text = "Протокол Диффи-Хеллмана не сработал!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void keyBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
