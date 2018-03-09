using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Messaging
{
    /// <summary>
    /// UnsubscribeAction
    /// </summary>
    /// <returns></returns>
    public delegate bool UnsubscribeAction();

    /// <summary>
    /// IMessageBus
    /// </summary>
    public interface IMessageBus : IDisposable
    {
        /// <summary>
        /// Initializes the specified node name.
        /// </summary>
        /// <param name="nodeName">Name of the node.</param>
        void Initialize(string nodeName);

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        void Publish<TMessage>(TMessage message) where TMessage : class;

        /// <summary>
        /// Subscribes the specified instance.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        UnsubscribeAction SubscribeAll<TMessage>(Consumes<TMessage>.All instance) where TMessage : class;

        /// <summary>
        /// Subscribes the specified instance.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        UnsubscribeAction SubscribeContext<TMessage>(Consumes<TMessage>.Context instance) where TMessage : class;

        /// <summary>
        /// Subscribes the specified instance.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        UnsubscribeAction SubscribeSelected<TMessage>(Consumes<TMessage>.Selected instance) where TMessage : class;
    }
}
