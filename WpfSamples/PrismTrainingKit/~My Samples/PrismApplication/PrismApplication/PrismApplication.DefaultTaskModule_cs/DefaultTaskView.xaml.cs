using System;
using System.Windows.Controls;

namespace PrismApplication
{
    /// <summary>
    /// Interaction logic for PatientHeaderView.xaml
    /// </summary>
    public partial class DefaultTaskView : UserControl
    {
        private byte[] heavyPayload = new byte[1024*1024*1];

        public DefaultTaskView()
        {
            InitializeComponent();
            this.IdLabel.Text = Guid.NewGuid().ToString();
        }
    }
}
