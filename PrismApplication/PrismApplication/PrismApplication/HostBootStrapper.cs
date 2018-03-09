#region Copyright Koninklijke Philips Electronics N.V. 2016
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: filename.cs
//
#endregion

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace PrismApplication
{
    public class HostBootStrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            IUnityContainer container = this.Container;
            container.LoadConfiguration();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ConfigurationModuleCatalog moduleCatalog = new ConfigurationModuleCatalog();
            return moduleCatalog;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(HostControllerModule));
        }

        protected override DependencyObject CreateShell()
        {
            // Use the container to create an instance of the shell.
            ShellWindow view = this.Container.TryResolve<ShellWindow>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History