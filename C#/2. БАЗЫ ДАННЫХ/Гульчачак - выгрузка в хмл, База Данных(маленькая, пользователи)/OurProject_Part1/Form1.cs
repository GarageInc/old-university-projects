using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace OurProject_Part1
{
    public partial class Form1 : Form
    {
        DataSet users = new DataSet();//Для таблицы Заказов и ЗаписейОплаты

        public Form1()
        {
            InitializeComponent();

            // Проверяем наличие файлов
            if (!File.Exists("users.xml"))
                CreateBD();

            // считываем информацию из файлов с данными
            users.ReadXml("users.xml", XmlReadMode.ReadSchema);

            // установка источника данных для dataGridView1
            dataGridView1.DataSource = users.Tables["Пользователи"];

            // отмена генерации столбцов dataGridView1
            dataGridView1.AutoGenerateColumns = false;
        }

        // База данных - создание
        public static void CreateBD()
        {
            // создание набора данных
            DataSet ds = new DataSet();

            DataColumn dc;
            // создаем таблицу заказов
            // добавляем таблицу в список таблиц набора данных
            ds.Tables.Add(new DataTable("Пользователи"));
            // атрибут
            dc = new DataColumn("Номер", Type.GetType("System.Int32"));
            ds.Tables["Пользователи"].Columns.Add(dc);
            // атрибут
            dc = new DataColumn("Имя", Type.GetType("System.String"));
            ds.Tables["Пользователи"].Columns.Add(dc);
            // атрибут
            dc = new DataColumn("Фамилия", Type.GetType("System.String"));
            ds.Tables["Пользователи"].Columns.Add(dc);
            // атрибут
            dc = new DataColumn("ДатаРождения", Type.GetType("System.String"));
            ds.Tables["Пользователи"].Columns.Add(dc);

            // первичным ключом будет номер
            DataColumn[] key = new DataColumn[1] { ds.Tables["Пользователи"].Columns["Номер"] };//
            ds.Tables["Пользователи"].PrimaryKey = key;

            ds.WriteXml("users.xml", XmlWriteMode.WriteSchema);
        }

        // Сохранение в файл при закрытии
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            users.WriteXml("users.xml", XmlWriteMode.WriteSchema);
        }

        // Удаление пользователя
        private void DELETEOLD_Click(object sender, EventArgs e)
        {
            // если пользователь не был выбран, удалять нечего
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пользователь для удаления не выбран!");
                return;
            }

            // получение номера текущего выбранного пользователя
            int nom = (int)dataGridView1.SelectedRows[0].Cells["Номер"].Value;

            // выбираем все записи, соответствующие текущему пользователю
            DataRow[] drs = users.Tables["Пользователи"].Select("Номер=" + nom);

            // удаляем все найденные пользователи из таблиц набора данных
            for (int i = drs.Length - 1; i >= 0; i--)
                drs[i].Delete();
        }

        // Добавление нового пользователя
        private void ADDNEW_Click(object sender, EventArgs e)
        {
            try
            {
                int nomer = int.Parse(textBox1.Text);
                string first = textBox2.Text;
                string second = textBox3.Text;

                int day = int.Parse(textBox4.Text);
                int month = int.Parse(textBox5.Text);
                int year = int.Parse(textBox6.Text);

                // Проверим правильность даты
                if (! ( (day>0 && day<=31) && (month>0 && month<=12) ) )
                    MessageBox.Show("Ошибка ввода данных!\nПроверьте правильность заполненности полей даты рождения");
                
                BirthDate birthDate= new BirthDate(day,month,year);
                Name name = new Name(first,second); 

                User user = new User(nomer,name, birthDate);

                // генерация данных о пользователя
                // создание нового пользователя
                DataRow newrow = users.Tables["Пользователи"].NewRow();

                // заполнение атрибутов
                newrow["Номер"] = user.Id;            
                newrow["Имя"] = user.Name.First;
                newrow["Фамилия"] = user.Name.Second;
                newrow["ДатаРождения"] = user.BirthDate.GetDate();

                // записываем созданную запись в таблицу
                users.Tables["Пользователи"].Rows.Add(newrow);
                
                // отмена выделения всех выбранных строк в DataGridView
                foreach (DataGridViewRow dgvr in dataGridView1.SelectedRows)
                    dgvr.Selected = false;
            
                // установка выбора вновь созданного заказа
                // последняя строка DataGridView – это строка для ручного ввода новой 
                // записи, поэтому последняя значимая строка – предпоследняя
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Selected = true; 
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка ввода данных!\nПроверьте правильность заполненности полей, уникальность ID пользователей.\n"+ex.Message);
            }   
        }
    }
}

