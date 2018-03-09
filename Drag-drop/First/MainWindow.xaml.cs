using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace First
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data data = null;
        public MainWindow()
        {
            InitializeComponent();
            data = new Data();
            this.DataContext = data;
        }

        private void FirstOne_MouseMove(object sender, MouseEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            this.data.MouseState = e.LeftButton.ToString();
            if ( e.LeftButton == MouseButtonState.Pressed)
            {
                this.data.EntryCounter++;
                //System.Threading.Thread.Sleep(5000);
                DragDrop.DoDragDrop(ellipse, "Red", DragDropEffects.Move);
            }
        }

        private void SecondOne_Drop(object sender, DragEventArgs e)
        {
            this.SecondOne.Fill = (Brush)new BrushConverter().ConvertFromString((string)e.Data.GetData(DataFormats.StringFormat));
        }

        private void SecondOne_DragEnter(object sender, DragEventArgs e)
        {
            this.data.EntryCounter2++;
        }
    }
}
