using System.ComponentModel.Composition;
using System.Windows.Controls;
using Prism.Workshop.Infrastructure;

namespace Prism.Workshop.ModuleB.Views
{
    [PartCreationPolicy(CreationPolicy.Shared)] // creates a singleton instance of the view
    [ViewExport (RegionName = "MainRegion")] // registers the view with the MainRegion. More info: http://msdn.microsoft.com/en-us/library/ff921074(v=PandP.40).aspx    
    public partial class ModuleBView : UserControl
    {
        public ModuleBView()
        {
            InitializeComponent();
        }
    }
}
