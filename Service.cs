using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace temp
{
    public class Service
    {
        private string _ip=string.Empty;
        private int _port;
        private Socket _socket;
        private byte[] buffer = new byte[1024 * 1024 * 2];
        public Service(string ip,int port)
        {
            _ip=ip;
            _port=port;
        }

        public void StartListen()
        {
            _socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            var adress=IPAddress.Parse(_ip);
            IPEndPoint endPoint=new IPEndPoint(adress,_port);
            _socket.Bind(endPoint);
            _socket.Listen(int.MaxValue);
            System.Console.WriteLine("监听{0}消息成功！！！",_socket.LocalEndPoint.ToString());
            var thread=new Thread(ListenClientConn);
            thread.Start();
        }

        private void ListenClientConn()
        {
            System.Console.WriteLine("循环开始");
            while(true)
            {
                Socket socket=_socket.Accept();
                socket.Send(Encoding.UTF8.GetBytes("服务端发送消息:"));
                Thread thread = new Thread(ReceiveMessage);
                thread.Start(socket);
            }
        }

        private void ReceiveMessage(object obj)
        {
            Socket clientSocket = (Socket)obj;
            while(true)
            {
                try
                {
                    int length=clientSocket.Receive(buffer);
                    System.Console.WriteLine("接收客户端{0},消息{1}", clientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(buffer, 0, length));
            
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }
    }
}