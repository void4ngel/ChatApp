using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    internal class Server
    {
        private bool _isRunning = true;
        private TcpListener _listener;
        private List<ClientInfo> _clients = new List<ClientInfo>();
        private List<Thread> _clientThreads = new List<Thread>();
        private IPAddress _IPaddr;
        private int _port;

        public void Start()
        {
            Console.Write("Введите IP адрес (127.0.0.1 по умолчанию): ");
            _IPaddr = IPAddress.Parse(Console.ReadLine());
            Console.Write("Введите порт: ");
            _port = int.Parse(Console.ReadLine());

            //начинаем слушать на входящих клиентов
            _listener = new TcpListener(_IPaddr, _port);
            _listener.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений... ");

            while (_isRunning)
            {
                try
                {
                    //клиент появился - запоминаем инфу о нем, вносим в список подключенных, получаем его стрим
                    TcpClient client = _listener.AcceptTcpClient();

                    ClientInfo info = new ClientInfo
                    {
                        Client = client,
                        IPAdress = client.Client.RemoteEndPoint.ToString()
                    };

                    Console.WriteLine($"Входящее подключение: {info.IPAdress}");

                    //lock (_clients)
                    //{
                        _clients.Add(info);
                        Console.WriteLine($"{DateTime.Now:HH:mm:ss}| В списке клиентов: {_clients.Count}");
                    //}

                    // создаем обработчик для каждого подключившегося клиента в отдельном потоке
                    ClientHandler handler = new ClientHandler(info, _clients);
                    Thread clientThread = new Thread(handler.Handle);

                    _clientThreads.Add(clientThread);
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}| Поток запущен. ID: {clientThread.ManagedThreadId}");
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}| Возврат к ожиданию...\n");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Ошибка при принятии подключения: {ex.Message}");
                }
                
            }
        }
        public void Stop()
        {

            _isRunning = false;
            _listener.Stop(); // останавливаем приём новых подключений

            // ждём завершения всех потоков
            foreach (Thread thread in _clientThreads)
            {
                thread.Join(); // Ждём, пока поток завершится
            }
        }

    }

}

