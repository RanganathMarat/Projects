﻿using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition.Hosting;

namespace Prism.Workshop.Shell
{
    public class WorkshopMefBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            // Return an instance of the Shell, retrieving it from the MEF container.
            // As the shell has no dependencies (imports) it could just be created with "new Shell()", but it is recommended to always export the shell accounting for future updates to shell dependencies.
            return this.Container.GetExportedValue<Shell>();   
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            // Set the shell as the RootVisual (startup window in this case) of the application.
            Application.Current.RootVisual = (UIElement)this.Shell;
        }

        protected override void ConfigureAggregateCatalog()
        {
            // Modules are added / registered in the aggregate catalog
            // More info: http://msdn.microsoft.com/en-us/library/ff921163(PandP.40).aspx
            
            base.ConfigureAggregateCatalog();

            // Add the shell to the catalog so that it can be retrieved afterwards in the CreateShell method.            
            this.AggregateCatalog.Catalogs.Add(new TypeCatalog(new[] { typeof(Shell) }));

            // Add Module A to the catalog
            this.AggregateCatalog.Catalogs.Add(new TypeCatalog(new[] { typeof(ModuleA) }));

            // Both previous registrations could be replaced by registering the whole assembly:
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Shell).Assembly));           
        }       
    }
}
