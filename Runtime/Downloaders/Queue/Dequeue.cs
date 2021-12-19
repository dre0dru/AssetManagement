using System.Collections.Generic;

namespace AddressableAssets.Downloaders.Queue
{
    public class Dequeue<T>
    {
        private readonly LinkedList<T> _linkedList;

        public int Count => _linkedList.Count;
        
        public Dequeue()
        {
            _linkedList = new LinkedList<T>();
        }

        public void EnqueueFirst(T value)
        {
            _linkedList.AddFirst(value);
        }

        public T PeekFirst()
        {
            return _linkedList.First.Value;
        }

        public void EnqueueLast(T value)
        {
            _linkedList.AddLast(value);
        }

        public T PeekLast()
        {
            return _linkedList.Last.Value;
        }

    }
}
