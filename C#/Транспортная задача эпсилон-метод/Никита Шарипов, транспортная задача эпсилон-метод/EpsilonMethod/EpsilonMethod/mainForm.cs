using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace EpsilonMethod
{
    public partial class mainForm : Form
    {
        // Основной класс транспортной задачи
        private Transport задача;

        // Промежуточные переменный
        double[,] ОпорныйПлан = null;
        double[,] ОптимальныйПлан;

        public mainForm()
        {
            InitializeComponent();
        }
        
        // ФУНКЦИИ, КОТОРЫЙ ОТРАБАТЫВАЮТ ПРИ ИЗМЕНЕНИИ ЗНАЧЕНИЙ НА ПОЛЯХ ФОРМЫ
        private void nudA_ValueChanged(object sender, EventArgs e)
        {
            int rowCount = Convert.ToInt32(nudA.Value);
            gridA.RowCount = rowCount;
            gridC.RowCount = rowCount;

            gridA.Rows[rowCount - 1].HeaderCell.Value = "A" + rowCount.ToString();
            gridC.Rows[rowCount - 1].HeaderCell.Value = "A" + rowCount.ToString();
        }

        private void nudB_ValueChanged(object sender, EventArgs e)
        {
            int colCount = Convert.ToInt32(nudB.Value);
            gridB.RowCount = colCount;
            gridC.ColumnCount = colCount;

            gridB.Rows[colCount - 1].HeaderCell.Value = "B" + colCount.ToString();
            gridC.Columns[colCount - 1].HeaderText = "B" + colCount.ToString();
        }

        /// <summary>
        /// Кнопка "Решить". Запускается решение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSolve_Click(object sender, EventArgs e)
        {
            // Заполняем поля и проводим проверку
            СчитатьОсновныеДанные();
            ПроверитьПравильностьДанных();

            // Теперь, инициализируем переменные
            gridSupport.RowCount = задача.КоличествоПоставщиков;
            gridFinal.RowCount = задача.КоличествоПоставщиков;

            gridSupport.ColumnCount = задача.КоличествоПотребителей;
            gridFinal.ColumnCount = задача.КоличествоПотребителей;
            
            // Выводим опорный план, который получен при запуске конструктора в задаче
            ОпорныйПлан = задача.МатрицаПоискаОптимальногоПлана;
            ЗаполнитьУказаннуюТаблицу(gridSupport, ОпорныйПлан);

            // Запускаем задачу на решение:
            задача.НачатьРешение();// Там же и выводится результат

            // Получим оптимальный план(после решения)
            ОптимальныйПлан = задача.МатрицаПоискаОптимальногоПлана;
            ЗаполнитьУказаннуюТаблицу(gridFinal, ОптимальныйПлан);
            
            lblOptimum.Text = "Цена перевозок: " + задача.КонечнаяСумма.ToString();
        }


        /// <summary>
        /// Функция заполнения гридов
        /// </summary>
        private void СчитатьОсновныеДанные()
        {
            double[] поставщики = new double[gridA.RowCount];
            double[] потребители = new double[gridB.RowCount];
            double[,] стоимости = new double[gridA.RowCount, gridB.RowCount];
            
            try
            {
                int n = gridA.RowCount;
                int m = gridB.RowCount;
                for (int i = 0; i < n; ++i)
                {
                    поставщики[i] = Convert.ToInt32(gridA.Rows[i].Cells[0].Value);
                }

                for (int i = 0; i < m; ++i)
                {
                    потребители[i] = Convert.ToInt32(gridB.Rows[i].Cells[0].Value);
                }

                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < m; ++j)
                    {
                        стоимости[i, j] = Convert.ToInt32(gridC.Rows[i].Cells[j].Value);
                    }
                }

                // Инициализируем переменный в задачи
                задача = new Transport(ref resultRichTextBox,стоимости,потребители,поставщики);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Валидация - проверка на правильность ввода
        /// </summary>
        private void ПроверитьПравильностьДанных()
        {
            double totalProducts = 0;
            for (int i = 0; i < задача.КоличествоПоставщиков; ++i)
            {
                totalProducts += задача.Поставщики[i];
            }

            double totalNeeds = 0;
            for (int i = 0; i < задача.КоличествоПотребителей; ++i)
            {
                totalNeeds += задача.Потребители[i];
            }
            string text ="";
            if ((int)totalProducts != (int)totalNeeds)
            {
                text+=("\nКоличество товаров не соответствует потреблению, задача НЕзамкнута!");
            }
            else
            {
                text+=("\nКоличество товаров соответствует потреблению(замкнутая задача)");
            }

            int kol = 0;
            for (int i = 0; i < задача.КоличествоПоставщиков; i++)
            {
                for (int j = 0; j < задача.КоличествоПотребителей; j++)
                {
                    if (задача.МатрицаПоискаОптимальногоПлана[i, j] == 0)
                    {
                        kol++;
                    }
                }
            }

            if (kol == задача.КоличествоПоставщиков + задача.КоличествоПотребителей - 1)
                text += "\nМетод северо-западного угла: невырожденный план\n";
            else
                text += "\nМетод северо-западного угла: вырожденный план\n";

            resultRichTextBox.Text+=text+'\n';
        }

        /// <summary>
        /// Заполнение указанного грида-таблицы
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="arr"></param>
        private void ЗаполнитьУказаннуюТаблицу(DataGridView grid, double[,] arr)
        {
            for (int i = 0; i < задача.КоличествоПоставщиков; i++)
            {
                grid.Rows[i].HeaderCell.Value = "A" + (i + 1).ToString();
                for (int j = 0; j < задача.КоличествоПотребителей; j++)
                {
                    grid.Rows[i].Cells[j].Value = arr[i, j].ToString();
                    grid.Columns[j].HeaderText = "B" + (j + 1).ToString();
                }
            }
        }
    }
    
}
