namespace App
{
    /* Элемент приоритетной очереди.
     * Переменная Т хранит значение элемента, а Priority хранит приоритет элемента.
     */
    class Node<T>
    {
        public T Value { get; set; }
        public int Priority { get; set; }

        public Node(){}

        public Node(T value, int priority)
        {
            Value = value;
            Priority = priority;
        } 
    }
}
