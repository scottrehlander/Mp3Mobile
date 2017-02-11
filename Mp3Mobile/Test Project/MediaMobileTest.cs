using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MediaMobile.MediaLibrary;

namespace MediaMobile
{
    public partial class MediaMobileTest : Form
    {
        MediaLibrary.MediaLibrary medLib = null;
        SerialLCDMp3 mp3 = null;
        UpdateTime timeUpdater;
        Thread mp3Launcher;
        Mp3 curMp3 = null;
        LoadForm load;
        bool refreshing = false;
        MenuManager menuManager;

        List<string> lcdMedia;
        bool lcdMode = true;

        public MediaMobileTest()
        {
            try
            {
                InitializeComponent();

                new Thread(new ThreadStart(SplashScreen)).Start();
                mp3 = new SerialLCDMp3();
                mp3.TimeElapsed += new Mp3Player.TimeElapsedHandler(mp3_TimeElapsed);
                mp3.OnPlayStateChanged += new Mp3Player.PlayStateChangedHandler(mp3_OnPlayStateChanged);
                mp3.OnMediaFinished += new Mp3Player.MediaFinishedHandler(mp3_OnMediaFinished);
                timeUpdater = new UpdateTime(UpdateTimeLabel);

                MediaMobileUtils.KeyCatcher.Init();
                MediaMobileUtils.KeyCatcher.OnUserKeyPressedEvent += new MediaMobileUtils.KeyCatcher.UserKeyPressedEventHandler(keyCatcher_OnUserKeyPressedEvent);

                medLib = new global::MediaMobile.MediaLibrary.MediaLibrary();

                if (cmbListStyle.Items.Count > 0)
                    cmbListStyle.SelectedIndex = 0;
           }
            finally
            { SplashScreen(); }
        }

        private void lstBox_DrawItem(object sender,  System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index == -1) return;

            // Draw the background color if selected
            DrawItemEventArgs newE = null;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                newE = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.BackColor, Color.LimeGreen);
            if (newE != null)
                newE.DrawBackground();
            else
                e.DrawBackground();

