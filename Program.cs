using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace temp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Task.Factory.StartNew(()=>{
            //    var client=new Client("127.0.0.1",5674);
            //    client.StartClient();
            //    Console.ReadKey();
            //});
            UDPClient client=new UDPClient("127.0.0.1", 5674);
            client.SendData();
            Console.Read();
        }
    }
}
