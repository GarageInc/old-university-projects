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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Отправить зашифрованное сообщение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonNewMessageAndListen_User1);
            // 
            // showMessagesRichTextBox
            // 
            this.showMessagesRichTextBox.Location = new System.Drawing.Point(285, 12);
            this.showMessagesRichTextBox.Name = "showMessagesRichTextBox";
            this.showMessagesRichTextBox.Size = new System.Drawing.Size(411, 207);
            this.showMessagesRichTextBox.TabIndex = 1;
            this.showMessagesRichTextBox.Text = "";
            // 
            // firstMessageRichTextBox
            // 
            this.firstMessageRichTextBox.Location = new System.Drawing.Point(33, 225);
            this.firstMessageRichTextBox.Name = "firstMessageRichTextBox";
            this.firstMessageRichTextBox.Size = new System.Drawing.Size(246, 109);
            this.firstMessageRichTextBox.TabIndex = 2;
            this.firstMessageRichTextBox.Text = "";
            // 
            // keyBox
            // 
            this.keyBox.Location = new System.Drawing.Point(33, 28);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(246, 20);
            this.keyBox.TabIndex = 3;
            this.keyBox.Text = "1234";
            this.keyBox.TextChanged += new System.EventHandler(this.keyBox_TextChanged);
            // 
            // secretKeyBox
            // 
            this.secretKeyBox.Location = new System.Drawing.Point(33, 110);
            this.secretKeyBox.Name = "secretKeyBox";
            this.secretKeyBox.Size = new System.Drawing.Size(246, 20);
            this.secretKeyBox.TabIndex = 6;
            // 
            // cryptedMessageTextBox
            // 
            this.cryptedMessageTextBox.Location = new System.Drawing.Point(285, 225);
            this.cryptedMessageTextBox.Name = "cryptedMessageTextBox";
            this.cryptedMessageTextBox.Size = new System.Drawing.Size(411, 109);
            this.cryptedMessageTextBox.TabIndex = 7;
            this.cryptedMessageTextBox.Text = "";
            // 
            // buttonGenerateSecretKey
            // 
            this.buttonGenerateSecretKey.Location = new System.Drawing.Point(33, 54);
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
            this.label2.Location = new System.Drawing.Point(30, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Основа для ключа:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 346);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGenerateSecretKey);
            this.Controls.Add(this.cryptedMessageTextBox);
            this.Controls.Add(this.secretKeyBox);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.firstMessageRichTextBox);
            this.Controls.Add(this.showMessagesRichTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Мини-чат";
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

