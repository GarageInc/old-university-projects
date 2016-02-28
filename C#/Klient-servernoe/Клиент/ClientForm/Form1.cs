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
        bool running = false; // функция проверки, подключен ли пользователь
        Socket client; // сокет для клиента
        IPAddress address; // ip сервера
        int port = 10000; // порт сервера

        bool sign; //проверка ЭЦП

        BigInteger DHkey;// Ключ для Диффи-Хелмана
        BigInteger N = 3;
        BigInteger E;

        string login;

        // Список потоков
        private List<Thread> threads = new List<Thread>();

        public Form1()
        {
            InitializeComponent();

            // При завершении работы закрываем соединение
            Application.ApplicationExit += Application_ApplicationExit;
        }

        // Функция, инициализирует переменные при появление формы
        protected void Form1_Load(object sender, EventArgs e)
        {
            txtIP.Text = "192.168.1.36";// localhost в народе
            //this.ineractivePanel.Visible = false;
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            CloseConnection();
        }

        // Закрываем соединение и все работающие потоки
        void CloseConnection()
        {
            if (running) { client.Close(); } //если порт был привязан, то прекращаем слушание

            running = false;

            for (int i = 0; i < threads.Count(); i++) // Перебираем весь список потоков
            {
                if (threads[i].ThreadState == ThreadState.Running) // Если какой-то из потоков запущен
                {
                    threads[i].Abort(); // То закрываем его и
                    threads.Remove(threads[i]);// Удаляем в списке
                }
            };
        }

        // Кнопка регистрации на стороне сервера
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Проверяем IP-адрес
            try
            {
                address = IPAddress.Parse(txtIP.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка! Введите IP-адрес.");
                return;
            }

            // Валидация логина
            if (txtLoginReg.Text.Contains(" ") || txtLoginReg.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка! Введите корректный логин.");
                return;
            }

            // Валидация пароля
            if (txtPasswordReg.Text.Contains(" ") || txtPasswordReg.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка! Введите корректный пароль.");
                return;
            }

            if (txtPasswordReg.Text != txtPasswordRegRepeat.Text)
            {
                MessageBox.Show("Ошибка! Пароли не совпадают.");
                return;
            }

            Connect();// Соединяемся
            SendDataReg(); // Начинаем принимать сообщения
        }

        void Connect()
        {
            try
            {
                if (running)
                {
                    trace("Уже соединены");
                    return;
                }
                //создаем сокет на основе TCP/IP
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // trace("init");
                client.Connect(address, port);
                // trace("connected");
                // client.Listen(50);
                // trace("listen");
                running = true; // Указываем, что мы подключились к серверу
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка! Не удается подключиться к серверу.");
                trace(e.Message);
                return;
            }
        }

        void SendDataReg() //функция отправки логина и пароля для регистрации
        {
            Thread th = new Thread(delegate () //Cоздаем новый поток и передаем ему делегат - безымянную функцию
            {
            try
            {
                SendBytes("R");// Отправляем сообщение о том, что мы регистрируемся

                generateDHKey();// Генерируем ключ

                // Отправляем зашифрованное сообщение: введенный логин
                SendBytes(Functions.RC4(txtLoginReg.Text, DHkey.ToString()));

                // Отправляем зашифрованное сообщение: введенный пароль
                SendBytes(Functions.RC4(txtPasswordReg.Text, DHkey.ToString()));

                // создаем временную переменную для хранения принятых данных
                string res = ReceiveString(); // конвертируем байты в строку

                // Смотрим на полученные результат:
                if (res[0] == '0')
                {
                    MessageBox.Show("Этот логин уже используется. Пожалуйста, введите логин заново.");
                    return;
                }
                else if (res[0] == '1')
                {
                    MessageBox.Show("Регистрация прошла успешно! (" + DHkey.ToString() + ")");
                    return;
                }
                else
                {
                    trace("REGISTRATION pass");
                }
            }
            catch (Exception e)
            {

                trace("REGISTRATION ERROR: " + e.Message);
            }
            });

            th.Start(); // Запускаем поток
            threads.Add( th ); // Заносим поток в список потоков
        }

        // Отправка данных входа
        public void SendDataIn()
        {
            Thread th = new Thread(delegate () //создаем новый поток
            {
            try
            {
                SendBytes("I");

                // Отправляется сообщение: логин
                SendBytes(txtLoginIn.Text);

                BigInteger n = BigInteger.Parse(ReceiveString());
                SendBytes(Functions.calcHash(txtPasswordIn.Text + n));// Отправляется MD5-хеш сообщения

                generateDHKey();// Генерируем ключ

                N = BigInteger.Parse(ReceiveString()); // открыто получаем публичные ключи для проверки ЭЦП
                E = BigInteger.Parse(ReceiveString());

                //trace("----");
                string tmp = ReceiveString();

                //trace("!!!!");

                //trace(tmp);
                //trace("????");
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
                    /*
                        this.txtLoginIn.Invoke(new Action(() => { txtLoginIn.Clear(); }));
                        this.txtPasswordIn.Invoke(new Action(() => { txtPasswordIn.Clear(); }));
                        this.groupBox1.Invoke(new Action(() => { groupBox1.Enabled = false; })); // Enabled = false;
                        this.btnLogIn.Invoke(new Action(() => { btnLogIn.Enabled = false; }));// Enabled = false;
                        this.groupBox2.Invoke(new Action(() => { groupBox2.Enabled = false; })); //Enabled = false;
                        this.btnSignUp.Invoke(new Action(() => { btnSignUp.Enabled = false; })); //Enabled = false;
                        //this.ineractivePanel.Invoke(new Action(() => { ineractivePanel.Visible = true; })); //Visible = true;
                        this.btn_logout.Invoke(new Action(() => { btn_logout.Visible = true; }));
                      */
                      
                    return;
                }
                else
                {
                    trace("LOGIN: pass");
                    // pass не предусмотрен иной вариант
                }
            }
            catch (Exception e)
            {
                trace("LOGIN ERROR: " + e.Message);
            }
            });

            th.Start(); // запускаем поток
            threads.Add(th); // заносим поток в список потоков
        }

        // Функция отправки сообщения
        void SendBytes(string message)
        {
            try
            {
                trace("-> Begin sending bytes...");

                byte[] bytes = Encoding.Unicode.GetBytes(message); //инкодим стринг в байты

                client.Send(bytes); //отправляем строку

                trace(" = Bytes sended...");

            }
            catch (Exception e)
            {
                trace("ERROR SENDING:" + e.Message);
            }
        }

        // Функция получения байтов из потока
        string ReceiveString()
        {
            try
            {
                trace("-> Begin reading bytes...");
                byte[] bytes = new byte[1024];

                int bytesRec = client.Receive(bytes);
                string data = Encoding.Unicode.GetString(bytes, 0, bytesRec);

                trace(" = Read bytes:" + data);
                return data; //возвращаем байты из функции
            }
            catch (Exception e)
            {
                trace("ERROR RECEIVE: " + e.Message);
                return "0";
            }
        }

        // Хэндлер для нажатий по кнопке "Войти"
        void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                address = IPAddress.Parse(txtIP.Text);
            } //проверяем, написал ли чего юзер в поле для ипа
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
            SendDataIn(); //начинаем принимать сообщения
        }

        public void generateDHKey()
        {
            BigInteger n = BigInteger.Parse(ReceiveString());
            BigInteger q = BigInteger.Parse(ReceiveString());
            BigInteger x = Functions.num_gen(7);
            BigInteger pol = Functions.PowMod(q, x, n);

            SendBytes(pol.ToString());

            BigInteger t = BigInteger.Parse(ReceiveString());

            DHkey = Functions.PowMod(t, x, n);
        }


        // Разрыв соединения
        private void button_Logout_Click(object sender, EventArgs e)
        {
            SendBytes(Functions.RC4("E", DHkey.ToString()));

            CloseConnection();

            this.groupBox1.Enabled = true;
            this.btnLogIn.Enabled = true;
            this.groupBox2.Enabled = true;
            this.btnSignUp.Enabled = true;
            //this.ineractivePanel.Visible = false;
            this.txtMessage.Clear();
            this.responsesBox.Clear();
        }

        // Хэндлер на нажатие кнопки "Отправить"
        private void button_Send_Click(object sender, EventArgs e)
        {
            // И создаем поток для принятия от сервера дальнейших сообщений
            Thread thh = new Thread(delegate ()
            {
                try
                {
                    button_Send.Enabled = false;
                    SendBytes(Functions.RC4(txtMessage.Text, DHkey.ToString()));// Отправляется текст и ключ

                    string mess = Functions.RC4(ReceiveString(), DHkey.ToString()); // ответ от сервера сообщение

                    BigInteger EDS = BigInteger.Parse(Functions.RC4(ReceiveString(), DHkey.ToString())); // ЭЦП 
                    string eds = Functions.getStrFromBytes(BigInteger.ModPow(EDS, E, N).ToByteArray()); // прообраз сообщения

                    if (mess == eds)
                        sign = true;
                    else
                        sign = false;

                    trace(login + ": " + txtMessage.Text);
                    trace("Сервер: " + mess);
                    trace("ЭЦП сервера: " + sign);

                    button_Send.Enabled = true;
                } catch(Exception er)
                {
                    trace("ERROR: " + er.Message);
                    button_Send.Enabled = true;
                }
                
            });            

            thh.Start(); // запускаем этот поток
            threads.Add( thh );
        }


        public void trace(string message)
        {
            responsesBox.Invoke(new Action(() => {
                responsesBox.AppendText(message + "\r\n");
            }));
        }

    }
}