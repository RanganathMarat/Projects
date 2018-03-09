using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;

namespace Common.Messaging
{
    /// <summary>
    ///     MessagingConfiguration
    /// </summary>
    public class MessagingConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IDictionary<string, MessagingEndpointElement> Endpoints { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            if (Endpoints != null) return;

            Endpoints = new Dictionary<string, MessagingEndpointElement>();
            //var fileMap = new ConfigurationFileMap(Assembly.GetEntryAssembly().FullName + ".config");
            //Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            var connectionManagerDataSection = ConfigurationManager.GetSection(SectionName) as MessagingConfigurationSection;
            if (connectionManagerDataSection == null) return;

            foreach (MessagingEndpointElement endpointElement in connectionManagerDataSection.MessagingEndpoints)
            {
                Endpoints.Add(endpointElement.Name, endpointElement);
            }
        }

        /// <summary>
        ///     The name of this section in the app.config.
        /// </summary>
        private const string SectionName = "MessagingConfigurationSection";

        private const string EndpointCollectionName = "MessagingEndpoints";

        /// <summary>
        /// Gets the messaging endpoints.
        /// </summary>
        /// <value>
        /// The messaging endpoints.
        /// </value>
        [ConfigurationProperty(EndpointCollectionName)]
        [ConfigurationCollection(typeof (MessagingEndpointsCollection), AddItemName = "add")]
        internal MessagingEndpointsCollection MessagingEndpoints
        {
            get { return (MessagingEndpointsCollection) base[EndpointCollectionName]; }
        }
    }

    /// <summary>
    ///     MessagingEndpointsCollection
    /// </summary>
    internal class MessagingEndpointsCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MessagingEndpointElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MessagingEndpointElement) element).Name;
        }
    }

    /// <summary>
    ///     CaseInsensitiveEnumConfigConverter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CaseInsensitiveEnumConfigConverter<T> : ConfigurationConverterBase
    {
        public override object ConvertFrom(
            ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            return data != null ? Enum.Parse(typeof (T), (string) data, true) : default(T);
        }
    }

    /// <summary>
    ///     MessagingEndpointElement
    /// </summary>
    public class MessagingEndpointElement : ConfigurationElement
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        ///     Gets or sets the type of the mq.
        /// </summary>
        /// <value>
        ///     The type of the mq.
        /// </value>
        [TypeConverter(typeof (CaseInsensitiveEnumConfigConverter<MQType>))]
        [ConfigurationProperty("mqtype", IsRequired = true, DefaultValue = MQType.RabbitMQ)]
        public MQType MQType
        {
            get { return (MQType) this["mqtype"]; }
            set { this["mqtype"] = value; }
        }

        /// <summary>
        ///     Gets or sets the topology.
        /// </summary>
        /// <value>
        ///     The topology.
        /// </value>
        [TypeConverter(typeof (CaseInsensitiveEnumConfigConverter<Topology>))]
        [ConfigurationProperty("topology", IsRequired = true, DefaultValue = Topology.Basic)]
        public Topology Topology
        {
            get { return (Topology) this["topology"]; }
            set { this["topology"] = value; }
        }

        /// <summary>
        ///     Gets or sets the queue.
        /// </summary>
        /// <value>
        ///     The queue.
        /// </value>
        [ConfigurationProperty("queue", IsRequired = false)]
        public string Queue
        {
            get { return (string) this["queue"]; }
            set { this["queue"] = value; }
        }

        /// <summary>
        ///     Gets or sets the exchange.
        /// </summary>
        /// <value>
        ///     The exchange.
        /// </value>
        [ConfigurationProperty("exchange", IsRequired = false)]
        public string Exchange
        {
            get { return (string) this["exchange"]; }
            set { this["exchange"] = value; }
        }

        /// <summary>
        ///     Gets or sets the bindig.
        /// </summary>
        /// <value>
        ///     The bindig.
        /// </value>
        [ConfigurationProperty("binding", IsRequired = false)]
        public string Bindig
        {
            get { return (string)this["binding"]; }
            set { this["binding"] = value; }
        }
    }

    /// <summary>
    ///     Messaging middle ware type
    /// </summary>
    public enum MQType
    {
        /// <summary>
        /// The rabbit mq
        /// </summary>
// ReSharper disable InconsistentNaming
        RabbitMQ,
// ReSharper restore InconsistentNaming
        /// <summary>
        /// The zero mq
        /// </summary>
// ReSharper disable InconsistentNaming
        ZeroMQ
// ReSharper restore InconsistentNaming
    }

    /// <summary>
    ///     Messaging Topology
    /// </summary>
    public enum Topology
    {
        /// <summary>
        /// The basic
        /// </summary>
        Basic,
        /// <summary>
        /// The pub sub
        /// </summary>
        PubSub,
        /// <summary>
        /// The routing
        /// </summary>
        Routing,
        /// <summary>
        /// The topic
        /// </summary>
        Topic,
        /// <summary>
        /// The RPC
        /// </summary>
// ReSharper disable InconsistentNaming
        RPC
// ReSharper restore InconsistentNaming
    }
}