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
            this.secondMessageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.secretKeyBox = new System.Windows.Forms.TextBox();
            this.decyptedMessageTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGenerateSecretKey = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Отправить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonNewMessageAndListen_User1);
            // 
            // showMessagesRichTextBox
            // 
            this.showMessagesRichTextBox.Location = new System.Drawing.Point(285, 12);
            this.showMessagesRichTextBox.Name = "showMessagesRichTextBox";
            this.showMessagesRichTextBox.Size = new System.Drawing.Size(411, 215);
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
            this.keyBox.Location = new System.Drawing.Point(33, 24);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(100, 20);
            this.keyBox.TabIndex = 3;
            this.keyBox.Text = "1234";
            // 
            // secondMessageRichTextBox
            // 
            this.secondMessageRichTextBox.Location = new System.Drawing.Point(702, 225);
            this.secondMessageRichTextBox.Name = "secondMessageRichTextBox";
            this.secondMessageRichTextBox.Size = new System.Drawing.Size(246, 109);
            this.secondMessageRichTextBox.TabIndex = 5;
            this.secondMessageRichTextBox.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(702, 187);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Отправить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ButtonNewMessageAndListen_User2);
            // 
            // secretKeyBox
            // 
            this.secretKeyBox.Location = new System.Drawing.Point(33, 79);
            this.secretKeyBox.Name = "secretKeyBox";
            this.secretKeyBox.Size = new System.Drawing.Size(100, 20);
            this.secretKeyBox.TabIndex = 6;
            // 
            // decyptedMessageTextBox
            // 
            this.decyptedMessageTextBox.Location = new System.Drawing.Point(285, 286);
            this.decyptedMessageTextBox.Name = "decyptedMessageTextBox";
            this.decyptedMessageTextBox.Size = new System.Drawing.Size(411, 48);
            this.decyptedMessageTextBox.TabIndex = 7;
            this.decyptedMessageTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Зашифрованный текст:";
            // 
            // buttonGenerateSecretKey
            // 
            this.buttonGenerateSecretKey.Location = new System.Drawing.Point(33, 50);
            this.buttonGenerateSecretKey.Name = "buttonGenerateSecretKey";
            this.buttonGenerateSecretKey.Size = new System.Drawing.Size(183, 23);
            this.buttonGenerateSecretKey.TabIndex = 9;
            this.buttonGenerateSecretKey.Text = "Генерация секретного ключа";
            this.buttonGenerateSecretKey.UseVisualStyleBackColor = true;
            this.buttonGenerateSecretKey.Click += new System.EventHandler(this.buttonGenerateSecretKey_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Обычный какой-то общий текст-ключ:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 346);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGenerateSecretKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decyptedMessageTextBox);
            this.Controls.Add(this.secretKeyBox);
            this.Controls.Add(this.secondMessageRichTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.firstMessageRichTextBox);
            this.Controls.Add(this.showMessagesRichTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Мини-чат";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox showMessagesRichTextBox;
        private System.Windows.Forms.RichTextBox firstMessageRichTextBox;
        private System.Windows.Forms.TextBox keyBox;
        private System.Windows.Forms.RichTextBox secondMessageRichTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox secretKeyBox;
        private System.Windows.Forms.RichTextBox decyptedMessageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGenerateSecretKey;
        private System.Windows.Forms.Label label2;
    }
}

