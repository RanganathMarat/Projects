using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace PrismSample
{
  public   class ApplicationBootStrapper:MefBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
           return  this.Container.GetExportedValue<ShellWindow>();
           
        }
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog .Catalogs.Add (new AssemblyCatalog (typeof (ShellWindow).Assembly) );
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeModule.EmployeeCrudModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ProjectsModule.ProjectCrudModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TestModuleLib.TestModule).Assembly));

        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }
       
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = this.Shell as Window;
            Application.Current.MainWindow.Show();
        }
        //protected override void ConfigureContainer()
        //{
        //    base.ConfigureContainer();
         
        //}
    }
}
