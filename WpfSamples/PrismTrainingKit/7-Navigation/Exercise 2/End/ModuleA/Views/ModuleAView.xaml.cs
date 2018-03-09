using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA.Views
{

    [Export(ViewNames.ModuleAView)]
    public partial class ModuleAView : UserControl, INavigationAware
    {
        public ModuleAView()
        {
            InitializeComponent();
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string previousPage = navigationContext.Parameters["previous"];
            if (!String.IsNullOrEmpty(previousPage))
            {
                this.txtNavigatedFrom.Text = string.Format("Navigating from: {0}", previousPage.Split('?')[0]);
            }
        }
    }
}
