namespace HostWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hostingForm1 = new HostWinForms.HostingForm();
            this.SuspendLayout();
            // 
            // hostingForm1
            // 
            this.hostingForm1.Customers = null;
            this.hostingForm1.Location = new System.Drawing.Point(82, 76);
            this.hostingForm1.Name = "hostingForm1";
            this.hostingForm1.SelectedIndex = -1;
            this.hostingForm1.Size = new System.Drawing.Size(200, 100);
            this.hostingForm1.TabIndex = 0;
            this.hostingForm1.Text = "hostingForm1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 251);
            this.Controls.Add(this.hostingForm1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private WPFCustomControl.ComboboxWithGrid hostedComponent1;
        private HostingForm hostingForm1;


    }
}

