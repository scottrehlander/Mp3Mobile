using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialLCDInterface
{
    public partial class TestForm : Form
    {
        public static void Main(string[] args)
        {
            System.Windows.Forms.Application.Run(new TestForm());
        }

        public TestForm()
        {
            InitializeComponent();

            SerialLCDInterface.InitializeDevice(1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SerialLCDInterface.RowMemoryAddress rowMemoryAddr = GetRowAddr(int.Parse(txtSaveAddress.Text));

            SerialLCDInterface.UpdateLineText(rowMemoryAddr, txtSaveText.Text);

            //slcd.WriteString(rowMemoryAddr, txtSaveText.Text);
            //txtSaveText.Clear();
            //txtSaveAddress.Clear();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            SerialLCDInterface.MoveCursorToPos(int.Parse(txtDispRow.Text), int.Parse(txtDispCol.Text));
            SerialLCDInterface.DisplayString(GetRowAddr(int.Parse(txtDispAddress.Text)));
        }

        private void btnClearScreen_Click(object sender, EventArgs e)
        {
            SerialLCDInterface.ClearScreen();
        }

        private SerialLCDInterface.RowMemoryAddress GetRowAddr(int rowNumber)
        {
            SerialLCDInterface.RowMemoryAddress rowMemoryAddr = SerialLCDInterface.RowMemoryAddress.Line1;
            switch (rowNumber)
            {
                case 1:
                    rowMemoryAddr = SerialLCDInterface.RowMemoryAddress.Line1;
                    break;
                case 2:
                    rowMemoryAddr = SerialLCDInterface.RowMemoryAddress.Line2;
                    break;
                case 3:
                    rowMemoryAddr = SerialLCDInterface.RowMemoryAddress.Line3;
                    break;
                case 4:
                    rowMemoryAddr = SerialLCDInterface.RowMemoryAddress.Line4;
                    break;
            }
            return rowMemoryAddr;
        }

        private void btnSetBacklight_Click(object sender, EventArgs e)
        {
            SerialLCDInterface.SetBacklightBrightness(int.Parse(txtBacklight.Text));
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(SerialLCDInterface.ILoveXtine));
            t.Start();
        }

        private void btnShowTime_Click(object sender, EventArgs e)
        {
            SerialLCDInterface.DisplayTime();
        }

        private void btnClearLine_Click(object sender, EventArgs e)
        {
            SerialLCDInterface.ClearLine(GetRowAddr(int.Parse(txtLineNum.Text)));
        }


    }
}