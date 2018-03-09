using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;

namespace ProjectsModule
{
    [ModuleExport(typeof (ProjectCrudModule))]
    
    public class ProjectCrudModule:IModule
    {
        [Import]
        public IRegionManager Region { get; set; }
        public void Initialize()
        {
            Region.RequestNavigate("PROJECTVIEW", "ProjectView");
           
        }
    }
}
