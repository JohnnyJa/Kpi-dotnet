using System.Collections;
using MyQueue.Interfaces;

namespace MyQueue;

public class MyQueue<T> : IEnumerable<T>
{
    private Node? _head;
    private Node? _tail;
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private class Node
    {
        public required T Value { get; set; }
        public required Node? Next { get; set; }
    }
    
    private class MyEnumerator : IEnumerator<T>
    {
        private readonly MyQueue<T> _queue;
        private Node _currentNode;
        private bool _ended;
     
        internal MyEnumerator(MyQueue<T> queue)
        {
            _queue = queue;
            _ended = false;
            _currentNode = _queue._head ?? throw new NullReferenceException("Root wasn't instantiated");
        }

        public T Current
        {
            get
            {
                if (_ended)
                {
                    throw new InvalidOperationException("Enum has already ended");
                }
                
                return _currentNode.Value!;
            }
        }

        object IEnumerator.Current => Current!;
        
        public bool MoveNext()
        {
            if (_ended)
            {
                return false;
            }

            if (_currentNode.Next is null)
            {
                _ended = true;
                return false;
            }

            _currentNode = _currentNode.Next;
            return true;

        }

        public void Reset()
        {
            _ended = false;
            _currentNode = _queue._head!; //Has already been checked in ctor
        }

        public void Dispose()
        {
            _ended = true;
        }
    }
}