namespace MediaMobile
{
    partial class PlaylistEditorTest
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
            this.components = new System.ComponentModel.Container();
            this.lstExistingPlaylists = new System.Windows.Forms.ListBox();
            this.mnuPlaylistOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createNewPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renamePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvMedia = new System.Windows.Forms.TreeView();
            this.mnuManagePlaylist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addMediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlaylistOptions.SuspendLayout();
            this.mnuManagePlaylist.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstExistingPlaylists
            // 
            this.lstExistingPlaylists.ContextMenuStrip = this.mnuPlaylistOptions;
            this.lstExistingPlaylists.FormattingEnabled = true;
            this.lstExistingPlaylists.Location = new System.Drawing.Point(12, 12);
            this.lstExistingPlaylists.Name = "lstExistingPlaylists";
            this.lstExistingPlaylists.Size = new System.Drawing.Size(132, 264);
            this.lstExistingPlaylists.TabIndex = 0;
            this.lstExistingPlaylists.SelectedIndexChanged += new System.EventHandler(this.lstExistingPlaylists_SelectedIndexChanged);
            this.lstExistingPlaylists.KeyDown += new System.Windows.Forms.KeyEventHandler(lstExistingPlaylists_KeyDown);
            // 
            // mnuPlaylistOptions
            // 
            this.mnuPlaylistOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewPlaylistToolStripMenuItem,
            this.renamePlaylistToolStripMenuItem});
            this.mnuPlaylistOptions.Name = "mnuPlaylistOptions";
            this.mnuPlaylistOptions.Size = new System.Drawing.Size(185, 48);
            // 
            // createNewPlaylistToolStripMenuItem
            // 
            this.createNewPlaylistToolStripMenuItem.Name = "createNewPlaylistToolStripMenuItem";
            this.createNewPlaylistToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.createNewPlaylistToolStripMenuItem.Text = "Create New Playlist...";
            this.createNewPlaylistToolStripMenuItem.Click += new System.EventHandler(this.createNewPlaylistToolStripMenuItem_Click);
            // 
            // renamePlaylistToolStripMenuItem
            // 
            this.renamePlaylistToolStripMenuItem.Name = "renamePlaylistToolStripMenuItem";
            this.renamePlaylistToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.renamePlaylistToolStripMenuItem.Text = "Rename Playlist...";
            this.renamePlaylistToolStripMenuItem.Click += new System.EventHandler(this.renamePlaylistToolStripMenuItem_Click);
            // 
            // tvMedia
            // 
            this.tvMedia.ContextMenuStrip = this.mnuManagePlaylist;
            this.tvMedia.Location = new System.Drawing.Point(150, 12);
            this.tvMedia.Name = "tvMedia";
            this.tvMedia.Size = new System.Drawing.Size(538, 264);
            this.tvMedia.TabIndex = 1;
            // 
            // mnuManagePlaylist
            // 
            this.mnuManagePlaylist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMediaToolStripMenuItem});
            this.mnuManagePlaylist.Name = "mnuManagePlaylist";
            this.mnuManagePlaylist.Size = new System.Drawing.Size(153, 48);
            // 
            // addMediaToolStripMenuItem
            // 
            this.addMediaToolStripMenuItem.Name = "addMediaToolStripMenuItem";
            this.addMediaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addMediaToolStripMenuItem.Text = "Add Media...";
            this.addMediaToolStripMenuItem.Click += new System.EventHandler(this.addMediaToolStripMenuItem_Click);
            // 
            // PlaylistEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 286);
            this.Controls.Add(this.tvMedia);
            this.Controls.Add(this.lstExistingPlaylists);
            this.Name = "PlaylistEditor";
            this.Text = "PlaylistEditor";
            this.mnuPlaylistOptions.ResumeLayout(false);
            this.mnuManagePlaylist.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstExistingPlaylists;
        private System.Windows.Forms.TreeView tvMedia;
        private System.Windows.Forms.ContextMenuStrip mnuPlaylistOptions;
        private System.Windows.Forms.ToolStripMenuItem createNewPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renamePlaylistToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnuManagePlaylist;
        private System.Windows.Forms.ToolStripMenuItem addMediaToolStripMenuItem;
    }
}