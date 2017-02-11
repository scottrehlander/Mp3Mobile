using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MediaMobile
{
    public partial class LoadForm : Form
    {
        public void SetDescriptoin(string s)
        {
             lblDescription.Text = s;    
        }

        public LoadForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            new System.Threading.Thread(new System.Threading.ThreadStart(refreshit)).Start(); ;
        }

        public void refreshit()
        {
            while (1 == 1)
            {
                System.Threading.Thread.Sleep(75);
                this.Refresh();
            }
        }
    }
}