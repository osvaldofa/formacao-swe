// Conversão baseada na relação de tipos
using System;

public class Documento
{
    public string Numero { get; set; }
}

public class NotaFiscal : Documento
{
    public decimal ValorTotal { get; set; }
}

public class Contrato : Documento
{
    public DateTime DataAssinatura { get; set; }
}

public class Pessoa
{
    public string Nome { get; set; }
}

// -------------------- DEMO --------------------
var nf = new NotaFiscal { Numero = "NF123", ValorTotal = 1500.50m };

// 1️⃣ Conversão implícita (upcasting) — de NotaFiscal (filha) para Documento (base)
Documento doc = nf; // Implícito
Console.WriteLine($"(Upcasting) Documento.Numero = {doc.Numero}");

// 2️⃣ Conversão explícita (downcasting) — de Documento para NotaFiscal
NotaFiscal nf2 = (NotaFiscal)doc; // Explícito
Console.WriteLine($"(Downcasting) NotaFiscal.ValorTotal = {nf2.ValorTotal}");

// 3️⃣ Conversão entre "irmãs" — precisa passar pela base e fazer cast
Contrato contrato = new Contrato { Numero = "CT456", DataAssinatura = DateTime.Now };

try
{
    // Isso vai gerar erro em runtime se o objeto não for compatível
    Contrato contratoInvalido = (Contrato)doc; 
    Console.WriteLine($"Contrato.Numero = {contratoInvalido.Numero}");
}
catch (InvalidCastException ex)
{
    Console.WriteLine($"(Entre irmãs) Falha no cast: {ex.Message}");
}

// 4️⃣ Sem relação de herança — precisa parse/manual
Pessoa pessoa = new Pessoa { Nome = "Maria" };
// Não é possível fazer cast direto: Documento -> Pessoa
// Aqui precisaríamos implementar uma conversão customizada
var pessoaViaParse = new Pessoa { Nome = doc.Numero }; 
Console.WriteLine($"(Sem herança) Pessoa.Nome = {pessoaViaParse.Nome}");
