using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Philips.Platform.Presentation;
using Philips.Platform.Presentation.Automation;
using Philips.Platform.Presentation.Controls;
using MessageBox = Philips.Platform.Presentation.Controls.MessageBox;

namespace TryDls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MyDialog mydialog = new MyDialog();
            mydialog.ShowDialog();
            #region PlainDialog

            //var d = new Dialog
            //{
            //    Content = "Test content on the dialog",
            //    Width = 300,
            //    Height = 250,
            //    Title = "Scanner",
            //    HasCloseButton = true

            //};
            //d.Resources.MergedDictionaries.Add
            //(
            //    new ResourceDictionary { Source = new Uri("/Philips.Platform.Presentation;component/DLS/DLS.VeryDark.xaml", UriKind.Relative) }
            //);
            //d.Style = (Style)TryFindResource("DialogStyle");
            //d.SetValue(AutomationProperties.AutomationIdProperty, "TestDialog");
            //d.SetResourceReference(StyleProperty, "DialogStyle");
            //d.ShowDialog();

            #endregion


        }
    }
}
