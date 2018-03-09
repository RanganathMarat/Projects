using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AttachingMultipleBehiorsToObjectSample
{
    public static class AttachedProperties
    {
        #region TheBehaviors attached Property
        public static IEnumerable<IBehavior> GetTheBehaviors(DependencyObject obj)
        {
            return (IEnumerable<IBehavior>)obj.GetValue(TheBehaviorsProperty);
        }

        public static void SetTheBehaviors(DependencyObject obj, IEnumerable<IBehavior> value)
        {
            obj.SetValue(TheBehaviorsProperty, value);
        }

        public static readonly DependencyProperty TheBehaviorsProperty =
        DependencyProperty.RegisterAttached
        (
            "TheBehaviors",
            typeof(IEnumerable<IBehavior>),
            typeof(AttachedProperties),
            new PropertyMetadata(null, OnBehaviorsChanged)
        );

        private static void OnBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)d;

            IEnumerable<IBehavior> oldBehaviors = (IEnumerable<IBehavior>)e.OldValue;

            if (oldBehaviors != null)
            {
                foreach(IBehavior oldBehavior in oldBehaviors)
                {
                    oldBehavior.Detach(el);
                }
            }

            IEnumerable<IBehavior> newBehaviors = (IEnumerable<IBehavior>)e.NewValue;

            if (newBehaviors != null)
            {
                foreach (IBehavior newBehavior in newBehaviors)
                {
                    newBehavior.Attach(el);
                }
            }
        }
        #endregion TheBehaviors attached Property

    }
}
