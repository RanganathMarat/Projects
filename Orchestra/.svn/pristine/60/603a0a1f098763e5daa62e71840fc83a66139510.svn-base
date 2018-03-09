namespace Common.Messaging
{
    /// <summary>
    ///     Consumes
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public static class Consumes<TMessage> where TMessage : class
    {
        /// <summary>
        /// All
        /// </summary>
        public interface All : IConsumer
        {
            /// <summary>
            /// Consumes the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            void Consume(TMessage message);
        }


        /// <summary>
        /// Context
        /// </summary>
        public interface Context : Consumes<IConsumeContext<TMessage>>.All
        {
        }
        
        /// <summary>
        /// Selected
        /// </summary>
        public interface Selected : All
        {
            /// <summary>
            /// Accepts the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <returns></returns>
            bool Accept(TMessage message);
        }
    }

    /// <summary>
    /// IConsumeContext
    /// </summary>
    public interface IConsumeContext<TMessage>
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        TMessage Message { get; }

        /// <summary>
        /// Responds the specified message.
        /// </summary>
        /// <typeparam name="TRespond">The type of the respond.</typeparam>
        /// <param name="message">The message.</param>
        void Respond<TRespond>(TRespond message) where TRespond : class;
    }

    /// <summary>
    ///     IConsumer
    /// </summary>
    public interface IConsumer
    {

    }
}