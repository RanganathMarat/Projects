using System.Windows.Controls;
using System.Windows.Input;

namespace PrismApplication
{
    /// <summary>
    /// Interaction logic for ScanlistView.xaml
    /// </summary>
    public partial class ScanlistView : UserControl
    {
        public ScanlistView()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineLeft();
            }
            else
            {
                scrollviewer.LineRight();
            }
            e.Handled = true;
        }

    }
}
