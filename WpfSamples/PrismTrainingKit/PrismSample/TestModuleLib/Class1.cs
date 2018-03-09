using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;

namespace TestModuleLib
{
    [ModuleExport(typeof (TestModule))]
    public class TestModule:IModule
    {
        [Import]
        public IRegionManager _regionManager { get; set; }


        public void Initialize()
        {
            _regionManager.AddToRegion("TestView", 
                new Views.TestView()
                { DataContext = new ViewModel.TestViewModel() });
        }
    }
}
