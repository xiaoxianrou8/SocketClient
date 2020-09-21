using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace temp
{
    public class UDPClient
    {
        private readonly string _ip;
        private readonly int _port;

        public UDPClient(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public void SendData()
        {
            IPEndPoint endPoint=new IPEndPoint(IPAddress.Parse(_ip),_port);
            UdpClient client=new UdpClient();
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
                var bytes = Encoding.UTF8.GetBytes(i.ToString());
                client.Send(bytes,bytes.Length,endPoint);
            }
        }
}
}
