using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ChatClientWinForms
{
    public class ChatService
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _receiveThread;
        private bool _isRunning;

        public event Action<string> MessageReceived;
        public event Action<List<String>> OnUsersUpdated;

        public bool Connect(string ip, int port, string username)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(ip, port);
                _stream = _client.GetStream();
                _isRunning = true;

                byte[] msgBytes = Encoding.UTF8.GetBytes(username);
                _stream.Write(msgBytes, 0, msgBytes.Length);

                _receiveThread = new Thread(() =>
                {
                    ReceiveLoop();
                });
          
                _receiveThread.Start();
                Debug.WriteLine($"DEBUG: Подключение установлено, поток запущен");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ОШИБКА Connect: {e.Message}");
                MessageBox.Show($"Ошибка подключения: {e.Message}");
                return false;
            }
        }
        private void ReceiveLoop()
        {
            byte[] buffer = new byte[4096];
            StringBuilder messageBuilder = new StringBuilder();

            try
            {
                while (_isRunning && _client.Connected)
                {
                   
                    if (_stream.DataAvailable)
                    {
                        int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            messageBuilder.Append(received);
                
                            string allData = messageBuilder.ToString();
                            int newlineIndex;

                            while ((newlineIndex = allData.IndexOf('\n')) != -1)
                            {
                                string message = allData.Substring(0, newlineIndex).Trim();

                                if (message.StartsWith("/ULIST "))
                                {
                                    string usersString = message.Substring("/ULIST ".Length);
                                    List<string> users = usersString.Split(',')
                                        .Where(u => !string.IsNullOrEmpty(u))
                                        .ToList();

                                    OnUsersUpdated?.Invoke(users);
                                }
                                else if (!string.IsNullOrEmpty(message))
                                {
                                    MessageReceived?.Invoke(message);
                                }

                                allData = allData.Substring(newlineIndex + 1);
                            }

                            messageBuilder.Clear();
                            messageBuilder.Append(allData);
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Ошибка в ReceiveLoop: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("DEBUG: ReceiveLoop завершил работу");
            }
        }
        public void SendMessage(string message, bool isPrivate = false, string receiverUser = "")
        {
            if (!_client.Connected) return;

            string messageToSend;

            if (isPrivate && !string.IsNullOrEmpty(receiverUser))
            {
                messageToSend = $"/pm {receiverUser} {message}";
            }
            else
            {
                messageToSend = message;
            }
            byte[] data = Encoding.UTF8.GetBytes(messageToSend + "\n");
            _stream.Write(data, 0, data.Length);
            _stream.Flush();
        }
        public void Disconnect()
        {
            _isRunning = false;
            SendMessage("/exit");
            _receiveThread?.Join(1000);
            _stream?.Close();
            _client?.Close();
        }

    }
}
