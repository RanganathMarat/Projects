
using CMSG = Common.Messaging;

namespace Messaging.RabbitMQ
{
    /// <summary>
    /// Consumes All
    /// </summary>
    public class ConsumesAll<T> where T : class
    {
        private readonly CMSG.Consumes<T>.All _consumer;


        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumesAll{T}"/> class.
        /// </summary>
        public ConsumesAll(CMSG.Consumes<T>.All consumer)
        {
            _consumer = consumer;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(T message)
        {
            _consumer.Consume(message);
        }
    }

    /// <summary>
    /// Consumes Selected
    /// </summary>
    public class ConsumesSelected<T> where T : class
    {
        private readonly CMSG.Consumes<T>.Selected _consumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumesAll{T}"/> class.
        /// </summary>
        public ConsumesSelected(CMSG.Consumes<T>.Selected consumer)
        {
            _consumer = consumer;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(T message)
        {
            _consumer.Consume(message);
        }

        /// <summary>
        /// Accepts the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public bool Accept(T message)
        {
            return _consumer.Accept(message);
        }
    }

    /// <summary>
    /// Consumes Context
    /// </summary>
    public class ConsumesContext<T> where T : class
    {
        private readonly CMSG.Consumes<T>.Context _consumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumesAll{T}"/> class.
        /// </summary>
        public ConsumesContext(CMSG.Consumes<T>.Context consumer)
        {
            _consumer = consumer;
        }
        ///// <summary>
        ///// Consumes the specified message.
        ///// </summary>
        ///// <param name="context">The context.</param>
        //public void Consume(IConsumeContext<T> context)
        //{
        //    _consumer.Consume(new ConsumeContext<T>(context));
        //}
    }

    /// <summary>
    /// ConsumeContext
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConsumeContext<T> where T : class
    {
        //private readonly IConsumeContext<T> _context;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ConsumeContext{T}"/> class.
        ///// </summary>
        //public ConsumeContext(IConsumeContext<T> context)
        //{
        //    _context = context;
        //}

        ///// <summary>
        ///// Gets the message.
        ///// </summary>
        ///// <value>
        ///// The message.
        ///// </value>
        //public T Message { get { return _context.Message; } }

        ///// <summary>
        ///// Responds the specified message.
        ///// </summary>
        ///// <typeparam name="TRespond">The type of the respond.</typeparam>
        ///// <param name="message">The message.</param>
        //public void Respond<TRespond>(TRespond message) where TRespond : class
        //{
        //    _context.Respond(message);
        //}
    }
}
