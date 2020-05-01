using System.Windows.Controls;

namespace PrismApplication
{
    /// <summary>
    /// Interaction logic for TaskContentView.xaml
    /// </summary>
    public partial class TaskContainerView : UserControl
    {
        public TaskContainerView()
        {
            InitializeComponent();
            TabControl.SelectionChanged += (o, e) =>
            {

            };
        }
    }
}
