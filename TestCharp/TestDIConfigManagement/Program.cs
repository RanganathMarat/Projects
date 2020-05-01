using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Philips.DI.Config.PatientRegistration;
using Philips.DI.Interfaces.ConfigurationData;
using Philips.DI.Services.ConfigurationProvider;

namespace TestDIConfigManagement
{
    class Program
    {
        static void Main(string[] args)
        {   
            ConfigurationProvider configProvider = new ConfigurationProvider();
            PatientWeight diConfigurationData = configProvider.Get(PatientWeight.ConfigKey) as PatientWeight;
            Console.WriteLine(diConfigurationData?.MaximumWeight);
        }
    }
}
