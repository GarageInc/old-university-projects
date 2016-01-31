namespace ClientForm
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
            this.txtLoginIn = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.txtPasswordIn = new System.Windows.Forms.TextBox();
            this.lblLoginIn = new System.Windows.Forms.Label();
            this.lvlPasswordIn = new System.Windows.Forms.Label();
            this.txtLoginReg = new System.Windows.Forms.TextBox();
            this.lblLoginReg = new System.Windows.Forms.Label();
            this.lblPasswordReg = new System.Windows.Forms.Label();
            this.txtPasswordReg = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPasswordRegRepeat = new System.Windows.Forms.Label();
            this.txtPasswordRegRepeat = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_logout = new System.Windows.Forms.Button();
            this.button_Send = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLoginIn
            // 
            this.txtLoginIn.Location = new System.Drawing.Point(9, 32);
            this.txtLoginIn.Name = "txtLoginIn";
            this.txtLoginIn.Size = new System.Drawing.Size(192, 21);
            this.txtLoginIn.TabIndex = 2;
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnLogIn.Location = new System.Drawing.Point(170, 154);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(77, 23);
            this.btnLogIn.TabIndex = 4;
            this.btnLogIn.Text = "Войти";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnSignUp
            // 
            this.btnSignUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSignUp.Location = new System.Drawing.Point(388, 154);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(108, 23);
            this.btnSignUp.TabIndex = 7;
            this.btnSignUp.Text = "Регистрация";
            this.btnSignUp.UseVisualStyleBackColor = true;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // txtPasswordIn
            // 
            this.txtPasswordIn.Location = new System.Drawing.Point(9, 72);
            this.txtPasswordIn.Name = "txtPasswordIn";
            this.txtPasswordIn.Size = new System.Drawing.Size(192, 21);
            this.txtPasswordIn.TabIndex = 3;
            this.txtPasswordIn.UseSystemPasswordChar = true;
            // 
            // lblLoginIn
            // 
            this.lblLoginIn.AutoSize = true;
            this.lblLoginIn.Location = new System.Drawing.Point(84, 16);
            this.lblLoginIn.Name = "lblLoginIn";
            this.lblLoginIn.Size = new System.Drawing.Size(41, 15);
            this.lblLoginIn.TabIndex = 4;
            this.lblLoginIn.Text = "Логин";
            // 
            // lvlPasswordIn
            // 
            this.lvlPasswordIn.AutoSize = true;
            this.lvlPasswordIn.Location = new System.Drawing.Point(84, 54);
            this.lvlPasswordIn.Name = "lvlPasswordIn";
            this.lvlPasswordIn.Size = new System.Drawing.Size(51, 15);
            this.lvlPasswordIn.TabIndex = 5;
            this.lvlPasswordIn.Text = "Пароль";
            // 
            // txtLoginReg
            // 
            this.txtLoginReg.Location = new System.Drawing.Point(6, 33);
            this.txtLoginReg.Name = "txtLoginReg";
            this.txtLoginReg.Size = new System.Drawing.Size(195, 21);
            this.txtLoginReg.TabIndex = 5;
            // 
            // lblLoginReg
            // 
            this.lblLoginReg.AutoSize = true;
            this.lblLoginReg.Location = new System.Drawing.Point(85, 18);
            this.lblLoginReg.Name = "lblLoginReg";
            this.lblLoginReg.Size = new System.Drawing.Size(41, 15);
            this.lblLoginReg.TabIndex = 7;
            this.lblLoginReg.Text = "Логин";
            // 
            // lblPasswordReg
            // 
            this.lblPasswordReg.AutoSize = true;
            this.lblPasswordReg.Location = new System.Drawing.Point(78, 56);
            this.lblPasswordReg.Name = "lblPasswordReg";
            this.lblPasswordReg.Size = new System.Drawing.Size(51, 15);
            this.lblPasswordReg.TabIndex = 10;
            this.lblPasswordReg.Text = "Пароль";
            // 
            // txtPasswordReg
            // 
            this.txtPasswordReg.Location = new System.Drawing.Point(6, 72);
            this.txtPasswordReg.Name = "txtPasswordReg";
            this.txtPasswordReg.Size = new System.Drawing.Size(195, 21);
            this.txtPasswordReg.TabIndex = 6;
            this.txtPasswordReg.UseSystemPasswordChar = true;
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtIP.ForeColor = System.Drawing.Color.Red;
            this.txtIP.Location = new System.Drawing.Point(54, 6);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(162, 21);
            this.txtIP.TabIndex = 1;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblIP.Location = new System.Drawing.Point(21, 9);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(18, 15);
            this.lblIP.TabIndex = 12;
            this.lblIP.Text = "IP";
            // 
            // lblPasswordRegRepeat
            // 
            this.lblPasswordRegRepeat.AutoSize = true;
            this.lblPasswordRegRepeat.Location = new System.Drawing.Point(56, 95);
            this.lblPasswordRegRepeat.Name = "lblPasswordRegRepeat";
            this.lblPasswordRegRepeat.Size = new System.Drawing.Size(117, 15);
            this.lblPasswordRegRepeat.TabIndex = 14;
            this.lblPasswordRegRepeat.Text = "Повторите пароль";
            // 
            // txtPasswordRegRepeat
            // 
            this.txtPasswordRegRepeat.Location = new System.Drawing.Point(6, 111);
            this.txtPasswordRegRepeat.Name = "txtPasswordRegRepeat";
            this.txtPasswordRegRepeat.Size = new System.Drawing.Size(195, 21);
            this.txtPasswordRegRepeat.TabIndex = 13;
            this.txtPasswordRegRepeat.UseSystemPasswordChar = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLoginReg);
            this.groupBox1.Controls.Add(this.txtLoginReg);
            this.groupBox1.Controls.Add(this.txtPasswordReg);
            this.groupBox1.Controls.Add(this.lblPasswordReg);
            this.groupBox1.Controls.Add(this.lblPasswordRegRepeat);
            this.groupBox1.Controls.Add(this.txtPasswordRegRepeat);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox1.Location = new System.Drawing.Point(281, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 145);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Регистрация";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblLoginIn);
            this.groupBox2.Controls.Add(this.txtLoginIn);
            this.groupBox2.Controls.Add(this.txtPasswordIn);
            this.groupBox2.Controls.Add(this.lvlPasswordIn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox2.Location = new System.Drawing.Point(15, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 104);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Авторизация";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "Ответ сервера";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Сообщение для отправки";
            // 
            // btn_logout
            // 
            this.btn_logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_logout.Location = new System.Drawing.Point(403, 26);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(75, 23);
            this.btn_logout.TabIndex = 22;
            this.btn_logout.Text = "Выйти";
            this.btn_logout.UseVisualStyleBackColor = true;
            this.btn_logout.Visible = false;
            this.btn_logout.Click += new System.EventHandler(this.button_Logout_Click);
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(280, 26);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(94, 23);
            this.button_Send.TabIndex = 21;
            this.button_Send.Text = "Отправить";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(6, 70);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(461, 162);
            this.textBox2.TabIndex = 20;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 26);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(255, 21);
            this.txtMessage.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Send);
            this.panel1.Controls.Add(this.btn_logout);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panel1.Location = new System.Drawing.Point(15, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 247);
            this.panel1.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(511, 435);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.btnLogIn);
            this.Name = "Form1";
            this.Text = "Сторона клиента";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLoginIn;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.TextBox txtPasswordIn;
        private System.Windows.Forms.Label lblLoginIn;
        private System.Windows.Forms.Label lvlPasswordIn;
        private System.Windows.Forms.TextBox txtLoginReg;
        private System.Windows.Forms.Label lblLoginReg;
        private System.Windows.Forms.Label lblPasswordReg;
        private System.Windows.Forms.TextBox txtPasswordReg;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPasswordRegRepeat;
        private System.Windows.Forms.TextBox txtPasswordRegRepeat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_logout;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Panel panel1;
    }
}
