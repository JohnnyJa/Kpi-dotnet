// See https://aka.ms/new-console-template for more information

using MyQueue;

var queue = new MyQueue<int>();

queue.QueueCleared += delegate { Console.WriteLine("Oh no!!! Everything has been vanished."); };

queue.ItemDeleted += delegate { Console.WriteLine("Something was deleted. Oops:("); };

queue.ItemAdded += delegate { Console.WriteLine("Something was added. Yey!!"); }; 

queue.Enqueue(1);
queue.Enqueue(2);
queue.Enqueue(3);
queue.Enqueue(4);

queue.Dequeue();
queue.Dequeue();

queue.Clear();
