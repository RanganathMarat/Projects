using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LooklessControlWithOrientationAttachedPropertySample
{
    public class AttachedProperties
    {
        #region TheOrientation attached Property
        public static Orientation GetTheOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(TheOrientationProperty);
        }

        public static void SetTheOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(TheOrientationProperty, value);
        }

        public static readonly DependencyProperty TheOrientationProperty =
        DependencyProperty.RegisterAttached
        (
            "TheOrientation",
            typeof(Orientation),
            typeof(AttachedProperties),
            new PropertyMetadata(Orientation.Horizontal)
        );
        #endregion TheOrientation attached Property
        
    }
}
