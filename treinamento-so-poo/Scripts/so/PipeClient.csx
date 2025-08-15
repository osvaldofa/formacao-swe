using System.IO.Pipes;
using System.IO;
using System;

var client = new NamedPipeClientStream(".", "meuPipe", PipeDirection.In);

Console.WriteLine("Conectando ao servidor...");
client.Connect(); // 🔹 Aqui o cliente conecta

var reader = new StreamReader(client);
var mensagem = reader.ReadLine();

Console.WriteLine($"Mensagem recebida: {mensagem}");