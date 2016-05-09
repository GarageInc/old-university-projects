using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Graph = System.Windows.Forms.DataVisualization.Charting;

namespace Research
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected void initChartBy(string name)
        {
            mainChart.Series.Add(name);

            mainChart.Series[name].ChartType = Graph.SeriesChartType.Line;

            //mainChart.Series[name].Color = Color.LightGreen;
            mainChart.Series[name].BorderWidth = 3;


            mainChart.Series[name].LegendText = name;
        }

        // Названия графиков
        string chebishebHighBorderName = "Верхняя граница Чебышева";
        string chebishebLowBorderName = "Нижняя граница Чебышева";
        string atkinName = "Тест Аткина";

        // Контейнер результатов
        List<Pair> results = new List<Pair>();

        // Функция по запуску Решета Аткина и рисованию графиков
        protected void addAlgorithmResults(int limit, int step)
        {
            results = AtkinAlgorithm.getPairsInInterval(limit, step);

            setSeries();
        }

        // Функция непосредственного рисования графиков
        protected void setSeries()
        {
            mainChart.Series.Clear();

            initChartBy(chebishebHighBorderName);
            initChartBy(chebishebLowBorderName);
            initChartBy(atkinName);

            foreach (var item in results)
            {
                mainChart.Series[atkinName].Points.AddXY(item.valueX, item.valueY);
                mainChart.Series[chebishebLowBorderName].Points.AddXY(item.valueX, getLowChebishevValue(item.valueX));
                mainChart.Series[chebishebHighBorderName].Points.AddXY(item.valueX, getHightChebishevValue(item.valueX));
            }
        }

        // Получение нижнего значения А
        public double getLowChebishevValue(int n)
        {
            double A = trackBarA.Value / 100.0;

            var a = Math.Log(4, 2);

            return A * n / Math.Log(n, Math.E);
        }

        // Получение верхнего значения коэффициента B
        public double getHightChebishevValue(int n)
        {
            double B = trackBarB.Value / 100.0;

            return B * n / Math.Log(n, Math.E);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            this.mainChart.Titles.Clear();
            this.mainChart.Titles.Add("Происходит подсчет данных...");
            
            int limit = int.Parse(textBoxLimit.Text);
            int step = int.Parse(textBoxStep.Text);

            addAlgorithmResults(limit, step);

            this.mainChart.Titles.Clear();
            this.mainChart.Titles.Add("Подсчет окончен");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainChart.Legends.Add("MyLegend");
            mainChart.Legends["MyLegend"].BorderColor = Color.Tomato;

            var maximum = 100 * 2 * (Math.Log(2, Math.E));
            var minimum = 100 * (Math.Log(2, Math.E)) / 2;
            trackBarA.Minimum = (int)minimum;
            trackBarA.Maximum = (int)maximum;

            trackBarB.Minimum = (int)minimum;
            trackBarB.Maximum = (int)maximum;

            trackBarA.Value = trackBarA.Minimum;
            trackBarB.Value = trackBarB.Maximum;

            trackBarA_ValueChanged(null, null);
        }

        private void trackBarA_ValueChanged(object sender, EventArgs e)
        {
            trackBarA.Maximum = trackBarB.Value;

            labelA.Text = (trackBarA.Value / 100.0).ToString();
            labelB.Text = (trackBarB.Value / 100.0).ToString();

            setSeries();
        }

        private void trackBarB_ValueChanged(object sender, EventArgs e)
        {
            trackBarB.Minimum = trackBarA.Value;

            labelA.Text = (trackBarA.Value / 100.0).ToString();
            labelB.Text = (trackBarB.Value / 100.0).ToString();

            setSeries();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();
            label1.Text = "Статус";
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();

            label1.Text = "Выполняется расчёт";

            BigInteger kil=0;

            int i=0;

            double t = 0, pc = 0, x = 0, sum2=0, sum = 0, sum1 = 0, xl2 = 0, c4 = 0.493091099368767, c5 = 0.150757555616266, c6 = 0.174762639299271;// sum dlya x^8
            x = 10;

            double rbV_C = double.Parse(textBoxС_V.Text);

            t = 0.333333333333333;

            StreamReader file = new StreamReader("prost.txt");
            
            kil = 0; pc = 0; sum = 0; sum1 = 0;

            while (x <= 100000000)
            {
                table.Rows.Add();

                while (!file.EndOfStream && pc < x)
                {
                    pc = Double.Parse( file.ReadLine() );

                    if (pc < x)
                    {

                        if (rbG.Checked)
                            sum1 += Math.Log(pc, Math.E) / (pc * pc);
                        else if (rbD.Checked)
                            sum1 += Math.Log(pc, Math.E) / (pc * pc * pc);
                        else if (rbE.Checked)
                            sum1 += (1 / (pc * pc * pc));
                        else
                        {
                            sum2 += Math.Log(pc);
                            sum = sum2 - x + Math.Sqrt(x);
                        }

                    } //else kil++;
                }

                if (rbE.Checked)
                    table.Rows[i].Cells[0].Value = (sum1).ToString();
                else if (rbD.Checked)
                    table.Rows[i].Cells[0].Value = (sum1).ToString();
                else if(rbG.Checked)
                    table.Rows[i].Cells[0].Value = (sum1).ToString();
                else
                    table.Rows[i].Cells[0].Value = (sum).ToString();

                kil++;

                if (rbA.Checked)
                    xl2 = Math.Sqrt(x);
                else if (rbB.Checked)
                    xl2 = Math.Sqrt(x) * Math.Log(x, Math.E);
                else if (rbV.Checked)
                    xl2 = x - Math.Sqrt(x) - rbV_C * (Math.Log(x, Math.E) / Math.Sqrt(x));
                else if (rbD.Checked)
                    xl2 = c5 - (1 / (x * x));
                else if (rbE.Checked)
                    xl2 = c6 - 1 / ( x * x * Math.Log(x, Math.E));
                else
                    xl2 = c4 - (1 / x);

                table.Rows[i].Cells[1].Value = (xl2).ToString();

                if (rbE.Checked)
                    table.Rows[i].Cells[2].Value = (sum1 / xl2).ToString();
                else if (rbG.Checked)
                    table.Rows[i].Cells[2].Value = (sum1 / xl2).ToString();
                else if (rbD.Checked)
                    table.Rows[i].Cells[2].Value = (sum1 / xl2).ToString();
                else
                    table.Rows[i].Cells[2].Value = (sum / xl2).ToString();

                table.Rows[i].Cells[3].Value = (x).ToString();

                if (rbG.Checked)
                    sum1 += (Math.Log(pc, Math.E)) / (pc * pc);
                else if (rbD.Checked)
                    sum1 += Math.Log(pc, Math.E) / (pc * pc * pc);
                else if (rbE.Checked)
                    sum1 += (1 / (pc * pc * pc));
                else
                {
                    sum2 += Math.Log(pc, Math.E);
                    sum = sum2 - x + Math.Sqrt(x);
                }
                i++;
                x *= 10;
            }

            helpl.Text = i.ToString();

            label1.Text = "Готово";
            
            file.Close();
        }
    }

    class Pair
    {

        public int valueY { get; set; }
        public int valueX { get; set; }

        public Pair(int valueX, int valueY)
        {
            this.valueX = valueX;
            this.valueY = valueY;
        }
    }
}
