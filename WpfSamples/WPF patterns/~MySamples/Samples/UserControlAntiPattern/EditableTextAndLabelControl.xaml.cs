using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using UserControlAntiPattern.ViewModel;

namespace UserControlAntiPattern
{
    /// <summary>
    /// Interaction logic for EditableTextAndLabelControl.xaml
    /// </summary>
    public partial class EditableTextAndLabelControl : UserControl
    {

        public EditableTextAndLabelControl()
        {
            InitializeComponent();
        }

        private void UserButtonForSave_Click(object sender, RoutedEventArgs e)
        {
            if( SaveEvent !=null)
            {
                SaveEvent(LabelText, EditableText);
            }
        }

        #region Event definition
        public event Action<string, string> SaveEvent;
        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(EditableTextAndLabelControl), new PropertyMetadata(null));

        public string LabelText {
            get{return (string)GetValue(LabelTextProperty);}
            set{SetValue(LabelTextProperty, value);}
        }

        public static readonly DependencyProperty EditableTextProperty = DependencyProperty.Register("EditableText", typeof(string), typeof(EditableTextAndLabelControl), new PropertyMetadata(null));

        public string EditableText {
            get{return (string)GetValue(EditableTextProperty);}
            set{SetValue(EditableTextProperty, value);}
        }

        #endregion

    }
}
