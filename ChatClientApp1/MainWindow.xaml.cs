using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClientApp
{
    public partial class MainWindow : Window
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientNameInput.Text))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            try
            {
                _client = new TcpClient("192.168.69.249", 13000);
                _stream = _client.GetStream();

                // Send client name to the server
                string joinMessage = $"{ClientNameInput.Text}";
                byte[] data = Encoding.UTF8.GetBytes(joinMessage);
                await _stream.WriteAsync(data, 0, data.Length);

                ChatLog.Items.Add("Connected to the server.");
                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = true;
                SendButton.IsEnabled = true;

                _ = Task.Run(() => ReceiveMessagesAsync());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_client != null && _client.Connected)
                {
                    // Send a disconnect message to the server
                    string disconnectMessage = "DISCONNECT";
                    byte[] data = Encoding.UTF8.GetBytes(disconnectMessage);
                    await _stream.WriteAsync(data, 0, data.Length);

                    _client.Close();
                    _client = null;

                    ChatLog.Items.Add("Disconnected from the server.");
                    ConnectButton.IsEnabled = true;
                    DisconnectButton.IsEnabled = false;
                    SendButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Dispatcher.Invoke(() =>
                    {
                        ChatLog.Items.Add(message);
                    });
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ChatLog.Items.Add($"Error: {ex.Message}");
                });
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageInput.Text;

            if (!string.IsNullOrWhiteSpace(message))
            {
                // Send the message to the server
                byte[] data = Encoding.UTF8.GetBytes(message);
                await _stream.WriteAsync(data, 0, data.Length);
                ChatLog.Items.Add($"You: {message}");
                // Clear the input field without adding the message to the local log
                MessageInput.Clear();
            }
        }

    }
}
