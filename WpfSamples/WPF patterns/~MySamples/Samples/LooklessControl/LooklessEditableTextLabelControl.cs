using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LooklessControl
{
    class LooklessEditableTextLabelControl : Control
    {
        public event Action<string, string> SaveEvent;
        
        // Add dependency properties


        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(LooklessEditableTextLabelControl), new PropertyMetadata(null));



        public string EditableText
        {
            get { return (string)GetValue(EditableTextProperty); }
            set { SetValue(EditableTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditableText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditableTextProperty =
            DependencyProperty.Register("EditableText", typeof(string), typeof(LooklessEditableTextLabelControl), new PropertyMetadata(null));


        // A new public method that serves to support the link to the user action on the control. Similar to the Command Binding.
        public void Save()
        {
            if (SaveEvent != null)
            {
                SaveEvent(LabelText, EditableText);
            }
        }
    }
}
