using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;// подключить в ссылках файл .dll System.Data.SqlServerCe
using System.Data.SqlClient;
using System.Configuration;

namespace Cargo
{
    public partial class Form1 : Form
    {
        // В этот объект попадают и обрабатываются все таблицы
         DataSet ds;
        // Строка соединения с базой данных(Database-базаданных грузоперевозок) SQL

         string connectionString;
 
        // Через эти объекты происходит связывание таблиц в базе данных, см на их название. Т.е. сохранение, редактирование и тп.
         SqlDataAdapter daM_SOTRUDNIK;
         SqlDataAdapter daM_TRANSPORT;
         SqlDataAdapter daM_CLIENT;
         SqlDataAdapter daM_DOSTAVKA;
         SqlDataAdapter daM_TIP;
         SqlDataAdapter daM_GRUZ;
        
        // Т.к. у нас 6 таблиц - мы можем сделать 6 команд(которые управляют каждой из таблиц)
         SqlCommand[] commands = new SqlCommand[6];

        public Form1()
        {
            // Создаем объекты на форме
            InitializeComponent();

            // Создаем соединение с базой данных
            SqlConnection connect;
            // Открываем(активируем соединение)
            
            // Попытаемся соединиться
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Cargo.Properties.Settings.CargoConnectionString"].ConnectionString;
                connect = new SqlConnection(connectionString);
                connect.Open();
            }
            catch(Exception error)
            {
                // Если не получилось соединиться с базой данных - покажем, почему(система сама выдаст причину)
                MessageBox.Show(error.Message);
                return;
            }

            // Соединения с базой данных - назначаем команды для каждой из таблиц(выбор всех элементов каждой из сущностей)
            commands[0] = new SqlCommand("SELECT * from m_sotrydnik", connect);
            commands[1] = new SqlCommand("SELECT * from m_transport", connect);
            commands[2] = new SqlCommand("SELECT * from m_klient", connect);
            commands[3] = new SqlCommand("SELECT * from m_dostavka", connect);
            commands[4] = new SqlCommand("SELECT * from m_tip", connect);
            commands[5] = new SqlCommand("SELECT * from m_gruz", connect);

            // Создаем объект, в который загрузим все таблицы
            ds = new DataSet();

            // 1 Выгрузка из базы данных в датасет
            daM_SOTRUDNIK = new SqlDataAdapter(commands[0]);// Для каждого адаптера назначаем свою команду
            daM_SOTRUDNIK.FillSchema(ds, SchemaType.Source, "m_sotrydnik");// Заполняем схему для таблицы
            daM_SOTRUDNIK.Fill(ds, "m_sotrydnik");// Переносим таблицу из БД своеобразно в объект ds

            // Назначим ресурсы
            dataGridView1.DataSource = ds.Tables["m_sotrydnik"];
            // отмена генерации столбцов dataGridView - так просто всё правильно работает, информация из гугла
            dataGridView1.AutoGenerateColumns = false;


            // 2 Выгрузка из базы данных в датасет
            daM_TRANSPORT = new SqlDataAdapter(commands[1]);// все аналогично, см п.1
            daM_TRANSPORT.FillSchema(ds, SchemaType.Source, "m_transport");
            daM_TRANSPORT.Fill(ds, "m_transport");

            // Назначим ресурсы
            dataGridView2.DataSource = ds.Tables["m_transport"];
            // отмена генерации столбцов dataGridView
            dataGridView2.AutoGenerateColumns = false;


            // 3 Выгрузка из базы данных в датасет
            daM_CLIENT = new SqlDataAdapter(commands[2]);// все аналогично, см п.1
            daM_CLIENT.FillSchema(ds, SchemaType.Source, "m_klient");
            daM_CLIENT.Fill(ds, "m_klient");

            // Назначим ресурсы
            dataGridView3.DataSource = ds.Tables["m_klient"];
            // отмена генерации столбцов dataGridView
            dataGridView3.AutoGenerateColumns = false;


            // 4 Выгрузка из базы данных в датасет
            daM_DOSTAVKA = new SqlDataAdapter(commands[3]);// все аналогично, см п.1
            daM_DOSTAVKA.FillSchema(ds, SchemaType.Source, "m_dostavka");
            daM_DOSTAVKA.Fill(ds, "m_dostavka");

            // Назначим ресурсы
            dataGridView4.DataSource = ds.Tables["m_dostavka"];
            // отмена генерации столбцов dataGridView
            dataGridView4.AutoGenerateColumns = false;


            // 5 Выгрузка из базы данных в датасет
            daM_TIP = new SqlDataAdapter(commands[4]);// все аналогично, см п.1
            daM_TIP.FillSchema(ds, SchemaType.Source, "m_tip");
            daM_TIP.Fill(ds, "m_tip");

            // Назначим ресурсы
            dataGridView5.DataSource = ds.Tables["m_tip"];
            // отмена генерации столбцов dataGridView
            dataGridView5.AutoGenerateColumns = false;


            // 6 Выгрузка из базы данных в датасет
            daM_GRUZ = new SqlDataAdapter(commands[5]);
            daM_GRUZ.FillSchema(ds, SchemaType.Source, "m_gruz");
            daM_GRUZ.Fill(ds, "m_gruz");

            // Назначим ресурсы
            dataGridView6.DataSource = ds.Tables["m_gruz"];
            // отмена генерации столбцов dataGridView
            dataGridView6.AutoGenerateColumns = false;

            // Закрытие соединения
            connect.Close();


