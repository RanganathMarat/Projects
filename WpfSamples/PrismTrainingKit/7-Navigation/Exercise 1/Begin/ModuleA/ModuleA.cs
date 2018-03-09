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
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using ModuleA.Views;
using Infrastructure;


namespace ModuleA
{
    [ModuleExport(typeof(ModuleA))]
    public class ModuleA : IModule
    {

        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            // Register ModuleANavigationItemView view using ViewDiscovery
        }
    }
}
