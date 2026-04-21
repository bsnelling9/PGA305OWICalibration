namespace PGA305OWICalibration
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            listBox1 = new ListBox();
            u2a = new Button();
            comboBox1 = new ComboBox();
            button2 = new Button();
            btnRead = new Button();
            btnDebugGPIO = new Button();
            btnManualHardwareTest = new Button();
            btnTestGPIO11 = new Button();
            btnActivate = new Button();
            btnLoadEEPROM = new Button();
            btnPartSerial = new Button();
            btnGetHW = new Button();
            btnConnectSTM32 = new Button();
            btnInitUSB2ANY = new Button();
            btnInitPGA305 = new Button();
            btnFindDUT = new Button();
            btnGetMetaData = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(321, 196);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(739, 319);
            listBox1.TabIndex = 1;
            // 
            // u2a
            // 
            u2a.Location = new Point(1092, 194);
            u2a.Name = "u2a";
            u2a.Size = new Size(119, 48);
            u2a.TabIndex = 2;
            u2a.Text = "U2A";
            u2a.UseVisualStyleBackColor = true;
            u2a.Click += u2a_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(321, 158);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(269, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(1081, 12);
            button2.Name = "button2";
            button2.Size = new Size(120, 41);
            button2.TabIndex = 4;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(1081, 566);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(118, 41);
            btnRead.TabIndex = 5;
            btnRead.Text = "Read Part and Serial Number";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // btnDebugGPIO
            // 
            btnDebugGPIO.Location = new Point(1124, 98);
            btnDebugGPIO.Name = "btnDebugGPIO";
            btnDebugGPIO.Size = new Size(115, 37);
            btnDebugGPIO.TabIndex = 6;
            btnDebugGPIO.Text = "Debug GPIO";
            btnDebugGPIO.UseVisualStyleBackColor = true;
            btnDebugGPIO.Click += btnDebugGPIO_Click;
            // 
            // btnManualHardwareTest
            // 
            btnManualHardwareTest.Location = new Point(915, 81);
            btnManualHardwareTest.Name = "btnManualHardwareTest";
            btnManualHardwareTest.Size = new Size(93, 41);
            btnManualHardwareTest.TabIndex = 7;
            btnManualHardwareTest.Text = "Manual Test";
            btnManualHardwareTest.UseVisualStyleBackColor = true;
            btnManualHardwareTest.Click += btnManualHardwareTest_Click;
            // 
            // btnTestGPIO11
            // 
            btnTestGPIO11.Location = new Point(491, 77);
            btnTestGPIO11.Name = "btnTestGPIO11";
            btnTestGPIO11.Size = new Size(99, 45);
            btnTestGPIO11.TabIndex = 8;
            btnTestGPIO11.Text = "Test GPIO11";
            btnTestGPIO11.UseVisualStyleBackColor = true;
            btnTestGPIO11.Click += btnTestGPIO11_Click;
            // 
            // btnActivate
            // 
            btnActivate.Location = new Point(1092, 266);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(116, 39);
            btnActivate.TabIndex = 9;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += btnActivate_Click;
            // 
            // btnLoadEEPROM
            // 
            btnLoadEEPROM.Location = new Point(1095, 336);
            btnLoadEEPROM.Name = "btnLoadEEPROM";
            btnLoadEEPROM.Size = new Size(116, 51);
            btnLoadEEPROM.TabIndex = 10;
            btnLoadEEPROM.Text = "Load EEPROM";
            btnLoadEEPROM.UseVisualStyleBackColor = true;
            btnLoadEEPROM.Click += btnLoadEEPROM_Click;
            // 
            // btnPartSerial
            // 
            btnPartSerial.Location = new Point(1092, 424);
            btnPartSerial.Name = "btnPartSerial";
            btnPartSerial.Size = new Size(122, 61);
            btnPartSerial.TabIndex = 11;
            btnPartSerial.Text = "Get Part and Serial";
            btnPartSerial.UseVisualStyleBackColor = true;
            btnPartSerial.Click += btnPartSerial_Click;
            // 
            // btnGetHW
            // 
            btnGetHW.Location = new Point(45, 194);
            btnGetHW.Name = "btnGetHW";
            btnGetHW.Size = new Size(117, 46);
            btnGetHW.TabIndex = 12;
            btnGetHW.Text = "Get STM32";
            btnGetHW.UseVisualStyleBackColor = true;
            btnGetHW.Click += btnGetHW_Click;
            // 
            // btnConnectSTM32
            // 
            btnConnectSTM32.Location = new Point(45, 258);
            btnConnectSTM32.Name = "btnConnectSTM32";
            btnConnectSTM32.Size = new Size(120, 55);
            btnConnectSTM32.TabIndex = 13;
            btnConnectSTM32.Text = "Connect to STM32";
            btnConnectSTM32.UseVisualStyleBackColor = true;
            btnConnectSTM32.Click += btnConnectSTM32_Click;
            // 
            // btnInitUSB2ANY
            // 
            btnInitUSB2ANY.Location = new Point(45, 333);
            btnInitUSB2ANY.Name = "btnInitUSB2ANY";
            btnInitUSB2ANY.Size = new Size(119, 56);
            btnInitUSB2ANY.TabIndex = 14;
            btnInitUSB2ANY.Text = "Connect USB2ANY";
            btnInitUSB2ANY.UseVisualStyleBackColor = true;
            btnInitUSB2ANY.Click += btnInitUSB2ANY_Click;
            // 
            // btnInitPGA305
            // 
            btnInitPGA305.Location = new Point(48, 486);
            btnInitPGA305.Name = "btnInitPGA305";
            btnInitPGA305.Size = new Size(117, 49);
            btnInitPGA305.TabIndex = 15;
            btnInitPGA305.Text = "Initialize PGA305";
            btnInitPGA305.UseVisualStyleBackColor = true;
            btnInitPGA305.Click += btnInitPGA305_Click;
            // 
            // btnFindDUT
            // 
            btnFindDUT.Location = new Point(44, 402);
            btnFindDUT.Name = "btnFindDUT";
            btnFindDUT.Size = new Size(123, 63);
            btnFindDUT.TabIndex = 16;
            btnFindDUT.Text = "Find DUT";
            btnFindDUT.UseVisualStyleBackColor = true;
            btnFindDUT.Click += btnFindDUT_Click;
            // 
            // btnGetMetaData
            // 
            btnGetMetaData.Location = new Point(51, 552);
            btnGetMetaData.Name = "btnGetMetaData";
            btnGetMetaData.Size = new Size(118, 66);
            btnGetMetaData.TabIndex = 17;
            btnGetMetaData.Text = "Get Meta Data";
            btnGetMetaData.UseVisualStyleBackColor = true;
            btnGetMetaData.Click += btnGetMetaData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1341, 653);
            Controls.Add(btnGetMetaData);
            Controls.Add(btnFindDUT);
            Controls.Add(btnInitPGA305);
            Controls.Add(btnInitUSB2ANY);
            Controls.Add(btnConnectSTM32);
            Controls.Add(btnGetHW);
            Controls.Add(btnPartSerial);
            Controls.Add(btnLoadEEPROM);
            Controls.Add(btnActivate);
            Controls.Add(btnTestGPIO11);
            Controls.Add(btnManualHardwareTest);
            Controls.Add(btnDebugGPIO);
            Controls.Add(btnRead);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(u2a);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private ListBox listBox1;
        private Button u2a;
        private ComboBox comboBox1;
        private Button button2;
        private Button btnRead;
        private Button btnDebugGPIO;
        private Button btnManualHardwareTest;
        private Button btnTestGPIO11;
        private Button btnActivate;
        private Button btnLoadEEPROM;
        private Button btnPartSerial;
        private Button btnGetHW;
        private Button btnConnectSTM32;
        private Button btnInitUSB2ANY;
        private Button btnInitPGA305;
        private Button btnFindDUT;
        private Label label1;
        private Button btnGetMetaData;
    }
}
