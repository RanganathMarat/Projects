using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition.Hosting;


namespace NavigationJournal
{
    public class WorkshopBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
           return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.RootVisual = (UIElement)this.Shell;
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new TypeCatalog(new[] {
                typeof(Shell),
            }));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleA.ModuleA).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleB.ModuleB).Assembly));
        }

    }
}
