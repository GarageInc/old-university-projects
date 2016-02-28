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
    // Структура для хранения преобразований Евклида
    public struct Euclid_result
    {
        public BigInteger C;
        public BigInteger x;
        public BigInteger y;
    }

    public partial class Form1 : Form
    {
        bool running = false; //запущен ли сервер

        IPAddress host; //IP сервера
        int port; //порт сервера

        Socket socket; // сокет для прослушивания
        IPEndPoint point; // информация о точке (ип, порт)

        private List<Thread> threads = new List<Thread>(); // лист для потоков

        BigInteger DHkey;// общий ключ по Диффи Хеллману
        BigInteger P, Q, N, FI, E, D;

        // Список пользователей
        List<User> users = new List<User>();
        List<Socket> clients = new List<Socket>(); // клиент


        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += Application_ApplicationExit; //указываем, что при закрытии программы будет вызываться ивент
        }

        void Application_ApplicationExit(object sender, EventArgs e) //ивент закрытия программы
        {
            if (running) {
                socket.Close();
            } //если порт был привязан, то прекращаем слушание
            running = false;

            for (int i = 0; i < threads.Count(); i++) //перебираем весь список потоков
            {
                if (threads[i].ThreadState == ThreadState.Running) //если какой-то из потоков запущен
                {
                    threads[i].Abort(); //то закрываем его
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            port = 10000; // назначаем порт
            host = IPAddress.Parse("127.0.0.1");
        }

        // Хэндлер кнопки запуска сервера
        private void btnTurn_Click(object sender, EventArgs e)
        {
            if ( !running )
            {
                // создаем сокет для прослушки
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                point = new IPEndPoint(IPAddress.Any, port);

                socket.Bind(point); // связываем сокет с точкой
                socket.Listen(300); // начинаем прослушивать, макс. 300

                btnTurn.Text = "Остановка сервера";
                trace("Сервер запущен..."); 

                running = true; 

                AcceptSocket(); // начинаем принимать сокеты
            }
            else
            {
                btnTurn.Text = "Запуск сервера";
                trace("Сервер остановлен...");

                running = false;
                socket.Close();

                // закрываем и очищаем потоки
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
        
        // функция прослушки юзеров
        private void AcceptSocket()
        {
            Thread th = new Thread(delegate () //создаем новый поток для прослушки
            {
                while ( running ) // делаем, пока сервер запущен
                {
                    trace("ACCEPTING");
                    Socket temp = socket.Accept();// Появилось новое соединение
                    trace("ACCEPTED");

                    Thread subth = new Thread(delegate () //создаем новый поток для прослушки
                    {
                        bool isLoginned = false;
                        while ( !isLoginned )
                        {
                            string login, password, res; // временные переменные

                            res = ReceiveString(temp); //конвертируем байты в "путь" (регистр или авторизац)

                            if (res[0] == 'R') //если "путь" будет регистрация, то
                            {
                                generateKey(temp);

                                txtLog.Invoke(new Action(() => {
                                    trace("При регистрации был сгенерирован ключ: " + DHkey.ToString());
                                }));

                                login = RC4(ReceiveString(temp), DHkey.ToString()); //конвертируем байты в логин
                                password = RC4(ReceiveString(temp), DHkey.ToString()); //конвертируем байты в пароль

                                User user = this.users.Where(x => x.login == login).FirstOrDefault();

                                if (user != null) // если такой логин уже содержится
                                {
                                    SendBytes(temp, "0");  //то отправляем клиенту сообщение о том, что такой логин уже есть
                                }
                                else
                                {
                                    users.Add(new User(login, password));
                                    SendBytes(temp, "1");// отправляем клиенту сообщение о том, что регистрация прошла успешно
                                    this.txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " зарегистрировался.\r\n"); }));
                                }
                            }
                            else if (res[0] == 'I') // иначе, если вход, то
                            {


                                password = "pswd";
                                bool exists = true;
                                login = ReceiveString(temp); //конвертируем байты в логин

                                User user = this.users.Where(x => x.login == login).FirstOrDefault();

                                if (user != null)
                                {
                                    password = user.password;
                                }
                                else
                                {
                                    exists = false;
                                }

                                Random rnd = new Random();
                                BigInteger n = Function.num_gen(10);

                                SendBytes(temp, n.ToString());

                                string hash1 = ReceiveString(temp);
                                string hash2 = Function.calcHash(password + n);

                                generateKey(temp);

                                generateRSA();

                                // Отправляем открытые ключи
                                SendBytes(temp, N.ToString());
                                SendBytes(temp, E.ToString());

                                trace("EXISTS: " + exists);
                                if (exists == false || hash1 != hash2)
                                {

                                    SendBytes(temp, "0");// Ошибка
                                }
                                else
                                {
                                    isLoginned = true;

                                    clients.Add(temp);

                                    txtLog.Invoke(new Action(() => {
                                        trace("При авторизации был сгенерирован ключ: " + DHkey.ToString());
                                    }));

                                    // Отправляем клиенту сообщение о том, что пользователь успешно вошел
                                    SendBytes(temp, "1");

                                    this.txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " вошел в сеть\r\n"); }));

                                    // И создаем поток для принятия от юзера дальнейших сообщений
                                    Thread thh = new Thread(delegate ()
                                    {
                                        try
                                        {
                                            // ждем сообщение от пользователя
                                            string m = RC4(ReceiveString(temp), DHkey.ToString()); ;

                                            while (m != "E")
                                            {
                                                trace(">user----");
                                                //получили от юзера сообщение m
                                                string M;

                                                if (!running)
                                                {
                                                    M = "Сервер недоступен!\r\n";
                                                    trace(M);
                                                    SendBytes(temp, RC4(M, DHkey.ToString()));
                                                }

                                                if ((running) && (m != "E"))
                                                {
                                                    M = "Cервер получил сообщение: " + m;
                                                    trace(M);
                                                    SendBytes(temp, RC4(M, DHkey.ToString()));

                                                    BigInteger EDS = new BigInteger(Function.getBytesFromStr(M));// ЭЦП
                                                    SendBytes(temp, RC4(BigInteger.ModPow(EDS, D, N).ToString(), DHkey.ToString()));//отправляем ЭЦП

                                                    this.txtUsers.Invoke(new Action(() => {
                                                        txtUsers.AppendText( login + ":\r\n" + m + "\r\n");
                                                    }));
                                                }
                                                else
                                                    txtUsers.Invoke(
                                                   new Action(() => {
                                                       txtUsers.AppendText("Пользователь " + login + " вышел из сети\r\n");
                                                   }));

                                                m = RC4(ReceiveString(temp), DHkey.ToString());
                                            }
                                        }
                                        catch
                                        {
                                            txtUsers.Invoke(new Action(() => { txtUsers.AppendText("Пользователь " + login + " вышел из сети\r\n"); }));
                                        }
                                    });

                                    thh.Start(); // запускаем этот поток
                                    threads.Add(thh);
                                }
                            }
                            else
                            {
                                trace("LISTENING: pass");
                                // pass, в ином случае не реагировать на входящие данные
                            }
                        }                        
                    });

                    subth.Start(); // запускаем этот поток
                    threads.Add(subth);

                }
            });

            th.Start(); // запускаем весь предыдущий поток
            threads.Add(th); // Добавляем поток в список потоков
        }

        public void trace(string message)
        {
            txtLog.Invoke(new Action(() => {
                txtLog.AppendText(message + "\r\n");
            }));
        }

        string ReceiveString(Socket client) //функция получения байтов
        { 
            try
            {
                trace("-> Begin reading bytes...");
                byte[] bytes = new byte[1024];

                int bytesRec = client.Receive(bytes);
                string data = Encoding.Unicode.GetString(bytes, 0, bytesRec);
                
                trace(" = Read bytes:" + data);
                return data; //возвращаем байты из функции
            } catch(Exception e)
            {
                trace("ERROR: " + e.Message);
                return "0";
            }
           
        }

        void SendBytes(Socket client, string message) //функция отправки байтов
        {
            try
            {
                trace("-> Begin sending bytes...");
                
                byte[] bytes = Encoding.Unicode.GetBytes(message); //инкодим стринг в байты
                
                client.Send(bytes); //отправляем строку

                trace(" = Bytes sended...");

            } catch(Exception e)
            {
                trace("ERROR:" + e.Message);
            }
        }
        
        public void generateKey(Socket temp)
        {
            trace("Generating key...");
            BigInteger n = Function.num_gen(8);
            SendBytes(temp, n.ToString());

            BigInteger q = Function.num_gen(8);
            SendBytes(temp, q.ToString());

            BigInteger y = Function.num_gen(7);
            BigInteger K = Function.PowMod(q, y, n);
            BigInteger M = BigInteger.Parse( ReceiveString(temp));

            SendBytes(temp, K.ToString());

            DHkey = Function.PowMod(M, y, n);
            trace("Key generated!");
        }

        public void generateRSA()
        {
            P = Function.num_gen(100);
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

    class User
    {
        public string login;
        public string password;

        public User(string l, string p)
        {
            login = l;
            password = p;
        }
    }
}
