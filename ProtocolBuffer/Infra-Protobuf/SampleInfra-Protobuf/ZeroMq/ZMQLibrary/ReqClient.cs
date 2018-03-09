using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Philips.PmsMR.Protobuf.Serialization;
using System.IO;
using NetMQ;

namespace Philips.PmsMR.Coreservices.ZMQ
{
    public abstract class ReqClient : IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reqClientAddress"></param>
        /// <param name="context"></param>
        protected ReqClient (string reqClientAddress, NetMQ.NetMQContext context)
        {
            this.reqAdress = reqClientAddress;
            this.context = context;
        }

        public void Initialize()
        {
            if( reqSocket != null)
            {
                throw new System.InvalidOperationException ( "Double call of the Initiliaze method");
            }
            reqSocket = context.CreateRequestSocket();
            reqSocket.Connect(reqAdress);
        }

        /// <summary>
        /// Send message.
        /// The message should be protobuf serializable.
        /// </summary>
        /// <param name="message"></param>
        public void Send ( object serializableObject)
        { 
            if( serializableObject == null)
            {
                throw new System.ArgumentNullException("The message to be sent is null");
            }

            var serializer = CreateProtobufSerializer();
            var message = new NetMQ.NetMQMessage(); 

            using ( var stream = new MemoryStream() )
            {
                serializer.Serialize(serializableObject, stream);
                message.Append(stream.ToArray());
            }
            reqSocket.SendMessage(message);

            var responses = new List<object>();
            bool more = true;
            byte[] bytes;

            while ( more)
            {
                reqSocket.TryReceiveFrameBytes(new TimeSpan(0,0,5), out bytes, out more );
                if (bytes != null)
                {
                    using (var stream = new MemoryStream(bytes))
                    {
                        var obj = serializer.Deserialize(stream);
                        if (obj == null)
                        {
                            throw new System.InvalidOperationException("Failed to deserialize the received message");
                        }
                        responses.Add(obj);
                    }
                }
                if(responses.Count > 0)
                {
                    HandleResponse(responses);
                }
            }
        }

        /// <summary>
        /// Return the serializer that should be used by the implementation
        /// </summary>
        /// <returns></returns>
        protected abstract ISerializer CreateProtobufSerializer();

        /// <summary>
        /// The response handler to be implemented by the Request client
        /// </summary>
        /// <param name="responses"></param>
        protected abstract void HandleResponse(IList<object> responses);
                
        #region Disposable implementation
        /// <summary>
        /// Implementaion of the Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~ReqClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposal of resources.
        /// </summary>
        /// <param name="disposing">Is set when explicitly called</param>
        protected virtual void Dispose ( bool disposing)
        {
            if( !disposed)
            {
                if (disposing) 
                {
                    if (reqSocket != null)
                    {
                        reqSocket.Dispose();
                    }
                    disposed = true;
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false, "Forgot to dispose object of type ZeroMQServer");
                }
            }
        }

        /// <summary>
        /// Flag to indicate disposed state of the object
        /// </summary>
        protected bool disposed = false;
        #endregion

        /// <summary>
        /// The address for the client to connect to.
        /// </summary>
        private readonly string reqAdress = null;

        /// <summary>
        /// The netMQ context
        /// </summary>
        private NetMQ.NetMQContext context = null;

        private NetMQ.Sockets.RequestSocket reqSocket = null;
    }
}
