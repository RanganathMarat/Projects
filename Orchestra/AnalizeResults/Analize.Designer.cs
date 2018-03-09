using IManageApp;
using Common.Messaging;

namespace AnalizeResultsApp
{
    partial class Analize
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
            if (InMessage != null) InMessage.Dispose();
            InMessage = null;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMenue = new System.Windows.Forms.Panel();
            this.btnCloseAll = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnMethods = new System.Windows.Forms.Button();
            this.pnlMainPanel = new System.Windows.Forms.Panel();
            this.btnCrashTest = new System.Windows.Forms.Button();
            this.pnlMenue.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenue
            // 
            this.pnlMenue.Controls.Add(this.btnCrashTest);
            this.pnlMenue.Controls.Add(this.btnCloseAll);
            this.pnlMenue.Controls.Add(this.btnOpen);
            this.pnlMenue.Controls.Add(this.btnExecute);
            this.pnlMenue.Controls.Add(this.btnMethods);
            this.pnlMenue.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMenue.Location = new System.Drawing.Point(0, 0);
            this.pnlMenue.Name = "pnlMenue";
            this.pnlMenue.Size = new System.Drawing.Size(1049, 28);
            this.pnlMenue.TabIndex = 1;
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseAll.Location = new System.Drawing.Point(971, 3);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAll.TabIndex = 9;
            this.btnCloseAll.Text = "Close All";
            this.btnCloseAll.UseVisualStyleBackColor = true;
            this.btnCloseAll.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Location = new System.Drawing.Point(184, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Location = new System.Drawing.Point(52, 3);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(43, 23);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Ex";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnMethods
            // 
            this.btnMethods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMethods.Location = new System.Drawing.Point(3, 3);
            this.btnMethods.Name = "btnMethods";
            this.btnMethods.Size = new System.Drawing.Size(43, 23);
            this.btnMethods.TabIndex = 1;
            this.btnMethods.Text = "Me";
            this.btnMethods.UseVisualStyleBackColor = true;
            this.btnMethods.Click += new System.EventHandler(this.btnMethods_Click);
            // 
            // pnlMainPanel
            // 
            this.pnlMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainPanel.Location = new System.Drawing.Point(0, 28);
            this.pnlMainPanel.Name = "pnlMainPanel";
            this.pnlMainPanel.Size = new System.Drawing.Size(1049, 590);
            this.pnlMainPanel.TabIndex = 2;
            // 
            // btnCrashTest
            // 
            this.btnCrashTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrashTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrashTest.Location = new System.Drawing.Point(890, 3);
            this.btnCrashTest.Name = "btnCrashTest";
            this.btnCrashTest.Size = new System.Drawing.Size(75, 23);
            this.btnCrashTest.TabIndex = 10;
            this.btnCrashTest.Text = "Crash Test";
            this.btnCrashTest.UseVisualStyleBackColor = true;
            this.btnCrashTest.Click += new System.EventHandler(this.btnCrashTest_Click);
            // 
            // Analize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1049, 618);
            this.Controls.Add(this.pnlMainPanel);
            this.Controls.Add(this.pnlMenue);
            this.Name = "Analize";
            this.Text = "Analize";
            this.pnlMenue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenue;
        private System.Windows.Forms.Panel pnlMainPanel;
        private System.Windows.Forms.Button btnMethods;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnCloseAll;
        private System.Windows.Forms.Button btnCrashTest;
    }
}

