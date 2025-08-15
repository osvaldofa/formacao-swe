using System;
using System.Diagnostics;

const int tamanho = 100_000_000; // ~400MB (int = 4 bytes)
int[] array = new int[tamanho];

// Inicializa o array (para evitar paginação preguiçosa)
for (int i = 0; i < tamanho; i++)
    array[i] = i;

Console.WriteLine("Iniciando benchmark...");

// Acesso sequencial
var watchSeq = Stopwatch.StartNew();
long somaSeq = 0;
for (int i = 0; i < array.Length; i++)
    somaSeq += array[i];
watchSeq.Stop();
Console.WriteLine($"Acesso SEQUENCIAL: {watchSeq.ElapsedMilliseconds} ms");

// Acesso aleatório
var rand = new Random(42); // semente fixa para consistência
var watchRand = Stopwatch.StartNew();
long somaRand = 0;
for (int i = 0; i < array.Length; i++)
{
    int index = rand.Next(array.Length);
    somaRand += array[index];
}
watchRand.Stop();
Console.WriteLine($"Acesso ALEATÓRIO: {watchRand.ElapsedMilliseconds} ms");

// Previne otimização
Console.WriteLine($"Somas: Seq = {somaSeq}, Rand = {somaRand}");