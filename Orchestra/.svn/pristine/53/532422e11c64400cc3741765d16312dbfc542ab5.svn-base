using System;
using System.Collections.Generic;
using System.IO;
using Common.Messages;
using RabbitMQ.Client;
using System.Threading;
using ZeroMQ;
using System.Text;

namespace Common.Messaging
{
    /// <summary>
    /// Messaging
    /// </summary>
    public static class Messaging
    {
        private static readonly MessagingConfigurationSection Configuration;

        /// <summary>
        /// Initializes the <see cref="Messaging"/> class.
        /// </summary>
        static Messaging()
        {
            if (Configuration != null) return;

            Configuration = new MessagingConfigurationSection();
            Configuration.Initialize();
        }

        /// <summary>
        /// Proxy to the specified end point.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        /// <returns></returns>
        public static IMessaging Proxy(string endPoint)
        {
            if(!Configuration.Endpoints.ContainsKey(endPoint)) throw  new InvalidOperationException("Endpoint not found.");

            var endpoint =Configuration.Endpoints[endPoint];
            switch (endpoint.MQType)
            {
                case MQType.RabbitMQ:
                    switch (endpoint.Topology)
                    {
                        case Topology.Basic:
                            if (string.IsNullOrEmpty(endpoint.Queue)) throw new InvalidOperationException("No Queue specified.");
                            return BasicRMQ(endpoint.Queue);
                        case Topology.PubSub:
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            return PubSub(endpoint.Queue, endpoint.Exchange);
                        case Topology.Routing:
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            if (string.IsNullOrEmpty(endpoint.Bindig)) throw new InvalidOperationException("No Bindig specified.");
                            return Routing(endpoint.Queue, endpoint.Exchange, endpoint.Bindig);
                        case Topology.Topic:
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            if (string.IsNullOrEmpty(endpoint.Bindig)) throw new InvalidOperationException("No Bindig specified.");
                            return Topics(endpoint.Queue, endpoint.Exchange, endpoint.Bindig);
                        case Topology.RPC:
                            if (string.IsNullOrEmpty(endpoint.Queue)) throw new InvalidOperationException("No Queue specified.");
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            return RPC(endpoint.Queue, endpoint.Exchange);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case MQType.ZeroMQ:
                    switch (endpoint.Topology)
                    {
                        case Topology.Basic:
                            if (string.IsNullOrEmpty(endpoint.Bindig)) throw new InvalidOperationException("No Bindig specified.");
                            return BasicZMQ(endpoint.Bindig);
                        case Topology.PubSub:
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            return PubSub(endpoint.Queue, endpoint.Exchange);
                        case Topology.Routing:
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            if (string.IsNullOrEmpty(endpoint.Bindig)) throw new InvalidOperationException("No Bindig specified.");
                            return Routing(endpoint.Queue, endpoint.Exchange, endpoint.Bindig);
                        case Topology.Topic:
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            if (string.IsNullOrEmpty(endpoint.Bindig)) throw new InvalidOperationException("No Bindig specified.");
                            return Topics(endpoint.Queue, endpoint.Exchange, endpoint.Bindig);
                        case Topology.RPC:
                            if (string.IsNullOrEmpty(endpoint.Queue)) throw new InvalidOperationException("No Queue specified.");
                            if (string.IsNullOrEmpty(endpoint.Exchange)) throw new InvalidOperationException("No Exchange specified.");
                            return RPC(endpoint.Queue, endpoint.Exchange);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Basics the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <returns></returns>
        private static IMessaging BasicRMQ(string queue)
        {
            return new BasicRabbit(queue);
        }

        /// <summary>
        /// Basics the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <returns></returns>
        private static IMessaging BasicZMQ(string queue)
        {
            return new BasicZero(queue);
        }

        /// <summary>
        /// Pubs the sub.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <returns></returns>
        private static IMessaging PubSub(string queue, string exchange)
        {
            return new PubSubRabbit(queue, exchange);
        }

        /// <summary>
        /// Routings the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="keyWords">The key words.</param>
        /// <returns></returns>
        private static IMessaging Routing(string queue, string exchange, string keyWords)
        {
            return new RoutingRabbit(queue, exchange, keyWords);
        }

        /// <summary>
        /// Topicses the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="topicDescriptor">The topic descriptor.</param>
        /// <returns></returns>
        private static IMessaging Topics(string queue, string exchange, string topicDescriptor)
        {
            return new TopicsRabbit(queue, exchange, topicDescriptor);
        }

        /// <summary>
        /// RPCs the specified server queue.
        /// </summary>
        /// <param name="serverQueue">The server queue.</param>
        /// <param name="responceQueue">The responce queue.</param>
        /// <returns></returns>
        private static IMessaging RPC(string serverQueue, string responceQueue)
        {
            return new RPCRabbit(serverQueue, responceQueue);
        }

        /// <summary>
        /// Basic
        /// </summary>
        internal class BasicZero : ZeroMQBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BasicZero"/> class.
            /// </summary>
            /// <param name="binding">The binding.</param>
            public BasicZero(string binding)
                : base(Topology.Basic, binding)
            {
                
            }
        }

        /// <summary>
        /// Basic
        /// </summary>
        internal class BasicRabbit : RabbitMQBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BasicRabbit"/> class.
            /// </summary>
            /// <param name="queue">The queue.</param>
            public BasicRabbit(string queue)
                : base(queue, string.Empty, string.Empty)
            {
                ExchangeType = string.Empty;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BasicRabbit"/> class.
            /// </summary>
            /// <param name="queue">The queue.</param>
            /// <param name="exchange">The exchange.</param>
            /// <param name="bindings">The bindings.</param>
            protected BasicRabbit(string queue, string exchange, string bindings)
                : base(queue, exchange, bindings)
            {
            }
        }

        /// <summary>
        /// PubSub
        /// </summary>
        internal class PubSubRabbit : BasicRabbit
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PubSubRabbit"/> class.
            /// </summary>
            /// <param name="queue">The queue.</param>
            /// <param name="exchange">The exchange.</param>
            public PubSubRabbit(string queue, string exchange)
                : base(queue, exchange, string.Empty)
            {
                ExchangeType = RabbitMQ.Client.ExchangeType.Fanout;
            }
        }

        /// <summary>
        /// Routing
        /// </summary>
        internal class RoutingRabbit : BasicRabbit
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RoutingRabbit"/> class.
            /// </summary>
            /// <param name="queue">The queue.</param>
            /// <param name="exchange">The exchange.</param>
            /// <param name="keyWords">The comma separetd keywords.</param>
            public RoutingRabbit(string queue, string exchange, string keyWords)
                : base(queue, exchange, keyWords)
            {
                ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
            }
        }

        /// <summary>
        /// Topics
        /// </summary>
        internal class TopicsRabbit : BasicRabbit
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TopicsRabbit"/> class.
            /// </summary>
            /// <param name="queue">The queue.</param>
            /// <param name="exchange">The exchange.</param>
            /// <param name="topicDescriptor">The dot separeted topic descriptor.</param>
            public TopicsRabbit(string queue, string exchange, string topicDescriptor)
                : base(queue, exchange, topicDescriptor)
            {
                ExchangeType = RabbitMQ.Client.ExchangeType.Topic;
            }
        }

