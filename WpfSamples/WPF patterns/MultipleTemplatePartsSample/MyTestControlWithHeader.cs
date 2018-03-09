using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MultipleTemplatePartsSample
{
    public class MyTestControlWithHeader : Control
    {
        #region HeaderTemplate Dependency Property
        public ControlTemplate HeaderTemplate
        {
            get { return (ControlTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.Register
        (
            "HeaderTemplate",
            typeof(ControlTemplate),
            typeof(MyTestControlWithHeader),
            new PropertyMetadata(null)
        );
        #endregion HeaderTemplate Dependency Property

        #region MainTemplate Dependency Property
        public ControlTemplate MainTemplate
        {
            get { return (ControlTemplate)GetValue(MainTemplateProperty); }
            set { SetValue(MainTemplateProperty, value); }
        }

        public static readonly DependencyProperty MainTemplateProperty =
        DependencyProperty.Register
        (
            "MainTemplate",
            typeof(ControlTemplate),
            typeof(MyTestControlWithHeader),
            new PropertyMetadata(null)
        );
        #endregion MainTemplate Dependency Property

        #region EventLog Dependency Property
        public string EventLog
        {
            get { return (string)GetValue(EventLogProperty); }
            set { SetValue(EventLogProperty, value); }
        }

        public static readonly DependencyProperty EventLogProperty =
        DependencyProperty.Register
        (
            "EventLog",
            typeof(string),
            typeof(MyTestControlWithHeader),
            new PropertyMetadata(null)
        );
        #endregion EventLog Dependency Property

        public void LogClickEvent()
        {
            EventLog += "\nClick Recieved";
        }
    }
}
