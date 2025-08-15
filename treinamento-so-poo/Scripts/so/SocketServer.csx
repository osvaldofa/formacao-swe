using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Configuração do servidor
var ip = IPAddress.Loopback; // localhost
var port = 5000;
var listener = new TcpListener(ip, port);

listener.Start();
Console.WriteLine($"[Servidor] Aguardando conexão em {ip}:{port}...");

// Aceita a conexão do cliente
var client = listener.AcceptTcpClient();
Console.WriteLine("[Servidor] Cliente conectado.");

// Lê a mensagem enviada
var stream = client.GetStream();
byte[] buffer = new byte[1024];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string mensagem = Encoding.UTF8.GetString(buffer, 0, bytesRead);

Console.WriteLine($"[Servidor] Recebido: {mensagem}");

// Responde ao cliente
string resposta = "Mensagem recebida com sucesso!";
byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);
stream.Write(respostaBytes, 0, respostaBytes.Length);

Console.WriteLine("[Servidor] Resposta enviada.");
listener.Stop();
