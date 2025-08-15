// Classe base
public class Animal
{
    public string Nome { get; set; }

    public Animal(string nome)
    {
        Nome = nome;
    }

    public virtual void Falar()
    {
        Console.WriteLine($"{Nome} faz um som.");
    }
}

// Classe derivada 1
public class Cachorro : Animal
{
    public Cachorro(string nome) : base(nome) { }

    public override void Falar()
    {
        Console.WriteLine($"{Nome} diz: Au Au!");
    }
}

// Classe derivada 2
public class Gato : Animal
{
    public Gato(string nome) : base(nome) { }

    public override void Falar()
    {
        Console.WriteLine($"{Nome} diz: Miau!");
    }
}

// Execução no script
var animais = new List<Animal>
{
    new Cachorro("Rex"),
    new Gato("Mimi")
};

foreach (var animal in animais)
{
    animal.Falar();
}
