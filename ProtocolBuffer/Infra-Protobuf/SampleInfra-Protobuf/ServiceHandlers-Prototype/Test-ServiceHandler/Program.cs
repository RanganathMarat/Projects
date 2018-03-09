using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInfraServices;

namespace Test_ServiceHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = NetMQ.NetMQContext.Create();
            TestClient client = new TestClient("tcp://127.0.0.1:5656", context);
            client.Initialize();
            client.Send((object)new PluginInfraServices.QueryVersionRequestMessage());
            Console.ReadLine();
            Console.WriteLine("Start disposing");
            client.Dispose();
            context.Dispose();
        }
    }
}
