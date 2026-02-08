using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    internal class ClientInfo
    {
        public TcpClient Client {  get; set; }
        public string IPAdress { get; set; }
        public string NickName {  get; set; }
        public object StreamLock { get; } = new object();
    }
}
