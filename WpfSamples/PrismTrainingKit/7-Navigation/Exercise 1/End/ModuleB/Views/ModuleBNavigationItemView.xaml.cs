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
using Infrastructure;

namespace ModuleB.Views
{
    [Export]
    public partial class ModuleBNavigationItemView : UserControl
    {

        private static Uri moduleBViewUri = new Uri(ViewNames.ModuleBView, UriKind.Relative);

        [Import]
        public IRegionManager regionManager;
        
        public ModuleBNavigationItemView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            regionManager.RequestNavigate(RegionNames.MainRegion, moduleBViewUri);
        }
    }
}
