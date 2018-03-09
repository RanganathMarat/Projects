using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMPattternDemo
{
    class MVVMButton:Button
    {
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (Command != null)
            {
                if (Command.CanExecute(e))
                {
                    Command.Execute(e);
                }
            }

        }
    }
}
