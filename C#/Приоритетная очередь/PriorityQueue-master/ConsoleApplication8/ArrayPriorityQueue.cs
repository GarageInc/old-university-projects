using System;

namespace App
{
    /* Приоритетная очередь, реализованная массивом, наследующийся 
     * от абстракного класса AbstractPriorityQueue и реализующий его 
     * абстракные методы
     * */
    class ArrayPriorityQueue<T> : AbstractPriorityQueue<T>
    {
        private Node<T>[] _queue;

        public ArrayPriorityQueue()
        {
            _queue = new Node<T>[0];
        }
        public ArrayPriorityQueue(Node<T> node)
        {
            _queue = new Node<T>[1]{node};
        }

        // Метод возвращение элемента с наивышсшим приоритетом
        public override Node<T> DeQueue()
        {
            // Если массив пуст, то возвращаем null
            if (_queue.Length == 0)
            {
                return null;
            }

            /* Объяляем две переменные для хранения элемента с наибольшим приоритетом
             * и его индекса (он нужен при удаление возвращаемого элемента)
             **/
            var temp = _queue[0];
            int index = 0;

            /* Проходим весь массив, сравнивая текущий элемент с временным переменным и, если 
             * его приоритет больше временного, то меняем значение временного переменного на текущий
             * и сохраняем его индекс
             **/
            for (int i = 0; i < _queue.Length; i++)
            {
                if (temp.Priority < _queue[i].Priority)
                {
                    temp = _queue[i];
                    index = i;
                }
            }

            // Меняем местами элемент с наибольшим приоритетом и последний
            var buf = _queue[_queue.Length - 1];
            _queue[_queue.Length - 1] = _queue[index];
            _queue[index] = buf;

            /* При изменении размера массива удаляется последний элемент,
             * тем самым мы избавляемся от возвращаемого элемента
             * */
            Array.Resize(ref _queue, _queue.Length - 1);
            return temp;
        }

        // Метод добавление элемента в очередь
        public override void EnQueue(Node<T> node)
        {
            /* Увеличиваем размер массив на один элемент 
             * и на присваеваем новому элементу значение аргумента
             **/
            Array.Resize(ref _queue, _queue.Length + 1);
            _queue[_queue.Length - 1] = node;
        }
    }
}
