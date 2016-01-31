using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Shop
{
    // Окно авторизации пользователя.
    public partial class AuthorizationWindow : Form
    {
        public AuthorizationWindow()
        {
            InitializeComponent();

            database = new Model();
        }

        // Модель, с помощью которой соединяем с базой данных
        Model database;

        // Кнопка авторизации
        private void authorizeButton_Click(object sender, EventArgs e)
        {
            string pass = passwordTextBox.Text;
            string login = loginTextBox.Text;

            // Первый пользователь - всегда будет админом
            if(!database.Users.Any())
            {
                var user = new User
                {
                    UserName = login,
                    Password = pass,
                    UserInformation = "",
                    IsAdmin = true
                };

                database.Users.Add(user);
                database.SaveChanges();

                var thisUser = database.Users.Where(x => x.Password == pass && x.UserName == login).First();

                // И откроем ему панель администратора
                MainAdminWindow maw = new MainAdminWindow(thisUser);
                maw.Show();

                // А это окно - закроем
                this.Hide();
            }
            else
            {
                var u = database.Users.ToList();
                // Проверим, есть ли уже такой пользователь
                if(database.Users.Where(x=>x.Password==pass && x.UserName==login).Count()==1)
                {
                    var user = database.Users.Where(x => x.Password == pass && x.UserName == login).First();
                    // Если это админ - то открываем админское окно

                    if(user.IsAdmin)
                    {
                        // И откроем ему панель администратора
                        MainAdminWindow maw = new MainAdminWindow(user);
                        maw.Show();

                        // А это окно - закроем
                        this.Hide();
                        return;
                    }

                    // если есть такой пользователь - то пропускаем его в окно клиента
                    MainClientWindow mcw = new MainClientWindow(user);
                    mcw.Show();
                    this.Hide();
                }
                else
                {
                    // Иначе говорим, что такого нет и просим зарегистрироваться
                    MessageBox.Show("Такой пользователь отсутствует, пожалуйста, зарегистрируйтесь!");
                }
            }
        }

        // Если пользователь не существует - создается новый пользователь(регистрация)
        private void registrationButton_Click(object sender, EventArgs e)
        {
            string pass = passwordTextBox.Text;
            string login = loginTextBox.Text;

            // проверим на правильный ввод
            if(pass=="" || login=="")
            {
                MessageBox.Show("Поле не может быть пустым!");
                return;
            }

            // Не сущетсвует ли уже такой же пользователь?
            if(database.Users.Where(x=>x.UserName == login).Any())
            {
                MessageBox.Show("Такой пользователь уже существует!");
                return;
            }

            var user = new User
            {
                UserName = login,
                Password = pass,
                UserInformation = ""
            };

            database.Users.Add(user);
            database.SaveChanges();

            MessageBox.Show("Успешная регистрация!");

            // И откроем ему окно пользователя
            user = database.Users.Where(x => x.Password == pass && x.UserName == login).First();
            MainClientWindow mcw = new MainClientWindow(user);
            mcw.Show();

            // А это окно - закроем
            this.Hide();
        }
    }

    
}
