using System.Windows;
using Workshop.ModuleA;
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
            return new ModuleCatalog().AddModule(typeof(ModuleA));
        }
    }
}
