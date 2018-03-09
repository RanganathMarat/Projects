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

namespace KeyBoardHook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)this.OnlyButton.Content == "Start")
            {
                this.KeyUp += MainWindow_KeyDown;
            }
            else
            {
                this.MytextBox.Document.Blocks.Clear();
            }

            this.OnlyButton.Content = ((string)this.OnlyButton.Content == "Start") ? "Stop" : "Start";
            // Start the Keyboard Hook 
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.Tab )  )
            {
                this.MytextBox.AppendText(e.Key.ToString());
                this.MytextBox.AppendText("\n");
            }
        }

    }
}
