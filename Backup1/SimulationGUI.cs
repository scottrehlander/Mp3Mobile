using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialLCDInterface
{
    public partial class SimulationGUI : Form
    {
        public SimulationGUI()
        {
            InitializeComponent();

            string s = "";
            for (int j = 0; j < 20; j++)
                s += " ";

            for (int i = 0; i < 4; i++)
                lstDisplay.Items.Add(s);
        }

        public void DisplayString(int line, string dispString, int colNum)
        {
            // we must make sure that colNum + displayString.length == 20;
            if (dispString.Length < 20)
                for (int i = colNum + dispString.Length; i < 20; i++)
                    dispString += " ";
           
            lstDisplay.Items[line] = 
                lstDisplay.Items[line].ToString().Substring(0, colNum) +
                dispString.Substring(0, 20 - colNum);
        }

        private void lstDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstDisplay.Enabled = false;
            lstDisplay.ClearSelected();
        }
    }
}