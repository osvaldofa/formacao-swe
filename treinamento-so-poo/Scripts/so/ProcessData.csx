using System;
using System.Diagnostics;

var processoAtual = Process.GetCurrentProcess();

Console.WriteLine($"Processo: {processoAtual.ProcessName}");
Console.WriteLine($"PID: {processoAtual.Id}");
Console.WriteLine($"Threads ativas: {processoAtual.Threads.Count}");
Console.WriteLine();

// Percorre as threads do processo
foreach (ProcessThread thread in processoAtual.Threads)
{
    Console.WriteLine($"Thread ID: {thread.Id}");
    Console.WriteLine($"  Estado: {thread.ThreadState}");
    Console.WriteLine($"  Prioridade: {thread.BasePriority}");
    Console.WriteLine($"  Tempo de CPU: {thread.TotalProcessorTime}");
    Console.WriteLine();
}