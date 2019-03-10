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
   public  class Program
    {
        static void Main(string[] args)
        {
            IMessagingService messagingService = new MessagingService();
            var mdBroker = messagingService.Proxy(new ServiceEndPoint()
            {
                Name = "TestMDBrokerPortBehavior",
                EndPointType = EndPointType.Bind,
                Port = 4999,
                Serializer = Philips.DI.Interfaces.Services.Serialization.SerializeType.Binary,
                Topology = Topology.BROKER,
                Transport = TransportType.TCP
            }).ConvertTo<IBroker>();
            mdBroker.BrokerCallback = new MdBrokerCallBack();
            Console.WriteLine("Starting the MDBroker Service....");
            mdBroker.Start();
            Console.ReadLine();
            Console.WriteLine("Closing the Service.");
            mdBroker.Stop();
            mdBroker.Dispose();
        }
    }

    /// <summary>
    /// MDBrokerCallBack
    /// </summary>
    public class MdBrokerCallBack : IBrokerCallback
    {
        private readonly CustomMdBrokerCallBackService _mdBrokerCallBackService = new CustomMdBrokerCallBackService();

        /// <summary>
        /// GetService
        /// </summary>
        /// <returns></returns>
        public BaseBrokerCallbackService GetService()
        {
            return _mdBrokerCallBackService;
        }
    }

    /// <summary>
    /// Call back service for MDBroker
    /// </summary>
    public class CustomMdBrokerCallBackService : BaseBrokerCallbackService
    {
        private MdBrokerResponseMessage mdBrokerHandler(MdBrokerRequestMessage arg)
        {
            string requestMessage = arg.MdBrokerInputString;
            var responseMessage = int.TryParse(requestMessage, out var input) ? (20 * input).ToString() : requestMessage;
            return new MdBrokerResponseMessage() { MdBrokerOutputString = responseMessage };
        }

        public override void InitializeWorker(IWorker worker)
        {
            worker.RegisterHandler<MdBrokerRequestMessage, MdBrokerResponseMessage>(mdBrokerHandler);
        }
    }
}
