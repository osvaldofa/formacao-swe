// Lista
var lista = new List<string>();
lista.Add("Ana");
lista.Add("Bruno");
lista.Add("Carla");

Console.WriteLine("=== Lista ===");
foreach (var nome in lista)
{
    Console.WriteLine(nome);
}

// Fila (Queue) - FIFO (First In, First Out)
var fila = new Queue<string>();
fila.Enqueue("Primeiro");
fila.Enqueue("Segundo");
fila.Enqueue("Terceiro");

Console.WriteLine("\n=== Fila ===");
while (fila.Count > 0)
{
    Console.WriteLine($"Saindo: {fila.Dequeue()}");
}

// Pilha (Stack) - LIFO (Last In, First Out)
var pilha = new Stack<string>();
pilha.Push("Topo 1");
pilha.Push("Topo 2");
pilha.Push("Topo 3");

Console.WriteLine("\n=== Pilha ===");
while (pilha.Count > 0)
{
    Console.WriteLine($"Saindo: {pilha.Pop()}");
}
