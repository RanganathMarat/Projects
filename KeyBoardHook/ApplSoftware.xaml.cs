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
using System.Windows.Shapes;

namespace KeyBoardHook
{
    /// <summary>
    /// Interaction logic for ApplSoftware.xaml
    /// </summary>
    public partial class ApplSoftware : Window
    {
        public ApplSoftware()
        {
            InitializeComponent();
            this.KeyDown += ApplSoftware_KeyUp;
        }

        void ApplSoftware_KeyUp(object sender, KeyEventArgs e)
        {
            if( (ModifierKeys.Control == Keyboard.Modifiers) && (e.Key == Key.Tab))
            {
                e.Handled = true;
               MessageBox.Show(e.Key.ToString());
            }
        }

        
    }
}
