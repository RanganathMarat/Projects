using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BehaviorSample
{
    public static class AttachedProperties
    {

        #region TheBehavior attached Property
        public static IBehavior GetTheBehavior(DependencyObject obj)
        {
            return (IBehavior)obj.GetValue(TheBehaviorProperty);
        }

        public static void SetTheBehavior(DependencyObject obj, IBehavior value)
        {
            obj.SetValue(TheBehaviorProperty, value);
        }

        public static readonly DependencyProperty TheBehaviorProperty =
        DependencyProperty.RegisterAttached
        (
            "TheBehavior",
            typeof(IBehavior),
            typeof(AttachedProperties),
            new PropertyMetadata(null, OnTheBehaviorChanged)
        );

        private static void OnTheBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)d;

            IBehavior oldBehavior = (IBehavior) e.OldValue;

            if (oldBehavior != null)
                oldBehavior.Detach(el);

            IBehavior newBehavior = (IBehavior) e.NewValue;

            if (newBehavior != null)
                newBehavior.Attach(el);
        }
        #endregion TheBehavior attached Property
    }
}
