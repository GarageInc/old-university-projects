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
            this.txtIPPORT = new System.Windows.Forms.TextBox();
            this.lvlIPPORT = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtUsers
            // 
            this.txtUsers.BackColor = System.Drawing.Color.Silver;
            this.txtUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtUsers.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtUsers.Location = new System.Drawing.Point(16, 102);
            this.txtUsers.Multiline = true;
            this.txtUsers.Name = "txtUsers";
            this.txtUsers.ReadOnly = true;
            this.txtUsers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUsers.Size = new System.Drawing.Size(376, 360);
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
            // txtIPPORT
            // 
            this.txtIPPORT.BackColor = System.Drawing.SystemColors.Window;
            this.txtIPPORT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtIPPORT.ForeColor = System.Drawing.Color.Red;
            this.txtIPPORT.Location = new System.Drawing.Point(79, 70);
            this.txtIPPORT.Name = "txtIPPORT";
            this.txtIPPORT.ReadOnly = true;
            this.txtIPPORT.Size = new System.Drawing.Size(313, 26);
            this.txtIPPORT.TabIndex = 4;
            // 
            // lvlIPPORT
            // 
            this.lvlIPPORT.AutoSize = true;
            this.lvlIPPORT.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvlIPPORT.ForeColor = System.Drawing.Color.Black;
            this.lvlIPPORT.Location = new System.Drawing.Point(12, 74);
            this.lvlIPPORT.Name = "lvlIPPORT";
            this.lvlIPPORT.Size = new System.Drawing.Size(61, 19);
            this.lvlIPPORT.TabIndex = 4;
            this.lvlIPPORT.Text = "IP-port:";
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
            this.Controls.Add(this.txtIPPORT);
            this.Controls.Add(this.lvlIPPORT);
            this.Name = "Form1";
            this.Text = "Сервер";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtUsers;
        internal System.Windows.Forms.Button btnTurn;
        internal System.Windows.Forms.TextBox txtIPPORT;
        internal System.Windows.Forms.Label lvlIPPORT;
        private System.Windows.Forms.TextBox txtLog;
    }
}

