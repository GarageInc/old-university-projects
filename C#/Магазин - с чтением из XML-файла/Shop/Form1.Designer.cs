namespace Shop
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
            this.readConfigButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.productsInput = new System.Windows.Forms.TextBox();
            this.considerButton = new System.Windows.Forms.Button();
            this.createConfigButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // readConfigButton
            // 
            this.readConfigButton.Location = new System.Drawing.Point(151, 114);
            this.readConfigButton.Name = "readConfigButton";
            this.readConfigButton.Size = new System.Drawing.Size(75, 39);
            this.readConfigButton.TabIndex = 0;
            this.readConfigButton.Text = "Прочитать конфиг";
            this.readConfigButton.UseVisualStyleBackColor = true;
            this.readConfigButton.Click += new System.EventHandler(this.readConfigButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ввод товаров:";
            // 
            // productsInput
            // 
            this.productsInput.Location = new System.Drawing.Point(137, 184);
            this.productsInput.Name = "productsInput";
            this.productsInput.Size = new System.Drawing.Size(100, 20);
            this.productsInput.TabIndex = 2;
            // 
            // considerButton
            // 
            this.considerButton.Location = new System.Drawing.Point(151, 240);
            this.considerButton.Name = "considerButton";
            this.considerButton.Size = new System.Drawing.Size(75, 23);
            this.considerButton.TabIndex = 3;
            this.considerButton.Text = "Посчитать";
            this.considerButton.UseVisualStyleBackColor = true;
            this.considerButton.Click += new System.EventHandler(this.considerButton_Click);
            // 
            // createConfigButton
            // 
            this.createConfigButton.Location = new System.Drawing.Point(126, 55);
            this.createConfigButton.Name = "createConfigButton";
            this.createConfigButton.Size = new System.Drawing.Size(128, 39);
            this.createConfigButton.TabIndex = 4;
            this.createConfigButton.Text = "Создать стандартный конфиг-xml";
            this.createConfigButton.UseVisualStyleBackColor = true;
            this.createConfigButton.Click += new System.EventHandler(this.createConfigButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 341);
            this.Controls.Add(this.createConfigButton);
            this.Controls.Add(this.considerButton);
            this.Controls.Add(this.productsInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readConfigButton);
            this.Name = "Form1";
            this.Text = "Магазин";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readConfigButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox productsInput;
        private System.Windows.Forms.Button considerButton;
        private System.Windows.Forms.Button createConfigButton;
    }
}

