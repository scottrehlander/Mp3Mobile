using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediaMobile.MediaLibrary;

namespace MediaMobile
{
    public partial class PlaylistEditorTest : Form
    {
        MediaLibrary.MediaLibrary medLib;

        public PlaylistEditorTest(MediaLibrary.MediaLibrary mediaLibrary)
        {
            InitializeComponent();

            medLib = mediaLibrary;
            UpdatePlaylists();
            if (lstExistingPlaylists.Items.Count > 0)
                lstExistingPlaylists.SelectedIndex = 0;
        }

        private void createNewPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            medLib.AddPlaylist();
            UpdatePlaylists();
        }

        private void UpdatePlaylists()
        {
            lstExistingPlaylists.Items.Clear();
            foreach (MediaPlaylist playlist in medLib.Playlists)
                lstExistingPlaylists.Items.Add(playlist.Name);
        }

        private void renamePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstExistingPlaylists.SelectedItems.Count == 0) return;

            OptionsFormTest options = new OptionsFormTest(OptionsFormTest.FormType.Rename);
            if (options.ShowDialog() != DialogResult.OK) return;

            foreach (MediaPlaylist playlist in medLib.Playlists)
                if (playlist.Name.Equals(lstExistingPlaylists.SelectedItems[0].ToString()))
                {
                    playlist.Name = options.RenameString;
                    playlist.Save();
                    UpdatePlaylists();
                    break;
                }
        }

        private void UpdateMediaList(MediaPlaylist playlist)
        {
            tvMedia.Nodes.Clear();

            foreach (MediaObject mo in playlist.MediaList)
                tvMedia.Nodes.Add((mo as Mp3).TrackTitle);

            foreach (MediaPlaylist embeddedPlaylist in playlist.EmbeddedLists)
                CreateEmbeddedPlaylist(embeddedPlaylist, null);
        }


        private void CreateEmbeddedPlaylist(MediaPlaylist playlist, TreeNode embedNodeParent)
        {
            TreeNode embeddedNode = new TreeNode(playlist.Name);
            if (embedNodeParent == null)
                tvMedia.Nodes.Add(embeddedNode);
            else
                embedNodeParent.Nodes.Add(embeddedNode);

            foreach (MediaObject mo in playlist.MediaList)
                embeddedNode.Nodes.Add((mo as Mp3).TrackTitle);

            foreach (MediaPlaylist embeddedPlaylist in playlist.EmbeddedLists)
                CreateEmbeddedPlaylist(embeddedPlaylist, embeddedNode);
        }

        private void lstExistingPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExistingPlaylists.SelectedItems.Count == 0) return;

            foreach (MediaPlaylist playlist in medLib.Playlists)
                if (playlist.Name.Equals(lstExistingPlaylists.SelectedItems[0].ToString()))
                {
                    UpdateMediaList(playlist);
                    return;
                }
        }

        void lstExistingPlaylists_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (lstExistingPlaylists.SelectedItems.Count == 0) return;

            if (e.KeyData == Keys.Delete)
            {
                e.Handled = true;

                if (MessageBox.Show(this, "Are you sure you want to permanently delete this playlist?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
                
                // If we want to delete...
                foreach(MediaPlaylist playlist in medLib.Playlists)
                {
                    if(playlist.Name.Equals(lstExistingPlaylists.SelectedItems[0].ToString()))
                    {
                        medLib.RemovePlaylist(playlist.Filename);
                        UpdatePlaylists();
                        tvMedia.Nodes.Clear();
                        break;
                    }
                }
            }
        }


        private void addMediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstExistingPlaylists.SelectedItems.Count == 0) return;

            foreach (MediaPlaylist playlist in medLib.Playlists)
            {
                if (playlist.Name.Equals(lstExistingPlaylists.SelectedItems[0].ToString()))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() == DialogResult.OK)
                        foreach (string media in ofd.FileNames)
                            if (MediaMobileUtils.LibraryUtilities.EndsWithKnownExtension(media))
                            {
                                playlist.AddMedia(new Mp3(media));
                                playlist.Save();
                                UpdateMediaList(playlist);
                            }
                    break;
                }
            }
        }
    }
}