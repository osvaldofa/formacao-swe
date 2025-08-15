using System;
using System.Diagnostics;

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
Parallel.For(0, 5, i =>
{
    TrabalhoPesado();
});
sw.Stop();
Console.WriteLine($"Tempo paralelo: {sw.ElapsedMilliseconds} ms");

void TrabalhoPesado()
{
    Task.Delay(1000).Wait();
}