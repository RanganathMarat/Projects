using System.Windows;
using System.Windows.Controls;

namespace PrismApplication
{
    /// <summary>
    /// Interaction logic for PatientHeaderView.xaml
    /// </summary>
    public partial class PatientHeaderView : UserControl
    {
        public PatientHeaderView()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            HeaderViewModel vm = (HeaderViewModel)this.DataContext;
            if(vm != null)
            {
                vm.MoveToAcqCommand();
            }
        }
    }
}
