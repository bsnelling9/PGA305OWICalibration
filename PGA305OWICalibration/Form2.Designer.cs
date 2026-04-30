namespace PGA305OWICalibration
{
    partial class Form2
    {
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

        private void InitializeComponent()
        {
            btnExitDebug = new Button();
            u2a = new Button();
            button2 = new Button();
            btnRead = new Button();
            btnDebugGPIO = new Button();
            btnManualHardwareTest = new Button();
            btnTestGPIO11 = new Button();
            btnActivate = new Button();
            btnLoadEEPROM = new Button();
            btnGetSerial = new Button();
            button10 = new Button();
            listBoxDebug = new ListBox();
            SuspendLayout();
            // 
            // btnExitDebug
            // 
            btnExitDebug.Location = new Point(43, 23);
            btnExitDebug.Name = "btnExitDebug";
            btnExitDebug.Size = new Size(85, 41);
            btnExitDebug.TabIndex = 0;
            btnExitDebug.Text = "Exit";
            btnExitDebug.UseVisualStyleBackColor = true;
            btnExitDebug.Click += btnExitDebug_Click;
            // 
            // u2a
            // 
            u2a.Location = new Point(632, 39);
            u2a.Name = "u2a";
            u2a.Size = new Size(119, 48);
            u2a.TabIndex = 2;
            u2a.Text = "U2A";
            u2a.UseVisualStyleBackColor = true;
            u2a.Click += u2a_Click;
            // 
            // button2
            // 
            button2.Location = new Point(43, 189);
            button2.Name = "button2";
            button2.Size = new Size(120, 41);
            button2.TabIndex = 4;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(632, 368);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(118, 41);
            btnRead.TabIndex = 5;
            btnRead.Text = "Read Part and Serial Number";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // btnDebugGPIO
            // 
            btnDebugGPIO.Location = new Point(43, 334);
            btnDebugGPIO.Name = "btnDebugGPIO";
            btnDebugGPIO.Size = new Size(115, 37);
            btnDebugGPIO.TabIndex = 6;
            btnDebugGPIO.Text = "Debug GPIO";
            btnDebugGPIO.UseVisualStyleBackColor = true;
            btnDebugGPIO.Click += btnDebugGPIO_Click;
            // 
            // btnManualHardwareTest
            // 
            btnManualHardwareTest.Location = new Point(43, 105);
            btnManualHardwareTest.Name = "btnManualHardwareTest";
            btnManualHardwareTest.Size = new Size(93, 41);
            btnManualHardwareTest.TabIndex = 7;
            btnManualHardwareTest.Text = "Manual Test";
            btnManualHardwareTest.UseVisualStyleBackColor = true;
            btnManualHardwareTest.Click += btnManualHardwareTest_Click;
            // 
            // btnTestGPIO11
            // 
            btnTestGPIO11.Location = new Point(43, 254);
            btnTestGPIO11.Name = "btnTestGPIO11";
            btnTestGPIO11.Size = new Size(99, 45);
            btnTestGPIO11.TabIndex = 8;
            btnTestGPIO11.Text = "Test GPIO11";
            btnTestGPIO11.UseVisualStyleBackColor = true;
            btnTestGPIO11.Click += btnTestGPIO11_Click;
            // 
            // btnActivate
            // 
            btnActivate.Location = new Point(632, 107);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(116, 39);
            btnActivate.TabIndex = 9;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += btnActivate_Click;
            // 
            // btnLoadEEPROM
            // 
            btnLoadEEPROM.Location = new Point(632, 179);
            btnLoadEEPROM.Name = "btnLoadEEPROM";
            btnLoadEEPROM.Size = new Size(116, 51);
            btnLoadEEPROM.TabIndex = 10;
            btnLoadEEPROM.Text = "Load EEPROM";
            btnLoadEEPROM.UseVisualStyleBackColor = true;
            btnLoadEEPROM.Click += btnLoadEEPROM_Click;
            // 
            // btnGetSerial
            // 
            btnGetSerial.Location = new Point(632, 266);
            btnGetSerial.Name = "btnGetSerial";
            btnGetSerial.Size = new Size(122, 61);
            btnGetSerial.TabIndex = 11;
            btnGetSerial.Text = "Get Part and Serial";
            btnGetSerial.UseVisualStyleBackColor = true;
            btnGetSerial.Click += btnPartSerial_Click;
            // 
            // button10
            // 
            button10.Location = new Point(-829, 16);
            button10.Name = "button10";
            button10.Size = new Size(136, 46);
            button10.TabIndex = 20;
            button10.Text = "Debug";
            button10.UseVisualStyleBackColor = true;
            // 
            // listBoxDebug
            // 
            listBoxDebug.FormattingEnabled = true;
            listBoxDebug.ItemHeight = 15;
            listBoxDebug.Location = new Point(221, 111);
            listBoxDebug.Name = "listBoxDebug";
            listBoxDebug.Size = new Size(349, 274);
            listBoxDebug.TabIndex = 21;
            //listBoxDebug.SelectedIndexChanged += listBoxDebug_SelectedIndexChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 692);
            Controls.Add(listBoxDebug);
            Controls.Add(button10);
            Controls.Add(btnExitDebug);
            Controls.Add(u2a);
            Controls.Add(button2);
            Controls.Add(btnRead);
            Controls.Add(btnDebugGPIO);
            Controls.Add(btnManualHardwareTest);
            Controls.Add(btnTestGPIO11);
            Controls.Add(btnActivate);
            Controls.Add(btnLoadEEPROM);
            Controls.Add(btnGetSerial);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private Button btnExitDebug;
        private Button u2a;
        private Button button2;
        private Button btnRead;
        private Button btnDebugGPIO;
        private Button btnManualHardwareTest;
        private Button btnTestGPIO11;
        private Button btnActivate;
        private Button btnLoadEEPROM;
        private Button btnGetSerial;
        private Button button10;
        private ListBox listBoxDebug;
    }
}