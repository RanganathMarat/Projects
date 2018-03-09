using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;
using System;
using Workshop.Infrastructure.Services;
using Microsoft.Practices.Prism.Logging;

namespace Prism.Workshop
{
    public class WorkshopBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Shell shell = new Shell();
            Application.Current.RootVisual = shell;
            return shell;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return
               Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                   new Uri("/Prism.Workshop;component/ModulesCatalog.xaml", UriKind.Relative));
        }

        protected override void ConfigureContainer()
        {
            RegisterTypeIfMissing(typeof(IUIMessagesService), typeof(UIMessagesService), true);
            base.ConfigureContainer();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new XmlLogger();
        }        
    }
}
