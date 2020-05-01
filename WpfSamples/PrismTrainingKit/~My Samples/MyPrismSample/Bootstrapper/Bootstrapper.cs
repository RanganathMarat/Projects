using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using MyPrismSample.Module;
using Prism;
using Prism.Modularity;
using Prism.Unity;

namespace MyPrismSample.Bootstrapper
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ConfigurationModuleCatalog configurationModuleCatalog = new ConfigurationModuleCatalog();
            return configurationModuleCatalog;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(AuthorListModule));
        }
    }

    public static class RegionNames
    {
        public const string AuthorListRegion = "AuthorListRegion";
        public const string AuthorBookDetailsRegion = "AuthorBookDetailsRegion";
    }
}
