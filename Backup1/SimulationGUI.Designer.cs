namespace SerialLCDInterface
{
    partial class SimulationGUI
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
            this.lstDisplay = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstDisplay
            // 
            this.lstDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDisplay.BackColor = System.Drawing.Color.Black;
            this.lstDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDisplay.Enabled = false;
            this.lstDisplay.ForeColor = System.Drawing.Color.Lime;
            this.lstDisplay.FormattingEnabled = true;
            this.lstDisplay.Location = new System.Drawing.Point(0, 7);
            this.lstDisplay.Name = "lstDisplay";
            this.lstDisplay.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstDisplay.Size = new System.Drawing.Size(132, 65);
            this.lstDisplay.TabIndex = 0;
            this.lstDisplay.SelectedIndexChanged += new System.EventHandler(this.lstDisplay_SelectedIndexChanged);
            // 
            // SimulationGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(133, 78);
            this.Controls.Add(this.lstDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(200, 0);
            this.Name = "SimulationGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SimulationGUI";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox lstDisplay;

    }
}