namespace MediaMobile
{
    partial class OptionsFormTest
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
            this.pnlRename = new System.Windows.Forms.Panel();
            this.lblNEWPLAYLISTNAME = new System.Windows.Forms.Label();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.btnRenameOK = new System.Windows.Forms.Button();
            this.btnRenameCancel = new System.Windows.Forms.Button();
            this.pnlRename.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRename
            // 
            this.pnlRename.Controls.Add(this.btnRenameCancel);
            this.pnlRename.Controls.Add(this.btnRenameOK);
            this.pnlRename.Controls.Add(this.txtRename);
            this.pnlRename.Controls.Add(this.lblNEWPLAYLISTNAME);
            this.pnlRename.Location = new System.Drawing.Point(12, 12);
            this.pnlRename.Name = "pnlRename";
            this.pnlRename.Size = new System.Drawing.Size(361, 96);
            this.pnlRename.TabIndex = 0;
            // 
            // lblNEWPLAYLISTNAME
            // 
            this.lblNEWPLAYLISTNAME.AutoSize = true;
            this.lblNEWPLAYLISTNAME.Location = new System.Drawing.Point(14, 14);
            this.lblNEWPLAYLISTNAME.Name = "lblNEWPLAYLISTNAME";
            this.lblNEWPLAYLISTNAME.Size = new System.Drawing.Size(98, 13);
            this.lblNEWPLAYLISTNAME.TabIndex = 0;
            this.lblNEWPLAYLISTNAME.Text = "New Playlist Name:";
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(118, 14);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(225, 20);
            this.txtRename.TabIndex = 1;
            // 
            // btnRenameOK
            // 
            this.btnRenameOK.Location = new System.Drawing.Point(80, 52);
            this.btnRenameOK.Name = "btnRenameOK";
            this.btnRenameOK.Size = new System.Drawing.Size(75, 23);
            this.btnRenameOK.TabIndex = 2;
            this.btnRenameOK.Text = "OK";
            this.btnRenameOK.UseVisualStyleBackColor = true;
            this.btnRenameOK.Click += new System.EventHandler(this.btnRenameOK_Click);
            // 
            // btnRenameCancel
            // 
            this.btnRenameCancel.Location = new System.Drawing.Point(198, 52);
            this.btnRenameCancel.Name = "btnRenameCancel";
            this.btnRenameCancel.Size = new System.Drawing.Size(75, 23);
            this.btnRenameCancel.TabIndex = 3;
            this.btnRenameCancel.Text = "Cancel";
            this.btnRenameCancel.UseVisualStyleBackColor = true;
            this.btnRenameCancel.Click += new System.EventHandler(this.btnRenameCancel_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 419);
            this.Controls.Add(this.pnlRename);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.pnlRename.ResumeLayout(false);
            this.pnlRename.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRename;
        private System.Windows.Forms.Button btnRenameCancel;
        private System.Windows.Forms.Button btnRenameOK;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.Label lblNEWPLAYLISTNAME;
    }
}