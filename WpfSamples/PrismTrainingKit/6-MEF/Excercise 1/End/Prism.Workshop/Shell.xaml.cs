using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Prism.Workshop.Shell
{
    [Export]
    public partial class Shell : UserControl
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
