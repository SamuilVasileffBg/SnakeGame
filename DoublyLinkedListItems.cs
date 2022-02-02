using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class DoublyLinkedListItems<T>
    {
        public DoublyLinkedListItems()
        {
        }
        public DoublyLinkedListItems(T value)
        {
            Value = value;
        }

        public DoublyLinkedListItems<T> Next { get; set; }
        public DoublyLinkedListItems<T> Previous { get; set; }
        public T Value { get; set; } = default(T);
    }
}
