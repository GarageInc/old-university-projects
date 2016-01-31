namespace Shop
{
    partial class MainAdminWindow
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
            this.productGrid = new System.Windows.Forms.DataGridView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.productsPage = new System.Windows.Forms.TabPage();
            this.deleteButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.productDescrBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.createProduct = new System.Windows.Forms.Button();
            this.purchasePage = new System.Windows.Forms.TabPage();
            this.uncheckPurchaseButton = new System.Windows.Forms.Button();
            this.checkPruchaseButton = new System.Windows.Forms.Button();
            this.purchaseGrid = new System.Windows.Forms.DataGridView();
            this.usersPage = new System.Windows.Forms.TabPage();
            this.unMakeAsAdminButton = new System.Windows.Forms.Button();
            this.makeAsAdminButton = new System.Windows.Forms.Button();
            this.usersGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьОкноЗаказовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.productGrid)).BeginInit();
            this.tabControl.SuspendLayout();
            this.productsPage.SuspendLayout();
            this.purchasePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseGrid)).BeginInit();
            this.usersPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // tabControl
            // 
            this.tabControl.Controls.Add(this.productsPage);
            this.tabControl.Controls.Add(this.purchasePage);
            this.tabControl.Controls.Add(this.usersPage);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1172, 515);
            this.tabControl.TabIndex = 2;
            // 
            // productsPage
            // 
            this.productsPage.Controls.Add(this.deleteButton);
            this.productsPage.Controls.Add(this.label4);
            this.productsPage.Controls.Add(this.textBox3);
            this.productsPage.Controls.Add(this.label3);
            this.productsPage.Controls.Add(this.textBox2);
            this.productsPage.Controls.Add(this.productDescrBox);
            this.productsPage.Controls.Add(this.label2);
            this.productsPage.Controls.Add(this.label1);
            this.productsPage.Controls.Add(this.textBox1);
            this.productsPage.Controls.Add(this.createProduct);
            this.productsPage.Controls.Add(this.productGrid);
            this.productsPage.Location = new System.Drawing.Point(4, 22);
            this.productsPage.Name = "productsPage";
            this.productsPage.Padding = new System.Windows.Forms.Padding(3);
            this.productsPage.Size = new System.Drawing.Size(1164, 489);
            this.productsPage.TabIndex = 0;
            this.productsPage.Text = "Склад";
            this.productsPage.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(990, 348);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(166, 23);
            this.deleteButton.TabIndex = 24;
            this.deleteButton.Text = "Удалить выбранный продукт из базы";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 390);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Количество";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(424, 387);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(267, 20);
            this.textBox3.TabIndex = 22;
            this.textBox3.Text = "0";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Цена/единицу";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(424, 351);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(267, 20);
            this.textBox2.TabIndex = 20;
            this.textBox2.Text = "0.0";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // productDescrBox
            // 
            this.productDescrBox.Location = new System.Drawing.Point(64, 387);
            this.productDescrBox.Name = "productDescrBox";
            this.productDescrBox.Size = new System.Drawing.Size(267, 96);
            this.productDescrBox.TabIndex = 19;
            this.productDescrBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Описание";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Имя";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 351);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(267, 20);
            this.textBox1.TabIndex = 16;
            // 
            // createProduct
            // 
            this.createProduct.Location = new System.Drawing.Point(737, 349);
            this.createProduct.Name = "createProduct";
            this.createProduct.Size = new System.Drawing.Size(166, 23);
            this.createProduct.TabIndex = 15;
            this.createProduct.Text = "Добавить новый продукт";
            this.createProduct.UseVisualStyleBackColor = true;
            this.createProduct.Click += new System.EventHandler(this.createProduct_Click);
            // 
            // purchasePage
            // 
            this.purchasePage.Controls.Add(this.uncheckPurchaseButton);
            this.purchasePage.Controls.Add(this.checkPruchaseButton);
            this.purchasePage.Controls.Add(this.purchaseGrid);
            this.purchasePage.Location = new System.Drawing.Point(4, 22);
            this.purchasePage.Name = "purchasePage";
            this.purchasePage.Padding = new System.Windows.Forms.Padding(3);
            this.purchasePage.Size = new System.Drawing.Size(1164, 489);
            this.purchasePage.TabIndex = 1;
            this.purchasePage.Text = "Чеки продаж";
            this.purchasePage.UseVisualStyleBackColor = true;
            // 
            // uncheckPurchaseButton
            // 
            this.uncheckPurchaseButton.Location = new System.Drawing.Point(395, 399);
            this.uncheckPurchaseButton.Name = "uncheckPurchaseButton";
            this.uncheckPurchaseButton.Size = new System.Drawing.Size(349, 46);
            this.uncheckPurchaseButton.TabIndex = 4;
            this.uncheckPurchaseButton.Text = "Убрать подтверждение выбранного чека";
            this.uncheckPurchaseButton.UseVisualStyleBackColor = true;
            this.uncheckPurchaseButton.Click += new System.EventHandler(this.UncheckPurchaseButtonClick);
            // 
            // checkPruchaseButton
            // 
            this.checkPruchaseButton.Location = new System.Drawing.Point(395, 347);
            this.checkPruchaseButton.Name = "checkPruchaseButton";
            this.checkPruchaseButton.Size = new System.Drawing.Size(349, 46);
            this.checkPruchaseButton.TabIndex = 3;
            this.checkPruchaseButton.Text = "Подтвердить выбранный чек";
            this.checkPruchaseButton.UseVisualStyleBackColor = true;
            this.checkPruchaseButton.Click += new System.EventHandler(this.CheckPurchaseButtonClick);
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
            // usersPage
            // 
            this.usersPage.Controls.Add(this.unMakeAsAdminButton);
            this.usersPage.Controls.Add(this.makeAsAdminButton);
            this.usersPage.Controls.Add(this.usersGrid);
            this.usersPage.Location = new System.Drawing.Point(4, 22);
            this.usersPage.Name = "usersPage";
            this.usersPage.Size = new System.Drawing.Size(1164, 489);
            this.usersPage.TabIndex = 2;
            this.usersPage.Text = "Пользователи";
            this.usersPage.UseVisualStyleBackColor = true;
            // 
            // unMakeAsAdminButton
            // 
            this.unMakeAsAdminButton.Location = new System.Drawing.Point(555, 381);
            this.unMakeAsAdminButton.Name = "unMakeAsAdminButton";
            this.unMakeAsAdminButton.Size = new System.Drawing.Size(349, 46);
            this.unMakeAsAdminButton.TabIndex = 5;
            this.unMakeAsAdminButton.Text = "Убрать из списка администрации";
            this.unMakeAsAdminButton.UseVisualStyleBackColor = true;
            this.unMakeAsAdminButton.Click += new System.EventHandler(this.unMakeAsAdminButton_Click);
            // 
            // makeAsAdminButton
            // 
            this.makeAsAdminButton.Location = new System.Drawing.Point(200, 381);
            this.makeAsAdminButton.Name = "makeAsAdminButton";
            this.makeAsAdminButton.Size = new System.Drawing.Size(349, 46);
            this.makeAsAdminButton.TabIndex = 4;
            this.makeAsAdminButton.Text = "Сделать администратором";
            this.makeAsAdminButton.UseVisualStyleBackColor = true;
            this.makeAsAdminButton.Click += new System.EventHandler(this.makeAsAdminButton_Click);
            // 
            // usersGrid
            // 
            this.usersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersGrid.Location = new System.Drawing.Point(6, 6);
            this.usersGrid.Name = "usersGrid";
            this.usersGrid.Size = new System.Drawing.Size(1152, 335);
            this.usersGrid.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1196, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // управлениеToolStripMenuItem
            // 
            this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьToolStripMenuItem,
            this.открытьОкноЗаказовToolStripMenuItem});
            this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
            this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.управлениеToolStripMenuItem.Text = "Управление";
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.RefreshGridsInfo);
            // 
            // открытьОкноЗаказовToolStripMenuItem
            // 
            this.открытьОкноЗаказовToolStripMenuItem.Name = "открытьОкноЗаказовToolStripMenuItem";
            this.открытьОкноЗаказовToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.открытьОкноЗаказовToolStripMenuItem.Text = "Открыть окно заказов";
            this.открытьОкноЗаказовToolStripMenuItem.Click += new System.EventHandler(this.открытьОкноЗаказовToolStripMenuItem_Click);
            // 
            // MainAdminWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 563);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainAdminWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Окно администратора";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainAdminWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.productGrid)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.productsPage.ResumeLayout(false);
            this.productsPage.PerformLayout();
            this.purchasePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.purchaseGrid)).EndInit();
            this.usersPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usersGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView productGrid;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage productsPage;
        private System.Windows.Forms.TabPage purchasePage;
        private System.Windows.Forms.TabPage usersPage;
        private System.Windows.Forms.DataGridView purchaseGrid;
        private System.Windows.Forms.DataGridView usersGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RichTextBox productDescrBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button createProduct;
        private System.Windows.Forms.Button checkPruchaseButton;
        private System.Windows.Forms.Button uncheckPurchaseButton;
        private System.Windows.Forms.ToolStripMenuItem открытьОкноЗаказовToolStripMenuItem;
        private System.Windows.Forms.Button makeAsAdminButton;
        private System.Windows.Forms.Button unMakeAsAdminButton;
    }
}

