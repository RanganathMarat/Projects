using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TryContainers
{
    /// <summary>   
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs s)
        {
            // Putting a sleep in this method to view the behavior of
            // the Splash screen.
            base.OnStartup(s);
            System.Threading.Thread.Sleep(5000);
        }

        //protected override void OnActivated(EventArgs e)
        //{
        //    base.OnActivated(e);
        //    MessageBox.Show("Switch");
        //}
    }
}
