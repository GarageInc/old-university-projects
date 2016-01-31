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

namespace Library
{
    public partial class Form1 : Form
    {
        // В этот объект попадают и обрабатываются все таблицы
        static DataSet ds;
        // Строка соединения с базой данных(Library-библиотека) SQL
        static string connectionString = ConfigurationManager.ConnectionStrings["Library.Properties.Settings.LibraryConnectionString"].ConnectionString;
 
        // Через эти объекты происходит связывание таблиц в базе данных, см на их название. Т.е. сохранение, редактирование и тп.
        static SqlDataAdapter daSOTR;
        static SqlDataAdapter daAUTHOR;
        static SqlDataAdapter daBOOK;
        static SqlDataAdapter daAUTHORBOOK;
        static SqlDataAdapter daSCHOOL;
        static SqlDataAdapter daAGREEMENT;
        static SqlDataAdapter daAGREEMENTNBOOK;
        
        // Т.к. у нас 7 таблиц - мы можем сделать 7 команд(которые управляют каждой из таблиц)
        static SqlCommand[] commands = new SqlCommand[7];

        public Form1()
        {
            // Создаем объекты на форме
            InitializeComponent();

            // Создаем соединение с базой данных
            SqlConnection connect = new SqlConnection(connectionString);
            // Открываем(активируем соединение)
            connect.Open();

            // Соединения с базой данных - назначаем команды для каждой из таблиц(выбор всех элементов каждой из сущностей)
            commands[0] = new SqlCommand("SELECT * from SOTR", connect);
            commands[1] = new SqlCommand("SELECT * from AUTHOR", connect);
            commands[2] = new SqlCommand("SELECT * from BOOK", connect);
            commands[3] = new SqlCommand("SELECT * from AUTHORBOOK", connect);
            commands[4] = new SqlCommand("SELECT * from SCHOOL", connect);
            commands[5] = new SqlCommand("SELECT * from AGREEMENT", connect);
            commands[6] = new SqlCommand("SELECT * from AGREEMENTNBOOK", connect);

            // Создаем объект, в который загрузим все таблицы
            ds = new DataSet();

            // 1 Выгрузка из базы данных в датасет
            daSOTR = new SqlDataAdapter(commands[0]);// Для каждого адаптера назначаем свою команду
            daSOTR.FillSchema(ds, SchemaType.Source, "SOTR");// Заполняем схему для таблицы
            daSOTR.Fill(ds, "SOTR");// Переносим таблицу из БД своеобразно в объект ds

            // Назначим ресурсы для SOTR
            dataGridView1.DataSource = ds.Tables["SOTR"];
            // отмена генерации столбцов dataGridView - так просто всё правильно работает, информация из гугла
            dataGridView1.AutoGenerateColumns = false;


            // 2 Выгрузка из базы данных в датасет
            daAUTHOR = new SqlDataAdapter(commands[1]);// все аналогично, см п.1
            daAUTHOR.FillSchema(ds, SchemaType.Source, "AUTHOR");
            daAUTHOR.Fill(ds, "AUTHOR");

            // Назначим ресурсы для AUTHOR
            dataGridView2.DataSource = ds.Tables["AUTHOR"];
            // отмена генерации столбцов dataGridView
            dataGridView2.AutoGenerateColumns = false;


            // 3 Выгрузка из базы данных в датасет
            daBOOK = new SqlDataAdapter(commands[2]);// все аналогично, см п.1
            daBOOK.FillSchema(ds, SchemaType.Source, "BOOK");
            daBOOK.Fill(ds, "BOOK");

            // Назначим ресурсы для BOOK
            dataGridView3.DataSource = ds.Tables["BOOK"];
            // отмена генерации столбцов dataGridView
            dataGridView3.AutoGenerateColumns = false;


            // 4 Выгрузка из базы данных в датасет
            daAUTHORBOOK = new SqlDataAdapter(commands[3]);// все аналогично, см п.1
            daAUTHORBOOK.FillSchema(ds, SchemaType.Source, "AUTHORBOOK");
            daAUTHORBOOK.Fill(ds, "AUTHORBOOK");

            // Назначим ресурсы для AUTHORBOOK
            dataGridView4.DataSource = ds.Tables["AUTHORBOOK"];
            // отмена генерации столбцов dataGridView
            dataGridView4.AutoGenerateColumns = false;


            // 5 Выгрузка из базы данных в датасет
            daSCHOOL = new SqlDataAdapter(commands[4]);// все аналогично, см п.1
            daSCHOOL.FillSchema(ds, SchemaType.Source, "SCHOOL");
            daSCHOOL.Fill(ds, "SCHOOL");

            // Назначим ресурсы для SCHOOL
            dataGridView5.DataSource = ds.Tables["SCHOOL"];
            // отмена генерации столбцов dataGridView
            dataGridView5.AutoGenerateColumns = false;


            // 6 Выгрузка из базы данных в датасет
            daAGREEMENT = new SqlDataAdapter(commands[5]);
            daAGREEMENT.FillSchema(ds, SchemaType.Source, "AGREEMENT");
            daAGREEMENT.Fill(ds, "AGREEMENT");

            // Назначим ресурсы для AGREEMENT
            dataGridView6.DataSource = ds.Tables["AGREEMENT"];
            // отмена генерации столбцов dataGridView
            dataGridView6.AutoGenerateColumns = false;


            // 7 Выгрузка из базы данных в датасет
            daAGREEMENTNBOOK = new SqlDataAdapter(commands[6]);
            daAGREEMENTNBOOK.FillSchema(ds, SchemaType.Source, "AGREEMENTNBOOK");
            daAGREEMENTNBOOK.Fill(ds, "AGREEMENTNBOOK");

            // Назначим ресурсы для AGREEMENT
            dataGridView7.DataSource = ds.Tables["AGREEMENTNBOOK"];
            // отмена генерации столбцов dataGridView
            dataGridView7.AutoGenerateColumns = false;

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

        // Добавление в SOTR
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // переносим наши данные из формы для SOTR в переменные
                int id = int.Parse(textBox1.Text);
                string fio = textBox2.Text;
                char gender = char.Parse(comboBox1.Text);
                int tel = int.Parse(textBox4.Text);
                char educ = char.Parse(comboBox2.Text);
                int sal = int.Parse(textBox6.Text);
                Decimal p1 = Decimal.Parse(textBox7.Text);
                string p2 = textBox8.Text;
                string post = textBox9.Text;

                // берем таблицу сотруднков
                DataTable dtSOTR = ds.Tables["SOTR"];
                // добавляем новый объект
                dtSOTR.Rows.Add(new object[] {id,fio,gender,tel,educ,sal,p1,p2,post});

                // вызываем функцию сохранения изменений в базе данных
                AddToTableAndSave(daSOTR, "SOTR");
            }
            catch(Exception ex)
            {
                // если мы ввели неверные данные или неправильно вставили - получаем сообщение с текстом ошибки
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление авторов - всё так же, как и добавление сотрудников
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox14.Text);
                string fio = textBox13.Text;
                decimal p1 = decimal.Parse(textBox10.Text);
                string p2 = textBox8.Text;
                decimal a1 = decimal.Parse(textBox12.Text);
                string a2 = textBox11.Text;
                string info = textBox3.Text;

