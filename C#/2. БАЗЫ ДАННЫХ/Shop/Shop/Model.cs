namespace Shop
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {
        // Контекст настроен для использования строки подключения "Model" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Shop.Model" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "Model" 
        // в файле конфигурации приложения.
        public Model()
            : base("name=Model")
        {

        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // Укажем, какие модели используем в базе данных
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<User> Users { get; set; }
        
        public static Model Create()
        {
            return new Model();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Говорим, что есть связь один ко многим(у пользователя может быть много заказов)
            modelBuilder.Entity<User>().HasMany(c => c.Purchases);
        }
    }

    // Класс продукта - управляется и загружается администратором - тем, кому дан доступ
    public class Product
    {
        // Уникальный идентификатор
        public int Id { get; set; }

        // Название
        public string Name { get; set; }
        
        // Описание
        public string Description { get; set; }
        
        // Цена
        public decimal Price { get; set; }
        
        // Дата поступления-создания
        public DateTime CreateDate { get; set; }

        // Количество поступившего товара
        public int Count { get; set; }

        // Количество купленного товара в сумме
        public int AllPurchasedCount { get; set; }
    }

    // Класс покупка-чек
    public class Purchase
    {
        // Уникальный идентификатор
        public int Id { get; set; }

        // Купленный продукт/товар
        public Product Product { get; set; }
        
        // ID купленного продукта
        public int? ProductId { get; set; }
        
        // Покупатель
        public User User { get; set; }

        // ID покупателя
        public int? UserId { get; set; }
                
        // Дата заказа
        public DateTime CreateDate { get; set; }

        // Стоимость = цена * количество
        public decimal Cost { get; set; }

        // Количество купленного товара
        public int PurchasedCount { get; set; }

        // Закрытость чека(проверен администрацией и подтвержден, заказ отправлен по адресу)
        public bool Closed { get; set; }
    }
    
    // Человек, который совершает покупку
    public class User
    {
        // Уникальный идентификатор
        public int Id { get; set; }

        // Имя
        public string UserName { get; set; }

        // Пароль
        public string Password { get; set; }

        // Информация о пользователе(адрес, контакты и тп)
        public string UserInformation { get; set; }
        
        // Чеки пользователя по тем продуктам, что он купил
        public ICollection<Purchase> Purchases { get; set; }

        // Администратор ли это
        public bool IsAdmin { get; set; }
    }
}