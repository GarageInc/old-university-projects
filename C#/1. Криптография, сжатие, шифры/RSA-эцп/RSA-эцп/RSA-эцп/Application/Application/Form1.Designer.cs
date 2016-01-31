namespace ApplicationMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.generateSimpleButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.checkTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.newSingTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ourMessageTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.signTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.sendedMessageTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dAliceTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nAliceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.eAliceTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.aliceMessageTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.qAliceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pAliceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.dBobTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.eBobTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bobMessageTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.generateButton2 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.nBobTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.qBobTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.pBobTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // generateSimpleButton
            // 
            this.generateSimpleButton.Location = new System.Drawing.Point(538, 21);
            this.generateSimpleButton.Name = "generateSimpleButton";
            this.generateSimpleButton.Size = new System.Drawing.Size(214, 44);
            this.generateSimpleButton.TabIndex = 87;
            this.generateSimpleButton.Text = "Сгенерировать простые числа для Алисы и Боба";
            this.generateSimpleButton.UseVisualStyleBackColor = true;
            this.generateSimpleButton.Click += new System.EventHandler(this.generateSimpleButton_Click);
            // 
            // checkButton
            // 
            this.checkButton.Location = new System.Drawing.Point(516, 317);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(256, 29);
            this.checkButton.TabIndex = 86;
            this.checkButton.Text = "Проверить ЭЦП(изменила ЕВА сообщение?)";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // checkTextBox
            // 
            this.checkTextBox.Enabled = false;
            this.checkTextBox.Location = new System.Drawing.Point(544, 404);
            this.checkTextBox.Name = "checkTextBox";
            this.checkTextBox.Size = new System.Drawing.Size(208, 20);
            this.checkTextBox.TabIndex = 85;
            this.checkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.checkTextBox.TextChanged += new System.EventHandler(this.checkTextBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(390, 407);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 13);
            this.label11.TabIndex = 84;
            this.label11.Text = "Изменила ЕВА сообщение?";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // newSingTextBox
            // 
            this.newSingTextBox.Location = new System.Drawing.Point(544, 378);
            this.newSingTextBox.Name = "newSingTextBox";
            this.newSingTextBox.Size = new System.Drawing.Size(208, 20);
            this.newSingTextBox.TabIndex = 83;
            this.newSingTextBox.TextChanged += new System.EventHandler(this.newSingTextBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(410, 352);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 13);
            this.label12.TabIndex = 82;
            this.label12.Text = "Сообщение исходное m:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // ourMessageTextBox
            // 
            this.ourMessageTextBox.Location = new System.Drawing.Point(544, 352);
            this.ourMessageTextBox.Name = "ourMessageTextBox";
            this.ourMessageTextBox.Size = new System.Drawing.Size(208, 20);
            this.ourMessageTextBox.TabIndex = 81;
            this.ourMessageTextBox.TextChanged += new System.EventHandler(this.ourMessageTextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(370, 382);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(172, 13);
            this.label13.TabIndex = 80;
            this.label13.Text = "Сообщение расшифрованное m\':";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // signTextBox
            // 
            this.signTextBox.Enabled = false;
            this.signTextBox.Location = new System.Drawing.Point(544, 291);
            this.signTextBox.Name = "signTextBox";
            this.signTextBox.Size = new System.Drawing.Size(204, 20);
            this.signTextBox.TabIndex = 79;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(465, 294);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "Подпись с:";
            // 
            // sendedMessageTextBox
            // 
            this.sendedMessageTextBox.Location = new System.Drawing.Point(544, 265);
            this.sendedMessageTextBox.Name = "sendedMessageTextBox";
            this.sendedMessageTextBox.Size = new System.Drawing.Size(204, 20);
            this.sendedMessageTextBox.TabIndex = 77;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(462, 268);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 76;
            this.label10.Text = "Сообщение m:";
            // 
            // dAliceTextBox
            // 
            this.dAliceTextBox.Location = new System.Drawing.Point(135, 223);
            this.dAliceTextBox.Name = "dAliceTextBox";
            this.dAliceTextBox.Size = new System.Drawing.Size(204, 20);
            this.dAliceTextBox.TabIndex = 71;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-1, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "Приавтный ключ d = prA:";
            // 
            // nAliceTextBox
            // 
            this.nAliceTextBox.Location = new System.Drawing.Point(135, 118);
            this.nAliceTextBox.Name = "nAliceTextBox";
            this.nAliceTextBox.Size = new System.Drawing.Size(204, 20);
            this.nAliceTextBox.TabIndex = 69;
            this.nAliceTextBox.TextChanged += new System.EventHandler(this.nTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 68;
            this.label4.Text = "Na:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // eAliceTextBox
            // 
            this.eAliceTextBox.Location = new System.Drawing.Point(135, 196);
            this.eAliceTextBox.Name = "eAliceTextBox";
            this.eAliceTextBox.Size = new System.Drawing.Size(204, 20);
            this.eAliceTextBox.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Публ.ключ e = puA:";
            // 
            // aliceMessageTextBox
            // 
            this.aliceMessageTextBox.Location = new System.Drawing.Point(137, 273);
            this.aliceMessageTextBox.Name = "aliceMessageTextBox";
            this.aliceMessageTextBox.Size = new System.Drawing.Size(202, 20);
            this.aliceMessageTextBox.TabIndex = 65;
            this.aliceMessageTextBox.Text = "Сообщение от Алисы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Сообщение m:";
            // 
            // qAliceTextBox
            // 
            this.qAliceTextBox.Location = new System.Drawing.Point(135, 170);
            this.qAliceTextBox.Name = "qAliceTextBox";
            this.qAliceTextBox.Size = new System.Drawing.Size(204, 20);
            this.qAliceTextBox.TabIndex = 63;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Простое число q:";
            // 
            // pAliceTextBox
            // 
            this.pAliceTextBox.Location = new System.Drawing.Point(137, 144);
            this.pAliceTextBox.Name = "pAliceTextBox";
            this.pAliceTextBox.Size = new System.Drawing.Size(202, 20);
            this.pAliceTextBox.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "Простое число p:";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(137, 310);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(202, 30);
            this.generateButton.TabIndex = 59;
            this.generateButton.Text = "Отправить сообщение --->";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.ШифровкаОтАлисы);
            // 
            // dBobTextBox
            // 
            this.dBobTextBox.Location = new System.Drawing.Point(1012, 224);
            this.dBobTextBox.Name = "dBobTextBox";
            this.dBobTextBox.Size = new System.Drawing.Size(204, 20);
            this.dBobTextBox.TabIndex = 94;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(898, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 93;
            this.label6.Text = "Прив.ключа d = prB:";
            // 
            // eBobTextBox
            // 
            this.eBobTextBox.Location = new System.Drawing.Point(1012, 197);
            this.eBobTextBox.Name = "eBobTextBox";
            this.eBobTextBox.Size = new System.Drawing.Size(204, 20);
            this.eBobTextBox.TabIndex = 92;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(902, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 91;
            this.label7.Text = "Публ. ключ e = prB:";
            // 
            // bobMessageTextBox
            // 
            this.bobMessageTextBox.Location = new System.Drawing.Point(1014, 274);
            this.bobMessageTextBox.Name = "bobMessageTextBox";
            this.bobMessageTextBox.Size = new System.Drawing.Size(202, 20);
            this.bobMessageTextBox.TabIndex = 90;
            this.bobMessageTextBox.Text = "Сообщение от Боба";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(929, 277);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 89;
            this.label14.Text = "Сообщение m:";
            // 
            // generateButton2
            // 
            this.generateButton2.Location = new System.Drawing.Point(1014, 311);
            this.generateButton2.Name = "generateButton2";
            this.generateButton2.Size = new System.Drawing.Size(202, 30);
            this.generateButton2.TabIndex = 88;
            this.generateButton2.Text = "<---Отправить сообщение";
            this.generateButton2.UseVisualStyleBackColor = true;
            this.generateButton2.Click += new System.EventHandler(this.ШифровкаОтБоба);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(202, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 95;
            this.label15.Text = "АЛИСА";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1098, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 96;
            this.label16.Text = "БОБ";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(374, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 93);
            this.panel1.TabIndex = 97;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(569, 249);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(167, 13);
            this.label17.TabIndex = 98;
            this.label17.Text = "ЕВА(может менять сообщение)";
            // 
            // nBobTextBox
            // 
            this.nBobTextBox.Location = new System.Drawing.Point(1012, 118);
            this.nBobTextBox.Name = "nBobTextBox";
            this.nBobTextBox.Size = new System.Drawing.Size(204, 20);
            this.nBobTextBox.TabIndex = 104;
            this.nBobTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(981, 121);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 13);
            this.label18.TabIndex = 103;
            this.label18.Text = "Nb:";
            // 
            // qBobTextBox
            // 
            this.qBobTextBox.Location = new System.Drawing.Point(1012, 171);
            this.qBobTextBox.Name = "qBobTextBox";
            this.qBobTextBox.Size = new System.Drawing.Size(204, 20);
            this.qBobTextBox.TabIndex = 102;
            this.qBobTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(911, 173);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(94, 13);
            this.label19.TabIndex = 101;
            this.label19.Text = "Простое число q:";
            // 
            // pBobTextBox
            // 
            this.pBobTextBox.Location = new System.Drawing.Point(1012, 145);
            this.pBobTextBox.Name = "pBobTextBox";
            this.pBobTextBox.Size = new System.Drawing.Size(202, 20);
            this.pBobTextBox.TabIndex = 100;
            this.pBobTextBox.TextChanged += new System.EventHandler(this.pBobTextBox_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(914, 151);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 13);
            this.label20.TabIndex = 99;
            this.label20.Text = "Простое число p:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 452);
            this.Controls.Add(this.nBobTextBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.qBobTextBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.pBobTextBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dBobTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.eBobTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bobMessageTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.generateButton2);
            this.Controls.Add(this.generateSimpleButton);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.checkTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.newSingTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ourMessageTextBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.signTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.sendedMessageTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dAliceTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nAliceTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eAliceTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.aliceMessageTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.qAliceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pAliceTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generateButton);
            this.Name = "Form1";
            this.Text = "Электронная цифровая подпись RSA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateSimpleButton;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.TextBox checkTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox newSingTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ourMessageTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox signTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox sendedMessageTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox dAliceTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox nAliceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox eAliceTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox aliceMessageTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox qAliceTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pAliceTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.TextBox dBobTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox eBobTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bobMessageTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button generateButton2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox nBobTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox qBobTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox pBobTextBox;
        private System.Windows.Forms.Label label20;
    }
}

