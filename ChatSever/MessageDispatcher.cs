using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    internal static class MessageDispatcher
    {
        public static void Broadcast(string message, List<ClientInfo> clients, ClientInfo sender = null)
        {
            //проверяем входные данные
            if (string.IsNullOrEmpty(message) || clients == null)
                return;

            byte[] data = Encoding.UTF8.GetBytes(message + "\n");

            // копирование списка чтобы избежать исключения при изменении во время итерации
            List<ClientInfo> clientsCopy;
            lock (clients)
            {
                clientsCopy = [.. clients];
            }

            foreach (ClientInfo client in clientsCopy)
            {
                try
                {
                    if (sender != null && client == sender)
                        continue;
                    if (client.Client.Connected)
                    {
                        NetworkStream stream = client.Client.GetStream();
                        stream.Write(data, 0, data.Length);
                        stream.Flush();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Не удалось отправить сообщение клиенту {client.NickName}: {e.Message}");
                }

            }
        }


    }
}
