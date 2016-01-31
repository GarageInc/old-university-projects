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
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections;
using System.Numerics;
using System.Data.OleDb;

namespace ServerForm
{
    public struct Euclid_result
    {
        public BigInteger C;
        public BigInteger x;
        public BigInteger y;
    }

    public partial class Form1 : Form
    {
        bool running = false; //запущен ли сервер
        int port; //порт сервера
        IPAddress address; //айпи сервера
        Socket listener; //сокет для прослушивания
        IPEndPoint Point; //информация о точке (ип, порт)
        Socket client; //клиент
        private List<Thread> threads = new List<Thread>(); //лист для потоков
        BigInteger DHkey;// общий ключ по Диффи Хеллману
        BigInteger P, Q, N, FI, E, D;


        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBase.accdb";

        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += Application_ApplicationExit; //указываем, что при закрытии программы будет вызываться ивент
        }

        void Application_ApplicationExit(object sender, EventArgs e) //ивент закрытия программы
        {
            if (running) { listener.Close(); } //если порт был привязан, то прекращаем слушание
            running = false;
            for (int i = 0; i < threads.Count(); i++) //перебираем весь список потоков
            {
                if (threads[i].ThreadState == ThreadState.Running) //если какой-то из потоков запущен
                {
                    threads[i].Abort(); //то закрываем его
                }
            };
        }

