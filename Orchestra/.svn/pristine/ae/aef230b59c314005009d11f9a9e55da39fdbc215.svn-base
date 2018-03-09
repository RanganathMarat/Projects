using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Common.Controls;
using Common.Messages;
using Common.Messaging;

namespace ExecuteMethodsApp
{
    /// <summary>
    ///     Execute
    /// </summary>
    public partial class Execute : AppForm
    {
        private string _selectedMethodFullName;
        private string _selectedMethodName;
        private IConsume _consumeData;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Execute" /> class.
        /// </summary>
        public Execute() :base(Oporational.AppType.Execute)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            Load += Form_Loaded;
            Closed += Form_Closed;

            methodsControl.MethodInvoked += methodsControl_MethodInvoked;
            methodsControl.LoadMethods();
        }

        /// <summary>
        /// Handles the Loaded event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void Form_Loaded(object sender, EventArgs e)
        {
            InMessage = Messaging.Proxy("InMessage").Consumer;
            InMessage.RegisterHandler<Execution.LoadResult>(Consume);
            InMessage.RegisterHandler<Execution.Started>(Consume);
            InMessage.RegisterHandler<Execution.Ended>(Consume);

            OutMessage = Messaging.Proxy("OutMessage").Publisher;

            _consumeData = Messaging.Proxy("ConsumeData").Consumer;
            _consumeData.RegisterHandler<Execution.DataPoint>(Consume);

            base.Form_Loaded(sender, e);

            InMessage.DeQueue(true);
            _consumeData.DeQueue(true);
        }

        /// <summary>
        ///     Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.DataPoint message)
        {
            if (InvokeRequired)
            {
                Action callback = () => Consume(message);
                Invoke(callback);
                return;
            }

            chart1.HandleDataPoint(message);
        }

        /// <summary>
        ///     Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.Ended message)
        {
            if (InvokeRequired)
            {
                Action callback = () => Consume(message);
                Invoke(callback);
                return;
            }

            WriteStatusMessage(message.Message, message.Error);
        }

        /// <summary>
        ///     Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.LoadResult message)
        {
            if (InvokeRequired)
            {
                Action callback = () => Consume(message);
                Invoke(callback);
                return;
            }

            WriteStatusMessage(message.Result, message.Error);
        }

        /// <summary>
        ///     Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.Started message)
        {
            if (InvokeRequired)
            {
                Action callback = () => Consume(message);
                Invoke(callback);
                return;
            }

            WriteStatusMessage(message.MethodName + " Started...", false);
        }

        private void methodsControl_MethodInvoked(object sender, MethodInvokedEventArgs e)
        {
            _selectedMethodName = e.MethodName;
            _selectedMethodFullName = e.MethodFullName;

            string content = File.ReadAllText(_selectedMethodFullName);

            txtEditor.Text = content;
        }

        private void btnMethods_Click(object sender, EventArgs e)
        {
            if (Callback == null) return;
            Callback.StartApplication(Oporational.AppType.Methods);
        }

        private void btnAnalize_Click(object sender, EventArgs e)
        {
            if (Callback == null) return;
            Callback.StartApplication(Oporational.AppType.Results);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMethodName))
            {
                WriteMsg("Select a method...", Color.Red);
                return;
            }

            if (CheckAvailable(Oporational.AppType.EquEngine))
            {
                chart1.ClearGraph();
                OutMessage.Publish(new Execution.StartExecution
                    {
                        MethodName = _selectedMethodName
                    });
            }
            else
            {
                WriteStatusMessage("EquEngine not avilable or not responding", true);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMethodName))
            {
                WriteMsg("Select a method...", Color.Red);
                return;
            }

            if (CheckAvailable(Oporational.AppType.EquEngine))
            {
                OutMessage.Publish(new Execution.LoadMethod
                    {
                        FullName = _selectedMethodFullName,
                        MethodName = _selectedMethodName
                    });
            }
            else
            {
                WriteStatusMessage("EquEngine not avilable or not responding", true);
            }
        }

        private void WriteStatusMessage(string message, bool error)
        {
            WriteMsg(message, error ? Color.Red : Color.White);
        }

        private void WriteMsg(string message, Color color)
        {
            var selectionstart = rtxbStatus.Text.Length;
            rtxbStatus.AppendText(Environment.NewLine + message);
            var selectionLength = rtxbStatus.Text.Length - selectionstart;
            rtxbStatus.Select(selectionstart, selectionLength);
            rtxbStatus.SelectionColor = color;
            rtxbStatus.Select(rtxbStatus.Text.Length, 0);
        }
    }
}