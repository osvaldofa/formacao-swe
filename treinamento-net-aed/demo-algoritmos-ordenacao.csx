#r "System.Core"
using System;
using System.Diagnostics;

public static class SortingAlgorithms
{
    // ----------------- QuickSort -----------------
    public static (TimeSpan, long) QuickSort(int[] array)
    {
        long comparisons = 0;
        var stopwatch = Stopwatch.StartNew();
        QuickSortRecursive(array, 0, array.Length - 1, ref comparisons);
        stopwatch.Stop();
        return (stopwatch.Elapsed, comparisons);
    }

    private static void QuickSortRecursive(int[] array, int low, int high, ref long comparisons)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high, ref comparisons);
            QuickSortRecursive(array, low, pivotIndex - 1, ref comparisons);
            QuickSortRecursive(array, pivotIndex + 1, high, ref comparisons);
        }
    }

    private static int Partition(int[] array, int low, int high, ref long comparisons)
    {
        int pivot = array[high]; // pivô = último elemento
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            comparisons++;
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, high);
        return i + 1;
    }

    private static void Swap(int[] array, int i, int j)
    {
        if (i != j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    // ----------------- MergeSort -----------------
    public static (TimeSpan, long) MergeSort(int[] array)
    {
        long comparisons = 0;
        var stopwatch = Stopwatch.StartNew();
        MergeSortRecursive(array, 0, array.Length - 1, ref comparisons);
        stopwatch.Stop();
        return (stopwatch.Elapsed, comparisons);
    }

    private static void MergeSortRecursive(int[] array, int left, int right, ref long comparisons)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSortRecursive(array, left, middle, ref comparisons);
            MergeSortRecursive(array, middle + 1, right, ref comparisons);
            Merge(array, left, middle, right, ref comparisons);
        }
    }

    private static void Merge(int[] array, int left, int middle, int right, ref long comparisons)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        int[] L = new int[n1];
        int[] R = new int[n2];

        Array.Copy(array, left, L, 0, n1);
        Array.Copy(array, middle + 1, R, 0, n2);

        int i = 0, j = 0, k = left;
        while (i < n1 && j < n2)
        {
            comparisons++;
            if (L[i] <= R[j])
            {
                array[k++] = L[i++];
            }
            else
            {
                array[k++] = R[j++];
            }
        }

        while (i < n1) array[k++] = L[i++];
        while (j < n2) array[k++] = R[j++];
    }

    // ----------------- HeapSort -----------------
    public static (TimeSpan, long) HeapSort(int[] array)
    {
        long comparisons = 0;
        var stopwatch = Stopwatch.StartNew();
        int n = array.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(array, n, i, ref comparisons);

        for (int i = n - 1; i > 0; i--)
        {
            Swap(array, 0, i);
            Heapify(array, i, 0, ref comparisons);
        }

        stopwatch.Stop();
        return (stopwatch.Elapsed, comparisons);
    }

    private static void Heapify(int[] array, int n, int i, ref long comparisons)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n)
        {
            comparisons++;
            if (array[left] > array[largest])
                largest = left;
        }

        if (right < n)
        {
            comparisons++;
            if (array[right] > array[largest])
                largest = right;
        }

        if (largest != i)
        {
            Swap(array, i, largest);
            Heapify(array, n, largest, ref comparisons);
        }
    }

    // ----------------- Array.Sort -----------------
    public static (TimeSpan, long) DotNetSort(int[] array)
    {
        long comparisons = 0;

        var stopwatch = Stopwatch.StartNew();
        Array.Sort(array, (a, b) => { comparisons++; return a.CompareTo(b); });
        stopwatch.Stop();

        return (stopwatch.Elapsed, comparisons);
    }
}

// ----------------- Programa Principal -----------------
var rand = new Random();
int size = 100_000;
int[] original = new int[size];
for (int i = 0; i < size; i++)
    original[i] = rand.Next(0, 1_000_000);

// cópias para cada algoritmo
int[] arrQuick = (int[])original.Clone();
int[] arrMerge = (int[])original.Clone();
int[] arrHeap = (int[])original.Clone();
int[] arrDotNet = (int[])original.Clone();

var quick = SortingAlgorithms.QuickSort(arrQuick);
Console.WriteLine($"QuickSort -> Tempo: {quick.Item1.TotalMilliseconds} ms | Comparações: {quick.Item2}");

var merge = SortingAlgorithms.MergeSort(arrMerge);
Console.WriteLine($"MergeSort -> Tempo: {merge.Item1.TotalMilliseconds} ms | Comparações: {merge.Item2}");

var heap = SortingAlgorithms.HeapSort(arrHeap);
Console.WriteLine($"HeapSort  -> Tempo: {heap.Item1.TotalMilliseconds} ms | Comparações: {heap.Item2}");

var dotnet = SortingAlgorithms.DotNetSort(arrDotNet);
Console.WriteLine($"Array.Sort -> Tempo: {dotnet.Item1.TotalMilliseconds} ms | Comparações: {dotnet.Item2}");
