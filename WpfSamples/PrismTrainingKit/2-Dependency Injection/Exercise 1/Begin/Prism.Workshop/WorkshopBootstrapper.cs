using System.Windows;
using System;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;

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
            return  Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                   new Uri("/Prism.Workshop;component/ModulesCatalog.xaml", UriKind.Relative));
        }
    }
}
