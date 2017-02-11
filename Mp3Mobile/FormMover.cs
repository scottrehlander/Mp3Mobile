using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MediaMobile
{
    public partial class FormMover : UserControl
    {
        System.Windows.Forms.Form fr;
        private Point mouseOffset;
        private bool isMouseDown = false;

        public FormMover()
        {
            InitializeComponent();
        }


        void FormMover_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int xOffset;
            int yOffset;
            // MessageBox.Show("click");
            if (e.Button == MouseButtons.Left)
            {
                // Assign coordinates to mouseOffset variable based on 
                // current position of the mouse pointer.
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                    SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }    
        }

        private void FormMover_MouseMove(object sender, MouseEventArgs e)
        {
            // If the left mouse is held down.
            if (isMouseDown)
            {
                // Set the form's location property to the new position.
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X + 8, mouseOffset.Y + 28);
                fr.Location = mousePos;
            }

        }

        /// <summary>
        /// finalize Form Moving 
        /// by Seting The Flage (isMouseDown) = false 
        /// to disable Moving Form With Mouse Moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMover_MouseUp(object sender, MouseEventArgs e)
        {
            // Changes the isMouseDown field so that the form does
            // not move unless the user is pressing the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            fr = (System.Windows.Forms.Form)this.FindForm();
        }
    }
}
