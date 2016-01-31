namespace Shop
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class MainAdminWindow : Form
    {
        public MainAdminWindow(User user)
        {
            InitializeComponent();

            database = new Model();

            User = user;

            UpdateInfo();

            this.Text += " " + user.UserName;

            // Таблицы только для чтения
            productGrid.ReadOnly = true;
            usersGrid.ReadOnly = true;
            purchaseGrid.ReadOnly = true;
        }

        // Модель, через которую обращаемся к базе данных
        Model database;
        User User;

        // Функция обновления информации в окнах
        public void UpdateInfo()
        {
            productGrid.DataSource = database.Products.ToList();
            usersGrid.DataSource = database.Users.ToList();
            purchaseGrid.DataSource = database.Purchases.ToList();

        }
        
        // Кнопка обновления
        private void RefreshGridsInfo(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        // Создание нового продукта(добавление на склад)
        private void createProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                string description = productDescrBox.Text;

                int price = int.Parse(textBox2.Text);
                int count = int.Parse(textBox3.Text);

                var product = new Product
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Count = count,
                    CreateDate = DateTime.Now,
                    AllPurchasedCount = 0
                };

                database.Products.Add(product);
                database.SaveChanges();

                UpdateInfo();

                MessageBox.Show("Продукт добавлен!");
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        // Кнопка удаления выбранного продукта
        // Нельзя удалить продукт, который уже выбран покупателем хотя бы один раз! Т.к. у него есть уже сделанный клиентом чек.
        // Законодательство РФ обязует продавца продать покупателю выбранный им товар
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (productGrid.CurrentRow != null)
            {
                int id = int.Parse(productGrid.CurrentRow.Cells[0].Value.ToString());
                var product = database.Products.Find(id);
                
                // Получим все чеки по данному продукту, которые могут быть незакрытыми
                if(database.Purchases.Where(x=>x.ProductId==product.Id).Any())
                {
                    MessageBox.Show("Продукт удалить нельзя, по нему уже есть чеки");
                    return;
                }


                database.Products.Remove(product);
                database.SaveChanges();

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        // Кнопка подтверждения чека
        private void CheckPurchaseButtonClick(object sender, EventArgs e)
        {
            if (purchaseGrid.CurrentRow != null)
            {
                int id = int.Parse(purchaseGrid.CurrentRow.Cells[0].Value.ToString());
                var purchase = database.Purchases.Find(id);

                // Указываем, что данный чек подтвержден
                purchase.Closed = true;

                database.Entry(purchase).State = System.Data.Entity.EntityState.Modified;

                // Сохраняем изменения
                database.SaveChanges();

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        // Кнопка СНЯТИЯ подтверждения чека(если ошибочно подтвержден)
        private void UncheckPurchaseButtonClick(object sender, EventArgs e)
        {
            if (purchaseGrid.CurrentRow != null)
            {
                int id = int.Parse(purchaseGrid.CurrentRow.Cells[0].Value.ToString());
                var purchase = database.Purchases.Find(id);

                // Указываем, что данный чек НЕ подтвержден
                purchase.Closed = false;

                database.Entry(purchase).State = System.Data.Entity.EntityState.Modified;

                // Сохраняем изменения
                database.SaveChanges();

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        // Открытие окна заказов
        private void открытьОкноЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainClientWindow mcw = new MainClientWindow(User);
            mcw.Show();

            // Закроем текущее окно
            this.Hide();
        }

        // Кнопка "сделать администратором" того пользователя, который выбран на гриде
        private void makeAsAdminButton_Click(object sender, EventArgs e)
        {
            if (usersGrid.CurrentRow != null)
            {
                int id = int.Parse(usersGrid.CurrentRow.Cells[0].Value.ToString());
                var user = database.Users.Find(id);

                if(user.IsAdmin)
                {
                    MessageBox.Show("Пользователь " + user.UserName + " уже администратор!");
                    return;
                }

                user.IsAdmin = true;

                database.Entry(user).State = System.Data.Entity.EntityState.Modified;

                // Сохраняем изменения
                database.SaveChanges();

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        // Убрать из списка администраторов выбранного пользователя
        private void unMakeAsAdminButton_Click(object sender, EventArgs e)
        {
            if (usersGrid.CurrentRow != null)
            {
                int id = int.Parse(usersGrid.CurrentRow.Cells[0].Value.ToString());
                var user = database.Users.Find(id);

                if (user.IsAdmin==false)
                {
                    MessageBox.Show("Пользователь " + user.UserName + " итак не администратор!");
                    return;
                }

                user.IsAdmin = false;

                database.Entry(user).State = System.Data.Entity.EntityState.Modified;

                // Сохраняем изменения
                database.SaveChanges();

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        private void MainAdminWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }
    }
}
