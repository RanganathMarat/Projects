using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AttachingMultipleBehiorsToObjectSample
{
    public class ChangeWidthBehavior : IBehavior
    {
        public void Attach(FrameworkElement el)
        {
            el.MouseEnter += el_MouseEnter;
            el.MouseLeave += el_MouseLeave;
        }

        void el_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)sender;

            el.Width /= 2d;
        }

        void el_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            FrameworkElement el = (FrameworkElement)sender;

            el.Width *= 2d;
        }

        public void Detach(FrameworkElement el)
        {
            el.MouseLeave -= el_MouseLeave;
            el.MouseEnter -= el_MouseEnter;
        }
    }
}
