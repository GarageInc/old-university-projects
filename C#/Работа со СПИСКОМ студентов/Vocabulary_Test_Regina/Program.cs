using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Work
{
    class Node
    {
        public void SetNextNode(Node _nextNode)
        {
            this.next = _nextNode;
        }
        
        public Node next;
        public Student student;
    }

    class List
    {
        public List()
        {
            // создание пустого списка
            this.headNode = null;
            this.tailNode = this.headNode;
        }
 
        public void Push(Student _Student)
        {
            if (headNode == null)
            {
                // создать узел, сделать его головным
                this.headNode = new Node();
                this.headNode.student = _Student;
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
                this.tailNode.student = _Student;
                // слудующего узла пока нет
                this.tailNode.SetNextNode(null);
            }
        }

        public void Print()
        {
            Node t = headNode;
            while (t != null)
            {
                Console.WriteLine(t.student.ToString());
                t = t.next;
            }
        }
        
        public Node headNode;//Начало
        public Node tailNode;//Конец
    }

    class Student
    {
        public string fam;//фамилия
        public string pol;//пол
        public int old;//возраст

        //Конструктор инициализации по умолчанию
        public Student(string e="", string r="",string s="")
        {
            this.fam = e;
            this.pol = r;
            this.old=int.Parse(s);
        }

        public override string ToString()
        {
            return (fam+" "+pol+" "+old);
        }

    }


    class Program
    {

        //Автоматизированная работа.
        public static void Visualization(List Students)
        {

            //Печатаем
            Students.Print();
            Console.WriteLine();
            //Удаляем студентов с возрастом меньше 18

            while (Students.headNode!=null && Students.headNode.student.old<18)
            {
                Students.headNode=Students.headNode.next;
            }
            //Тут взяли студента старше 18 лет
            Node temp2 = Students.headNode;
         
            while (temp2.next != null)
            {
                bool per=false;
                if (temp2.next.student.old < 18)
                {
                    per = true;
                    temp2.next = temp2.next.next;
                }; ;

                if(!per)
                    temp2 = temp2.next;

            }

            //Результат
            Students.Print();
            Console.WriteLine();

            //Разбиваем на две пары
            List M = new List();
            List W = new List();
            Node temp = Students.headNode;
            while (temp != null)
            {
                if (temp.student.pol.CompareTo("m") == 0)
                    M.Push(temp.student);
                else
                    W.Push(temp.student);

                temp = temp.next;
            }
            
            //Результаты деления
            M.Print();
            Console.WriteLine();
            W.Print();
            Console.WriteLine();

        }


        static void Main(string[] args)
        {
            //Создаем список
            List Students = new List();

            //Добавляем в список слова из файла
            //Создаем поток для чтения
            StreamReader sr = new StreamReader("students.txt", Encoding.GetEncoding(1251));

            //Считываем из файла, пока он не закончится
            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(' ');//Вся строка - разбивается на массив из трех элементов
                Student el=new Student(s[0], s[1], s[2]);//Добавляем в Студента теста
                //Закидываем в список
                Students.Push(el);
            }
        //    Students.Print();
            //Запускаем функцию визуализации работы
            Visualization(Students);

            Console.ReadKey();
        }
    }
}
