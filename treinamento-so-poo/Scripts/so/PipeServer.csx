using System.IO.Pipes;
using System.IO;
using System;

var server = new NamedPipeServerStream("meuPipe");

Console.WriteLine("Aguardando conexão...");
server.WaitForConnection(); // 🔹 Aqui o servidor fica bloqueado até o cliente conectar

var writer = new StreamWriter(server);
writer.AutoFlush = true;

writer.WriteLine("Olá, cliente, aqui é o Servidor Supremo da Galáxia!");
Console.WriteLine("Mensagem enviada para o cliente.");