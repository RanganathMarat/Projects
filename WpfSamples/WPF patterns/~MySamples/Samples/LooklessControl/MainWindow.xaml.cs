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

namespace LooklessControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FirstControl.SaveEvent += FirstControl_SaveEvent;
            SecondControl.SaveEvent += FirstControl_SaveEvent; 
        }

        void FirstControl_SaveEvent(string arg1, string arg2)
        {
            OutputBlock.Text += "The Control " + arg1 + " displayed the text " + arg2 + "\n";
        }
    }
}
