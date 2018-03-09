using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Prism.Workshop
{
    public class ModuleA : IModule
    {
        private IRegionManager regionManager;

        public ModuleA(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        
        public void Initialize()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "I'm in the Shell";
            this.regionManager.Regions["MainRegion"].Add(textBlock);
        }
    }
}
