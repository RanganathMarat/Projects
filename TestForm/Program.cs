using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var context = System.Threading.SynchronizationContext.Current; 
            if (context == null) 
            {
                MessageBox.Show("NOT Present");

            }
            else
            {
                MessageBox.Show("Present");            
            }

            Form form = new Form();
            context = System.Threading.SynchronizationContext.Current; 
            if (context == null) 
            {
                MessageBox.Show("2 NOT Present");
            }
            else
            {
                MessageBox.Show("2 Present");
            }

            Application.Run(new Form1());
        }
    }
}
