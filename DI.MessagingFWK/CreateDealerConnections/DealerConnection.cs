using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Philips.DI.Interfaces.Services.Messaging;
using Philips.DI.Interfaces.Services.Messaging.Model;
using Philips.DI.Services.Messaging;

namespace CreateMDBroker
{
    class DealerConnection
    {
        static void Main(string[] args)
        {
            IMessagingService messagingService = new MessagingService();

            //for (var index = 0; index < 1000; index++)
            {
                var dealer = messagingService.Proxy(new ServiceEndPoint()
                {
                    Name = "TestMDBrokerPortBehavior",
                    Port = 4999,
                    Topology = Topology.DEALER,
                    EndPointType = EndPointType.Connect,
                    Serializer = Philips.DI.Interfaces.Services.Serialization.SerializeType.Binary,
                    Transport = TransportType.TCP
                }).ConvertTo<IDealer>();
                Console.WriteLine("Starting the Dealer...");
                dealer.Start();
                Console.WriteLine("Started the Dealer...");
                var msgTask = dealer.SendSync<MdBrokerRequestMessage, MdBrokerResponseMessage>(
                    new MdBrokerRequestMessage() { MdBrokerInputString = "TestString" });
                Console.WriteLine(msgTask.MdBrokerOutputString);
                Console.WriteLine("Enter to Stop and Dispose....");
                Console.ReadKey();
                dealer.Stop();
                dealer.Dispose();
                //Console.WriteLine("Disposing {0}", index);
                //System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine("Disposed....");
            Console.ReadKey();
            Console.WriteLine("Ending...");

        }
    }
}
