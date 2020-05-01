using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFCustomControl;

namespace HostWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            List<Customer> customers = new List<Customer>();

            for (int index = 0; index < 10; index++)
            {
                Customer customer = new Customer() { Name = "Name" + index, Address = "Address" + index, Email = "Email" + index };
                customers.Add(customer);
            }
            this.hostingForm1.Customers = customers;
            this.hostingForm1.SelectedIndex = 7;
            //this.Controls.Add(hostingForm);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
