using System.Collections.Generic;

namespace App
{
    /* Приоритетная очередь, реализованная списком, наследующийся 
     * от абстракного класса AbstractPriorityQueue и реализующий его 
     * абстракные методы
     * */
    class ListPriorityQueue<T> : AbstractPriorityQueue<T>
    {
        public ListPriorityQueue(Node<T> node)
        {
            _queue = new List<Node<T>>();
            _queue.Add(node);
        }

        public ListPriorityQueue()
        {
            _queue = new List<Node<T>>();
        }

        List<Node<T>> _queue;

        // Метод возвращение элемента с наивышсшим приоритетом
        public override Node<T> DeQueue()
        {
            // Если список пуст, то возвращаем null
            if (_queue.Count == 0)
            {
                return null;
            }

            /* Объяляем две переменные для хранения элемента с наибольшим приоритетом
             **/
            var temp = _queue[0];

            /* Проходим весь массив, сравнивая текущий элемент с временным переменным и, если 
             * его приоритет больше временного, то меняем значение временного переменного на текущий
             **/
            foreach (var node in _queue)
            {
                if (temp.Priority < node.Priority)
                {
                    temp = node;
                }
            }

            // Удаляем возвращаемый элемент по его индексу
            _queue.Remove(temp);
            return temp;
        }

        // Метод добавление элемента в очередь
        public override void EnQueue(Node<T> node)
        {
            _queue.Add(node);
        }
    }
}
