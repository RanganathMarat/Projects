using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Windows.Forms;
using IManageApp;

namespace Common.Controls
{
    /// <summary>
    ///     AppBase
    /// </summary>
    public class AppBase : MarshalByRefObject, IManageApplication, ISponsor
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppBase" /> class.
        /// </summary>
        public AppBase()
        {
            Application.ThreadException += Application_ThreadException;
        }

        /// <summary>
        /// Gets or sets the main form.
        /// </summary>
        /// <value>
        /// The main form.
        /// </value>
        protected AppForm MainForm { get; set; }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>
        /// The callback.
        /// </value>
        protected IManageAppCallback Callback { get; set; }

        /// <summary>
        /// Initializes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public virtual void Initialize(IManageAppCallback callback)
        {
            Callback = callback;
            ((ILease) RemotingServices.GetLifetimeService(this)).Register((ISponsor) Callback);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public void Close()
        {
            CloseApp();
        }

        /// <summary>
        ///     Pings this instance.
        /// </summary>
        /// <returns></returns>
        public bool Ping()
        {
            return true;
        }

        /// <summary>
        /// Requests a sponsoring client to renew the lease for the specified object.
        /// </summary>
        /// <param name="lease">The lifetime lease of the object that requires lease renewal.</param>
        /// <returns>
        /// The additional lease time for the specified object.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public TimeSpan Renewal(ILease lease)
        {
            Debug.Assert(lease.CurrentState == LeaseState.Active);

            //Renew lease by 5 minutes
            return TimeSpan.FromMinutes(5);
        }

        /// <summary>
        /// Handles the ThreadException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ThreadExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CloseApp();
        }

        /// <summary>
        /// Sets the visual style.
        /// </summary>
        protected static void SetVisualStyle()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
            {
            }
            // ReSharper restore EmptyGeneralCatchClause
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        protected void CloseApp()
        {
            if (MainForm == null) return;
            MainForm.CloseApp();
            MainForm = null;
        }
    }
}