using System.ComponentModel.Composition;
using Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using ModuleB.Views;

namespace ModuleB
{
    [ModuleExport(typeof(ModuleB))]
    public class ModuleB : IModule
    {

        [Import]
        public IRegionManager regionManager;

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, typeof(ModuleBNavigationItemView));
        }
    }
}
