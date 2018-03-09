using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common;
using Common.Controls;
using Common.Messages;
using Common.Messaging;

namespace ManageMethodsApp
{
    /// <summary>
    ///     Methods
    /// </summary>
    public partial class Methods : AppForm
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Methods" /> class.
        /// </summary>
        public Methods() :base(Oporational.AppType.Methods)
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
            base.Form_Loaded(sender, e);
            InMessage.DeQueue(true);
        }

        private void btnAnalize_Click(object sender, EventArgs e)
        {
            if (Callback == null) return;

            Callback.StartApplication(Oporational.AppType.Results);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (Callback == null) return;

            Callback.StartApplication(Oporational.AppType.Methods);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtEditor.Text = Global.Database.SampleMethod;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtEditor.ReadOnly = true;

                string[] lines = txtEditor.Lines;

                string fileName = GetFileName(lines);

                string fullName = Path.Combine(Global.Database.MethodsDb, fileName);

                File.WriteAllLines(fullName + Global.Database.MethodsExt, lines);

                methodsControl.LoadMethods();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                txtEditor.ReadOnly = false;
            }
        }

        private static string GetFileName(IEnumerable<string> lines)
        {
            string filename = (from txt in lines
                               where txt.Contains(Global.Database.FileNamTag)
                               select txt).Single();

            filename = filename.Split('=')[1].Trim();

            if (string.IsNullOrEmpty(filename)) throw new Exception("Give a valid filename!");

            return filename;
        }

        private void txtEditor_TextChanged(object sender, EventArgs e)
        {
            Compile();
        }

        private void Compile()
        {
            Action callback = () => UpdateCompileResult(Global.Instructions.Compile(txtEditor.Lines));

            callback.BeginInvoke(null, null);
        }

        private void UpdateCompileResult(string result)
        {
            if (InvokeRequired)
            {
                Action callback = () => UpdateCompileResult(result);
                Invoke(callback);
                return;
            }

            txtResult.Text = result;
            btnSave.Enabled = string.IsNullOrEmpty(result);
        }

        private void methodsControl_MethodInvoked(object sender, MethodInvokedEventArgs e)
        {
            string content = File.ReadAllText(e.MethodFullName);

            txtEditor.Text = content;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            methodsControl.DeleteSelectedItem();
        }
    }
}