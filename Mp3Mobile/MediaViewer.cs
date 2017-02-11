using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MediaMobile
{
    public partial class MediaViewer : UserControl
    {
        int topCursor = 0;

        List<Label> labels = new List<Label>();

        public MediaViewer()
        {
            InitializeComponent();
        }

        public void AddEntry(string text, string path)
        {
            Label l = MakeLabel(text, path);
            pnlViewer.Controls.Add(l);
            labels.Add(l);
        }

        public void NewList(SortedList<string, string> entries)
        {
            pnlViewer.Controls.Clear();
            topCursor = 0;
            foreach(KeyValuePair<string, string> entry in entries)
            {
                Label l = MakeLabel(entry.Key, entry.Value);
                pnlViewer.Controls.Add(l);
                labels.Add(l);
            }
        }

        private Label MakeLabel(string text, string tag)
        {
            Label l = new Label();
            l.Text = text;
            l.Tag = tag;
            l.ForeColor = Color.White;
            l.Top = topCursor;
            l.Width = pnlViewer.Width;
            topCursor += l.Height;

            l.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            l.MouseEnter += new EventHandler(label_mouseover);
            l.MouseLeave += new EventHandler(l_MouseLeave);

            return l;
        }

        private void l_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Black;
        }

        private void label_mouseover(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Green;
        }
    }
}
