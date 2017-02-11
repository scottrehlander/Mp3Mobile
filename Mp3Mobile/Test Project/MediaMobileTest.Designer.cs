namespace MediaMobile
{
    partial class MediaMobileTest
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.playbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shuffleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.lstMedia = new System.Windows.Forms.ListBox();
            this.lblPlayState = new System.Windows.Forms.Label();
            this.playTimeElapsed = new System.Windows.Forms.Label();
            this.btnRefreshScreen = new System.Windows.Forms.Button();
            this.cmbArtists = new System.Windows.Forms.ComboBox();
            this.cmbListStyle = new System.Windows.Forms.ComboBox();
            this.lblExit = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblMin = new System.Windows.Forms.Label();
            this.formMover1 = new MediaMobile.FormMover();
            this.btnPlaylistEditor = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playbackToolStripMenuItem,
            this.managementToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // playbackToolStripMenuItem
            // 
            this.playbackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shuffleToolStripMenuItem});
            this.playbackToolStripMenuItem.ForeColor = System.Drawing.Color.LimeGreen;
            this.playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
            this.playbackToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.playbackToolStripMenuItem.Text = "Playback";
            // 
            // shuffleToolStripMenuItem
            // 
            this.shuffleToolStripMenuItem.Checked = true;
            this.shuffleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shuffleToolStripMenuItem.Name = "shuffleToolStripMenuItem";
            this.shuffleToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.shuffleToolStripMenuItem.Text = "Shuffle";
            this.shuffleToolStripMenuItem.Click += new System.EventHandler(this.shuffleToolStripMenuItem_Click);
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playlistEditorToolStripMenuItem});
            this.managementToolStripMenuItem.ForeColor = System.Drawing.Color.LimeGreen;
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.managementToolStripMenuItem.Text = "Management";
            // 
            // playlistEditorToolStripMenuItem
            // 
            this.playlistEditorToolStripMenuItem.Name = "playlistEditorToolStripMenuItem";
            this.playlistEditorToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.playlistEditorToolStripMenuItem.Text = "Playlist Editor...";
            this.playlistEditorToolStripMenuItem.Click += new System.EventHandler(this.playlistEditorToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.ForeColor = System.Drawing.Color.LimeGreen;
            this.button1.Location = new System.Drawing.Point(544, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "test library";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstMedia
            // 
            this.lstMedia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMedia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstMedia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstMedia.FormattingEnabled = true;
            this.lstMedia.Location = new System.Drawing.Point(10, 64);
            this.lstMedia.Name = "lstMedia";
            this.lstMedia.Size = new System.Drawing.Size(609, 173);
            this.lstMedia.TabIndex = 1;
            this.lstMedia.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBox_DrawItem);
            this.lstMedia.DoubleClick += new System.EventHandler(this.lstMedia_DoubleClick);
            // 
            // lblPlayState
            // 
            this.lblPlayState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPlayState.AutoSize = true;
            this.lblPlayState.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblPlayState.Location = new System.Drawing.Point(10, 252);
            this.lblPlayState.Name = "lblPlayState";
            this.lblPlayState.Size = new System.Drawing.Size(47, 13);
            this.lblPlayState.TabIndex = 2;
            this.lblPlayState.Text = "Stopped";
            // 
            // playTimeElapsed
            // 
            this.playTimeElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playTimeElapsed.AutoSize = true;
            this.playTimeElapsed.ForeColor = System.Drawing.Color.LimeGreen;
            this.playTimeElapsed.Location = new System.Drawing.Point(11, 268);
            this.playTimeElapsed.Name = "playTimeElapsed";
            this.playTimeElapsed.Size = new System.Drawing.Size(34, 13);
            this.playTimeElapsed.TabIndex = 3;
            this.playTimeElapsed.Text = "00:00";
            // 
            // btnRefreshScreen
            // 
            this.btnRefreshScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshScreen.BackColor = System.Drawing.Color.Black;
            this.btnRefreshScreen.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnRefreshScreen.Location = new System.Drawing.Point(430, 275);
            this.btnRefreshScreen.Name = "btnRefreshScreen";
            this.btnRefreshScreen.Size = new System.Drawing.Size(108, 23);
            this.btnRefreshScreen.TabIndex = 4;
            this.btnRefreshScreen.Text = "Refresh Screen";
            this.btnRefreshScreen.UseVisualStyleBackColor = false;
            this.btnRefreshScreen.Click += new System.EventHandler(this.btnRefreshScreen_Click);
            // 
            // cmbArtists
            // 
            this.cmbArtists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbArtists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArtists.FormattingEnabled = true;
            this.cmbArtists.Location = new System.Drawing.Point(91, 33);
            this.cmbArtists.Name = "cmbArtists";
            this.cmbArtists.Size = new System.Drawing.Size(528, 21);
            this.cmbArtists.TabIndex = 5;
            this.cmbArtists.SelectedIndexChanged += new System.EventHandler(this.cmbArtists_SelectedIndexChanged);
            // 
            // cmbListStyle
            // 
            this.cmbListStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListStyle.FormattingEnabled = true;
            this.cmbListStyle.Items.AddRange(new object[] {
            "Library",
            "Playlist"});
            this.cmbListStyle.Location = new System.Drawing.Point(10, 33);
            this.cmbListStyle.Name = "cmbListStyle";
            this.cmbListStyle.Size = new System.Drawing.Size(75, 21);
            this.cmbListStyle.TabIndex = 8;
            this.cmbListStyle.SelectedIndexChanged += new System.EventHandler(this.cmbListStyle_SelectedIndexChanged);
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.BackColor = System.Drawing.Color.Transparent;
            this.lblExit.CausesValidation = false;
            this.lblExit.ForeColor = System.Drawing.Color.White;
            this.lblExit.Location = new System.Drawing.Point(607, 0);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(12, 13);
            this.lblExit.TabIndex = 10;
            this.lblExit.Text = "x";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnPlaylistEditor);
            this.pnlMain.Controls.Add(this.lblMin);
            this.pnlMain.Controls.Add(this.formMover1);
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Controls.Add(this.lblExit);
            this.pnlMain.Controls.Add(this.lstMedia);
            this.pnlMain.Controls.Add(this.cmbListStyle);
            this.pnlMain.Controls.Add(this.lblPlayState);
            this.pnlMain.Controls.Add(this.cmbArtists);
            this.pnlMain.Controls.Add(this.playTimeElapsed);
            this.pnlMain.Controls.Add(this.btnRefreshScreen);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(633, 313);
            this.pnlMain.TabIndex = 14;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.BackColor = System.Drawing.Color.Transparent;
            this.lblMin.CausesValidation = false;
            this.lblMin.ForeColor = System.Drawing.Color.White;
            this.lblMin.Location = new System.Drawing.Point(591, -1);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(13, 13);
            this.lblMin.TabIndex = 14;
            this.lblMin.Text = "_";
            this.lblMin.Click += new System.EventHandler(this.lblMin_Click);
            // 
            // formMover1
            // 
            this.formMover1.BackColor = System.Drawing.Color.Black;
            this.formMover1.Location = new System.Drawing.Point(0, 0);
            this.formMover1.Name = "formMover1";
            this.formMover1.Size = new System.Drawing.Size(593, 17);
            this.formMover1.TabIndex = 13;
            // 
            // btnPlaylistEditor
            // 
            this.btnPlaylistEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaylistEditor.BackColor = System.Drawing.Color.Black;
            this.btnPlaylistEditor.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnPlaylistEditor.Location = new System.Drawing.Point(316, 275);
            this.btnPlaylistEditor.Name = "btnPlaylistEditor";
            this.btnPlaylistEditor.Size = new System.Drawing.Size(108, 23);
            this.btnPlaylistEditor.TabIndex = 15;
            this.btnPlaylistEditor.Text = "Playlist Editor";
            this.btnPlaylistEditor.UseVisualStyleBackColor = false;
            this.btnPlaylistEditor.Click += new System.EventHandler(this.btnPlaylistEditor_Click);
            // 
            // MediaMobileTest
            // 
            this.AccessibleDescription = "5";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(633, 313);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(5)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MediaMobileTest";
            this.Text = "Mp3Mobile";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MediaMobile_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void MediaMobile_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            mp3.ClearScreen();
            mp3.SetBacklightBrightness(0);
            System.Environment.Exit(0);
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shuffleToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstMedia;
        private System.Windows.Forms.Label lblPlayState;
        private System.Windows.Forms.Label playTimeElapsed;
        private System.Windows.Forms.Button btnRefreshScreen;
        private System.Windows.Forms.ComboBox cmbArtists;
        private System.Windows.Forms.ComboBox cmbListStyle;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistEditorToolStripMenuItem;
        private System.Windows.Forms.Label lblExit;
        private FormMover formMover1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Button btnPlaylistEditor;


    }
}