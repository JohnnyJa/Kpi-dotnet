using System.Collections;
using MyQueue.Interfaces;

namespace MyQueue;

public class MyQueue<T> : IEnumerable<T>, ICollection, IQueue<T>
{
    private Node? _head;
    private Node? _tail;
    private int _size = 0;


    public int Count => _size;
    public bool IsSynchronized => false;
    public object SyncRoot => this;

    public void CopyTo(Array array, int index)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be greater than 0.");
        }

        if (index > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index,
                "Index must be less or equal than size of array.");
        }

        if (array.Length - index < _size)
        {
            throw new ArgumentException("Invalid length of the array.");
        }

        if (_head is null)
        {
            return;
        }

        var sourceArray = ToArray();

        Array.Copy(sourceArray, 0, array, index, _size);
    }

    public void Clear()
    {
        _size = 0;
        _head = null;
    }

    public bool Contains(T item)
    {
        if (_head is null) return false;

        var currentNode = new Node
        {
            Value = _head.Value,
            Next = _head.Next
        };

        while (currentNode is not null)
        {
            //Why is this warning here????
            if (currentNode.Value!.Equals(item))
            {
                return true;
            }

            currentNode = currentNode.Next;
        }

        return false;
    }

    public void Enqueue(T item)
    {
        if (_head is null)
        {
            _head = new Node()
            {
                Value = item,
                Next = null
            };
            _tail = _head;
        }
        else
        {
            var newNode = new Node()
            {
                Value = item,
                Next = null
            };
            _tail!.Next = newNode;          //Will never be null if head isn't null
            _tail = _tail.Next;
        }

        _size++;
    }

    public T Dequeue()
    {
        if (_head is null)
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        
        var removed = _head.Value;
        _head = _head.Next;
        
        _size--;
        
        return removed;
    }

    public T Peek()
    {
        if (_head is null)
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        
        return _head.Value;
    }

    public T[] ToArray()
    {
        if (_head is null)
        {
            return Array.Empty<T>();
        }
        
        var currentNode = new Node()
        {
            Value = _head.Value,
            Next = _head.Next
        };
        
        var array = new T[_size];
        int i = 0;

        while (currentNode is not null)
        {
            array[i++] = currentNode.Value;
            currentNode = currentNode.Next;
        }

        return array;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new MyEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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

            //Has already been checked in ctor
            _currentNode = _queue._head!;
        }

        public void Dispose()
        {
            _ended = true;
        }
    }

    private class Node
    {
        public required T Value { get; set; }
        public required Node? Next { get; set; }
    }
}