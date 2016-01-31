using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.Numerics;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        bool running = false; //функция проверки, подключен ли пользователь
        Socket client; //сокет для клиента
        IPAddress address; //ip сервера
        int port = 10000; //порт сервера
        bool sign; //проверка ЭЦП
        BigInteger DHkey;
        BigInteger N=3;
        BigInteger E;

        string login;

        private List<Thread> threads = new List<Thread>(); //список потоков

        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += Application_ApplicationExit;
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            CloseConnection();
        }

        void CloseConnection()
        {
            if (running) { client.Close(); } //если порт был привязан, то прекращаем слушание

            running = false;

            for (int i = 0; i < threads.Count(); i++) //перебираем весь список потоков
            {
                if (threads[i].ThreadState == ThreadState.Running) //если какой-то из потоков запущен
                {
                    threads[i].Abort(); //то закрываем его
                    threads.Remove(threads[i]);
                }
            };
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            try { address = IPAddress.Parse(txtIP.Text); }
            catch
            {
                MessageBox.Show("Ошибка! Введите IP-адрес."); return;
            }
            if (txtLoginReg.Text.Contains(" ") || txtLoginReg.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка! Введите корректный логин."); return;
            }

            if (txtPasswordReg.Text.Contains(" ") || txtPasswordReg.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка! Введите корректный пароль."); return;
            }

            if (txtPasswordReg.Text != txtPasswordRegRepeat.Text)
            {
                MessageBox.Show("Ошибка! Пароли не совпадают."); return;
            }

            Connect();
            running = true; //указываем, что мы подключились к серверу
            SendDataReg(); //начинаем принимать сообщения
        }

        void Connect()
        {
            try
            {
                //создаем сокет
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(address, port);
            }
            catch
            {
                MessageBox.Show("Ошибка! Не удается подключиться к серверу."); return;
            }
        }

        void SendDataReg() //функция отправки логина и пароля для рега
        {
            Thread th = new Thread(delegate () //создаем новый поток
            {
                try
                {
                    SendBytes("R");//Отправляем сообщение о том,что мы регистрируемся
                    generateKey();
                    Thread.Sleep(500);
                  //  MessageBox.Show(DHkey.ToString());
                    SendBytes(RC4(txtLoginReg.Text, DHkey.ToString())); //отправляем сообщение с логином
                    Thread.Sleep(500);
                    SendBytes(RC4(txtPasswordReg.Text, DHkey.ToString()));
                    Thread.Sleep(500);
                   // MessageBox.Show("отпр в качестве логина"+ RC4(txtLoginReg.Text, DHkey.ToString()));
                   // MessageBox.Show("отпр в качестве пароля" + RC4(txtLoginReg.Text, DHkey.ToString()));

                    string res; //создаем временную переменную для хранения принятых данных
                    res = Encoding.Unicode.GetString(ReceiveBytes()); //конвертируем байты в строку
                    if (res[0] == '0')
                    {
                        MessageBox.Show("Этот логин уже используется. Пожалуйста, введите логин заново.");
                        CloseConnection(); //закрываем соединение с сервером
                        return;
                    }
                    else if (res[0] == '1')
                    {
                        MessageBox.Show("Регистрация прошла успешно! (" + DHkey.ToString() + ")");
                        this.txtLoginReg.Invoke(new Action(() => { txtLoginReg.Clear(); }));
                        this.txtPasswordReg.Invoke(new Action(() => { txtPasswordReg.Clear(); }));
                        this.txtPasswordRegRepeat.Invoke(new Action(() => { txtPasswordRegRepeat.Clear(); }));
                        CloseConnection();
                        return;
                    }
                }
                catch { }
            });
            th.Start(); //запускаем поток
            threads.Add(th); //заносим поток в список потоков
        }

        public void SendDataIn()
        {
            Thread th = new Thread(delegate () //создаем новый поток
            {
                try
                {
                    SendBytes("I");
                    Thread.Sleep(500);
                    //textBox2.Invoke(new Action(() => { textBox2.AppendText("Был сгенерирован ключ:" + DHkey.ToString() + "\r\n"); }));
                    SendBytes(txtLoginIn.Text);
                    BigInteger n = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes()));
                    SendBytes(Function.calcHash(txtPasswordIn.Text + n));
                    generateKey();
                    N = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes())); // открыто получаем публичные ключи для проверки ЭЦП
                    E = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes()));

                    string tmp = Encoding.Unicode.GetString(ReceiveBytes());

                    if (tmp[0] == '0')
                    {
                        MessageBox.Show("Неверная комбинация логина и пароля! Повторите попытку.");
                        CloseConnection();
                        return;
                    }

                    else if (tmp[0] == '1')
                    {
                        MessageBox.Show("Вход выполнен! (" + DHkey.ToString() + ")");
                        login = txtLoginIn.Text;
                        this.txtLoginIn.Invoke(new Action(() => { txtLoginIn.Clear(); }));
                        this.txtPasswordIn.Invoke(new Action(() => { txtPasswordIn.Clear(); }));
                        this.groupBox1.Invoke(new Action(() => { groupBox1.Enabled = false; })); // Enabled = false;
                        this.btnLogIn.Invoke(new Action(() => { btnLogIn.Enabled = false; }));// Enabled = false;
                        this.groupBox2.Invoke(new Action(() => { groupBox2.Enabled = false; })); //Enabled = false;
                        this.btnSignUp.Invoke(new Action(() => { btnSignUp.Enabled = false; })); //Enabled = false;
                        this.panel1.Invoke(new Action(() => { panel1.Visible = true; })); //Visible = true;
                        this.btn_logout.Invoke(new Action(() => { btn_logout.Visible = true; }));
                        Size s = this.Size;
                        s.Height = 473;
                        this.Invoke(new Action(() => { Size = s; }));
                        //CloseConnection();
                        return;
                    }
                }
                catch { }
            });
            th.Start(); //запускаем поток
            threads.Add(th); //заносим поток в список потоков
        } //отправка данных входа

        void SendBytes(string message) //функция отправки байтов
        {
            byte[] bytes = new byte[message.Length * 2]; //переменная для отправки(получения) байтов
            byte[] bsize; //будет хранить в себе размер полученных данных в байтах
            bsize = BitConverter.GetBytes(message.Length); //конвертируем размер исходных данных в байты
            bytes = Encoding.Unicode.GetBytes(message); //инкодим стринг в байты
            client.Send(bsize); //отправляем размер строки
            client.Send(bytes); //отправляем строку
        }

        byte[] ReceiveBytes() //функция получения байтов
        {
            byte[] bytes; //будет хранить в себе полученные данные в байтах 
            byte[] bsize = new byte[8]; //будет хранить в себе размер полученных данных в байтах
            int size; //размер полученных данных в инт формате

            client.Receive(bsize); //получаем данных о размере строки 
            size = BitConverter.ToInt32(bsize, 0); //конвертируем размер строки в инт переменную

            bytes = new byte[size * 2]; //выделяем память под байты в размере длины полученной троки

            client.Receive(bytes); //принимаем байты

            return bytes; //возвращаем байты из функции
        }

        void btnLogIn_Click(object sender, EventArgs e)
        {
            try { address = IPAddress.Parse(txtIP.Text); } //проверяем, написал ли чего юзер в поле для ипа
            catch //если нет (выдал ошибку)
            {
                MessageBox.Show("Ошибка! Введите IP-адрес."); //говорим ему боксом, что он не прав и пусть введет
                return; //возвращаемся
            }
            //проверяем, корректно ли юзер ввел логин, нет ли пробелов, и не пустая ли строка
            if (txtLoginIn.Text.Contains(" ") || txtLoginIn.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка! Введите корректный логин."); return; //возвращаемся
            }

            if (txtPasswordIn.Text.Contains(" ") || txtPasswordIn.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка! Введите корректный пароль."); return;
            }

            Connect();   //коннектимся функцией
            running = true; //указываем, что мы подключились к серверу
            SendDataIn(); //начинаем принимать сообщения
        }

        public void generateKey()
        {
            BigInteger n = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes()));
            BigInteger q = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes()));
            BigInteger x = Function.num_gen(7);
            BigInteger pol = Function.PowMod(q, x, n);
            SendBytes(pol.ToString());
            BigInteger t = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes()));
            DHkey = Function.PowMod(t, x, n);
        }

        string RC4(string input, string key)
        {
            StringBuilder result = new StringBuilder();
            int x, y, j = 0;
            int[] box = new int[256];

            for (int i = 0; i < 256; i++)
            {
                box[i] = i;
            }

            for (int i = 0; i < 256; i++)
            {
                j = (key[i % key.Length] + box[i] + j) % 256;
                x = box[i];
                box[i] = box[j];
                box[j] = x;
            }

            for (int i = 0; i < input.Length; i++)
            {
                y = i % 256;
                j = (box[y] + j) % 256;
                x = box[y];
                box[y] = box[j];
                box[j] = x;

                result.Append((char)(input[i] ^ box[(box[y] + box[j]) % 256]));
            }
            return result.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size s = this.Size;
            s.Height = 220;
            this.Size = s;
            panel1.Visible = false;
          //  txtIP.Text = "192.168.2.1";
            txtIP.Text = "127.0.0.1";
        }

        private void button_Logout_Click(object sender, EventArgs e)
        {
            SendBytes(RC4("E", DHkey.ToString()));
            //SendBytes("E");
            CloseConnection();
            this.groupBox1.Enabled = true;
            this.btnLogIn.Enabled = true;
            this.groupBox2.Enabled = true;
            this.btnSignUp.Enabled = true;
            this.panel1.Visible = false;
            this.txtMessage.Clear();
            this.textBox2.Clear();
            Size s = this.Size;
            s.Height = 220;
            this.Size = s;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            Thread thh = new Thread(delegate () //И создаем поток для принятия от юзера дальнейших сообщений
            {
                SendBytes(RC4(txtMessage.Text, DHkey.ToString()));
                string mess = RC4(Encoding.Unicode.GetString(ReceiveBytes()), DHkey.ToString()); //ответ от сервера сообщение
                BigInteger EDS = BigInteger.Parse(RC4(Encoding.Unicode.GetString(ReceiveBytes()), DHkey.ToString())); //ЭЦП 
                string eds = Function.getStrFromBytes(BigInteger.ModPow(EDS, E, N).ToByteArray()); //прообраз сообщения
                if (mess == eds)
                    sign = true;
                else sign = false;
                this.textBox2.Invoke(new Action(() =>
                {
                    textBox2.AppendText(login + ": " + txtMessage.Text + "\r\n" + "Сервер: " + mess + "\r\nЭЦП сервера: "+sign);
                }));
            });             
            thh.Start(); //запускаем этот поток
            threads.Add(thh);
        }
    }
}