        /// <summary>
        /// RPC
        /// </summary>
        internal class RPCRabbit : BasicRabbit
        {
            /// <summary>
            /// Gets or sets the responce queue.
            /// </summary>
            /// <value>
            /// The responce queue.
            /// </value>
            public string ResponceQueue { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="RPCRabbit" /> class.
            /// </summary>
            /// <param name="serverQueue">The queue.</param>
            /// <param name="responceQueue">The responce queue.</param>
            public RPCRabbit(string serverQueue, string responceQueue)
                : base(serverQueue)
            {
                ResponceQueue = responceQueue;
            }
        }

        [Flags]
        internal enum InstanceType
        {
            Consumer,
            Publisher
        }

        /// <summary>
        /// EventMaper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal class EventMaper<T> where T : MessageBase
        {
            /// <summary>
            /// The handler
            /// </summary>
            private readonly Action<T> _handler;

            /// <summary>
            /// Initializes a new instance of the <see cref="EventMaper{T}"/> class.
            /// </summary>
            /// <param name="handler">The handler.</param>
            public EventMaper(Action<T> handler)
            {
                _handler = handler;
            }

            /// <summary>
            /// Gets the key.
            /// </summary>
            /// <value>
            /// The key.
            /// </value>
            public string Key { get { return typeof(T).ToString(); } }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public Action<MessageBase> Value { get { return Invoke; } }

            /// <summary>
            /// Invokes the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            private void Invoke(MessageBase message)
            {
                _handler.Invoke((T)message);
            }
        }

