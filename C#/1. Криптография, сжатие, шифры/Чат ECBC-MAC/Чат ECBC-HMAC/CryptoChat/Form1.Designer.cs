namespace CryptoChat
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.showMessagesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.firstMessageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.keyBox = new System.Windows.Forms.TextBox();
            this.secretKeyBox = new System.Windows.Forms.TextBox();
            this.cryptedMessageTextBox = new System.Windows.Forms.RichTextBox();
            this.buttonGenerateSecretKey = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(27, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Отправить сообщение c подписью";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonNewMessageAndListen_User1);
            // 
            // showMessagesRichTextBox
            // 
            this.showMessagesRichTextBox.Location = new System.Drawing.Point(24, 27);
            this.showMessagesRichTextBox.Name = "showMessagesRichTextBox";
            this.showMessagesRichTextBox.Size = new System.Drawing.Size(411, 207);
            this.showMessagesRichTextBox.TabIndex = 1;
            this.showMessagesRichTextBox.Text = "";
            // 
            // firstMessageRichTextBox
            // 
            this.firstMessageRichTextBox.Location = new System.Drawing.Point(27, 224);
            this.firstMessageRichTextBox.Name = "firstMessageRichTextBox";
            this.firstMessageRichTextBox.Size = new System.Drawing.Size(246, 186);
            this.firstMessageRichTextBox.TabIndex = 2;
            this.firstMessageRichTextBox.Text = "Тут какое-то шифруемое сообщение! ";
            // 
            // keyBox
            // 
            this.keyBox.Location = new System.Drawing.Point(27, 27);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(246, 20);
            this.keyBox.TabIndex = 3;
            this.keyBox.Text = "1234";
            this.keyBox.TextChanged += new System.EventHandler(this.keyBox_TextChanged);
            // 
            // secretKeyBox
            // 
            this.secretKeyBox.Location = new System.Drawing.Point(27, 109);
            this.secretKeyBox.Name = "secretKeyBox";
            this.secretKeyBox.Size = new System.Drawing.Size(246, 20);
            this.secretKeyBox.TabIndex = 6;
            // 
            // cryptedMessageTextBox
            // 
            this.cryptedMessageTextBox.Location = new System.Drawing.Point(24, 301);
            this.cryptedMessageTextBox.Name = "cryptedMessageTextBox";
            this.cryptedMessageTextBox.Size = new System.Drawing.Size(411, 109);
            this.cryptedMessageTextBox.TabIndex = 7;
            this.cryptedMessageTextBox.Text = "";
            // 
            // buttonGenerateSecretKey
            // 
            this.buttonGenerateSecretKey.Location = new System.Drawing.Point(27, 53);
            this.buttonGenerateSecretKey.Name = "buttonGenerateSecretKey";
            this.buttonGenerateSecretKey.Size = new System.Drawing.Size(246, 50);
            this.buttonGenerateSecretKey.TabIndex = 9;
            this.buttonGenerateSecretKey.Text = "Генерация секретного ключа по протоколу Диффи-Хеллмана";
            this.buttonGenerateSecretKey.UseVisualStyleBackColor = true;
            this.buttonGenerateSecretKey.Click += new System.EventHandler(this.buttonGenerateSecretKey_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Основа для ключа:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.firstMessageRichTextBox);
            this.panel1.Controls.Add(this.buttonGenerateSecretKey);
            this.panel1.Controls.Add(this.keyBox);
            this.panel1.Controls.Add(this.secretKeyBox);
            this.panel1.Location = new System.Drawing.Point(44, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 431);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(27, 201);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(254, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "изменить сообщение(будто вмешалась ЕВА)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cryptedMessageTextBox);
            this.panel2.Controls.Add(this.showMessagesRichTextBox);
            this.panel2.Location = new System.Drawing.Point(406, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(469, 430);
            this.panel2.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Верифицированное сообщение для сервера:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Подпись ECBC-PMAC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(623, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "СЕРВЕР-БОБ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "КЛИЕНТ-АЛИСА";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 531);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Эмулятор общения клиента чата с сервером";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox showMessagesRichTextBox;
        private System.Windows.Forms.RichTextBox firstMessageRichTextBox;
        private System.Windows.Forms.TextBox keyBox;
        private System.Windows.Forms.TextBox secretKeyBox;
        private System.Windows.Forms.RichTextBox cryptedMessageTextBox;
        private System.Windows.Forms.Button buttonGenerateSecretKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

