using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatServer
{
    internal class ClientHandler
    {
        private ClientInfo _clientInfo;
        private List<ClientInfo> _clients;

        public ClientHandler(ClientInfo clientInfo, List<ClientInfo> clients)
        {
            this._clientInfo = clientInfo;
            this._clients = clients;
        }

        public void Handle()
        {
            try
            {
                NetworkStream stream = _clientInfo.Client.GetStream();
                byte[] receiveBuffer = new byte[4096];

                //уведомляем всех клиентов о новом подклчючении
                int nickBytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                string nickName = Encoding.UTF8.GetString(receiveBuffer, 0, nickBytes);
                string welcomeMessage = $"[SYSTEM] {nickName} joined";
                _clientInfo.NickName = nickName;

                Console.WriteLine($"{DateTime.Now:HH:mm:ss}| Установлено соединение: [{nickName}] ({_clientInfo.IPAdress})");
                
                MessageDispatcher.Broadcast(welcomeMessage, _clients, _clientInfo);
                Thread.Sleep(100);
                BroadcastUserList();
                // цикл чтения сообщений от клиента
                while (_clientInfo.Client.Connected)
                {
                    int bytesReceived = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    string data = Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived).Trim();
                    string formattedData = $"[{nickName}] {data}";

                    if (bytesReceived == 0)
                    {
                        break;
                    }

                    //обработка команд
                    if (data.StartsWith('/'))
                    {
                        ProcessCommand(data, _clientInfo);

                        if (data == "/exit")
                            break;

                        continue;
                    }

                    //рассылка сообщения всем клиентам кроме отправителя
                    MessageDispatcher.Broadcast(formattedData, _clients, _clientInfo);

                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}| [{nickName}] {data}");
                    //}
                    Thread.Sleep(50);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss}| Соединение разорвано: [{_clientInfo.NickName}] ({_clientInfo.IPAdress})");
            }
            finally
            {
                //удаление клиента из списка
                lock (_clients)
                {
                    _clients.Remove(_clientInfo);
                }
                BroadcastUserList();
                if (!string.IsNullOrEmpty(_clientInfo.NickName))
                {
                    MessageDispatcher.Broadcast($"{_clientInfo.NickName} left", _clients, _clientInfo);
                }

                _clientInfo.Client?.Close();
            }

        }

        private void ProcessCommand(string message, ClientInfo client)
        {
            string[] splittedMessage = message.Split(' ', 3);
            string command = splittedMessage[0];

            switch (command)
            {
                case "/users":
                    HandleUsersCommand(client);
                    break;

                case "/pm":
                    HandlePrivateMessage(splittedMessage, client);
                    break;

                case "/exit":
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}| Клиент отключился: {_clientInfo.NickName} ({_clientInfo.IPAdress}) ");
                    break;

                case "/help":
                    HandleHelpCommand(client);
                    break;

                default:
                    SendSystemMessage($"Неизвестная команда: {command}. Введите /help для списка команд.", client);
                    break;

            }
        }
        private void SendSystemMessage(string message, ClientInfo client)
        {
            lock (client.StreamLock)
            {
                try
                {
                    NetworkStream stream = client.Client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes($"{message}\n");

                    lock (_clients)
                    {
                        stream.Write(data, 0, data.Length);
                        stream.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось отправить системное сообщение {client.NickName}: {ex.Message}");
                }
            }
        }
        private void HandleUsersCommand(ClientInfo client)
        {
            lock (_clients)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Users connected");

                foreach (ClientInfo clientInfo in _clients)
                {
                    string indicator = (clientInfo == client) ? " (You)" : "";
                    sb.AppendLine($"•{clientInfo.NickName} {indicator}");
                }

                SendSystemMessage(sb.ToString(), client);
            }
        }
        private void HandleHelpCommand(ClientInfo client)
        {
            string data = "=== Доступные команды ===\n/users - список пользователей онлайн\n/pm [ник] [сообщение] - личное сообщение\n/exit - выход из чата" +
                "\n/help - эта справка\n=================";
            SendSystemMessage(data, client);
        }
        private void HandlePrivateMessage(string[] messageParts, ClientInfo sender)
        {
            if (messageParts.Length < 3)
            {
                SendSystemMessage("Неверный формат команды. Используйте: /pm [ник] [сообщение]", sender);
                return;
            }

            string recieverNickname = messageParts[1];
            string privateMessage = messageParts[2];

            ClientInfo receiver = null;

            lock (_clients)
            {
                receiver = _clients.FirstOrDefault(c => c.NickName.Equals(recieverNickname, StringComparison.OrdinalIgnoreCase));
            }

            if (receiver == null)
            {
                SendSystemMessage("Пользователь не найден или не в сети.", sender);
            }
            if (receiver == sender)
            {
                SendSystemMessage("Нельзя отправить сообщение самому себе.", sender);
                return;
            }

            SendSystemMessage($"ЛС от {sender.NickName}: {privateMessage}", receiver);
            //SendSystemMessage($"ЛС для {recieverNickname}", sender);
            Console.WriteLine($"[ЛС] {sender.NickName} -> {receiver.NickName}: {privateMessage}");
        }
        private void BroadcastUserList()
        {
            List<ClientInfo> clientsCopy;
            lock (_clients)
            {
                clientsCopy = [.. _clients];
            }
            try
            {
                string userList;
                userList = string.Join(",", clientsCopy.Select(c => c.NickName));
                
                string message = $"/ULIST {userList}\n";
                byte[] data = Encoding.UTF8.GetBytes(message);

                lock (clientsCopy)
                {
                    foreach (ClientInfo client in clientsCopy)
                    {
                        if (client.Client.Connected)
                        {
                            lock (client.StreamLock)
                            {
                                NetworkStream stream = client.Client.GetStream();
                                stream.Write(data, 0, data.Length);
                                stream.Flush();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }

        }
    }
}
