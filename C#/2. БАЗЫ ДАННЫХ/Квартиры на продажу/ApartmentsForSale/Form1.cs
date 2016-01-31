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

namespace ApartmentsForSale
{
    public partial class Form1 : Form
    {
        // Таблицы
        //OwnerP
        //Buyer
        //Rielt
        //Purchse
        //RealEstate
        //OwnerRealEstate

        // В этот объект попадают и обрабатываются все таблицы
        static DataSet ds;
        // Строка соединения с базой данных(Database-базаданных продаж квартир) SQL

        static string connectionString;
 
        // Через эти объекты происходит связывание таблиц в базе данных, см на их название. Т.е. сохранение, редактирование и тп.
        static SqlDataAdapter daOWNERP;
        static SqlDataAdapter daBUYER;
        static SqlDataAdapter daRIELT;
        static SqlDataAdapter daPURCHSE;
        static SqlDataAdapter daREALESTATE;
        static SqlDataAdapter daOWNERREALESTATE;
        
        // Т.к. у нас 6 таблиц - мы можем сделать 6 команд(которые управляют каждой из таблиц)
        static SqlCommand[] commands = new SqlCommand[6];

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
                connectionString = ConfigurationManager.ConnectionStrings["ApartmentsForSale.Properties.Settings.APARTMENTSFORSALEConnectionString"].ConnectionString;
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
            commands[0] = new SqlCommand("SELECT * from OwnerP", connect);
            commands[1] = new SqlCommand("SELECT * from Buyer", connect);
            commands[2] = new SqlCommand("SELECT * from Rielt", connect);
            commands[3] = new SqlCommand("SELECT * from Purchse", connect);
            commands[4] = new SqlCommand("SELECT * from RealEstate", connect);
            commands[5] = new SqlCommand("SELECT * from OwnerRealEstate", connect);

            // Создаем объект, в который загрузим все таблицы
            ds = new DataSet();

            // 1 Выгрузка из базы данных в датасет для таблицы владельцев
            daOWNERP = new SqlDataAdapter(commands[0]);// Для каждого адаптера назначаем свою команду
            daOWNERP.FillSchema(ds, SchemaType.Source, "OwnerP");// Заполняем схему для таблицы
            daOWNERP.Fill(ds, "OwnerP");// Переносим таблицу из БД своеобразно в объект ds

            // Назначим ресурсы
            dataGridView1.DataSource = ds.Tables["OwnerP"];
            // отмена генерации столбцов dataGridView - так просто всё правильно работает, информация из гугла
            dataGridView1.AutoGenerateColumns = false;


            // 2 Выгрузка из базы данных в датасет для таблицы покупателей
            daBUYER = new SqlDataAdapter(commands[1]);// все аналогично, см п.1
            daBUYER.FillSchema(ds, SchemaType.Source, "Buyer");
            daBUYER.Fill(ds, "Buyer");

            // Назначим ресурсы
            dataGridView2.DataSource = ds.Tables["Buyer"];
            // отмена генерации столбцов dataGridView
            dataGridView2.AutoGenerateColumns = false;


            // 3 Выгрузка из базы данных в датасет для таблицы риелторов
            daRIELT = new SqlDataAdapter(commands[2]);// все аналогично, см п.1
            daRIELT.FillSchema(ds, SchemaType.Source, "Rielt");
            daRIELT.Fill(ds, "Rielt");

            // Назначим ресурсы
            dataGridView3.DataSource = ds.Tables["Rielt"];
            // отмена генерации столбцов dataGridView
            dataGridView3.AutoGenerateColumns = false;


            // 4 Выгрузка из базы данных в датасет
            daPURCHSE = new SqlDataAdapter(commands[3]);// все аналогично, см п.1
            daPURCHSE.FillSchema(ds, SchemaType.Source, "Purchse");
            daPURCHSE.Fill(ds, "Purchse");

            // Назначим ресурсы
            dataGridView4.DataSource = ds.Tables["Purchse"];
            // отмена генерации столбцов dataGridView
            dataGridView4.AutoGenerateColumns = false;


            // 5 Выгрузка из базы данных в датасет
            daREALESTATE = new SqlDataAdapter(commands[4]);// все аналогично, см п.1
            daREALESTATE.FillSchema(ds, SchemaType.Source, "RealEstate");
            daREALESTATE.Fill(ds, "RealEstate");

            // Назначим ресурсы
            dataGridView5.DataSource = ds.Tables["RealEstate"];
            // отмена генерации столбцов dataGridView
            dataGridView5.AutoGenerateColumns = false;