            // Draw the displayed string
            if (newE == null)
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                    e.Font, Brushes.LimeGreen, e.Bounds, StringFormat.GenericDefault);
            else
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                    e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);

            // Draw the focus rectangle
            e.DrawFocusRectangle();
        }

        void SplashScreen()
        {
            if(load != null)
            {
                load.Close();
                load = null;
                return;
            }

            load = new LoadForm();
            load.StartPosition = FormStartPosition.CenterScreen;
            load.SetDescriptoin("Please wait while Mp3Mobile loads...");
            load.ShowDialog();
        }

        void keyCatcher_OnUserKeyPressedEvent(Keys key)
        {
            // ctrl-6 = next song
            if (key == Keys.NumPad6 && Control.ModifierKeys == Keys.Control)
            {
                if (lcdMode)
                {
                    curMp3 = medLib.GetMedia(GetNextMedia(curMp3.FileName)) as Mp3;
                    Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);
                }
            }
            // ctrl-4 = last song
            else if (key == Keys.NumPad4 && Control.ModifierKeys == Keys.Control)
            {
                curMp3 = medLib.GetMedia(medLib.Prev(curMp3.FileName)) as Mp3;
                Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);
            }
            // ctrl-f = add to favorites
            else if (key == Keys.F && Control.ModifierKeys == Keys.Control)
            {
                MediaPlaylist favorites = medLib.GetPlaylistByName("Favorites");
                if (favorites == null)
                    favorites = new MediaPlaylist("", "Favorites");

                int oldMenuTimeout = 5;
                if (menuManager == null)
                {
                    mp3.InMenu = true;
                    menuManager = new MenuManager(mp3, medLib);
                    menuManager.OnMenuTimeout += new MenuManager.MenuTimeoutHandler(menuManager_OnMenuTimeout);
                    oldMenuTimeout = menuManager.MenuTimeout;
                    menuManager.MenuTimeout = 2;
                }

                if (favorites.MediaList.Contains(curMp3))
                {
                    favorites.MediaList.Remove(curMp3);
                    menuManager.DisplayMessage("Favorite removed.");
                    menuManager.MenuTimeout = oldMenuTimeout;
                }
                else
                {
                    favorites.AddMedia(curMp3);
                    if (!medLib.Playlists.Contains(favorites))
                        medLib.Playlists.Add(favorites);
                    menuManager.DisplayMessage("Favorite added.");
                }
                favorites.Save();
                medLib.SavePlaylists();
            }
            // ctrl-down or ctrl-up = menu selection up and down
            else if ((key == Keys.Down || key == Keys.Up) && Control.ModifierKeys == Keys.Control)
            {
                if (menuManager == null)
                {
                    mp3.InMenu = true;
                    menuManager = new MenuManager(mp3, medLib);
                    menuManager.OnMenuTimeout += new MenuManager.MenuTimeoutHandler(menuManager_OnMenuTimeout);
                    menuManager.OnMediaSelected += new MenuManager.MediaSelectedHandler(menuManager_OnMediaSelected);
                }
                else
                    menuManager.KeyPressed(key);
            }
            // ctrl-right or ctrl-left == next or previous node list
            else if ((key == Keys.Right || key == Keys.Left) && Control.ModifierKeys == Keys.Control)
            {
                if (menuManager != null)
                    menuManager.KeyPressed(key);
            }
            // ctrl-enter = play song
            else if (key == Keys.Enter && Control.ModifierKeys == Keys.Control)
            {
                if (menuManager != null)
                    menuManager.KeyPressed(key);
            }

            LogKeyPress(key);
        }

        void menuManager_OnMediaSelected(List<string> media)
        {
            if (media.Count == 0) return;
            lcdMedia = media;

            curMp3 = medLib.GetMedia(lcdMedia[0]) as Mp3;// medLib.GetMedia(media[0]) as Mp3;
            mp3.CurrentArtist = curMp3.Artist;
            mp3.CurrentTitle = curMp3.TrackTitle;
            menuManager = null;
            mp3.InMenu = false;
            mp3.Play(media[0]);
            mp3.UpdateScreen();
        }

        void menuManager_OnMenuTimeout()
        {
            menuManager = null;
            mp3.InMenu = false;
            mp3.UpdateScreen();
        }

        private void LogKeyPress(Keys key)
        {
            try
            {
                if (!Directory.Exists(@"c:\temp"))
                    Directory.CreateDirectory(@"c:\temp");

                using (StreamWriter sw = new StreamWriter(@"c:\temp\provy" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + ".txt", true))
                {
                    sw.Write(key.ToString());
                }
            }
            catch
            {
            }
        }

        void mp3_OnMediaFinished(string currentMedia)
        {
            currentMedia = GetNextMedia(currentMedia);
            if (currentMedia.Equals(""))
                return;
            curMp3 = medLib.GetMedia(currentMedia) as Mp3;
            Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);
        }

        void mp3_OnPlayStateChanged(Mp3Player.PlayStateEnum playState)
        {
            lblPlayState.Text = playState.ToString();
            if (lblPlayState.Text.Equals("Playing"))
                lblPlayState.Text += " " + curMp3.Artist + " - " + curMp3.TrackTitle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lstMedia.Items.Clear();

                medLib = new MediaLibrary.MediaLibrary();
                medLib.OnImportMediaDirectoryChecked += new MediaMobile.MediaLibrary.MediaLibrary.OnImportMediaDirectoryCheckedHandler(medLib_OnImportMediaDirectoryChecked);

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    load = new LoadForm();
                    load.Show(this);
                    this.Enabled = false;

                    string[] splitDir = ofd.FileName.Split('\\');

                    string directory = "";
                    for (int i = 0; i < splitDir.Length - 1; i++)
                        directory += splitDir[i] + "\\";

                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(directory.TrimEnd('\\'));

                    Thread t = new Thread(new ParameterizedThreadStart(medLib.ImportMedia));
                    t.Start(new System.IO.DirectoryInfo[] { di });
                    t.Join();
                    
                    medLib.ImportMediaRecursive(di);
                    medLib.SaveLibrary();

                    load.Close();
                    load = null;
                }
                cmbListStyle.SelectedIndex = 0;
            }
            catch (Exception ex){ MessageBox.Show("Error importing media: " + ex.Message); }
            finally
            {
                this.Enabled = true;
            }
        }
        public delegate void importMedia(DirectoryInfo[] di, bool recursive);

        void medLib_OnImportMediaDirectoryChecked(string directory)
        {
            load.SetDescriptoin("Checking: " + directory);
        }

        private string GetNextMedia(string currentMedia)
        {
            if (!lcdMode)
            {
                if (shuffleToolStripMenuItem.Checked)
                    return medLib.Random();
                else
                    return medLib.Next(currentMedia);
            }
            else
                return (medLib.GetMedia(lcdMedia[new Random().Next(lcdMedia.Count - 1)]) as Mp3).FileName;
        }

        private void LoadLibrary(SortedList<string, SortedList<string, List<MediaObject>>> mediaByArtist)
        {
            foreach (string artist in mediaByArtist.Keys)
                cmbArtists.Items.Add(artist);
            if (cmbArtists.Items.Count > 0)
                cmbArtists.SelectedIndex = 0;
        }

        private void RefreshMediaList()
        {
            refreshing = true;
            try
            {
                if (cmbListStyle.Text.Equals("Library"))
                {
                    if (cmbArtists.Text.Equals("")) return;

                    lstMedia.Items.Clear();

                    SortedList<string, List<MediaObject>> media = medLib.GetMediaByArtist()[cmbArtists.Text];
                    foreach (KeyValuePair<string, List<MediaObject>> album in media)
                    {
                        foreach (MediaObject mo in album.Value)
                        {
                            string dispString = (mo as Mp3).TrackTitle.Equals("") ?
                                (mo as Mp3).FileName : (mo as Mp3).TrackTitle;
                            lstMedia.Items.Add(dispString);
                        }
                    }
                }
                else if (cmbListStyle.Text.Equals("Playlist"))
                {
                    lstMedia.Items.Clear();

                    foreach (MediaPlaylist playlist in medLib.Playlists)
                    {
                        if (cmbArtists.Text.Equals(playlist.Name))
                            foreach (MediaObject mo in playlist.MediaList)
                                if (mo is Mp3)
                                    lstMedia.Items.Add((mo as Mp3).TrackTitle);
                    }
                }
            }
            catch (Exception e) { throw e; }
            finally { refreshing = false; }
        }

        private void lstMedia_DoubleClick(object sender, EventArgs e)
        {
            mp3.ClearScreen();

            curMp3 = null;
            foreach (MediaObject mo in medLib.AllMedia)
            {
                if ((mo as Mp3).TrackTitle.Equals(lstMedia.SelectedItem.ToString()))
                {
                    curMp3 = (mo as Mp3);
                    break;
                }
            }
            if(curMp3 != null)
                Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);
        }

        private void Play(string fileName, string trackTitle, string artist)
        {
            mp3Launcher = new Thread(new ThreadStart(LaunchMedia));
            mp3Launcher.Start();
        }
        
        #region Play Time Updater

        private delegate void UpdateTime(string timeElapsedParam);
        void mp3_TimeElapsed(string timeElapsedParam)
        {
            playTimeElapsed.Invoke(timeUpdater, new object[] { timeElapsedParam });
        }

        void UpdateTimeLabel(string timeElapsedParam)
        {
            playTimeElapsed.Text = timeElapsedParam;
        }

        #endregion

        #region Media Launcher

        private void SelectCurrentMedia()
        {
            if (!cmbArtists.Text.Equals(curMp3.Artist))
                cmbArtists.Text = curMp3.Artist;

            while (refreshing) { }
            for (int i = 0; i < lstMedia.Items.Count; i++)
                if (lstMedia.Items[i].ToString().Equals(curMp3.TrackTitle))
                {
                    lstMedia.SelectedIndex = i;
                    i = lstMedia.Items.Count + 1;
                }
        }

        private void LaunchMedia()
        {
            try
            {
                SelectCurrentMedia();

                mp3.Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);

                int i = 0;
                while (1 == 1)
                {
                    // Retry to play the mp3 if we've been waiting for 1.6 seconds
                    if (++i % 2 == 0 && mp3.PlayState == Mp3Player.PlayStateEnum.Transitioning)
                        mp3.Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);

                    Thread.Sleep(800);
                    if (mp3.PlayState == Mp3Player.PlayStateEnum.Playing)
                        break;
                    else if (i > 4 || !curMp3.ProblemString.Equals(""))
                    {
                        curMp3.ProblemString = "Transition error.";

                        // We have been transitioning for too long - flag the error mp3
                        curMp3 = medLib.GetMedia(medLib.Next(curMp3.FileName)) as Mp3;
                        Play(curMp3.FileName, curMp3.TrackTitle, curMp3.Artist);
                        Thread.CurrentThread.Abort();
                    }
                }
            }
            catch (ThreadAbortException)
            { /* Do nothing */  }
            catch (Exception e)
            {
                MessageBox.Show("Error launching media: " + e.Message);
            }
        }

        private void btnRefreshScreen_Click(object sender, EventArgs e)
        {
            mp3.ClearScreen();
        }

        #endregion

        private void cmbArtists_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMediaList();
        }

        private void cmbListStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (medLib == null) return;
            if (cmbListStyle.Text.Equals("Library"))
            {
                cmbArtists.Items.Clear();
                LoadLibrary(medLib.GetMediaByArtist());
                RefreshMediaList();
            }
            else if (cmbListStyle.Text.Equals("Playlist"))
            {
                RefreshPlaylists();
                //lstMedia.Items.Clear();
            }
        }

        private void RefreshPlaylists()
        {
            string selectedPlaylist = cmbArtists.Text;

            cmbArtists.Items.Clear();
            foreach (MediaPlaylist playlist in medLib.Playlists)
                cmbArtists.Items.Add(playlist.Name);
            cmbArtists.Text = selectedPlaylist;

            if (cmbArtists.Items.Count > 0 && cmbArtists.Text.Equals(""))
                cmbArtists.SelectedIndex = 0;
        }

        private void shuffleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shuffleToolStripMenuItem.Checked = !shuffleToolStripMenuItem.Checked;
        }

        private void playlistEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaylistEditorTest playlistEditor = new PlaylistEditorTest(medLib);
            playlistEditor.ShowDialog();
            cmbListStyle_SelectedIndexChanged(playbackToolStripMenuItem, EventArgs.Empty);
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void lblMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnPlaylistEditor_Click(object sender, EventArgs e)
        {
            PlaylistEditorTest playlistEditor = new PlaylistEditorTest(medLib);
            playlistEditor.Show();
        }

    }
}