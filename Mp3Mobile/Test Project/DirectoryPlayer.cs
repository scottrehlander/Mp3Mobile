using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MediaMobile
{
    public partial class DirectoryPlayer : Form
    {
        string curDir = @"c:\";

        const string ACCESS_DENIED_MSG = "Access to this folder is denied";

        public string CurDir 
        { 
            set 
            { 
                curDir = value;
                lblCurrentDir.Text = curDir;
                UpdateDirs(); 
            }
            get { return curDir; }
        }

        public DirectoryPlayer()
        {
            InitializeComponent();

            CurDir = @"c:\Users\srehlander\documents\downloads";
        }

        private void UpdateDirs()
        {
            if (curDir.EndsWith(".."))
            {
                curDir = curDir.Substring(0, curDir.LastIndexOf('\\'));
                btnBackDir_Click(new object(), EventArgs.Empty);
            }

            if (!Directory.Exists(curDir)) throw new Exception("Directory " + curDir + " does not exist.");
            try
            {
                lbDirs.Items.Clear();
                lbFiles.Items.Clear();
                DirectoryInfo di = new DirectoryInfo(curDir);

                lbDirs.Items.Add("..");
                foreach (DirectoryInfo subDir in di.GetDirectories())
                    lbDirs.Items.Add(subDir.Name);

                foreach (FileInfo file in di.GetFiles())
                    if(file.Name.EndsWith(".mp3"))
                        lbFiles.Items.Add(file.Name);
            }
            catch (UnauthorizedAccessException ex) { lbDirs.Items.Add(ACCESS_DENIED_MSG); Console.Out.WriteLine("Error in UpdateDirs():  Unauthorized Access."); }
        }

        private void lbDirs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDirs.SelectedItem == null) return;
            if (lbDirs.SelectedItem.ToString().Equals(ACCESS_DENIED_MSG)) return;

            CurDir += "\\" + lbDirs.SelectedItem.ToString();
        }

        private void btnBackDir_Click(object sender, EventArgs e)
        {
            if (curDir.EndsWith(":\\")) return;
            if(CurDir.Contains("\\"))
                CurDir = curDir.Substring(0, curDir.LastIndexOf('\\'));
        }

        private void showTagTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showTagTitleToolStripMenuItem.Checked = !showTagTitleToolStripMenuItem.Checked;
        }
    }
}