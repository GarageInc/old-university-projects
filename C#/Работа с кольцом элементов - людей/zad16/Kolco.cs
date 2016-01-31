using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diana
{
    class Chelovek: IEquatable<Chelovek> , IComparable<Chelovek>
    {
        public string s;//Пол
        public int vozrast;//Возраст
        public bool TF;//Флаг - указывает - является этот человек первым в кольце или нет.

        //Конструктор человека, может работать по умолчанию
        public Chelovek(string str="", int vozr=0)
        {
            s = str;
            vozrast=vozr;//Возраст - потом будет использоваться для сортировки
        }

        //Вывод этого человека на экран
        public override string ToString()//Преобразование в строку(переопределение object.ToString())
        {
             return s+"("+vozrast+")";
        }

        // Default-овые функции ( скопировано с http://msdn.microsoft.com/ru-ru/library/b0zbh7b6.aspx) для работы сортировки кольца на основании возраста
        public int CompareTo(Chelovek chel)
        {
            
            if (chel == null)
                return 1;

            else
                return this.vozrast.CompareTo(chel.vozrast);
        }

        public int SortByNameAscending(int name1, int name2)
        {
            return name1.CompareTo(name2);
        }

        public override int GetHashCode()
        {
            return vozrast;
        }

        public bool Equals(Chelovek other)
        {
            if (other == null) return false;
            return (this.vozrast.Equals(other.vozrast));
        }
    }

    class Kolco//Класс для работы c "Кольцом" участников
    {
        public List<Chelovek> people;//Список участников, где string - либо M, либо W. Зависит от пола.

        public Kolco()//Конструктор класса 
        {
            this.people = new List<Chelovek>();
        }

        public override string ToString()//Преобразование в строку(переопределение object.ToString())
        {
            //Считаем, что 1 человек - это все же уже кольцо. Без людей - кольца нет.
            if (people != null)
            {
                string s = "";

                foreach (Chelovek chel in people)
                    s += chel.ToString()+" ";

                return s;
            }
            else
                return "_";
        }

        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }

        

        
    }
}
