using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace TestModuleLib.ViewModel
{
   public  class TestViewModel:NotificationObject    
   {
       string text;
       public string Text
       {
           get { return text; }
           set
           {
               if (text != value)
               {

                   text = value;
                   RaisePropertyChanged("Text");
               }
           }

       }
       public TestViewModel()
       {
           Text = "Hello From Test View Model";
           Get = new DelegateCommand(() => { GetLogic(); }, () => { return true; });
       }
       public ICommand Get{get;set;}
       public void GetLogic()
       {
           Text = "New Value from View Model";
       }

    }
}
