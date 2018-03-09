using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestCharp
{
    internal static class TryWindow
    {
        [STAThread]
        public static void Start()
        {
            Application app = new Application();
            Window win = new Window();
            app.MainWindow = win;
            win.Show();
            app.Run(); 
        }
    }
}
