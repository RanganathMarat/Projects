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

namespace DependencyPropertyChangeDetectionSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyControl1.SaveEvent += MyUserControl_SaveEvent;
            MyControl2.SaveEvent += MyUserControl_SaveEvent;
        }

        void MyUserControl_SaveEvent(string arg1, string arg2)
        {
            TheSaveEventLog.Text += "\nSaved string \"" + arg2 + "\" for label \"" + arg1 + "\"";
        }
    }
}
