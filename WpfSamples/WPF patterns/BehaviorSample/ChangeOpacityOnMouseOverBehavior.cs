using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BehaviorSample
{
    public class ChangeOpacityOnMouseOverBehavior : IBehavior
    {
        public void Attach(FrameworkElement el)
        {
            el.MouseEnter += el_MouseEnter;
            el.MouseLeave += el_MouseLeave;
        }

        void el_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)sender;

            el.Opacity = 1;
        }

        void el_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)sender;

            el.Opacity = 0.5;
        }

        public void Detach(FrameworkElement el)
        {
            el.MouseLeave -= el_MouseLeave;
            el.MouseEnter -= el_MouseEnter;
        }
    }
}
