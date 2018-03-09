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

namespace EmployeeModule.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    /// 

    [Export("EmployeeView")]
    public partial class EmployeeView : UserControl
    {

        [Import]
        public  IEventAggregator _eventAggregator { get; set; }
        public EmployeeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var _event = _eventAggregator.GetEvent<EventContracts.ProjectAddedEvent>();
            _event.Publish(new EventContracts.Project { ProjectId = "P100", ProjectName = "TEst" });
        }

    }
}