            // Установим ресурсы
            RefreshC();
            comboBox1.DataSource = passp_sotr;
            comboBox2.DataSource = passp_sotr;
        }

        List<string> passp_sotr = new List<string>();

        // Для загрузки IDшников в комбобоксы
        void RefreshC()
        {
            passp_sotr.Clear();
            // загрузим в комбобоксы реальные номера
            // выбор выбранного заказа и заполнение второго DataGridView
            DataRowCollection drs3 = ds.Tables["m_sotrydnik"].Rows;

            // последовательное заполнение информации о найденных записях 
            // в DataGridView для записей заказов
            foreach (DataRow dr in drs3)
            {
                passp_sotr.Add(dr["sotrydnik_number"].ToString());
            }

            comboBox1.Refresh();
            comboBox2.Refresh();
        }

        // Функция добавления объектов в таблицу и сохранения в базу данных - эту функцию мы потом везде вызываем
        public void AddToTableAndSave(SqlDataAdapter adapter, string tableName)
        {
            // создаем билдер - который связывает с таблицей все команды сохранения, Вставки и удаления
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            adapter.UpdateCommand = builder.GetUpdateCommand();// команда обновления таблицы в базе данных(т.к. в форме мы можем её изменить)
            adapter.InsertCommand = builder.GetInsertCommand();//  команда вставки в базу данных
            adapter.DeleteCommand = builder.GetDeleteCommand();//  команда удаления
            using (builder)// используя этот билдер мы обновляем нашу таблицу
            {
                adapter.Update(ds, tableName);// обновление таблицы
            }

            RefreshC();
        }

        // Добавление в таблицу сотрудников
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // переносим наши данные из формы для SOTR в переменные
                int id = int.Parse(textBox1.Text);
                string fio = textBox2.Text;
                DateTime date = DateTime.Parse(textBox9.Text);

                // берем таблицу сотруднков
                DataTable m_sotrydnik = ds.Tables["m_sotrydnik"];
                // добавляем новый объект
                m_sotrydnik.Rows.Add(new object[] { id, fio, date });

                // вызываем функцию сохранения изменений в базе данных
                AddToTableAndSave(daM_SOTRUDNIK, "m_sotrydnik");
            }
            catch(Exception ex)
            {
                // если мы ввели неверные данные или неправильно вставили - получаем сообщение с текстом ошибки
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление машин(транспортов)
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int pass = int.Parse(comboBox1.Text);
                string number = textBox13.Text;
                string marka = textBox10.Text;

                DataTable m_transport = ds.Tables["m_transport"];
                m_transport.Rows.Add(new object[] { pass, number, marka });

                AddToTableAndSave(daM_TRANSPORT, "m_transport");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Добавление клиентов
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(textBox21.Text);
                string addres = textBox20.Text;
                int skidka = int.Parse(textBox18.Text);

                DataTable m_klient = ds.Tables["m_klient"];
                m_klient.Rows.Add(new object[] { num, addres, skidka });

                AddToTableAndSave(daM_CLIENT, "m_klient");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление доставок
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int dost_n = int.Parse(textBox28.Text);
                DateTime dost_dz = DateTime.Parse(textBox27.Text);
                DateTime dost_dd = DateTime.Parse(textBox25.Text);
                decimal dost_s = decimal.Parse(textBox24.Text);
                int sotr_n = int.Parse(comboBox2.Text);
                int kl_n = int.Parse(textBox4.Text);

                DataTable m_dostavka = ds.Tables["m_dostavka"];
                m_dostavka.Rows.Add(new object[] { dost_n, dost_dz, dost_dd, dost_s, sotr_n, kl_n });

                AddToTableAndSave(daM_DOSTAVKA, "m_dostavka");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление поездок
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox32.Text);
                string nazv = textBox31.Text;

                DataTable m_tip = ds.Tables["m_tip"];
                m_tip.Rows.Add(new object[] { id, nazv });

                AddToTableAndSave(daM_TIP, "m_tip");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление грузов
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                int gruz_num = int.Parse(textBox33.Text);
                int dost_num = int.Parse(textBox34.Text);
                int t_n = int.Parse(textBox5.Text);

                DataTable m_gruz = ds.Tables["m_gruz"];
                m_gruz.Rows.Add(new object[] {gruz_num,dost_num,t_n});

                AddToTableAndSave(daM_GRUZ, "m_gruz");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Обработчики событий кнопок удаления:
        // Удаление строки из таблицы  сотрудников
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)// если выбранная строка не пуста
                ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Delete();// удаляем заданную строку
        }
        // Удаление  строки из таблицы транспортов-машин
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
                ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление строки из таблицы клиентов
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null)
                ((DataRowView)dataGridView3.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление  строки из таблицы  доставок
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow != null)
                ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление  строки из таблицы поездок
        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow != null)
                ((DataRowView)dataGridView5.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление  строки из таблицы грузов
        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow != null)
                ((DataRowView)dataGridView6.CurrentRow.DataBoundItem).Delete();
        }

        // Полное сохранение при закрытии - вызывается для каждой из таблиц. Мало ли какие изменения мы не внесли.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                AddToTableAndSave(daM_SOTRUDNIK, "m_sotrydnik");
                AddToTableAndSave(daM_TRANSPORT, "m_transport");
                AddToTableAndSave(daM_CLIENT, "m_klient");
                AddToTableAndSave(daM_DOSTAVKA, "m_dostavka");
                AddToTableAndSave(daM_TIP, "m_tip");
                AddToTableAndSave(daM_GRUZ, "m_gruz");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
