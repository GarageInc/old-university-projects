﻿
/*
  * Напишите реализацию класса, описывающего Приоритетную очередь, в которой
  * важно не то, кто встал последним (порядок помещения в неё не играет роли), 
  * а кто главнее. Более точно, при помещении в очередь указывается приоритет
  * помещаемого объекта (будем считать приоритеты целыми числами), а при взятии из 
  * очереди выбирается элемент с наибольшим приоритетом (или один из таких элементов).
  * 
  * Постройте семейство классов, начиная с абстрактного класса и заканчивая разными 
  * классами потомками. Постройте реализации, основанные на массивах и на списковой структуре данных.
  * Постройте классы потомки, хранящие в очереди элементы фиксированного типа. 
  * Постройте Windows-проект, демонстрирующий полиморфизм построенного семейства классов.
  */

using System;

namespace App
{
    class Expression
    {
        static void Main(string[] args)
        {
            AbstractPriorityQueue<string> priority = new ArrayPriorityQueue<string>();
            for (int i = 0; i < 5; i++)
            {
                priority.EnQueue(new Node<string>(Console.ReadLine(), i));
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(priority.DeQueue().Value);
            }

            priority = new ListPriorityQueue<string>();
            for (int i = 0; i < 5; i++)
            {
                priority.EnQueue(new Node<string>(Console.ReadLine(), i));
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(priority.DeQueue().Value);
            }
            Console.ReadKey();
        }
    }
}