namespace Shop
{
    partial class MainClientWindow
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.productsPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.addToBuckerButton = new System.Windows.Forms.Button();
            this.productGrid = new System.Windows.Forms.DataGridView();
            this.bucketPage = new System.Windows.Forms.TabPage();
            this.deleteFromBucket = new System.Windows.Forms.Button();
            this.doPurchase = new System.Windows.Forms.Button();
            this.bucketGrid = new System.Windows.Forms.DataGridView();
            this.purchasePage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.unCheckPurchase = new System.Windows.Forms.Button();
            this.purchaseGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editUserInfoButton = new System.Windows.Forms.Button();
            this.userInfoTextBox = new System.Windows.Forms.RichTextBox();
            this.tabControl.SuspendLayout();
            this.productsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productGrid)).BeginInit();
            this.bucketPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bucketGrid)).BeginInit();
            this.purchasePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.productsPage);
            this.tabControl.Controls.Add(this.bucketPage);
            this.tabControl.Controls.Add(this.purchasePage);
            this.tabControl.Location = new System.Drawing.Point(7, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1172, 452);
            this.tabControl.TabIndex = 14;
            // 
            // productsPage
            // 
            this.productsPage.Controls.Add(this.label4);
            this.productsPage.Controls.Add(this.countTextBox);
            this.productsPage.Controls.Add(this.addToBuckerButton);
            this.productsPage.Controls.Add(this.productGrid);
            this.productsPage.Location = new System.Drawing.Point(4, 22);
            this.productsPage.Name = "productsPage";
            this.productsPage.Padding = new System.Windows.Forms.Padding(3);
            this.productsPage.Size = new System.Drawing.Size(1164, 426);
            this.productsPage.TabIndex = 0;
            this.productsPage.Text = "Магазин";
            this.productsPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(638, 374);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Количество";
            // 
            // countTextBox
            // 
            this.countTextBox.Location = new System.Drawing.Point(718, 371);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.Size = new System.Drawing.Size(267, 20);
            this.countTextBox.TabIndex = 22;
            this.countTextBox.Text = "0";
            this.countTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // addToBuckerButton
            // 
            this.addToBuckerButton.Location = new System.Drawing.Point(992, 369);
            this.addToBuckerButton.Name = "addToBuckerButton";
            this.addToBuckerButton.Size = new System.Drawing.Size(166, 23);
            this.addToBuckerButton.TabIndex = 15;
            this.addToBuckerButton.Text = "Добавить в корзину";
            this.addToBuckerButton.UseVisualStyleBackColor = true;
            this.addToBuckerButton.Click += new System.EventHandler(this.addToBuckerButton_Click);
            // 
            // productGrid
            // 
            this.productGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.productGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productGrid.Location = new System.Drawing.Point(6, 6);
            this.productGrid.Name = "productGrid";
            this.productGrid.Size = new System.Drawing.Size(1152, 335);
            this.productGrid.TabIndex = 1;
            // 
            // bucketPage
            // 
            this.bucketPage.Controls.Add(this.deleteFromBucket);
            this.bucketPage.Controls.Add(this.doPurchase);
            this.bucketPage.Controls.Add(this.bucketGrid);
            this.bucketPage.Location = new System.Drawing.Point(4, 22);
            this.bucketPage.Name = "bucketPage";
            this.bucketPage.Padding = new System.Windows.Forms.Padding(3);
            this.bucketPage.Size = new System.Drawing.Size(1164, 426);
            this.bucketPage.TabIndex = 1;
            this.bucketPage.Text = "Моя корзина";
            this.bucketPage.UseVisualStyleBackColor = true;
            // 
            // deleteFromBucket
            // 
            this.deleteFromBucket.Location = new System.Drawing.Point(6, 361);
            this.deleteFromBucket.Name = "deleteFromBucket";
            this.deleteFromBucket.Size = new System.Drawing.Size(182, 46);
            this.deleteFromBucket.TabIndex = 4;
            this.deleteFromBucket.Text = "Удалить выбранный товар из корзины";
            this.deleteFromBucket.UseVisualStyleBackColor = true;
            this.deleteFromBucket.Click += new System.EventHandler(this.deleteFromBucket_Click);
            // 
            // doPurchase
            // 
            this.doPurchase.Location = new System.Drawing.Point(809, 361);
            this.doPurchase.Name = "doPurchase";
            this.doPurchase.Size = new System.Drawing.Size(349, 46);
            this.doPurchase.TabIndex = 3;
            this.doPurchase.Text = "Совершить заказ (отправить заявку)";
            this.doPurchase.UseVisualStyleBackColor = true;
            this.doPurchase.Click += new System.EventHandler(this.doPurchase_Click);
            // 
            // bucketGrid
            // 
            this.bucketGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bucketGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bucketGrid.Location = new System.Drawing.Point(6, 6);
            this.bucketGrid.Name = "bucketGrid";
            this.bucketGrid.Size = new System.Drawing.Size(1152, 335);
            this.bucketGrid.TabIndex = 2;
            // 
            // purchasePage
            // 
            this.purchasePage.Controls.Add(this.label2);
            this.purchasePage.Controls.Add(this.label1);
            this.purchasePage.Controls.Add(this.unCheckPurchase);
            this.purchasePage.Controls.Add(this.purchaseGrid);
            this.purchasePage.Location = new System.Drawing.Point(4, 22);
            this.purchasePage.Name = "purchasePage";
            this.purchasePage.Size = new System.Drawing.Size(1164, 426);
            this.purchasePage.TabIndex = 2;
            this.purchasePage.Text = "Мои оформленные заказы";
            this.purchasePage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(806, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(поле Checked стоит в положении true)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(805, 387);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "*Нельзя отменить заявку, которая уже отправлена администрацией";
            // 
            // unCheckPurchase
            // 
            this.unCheckPurchase.Location = new System.Drawing.Point(809, 348);
            this.unCheckPurchase.Name = "unCheckPurchase";
            this.unCheckPurchase.Size = new System.Drawing.Size(349, 32);
            this.unCheckPurchase.TabIndex = 4;
            this.unCheckPurchase.Text = "Отменить заявку";
            this.unCheckPurchase.UseVisualStyleBackColor = true;
            this.unCheckPurchase.Click += new System.EventHandler(this.unCheckPurchase_Click);
            // 
            // purchaseGrid
            // 
            this.purchaseGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.purchaseGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.purchaseGrid.Location = new System.Drawing.Point(6, 6);
            this.purchaseGrid.Name = "purchaseGrid";
            this.purchaseGrid.Size = new System.Drawing.Size(1152, 335);
            this.purchaseGrid.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1183, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // управлениеToolStripMenuItem
            // 
            this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьToolStripMenuItem});
            this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
            this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.управлениеToolStripMenuItem.Text = "Управление";
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.RefreshGridInfo);
            // 
            // editUserInfoButton
            // 
            this.editUserInfoButton.Location = new System.Drawing.Point(289, 507);
            this.editUserInfoButton.Name = "editUserInfoButton";
            this.editUserInfoButton.Size = new System.Drawing.Size(270, 47);
            this.editUserInfoButton.TabIndex = 16;
            this.editUserInfoButton.Text = "Редактировать способ соединения со мной";
            this.editUserInfoButton.UseVisualStyleBackColor = true;
            this.editUserInfoButton.Click += new System.EventHandler(this.editUserInfoButton_Click);
            // 
            // userInfoTextBox
            // 
            this.userInfoTextBox.Location = new System.Drawing.Point(7, 488);
            this.userInfoTextBox.Name = "userInfoTextBox";
            this.userInfoTextBox.Size = new System.Drawing.Size(276, 83);
            this.userInfoTextBox.TabIndex = 17;
            this.userInfoTextBox.Text = "";
            // 
            // MainClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 577);
            this.Controls.Add(this.userInfoTextBox);
            this.Controls.Add(this.editUserInfoButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainClientWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Окно клиента";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainClientWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainClientWindow_Load);
            this.tabControl.ResumeLayout(false);
            this.productsPage.ResumeLayout(false);
            this.productsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productGrid)).EndInit();
            this.bucketPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bucketGrid)).EndInit();
            this.purchasePage.ResumeLayout(false);
            this.purchasePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage productsPage;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.Button addToBuckerButton;
        private System.Windows.Forms.DataGridView productGrid;
        private System.Windows.Forms.TabPage bucketPage;
        private System.Windows.Forms.Button doPurchase;
        private System.Windows.Forms.DataGridView bucketGrid;
        private System.Windows.Forms.TabPage purchasePage;
        private System.Windows.Forms.DataGridView purchaseGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.Button editUserInfoButton;
        private System.Windows.Forms.RichTextBox userInfoTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteFromBucket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button unCheckPurchase;
    }
}

