using System;
using System.Windows.Forms;
using Common.Controls;
using Common.Messages;
using Common.Messaging;

namespace AnalizeResultsApp
{
    /// <summary>
    ///     Analize
    /// </summary>
    public partial class Analize : AppForm
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Analize" /> class.
        /// </summary>
        public Analize() : base(Oporational.AppType.Results)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            Load += Form_Loaded;
            Closed += Form_Closed;
        }

        /// <summary>
        /// Handles the Loaded event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void Form_Loaded(object sender, EventArgs e)
        {
            InMessage = Messaging.Proxy("InMessage").Consumer;
            base.Form_Loaded(sender, e);
            InMessage.DeQueue(true);
        }

        private void btnMethods_Click(object sender, EventArgs e)
        {
            if (Callback == null) return;

            Callback.StartApplication(Oporational.AppType.Methods);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (Callback == null) return;

            Callback.StartApplication(Oporational.AppType.EquEngine);
        }
    }
}