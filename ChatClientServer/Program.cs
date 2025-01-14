using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverHost = "127.0.0.1";
            int serverPort = 13000;

            Console.WriteLine("Attempting to connect to the server...");
            while (true)
            {
                try
                {
                    using TcpClient client = new TcpClient(serverHost, serverPort);
                    Console.WriteLine("Connected to the server!");

                    using NetworkStream stream = client.GetStream();
                    string message = "Hello, Server!";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                    Console.WriteLine($"Sent: {message}");

                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string serverResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Server says: {serverResponse}");

                    break; // Exit loop after successful connection
                }
                catch (SocketException)
                {
                    Console.WriteLine("Server not ready. Retrying in 2 seconds...");
                    Thread.Sleep(2000); // Wait before retrying
                }
            }
        }
    }
}
