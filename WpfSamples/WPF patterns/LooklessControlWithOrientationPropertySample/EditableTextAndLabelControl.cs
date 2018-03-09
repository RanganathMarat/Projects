using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LooklessControlWithOrientationPropertySample
{
    public class EditableTextAndLabelControl : Control
    {
        public event Action<string, string> SaveEvent = null;

        public void Save()
        {
            if (SaveEvent != null)
                SaveEvent(TheLabel, TheEditableText);
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


        #region TheOrientation Dependency Property
        public Orientation TheOrientation
        {
            get { return (Orientation)GetValue(TheOrientationProperty); }
            set { SetValue(TheOrientationProperty, value); }
        }

        public static readonly DependencyProperty TheOrientationProperty =
        DependencyProperty.Register
        (
            "TheOrientation",
            typeof(Orientation),
            typeof(EditableTextAndLabelControl),
            new PropertyMetadata(Orientation.Horizontal)
        );
        #endregion TheOrientation Dependency Property
    }
}
