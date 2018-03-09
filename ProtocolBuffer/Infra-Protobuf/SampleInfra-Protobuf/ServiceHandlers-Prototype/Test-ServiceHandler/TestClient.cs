using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Philips.PmsMR.Coreservices.ZMQ;
using Philips.PmsMR.Protobuf.Serialization;
using Philips.PmsMR.Protobuf.DataModel;
using Philips.PmsMR.Protobuf.ModelReflection;
using System.Threading;
namespace Test_ServiceHandler
{
    class TestClient : ReqClient
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="context"></param>
        public TestClient(string address, NetMQ.NetMQContext context)
            : base(address, context)
        {
            serializer = new Serializer(versionMapper.DefaultModeler);
        }

        /// <summary>
        /// Implemented Protobuf serializer
        /// </summary>
        /// <returns></returns>
        protected override Philips.PmsMR.Protobuf.Serialization.ISerializer CreateProtobufSerializer()
        {
            return serializer;
        }

        /// <summary>
        /// Implemented HandleResponse
        /// </summary>
        /// <param name="responses"></param>
        protected override void HandleResponse(IList<object> responses)
        {
            foreach( var response in responses)
            {
                var replyMessage = response as PluginInfraServices.QueryVersionResponseMessage;
                if( replyMessage != null)
                {
                    Console.WriteLine("The current thread ID is - {0} ", Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("Received "+ replyMessage.ProductModel);
                }
            }
        }

        /// <summary>
        /// The typemapper object
        /// </summary>
        private static readonly TypeMapper versionMapper = new TypeMapper(new PluginInfraServices.PluginInfraModel());

        /// <summary>
        /// Serializer used by the instance of the reply server.
        /// </summary>
        Philips.PmsMR.Protobuf.Serialization.ISerializer serializer = null;

    }
}
