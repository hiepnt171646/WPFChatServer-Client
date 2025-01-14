using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatClientApp
{
    public partial class MainWindow : Window
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private ObservableCollection<ChatMessage> _chatMessages;

        public MainWindow()
        {
            InitializeComponent();
            _chatMessages = new ObservableCollection<ChatMessage>();
            ChatLog.ItemsSource = _chatMessages;
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
                _client = new TcpClient("127.0.0.1", 13000);
                _stream = _client.GetStream();

                // Send client name to the server
                string joinMessage = $"{ClientNameInput.Text}";
                byte[] data = Encoding.ASCII.GetBytes(joinMessage.Trim());

                await _stream.WriteAsync(data, 0, data.Length);

                AddMessage("Connected to the server.", Colors.Green);
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
                    byte[] data = Encoding.ASCII.GetBytes(disconnectMessage);
                    await _stream.WriteAsync(data, 0, data.Length);

                    _client.Close();
                    _client = null;

                    AddMessage("Disconnected from the server.", Colors.Red);
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

                    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                    string[] parts = receivedData.Split('|');
                    if (parts.Length == 2)
                    {
                        string colorName = parts[0];
                        string message = parts[1];

                        Dispatcher.Invoke(() =>
                        {
                            AddMessage(message, (Color)ColorConverter.ConvertFromString(colorName));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    AddMessage($"Error: {ex.Message}", Colors.Red);
                });
            }
        }


        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageInput.Text;
            string selectedColor = (ColorPicker.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Black";

            if (!string.IsNullOrWhiteSpace(message))
            {
                string messageWithColor = $"{selectedColor}|{message}";
                byte[] data = Encoding.ASCII.GetBytes(messageWithColor);
                await _stream.WriteAsync(data, 0, data.Length);

                AddMessage($"You: {message}", (Color)ColorConverter.ConvertFromString(selectedColor));
                MessageInput.Clear();
            }
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true; // Prevent the default behavior of adding a newline
                SendButton_Click(sender, e);
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
