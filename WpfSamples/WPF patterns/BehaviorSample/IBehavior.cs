using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BehaviorSample
{
    public interface IBehavior
    {
        void Attach(FrameworkElement el);

        void Detach(FrameworkElement el);
    }
}
