using System;
using System.Collections.Generic;
using Common.Messaging;
using MassTransit;
using IConsumer = MassTransit.IConsumer;
using UnsubscribeAction = Common.Messaging.UnsubscribeAction;

namespace Messaging.MSMQ_MT
{
    /// <summary>
    /// MessageBus
    /// </summary>
    public class MessageBus : IMessageBus
    {
        /// <summary>
        /// Gets or sets the control bus.
        /// </summary>
        /// <value>
        /// The control bus.
        /// </value>
        public IServiceBus ControlBus { get; set; }
        /// <summary>
        /// Gets or sets the data bus.
        /// </summary>
        /// <value>
        /// The data bus.
        /// </value>
        public IServiceBus DataBus { get; set; }

        private List<IConsumer> _consumers;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            DataBus.Dispose();
            ControlBus.Dispose();
            _consumers.Clear();
        }

        /// <summary>
        /// Initializes the specified node name.
        /// </summary>
        /// <param name="nodeName">Name of the node.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Initialize(string nodeName)
        {
            InitializeServiceBus(nodeName);
            _consumers = new List<IConsumer>();
        }

        /// <summary>
        ///     Initializes the service bus.
        /// </summary>
        public void InitializeServiceBus(string queueName)
        {
            try
            {
                InitializeBus(queueName);
            }
            catch (Exception)
            {
                // Retry
                Bus.Shutdown();
                InitializeBus(queueName);
            }
        }

        private void InitializeBus(string queueName)
        {
            DataBus = ServiceBusFactory.New(x =>
            {
                x.ReceiveFrom(queueName);
                x.SetPurgeOnStartup(true);

                x.UseMsmq(q => q.UseSubscriptionService("msmq://localhost/mt_subscriptions"));

                x.UseControlBus();

                x.SetConcurrentConsumerLimit(4);
            });

            ControlBus = DataBus.ControlBus;
        }

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            DataBus.Publish(message);
        }

        /// <summary>
        /// Subscribes all.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public UnsubscribeAction SubscribeAll<TMessage>(Common.Messaging.Consumes<TMessage>.All instance) where TMessage : class
        {
            var consumesAll = new ConsumesAll<TMessage>(instance);
            var unsubscribeAction = DataBus.SubscribeInstance(consumesAll);
            _consumers.Add(consumesAll);
            return new UnsubscribeAction(unsubscribeAction);
        }

        /// <summary>
        /// Subscribes the context.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public UnsubscribeAction SubscribeContext<TMessage>(Common.Messaging.Consumes<TMessage>.Context instance) where TMessage : class
        {
            var consumesContext = new ConsumesContext<TMessage>(instance);
            var unsubscribeAction = DataBus.SubscribeInstance(consumesContext);
            _consumers.Add(consumesContext);
            return new UnsubscribeAction(unsubscribeAction);
        }

        /// <summary>
        /// Subscribes the selected.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public UnsubscribeAction SubscribeSelected<TMessage>(Common.Messaging.Consumes<TMessage>.Selected instance) where TMessage : class
        {
            var consumesSelected = new ConsumesSelected<TMessage>(instance);
            var unsubscribeAction = DataBus.SubscribeInstance(consumesSelected);
            _consumers.Add(consumesSelected);
            return new UnsubscribeAction(unsubscribeAction);
        }
    }
}
