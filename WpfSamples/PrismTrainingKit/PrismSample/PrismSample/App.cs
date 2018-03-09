using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PrismSample
{
    class App:Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationBootStrapper _bootStrapper = new ApplicationBootStrapper();
            _bootStrapper.Run();
        }

        [STAThread]
        static void Main()
        {
            App _obj = new App();
            _obj.Run();
        }

    }


}
