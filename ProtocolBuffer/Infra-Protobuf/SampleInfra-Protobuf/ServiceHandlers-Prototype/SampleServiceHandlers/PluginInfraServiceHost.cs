using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Philips.PmsMR.Coreservices.ZMQ;
using NetMQ;
using Philips.PmsMR.Protobuf.Serialization;
using Philips.PmsMR.Protobuf.DataModel;
using Philips.PmsMR.Protobuf.ModelReflection;
using PluginInfraServices;
using System.Threading;

namespace Philips.PmsMR.Coreservices.PostProcInfraService
{
    public class PluginInfraServiceHost: RepServer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PluginInfraServiceHost(string serverAddress, NetMQ.NetMQContext context) : base(serverAddress, context)
        {
            serializer = new Serializer(versionMapper.DefaultModeler);
        }

        /// <summary>
        /// Provide the serializer to be used by the Reply Server implementation.
        /// </summary>
        /// <returns></returns>
        protected override Philips.PmsMR.Protobuf.Serialization.ISerializer CreateProtobufSerializer()
        {
            return serializer;
        }

        /// <summary>
        /// The handler for the received messages
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="serializer"></param>
        /// <param name="requests"></param>
        protected override void HandleRequest(NetMQ.NetMQSocket socket, Philips.PmsMR.Protobuf.Serialization.ISerializer serializer, IList<object> requests)
        {
            foreach ( var request in requests)
            {
                var requestInstance = request as PluginInfraServices.QueryVersionRequestMessage;
                if( requestInstance != null)
                {
                    // ToDo - Perform the task;
                    Console.WriteLine("The current thread ID is - {0}", Thread.CurrentThread.ManagedThreadId);
                    // ToDo - Generate the reply
                    PluginInfraServices.QueryVersionResponseMessage response = new PluginInfraServices.QueryVersionResponseMessage()
                    {
                        ProductModel = "Ingenia",
                        RequestToken = new Guid(),
                        SystemType = "WA15",
                        Version = "R5.1.1"
                    };

                    var replyMessage = FormatReply(new object[] { response }, serializer);
                    socket.SendMultipartMessage(replyMessage);

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
