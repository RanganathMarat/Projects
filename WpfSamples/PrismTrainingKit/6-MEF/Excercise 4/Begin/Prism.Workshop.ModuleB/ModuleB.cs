using System.Windows;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Prism.Workshop.ModuleB
{
    [ModuleExport(typeof(ModuleB))]
    public class ModuleB : IModule
    {
        public void Initialize()
        {
            MessageBox.Show("Module B Initialized!");
        }
    }
}
