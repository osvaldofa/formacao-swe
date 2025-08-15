using System;
using System.Threading;

object locker = new object();

void AcessarRecurso(int id)
{
    lock (locker)
    {
        Console.WriteLine($"Thread {id} acessando recurso...");
        Thread.Sleep(1000);
        Console.WriteLine($"Thread {id} liberou recurso.");
    }
}

Thread[] threads = new Thread[5];

for (int i = 0; i < threads.Length; i++)
{
    int id = i;
    threads[i] = new Thread(() => AcessarRecurso(id));
    threads[i].Start();
}

foreach (var t in threads)
{
    t.Join();
}
