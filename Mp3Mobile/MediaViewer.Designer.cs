namespace MediaMobile
{
    partial class MediaViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlViewer
            // 
            this.pnlViewer.AutoScroll = true;
            this.pnlViewer.BackColor = System.Drawing.Color.Black;
            this.pnlViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(0, 0);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(279, 147);
            this.pnlViewer.TabIndex = 0;
            // 
            // MediaViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlViewer);
            this.Name = "MediaViewer";
            this.Size = new System.Drawing.Size(279, 147);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewer;
    }
}
