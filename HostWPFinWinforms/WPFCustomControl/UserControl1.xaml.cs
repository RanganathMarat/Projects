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

namespace WPFCustomControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ComboboxWithGrid : UserControl
    {
        public ComboboxWithGrid()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public int SelectedIndex
        {
            get
            {
                return combobox.SelectedIndex;
            }
            set
            {
                combobox.SelectedIndex = value;
                
            }
        }

        public List<Customer> Customers { get; set; }
    }
}
