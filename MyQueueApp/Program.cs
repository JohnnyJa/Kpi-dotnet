// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using MyQueue;

void OnVanished(object? sender, CollectionChangeEventArgs e) => Console.WriteLine("Oh no!!! Everything has been vanished.");
void OnDeleted(object? sender, CollectionChangeEventArgs e) => Console.WriteLine($"{e.Element} was deleted. Oops:("); 
void OnAdded(object? sender, CollectionChangeEventArgs e) => Console.WriteLine($"{e.Element} was added. Yey!!");

var queue = new MyQueue<int>();

queue.QueueCleared += OnVanished;

queue.ItemDeleted += OnDeleted;

queue.ItemAdded += OnAdded; 

queue.Enqueue(1);
queue.Enqueue(2);
queue.Enqueue(3);

queue.ItemAdded -= OnAdded;

queue.Enqueue(4);

queue.Dequeue();
queue.Dequeue();

queue.Clear();

try
{
    queue.Dequeue();
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e.Message);
}