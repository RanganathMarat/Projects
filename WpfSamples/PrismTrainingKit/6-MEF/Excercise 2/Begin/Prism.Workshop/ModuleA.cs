using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace Prism.Workshop.Shell
{
    [ModuleExport(typeof(ModuleA))]
    public class ModuleA : IModule
    {
        private readonly IRegionManager regionManager;

        [ImportingConstructor]
        public ModuleA(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        
        public void Initialize()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Module A Loaded!. Module A is located on the Shell project and registered on the Aggregate catalog in the Bootstrapper.";
            this.regionManager.Regions["MainRegion"].Add(textBlock);
        }
    }
}
