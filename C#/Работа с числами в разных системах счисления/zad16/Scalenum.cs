using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zad16
{

    class Node
    {
        public void SetNextNode(Node _nextNode)
        {
            this.next = _nextNode;
        }
        public int Element
        {
            get
            {
                return this.element;
            }
            set
            {
                this.element = value;
            }
        }

        public Node Next
        {
            get
            {
                return this.next;
            }
        }

        public Node next;
        public int element;
    }

    class List
    {
        public List()
        {
            // создание пустого списка
            this.headNode = null;
            this.tailNode = this.headNode;
            this.Length = 0;
        }
        public void Push(int _element)
        {
            if (headNode == null)
            {
                // создать узел, сделать его головным
                this.headNode = new Node();
                this.headNode.Element = _element;
                // этот же узел и является хвостовым
                this.tailNode = this.headNode;
                // следующего узла нет
                this.headNode.SetNextNode(null);
            }
            else
            {
                // создать временный узел
                Node newNode = new Node();
                // следующий за предыдущим хвостовым узлом - это наш временный новый узел
                this.tailNode.SetNextNode(newNode);
                // сделаь его же новым хвостовым
                this.tailNode = newNode;
                this.tailNode.Element = _element;
                // слудующего узла пока нет
                this.tailNode.SetNextNode(null);
            }

            ++this.Length;
        }
        public int this[int _position]
        {
            get
            {
                // дабы не загромождать пример, 
                // опустим проверку на валидность переданного параметра '_position'
                Node tempNode = this.headNode;
                for (int i = 0; i < _position; ++i)
                    // переходим к следующему узлу списка
                    tempNode = tempNode.Next;
                return tempNode.Element;
            }
            set
            {
                // дабы не загромождать пример, 
                // опустим проверку на валидность переданного параметра '_position'
                Node tempNode = this.headNode;
                for (int i = 0; i < _position; ++i)
                    // переходим к следующему узлу списка
                    tempNode = tempNode.Next;
                tempNode.Element = tempNode.Element + 1;
            }
            
        }
        
        public void Reverse()
        {
            //Создаем 1 вспомогательную ссылку и ссылку на новую голову. 
            Node helpNode = new Node();
            Node newHead = null;
            /* Смещаем голову на следующий элемент, пока не дойдем до конца, после чего у реальной головы ставим next в newHead, и newHead присваиваем этот узел. Рекомендую проделать на бумажке для понимания.               
            */
            while ((helpNode = headNode) != null)
            {
                headNode = headNode.Next;
                helpNode.next = newHead;
                newHead = helpNode;
            }
            headNode = newHead;
        }

        public void Sort()
        {


        }
        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }

        public int Length;
        public Node headNode;
        public Node tailNode;
    }


    class Scalenum//Класс для работы с q-ичной системой счисления
    {
        public int q;//Основание системы счисления
        public List digits;//Список ненулевых цифр в q-ичном представлении числа
        public List degrees;//Список степеней соответствующих цифр

        public Scalenum(int q = 10)//Конструктор класса с заданным основанием системы счисления:
        {//q - Основание системы счисления
            this.q = q;
            digits = new List();
            degrees = new List();
        }

        public static Scalenum operator +(Scalenum s1, Scalenum s2)//Сумма двух чисел в q-ичной системе счисления:
        {//s1, s2 - числа в q-ичной системе счисления
            if (s1.q != s2.q)
            {
                Console.WriteLine("Числа должны иметь одинаковое основание системы счисления.");
                return null;
            }
            Scalenum s = new Scalenum(s1.q);//Переменная для результата
            Functions.SumConst(ref s, Functions.Decoding(s1));
            Functions.SumConst(ref s, Functions.Decoding(s2));
            return s;
        }

        public static Scalenum operator *(Scalenum s1, Scalenum s2)//Произведение двух чисел в q-ичной системе счисления:
        {//s1, s2 - числа в q-ичной системе счисления
            if (s1.q != s2.q)
            {
                Console.WriteLine("Числа должны иметь одинаковое основание системы счисления.");
                return null;
            }
            Scalenum s = new Scalenum(s1.q);//Переменная для результата
            Scalenum q = new Scalenum(s1.q);//Переменная для степени q
            Functions.SumConst(ref q, Functions.Decoding(s1));
            for (int i = 0, j = 0; i < s2.degrees.Length; i++)//Перебираем все значащие цифры во втором числе
            {
                for (; j < s2.degrees[i]; j++, Functions.QMult(q)) ;//Доходим до нужной степени
                Functions.SumConst(ref s, Functions.Decoding(q) * s2.digits[i]);//Прибавление к результату s1 * q^s2.degrees[i] * s2.digits[i]
            }
            return s;
        }

        public static Scalenum operator %(Scalenum s1, Scalenum s2)//Остаток двух чисел в q-ичной системе счисления:
        {//s1, s2 - числа в q-ичной системе счисления
            if (s1.q != s2.q)
            {
                Console.WriteLine("Числа должны иметь одинаковое основание системы счисления.");
                return null;
            }
            Scalenum s = Functions.Coding(Functions.Decoding(s1) % Functions.Decoding(s2), s1.q);//Переменная для результата
            return s;
        }

        public void Insert(Scalenum s)//Вставка одного числа в центральную часть другого числа:
        {//s - число, которое вставляем
            if (s.q != q)
                Console.WriteLine("Числа должны иметь одинаковое основание системы счисления.");
            List digits1 = new List();//Список ненулевых цифр результата
            List degrees1 = new List();//Список соответствующих степеней результата
            for (int i = 0; i < digits.Length; i++)//Перебираем все цифры исходного числа
            {
                if (degrees[i] <= degrees[digits.Length - 1] / 2)// Если цифра в первой половине
                {
                    digits1.Push(digits[i]);//Добавляем ее
                    degrees1.Push(degrees[i]);
                    if (degrees[i + 1] > degrees[degrees.Length - 1] / 2)//Если это последняя цифра в первой половине
                    {
                        for (int j = 0; j < s.degrees.Length; j++)//Вставляем число s
                        {
                            digits1.Push(s.digits[j]);
                            degrees1.Push(s.degrees[j] + degrees[degrees.Length - 1] / 2 + 1);//Так как степень увеличивается на degrees[degrees.Count - 1] / 2 + 1
                        }
                    }
                }
                else//Если цифра во второй половине
                {
                    digits1.Push(digits[i]);//Добавляем ее
                    degrees1.Push(degrees[i] + s.degrees[s.degrees.Length - 1] + 1);//Так как смещено на s.degrees[s.degrees.Count - 1] + 1
                }
            }
            //digits.Clear();
            digits = digits1;//Сохранение в исходный
            //degrees.Clear();
            degrees = degrees1;//Сохранение в исходный
        }

        public override string ToString()//Преобразование в строку(переопределение object.ToString())
        {
            string s = "";//Строка-результат
            for (int i = 0; i < digits.Length; i++)
                s+= digits[i].ToString() + ' ' + degrees[i].ToString() + "\r\n";
            return s;
        }
    }
}
