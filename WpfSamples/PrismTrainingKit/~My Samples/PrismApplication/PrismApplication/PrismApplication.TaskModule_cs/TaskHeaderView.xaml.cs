using PrismApplication.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrismApplication
{
    /// <summary>
    /// Interaction logic for TaskHeaderView.xaml
    /// </summary>
    public partial class TaskHeaderView : UserControl
    {
        public TaskHeaderView()
        {
            InitializeComponent();
        }

        private void OnTaskHeaderMouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement control = (FrameworkElement)sender;
            TasklistViewModel viewModel = (TasklistViewModel)this.DataContext;
            if (control != null && viewModel != null)
            {
                ITask dataContext = (ITask)control.DataContext;
                if (dataContext != null)
                {
                    viewModel.SelectTaskCommand(dataContext);
                }
            }
        }
    }
}
