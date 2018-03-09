using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace EventContracts
{

   public  class Project
    {
        public string ProjectId{get;set;}
        public string ProjectName{get;set;}
    }
    public class ProjectAddedEvent:CompositePresentationEvent<Project>
    {

    }
}
