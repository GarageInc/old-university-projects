namespace WinFormCharpWebCam
{
    //Design by Pongsakorn Poosankam
    partial class mainWinForm
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
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.imgCapture1 = new System.Windows.Forms.PictureBox();
            this.bntStart = new System.Windows.Forms.Button();
            this.bntStop = new System.Windows.Forms.Button();
            this.bntContinue = new System.Windows.Forms.Button();
            this.bntCapture = new System.Windows.Forms.Button();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntVideoFormat = new System.Windows.Forms.Button();
            this.bntVideoSource = new System.Windows.Forms.Button();
            this.bntCamTimer = new System.Windows.Forms.Button();
            this.imgCapture2 = new System.Windows.Forms.PictureBox();
            this.imgCapture4 = new System.Windows.Forms.PictureBox();
            this.imgCapture5 = new System.Windows.Forms.PictureBox();
            this.imgCapture3 = new System.Windows.Forms.PictureBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.SavePhotos = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture3)).BeginInit();
            this.SuspendLayout();
            // 
            // imgVideo
            // 
            this.imgVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgVideo.Location = new System.Drawing.Point(12, 21);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(247, 202);
            this.imgVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgVideo.TabIndex = 0;
            this.imgVideo.TabStop = false;
            // 
            // imgCapture1
            // 
            this.imgCapture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture1.Location = new System.Drawing.Point(339, 21);
            this.imgCapture1.Name = "imgCapture1";
            this.imgCapture1.Size = new System.Drawing.Size(230, 202);
            this.imgCapture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCapture1.TabIndex = 1;
            this.imgCapture1.TabStop = false;
            // 
            // bntStart
            // 
            this.bntStart.Location = new System.Drawing.Point(12, 229);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(75, 46);
            this.bntStart.TabIndex = 2;
            this.bntStart.Text = "Начать";
            this.bntStart.UseVisualStyleBackColor = true;
            this.bntStart.Click += new System.EventHandler(this.bntStart_Click);
            // 
            // bntStop
            // 
            this.bntStop.Location = new System.Drawing.Point(93, 229);
            this.bntStop.Name = "bntStop";
            this.bntStop.Size = new System.Drawing.Size(72, 46);
            this.bntStop.TabIndex = 3;
            this.bntStop.Text = "Stop";
            this.bntStop.UseVisualStyleBackColor = true;
            this.bntStop.Click += new System.EventHandler(this.bntStop_Click);
            // 
            // bntContinue
            // 
            this.bntContinue.Location = new System.Drawing.Point(171, 229);
            this.bntContinue.Name = "bntContinue";
            this.bntContinue.Size = new System.Drawing.Size(88, 46);
            this.bntContinue.TabIndex = 4;
            this.bntContinue.Text = "Продолжить";
            this.bntContinue.UseVisualStyleBackColor = true;
            this.bntContinue.Click += new System.EventHandler(this.bntContinue_Click);
            // 
            // bntCapture
            // 
            this.bntCapture.Location = new System.Drawing.Point(339, 229);
            this.bntCapture.Name = "bntCapture";
            this.bntCapture.Size = new System.Drawing.Size(84, 46);
            this.bntCapture.TabIndex = 5;
            this.bntCapture.Text = "Взять изображение";
            this.bntCapture.UseVisualStyleBackColor = true;
            this.bntCapture.Click += new System.EventHandler(this.bntCapture_Click);
            // 
            // bntSave
            // 
            this.bntSave.Location = new System.Drawing.Point(490, 229);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(79, 46);
            this.bntSave.TabIndex = 6;
            this.bntSave.Text = "Сохранить изображение";
            this.bntSave.UseVisualStyleBackColor = true;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // bntVideoFormat
            // 
            this.bntVideoFormat.Location = new System.Drawing.Point(12, 281);
            this.bntVideoFormat.Name = "bntVideoFormat";
            this.bntVideoFormat.Size = new System.Drawing.Size(247, 50);
            this.bntVideoFormat.TabIndex = 7;
            this.bntVideoFormat.Text = "Параметры камеры";
            this.bntVideoFormat.UseVisualStyleBackColor = true;
            this.bntVideoFormat.Click += new System.EventHandler(this.bntVideoFormat_Click);
            // 
            // bntVideoSource
            // 
            this.bntVideoSource.Location = new System.Drawing.Point(339, 281);
            this.bntVideoSource.Name = "bntVideoSource";
            this.bntVideoSource.Size = new System.Drawing.Size(230, 50);
            this.bntVideoSource.TabIndex = 8;
            this.bntVideoSource.Text = "Параметры видео";
            this.bntVideoSource.UseVisualStyleBackColor = true;
            this.bntVideoSource.Click += new System.EventHandler(this.bntVideoSource_Click);
            // 
            // bntCamTimer
            // 
            this.bntCamTimer.Location = new System.Drawing.Point(624, 281);
            this.bntCamTimer.Name = "bntCamTimer";
            this.bntCamTimer.Size = new System.Drawing.Size(157, 46);
            this.bntCamTimer.TabIndex = 1;
            this.bntCamTimer.Text = "Делать фото!";
            this.bntCamTimer.UseVisualStyleBackColor = true;
            this.bntCamTimer.Click += new System.EventHandler(this.bntCamTimer_Click);
            // 
            // imgCapture2
            // 
            this.imgCapture2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture2.Location = new System.Drawing.Point(624, 21);
            this.imgCapture2.Name = "imgCapture2";
            this.imgCapture2.Size = new System.Drawing.Size(157, 120);
            this.imgCapture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCapture2.TabIndex = 10;
            this.imgCapture2.TabStop = false;
            this.imgCapture2.Click += new System.EventHandler(this.imgCapture2_Click);
            // 
            // imgCapture4
            // 
            this.imgCapture4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture4.Location = new System.Drawing.Point(624, 147);
            this.imgCapture4.Name = "imgCapture4";
            this.imgCapture4.Size = new System.Drawing.Size(157, 120);
            this.imgCapture4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCapture4.TabIndex = 11;
            this.imgCapture4.TabStop = false;
            this.imgCapture4.Click += new System.EventHandler(this.imgCapture4_Click);
            // 
            // imgCapture5
            // 
            this.imgCapture5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture5.Location = new System.Drawing.Point(820, 147);
            this.imgCapture5.Name = "imgCapture5";
            this.imgCapture5.Size = new System.Drawing.Size(157, 120);
            this.imgCapture5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCapture5.TabIndex = 12;
            this.imgCapture5.TabStop = false;
            this.imgCapture5.Click += new System.EventHandler(this.imgCapture5_Click);
            // 
            // imgCapture3
            // 
            this.imgCapture3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture3.Location = new System.Drawing.Point(820, 21);
            this.imgCapture3.Name = "imgCapture3";
            this.imgCapture3.Size = new System.Drawing.Size(157, 120);
            this.imgCapture3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCapture3.TabIndex = 13;
            this.imgCapture3.TabStop = false;
            this.imgCapture3.Click += new System.EventHandler(this.imgCapture3_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeLabel.Location = new System.Drawing.Point(726, 340);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(150, 30);
            this.timeLabel.TabIndex = 14;
            // 
            // SavePhotos
            // 
            this.SavePhotos.Location = new System.Drawing.Point(820, 281);
            this.SavePhotos.Name = "SavePhotos";
            this.SavePhotos.Size = new System.Drawing.Size(152, 46);
            this.SavePhotos.TabIndex = 15;
            this.SavePhotos.Text = "Сохранить фотографии!";
            this.SavePhotos.UseVisualStyleBackColor = true;
            this.SavePhotos.Click += new System.EventHandler(this.SavePhotos_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // mainWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 379);
            this.Controls.Add(this.SavePhotos);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.imgCapture3);
            this.Controls.Add(this.imgCapture5);
            this.Controls.Add(this.imgCapture4);
            this.Controls.Add(this.imgCapture2);
            this.Controls.Add(this.bntCamTimer);
            this.Controls.Add(this.bntVideoSource);
            this.Controls.Add(this.bntVideoFormat);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntCapture);
            this.Controls.Add(this.bntContinue);
            this.Controls.Add(this.bntStop);
            this.Controls.Add(this.bntStart);
            this.Controls.Add(this.imgCapture1);
            this.Controls.Add(this.imgVideo);
            this.Name = "mainWinForm";
            this.Text = "ВебКамера Фатхеевых";
            this.Load += new System.EventHandler(this.mainWinForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.Button bntStop;
        private System.Windows.Forms.Button bntContinue;
        private System.Windows.Forms.Button bntCapture;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntVideoFormat;
        private System.Windows.Forms.Button bntVideoSource;
        private System.Windows.Forms.Button bntCamTimer;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button SavePhotos;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.PictureBox imgCapture1;
        public System.Windows.Forms.PictureBox imgCapture2;
        public System.Windows.Forms.PictureBox imgCapture4;
        public System.Windows.Forms.PictureBox imgCapture5;
        public System.Windows.Forms.PictureBox imgCapture3;
        public System.Windows.Forms.PictureBox imgVideo;
    }
}

