using System.ComponentModel.Composition;
using System.Windows.Controls;
using Prism.Workshop.Infrastructure;
using Microsoft.Practices.Prism.Modularity;

namespace Prism.Workshop.Shell.Views
{
    [PartCreationPolicy(CreationPolicy.Shared)] // creates a singleton instance of the view
    [ViewExport(RegionName = "MainRegion")] // registers the view with the MainRegion. More info: http://msdn.microsoft.com/en-us/library/ff921074(v=PandP.40).aspx
    public partial class ModuleAView : UserControl
    {
        private readonly IModuleManager moduleManager;
        
        [ImportingConstructor]
        public ModuleAView(IModuleManager moduleManager)
        {
            this.moduleManager = moduleManager;
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.LoadButton.IsEnabled = false;
            this.moduleManager.LoadModule("ModuleB");
        }
    }
}
