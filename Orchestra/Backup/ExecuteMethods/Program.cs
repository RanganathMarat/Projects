using System;
using System.Windows.Forms;
using Common.Controls;

namespace ExecuteMethodsApp
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Execute());
        }
    }

    /// <summary>
    ///     App
    /// </summary>
    public class App : AppBase
    {
        #region IManageApplication Members

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public override void Start()
        {
            try
            {
                if (MainForm == null)
                {
                    SetVisualStyle();
                    MainForm = new Execute {Callback = Callback};

                    Application.Run(MainForm);
                }
                else
                {
                    MainForm.BringToView();
                }
            }
            catch (Exception)
            {
                CloseApp();
            }
        }

        #endregion
    }
}