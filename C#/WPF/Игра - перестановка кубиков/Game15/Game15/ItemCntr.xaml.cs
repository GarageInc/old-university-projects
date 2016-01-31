using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game15
{
    // Элемент доски - кубик - наследуется от базового объекта UserControl
    public partial class ЭлементДоски : UserControl
    {
        public int ЗначениеОбъекта
        {
            get { return значение; }
            set 
            {
                tbVal.Text = (value == 0) ? "" : value.ToString();
                значение = value;
            }
        }   
        
        // Число, которое он в себе содержит
        private int значение;

        // Позиции кубика
        public int позиция_I { get; set; }
        public int позиция_J { get; set; }
        
        // Конструктор - инциализация
        public ЭлементДоски()
        {
            InitializeComponent();
        }

    }
}
