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
 
        static SqlDataAdapter daSOTR;
        static SqlDataAdapter daAUTHOR;
        static SqlDataAdapter daBOOK;
        static SqlDataAdapter daAUTHORBOOK;
        static SqlDataAdapter daSCHOOL;
        static SqlDataAdapter daAGREEMENT;
        static SqlDataAdapter daAGREEMENTNBOOK;
        
        static SqlCommand[] commands = new SqlCommand[7];

        public Form1()
        {
            InitializeComponent();
            
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            // Соединения с базой данных
            commands[0] = new SqlCommand("SELECT * from SOTR", connect);
            commands[1] = new SqlCommand("SELECT * from AUTHOR", connect);
            commands[2] = new SqlCommand("SELECT * from BOOK", connect);
            commands[3] = new SqlCommand("SELECT * from AUTHORBOOK", connect);
            commands[4] = new SqlCommand("SELECT * from SCHOOL", connect);
            commands[5] = new SqlCommand("SELECT * from AGREEMENT", connect);
            commands[6] = new SqlCommand("SELECT * from AGREEMENTNBOOK", connect); 

            ds = new DataSet();

            // 1 Выгрузка из базы данных в датасет
            daSOTR = new SqlDataAdapter(commands[0]);
            daSOTR.FillSchema(ds, SchemaType.Source, "SOTR");
            daSOTR.Fill(ds, "SOTR");

            // Назначим ресурсы для SOTR
            dataGridView1.DataSource = ds.Tables["SOTR"];
            // отмена генерации столбцов dataGridView
            dataGridView1.AutoGenerateColumns = false;


            // 2 Выгрузка из базы данных в датасет
            daAUTHOR = new SqlDataAdapter(commands[1]);
            daAUTHOR.FillSchema(ds, SchemaType.Source, "AUTHOR");
            daAUTHOR.Fill(ds, "AUTHOR");

            // Назначим ресурсы для AUTHOR
            dataGridView2.DataSource = ds.Tables["AUTHOR"];
            // отмена генерации столбцов dataGridView
            dataGridView2.AutoGenerateColumns = false;


            // 3 Выгрузка из базы данных в датасет
            daBOOK = new SqlDataAdapter(commands[2]);
            daBOOK.FillSchema(ds, SchemaType.Source, "BOOK");
            daBOOK.Fill(ds, "BOOK");

            // Назначим ресурсы для BOOK
            dataGridView3.DataSource = ds.Tables["BOOK"];
            // отмена генерации столбцов dataGridView
            dataGridView3.AutoGenerateColumns = false;


            // 4 Выгрузка из базы данных в датасет
            daAUTHORBOOK = new SqlDataAdapter(commands[3]);
            daAUTHORBOOK.FillSchema(ds, SchemaType.Source, "AUTHORBOOK");
            daAUTHORBOOK.Fill(ds, "AUTHORBOOK");

            // Назначим ресурсы для AUTHORBOOK
            dataGridView4.DataSource = ds.Tables["AUTHORBOOK"];
            // отмена генерации столбцов dataGridView
            dataGridView4.AutoGenerateColumns = false;


            // 5 Выгрузка из базы данных в датасет
            daSCHOOL = new SqlDataAdapter(commands[4]);
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

        // Добавление в SOTR
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);
                string fio = textBox2.Text;
                char gender = char.Parse(comboBox1.Text);
                int tel = int.Parse(textBox4.Text);
                char educ = char.Parse(comboBox2.Text);
                int sal = int.Parse(textBox6.Text);
                Decimal p1 = Decimal.Parse(textBox7.Text);
                string p2 = textBox8.Text;
                string post = textBox9.Text;

                DataTable dtSOTR = ds.Tables["SOTR"];
                dtSOTR.Rows.Add(new object[] {id,fio,gender,tel,educ,sal,p1,p2,post});

                //SqlCommandBuilder buildSOTR = new SqlCommandBuilder(daSOTR);

                //daSOTR.UpdateCommand = buildSOTR.GetUpdateCommand();
                //daSOTR.InsertCommand = buildSOTR.GetInsertCommand();
                //daSOTR.DeleteCommand = buildSOTR.GetDeleteCommand();
                //using (buildSOTR)
                //{
                //    daSOTR.Update(ds,"SOTR");
                //}
                
                using (var con = new SqlConnection(connectionString))
                using (var adapter = new SqlDataAdapter("SELECT * FROM SOTR", con))
                using (new SqlCommandBuilder(adapter))
                {
                    con.Open();
                    adapter.Update(dtSOTR);
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        // Добавление авторов
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

                SqlCommandBuilder buildAUTHOR = new SqlCommandBuilder(daAUTHOR);

                daAUTHOR.UpdateCommand = buildAUTHOR.GetUpdateCommand();
                daAUTHOR.InsertCommand = buildAUTHOR.GetInsertCommand();
                daAUTHOR.DeleteCommand = buildAUTHOR.GetDeleteCommand();
                using (buildAUTHOR)
                {
                    daAUTHOR.Update(ds, "AUTHOR");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Функция добавления объектов в таблицу и сохранения в базу данных
        public void AddToTableAndSave()
        {

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

                SqlCommandBuilder buildBOOK = new SqlCommandBuilder(daBOOK);

                daBOOK.UpdateCommand = buildBOOK.GetUpdateCommand();
                daBOOK.InsertCommand = buildBOOK.GetInsertCommand();
                daBOOK.DeleteCommand = buildBOOK.GetDeleteCommand();
                using (buildBOOK)
                {
                    daBOOK.Update(ds, "BOOK");
                }
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

                SqlCommandBuilder buildAUTHORBOOK = new SqlCommandBuilder(daAUTHORBOOK);

                daAUTHORBOOK.UpdateCommand = buildAUTHORBOOK.GetUpdateCommand();
                daAUTHORBOOK.InsertCommand = buildAUTHORBOOK.GetInsertCommand();
                daAUTHORBOOK.DeleteCommand = buildAUTHORBOOK.GetDeleteCommand();
                using (buildAUTHORBOOK)
                {
                    daAUTHORBOOK.Update(ds, "AUTHORBOOK");
                }
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

                SqlCommandBuilder buildSCHOOL = new SqlCommandBuilder(daSCHOOL);

                daSCHOOL.UpdateCommand = buildSCHOOL.GetUpdateCommand();
                daSCHOOL.InsertCommand = buildSCHOOL.GetInsertCommand();
                daSCHOOL.DeleteCommand = buildSCHOOL.GetDeleteCommand();
                using (buildSCHOOL)
                {
                    daSCHOOL.Update(ds, "SCHOOL");
                }
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

                SqlCommandBuilder buildAGREEMENT = new SqlCommandBuilder(daAGREEMENT);

                daAGREEMENT.UpdateCommand = buildAGREEMENT.GetUpdateCommand();
                daAGREEMENT.InsertCommand = buildAGREEMENT.GetInsertCommand();
                daAGREEMENT.DeleteCommand = buildAGREEMENT.GetDeleteCommand();
                using (buildAGREEMENT)
                {
                    daAGREEMENT.Update(ds, "AGREEMENT");
                }
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

                SqlCommandBuilder buildAGREEMENTNBOOK = new SqlCommandBuilder(daAGREEMENTNBOOK);

                daAGREEMENTNBOOK.UpdateCommand = buildAGREEMENTNBOOK.GetUpdateCommand();
                daAGREEMENTNBOOK.InsertCommand = buildAGREEMENTNBOOK.GetInsertCommand();
                daAGREEMENTNBOOK.DeleteCommand = buildAGREEMENTNBOOK.GetDeleteCommand();
                using (buildAGREEMENTNBOOK)
                {
                    daAGREEMENTNBOOK.Update(ds, "AGREEMENTNBOOK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Удаление сотрудников
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
                ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Delete();
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
    }
}
