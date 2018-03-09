using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xaml;
using Microsoft.Practices.Unity;

namespace _5._2PrismDemo
{
    class Module_A :IModule
    {
        
        IRegionManager regionManager;

        public Module_A( IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
           
        {
            //System.Xml.XmlReader reader = System.Xml.XmlReader.Create("..//..//Views//View_one.xaml");
            //object viewroot = System.Windows.Markup.XamlReader.Load(reader);
            //reader.Close();
            this.regionManager.AddToRegion("MainRegion", new Views.View_One()) ;
            MessageBox.Show("Module Initialize");
        }
    }
}
