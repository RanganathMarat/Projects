using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;

namespace _5._2PrismDemo
{
    class ApplicationMEFBootStrapper : MefBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            throw new NotImplementedException();
        }
    }

    class ApplicationUnityBootStrapper:UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
           
        {
            base.InitializeShell();
            Application.Current.MainWindow = this.Shell as Window;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            System.Type moduleType = typeof(Module_A);

            ModuleInfo moduleInfo = new ModuleInfo() { 
                ModuleName = moduleType.Name, 
                ModuleType = moduleType.AssemblyQualifiedName };

            this.ModuleCatalog.AddModule(moduleInfo);
            
        }
    }
}
