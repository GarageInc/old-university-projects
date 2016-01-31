namespace Antivirus
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ScanButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.GeneratSignatureButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(469, 48);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(176, 42);
            this.OpenFileButton.TabIndex = 0;
            this.OpenFileButton.Text = "Выбрать файл для проверки";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(633, 20);
            this.textBox1.TabIndex = 1;
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.Location = new System.Drawing.Point(469, 134);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(176, 48);
            this.OpenFolderButton.TabIndex = 2;
            this.OpenFolderButton.Text = "Выбрать папку для проверки";
            this.OpenFolderButton.UseVisualStyleBackColor = true;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 101);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(633, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Check";
            // 
            // ScanButton
            // 
            this.ScanButton.Location = new System.Drawing.Point(469, 235);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(176, 47);
            this.ScanButton.TabIndex = 4;
            this.ScanButton.Text = "Сканировать";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 294);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(633, 44);
            this.progressBar1.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 200);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(633, 20);
            this.textBox3.TabIndex = 6;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(13, 278);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 13);
            this.resultLabel.TabIndex = 7;
            // 
            // GeneratSignatureButton
            // 
            this.GeneratSignatureButton.Location = new System.Drawing.Point(810, 15);
            this.GeneratSignatureButton.Name = "GeneratSignatureButton";
            this.GeneratSignatureButton.Size = new System.Drawing.Size(203, 34);
            this.GeneratSignatureButton.TabIndex = 8;
            this.GeneratSignatureButton.Text = "Сгенерировать сигнатуру вируса";
            this.GeneratSignatureButton.UseVisualStyleBackColor = true;
            this.GeneratSignatureButton.Click += new System.EventHandler(this.GeneratSignatureButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(664, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(493, 257);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(810, 56);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 13);
            this.label.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 361);
            this.Controls.Add(this.label);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.GeneratSignatureButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.OpenFolderButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.OpenFileButton);
            this.MaximumSize = new System.Drawing.Size(1200, 400);
            this.MinimumSize = new System.Drawing.Size(1200, 400);
            this.Name = "Form1";
            this.Text = "Антивирус";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button GeneratSignatureButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label;
    }
}

