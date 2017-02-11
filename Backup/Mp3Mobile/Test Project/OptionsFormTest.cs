using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MediaMobile
{
    public partial class OptionsFormTest : Form
    {
        private string renameString = "";
        public string RenameString { get { return renameString; } }

        public enum FormType
        {
            Rename = 0
        }

        public OptionsFormTest(FormType formType)
        {
            InitializeComponent();

            if (formType == FormType.Rename)
            {
                pnlRename.Location = new Point(0, 0);
                this.Text = "Rename";
                this.Width = pnlRename.Width + 15;
                this.Height = pnlRename.Height + 20;
            }
        }

        private void btnRenameOK_Click(object sender, EventArgs e)
        {
            renameString = txtRename.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnRenameCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}