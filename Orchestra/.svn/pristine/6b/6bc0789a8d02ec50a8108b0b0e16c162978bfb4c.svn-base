using System;
using System.Configuration;

namespace Common.Messaging
{
    /// <summary>
    ///     MessageBus
    /// </summary>
    public class MessageBus
    {
        /// <summary>
        ///     Gets or sets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static IMessageBus New(string nodeName)
        {
            var fileMap = new ConfigurationFileMap("Common.Messaging.dll.config");
            Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            var appSettingsSection = (AppSettingsSection) configuration.GetSection("appSettings");
            string imessageBusClass = appSettingsSection.Settings["imessagebusclass"].Value;
            string imessageBusAssembly = appSettingsSection.Settings["imessagebusassembly"].Value;

            var instance = Activator.CreateInstance(imessageBusAssembly, imessageBusClass).Unwrap() as IMessageBus;

            if (instance != null)
            {
                instance.Initialize(nodeName);

                return instance;
            }

            return null;
        }
    }
}