using System.Collections.Generic;
using Philips.DI.Config.PatientRegistration;
using Philips.DI.Interfaces.ConfigurationData;
using Philips.DI.Interfaces.ConfigurationRegistry;

namespace Philips.MR.ConfigurationRegistry
{
    public class MrConfigurationRegistry : IDiConfigurationRegistry 
    {
        public T Get<T>(string configKey) where T : DIConfigurationData
        {
            return (T)_configDictionary[configKey];
        }

        public MrConfigurationRegistry()
        {
            InitializeConfiguration();
        }

        private void InitializeConfiguration()
        {
            var pw = new PatientWeight(100);
            _configDictionary[PatientWeight.ConfigKey] = pw;

            var pH = new PatientHeight(99.9);
            _configDictionary[PatientHeight.ConfigKey] = pH;

        }

        private readonly Dictionary<string, DIConfigurationData>
            _configDictionary = new Dictionary<string, DIConfigurationData>();
    }
}
