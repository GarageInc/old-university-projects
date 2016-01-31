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
using System.Security.Cryptography;

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

        public byte[] ECBC(byte[] message, byte[] key)
        {
            // Разобьём на 4 блока:
            var length =  message.Length / 4;
            var mass_length = message.Length / 4;
            if (mass_length < 8)
                mass_length = 8;

            byte[] m0 = new byte[mass_length];
            Array.Copy(message, 0, m0, 0, length);

            byte[] m1 = new byte[mass_length];
            Array.Copy(message, length, m1, 0, length);

            byte[] m2 = new byte[mass_length];
            Array.Copy(message, length * 2, m2, 0, length);

            byte[] m3 = new byte[message.Length - length * 3];
            Array.Copy(message, length * 3, m3, 0, message.Length - length * 3);

            // для шифрования - SHA 256 - даёт блоки по 32 байта
            SHA256 mySHA256 = SHA256Managed.Create();
            var r1 = mySHA256.ComputeHash(message);
            var r2 = mySHA256.ComputeHash(m3);


            byte[] ecbc = new byte[length];

            // 4 раза шифруем перестановками и получаем результат. КЛЮЧ ВЕЗДЕ ОДИН!
            for (int i=0; i<4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            // Проводим через SHA-256:
                            var hash = mySHA256.ComputeHash(m0);
                            
                            // Проводим побитовое XOR сложение результата шифрования с блоками сообщения m1,m2,m3,m4
                            var xor = new XOR(hash.Length);
                            var result_xor = xor.EncodeVernamCipher(hash, m0);

                            // Первая блочная шифровка:
                            ecbc = des.Encryption(Encoding.Default.GetString(result_xor), Encoding.Default.GetString(key));
                            break;
                        }

                    case 1:
                        {
                            // Проводим через SHA-256:
                            var hash = mySHA256.ComputeHash(m1);

                            // Проводим побитовое XOR сложение результата шифрования с блоками сообщения m1,m2,m3,m4
                            var xor = new XOR(hash.Length);
                            var result_xor = xor.EncodeVernamCipher(hash,m1);

                            // Вторая блочная шифровка:
                            ecbc = des.Encryption(Encoding.Default.GetString(result_xor), Encoding.Default.GetString(key));
                            break;
                        }

                    case 2:
                        {
                            // Проводим через SHA-256:
                            var hash = mySHA256.ComputeHash(m2);

                            // Проводим побитовое XOR сложение результата шифрования с блоками сообщения m1,m2,m3,m4
                            var xor = new XOR(hash.Length);
                            var result_xor = xor.EncodeVernamCipher(hash, m2 );

                            // Третья блочная шифровка:
                            ecbc = des.Encryption(Encoding.Default.GetString(result_xor), Encoding.Default.GetString(key));
                            break;
                        }

                    case 3:
                        {
                            // Проводим через SHA-256:
                            var hash = mySHA256.ComputeHash(m3);

                            // Проводим побитовое XOR сложение результата шифрования с блоками сообщения m1,m2,m3,m4
                            var xor = new XOR(hash.Length);
                            var result_xor = xor.EncodeVernamCipher(hash, m3);

                            // Четвертая блочная шифровка
                            ecbc = des.Encryption(Encoding.Default.GetString(result_xor), Encoding.Default.GetString(key));
                            break;
                        }
                }


            }

            // Вернем результат
            return ecbc;
        }





        // Формируем данные
        public static void GetTextAndKeyBytes(string cipherString, out byte[] toEncryptArray, string key, out byte[] keyArray)
        {
            // получаем байты для переданной строки
            toEncryptArray = Encoding.Default.GetBytes(cipherString.ToString());

            // получаем байты нашего секретного ключа
            keyArray = Encoding.UTF8.GetBytes(key);
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

            // Получим подпись через ECBC(4 цикла)
            var oldmac = ECBC(bytesText, Encoding.Default.GetBytes(секретныйКлючКлиента));
            
            // Выводим сообщение с подписью
            cryptedMessageTextBox.Text = Encoding.Default.GetString(oldmac.ToArray());
            var mac = oldmac.ToArray().Take(48).ToList();
            // Прикрепляем к подписи всё сообщение и отправляем серверу(следующие строчки кода)
            mac.AddRange(bytesText);

            var result = mac.ToArray();

            // изменяем текст сообщения(если вдруг такое нужно сделать - это вмешательство Евы, она изменила 50ый байт сообщения
            if (result.Length > 14 && checkBox1.Checked)
            {
                result[50] = (byte)120;
            }

            // Отправляем серверу сообщение
            сервер.Send(result, result.Length, remoteep1);
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

            ChangeText("\nServer начал прослушивание для "+(string)user+"\n");

            while (true)
            {
                Thread.Sleep(1000);
                Byte[] data = клиент.Receive(ref localEp);

                // Получив сообщение - берем у него подпись: это первые 48 байт
                var mac = data.Take(48);
                // потом берем - сам текст сообщения
                var body = new List<byte>();
                for (int i = 48; i < data.Length; i++)
                {
                    body.Add(data[i]);
                }

                var mac2 = ECBC(body.ToArray(), Encoding.Default.GetBytes(секретныйКлючСервера)).Take(48).ToArray();
                
                // Вывод полученной информации:
                ChangeText("\nПришло сообщение от " + 
                    (string)user + 
                    " : " +
                    Encoding.Default.GetString(body.ToArray()));
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

            button1.Enabled = true;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
