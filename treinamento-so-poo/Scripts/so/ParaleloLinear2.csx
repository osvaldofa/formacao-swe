using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

var sw = new Stopwatch();

// Execução linear
sw.Start();
for (int i = 0; i < 5; i++)
{
    TrabalhoPesado();
}
sw.Stop();
Console.WriteLine($"Tempo linear: {sw.ElapsedMilliseconds} ms");

// Execução paralela
sw.Restart();
var threads = new List<Thread>();

for (int i = 0; i < 5; i++)
{
    var t = new Thread(TrabalhoPesado);
    threads.Add(t);
    t.Start();
}

// Aguarda todas as threads terminarem
foreach (var t in threads)
{
    t.Join();
}
sw.Stop();
Console.WriteLine($"Tempo paralelo: {sw.ElapsedMilliseconds} ms");

void TrabalhoPesado()
{
    Task.Delay(1000).Wait();
}