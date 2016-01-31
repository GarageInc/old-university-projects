namespace App
{
    // Абстракный класс
    abstract class AbstractPriorityQueue<T>
    {
        // Метод возвращения элемента
        public abstract Node<T> DeQueue();
        // Метод добавления нового элемента 
        public abstract void EnQueue(Node<T> node);
    }
}
