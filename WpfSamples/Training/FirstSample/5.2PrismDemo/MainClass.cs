using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._2PrismDemo
{
    class MainClass
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Windows.Application app = new System.Windows.Application();
            app.Startup += app_Startup;
            app.Run();
        }

        static void app_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            // Start the Bootstraper
            ApplicationUnityBootStrapper unityBootStrapper = new ApplicationUnityBootStrapper();
            unityBootStrapper.Run();
        }
    }
}
