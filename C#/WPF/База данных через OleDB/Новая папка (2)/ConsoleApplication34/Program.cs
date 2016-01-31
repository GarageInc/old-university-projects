using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Pokupatel
{
    class Program
    {
        private static OleDbConnection connect;
        static OleDbDataAdapter daTovar;
        static OleDbDataAdapter daPokupatel;
        static OleDbDataAdapter daZakaz;
        // Загрузка в DataSet
        public static void BuildDataSet(ref OleDbConnection connect, ref DataSet ds, ref OleDbDataAdapter[] adapt)
        {
            OleDbCommand[] commands = new OleDbCommand[3];

            commands[0] = new OleDbCommand("SELECT * from Tovar", connect);
            daTovar = new OleDbDataAdapter(commands[0]);
            daTovar.FillSchema(ds, SchemaType.Source, "Tovar");
            daTovar.Fill(ds, "Tovar");
            adapt[0] = daTovar;

            commands[1] = new OleDbCommand("SELECT * from Pokupatel", connect);
            daPokupatel = new OleDbDataAdapter(commands[1]);
            daPokupatel.FillSchema(ds, SchemaType.Source, "Pokupatel");
            daPokupatel.Fill(ds, "Pokupatel");
            adapt[1] = daPokupatel;

            commands[2] = new OleDbCommand("SELECT * from Zakaz", connect);
            daZakaz = new OleDbDataAdapter(commands[2]);
            daZakaz.FillSchema(ds, SchemaType.Source, "Zakaz");
            daZakaz.Fill(ds, "Zakaz");
            adapt[2] = daZakaz;

        }

        // Заполнение таблиц рандомными значениями
        public static void FillDataSet(ref DataSet ds, ref OleDbDataAdapter[] adapt)
        {
            // Заполняем случаными покупателями таблицу покупателей
            Random r = new Random();
            for (int i = 1; i < 10; i++)
                try
                {
                    ds.Tables["Pokupatel"].Rows.Add(
                        new object[] { i, "Pok_" + i });
                }
                catch
                {
                }

            //// Заполняем таблицу товаров
            //Console.WriteLine("Введите idTovar, name, amount, price: ");
            //for (int i = 0; i < 1; i++)
            //{
            //    // Добавляем товар в таблицу
            //    Console.WriteLine("  Введите idTovar (число):");
            //    int idTovar = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine("  Введите имя товара (строка любая):");
            //    string name = Console.ReadLine();
            //    Console.WriteLine("  Введите количество (число):");
            //    int amount = Convert.ToInt16(Console.ReadLine());
            //    Console.WriteLine("  Введите цену (число):");
            //    int price = Convert.ToInt16(Console.ReadLine());
            //    try
            //    {
            //        ds.Tables["Tovar"].Rows.Add(new object[] { idTovar, name, amount, price });
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("   Данное idTovar уже существует в таблице!");
            //    }
            //}

            //Console.WriteLine("!");
            //// Заполняем таблицу заказ
            //// Выбираем случайное число от 1 до 7
            //int k = r.Next(0,3);
            //// Добавляем товар
            //for (int j = 1; j < k; j++)
            //    try
            //    {
            //        // Попытаемся добавить новую строку в таблицу
            //        // Т.к. idPokup, idTovar - внешние ключи и они должны существовать уже в предыдущих таблицах, то
            //        // может выброситься исключение - которое мы ловим в блоке try{} и если вставить не получилось - то повторяем попытку с уже новыми значениями полей
            //        ds.Tables["Zakaz"].Rows.Add(
            //             new object[] { r.Next(0, 10), r.Next(0, 5), r.Next(0, 5), r.Next(0, 100), DateTime.Now });
            //    }
            //    catch
            //    {
            //        // Иначе повторяем попытку
            //        j = j - 1;
            //    }
            AddToTableAndSave(ds, daPokupatel, "Pokupatel");
        }


        // Печать таблицы
        public static void Print(DataSet ds, string name)
        {
            DataTable dt = ds.Tables[name];
            Console.WriteLine("\n ТАБЛИЦА:" + name);

            // Печатаем колонки
            foreach (DataColumn dc in dt.Columns)
                Console.Write(String.Format("{0}    ", dc.Caption));
            Console.WriteLine();
            // Печатаем содержимое таблицы построчно
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                    Console.Write(dr[i] + "        ");
                Console.WriteLine();
            }
        }

        // Функция добавления объектов в таблицу и сохранения в базу данных
        public static void AddToTableAndSave(DataSet ds, OleDbDataAdapter adapter, string tableName)
        {
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            using (builder)
            {
                adapter.Update(ds, tableName);
            }
        }

        static void Main(string[] args)
        {
            // Держим в памят
            DataSet ds = new DataSet("Torgovlya");
            // Объект для заполнения adapter'a
            OleDbDataAdapter[] adapt = new OleDbDataAdapter[3];
            // OleDb - то же самое, что и OleDb, но для локальной базы данных
            connect = new OleDbConnection(@"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=markett;Data Source=PB\MSSQLSERVER098");
            connect.Open();

            // Загрузка из OleDb в dataset
            BuildDataSet(ref connect, ref ds, ref adapt);

            // Заполнение dataseta'a(добавление случайных пользователей)
            FillDataSet(ref ds, ref adapt);

            Console.WriteLine("Успешная загрузка!");

            Print(ds, "Pokupatel");
            Print(ds, "Tovar");
            Print(ds, "Zakaz");

            bool yes = true;
            while (yes)
            {
                Console.WriteLine("\n* Введите\n 1-Для работы с товаром,\n 2-Для отмены заказа, \n 3-Печать таблицы заказов,\n 4-Печать таблицы покупателей,\n 0-Выход");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        RabotaSTovarom(ref ds, ref adapt);
                        break;
                    case 2:
                        RabotaSZakazom(ref ds, ref adapt);
                        break;
                    case 3:
                        Print(ds, "Zakaz");
                        break;
                    case 4:
                        Print(ds, "Pokupatel");
                        break;
                    case 0:
                        {
                            yes = false;
                            break;
                        }
                }
            }
            connect.Close();
        }

        // Работа с товаром
        public static void RabotaSTovarom(ref DataSet ds, ref OleDbDataAdapter[] adapt)
        {
            DataTable dtTovar = ds.Tables["Tovar"];
            DataTable dtZakaz = ds.Tables["Zakaz"];

            bool yes = true;
            while (yes)
            {
                Console.WriteLine("\n 1-Добавить\n 2-Удалить,\n 3-Поиск,\n 4-Просмотр таблицы,\n 5-Купить товар \n 0-Выход");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("   Введите номер (idTovar) товара:");
                            int nomer = int.Parse(Console.ReadLine());
                            DataRow dr = dtTovar.Rows.Find(nomer);
                            if (dr == null)
                            {
                                Console.WriteLine("   Введите 'да' или 'нет' (y/n)");
                                string s = Console.ReadLine();
                                if (s == "y" || s == "да")
                                {
                                    Console.WriteLine("   Введите название товара:");
                                    string name = Console.ReadLine();

                                    Console.WriteLine("   Введите количество:");
                                    int k = int.Parse(Console.ReadLine());

                                    Console.WriteLine("   Введите цену:");
                                    int price = int.Parse(Console.ReadLine());

                                    dtTovar.Rows.Add(new object[] { nomer, name, k, price });

                                    // Печатаем новую таблицу-результат
                                    Print(ds, "Tovar");
                                }
                            }
                            else
                                Console.WriteLine("\n   Данный товар(с таким idTovar) уже существует!");// \n - переход на новую строку
                            break;
                        }

                    case "2":
                        {
                            Console.WriteLine("   Введите номер (idTovar) удаляемого товара:");
                            int nomer = int.Parse(Console.ReadLine());
                            DataRow dr = dtTovar.Rows.Find(nomer);
                            if (dr != null)
                            {
                                Console.WriteLine("   Введите 'да' или 'нет' (y/n)");
                                string s = Console.ReadLine();
                                if (s == "y" || s == "да")
                                {
                                    dtTovar.Rows.Remove(dr);// Удалили строку с информацией о товаре
                                    // Печатаем новую таблицу-результат
                                    Print(ds, "Tovar");
                                }
                                else
                                    Console.WriteLine("   Удаление отменено");
                            }
                            else Console.WriteLine("   Данный товар отсутствует!");

                            break;
                        }

                    case "3":
                        {
                            Console.WriteLine("   Введите номер (idTovar) товара:");
                            int nomer = int.Parse(Console.ReadLine());
                            DataRow dr = dtTovar.Rows.Find(nomer);
                            if (dr != null)
                            {
                                DataTable dt = ds.Tables["Tovar"];
                                Console.WriteLine();

                                // Печатаем колонки
                                foreach (DataColumn dc in dt.Columns)
                                    Console.Write(String.Format("{0}       ", dc.Caption));
                                Console.WriteLine();
                                // Печатаем содержимое строки построчно
                                Console.WriteLine(dr.ToString());

                                // Печатаем новую таблицу-результат
                                Print(ds, "Tovar");
                            }
                            else
                                Console.WriteLine("\n   Данный товар(с таким idTovar) не существует!");// \n - переход на новую строку
                            break;
                        }

                    case "4":
                        Print(ds, "Tovar");
                        break;

                    case "5":
                        {
                            Console.WriteLine("   Введите номер (idTovar) товара:");
                            int nomer = int.Parse(Console.ReadLine());
                            DataRow dr = dtTovar.Rows.Find(nomer);
                            if (dr != null)
                            {
                                Console.WriteLine("   Введите количество покупаемого товара:");
                                int kol = int.Parse(Console.ReadLine());

                                // Сколько же у нас товара на складе? Получим информацию:
                                int real = (int)dr["amount"];
                                if (kol > real)
                                {
                                    Console.WriteLine("   Данное количество товара недоступно, доступно: " + real + " штук");
                                    break;
                                }
                                // Иначе выведем цену и при согласии продадим, создав новую запись в таблицу Zakaz
                                Console.WriteLine("   Цена = {0}*{1} =" + (kol * (int)dr["price"]) + "$", kol, (int)dr["price"]);
                                Console.WriteLine("   Вы хотитет заказать товар 'да'/'нет'?(y/n)?");
                                string s = Console.ReadLine();
                                if (s == "y" || s == "да")
                                {
                                    // Уменьшаем значение в таблице
                                    dr["amount"] = real - kol;
                                    bool truePokup = false;// Флаг, что покупатель существует
                                    while (!truePokup)
                                    {
                                        int idPokup = int.Parse("  Введите Ваш idPokup:");
                                        // Проверим - а существует ли такой покупатель? Если нет - то добавить мы не сможем
                                        DataTable dtPokup = ds.Tables["Pokupatel"];
                                        if (dtPokup.Rows.Find(idPokup) == null)
                                        {
                                            Console.WriteLine("   Покупатель с таким idPokup отсутствует в таблице!");
                                        }
                                        else
                                        {
                                            // Создаем новую запись в таблице "Заказы"
                                            CreateNewZakaz(ref ds, idPokup, (int)dr["idTovar"], kol);
                                            truePokup = true;
                                        }
                                    }

                                    // Если покупатель согласилась
                                    Console.WriteLine("   Заказ оформлен!(см. таблицу заказов");
                                }
                                else
                                {
                                    Console.WriteLine("   Заказ отменен");
                                }

                                // Печатаем новую таблицу-результат
                                Print(ds, "Tovar");
                            }
                            else
                                Console.WriteLine("\n   Данный товар(с таким idTovar) не существует!");// \n - переход на новую строку

                            break;
                        }

                    case "0":
                        {
                            yes = false;
                            break;
                        }
                }
            }

            AddToTableAndSave(ds, adapt[0], "Tovar");
            AddToTableAndSave(ds, adapt[2], "Zakaz");
        }

        // Работа с товаром
        public static void RabotaSZakazom(ref DataSet ds, ref OleDbDataAdapter[] adapt)
        {
            DataTable dtTovar = ds.Tables["Tovar"];
            DataTable dtZakaz = ds.Tables["Zakaz"];

            Console.WriteLine("   Введите номер отменяемого (idZakaz) Заказа:");
            int nomer = int.Parse(Console.ReadLine());
            DataRow dr = dtZakaz.Rows.Find(nomer);
            if (dr != null)
            {
                Console.WriteLine("   Вы точно хотитете отменить заказ? 'да'/'нет'  (y/n)?");
                string s = Console.ReadLine();
                if (s == "y" || s == "да")
                {
                    // Восстановим информацию о заказнном товаре(количестве) в таблице товаров
                    int idTovar = (int)dr["idTovar"];
                    DataRow dr2 = dtTovar.Rows.Find(idTovar);
                    ; dr2["amount"] = (int)dr2["amount"] + (int)dr["amount"];

                    // Удалили заказ
                    dtZakaz.Rows.Remove(dr);

                    // Если покупатель согласилась
                    Console.WriteLine("   Заказ оформлен!(см. таблицу заказов");
                }
                else
                {
                    Console.WriteLine("   Заказ не отменен");
                }

                // Печатаем новую таблицу-результат
                Print(ds, "Zakaz");
            }
            else
                Console.WriteLine("\n   Данный заказ(с таким idZakaz) не существует!");// \n - переход на новую строку

            AddToTableAndSave(ds, adapt[0], "Tovar");
            AddToTableAndSave(ds, adapt[2], "Zakaz");
        }

        // Создание нового заказа в таблице заказов
        private static void CreateNewZakaz(ref DataSet ds, int idPokup, int idTovar, int kol)
        {
            bool add = false;// Флаг, что не получилось добавить новое значение в таблицу
            Random r = new Random();
            while (!add)
            {
                try
                {
                    // Попытаемся добавить новую строку в таблицу
                    // Т.к. idPokup, idTovar - внешние ключи и они должны существовать уже в предыдущих таблицах, то
                    // может выброситься исключение - которое мы ловим в блоке try{} и если вставить не получилось - то повторяем попытку с уже новыми значениями полей
                    ds.Tables["Zakaz"].Rows.Add(
                         new object[] { r.Next(1, 100), idPokup, idTovar, kol, DateTime.Now });
                    add = true;
                }
                catch
                {
                    // Иначе повторяем попытку
                    add = false;
                }
            }
        }
    }

}
