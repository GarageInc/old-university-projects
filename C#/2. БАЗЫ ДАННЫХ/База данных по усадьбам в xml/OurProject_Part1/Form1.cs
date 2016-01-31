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
        // Этот датасет хранит в себе список усадеб в виде отдельной таблицы - контейнер усадеб
        DataSet homesteads = new DataSet();//Для таблицы усадеб
        
        public Form1()
        {
            InitializeComponent();

            // Проверяем наличие файлов
            if (!File.Exists("homesteads.xml"))
                CreateBD();

            // считываем информацию из файлов с данными
            homesteads.ReadXml("homesteads.xml", XmlReadMode.ReadSchema);

            // установка источника данных для dataGridView1
            dataGridView1.DataSource = homesteads.Tables["Усадьбы"];

            
            // отмена генерации столбцов dataGridView1
            dataGridView1.AutoGenerateColumns = false;
        }

        // База данных - создание
        public static void CreateBD()
        {
            // создание набора данных
            DataSet ds = new DataSet();

            DataColumn dc;
            // создаем таблицу
            // добавляем таблицу в список таблиц набора данных
            ds.Tables.Add(new DataTable("Усадьбы"));

            // Номер усадьбы по порядку
            DataColumn dcId = new DataColumn("id", Type.GetType("System.Int32"));
            dcId.AutoIncrement = true;
            ds.Tables["Усадьбы"].Columns.Add(dcId);

            // атрибут
            dc = new DataColumn("Адрес", Type.GetType("System.String"));
            ds.Tables["Усадьбы"].Columns.Add(dc);

            // атрибут
            dc = new DataColumn("Гараж", Type.GetType("System.String"));
            ds.Tables["Усадьбы"].Columns.Add(dc);
            // атрибут
            dc = new DataColumn("НалоговаяСтавкаГаража", Type.GetType("System.Double"));
            ds.Tables["Усадьбы"].Columns.Add(dc);

            // атрибут
            dc = new DataColumn("Дом", Type.GetType("System.String"));
            ds.Tables["Усадьбы"].Columns.Add(dc);
            // атрибут
            dc = new DataColumn("НалоговаяСтавкаДома", Type.GetType("System.Double"));
            ds.Tables["Усадьбы"].Columns.Add(dc);

            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dcId;
            ds.Tables["Усадьбы"].PrimaryKey = keys;

            ds.WriteXml("homesteads.xml", XmlWriteMode.WriteSchema);
        }

        // Сохранение в файл при закрытии
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            homesteads.WriteXml("homesteads.xml", XmlWriteMode.WriteSchema);
        }

        // Удаление усадьбы
        private void DELETEOLD_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)// если выбранная строка не пуста
                ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Delete();// удаляем заданную строку
            else
                MessageBox.Show("Усадьба для удаления не выбрана!"); 
        }

        // Добавление новой усадьбы
        private void ADDNEW_Click(object sender, EventArgs e)
        {
            try
            {
                string адрес = textBox1.Text;
                double налоговаяСтавкаНаГараж = Double.Parse(textBox2.Text);
                double налоговаяСтавкаНаДом = Double.Parse(textBox3.Text);

                Garage g;
                if(checkBox1.Checked)
                {
                    g = new Garage(налоговаяСтавкаНаГараж);
                }
                else
                {
                    g = new Garage();
                }

                House h;
                if (checkBox1.Checked)
                {
                    h = new House(налоговаяСтавкаНаДом);
                }
                else
                {
                    h = new House();
                }

                
                Homestead homestead = new Homestead(адрес,g,h);

                // генерация данных об усадьбе
                // создание нового усадьбы
                DataRow newrow = homesteads.Tables["Усадьбы"].NewRow();

                // заполнение атрибутов
                newrow["Адрес"] = homestead.адрес;
                if (checkBox1.Checked)
                {
                    newrow["Гараж"] = "есть";
                    newrow["НалоговаяСтавкаГаража"] = homestead.гараж.величинаНалога;
                }
                else
                {
                    newrow["Гараж"] = "нет";
                    newrow["НалоговаяСтавкаГаража"] = 0;
                }

                if (checkBox2.Checked)
                {
                    newrow["Дом"] = "есть";
                    newrow["НалоговаяСтавкаДома"] = homestead.дом.величинаНалога;
                }
                else
                {
                    newrow["Дом"] = "нет";
                    newrow["НалоговаяСтавкаДома"] = 0;
                }

                // записываем созданную запись в таблицу
                homesteads.Tables["Усадьбы"].Rows.Add(newrow);
                
                // отмена выделения всех выбранных строк в DataGridView
                foreach (DataGridViewRow dgvr in dataGridView1.SelectedRows)
                    dgvr.Selected = false;
            
                // установка выбора вновь созданного элемента
                // последняя строка DataGridView – это строка для ручного ввода новой 
                // записи, поэтому последняя значимая строка – предпоследняя
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Selected = true; 
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка ввода данных!\nПроверьте правильность заполненности полей.\n"+ex.Message);
            }   
        }

        // Подсчитать количество налога(общее по всем усадьбам)
        private void button1_Click(object sender, EventArgs e)
        {
            double sum = 0;

            // Посчитаем для каждой усадьбы
            var r = homesteads.Tables["Усадьбы"].Rows;
            foreach(DataRow  rr in r)
            {
                string s = rr["НалоговаяСтавкаГаража"].ToString() ;
                sum = sum + double.Parse(s);


                s = rr["НалоговаяСтавкаДома"].ToString();
                sum = sum + double.Parse(s);
            }

            // Выведем результат:
            textBox4.Text = sum.ToString();
        }
    }
}

