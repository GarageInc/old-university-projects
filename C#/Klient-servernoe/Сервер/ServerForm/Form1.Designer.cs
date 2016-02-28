namespace ServerForm
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
            this.txtUsers = new System.Windows.Forms.TextBox();
            this.btnTurn = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtUsers
            // 
            this.txtUsers.BackColor = System.Drawing.Color.Silver;
            this.txtUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtUsers.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtUsers.Location = new System.Drawing.Point(16, 64);
            this.txtUsers.Multiline = true;
            this.txtUsers.Name = "txtUsers";
            this.txtUsers.ReadOnly = true;
            this.txtUsers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUsers.Size = new System.Drawing.Size(376, 398);
            this.txtUsers.TabIndex = 3;
            // 
            // btnTurn
            // 
            this.btnTurn.BackColor = System.Drawing.Color.LightGreen;
            this.btnTurn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTurn.ForeColor = System.Drawing.Color.Black;
            this.btnTurn.Location = new System.Drawing.Point(12, 12);
            this.btnTurn.Name = "btnTurn";
            this.btnTurn.Size = new System.Drawing.Size(380, 46);
            this.btnTurn.TabIndex = 1;
            this.btnTurn.Text = "Запуск сервера";
            this.btnTurn.UseVisualStyleBackColor = false;
            this.btnTurn.Click += new System.EventHandler(this.btnTurn_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Silver;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLog.ForeColor = System.Drawing.Color.Crimson;
            this.txtLog.Location = new System.Drawing.Point(398, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(371, 450);
            this.txtLog.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(785, 474);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtUsers);
            this.Controls.Add(this.btnTurn);
            this.Name = "Form1";
            this.Text = "Сервер";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtUsers;
        internal System.Windows.Forms.Button btnTurn;
        private System.Windows.Forms.TextBox txtLog;
    }
}

