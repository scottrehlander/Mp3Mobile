namespace SerialLCDInterface
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSaveText = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSaveAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDispCol = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDispRow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.txtDispAddress = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearScreen = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClearLine = new System.Windows.Forms.Button();
            this.txtLineNum = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBacklight = new System.Windows.Forms.TextBox();
            this.btnSetBacklight = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnShowTime = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSaveText
            // 
            this.txtSaveText.Location = new System.Drawing.Point(91, 19);
            this.txtSaveText.Multiline = true;
            this.txtSaveText.Name = "txtSaveText";
            this.txtSaveText.Size = new System.Drawing.Size(245, 112);
            this.txtSaveText.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(342, 137);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSaveAddress
            // 
            this.txtSaveAddress.Location = new System.Drawing.Point(91, 137);
            this.txtSaveAddress.Name = "txtSaveAddress";
            this.txtSaveAddress.Size = new System.Drawing.Size(245, 20);
            this.txtSaveAddress.TabIndex = 2;
            this.txtSaveAddress.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Text:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Row Memory:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSaveText);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSaveAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 175);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save Text";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDispCol);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtDispRow);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnDisplay);
            this.groupBox2.Controls.Add(this.txtDispAddress);
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 88);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display Text";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Col:";
            // 
            // txtDispCol
            // 
            this.txtDispCol.Location = new System.Drawing.Point(275, 55);
            this.txtDispCol.Name = "txtDispCol";
            this.txtDispCol.Size = new System.Drawing.Size(61, 20);
            this.txtDispCol.TabIndex = 8;
            this.txtDispCol.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Row:";
            // 
            // txtDispRow
            // 
            this.txtDispRow.Location = new System.Drawing.Point(155, 55);
            this.txtDispRow.Name = "txtDispRow";
            this.txtDispRow.Size = new System.Drawing.Size(61, 20);
            this.txtDispRow.TabIndex = 5;
            this.txtDispRow.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Row Memory:";
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(342, 29);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(75, 23);
            this.btnDisplay.TabIndex = 1;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // txtDispAddress
            // 
            this.txtDispAddress.Location = new System.Drawing.Point(91, 29);
            this.txtDispAddress.Name = "txtDispAddress";
            this.txtDispAddress.Size = new System.Drawing.Size(245, 20);
            this.txtDispAddress.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearScreen);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btnClearLine);
            this.groupBox3.Controls.Add(this.txtLineNum);
            this.groupBox3.Location = new System.Drawing.Point(451, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(234, 86);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clear Text";
            // 
            // btnClearScreen
            // 
            this.btnClearScreen.Location = new System.Drawing.Point(132, 55);
            this.btnClearScreen.Name = "btnClearScreen";
            this.btnClearScreen.Size = new System.Drawing.Size(94, 23);
            this.btnClearScreen.TabIndex = 5;
            this.btnClearScreen.Text = "Clear Screen";
            this.btnClearScreen.UseVisualStyleBackColor = true;
            this.btnClearScreen.Click += new System.EventHandler(this.btnClearScreen_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Line:";
            // 
            // btnClearLine
            // 
            this.btnClearLine.Location = new System.Drawing.Point(132, 26);
            this.btnClearLine.Name = "btnClearLine";
            this.btnClearLine.Size = new System.Drawing.Size(94, 23);
            this.btnClearLine.TabIndex = 1;
            this.btnClearLine.Text = "Clear Line";
            this.btnClearLine.UseVisualStyleBackColor = true;
            this.btnClearLine.Click += new System.EventHandler(this.btnClearLine_Click);
            // 
            // txtLineNum
            // 
            this.txtLineNum.Location = new System.Drawing.Point(43, 27);
            this.txtLineNum.Name = "txtLineNum";
            this.txtLineNum.Size = new System.Drawing.Size(83, 20);
            this.txtLineNum.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBacklight);
            this.groupBox4.Controls.Add(this.btnSetBacklight);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(450, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(235, 83);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Backlight";
            // 
            // txtBacklight
            // 
            this.txtBacklight.Location = new System.Drawing.Point(71, 19);
            this.txtBacklight.Name = "txtBacklight";
            this.txtBacklight.Size = new System.Drawing.Size(155, 20);
            this.txtBacklight.TabIndex = 0;
            // 
            // btnSetBacklight
            // 
            this.btnSetBacklight.Location = new System.Drawing.Point(151, 45);
            this.btnSetBacklight.Name = "btnSetBacklight";
            this.btnSetBacklight.Size = new System.Drawing.Size(75, 23);
            this.btnSetBacklight.TabIndex = 1;
            this.btnSetBacklight.Text = "Set";
            this.btnSetBacklight.UseVisualStyleBackColor = true;
            this.btnSetBacklight.Click += new System.EventHandler(this.btnSetBacklight_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Brightness:";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(583, 258);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(93, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Run Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnShowTime
            // 
            this.btnShowTime.Location = new System.Drawing.Point(583, 229);
            this.btnShowTime.Name = "btnShowTime";
            this.btnShowTime.Size = new System.Drawing.Size(93, 23);
            this.btnShowTime.TabIndex = 11;
            this.btnShowTime.Text = "Display Time";
            this.btnShowTime.UseVisualStyleBackColor = true;
            this.btnShowTime.Click += new System.EventHandler(this.btnShowTime_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 309);
            this.Controls.Add(this.btnShowTime);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestForm";
            this.Text = "Scott\'s LCD Interface Tester";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSaveText;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtSaveAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDispCol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDispRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.TextBox txtDispAddress;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearScreen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClearLine;
        private System.Windows.Forms.TextBox txtLineNum;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtBacklight;
        private System.Windows.Forms.Button btnSetBacklight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnShowTime;
    }
}