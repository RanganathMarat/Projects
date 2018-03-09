using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Common.Controls
{
    /// <summary>
    /// MethodInvokedEventArgs
    /// </summary>
    public class MethodInvokedEventArgs : EventArgs
    {
        /// <summary>
        /// The method name
        /// </summary>
        public string MethodName;
        /// <summary>
        /// The method full name
        /// </summary>
        public string MethodFullName;
    }

    /// <summary>
    /// MethodsControl
    /// </summary>
    public partial class MethodsControl : UserControl
    {
        /// <summary>
        /// Occurs when [item invoked].
        /// </summary>
        public event EventHandler<MethodInvokedEventArgs> MethodInvoked;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodsControl"/> class.
        /// </summary>
        public MethodsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Loads the methods.
        /// </summary>
        public void LoadMethods()
        {
            var methodsDb = new DirectoryInfo(Global.Database.MethodsDb);
            if (!methodsDb.Exists) methodsDb.Create();

            IEnumerable<string> methods = from file in methodsDb.GetFiles("*" + Global.Database.MethodsExt)
                                          select file.Name;

            lstMethods.Items.Clear();
            lstMethods.Items.AddRange(methods.ToArray());
        }

        /// <summary>
        /// Deletes the selected item.
        /// </summary>
        public void DeleteSelectedItem()
        {
            if (lstMethods.SelectedItem == null) return;

            File.Delete((Path.Combine(Global.Database.MethodsDb, lstMethods.SelectedItem.ToString())));
            LoadMethods();
        }

        private void lstMethods_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var callback = MethodInvoked;

            if (callback != null)
            {
                callback.Invoke(this,
                                new MethodInvokedEventArgs
                                    {
                                        MethodName = lstMethods.SelectedItem.ToString(),
                                        MethodFullName =
                                            Path.Combine(Global.Database.MethodsDb, lstMethods.SelectedItem.ToString())
                                    });
            }
        }
    }
}