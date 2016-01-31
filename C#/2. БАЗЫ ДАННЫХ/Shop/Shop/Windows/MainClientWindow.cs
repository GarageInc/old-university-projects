using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop
{
    public partial class MainClientWindow : Form
    {
        public MainClientWindow(User user)
        {
            InitializeComponent();

            // Инциализируем переменные
            this.User = user;
            this.Text = "Здравствуйте, "+user.UserName+"!";
            database = new Model();
            Orders = new List<Order>();
            userInfoTextBox.Text = user.UserInformation;

            // Делаем таблицы недоступными для редактирования извне
            bucketGrid.ReadOnly = true;
            productGrid.ReadOnly = true;
            purchaseGrid.ReadOnly = true;

        }
        
        // Произошло открытие окна, изменили таблицы
        private void MainClientWindow_Load(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        // Модель, с помощью которой соединяем с базой данных
        Model database;

        // Пользователь, который сейчас "сидит" и пользуется
        User User;

        // Это корзина пользователя, его "продукты", которые он покупает
        List<Order> Orders;

        // Функция, которая обновляет всю информацию на странице клиента
        public void UpdateInfo()
        {
            // Таблица-корзина
            bucketGrid.DataSource = this.Orders;
            bucketGrid.Refresh();
            // Таблица - продукция магазина
            productGrid.DataSource = database.Products.ToList();
            // Таблица - все чеки
            purchaseGrid.DataSource = database.Purchases.Where(x => x.UserId == this.User.Id).ToList();
        }

        // Кнопка обновления таблиц
        private void RefreshGridInfo(object sender, EventArgs e)
        {

            UpdateInfo();
        }

        // Кнопка редактирования данных о пользователе
        private void editUserInfoButton_Click(object sender, EventArgs e)
        {
            this.User.UserInformation = userInfoTextBox.Text;

            database.Entry(User).State = EntityState.Modified;

            database.SaveChanges();
        }

        // Кнопка "Добавить в корзину"
        private void addToBuckerButton_Click(object sender, EventArgs e)
        {
            // получим значение количества
            int count = int.Parse(countTextBox.Text);
            
            if(count>0 == false)
            {
                MessageBox.Show("К сожалению, неправильное значение количества покупаемого товара");
                return;
            }

            // если мы выбрали продукт, то - проводим действия по добавлению в корзину
            if (productGrid.CurrentRow != null)
            {
                int id = int.Parse(productGrid.CurrentRow.Cells[0].Value.ToString());
                var product = database.Products.Find(id);

                // Проверим, хватит ли у нас продукта на складе?
                if(product.Count-product.AllPurchasedCount<count)
                {
                    MessageBox.Show("К сожалению, на складе отсутствует данный продукт");
                    return;
                }

                // Иначе - увеличиваем количество заказанного продукта на складе
                product.AllPurchasedCount += count;

                database.Entry(product).State = System.Data.Entity.EntityState.Modified;
                // Сохраняем изменения
                database.SaveChanges();

                // Если такой товар уже есть в корзине - то суммируем, иначе - добавляем
                if(Orders.Count(x=>x.Id==id)>0)
                {
                    for(int i=0; i< Orders.Count(); i++)
                    {
                        if(Orders[i].Id==id)
                        {
                            Orders[i].Count += count;
                            Orders[i].Cost += product.Price*count;
                            break;
                        }
                    }
                }
                else
                {
                    // Добавляем в корзину
                    this.Orders.Add(new Order
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Count = count,
                        Cost = count * product.Price
                    });
                }

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }

        }
        
        // Кнопка удаления заказа из корзины
        private void deleteFromBucket_Click(object sender, EventArgs e)
        {
            if (bucketGrid.CurrentRow != null)
            {
                int id = int.Parse(bucketGrid.CurrentRow.Cells[0].Value.ToString());
                var order = this.Orders.Where(x=>x.Id== id).First();

                // Получим связанный товар из базы данных
                var product = database.Products.Find(id);

                product.AllPurchasedCount -= order.Count;

                // Удалим из корзины
                Orders.Remove(order);

                // Сохраняем изменения в базе данных
                database.Entry(product).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                
                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        // Кнопка "Совершить заказ"
        private void doPurchase_Click(object sender, EventArgs e)
        {
            if(Orders.Any())
            {
                // Для каждой покупки
                foreach(var order in Orders)
                {
                    var product = database.Products.Where(x => x.Id == order.Id).First();
                    // создаем чек и сохраняем в базе данных. Администрация их потом "проверит"
                    var purchase = new Purchase
                    {
                        User = this.User,
                        UserId = this.User.Id,
                        Product= product,
                        ProductId= product.Id,
                        Cost=order.Cost,
                        Closed=false,
                        CreateDate=DateTime.Now,
                        PurchasedCount=order.Count
                    };

                    database.Purchases.Add(purchase);
                }

                // Сохраняем изменения в базе данных
                database.SaveChanges();

                // Очищаем корзину и обновляем таблицы
                Orders=new List<Order>();
                UpdateInfo();

                MessageBox.Show("Спасибо за покупку!");
            }
            else
            {
                MessageBox.Show("У Вас нет активных заказов в корзине!");
            }
        }

        // Если вдруг пользователь решил отменить неотправленную заявку(не проверенную администрацией) - то пожалуйста
        private void unCheckPurchase_Click(object sender, EventArgs e)
        {
            if (purchaseGrid.CurrentRow != null)
            {
                int id = int.Parse(purchaseGrid.CurrentRow.Cells[0].Value.ToString());
                
                // Получим покупку из базы данных
                var purchase = database.Purchases.Find(id);

                // Проверим - а не проверен ли уже и не отправлен товар клиенту?
                if(purchase.Closed)
                {
                    MessageBox.Show("Вы не можете удалить заказ, т.к. он уже проверен и принят администрацией нашего магазина!");
                    return;
                }

                // Получим связанный с ним товар
                var product = database.Products.Find(purchase.Id);

                // Возвращаем количество товара на прежнее значение
                product.AllPurchasedCount -= purchase.PurchasedCount;

                // Сохраняем измененный товар и удаляем покупку 
                database.Purchases.Remove(purchase);
                database.Entry(product).State = EntityState.Modified;
                database.SaveChanges();

                // Обновляем таблицы
                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Объект не выбран!");
            }
        }

        private void MainClientWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Если форма закрывается, а в корзине - не оформленные заказы, то их нужно обратно переписать в товары на складе
            foreach (var order in Orders)
            {
                var product = database.Products.Where(x => x.Id == order.Id).First();
                // создаем чек и сохраняем в базе данных. Администрация их потом "проверит"
                product.AllPurchasedCount -= order.Count;

                database.Entry(product).State = EntityState.Modified;
                
            }
            database.SaveChanges();
            Orders = new List<Order>();

            // Завершаем приложение(закрытие)
            Application.Exit();
        }

    }

    // Класс заказа, который хранится в корзине у покупателя
    public class Order
    {
        public int Id { get; set; }

        // Название
        public string Name { get; set; }

        // Количество
        public int Count { get; set; }

        // Стоимость
        public decimal Cost { get; set; }
    }

}
