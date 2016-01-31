using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OurProject_Part1
{
    public partial class Form3 : Form
    {
        DataSet dsi;
        public Form3(ref DataSet ds)
        {
            InitializeComponent();
            this.dsi = ds;

            dataGridView.DataSource = dsi.Tables["КомпанииИсполнители"];
            
        }

        //Add
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // генерация данных о заказе
                // создание нового заказа
                DataRow newrow = dsi.Tables["КомпанииИсполнители"].NewRow();
                // заполнение атрибутов заказчика

                if (textBox1.Text.CompareTo("0") == 0)
                    newrow["IDКомпанииИсполнителя"] = dsi.Tables["КомпанииИсполнители"].Rows.Count + 1;
                else
                    newrow["IDКомпанииИсполнителя"] = int.Parse(textBox1.Text);

                newrow["НазваниеКомпанииИсполнителя"] = (string)textBox2.Text;
                newrow["СпецификацияКомпании"] = (string)textBox3.Text;
                newrow["СредстваСвязи"] = (string)textBox4.Text;
                newrow["СчетчикКомпанииИсполнителя"] = int.Parse(textBox5.Text.ToString());

                // записываем созданную запись в таблицу
                dsi.Tables["КомпанииИсполнители"].Rows.Add(newrow);

                // работа с DataGridView, показываеющей таблицу заказчиков
                // отмена выделения всех выбранных строк в DataGridView
                foreach (DataGridViewRow dgvr in dataGridView.SelectedRows)
                    dgvr.Selected = false;

                // установка выбора вновь созданного заказчика
                // последняя строка DataGridView – это строка для ручного ввода новой 
                // записи, поэтому последняя значимая строка – предпоследняя
                dataGridView.Rows[dataGridView.Rows.Count - 2].Selected = true;
            }
            catch
            {
                MessageBox.Show("Данная компания уже существует в базе данных!");
            }
        }

        //Delete
        private void button2_Click(object sender, EventArgs e)
        {
            // если заказ не был выбран, удалять нечего
            if (dataGridView.SelectedRows.Count == 0)
                return;

            //получение номера текущего выбранного заказазчика - ID
            int nom = (int)dataGridView.SelectedRows[0].Cells["IDКомпанииИсполнителя"].Value;

            // выбираем все записи, соответствующие текущему заказу
            DataRow[] drs = dsi.Tables["КомпанииИсполнители"].Select("IDКомпанииИсполнителя=" + nom);

            // удаляем все найденные заказы из таблиц набора данных
            //for (int i = drs.Length - 1; i >= 0; i--)
            //  drs[i].Delete();
            // поиск заказа по ключу
            dsi.Tables["КомпанииИсполнители"].Rows.Find(new object[] { (object)nom }).Delete();
            // удаление заказа из набора данных  
            // DataGridView заказов обновится автоматически

        }

        //Save
        private void button3_Click(object sender, EventArgs e)
        {
            dsi.WriteXml("zaks.xml", XmlWriteMode.WriteSchema);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            dsi.WriteXml("zaks.xml", XmlWriteMode.WriteSchema);
        }


    }
}
