using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;

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
    }
}
