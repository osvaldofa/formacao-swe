using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Configuração do cliente
var ip = "127.0.0.1";
var port = 5000;

var client = new TcpClient();
client.Connect(ip, port);
Console.WriteLine("[Cliente] Conectado ao servidor.");

// Envia mensagem
string mensagem = "Olá servidor!";
byte[] mensagemBytes = Encoding.UTF8.GetBytes(mensagem);
var stream = client.GetStream();
stream.Write(mensagemBytes, 0, mensagemBytes.Length);
Console.WriteLine("[Cliente] Mensagem enviada.");

// Lê resposta
byte[] buffer = new byte[1024];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string resposta = Encoding.UTF8.GetString(buffer, 0, bytesRead);

Console.WriteLine($"[Cliente] Resposta do servidor: {resposta}");