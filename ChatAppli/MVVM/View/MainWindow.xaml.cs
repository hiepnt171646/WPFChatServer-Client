using ChatClientApp;
using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatAppli
{
    public partial class MainWindow : Window
    {
        private TcpListener _server;
        private ConcurrentDictionary<TcpClient, string> _connectedClients = new ConcurrentDictionary<TcpClient, string>();
        private ObservableCollection<ChatMessage> _chatMessages;
        private ObservableCollection<string> _connectedClientNames;

        public MainWindow()
        {
            InitializeComponent();
            _chatMessages = new ObservableCollection<ChatMessage>();
            ServerChatHistory.ItemsSource = _chatMessages;

            _connectedClientNames = new ObservableCollection<string>();
            ClientList.ItemsSource = _connectedClientNames;
        }


        private async void btnStartServer_Click(object sender, RoutedEventArgs e)
        {
            string host = "127.0.0.1";
            int port = 13000;

            try
            {
                _server = new TcpListener(IPAddress.Parse(host), port);
                _server.Start();

                AddMessage("Server started. Waiting for clients...", Colors.Green);

                await Task.Run(() => AcceptClientsAsync());
            }
            catch (Exception ex)
            {
                AddMessage($"Error: {ex.Message}", Colors.Red);
            }
        }

        private async Task AcceptClientsAsync()
        {
            while (true)
            {
                TcpClient client = await _server.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            string clientName = string.Empty;

            try
            {
                using NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                // First message from client is their name
                int nameBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                clientName = Encoding.ASCII.GetString(buffer, 0, nameBytesRead).Trim();

                _connectedClients.TryAdd(client, clientName);

                Dispatcher.Invoke(() =>
                {
                    _connectedClientNames.Add(clientName);
                    ClientCount.Text = $"Total Clients: {_connectedClientNames.Count}";
                    AddMessage($"{clientName} has joined.", Colors.Blue);
                });

                // Notify all clients about the new user
                await BroadcastMessageAsync($"{clientName} has joined the chat.", client, "Blue");

                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                    string[] parts = receivedData.Split('|');
                    if (parts.Length == 2)
                    {
                        string colorName = parts[0];
                        string message = parts[1];
                        string formattedMessage = $"{clientName}: {message}";

                        // Add the message to the server UI log
                        Dispatcher.Invoke(() =>
                        {
                            AddMessage(formattedMessage, (Color)ColorConverter.ConvertFromString(colorName));
                        });

                        // Broadcast the message to all other clients
                        await BroadcastMessageAsync(formattedMessage, client, colorName);
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    AddMessage($"Error with {clientName}: {ex.Message}", Colors.Red);
                });
            }
            finally
            {
                _connectedClients.TryRemove(client, out _);
                Dispatcher.Invoke(() =>
                {
                    _connectedClientNames.Remove(clientName);
                    ClientCount.Text = $"Total Clients: {_connectedClientNames.Count}";
                    AddMessage($"{clientName} has left.", Colors.Gray);
                });

                await BroadcastMessageAsync($"{clientName} has left the chat.", client, "Gray");
                client.Close();
            }
        }




        private async Task BroadcastMessageAsync(string message, TcpClient sender, string colorName)
        {
            byte[] data = Encoding.ASCII.GetBytes($"{colorName}|{message}");

            foreach (var client in _connectedClients.Keys)
            {
                if (client.Connected && client != sender)
                {
                    try
                    {
                        await client.GetStream().WriteAsync(data, 0, data.Length);
                    }
                    catch
                    {
                        // Ignore failed clients
                    }
                }
            }
        }




        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string serverMessage = ServerMessageInput.Text;
            string selectedColor = (ServerColorPicker.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Black";

            if (!string.IsNullOrWhiteSpace(serverMessage))
            {
                AddMessage($"Server: {serverMessage}", (Color)ColorConverter.ConvertFromString(selectedColor));
                _ = BroadcastMessageAsync($"Server: {serverMessage}", null, selectedColor);
                ServerMessageInput.Clear();
            }
        }

        private void ServerMessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true; // Prevent the default behavior of adding a newline
                btnSendMessage_Click(sender, e);
            }
        }


        private void AddMessage(string message, Color color)
        {
            _chatMessages.Add(new ChatMessage
            {
                Message = message,
                Color = new SolidColorBrush(color)
            });
        }
    }
}
