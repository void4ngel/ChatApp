using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class NetworkManager
    {
        private Thread _receiveThread;
        private bool _isRunning;
        private NetworkStream _stream;
        public void StartReceiving(NetworkStream stream)
        {
            _stream = stream;
            _isRunning = true;

            _receiveThread = new Thread(RecieveLoop);
            _receiveThread.IsBackground = true;
            _receiveThread.Start();
        }

        private void RecieveLoop()
        {
            byte[] receiveBuffer = new byte[1024];

            try
            {
                while (_isRunning && _stream != null)
                {
                    //Console.WriteLine(_stream.DataAvailable);
                    if (_stream.DataAvailable)
                    {
                        int? bytesReceived = _stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                        if (bytesReceived > 0)
                        {
                            string data = Encoding.UTF8.GetString(receiveBuffer, 0, (int)bytesReceived).Trim();

                            if (string.IsNullOrEmpty(data))
                            {
                                continue;
                            }
                            if (data.StartsWith("/ULIST"))
                            {
                                continue;
                            }
                            Console.WriteLine(data);

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
                if (_isRunning)
                {
                    //UIHelper.PrintMessage($"[Ошибка сети]: {ex.Message}");
                }
            }
        }

        public void StopReceiving()
        {
            _isRunning = false;
            _receiveThread?.Join(1000);
        }
    }
}
