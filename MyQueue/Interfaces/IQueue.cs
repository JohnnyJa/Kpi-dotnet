namespace MyQueue.Interfaces;

public interface IQueue<T>
{
    
    public void Clear();
    
    public bool Contains(T item);
    
    public T Dequeue();

    public void Enqueue(T item);

    public T Peek();
}