        private void btnTurn_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                //создаем сокет для прослушки
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Point = new IPEndPoint(IPAddress.Any, port);
                listener.Bind(Point); //связываем сокет с точкой
                listener.Listen(300); //начинаем прослушивать, макс. 300
                btnTurn.Text = "Остановка сервера";
                txtLog.AppendText("Сервер запущен...\r\n"); 
                running = true; 
                AcceptSocket(); //начинаем принимать сокеты
            }
            else
            {
                btnTurn.Text = "Запуск сервера";
                txtLog.AppendText("Сервер остановлен...\r\n");
                running = false;
                listener.Close();
                //закрываем и очищаем потоки
                for (int i = 0; i < threads.Count(); i++)
                {
                    if (threads[i].ThreadState == ThreadState.Running)
                    {
                        threads[i].Abort();
                        threads.Remove(threads[i]);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            port = 10000; //назначаем порт
         //   address = IPAddress.Parse("192.168.2.1");
            address = IPAddress.Parse("127.0.0.1");
         //   address = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2]; //берем из хоста айпи
            txtIPPORT.Text = address.ToString() + ":" + port.ToString();  //вписываем в поле ип:порт
        }

        private void AcceptSocket() //функция прослушки юзеров
        {
            Thread th = new Thread(delegate () //создаем новый поток для прослушки
            {
                while (running) //делаем ниже, пока сервер запущен
                {
                    try
                    {
                        client = listener.Accept(); //создаем сокет для клиента
                    }
                    catch { return; }

                    string login, password, res; //временную стринг переменную

                    res = Encoding.Unicode.GetString(ReceiveBytes(client)); //конвертируем байты в "путь" (регистр или авторизац)

                    if (res[0] == 'R') //если "путь" будет регистрация, то
                    {
                        generateKey();
                        txtLog.Invoke(new Action(() => { txtLog.AppendText("При регистрации был сгенерирован ключ: " + DHkey.ToString() + "\r\n"); }));
                        login = RC4(Encoding.Unicode.GetString(ReceiveBytes(client)), DHkey.ToString()); //конвертируем байты в логин
                        password = RC4(Encoding.Unicode.GetString(ReceiveBytes(client)), DHkey.ToString()); //конвертируем байты в пароль
                        //MessageBox.Show(login);
                        //MessageBox.Show(password);
                        OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
                        OleDbCommand myOleDbCommand = myOleDbConnection.CreateCommand();
                        myOleDbCommand.CommandText = "SELECT * FROM Пользователи where Логин='" + login + "'";
                        myOleDbConnection.Open();

                        OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                        if (myOleDbDataReader.Read()) //если такой логин уже содержится
                        {
                            SendBytes(client, "0");  //то отправляем клиенту сообщение о том, что такой логин уже есть
                            myOleDbDataReader.Close();
                            myOleDbConnection.Close();
                        }
                        else
                        {
                            myOleDbDataReader.Close();
                            myOleDbCommand.CommandText = "INSERT into Пользователи values('" + login + "', '" + password + "')";
                            myOleDbCommand.ExecuteNonQuery();
                            myOleDbConnection.Close();
                            SendBytes(client, "1");//отправляем клиенту сообщение о том, что регистрация прошла успешно
                            this.txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " зарегистрировался.\r\n"); }));
                        }
                    }
                    else if (res[0] == 'I') //иначе, если вход, то
                    {
                        password = "pswd";
                        bool exists = true;
                        login = Encoding.Unicode.GetString(ReceiveBytes(client)); //конвертируем байты в логин
                        //MessageBox.Show(login);
                        OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
                        OleDbCommand myOleDbCommand = myOleDbConnection.CreateCommand();
                        myOleDbCommand.CommandText = "SELECT Пароль FROM Пользователи where Логин='" + login + "'";
                        myOleDbConnection.Open();

                        OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                        if (myOleDbDataReader.Read())
                        {
                            password = myOleDbDataReader["Пароль"].ToString();
                        }
                        else
                        {
                            exists = false;
                        }
                        myOleDbDataReader.Close();
                        myOleDbConnection.Close();
                        Random rnd = new Random();
                        BigInteger n = Function.num_gen(10);
                        SendBytes(client, n.ToString());
                        string hash1 = Encoding.Unicode.GetString(ReceiveBytes(client));
                        string hash2 = Function.calcHash(password + n);
                        generateKey();
                        Thread.Sleep(500);
                        generateRSA();
                        SendBytes(client, N.ToString());
                        Thread.Sleep(500);
                        SendBytes(client, E.ToString());
                        if (exists == false || hash1 != hash2) { SendBytes(client, "0"); }

                        else
                        {
                            txtLog.Invoke(new Action(() => { txtLog.AppendText("При авторизации был сгенерирован ключ: " + DHkey.ToString() + "\r\n"); }));

                            SendBytes(client, "1"); //отправляем клиенту сообщение о том, что пользователь успешно вошел

                            this.txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " вошел в сеть\r\n"); }));

                            Thread thh = new Thread(delegate () //И создаем поток для принятия от юзера дальнейших сообщений
                            {
                                try
                                {
                                    string M, m = "";
                                    BigInteger EDS;
                                    while (m != "E")
                                    {
                                        m = RC4(Encoding.Unicode.GetString(ReceiveBytes(client)), DHkey.ToString());//получили от юзера сообщение m
                                        if (!running)
                                        {
                                            M = "Сервер недоступен!\r\n";
                                            SendBytes(client, RC4(M, DHkey.ToString()));
                                        }
                                        if ((running) && (m != "E"))
                                        {
                                            M = "Cервер получил сообщение: " + m;
                                            SendBytes(client, RC4(M, DHkey.ToString()));
                                            Thread.Sleep(500);
                                            EDS = new BigInteger(Function.getBytesFromStr(M));//ЭЦП
                                            SendBytes(client, RC4(BigInteger.ModPow(EDS, D, N).ToString(), DHkey.ToString()));//отправляем ЭЦП
                                            this.txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " написал:\r\n" + m + "\r\n"); }));
                                        }
                                        else txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " вышел из сети\r\n"); }));
                                    }
                                }
                                catch
                                {
                                    txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " вышел из сети\r\n"); }));
                                }
                            });
                            thh.Start(); //запускаем этот поток
                            threads.Add(thh);
                        }
                    }
                }
            });
            th.Start(); //запускаем весь предыдущий поток
            threads.Add(th); //Добавляем поток в список потоков
        }

        byte[] ReceiveBytes(Socket client) //функция получения байтов
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

        void SendBytes(Socket client, string message) //функция отправки байтов
        {
            byte[] bytes = new byte[message.Length * 2]; //переменная для отправки(получения) байтов
            byte[] bsize; //будет хранить в себе размер полученных данных в байтах
            bsize = BitConverter.GetBytes(message.Length); //конвертируем размер исходных данных в байты
            bytes = Encoding.Unicode.GetBytes(message); //инкодим стринг в байты
            client.Send(bsize); //отправляем размер строки
            client.Send(bytes); //отправляем строку
        }
        
        public void generateKey()
        {
            BigInteger n = Function.num_gen(8);
            SendBytes(client, n.ToString());
            Thread.Sleep(500);
            BigInteger q = Function.num_gen(8);
            SendBytes(client, q.ToString());
            BigInteger y = Function.num_gen(7);
            BigInteger K = Function.PowMod(q, y, n);
            BigInteger M = BigInteger.Parse(Encoding.Unicode.GetString(ReceiveBytes(client)));           
            SendBytes(client,K.ToString());
            DHkey = Function.PowMod(M, y, n);
        }

        public void generateRSA()
        {
            P = Function.num_gen(100);
            Thread.Sleep(1);
            Q = Function.num_gen(100);
            N = P * Q;
            FI = (P - 1) * (Q - 1);
            E = Function.num_gen(P.ToString().Length / 2 + 1);
            D = get_inverse(FI, E);
        }

        string RC4(string input, string key)
        {
            StringBuilder result = new StringBuilder();
            int x,y, j = 0;
            int[] box = new int[256];

            for (int i = 0; i < 256; i++) //начальная инициализация вектора перестановки ключом
            {
                box[i] = i;
            }

            for (int i = 0; i < 256; i++) 
            {
                j = (key[i % key.Length] + box[i] + j) % 256;
                x = box[i]; //swap
                box[i] = box[j];
                box[j] = x;
            }

            for (int i = 0; i < input.Length; i++) //Pseudo Random Generation Algoritm
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

        public Euclid_result euclid(BigInteger a, BigInteger b)
        {
            BigInteger A;
            BigInteger B;
            BigInteger x1 = 0;
            BigInteger y1 = 1;
            BigInteger x = 0;
            BigInteger y = 0;
            BigInteger AdivB;
            BigInteger AmodB;
            List<BigInteger> Div = new List<BigInteger>();

            if (a < b)
            {
                A = b;
                B = a;
            }
            else
            {
                A = a;
                B = b;
            }

            while (true)
            {
                AdivB = A / B;
                AmodB = A % B;
                Div.Insert(0, AdivB);
                if (AmodB == 0) break;
                A = B;
                B = AmodB;
            }

            for (int i = 1; i < Div.Count; i++)
            {
                x = y1;
                y = x1 - y1 * Div[i];
                x1 = x;
                y1 = y;
            }

            if (a < b)
            {
                A = b;
                B = a;
            }
            else
            {
                A = a;
                B = b;
            }

            Euclid_result result = new Euclid_result()
            {
                C = A * x + B * y,
                x = x,
                y = y
            };

            Div.Clear();
            return result;
        }

        public BigInteger get_inverse(BigInteger a, BigInteger b)
        {
            Euclid_result result = euclid(a, b);
            BigInteger c = result.y;
            if (c < 0)
                c = c + a;
            return c;
        }
    }
}
