using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Client
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private NetworkManager _manager = new NetworkManager();
        private IPAddress _IPaddr;
        private int _port;

        public void Start()
        {
            _client = new TcpClient();
            Console.Write("Введите имя пользователя: ");
            string msg = Console.ReadLine();
            Console.Write("Введите IP адрес: ");
            _IPaddr = IPAddress.Parse(Console.ReadLine());
            Console.Write("Введите порт: ");
            _port = int.Parse(Console.ReadLine());

            //подключаемся к серверу
            try
            {
                _client.Connect(_IPaddr, _port);
                Console.WriteLine("Подключение установлено. Помощь - /help\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            //создаем поток, вводим и отправляем ник на сервер
            _stream = _client.GetStream();
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
            _stream.Write(msgBytes, 0, msgBytes.Length);

            //поток получения сообщений
            Thread networkThread = new Thread(() =>
            {
                _manager.StartReceiving(_stream);
            });
            networkThread.IsBackground = true; 
            networkThread.Start();

            // цикл отправки сообщений
            while (true)
            {
                Console.Write("Введите сообщение: ");
                msg = Console.ReadLine();

                if (msg == "/exit")
                {
                    byte[] exitMsg = Encoding.UTF8.GetBytes("/exit\n");
                    _stream.Write(exitMsg, 0, exitMsg.Length);
                    break;
                }

                //проверка строки
                if(String.IsNullOrWhiteSpace(msg))
                {
                    Console.WriteLine("Сообщение не может быть пустым");
                    continue;
                }

                msgBytes = Encoding.UTF8.GetBytes(msg + "\n");
                _stream.Write(msgBytes, 0, msgBytes.Length);
                Console.WriteLine("\n");
            }

            //закрытие соединения
            _manager.StopReceiving();
            _stream.Close();
            _client.Close();

            Console.WriteLine("You left the chat.");

            Console.ReadKey();
        }
    }
}
