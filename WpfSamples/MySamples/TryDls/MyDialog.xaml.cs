using System.Windows;
using Dialog = Philips.Platform.Presentation.Controls.Dialog;

namespace TryDls
{
    /// <summary>
    /// Interaction logic for MyDialog.xaml
    /// </summary>
    public partial class MyDialog : Dialog
    {
        public MyDialog()
        {
            InitializeComponent();
        }

        private void DialogOkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
