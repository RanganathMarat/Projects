using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using Philips.PmsMR.Protobuf.Serialization;
using System.IO;
using System.Threading;

namespace Philips.PmsMR.Coreservices.ZMQ
{
    public abstract class RepServer : ZeroMQServer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serverAddress"></param>
        protected RepServer(string serverAddress, NetMQ.NetMQContext context)
        {
            this.replyServerAddress = serverAddress;
            this.context = context;
        }

        /// <summary>
        /// Initialize the server.
        /// </summary>
        public override void Initialize()
        {
            if( replySocket!=null)
            {
                throw new System.InvalidOperationException("Double call of Initialize");
            }
            // Create the response socket.
            replySocket = context.CreateResponseSocket();
            // Bind the response socket to the server address.
            replySocket.Bind(replyServerAddress);

            serializer = CreateProtobufSerializer();
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public override void Start(CancellationToken cancellationToken )
        {
            CheckDisposed();
            netMQPoller = new Poller();
            netMQPoller.AddSocket(replySocket);

            replySocket.ReceiveReady += OnNetMqSocketOnReceiveReady;

            cancellationToken.Register(() => {
                Console.WriteLine("A cancellation request is received!!!!");
                netMQPoller.Cancel();
            });

            netMQPoller.PollTillCancelled();
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public override void Stop()
        {
            CheckDisposed();
            if (netMQPoller.IsStarted)
            {
                netMQPoller.CancelAndJoin();
            }
            replySocket.ReceiveReady -= OnNetMqSocketOnReceiveReady;
        }

                /// <summary>
        ///     Called when [net mq socket on receive ready].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NetMQSocketEventArgs" /> instance containing the event data.</param>
        private void OnNetMqSocketOnReceiveReady(object sender, NetMQSocketEventArgs args)
        {
            CheckDisposed();
            NetMQSocket socket = args.Socket;
            bool more;
            var list = new List<object>();

            do
            {
                using (var stream = new MemoryStream(socket.ReceiveFrameBytes(out more)))
                {
                    var dataObj = serializer.Deserialize(stream);
                    list.Add(dataObj);
                }

            } while (more);
            HandleRequest(socket, serializer, list);

        }

        /// <summary>
        /// Formats the reply message using the supplied serializer.
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        protected NetMQMessage FormatReply( object[] reply, ISerializer serializer)
        {
            NetMQMessage message = null;
            if( reply != null && reply.Length !=0 )
            {
                message = new NetMQMessage();
                foreach (var replyPart in reply)
                {
                    using (var stream = new MemoryStream())
                    {
                        serializer.Serialize(replyPart, stream);
                        message.Append(stream.ToArray());
                    }
                }
            }
                return message;
        }

        /// <summary>
        /// The serializer used by the implementation of the reply server
        /// </summary>
        /// <returns>The serializer to be used for received bytes</returns>
        protected abstract ISerializer CreateProtobufSerializer();

        /// <summary>
        /// The request handler to be implemented by the server implementation.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="serializer"></param>
        /// <param name="requests"></param>
        protected abstract void HandleRequest(NetMQSocket socket, ISerializer serializer, IList<object> requests);

        #region IDisposable
        protected override void Dispose(bool disposing)
        {
            if( !disposed)
            {
                if (disposing) 
                { 
                    if( netMQPoller != null)
                    {

                        netMQPoller.Dispose();
                    }

                    if( replySocket != null)
                    {
                        replySocket.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
        #endregion


        /// <summary>
        /// Reply socket
        /// </summary>
        private NetMQ.Sockets.ResponseSocket replySocket;

        /// <summary>
        /// Address of the reply server. Format - tcp://127.0.01:12345
        /// </summary>
        private string replyServerAddress;

        /// <summary>
        /// The NetMQ context
        /// It is advised to have only netmq context per process.
        /// </summary>
        private NetMQ.NetMQContext context;

        /// <summary>
        /// The poller object for the socket.
        /// </summary>
        private NetMQ.Poller netMQPoller;

        /// <summary>
        /// The serializer object.
        /// </summary>
        private ISerializer serializer;
    
    }
}