                DataTable dtAUTHOR = ds.Tables["AUTHOR"];
                dtAUTHOR.Rows.Add(new object[] { id, fio, p1, p2, a1, a2, info });

                AddToTableAndSave(daAUTHOR, "AUTHOR");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Добавление книг
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox21.Text);
                string name = textBox20.Text;
                decimal pr = decimal.Parse(textBox18.Text);
                decimal circ = decimal.Parse(textBox17.Text);
                decimal date = decimal.Parse(textBox15.Text);
                decimal c = decimal.Parse(textBox19.Text);
                decimal am = decimal.Parse(textBox16.Text);
                
                DataTable dtBOOK = ds.Tables["BOOK"];
                dtBOOK.Rows.Add(new object[] { id, name, pr, circ, date, c, am });

                AddToTableAndSave(daBOOK, "BOOK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление авторов-книг
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                decimal id_ab = decimal.Parse(textBox28.Text);
                decimal id_b = decimal.Parse(textBox27.Text);
                decimal noc = decimal.Parse(textBox25.Text);
                decimal date = decimal.Parse(textBox24.Text);

                DataTable dtAUTHORBOOK = ds.Tables["AUTHORBOOK"];
                dtAUTHORBOOK.Rows.Add(new object[] { id_ab, id_b, noc, date });

                AddToTableAndSave(daAUTHORBOOK, "AUTHORBOOK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление школ
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox32.Text);
                string name = textBox31.Text;
                string info = textBox29.Text;
                decimal a1 = decimal.Parse(textBox26.Text);
                string a2 = textBox22.Text;
                decimal t = decimal.Parse(textBox30.Text);
                decimal c = decimal.Parse(textBox23.Text);

                DataTable dtSCHOOL = ds.Tables["SCHOOL"];
                dtSCHOOL.Rows.Add(new object[] { id, name, info, a1, a2, t, c});

                AddToTableAndSave(daSCHOOL, "SCHOOL");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление соглашений
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                decimal id = decimal.Parse(textBox33.Text);
                DateTime d = DateTime.Parse(textBox34.Text);
                char t = char.Parse(comboBox3.Text);
                decimal sum = decimal.Parse(textBox35.Text);
                decimal dead = decimal.Parse(textBox41.Text);
                decimal id_sotr = decimal.Parse(textBox36.Text);
                decimal id_school = decimal.Parse(textBox37.Text);

                DataTable dtAGREEMENT = ds.Tables["AGREEMENT"];
                dtAGREEMENT.Rows.Add(new object[] { id, d, t, sum, dead, id_sotr, id_school });

                AddToTableAndSave(daAGREEMENT, "AGREEMENT");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление соглашений-книг
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                decimal id_ab = decimal.Parse(textBox42.Text);
                decimal id_b = decimal.Parse(textBox40.Text);
                decimal noc = decimal.Parse(textBox39.Text);

                DataTable dtAGREEMENTNBOOK = ds.Tables["AGREEMENTNBOOK"];
                dtAGREEMENTNBOOK.Rows.Add(new object[] { id_ab, id_b, noc });

                AddToTableAndSave(daAGREEMENTNBOOK, "AGREEMENTNBOOK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Удаление сотрудников
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)// если выбранная строка не пуста
                ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Delete();// удаляем заданную строку
        }
        // Удаление авторов
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
                ((DataRowView)dataGridView2.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление книг
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null)
                ((DataRowView)dataGridView3.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление авторов и книг
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow != null)
                ((DataRowView)dataGridView4.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление школ
        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow != null)
                ((DataRowView)dataGridView5.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление соглашений
        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow != null)
                ((DataRowView)dataGridView6.CurrentRow.DataBoundItem).Delete();
        }
        // Удаление соглашений-книг
        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView7.CurrentRow != null)
                ((DataRowView)dataGridView7.CurrentRow.DataBoundItem).Delete();
        }

        // Полное сохранение при закрытии - вызывается для каждой из таблиц. Мало ли какие изменения мы не внесли.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AddToTableAndSave(daSOTR, "SOTR");
            AddToTableAndSave(daAUTHOR, "AUTHOR");
            AddToTableAndSave(daBOOK, "BOOK");
            AddToTableAndSave(daAUTHORBOOK, "AUTHORBOOK");
            AddToTableAndSave(daSCHOOL, "SCHOOL");
            AddToTableAndSave(daAGREEMENT, "AGREEMENT");
            AddToTableAndSave(daAGREEMENTNBOOK, "AGREEMENTNBOOK");
        }
    }
}
