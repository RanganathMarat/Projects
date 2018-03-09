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

namespace UserControlWithCodeBehind
{
    /// <summary>
    /// Interaction logic for EditableTextAndLabelControl.xaml
    /// </summary>
    public partial class EditableTextAndLabelControl : UserControl
    {
        public event Action<string, string> SaveEvent = null;

        public EditableTextAndLabelControl()
        {
            InitializeComponent();

            SaveButton.Click += SaveButton_Click;
        }

        // fires SaveEvent of the control
        void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveEvent != null)
            {
                SaveEvent(TheLabel, TheEditableText);
            }
        }

        #region TheLabel Dependency Property
        public string TheLabel
        {
            get { return (string)GetValue(TheLabelProperty); }
            set { SetValue(TheLabelProperty, value); }
        }

        public static readonly DependencyProperty TheLabelProperty =
        DependencyProperty.Register
        (
            "TheLabel",
            typeof(string),
            typeof(EditableTextAndLabelControl),
            new PropertyMetadata(null)
        );
        #endregion TheLabel Dependency Property


        #region TheEditableText Dependency Property
        public string TheEditableText
        {
            get { return (string)GetValue(TheEditableTextProperty); }
            set { SetValue(TheEditableTextProperty, value); }
        }

        public static readonly DependencyProperty TheEditableTextProperty =
        DependencyProperty.Register
        (
            "TheEditableText",
            typeof(string),
            typeof(EditableTextAndLabelControl),
            new PropertyMetadata(null)
        );
        #endregion TheEditableText Dependency Property
    }
}
