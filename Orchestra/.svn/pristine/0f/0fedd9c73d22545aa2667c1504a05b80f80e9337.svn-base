using System;
using System.Windows.Forms;
using Common.Messages;
using Common.Messaging;
using IManageApp;

namespace Common.Controls
{
    /// <summary>
    /// AppForm
    /// </summary>
    public class AppForm : Form
    {
        private readonly Oporational.AppType _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppForm"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AppForm(Oporational.AppType type)
        {
            _type = type;
        }

        /// <summary>
        /// Consumes the specified object.
        /// </summary>
        /// <param name="message">The object.</param>
        private void Consume(Oporational.Shutdown message)
        {
            CloseApp();
        }

        /// <summary>
        ///     Gets or sets the callback.
        /// </summary>
        /// <value>
        ///     The callback.
        /// </value>
        public IManageAppCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the service bus.
        /// </summary>
        /// <value>
        /// The service bus.
        /// </value>
        protected IPublish OutMessage { get; set; }

        /// <summary>
        /// Gets or sets the in message.
        /// </summary>
        /// <value>
        /// The in message.
        /// </value>
        protected IConsume InMessage { get; set; }

        /// <summary>
        /// Handles the Click event of the btnCloseAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCloseAll_Click(object sender, EventArgs e)
        {
            if (Callback == null)
            {
                Close();
                return;
            }

            Callback.CloseAll();
        }

        /// <summary>
        /// Checks the equ engine available.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        protected bool CheckAvailable(Oporational.AppType app)
        {
            return Callback == null || Callback.Ping(app);
        }

        /// <summary>
        /// Handles the Closed event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Form_Closed(object sender, EventArgs e)
        {
            if (Callback == null) return;
            Callback.AppClosed(_type);
        }

        /// <summary>
        /// Handles the Loaded event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void Form_Loaded(object sender, EventArgs e)
        {
            if (InMessage != null)
            {
                InMessage.AddBinding("Orchestra_Oporational_Out", string.Empty);
                InMessage.RegisterHandler<Oporational.Shutdown>(Consume);
            }

            if (Callback == null) return;
            Callback.AppStarted(_type);
        }

        /// <summary>
        /// Handles the Click event of the btnCrashTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected void btnCrashTest_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Brings to view.
        /// </summary>
        public void BringToView()
        {
            if (InvokeRequired)
            {
                Action callback = BringToFront;
                Invoke(callback);
                return;
            }

            BringToFront();
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        public void CloseApp()
        {
            if (InvokeRequired)
            {
                Action callback = Close;
                Invoke(callback);
            }

            Close();
        }
    }
}
