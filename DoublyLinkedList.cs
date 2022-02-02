using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class DoublyLinkedList<T> : IEnumerable<T>
    {
        private DoublyLinkedListItems<T> first;
        private DoublyLinkedListItems<T> last;

        public void AddFirst(T TValue)
        {
            DoublyLinkedListItems<T> newValue = new DoublyLinkedListItems<T>(TValue);
            if (first == null)
            {
                first = newValue;
                last = newValue;
                return;
            }

            first.Previous = newValue;
            newValue.Next = first;
            first = newValue;
        }
        public void AddLast(T TValue)
        {
            DoublyLinkedListItems<T> newValue = new DoublyLinkedListItems<T>(TValue);
            if (last == null)
            {
                first = newValue;
                last = newValue;
                return;
            }

            last.Next = newValue;
            newValue.Previous = last;
            last = newValue;
        }

        public T GetFirst()
        {
            if (first == null)
            {
                throw new ArgumentOutOfRangeException("The DoublyLinkedList doesn't contain a value.");
            }
            T returnValue = first.Value;
            if (first == last)
            {
                first = null;
                last = null;
                return returnValue;
            }

            first = first.Next;
            first.Previous = null;
            return returnValue;
        }
        public T GetLast()
        {
            if (last == null)
            {
                throw new ArgumentOutOfRangeException("The DoublyLinkedList doesn't contain a value.");
            }
            T returnValue = last.Value;
            if (first == last)
            {
                first = null;
                last = null;
                return returnValue;
            }

            last = last.Previous;
            last.Next = null;
            return returnValue;
        }

        public T PeekFirst()
        {
            if (first == null)
            {
                throw new ArgumentOutOfRangeException("The DoublyLinkedList doesn't contain a value.");
            }
            return first.Value;
        }

        public T PeekLast()
        {
            if (last == null)
            {
                throw new ArgumentOutOfRangeException("The DoublyLinkedList doesn't contain a value.");
            }
            return last.Value;
        }

        public int Count()
        {
            DoublyLinkedListItems<T> current = first;
            int count = 0;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        public T[] ToArray()
        {
            T[] returnArray = new T[this.Count()];
            DoublyLinkedListItems<T> current = first;
            for (int i = 0; i < this.Count(); i++)
            {
                returnArray[i] = current.Value;
                current = current.Next;
            }
            return returnArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListItems<T> current = first;
            while (current != null)
            {
                T returnValue = current.Value;
                current = current.Next;
                yield return returnValue;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
