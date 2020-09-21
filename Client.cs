using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketMsgProto;
using SocketService;

namespace temp
{
    public class Client
    {
        private string _ip = string.Empty;
        private int _port = 0;
        private Socket _socket = null;
        private byte[] buffer = new byte[1024 * 1024 * 2];

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">连接服务器的IP</param>
        /// <param name="port">连接服务器的端口</param>
        public Client(string ip, int port)
        {
            this._ip = ip;
            this._port = port;
        }

        public void StartClient()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                var address = IPAddress.Parse(_ip);
                var endPoint = new IPEndPoint(address, _port);
                _socket.Connect(endPoint);
                System.Console.WriteLine("C===服务器连接成功");
                for (int i = 0; i < 1900; i++)
                {
                    Thread.Sleep(100);
                    var sendMessage = $"<>客户端发送消息时间{DateTime.Now.ToLongTimeString()}";
                    var datas = BuildTcpProto.BuildSendBuffer(new MessageHeader() { StartSign = (short)0xFF }, $"{i}");
                    _socket.Send(datas);
                    System.Console.WriteLine("向服务器发送消息：{0}", sendMessage);
                }
            }
            catch (System.Exception ex)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("发送消息结束");
            Console.ReadKey();
        }
    }
}