using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace ProjectsModule.View
{
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    /// 

    [Export("ProjectView")]
    public partial class ProjectView : UserControl
    {

      //[Import]
       IEventAggregator _eventAgreegator { get; set; }

        [ImportingConstructor]
        public ProjectView(IEventAggregator _eventAgreegator)
        {

            InitializeComponent();
            this._eventAgreegator = _eventAgreegator;
            var _event = _eventAgreegator.GetEvent<EventContracts.ProjectAddedEvent>();
            _event.Subscribe(GetProjectData);
        }

        void GetProjectData(EventContracts.Project projectDetails)
        {
            this.DataContext = projectDetails;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ator = ServiceLocator.Current.GetService(typeof(EventAggregator)) as IEventAggregator;
           
        }
    }
}
