namespace Shop
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public partial class Form1 : Form
    {
        // Название файла конфига
        public string configFileName = "products.xml";

        // Список, в который из конфига загрузятся параметры
        public List<Product> Products { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        // При открытии программы заранее задаются её видимые области
        private void Form1_Load(object sender, EventArgs e)
        {
            Products = new List<Product>();

            invalidateForm();
        }
        
        // Функция, которая отвечает за внешний вид контролов на форме.
        public void invalidateForm()
        {
            createConfigButton.Enabled = !File.Exists(configFileName);

            readConfigButton.Enabled = File.Exists(configFileName);

            considerButton.Enabled = File.Exists(configFileName);
        }
        
        // Нажатие на кнопку чтения конфига
        private void readConfigButton_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Product[]));

                using (FileStream fs = new FileStream(configFileName, FileMode.OpenOrCreate))
                {
                    Product[] products = (Product[])formatter.Deserialize(fs);

                    // очистим глобальную переменную
                    Products.Clear();

                    // Добавляем продукты в глобальный список
                    foreach (Product p in products)
                    {
                        Products.Add(p);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Ошибка! " + error.Message);
            }
            
        }

        // Кнопка создания конфиг-файла
        private void createConfigButton_Click(object sender, EventArgs e)
        {
            // объекты для сериализации
            Product A = new Product("A");
            A.Positions.Add(new Position(1,5));
            A.Positions.Add(new Position(3, 14));
            A.Positions.Add(new Position(10, 40));


            Product B = new Product("B");
            B.Positions.Add(new Position(1, 1));
            B.Positions.Add(new Position(5, 4));
            B.Positions.Add(new Position(8, 6));


            Product C = new Product("C");
            C.Positions.Add(new Position(1, 3));
            C.Positions.Add(new Position(2, 5));
            C.Positions.Add(new Position(5, 11));

            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(Product[]));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(configFileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, new[] { A, B, C });
            }

            invalidateForm();
        }

        // Парсинг введенного пользователем последовательности товаров: например, "ААВССАВ"
        public double GetPrice(string products)
        {
            // Словарь - в нем хранится как ключ - название продукта, а как значение - количество
            Dictionary<string, int> products_by_count = new Dictionary<string, int>();

            // Сначала посчитаем количество
            for (int i = 0; i < products.Length; i++)
            {
                // Проверим - есть ли код продукта в глобальном массиве
                string p = products[i].ToString();

                if (Products.Count(x => String.Compare(x.Name,p) == 0) > 0)
                {
                    // Если такого продукта в словаре нет - добавляем его, иначе - увиличиваем его счетчик
                    if (products_by_count.ContainsKey(p))
                    {
                        products_by_count[p] = products_by_count[p] + 1;
                    }
                    else
                    {
                        products_by_count.Add(p, 1);
                    }
                } else
                {
                    throw new Exception("Продукт " + p + " отсутствует в конфигурационном файле!");
                }
            }
            
            // Теперь проведем подсчет цены по каждому продукты в словаре
            var price = 0.0;

            foreach (KeyValuePair<string, int> product_with_count in products_by_count)
            {
                var count = product_with_count.Value;// количество товара
                var name = product_with_count.Key;// название продкута
                
                var product = Products.First(x => x.Name == product_with_count.Key);

                for(int i= product.Positions.Count - 1; i >= 0; i--)
                {
                    var current_position = product.Positions[i];

                    var current_count = count / current_position.Count;

                    price += current_count * current_position.Price;

                    count = count - current_count * current_position.Count;
                }
                // считаем цену, начиная с самых больших количеств
                // то есть если количество продукта 47 - то берем 4 десятка, 2 тройки и 1 единичный продукт

                // для десятков
                /*
                var current_count = count / 10;
                price += current_count * product.PriceFor10;
                count = count - current_count * 10;

                // для оставшихся троек
                current_count = count / 3;
                price += current_count * product.PriceFor3;
                count = count - current_count * 3;
                
                // для оставшихся единичек
                current_count = count;
                price += current_count * product.PriceFor1;*/
            }

            return price;
        }

        // Кнопка "Посчитать" - считает введенную пользователем информацию
        private void considerButton_Click(object sender, EventArgs e)
        {
            considerButton.Enabled = true;

            try
            {
               var productsByString =  productsInput.Text;
                MessageBox.Show("Общая цена: " + GetPrice(productsByString));
            }
            catch(Exception error)
            {
                MessageBox.Show("Произошла ошибка! \n" + error.Message);
            }
        }




    }
}
