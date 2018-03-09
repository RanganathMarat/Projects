using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
namespace EmployeeModule
{

    [ModuleExport(typeof (EmployeeCrudModule))]
    public class EmployeeCrudModule:IModule
    {
        [Import]
        public IRegionManager _regionManager { get; set; }
        public void Initialize()
        {
            _regionManager.RequestNavigate("EMPVIEW", "EmployeeView");
        }
    }
}
