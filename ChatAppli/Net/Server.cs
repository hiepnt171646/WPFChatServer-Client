using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatAppli.Net
{
    public class Server
    {
        private TcpListener _listener;
        private readonly List<TcpClient> _clients = new List<TcpClient>();

        public async Task StartServer(string host, int port)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Parse(host), port);
                _listener.Start();
                Console.WriteLine($"Server started on {host}:{port}");

                while (true)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    lock (_clients)
                    {
                        _clients.Add(client);
                    }
                    Console.WriteLine("New client connected.");

                    _ = Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server error: {ex.Message}");
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            try
            {
                using var reader = new StreamReader(client.GetStream());
                while (client.Connected)
                {
                    var message = await reader.ReadLineAsync();
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine(message); // Log message to server console
                        BroadcastMessage(message, client);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                lock (_clients)
                {
                    _clients.Remove(client);
                }
                client.Close();
            }
        }

        private void BroadcastMessage(string message, TcpClient sender)
        {
            lock (_clients)
            {
                foreach (var client in _clients)
                {
                    if (client == sender) continue;

                    try
                    {
                        var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                        writer.WriteLine(message);
                    }
                    catch
                    {
                        // Ignore any errors during broadcasting
                    }
                }
            }
        }
    }
}
