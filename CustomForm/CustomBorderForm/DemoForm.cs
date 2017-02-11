#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Kobush.Windows.Forms
{
    [System.ComponentModel.DesignerCategory("form")]
    partial class DemoForm : LonghornForm
	{
		public DemoForm()
		{
			InitializeComponent();

			ToolStripManager.Renderer = new ToolStripSystemRenderer();

			textBox1.Text = this.Text;
			chkDoubleBuffering.Checked = this.NonClientAreaDoubleBuffering;
			chkEnableNCPaint.Checked = this.EnableNonClientAreaPaint;
            chkUseStyleManager.Checked = this.UseFormStyleManager;

			chkControlBox.Checked = this.ControlBox;
			chkMaximizeBox.Checked = this.MaximizeBox;
			chkMinimizeBox.Checked = this.MinimizeBox;
			chkHelpButton.Checked = this.HelpButton;

			numTransparency.Value = (decimal)(this.Opacity*100f);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Text = textBox1.Text;
		}

		private void chkDoubleBuffering_CheckedChanged(object sender, EventArgs e)
		{
			this.NonClientAreaDoubleBuffering = chkDoubleBuffering.Checked;
		}

		private void chkEnableNCPaint_CheckedChanged(object sender, EventArgs e)
		{
			this.EnableNonClientAreaPaint = chkEnableNCPaint.Checked;
		}

        private void chkUseStyleManager_CheckedChanged(object sender, EventArgs e)
        {
            this.UseFormStyleManager = chkUseStyleManager.Checked;
        }

		private void button2_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 100; i++)
			{
				System.Threading.Thread.Sleep(100);
			}
		}

		private void chkControlBox_CheckedChanged(object sender, EventArgs e)
		{
			this.ControlBox = chkControlBox.Checked;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.MaximizeBox = chkMaximizeBox.Checked;
		}

		private void chkHelpButton_CheckedChanged(object sender, EventArgs e)
		{
			this.HelpButton = chkHelpButton.Checked;
		}

		private void chkMinimizeBox_CheckedChanged(object sender, EventArgs e)
		{
			this.MinimizeBox = chkMinimizeBox.Checked;
		}

		private void numTransparency_ValueChanged(object sender, EventArgs e)
		{
			this.Opacity = (double)(numTransparency.Value/100m);
		}

        private void linkStyleEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formStyleEditor1.ShowFormEditor(this);
        }


	}
}