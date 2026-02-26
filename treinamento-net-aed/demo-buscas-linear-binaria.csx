#r "System.Core"
using System;
using System.Diagnostics;

// Função para gerar sequência de strings
IEnumerable<string> GenerateStrings(int count)
{
    var list = new List<string>(count);
    int length = 5; // tamanho fixo, ex: "AAAAA"
    char[] chars = new char[length];
    for (int i = 0; i < length; i++) chars[i] = 'A';

    for (int n = 0; n < count; n++)
    {
        list.Add(new string(chars));

        // Incrementa "AAAAA" → "AAAAB" → ...
        for (int pos = length - 1; pos >= 0; pos--)
        {
            if (chars[pos] < 'Z')
            {
                chars[pos]++;
                break;
            }
            else
            {
                chars[pos] = 'A';
            }
        }
    }
    return list;
}

// Busca linear (O(n))
string LinearSearch(string[] arr, string target, out int comparisons)
{
    comparisons = 0;
    foreach (var item in arr)
    {
        comparisons++;
        if (item == target) return item;
    }
    return null;
}

// Busca binária (O(log n))
string BinarySearch(string[] arr, string target, out int comparisons)
{
    comparisons = 0;
    int left = 0, right = arr.Length - 1;
    while (left <= right)
    {
        comparisons++;
        int mid = (left + right) / 2;
        int cmp = string.Compare(arr[mid], target, StringComparison.Ordinal);

        if (cmp == 0) return arr[mid];
        if (cmp < 0) left = mid + 1;
        else right = mid - 1;
    }
    return null;
}

// Demo
int size = 50000;
var strings = GenerateStrings(size).ToList();

// Array desordenado para busca linear
var random = new Random();
var shuffled = strings.OrderBy(_ => random.Next()).ToArray();

// Array ordenado para busca binária
var ordered = strings.ToArray();
Array.Sort(ordered);

// Número de buscas para acumular tempo
int repetitions = 1000;
var sw = new Stopwatch();

long totalLinearComparisons = 0;
long totalBinaryComparisons = 0;

// Busca Linear
sw.Start();
for (int i = 0; i < repetitions; i++)
{
    var target = strings[random.Next(size)];
    int comps;
    var resultLinear = LinearSearch(shuffled, target, out comps);
    totalLinearComparisons += comps;
}
sw.Stop();
Console.WriteLine($"Busca Linear ({repetitions} buscas): {sw.ElapsedMilliseconds} ms, Comparações totais: {totalLinearComparisons}, Média: {totalLinearComparisons / repetitions}");

// Busca Binária
sw.Restart();
for (int i = 0; i < repetitions; i++)
{
    var target = strings[random.Next(size)];
    int comps;
    var resultBinary = BinarySearch(ordered, target, out comps);
    totalBinaryComparisons += comps;
}
sw.Stop();
Console.WriteLine($"Busca Binária ({repetitions} buscas): {sw.ElapsedMilliseconds} ms, Comparações totais: {totalBinaryComparisons}, Média: {totalBinaryComparisons / repetitions}");
