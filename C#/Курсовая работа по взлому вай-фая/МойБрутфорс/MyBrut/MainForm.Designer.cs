namespace MyBrut
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.showRichTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listNet = new System.Windows.Forms.ListView();
            this.SSID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Signal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Crypt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SpecializeType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SupportedSystem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProfileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CanConnect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.ClearLog = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ButtonBrut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelShowUnUsedPasswords = new System.Windows.Forms.Label();
            this.labelShowUsed = new System.Windows.Forms.Label();
            this.labelViewPassInDict = new System.Windows.Forms.Label();
            this.labelAlsoPassInDict = new System.Windows.Forms.Label();
            this.labelUsedPassInDict = new System.Windows.Forms.Label();
            this.labelPassInDict = new System.Windows.Forms.Label();
            this.GetPasswordsButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelNameOfNetwork = new System.Windows.Forms.Label();
            this.labelMask = new System.Windows.Forms.Label();
            this.textBoxOfMask = new System.Windows.Forms.TextBox();
            this.labelSymbCount = new System.Windows.Forms.Label();
            this.textBoxMaxLength = new System.Windows.Forms.TextBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.textBoxMinLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // showRichTextBox2
            // 
            this.showRichTextBox2.Location = new System.Drawing.Point(16, 291);
            this.showRichTextBox2.Name = "showRichTextBox2";
            this.showRichTextBox2.Size = new System.Drawing.Size(877, 202);
            this.showRichTextBox2.TabIndex = 2;
            this.showRichTextBox2.Text = "";
            this.showRichTextBox2.TextChanged += new System.EventHandler(this.showRichTextBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Соединение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Connect);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 82);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "Рассоединение";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Disconnect);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 118);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 40);
            this.button4.TabIndex = 5;
            this.button4.Text = "Статус";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Status);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 151);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(133, 40);
            this.button5.TabIndex = 6;
            this.button5.Text = "Показать профиль XML";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ProfileXML);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 186);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(133, 40);
            this.button6.TabIndex = 7;
            this.button6.Text = "Удалить профиль из памяти";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.DeleteProfile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ЛОГГИРОВАНИЕ";
            // 
            // listNet
            // 
            this.listNet.AllowColumnReorder = true;
            this.listNet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SSID,
            this.Signal,
            this.Type,
            this.Crypt,
            this.SpecializeType,
            this.SupportedSystem,
            this.ProfileName,
            this.CanConnect});
            this.listNet.Location = new System.Drawing.Point(143, 12);
            this.listNet.Name = "listNet";
            this.listNet.Size = new System.Drawing.Size(884, 214);
            this.listNet.TabIndex = 10;
            this.listNet.UseCompatibleStateImageBehavior = false;
            this.listNet.View = System.Windows.Forms.View.Details;
            // 
            // SSID
            // 
            this.SSID.Text = "SSID";
            this.SSID.Width = 81;
            // 
            // Signal
            // 
            this.Signal.Text = "Качество";
            this.Signal.Width = 65;
            // 
            // Type
            // 
            this.Type.Text = "Тип безопасности";
            this.Type.Width = 105;
            // 
            // Crypt
            // 
            this.Crypt.Text = "Тип шифрования";
            this.Crypt.Width = 100;
            // 
            // SpecializeType
            // 
            this.SpecializeType.Text = "Тип сети: специальная или инфраструктура";
            this.SpecializeType.Width = 139;
            // 
            // SupportedSystem
            // 
            this.SupportedSystem.Text = "Поддерживаемая система ";
            this.SupportedSystem.Width = 127;
            // 
            // ProfileName
            // 
            this.ProfileName.Text = "Профильное имя";
            this.ProfileName.Width = 101;
            // 
            // CanConnect
            // 
            this.CanConnect.Text = "Возможность соединения";
            this.CanConnect.Width = 152;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 40);
            this.button1.TabIndex = 11;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ReShow);
            // 
            // ClearLog
            // 
            this.ClearLog.Location = new System.Drawing.Point(896, 468);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(133, 25);
            this.ClearLog.TabIndex = 12;
            this.ClearLog.Text = "Очистить лог";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // ButtonBrut
            // 
            this.ButtonBrut.Location = new System.Drawing.Point(895, 251);
            this.ButtonBrut.Name = "ButtonBrut";
            this.ButtonBrut.Size = new System.Drawing.Size(134, 40);
            this.ButtonBrut.TabIndex = 13;
            this.ButtonBrut.Text = "Начать перебор паролей к сети";
            this.ButtonBrut.UseVisualStyleBackColor = true;
            this.ButtonBrut.Click += new System.EventHandler(this.ButtonBrutClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelShowUnUsedPasswords);
            this.groupBox1.Controls.Add(this.labelShowUsed);
            this.groupBox1.Controls.Add(this.labelViewPassInDict);
            this.groupBox1.Controls.Add(this.labelAlsoPassInDict);
            this.groupBox1.Controls.Add(this.labelUsedPassInDict);
            this.groupBox1.Controls.Add(this.labelPassInDict);
            this.groupBox1.Location = new System.Drawing.Point(897, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 134);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Процесс";
            // 
            // labelShowUnUsedPasswords
            // 
            this.labelShowUnUsedPasswords.AutoSize = true;
            this.labelShowUnUsedPasswords.Location = new System.Drawing.Point(69, 109);
            this.labelShowUnUsedPasswords.MaximumSize = new System.Drawing.Size(300, 0);
            this.labelShowUnUsedPasswords.Name = "labelShowUnUsedPasswords";
            this.labelShowUnUsedPasswords.Size = new System.Drawing.Size(0, 13);
            this.labelShowUnUsedPasswords.TabIndex = 16;
            // 
            // labelShowUsed
            // 
            this.labelShowUsed.AutoSize = true;
            this.labelShowUsed.Location = new System.Drawing.Point(69, 76);
            this.labelShowUsed.MaximumSize = new System.Drawing.Size(300, 0);
            this.labelShowUsed.Name = "labelShowUsed";
            this.labelShowUsed.Size = new System.Drawing.Size(0, 13);
            this.labelShowUsed.TabIndex = 16;
            // 
            // labelViewPassInDict
            // 
            this.labelViewPassInDict.AutoSize = true;
            this.labelViewPassInDict.Location = new System.Drawing.Point(69, 41);
            this.labelViewPassInDict.MaximumSize = new System.Drawing.Size(300, 0);
            this.labelViewPassInDict.Name = "labelViewPassInDict";
            this.labelViewPassInDict.Size = new System.Drawing.Size(0, 13);
            this.labelViewPassInDict.TabIndex = 15;
            // 
            // labelAlsoPassInDict
            // 
            this.labelAlsoPassInDict.AutoSize = true;
            this.labelAlsoPassInDict.Location = new System.Drawing.Point(6, 86);
            this.labelAlsoPassInDict.Name = "labelAlsoPassInDict";
            this.labelAlsoPassInDict.Size = new System.Drawing.Size(59, 13);
            this.labelAlsoPassInDict.TabIndex = 2;
            this.labelAlsoPassInDict.Text = "Осталось:";
            // 
            // labelUsedPassInDict
            // 
            this.labelUsedPassInDict.AutoSize = true;
            this.labelUsedPassInDict.Location = new System.Drawing.Point(6, 54);
            this.labelUsedPassInDict.Name = "labelUsedPassInDict";
            this.labelUsedPassInDict.Size = new System.Drawing.Size(66, 13);
            this.labelUsedPassInDict.TabIndex = 1;
            this.labelUsedPassInDict.Text = "Перебрано:";
            // 
            // labelPassInDict
            // 
            this.labelPassInDict.AutoSize = true;
            this.labelPassInDict.Location = new System.Drawing.Point(6, 20);
            this.labelPassInDict.Name = "labelPassInDict";
            this.labelPassInDict.Size = new System.Drawing.Size(108, 13);
            this.labelPassInDict.TabIndex = 0;
            this.labelPassInDict.Text = "Паролей в словаре:";
            // 
            // GetPasswordsButton
            // 
            this.GetPasswordsButton.Location = new System.Drawing.Point(895, 229);
            this.GetPasswordsButton.Name = "GetPasswordsButton";
            this.GetPasswordsButton.Size = new System.Drawing.Size(134, 20);
            this.GetPasswordsButton.TabIndex = 15;
            this.GetPasswordsButton.Text = "Загрузить пароли";
            this.GetPasswordsButton.UseVisualStyleBackColor = true;
            this.GetPasswordsButton.Click += new System.EventHandler(this.GetPasswordsButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelNameOfNetwork);
            this.groupBox2.Location = new System.Drawing.Point(896, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 31);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сеть";
            // 
            // labelNameOfNetwork
            // 
            this.labelNameOfNetwork.AutoSize = true;
            this.labelNameOfNetwork.ForeColor = System.Drawing.Color.Red;
            this.labelNameOfNetwork.Location = new System.Drawing.Point(12, 14);
            this.labelNameOfNetwork.MaximumSize = new System.Drawing.Size(300, 0);
            this.labelNameOfNetwork.Name = "labelNameOfNetwork";
            this.labelNameOfNetwork.Size = new System.Drawing.Size(0, 13);
            this.labelNameOfNetwork.TabIndex = 15;
            this.labelNameOfNetwork.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelMask
            // 
            this.labelMask.AutoSize = true;
            this.labelMask.Location = new System.Drawing.Point(12, 232);
            this.labelMask.Name = "labelMask";
            this.labelMask.Size = new System.Drawing.Size(254, 13);
            this.labelMask.TabIndex = 18;
            this.labelMask.Text = "Я знаю маску пароля(элементы через запятую):";
            // 
            // textBoxOfMask
            // 
            this.textBoxOfMask.Location = new System.Drawing.Point(272, 231);
            this.textBoxOfMask.Name = "textBoxOfMask";
            this.textBoxOfMask.Size = new System.Drawing.Size(422, 20);
            this.textBoxOfMask.TabIndex = 19;
            this.textBoxOfMask.Text = "qwe,rty,123,7";
            // 
            // labelSymbCount
            // 
            this.labelSymbCount.AutoSize = true;
            this.labelSymbCount.Location = new System.Drawing.Point(13, 252);
            this.labelSymbCount.Name = "labelSymbCount";
            this.labelSymbCount.Size = new System.Drawing.Size(201, 13);
            this.labelSymbCount.TabIndex = 20;
            this.labelSymbCount.Text = "Максимальное количество символов:";
            // 
            // textBoxMaxLength
            // 
            this.textBoxMaxLength.Location = new System.Drawing.Point(272, 251);
            this.textBoxMaxLength.Name = "textBoxMaxLength";
            this.textBoxMaxLength.Size = new System.Drawing.Size(70, 20);
            this.textBoxMaxLength.TabIndex = 21;
            this.textBoxMaxLength.Text = "10";
            this.textBoxMaxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(700, 229);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(188, 43);
            this.GenerateButton.TabIndex = 22;
            this.GenerateButton.Text = "Сгенерировать возможные пароли и выгрзить на экран";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // textBoxMinLength
            // 
            this.textBoxMinLength.Location = new System.Drawing.Point(624, 252);
            this.textBoxMinLength.Name = "textBoxMinLength";
            this.textBoxMinLength.Size = new System.Drawing.Size(70, 20);
            this.textBoxMinLength.TabIndex = 24;
            this.textBoxMinLength.Text = "6";
            this.textBoxMinLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Минимальное количество символов:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1034, 511);
            this.Controls.Add(this.textBoxMinLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.textBoxMaxLength);
            this.Controls.Add(this.labelSymbCount);
            this.Controls.Add(this.textBoxOfMask);
            this.Controls.Add(this.labelMask);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GetPasswordsButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonBrut);
            this.Controls.Add(this.ClearLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listNet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.showRichTextBox2);
            this.MaximumSize = new System.Drawing.Size(1050, 550);
            this.MinimumSize = new System.Drawing.Size(1050, 550);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Панель управления Wi-Fi";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox showRichTextBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listNet;
        private System.Windows.Forms.ColumnHeader SSID;
        private System.Windows.Forms.ColumnHeader Signal;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Crypt;
        private System.Windows.Forms.ColumnHeader SpecializeType;
        private System.Windows.Forms.ColumnHeader SupportedSystem;
        private System.Windows.Forms.ColumnHeader ProfileName;
        private System.Windows.Forms.ColumnHeader CanConnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ButtonBrut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelShowUnUsedPasswords;
        private System.Windows.Forms.Label labelShowUsed;
        private System.Windows.Forms.Label labelViewPassInDict;
        private System.Windows.Forms.Label labelAlsoPassInDict;
        private System.Windows.Forms.Label labelUsedPassInDict;
        private System.Windows.Forms.Label labelPassInDict;
        private System.Windows.Forms.Button GetPasswordsButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelNameOfNetwork;
        private System.Windows.Forms.Label labelMask;
        private System.Windows.Forms.TextBox textBoxOfMask;
        private System.Windows.Forms.Label labelSymbCount;
        private System.Windows.Forms.TextBox textBoxMaxLength;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.TextBox textBoxMinLength;
        private System.Windows.Forms.Label label2;
    }
}

