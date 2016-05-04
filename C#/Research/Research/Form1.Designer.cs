namespace Research
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLimit = new System.Windows.Forms.TextBox();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarA = new System.Windows.Forms.TrackBar();
            this.trackBarB = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarB)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(676, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Начать!";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Верхняя граница n-чисел:";
            // 
            // textBoxLimit
            // 
            this.textBoxLimit.Location = new System.Drawing.Point(171, 14);
            this.textBoxLimit.Name = "textBoxLimit";
            this.textBoxLimit.Size = new System.Drawing.Size(209, 20);
            this.textBoxLimit.TabIndex = 2;
            this.textBoxLimit.Text = "1000";
            this.textBoxLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainChart
            // 
            chartArea3.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.mainChart.Legends.Add(legend3);
            this.mainChart.Location = new System.Drawing.Point(30, 86);
            this.mainChart.Name = "mainChart";
            this.mainChart.Size = new System.Drawing.Size(1082, 487);
            this.mainChart.TabIndex = 3;
            this.mainChart.Text = "chart1";
            // 
            // textBoxStep
            // 
            this.textBoxStep.Location = new System.Drawing.Point(440, 14);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(209, 20);
            this.textBoxStep.TabIndex = 5;
            this.textBoxStep.Text = "10";
            this.textBoxStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Шаг:";
            // 
            // trackBarA
            // 
            this.trackBarA.Location = new System.Drawing.Point(171, 40);
            this.trackBarA.Name = "trackBarA";
            this.trackBarA.Size = new System.Drawing.Size(209, 45);
            this.trackBarA.TabIndex = 6;
            this.trackBarA.ValueChanged += new System.EventHandler(this.trackBarA_ValueChanged);
            // 
            // trackBarB
            // 
            this.trackBarB.Location = new System.Drawing.Point(440, 40);
            this.trackBarB.Name = "trackBarB";
            this.trackBarB.Size = new System.Drawing.Size(209, 45);
            this.trackBarB.TabIndex = 7;
            this.trackBarB.ValueChanged += new System.EventHandler(this.trackBarB_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "А:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(417, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "B:";
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(268, 70);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(17, 13);
            this.labelA.TabIndex = 10;
            this.labelA.Text = "А:";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(540, 72);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(17, 13);
            this.labelB.TabIndex = 11;
            this.labelB.Text = "B:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 591);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarB);
            this.Controls.Add(this.trackBarA);
            this.Controls.Add(this.textBoxStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.textBoxLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStart);
            this.Name = "Form1";
            this.Text = "Сравнение Алгоритма Аткина и предсказания Чебышева";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLimit;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarA;
        private System.Windows.Forms.TrackBar trackBarB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.Label labelB;
    }
}

