using System.ComponentModel.Composition;
using System.Windows.Controls;
using Prism.Workshop.Shell.Infrastructure;

namespace Prism.Workshop.Shell.Views
{
    [PartCreationPolicy(CreationPolicy.NonShared)] // creates a new instance of the view each time it is imported
    [ViewExport(RegionName = "MainRegion")] // registers the view with the MainRegion. More info: http://msdn.microsoft.com/en-us/library/ff921074(v=PandP.40).aspx
    public partial class ModuleAView : UserControl
    {
        public ModuleAView()
        {
            InitializeComponent();
        }
    }
}