            // 6 Выгрузка из базы данных в датасет
            daOWNERREALESTATE = new SqlDataAdapter(commands[5]);
            daOWNERREALESTATE.FillSchema(ds, SchemaType.Source, "OwnerRealEstate");
            daOWNERREALESTATE.Fill(ds, "OwnerRealEstate");

            // Назначим ресурсы
            dataGridView6.DataSource = ds.Tables["OwnerRealEstate"];
            // отмена генерации столбцов dataGridView
            dataGridView6.AutoGenerateColumns = false;

            // Закрытие соединения
            connect.Close();
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
        }

        // Добавление в таблицу владельцев
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // переносим наши данные из формы в переменные
                string passpD = textBox1.Text;
                string fio = textBox2.Text;
                string telN = textBox9.Text;

                // берем таблицу сотруднков
                DataTable OwnerP = ds.Tables["OwnerP"];
                // добавляем новый объект
                OwnerP.Rows.Add(new object[] { passpD, fio, telN });

                // вызываем функцию сохранения изменений в базе данных
                AddToTableAndSave(daOWNERP, "OwnerP");
            }
            catch(Exception ex)
            {
                // если мы ввели неверные данные или неправильно вставили - получаем сообщение с текстом ошибки
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление покупателей
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string passpD = textBox14.Text;
                string fio = textBox13.Text;
                string telN = textBox10.Text;

                DataTable Buyer = ds.Tables["Buyer"];
                Buyer.Rows.Add(new object[] { passpD, fio, telN });

                AddToTableAndSave(daBUYER, "Buyer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Добавление риелторов
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int regN = int.Parse(textBox21.Text);
                string FIO = textBox20.Text;
                string contD = textBox18.Text;
                string qual = textBox6.Text;

                DataTable Rielt = ds.Tables["Rielt"];
                Rielt.Rows.Add(new object[] { regN, FIO, contD,qual });

                AddToTableAndSave(daRIELT, "Rielt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление покупок-продаж
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int cont = int.Parse(textBox28.Text);
                DateTime date = DateTime.Parse(textBox27.Text);
                int amount = int.Parse(textBox25.Text);
                string passpD = textBox24.Text;
                int regN = int.Parse(textBox3.Text);

                DataTable Purchse = ds.Tables["Purchse"];
                Purchse.Rows.Add(new object[] { cont,date,amount,passpD,regN});

                AddToTableAndSave(daPURCHSE, "Purchse");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление недвижимостей
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int regN = int.Parse(textBox32.Text);
                string addr = textBox4.Text;
                int cost = int.Parse(textBox7.Text);
                string typ = textBox31.Text;
                string cont = textBox8.Text;

                DataTable RealEstate = ds.Tables["RealEstate"];
                RealEstate.Rows.Add(new object[] { regN,addr,cost,typ,cont });

                AddToTableAndSave(daREALESTATE, "RealEstate");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление связей Владелец-Недвижимость
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string passpD = textBox33.Text;
                int regN = int.Parse(textBox5.Text);

                DataTable OwnerRealEstate = ds.Tables["OwnerRealEstate"];
                OwnerRealEstate.Rows.Add(new object[] { passpD, regN });

                AddToTableAndSave(daOWNERREALESTATE, "OwnerRealEstate");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Обработчики событий кнопок удаления:
        // Удаление строки из таблицы владельцев 
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)// если выбранная строка не пуста
                ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Delete();// удаляем заданную строку
        }
        // Удаление  строки из таблицы покупателей
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
                ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление строки из таблицы риелторов
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null)
                ((DataRowView)dataGridView3.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление  строки из таблицы  покупок
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow != null)
                ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление  строки из таблицы недвижимостей
        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow != null)
                ((DataRowView)dataGridView5.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление  строки из таблицы связей владельцев и недвижимостей
        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow != null)
                ((DataRowView)dataGridView6.CurrentRow.DataBoundItem).Delete();
        }

        // Полное сохранение при закрытии - вызывается для каждой из таблиц. Мало ли какие изменения мы не внесли.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AddToTableAndSave(daOWNERP, "OwnerP");
            AddToTableAndSave(daBUYER, "Buyer");
            AddToTableAndSave(daRIELT, "Rielt");
            AddToTableAndSave(daPURCHSE, "Purchse");
            AddToTableAndSave(daREALESTATE, "RealEstate");
            AddToTableAndSave(daOWNERREALESTATE, "OwnerRealEstate");
        }
    }
}
