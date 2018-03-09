using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCustomControl;

namespace HostWinForms
{
    [Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
    [DesignerSerializer("System.ComponentModel.Design.Serialization.TypeCodeDomSerializer , System.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
    class HostingForm:System.Windows.Forms.Integration.ElementHost
    {
        private ComboboxWithGrid comboboxWithGrid = new ComboboxWithGrid();

        public HostingForm()
        {
            this.Child = comboboxWithGrid;
        }

        public int SelectedIndex
        {
            get
            {
                return comboboxWithGrid.SelectedIndex;
            }
            set
            {
                comboboxWithGrid.SelectedIndex = value;
            }
        }


        public List<Customer> Customers
        {
            get
            {
                return comboboxWithGrid.Customers;
            }
            set
            {
                comboboxWithGrid.Customers = value;
            }
        }
    }
}
