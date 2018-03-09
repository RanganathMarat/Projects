using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Composite.Modularity;
using System.Windows;

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

        protected override Microsoft.Practices.Composite.Modularity.IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog();
        }
    }
}