        /// <summary>
        /// MessageBase
        /// </summary>
        internal class ZeroMQBase : IConsume, IPublish, IMessaging
        {
            /// <summary>
            /// The message handlers
            /// </summary>
            private readonly Dictionary<string, Action<MessageBase>> _handlers;
            /// <summary>
            /// The _connection
            /// </summary>
            private static ZmqContext _zmqContext;
            /// <summary>
            /// The channel
            /// </summary>
            private ZmqSocket _zmqSocket;
            /// <summary>
            /// The _type
            /// </summary>
            private InstanceType _type;
            /// <summary>
            /// The _continue dequeue
            /// </summary>
            private long _continueDequeue;
            /// <summary>
            /// Gets or sets the topology.
            /// </summary>
            /// <value>
            /// The topology.
            /// </value>
            private Topology Topology { get; set; }
            /// <summary>
            /// Gets or sets the type of the exchange.
            /// </summary>
            /// <value>
            /// The type of the exchange.
            /// </value>
            protected SocketType SocketType { get; set; }
            /// <summary>
            /// Gets or sets the binding.
            /// </summary>
            /// <value>
            /// The binding.
            /// </value>
            protected string Bindings { get; set; }
            /// <summary>
            /// Gets the Consumer.
            /// </summary>
            /// <value>
            /// The consume.
            /// </value>
            public IConsume Consumer
            {
                get
                {
                    _type = InstanceType.Consumer;

                    switch (Topology)
                    {
                        case Topology.Basic:
                            _zmqSocket = _zmqContext.CreateSocket(SocketType.PULL);
                            break;
                        //case Topology.PubSub:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Sub);
                        //    break;
                        //case Topology.Routing:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Req);
                        //    break;
                        //case Topology.Topic:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Req);
                        //    break;
                        //case Topology.RPC:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Req);
                        //    break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    _zmqSocket.Connect(Bindings);

                    return this;
                }
            }

            /// <summary>
            /// Gets the Publisher.
            /// </summary>
            /// <value>
            /// The publish.
            /// </value>
            public IPublish Publisher
            {
                get
                {
                    _type = InstanceType.Publisher;

                    switch (Topology)
                    {
                        case Topology.Basic:
                            _zmqSocket = _zmqContext.CreateSocket(SocketType.PUSH);
                            break;
                        //case Topology.PubSub:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Pub);
                        //    break;
                        //case Topology.Routing:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Rep);
                        //    break;
                        //case Topology.Topic:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Rep);
                        //    break;
                        //case Topology.RPC:
                        //    _zmqSocket = _zmqContext.CreateSocket(ZmqSocketType.Rep);
                        //    break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    _zmqSocket.Bind(Bindings);

                    return this;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MessageBase" /> class.
            /// </summary>
            /// <param name="topology">The topology.</param>
            /// <param name="bindings">The bindings.</param>
            protected ZeroMQBase(Topology topology, string bindings)
            {
                Bindings = bindings;
                Topology = topology;
                _handlers = new Dictionary<string, Action<MessageBase>>();
                if (_zmqContext == null) _zmqContext = ZmqContext.Create();
            }

            /// <summary>
            /// Publishes the specified Messaging.
            /// </summary>
            /// <param name="message">The Messaging.</param>
            /// <exception cref="System.ArgumentNullException">Messaging;Invalid Messaging...</exception>
            /// <exception cref="System.InvalidOperationException">Is a comsumer instance!!</exception>
            void IPublish.Publish<T>(T message)
            {
                if (Equals(message,default(T))) throw new ArgumentNullException("message", "Invalid Messaging...");
                if (_type == InstanceType.Consumer) throw new InvalidOperationException("Is a comsumer instance!!");

                _zmqSocket.Send(ToByte(message));
            }

            /// <summary>
            /// Object To byte[].
            /// </summary>
            /// <param name="message">The Messaging.</param>
            /// <returns></returns>
            private static byte[] ToByte<TMessage>(TMessage message)
            {
                using (MemoryStream bStream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(bStream, message);
                    return bStream.ToArray();
                }
            }

            /// <summary>
            /// Froms byte[] to Object.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <returns></returns>
            private static MessageBase FromByte(byte[] body)
            {
                using (MemoryStream bStream = new MemoryStream(body))
                {
                    return ProtoBuf.Serializer.Deserialize<MessageBase>(bStream);
                }
            }

            /// <summary>
            /// Dequeue the Messaging.
            /// </summary>
            /// <exception cref="System.ArgumentNullException">handler;Invalid handler...</exception>
            /// <exception cref="System.InvalidOperationException">Is a publisher instance!!</exception>
            void IConsume.DeQueue()
            {
                DeQueue(false);
            }

            /// <summary>
            /// Des the queue.
            /// </summary>
            /// <param name="purge">if set to <c>true</c> [purge].</param>
            /// <exception cref="System.InvalidOperationException">Is a publisher instance!!</exception>
            public void DeQueue(bool purge)
            {
                if (_type == InstanceType.Publisher) throw new InvalidOperationException("Is a publisher instance!!");
                Interlocked.Increment(ref _continueDequeue);


                Action startDequeue = () =>
                {
                    while (Interlocked.Read(ref _continueDequeue) != 0)
                    {
                        try
                        {
                            byte[] data = new byte [0];
                            int size;
                            data = _zmqSocket.Receive(data, out size);
                            HandleMessage(FromByte(data));
                        }
                        catch (EndOfStreamException)
                        {
                            break;
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch (System.Exception)
                        // ReSharper restore EmptyGeneralCatchClause
                        {
                            // Log error.
                        }
                    }
                };

                startDequeue.BeginInvoke(null, null);
            }

            /// <summary>
            /// Registers the handler.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="handler">The handler.</param>
            /// <exception cref="System.ArgumentNullException">handler;Invalid handler...</exception>
            public void RegisterHandler<T>(Action<T> handler) where T : MessageBase
            {
                if (handler == null) throw new ArgumentNullException("handler", "Invalid handler...");

                EventMaper<T> maper = new EventMaper<T>(handler);

                var key = maper.Key;
                if (!_handlers.ContainsKey(key))
                {
                    _handlers.Add(key, maper.Value);
                }
            }

            /// <summary>
            /// Handles the message.
            /// </summary>
            /// <param name="message">The message.</param>
            private void HandleMessage(MessageBase message)
            {
                var key = message.TypeName;
                if (_handlers.ContainsKey(key))
                {
                    _handlers[key].Invoke(message);
                }
            }

            /// <summary>
            /// Stops dequeuing.
            /// </summary>
            void IConsume.Stop()
            {
                Interlocked.Exchange(ref _continueDequeue, 0);
            }

            /// <summary>
            /// Binds the queue to exchange.
            /// </summary>
            /// <param name="exchange">The exchange.</param>
            /// <param name="binding">The binding.</param>
            public void AddBinding(string exchange, string binding)
            {
                // Need to be implemented
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Interlocked.Exchange(ref _continueDequeue, 0);

                if (_zmqSocket == null) return;

                _zmqSocket.Dispose();
                _handlers.Clear();
                _zmqSocket = null;
            }
        }

        /// <summary>
        /// MessageBase
        /// </summary>
        internal class RabbitMQBase : IConsume, IPublish, IMessaging
        {
            /// <summary>
            /// The message handlers
            /// </summary>
            private readonly Dictionary<string, Action<MessageBase>> _handlers;
            /// <summary>
            /// The _connection
            /// </summary>
            private IConnection _connection;
            /// <summary>
            /// The channel
            /// </summary>
            private IModel _channel;
            /// <summary>
            /// The _consumer
            /// </summary>
            private QueueingBasicConsumer _consumer;
            /// <summary>
            /// The _type
            /// </summary>
            private InstanceType _type;
            /// <summary>
            /// The _continue dequeue
            /// </summary>
            private long _continueDequeue;
            /// <summary>
            /// The host name
            /// </summary>
            private const string HostName = "localhost";
            /// <summary>
            /// Gets or sets the queue.
            /// </summary>
            /// <value>
            /// The queue.
            /// </value>
            protected string Queue { get; set; }
            /// <summary>
            /// Gets or sets the exchange.
            /// </summary>
            /// <value>
            /// The exchange.
            /// </value>
            protected string Exchange { get; set; }
            /// <summary>
            /// Gets or sets the type of the exchange.
            /// </summary>
            /// <value>
            /// The type of the exchange.
            /// </value>
            protected string ExchangeType { get; set; }
            /// <summary>
            /// Gets or sets the binding.
            /// </summary>
            /// <value>
            /// The binding.
            /// </value>
            protected string Bindings { get; set; }
            /// <summary>
            /// Gets the Consumer.
            /// </summary>
            /// <value>
            /// The consume.
            /// </value>
            public IConsume Consumer
            {
                get
                {
                    _type = InstanceType.Consumer;

                    var queueName = _channel.QueueDeclare(Queue, true, false, false, null);

                    if (!string.IsNullOrEmpty(Exchange))
                    {
                        _channel.ExchangeDeclare(Exchange, ExchangeType, true, false, null);
                        _channel.QueueBind(queueName, Exchange, Bindings);
                    }

                    _consumer = new QueueingBasicConsumer(_channel);
                    _channel.BasicConsume(queueName, true, _consumer);

                    return this;
                }
            }

            /// <summary>
            /// Gets the Publisher.
            /// </summary>
            /// <value>
            /// The publish.
            /// </value>
            public IPublish Publisher
            {
                get
                {
                    _type = InstanceType.Publisher;

                    if (!string.IsNullOrEmpty(Queue))
                    {
                        _channel.QueueDeclare(Queue, true, false, false, null);
                    }

                    if (!string.IsNullOrEmpty(Exchange))
                    {
                        _channel.ExchangeDeclare(Exchange, ExchangeType, true, false, null);
                    }

                    return this;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MessageBase"/> class.
            /// </summary>
            /// <param name="queue">The queue.</param>
            /// <param name="exchange">The exchange.</param>
            /// <param name="bindings">The bindings.</param>
            protected RabbitMQBase(string queue, string exchange, string bindings)
            {
                Queue = queue;
                Exchange = exchange;
                Bindings = bindings;
                _handlers = new Dictionary<string, Action<MessageBase>>();

                var factory = new ConnectionFactory { HostName = HostName };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }

            /// <summary>
            /// Publishes the specified Messaging.
            /// </summary>
            /// <param name="message">The Messaging.</param>
            /// <exception cref="System.ArgumentNullException">Messaging;Invalid Messaging...</exception>
            /// <exception cref="System.InvalidOperationException">Is a comsumer instance!!</exception>
            void IPublish.Publish<T>(T message)
            {
                if (Equals(message, default(T))) throw new ArgumentNullException("message", "Invalid Messaging...");
                if (_type == InstanceType.Consumer) throw new InvalidOperationException("Is a comsumer instance!!");

                _channel.BasicPublish(Exchange, !string.IsNullOrEmpty(Exchange) ? Bindings : Queue, null, ToByte(message));
            }

            /// <summary>
            /// Object To byte[].
            /// </summary>
            /// <param name="message">The Messaging.</param>
            /// <returns></returns>
            private static byte[] ToByte<TMessage>(TMessage message)
            {
                using (MemoryStream bStream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(bStream, message);
                    return bStream.ToArray();
                }
            }

            /// <summary>
            /// Froms byte[] to Object.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <returns></returns>
            private static MessageBase FromByte(byte[] body)
            {
                using (MemoryStream bStream = new MemoryStream(body))
                {
                    return ProtoBuf.Serializer.Deserialize<MessageBase>(bStream);
                }
            }

            /// <summary>
            /// Dequeue the Messaging.
            /// </summary>
            /// <exception cref="System.ArgumentNullException">handler;Invalid handler...</exception>
            /// <exception cref="System.InvalidOperationException">Is a publisher instance!!</exception>
            void IConsume.DeQueue()
            {
                DeQueue(false);
            }

            /// <summary>
            /// Des the queue.
            /// </summary>
            /// <param name="purge">if set to <c>true</c> [purge].</param>
            /// <exception cref="System.InvalidOperationException">Is a publisher instance!!</exception>
            public void DeQueue(bool purge)
            {
                if (_type == InstanceType.Publisher) throw new InvalidOperationException("Is a publisher instance!!");
                if (purge) _channel.QueuePurge(Queue);
                Interlocked.Increment(ref _continueDequeue);

                Action startDequeue = () =>
                {
                    while (Interlocked.Read(ref _continueDequeue) != 0)
                    {
                        try
                        {
                            var e = _consumer.Queue.Dequeue();
                            HandleMessage(FromByte(e.Body));
                        }
                        catch (EndOfStreamException)
                        {
                            break;
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch (System.Exception)
                        // ReSharper restore EmptyGeneralCatchClause
                        {
                            // Log error.
                        }
                    }
                };

                startDequeue.BeginInvoke(null, null);
            }

            /// <summary>
            /// Registers the handler.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="handler">The handler.</param>
            /// <exception cref="System.ArgumentNullException">handler;Invalid handler...</exception>
            public void RegisterHandler<T>(Action<T> handler) where T : MessageBase
            {
                if (handler == null) throw new ArgumentNullException("handler", "Invalid handler...");

                EventMaper<T> maper = new EventMaper<T>(handler);

                var key = maper.Key;
                if (!_handlers.ContainsKey(key))
                {
                    _handlers.Add(key, maper.Value);
                }
            }

            /// <summary>
            /// Handles the message.
            /// </summary>
            /// <param name="message">The message.</param>
            private void HandleMessage(MessageBase message)
            {
                var key = message.TypeName;
                if (_handlers.ContainsKey(key))
                {
                    _handlers[key].Invoke(message);
                }
            }

            /// <summary>
            /// Stops dequeuing.
            /// </summary>
            void IConsume.Stop()
            {
                Interlocked.Exchange(ref _continueDequeue, 0);
            }

            /// <summary>
            /// Binds the queue to exchange.
            /// </summary>
            /// <param name="exchange">The exchange.</param>
            /// <param name="binding">The binding.</param>
            public void AddBinding(string exchange, string binding)
            {
                _channel.QueueBind(Queue, exchange, binding);
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Interlocked.Exchange(ref _continueDequeue, 0);

                if (_connection == null) return;

                _channel.Abort();
                _connection.Close();
                _handlers.Clear();
                _consumer = null;
                _channel = null;
                _connection = null;
            }
        }

    }

    /// <summary>
    /// IConsume
    /// </summary>
    public interface IConsume : IDisposable
    {
        /// <summary>
        /// Des the queue.
        /// </summary>
        void DeQueue();

        /// <summary>
        /// Des the queue.
        /// </summary>
        /// <param name="purge">if set to <c>true</c> [purge].</param>
        void DeQueue(bool purge);

        /// <summary>
        /// Registers the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        void RegisterHandler<T>(Action<T> handler) where T:MessageBase;

        /// <summary>
        /// Stops dequeuing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Binds the queue to exchange.
        /// </summary>
        /// <param name="exchange">The exchange.</param>
        /// <param name="binding">The binding.</param>
        void AddBinding(string exchange, string binding);
    }

    /// <summary>
    /// IPublish
    /// </summary>
    public interface IPublish : IDisposable
    {
        /// <summary>
        /// Publishes the specified Messaging.
        /// </summary>
        /// <param name="message">The Messaging.</param>
        void Publish<T>(T message) where T : MessageBase;
    }

    /// <summary>
    /// IMessaging
    /// </summary>
    public interface IMessaging
    {
        /// <summary>
        /// Gets the consumer.
        /// </summary>
        /// <value>
        /// The consumer.
        /// </value>
        IConsume Consumer { get; }

        /// <summary>
        /// Gets the publisher.
        /// </summary>
        /// <value>
        /// The publisher.
        /// </value>
        IPublish Publisher { get; }
    }
}
