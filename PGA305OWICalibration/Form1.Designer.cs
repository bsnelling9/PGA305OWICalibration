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
            comboBox1 = new ComboBox();
            btnGetHW = new Button();
            btnConnectSTM32 = new Button();
            btnInitUSB2ANY = new Button();
            btnInitPGA305 = new Button();
            btnFindDUT = new Button();
            btnGetMetaData = new Button();
            btnActPGA = new Button();
            btnGetDIG_IF_CNTRL = new Button();
            btnDebug = new Button();
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
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(321, 158);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(269, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnGetHW
            // 
            btnGetHW.Location = new Point(44, 135);
            btnGetHW.Name = "btnGetHW";
            btnGetHW.Size = new Size(117, 46);
            btnGetHW.TabIndex = 12;
            btnGetHW.Text = "Get STM32";
            btnGetHW.UseVisualStyleBackColor = true;
            btnGetHW.Click += btnGetHW_Click;
            // 
            // btnConnectSTM32
            // 
            btnConnectSTM32.Location = new Point(41, 207);
            btnConnectSTM32.Name = "btnConnectSTM32";
            btnConnectSTM32.Size = new Size(120, 55);
            btnConnectSTM32.TabIndex = 13;
            btnConnectSTM32.Text = "Connect to STM32";
            btnConnectSTM32.UseVisualStyleBackColor = true;
            btnConnectSTM32.Click += btnConnectSTM32_Click;
            // 
            // btnInitUSB2ANY
            // 
            btnInitUSB2ANY.Location = new Point(41, 288);
            btnInitUSB2ANY.Name = "btnInitUSB2ANY";
            btnInitUSB2ANY.Size = new Size(119, 56);
            btnInitUSB2ANY.TabIndex = 14;
            btnInitUSB2ANY.Text = "Connect USB2ANY";
            btnInitUSB2ANY.UseVisualStyleBackColor = true;
            btnInitUSB2ANY.Click += btnInitUSB2ANY_Click;
            // 
            // btnInitPGA305
            // 
            btnInitPGA305.Location = new Point(37, 453);
            btnInitPGA305.Name = "btnInitPGA305";
            btnInitPGA305.Size = new Size(117, 49);
            btnInitPGA305.TabIndex = 15;
            btnInitPGA305.Text = "Initialize PGA305";
            btnInitPGA305.UseVisualStyleBackColor = true;
            btnInitPGA305.Click += btnInitPGA305_Click;
            // 
            // btnFindDUT
            // 
            btnFindDUT.Location = new Point(37, 373);
            btnFindDUT.Name = "btnFindDUT";
            btnFindDUT.Size = new Size(123, 63);
            btnFindDUT.TabIndex = 16;
            btnFindDUT.Text = "Find DUT";
            btnFindDUT.UseVisualStyleBackColor = true;
            btnFindDUT.Click += btnFindDUT_Click;
            // 
            // btnGetMetaData
            // 
            btnGetMetaData.Location = new Point(36, 604);
            btnGetMetaData.Name = "btnGetMetaData";
            btnGetMetaData.Size = new Size(118, 66);
            btnGetMetaData.TabIndex = 17;
            btnGetMetaData.Text = "Get Meta Data";
            btnGetMetaData.UseVisualStyleBackColor = true;
            btnGetMetaData.Click += btnGetMetaData_Click;
            // 
            // btnActPGA
            // 
            btnActPGA.Location = new Point(37, 514);
            btnActPGA.Name = "btnActPGA";
            btnActPGA.Size = new Size(121, 53);
            btnActPGA.TabIndex = 18;
            btnActPGA.Text = "Activate";
            btnActPGA.UseVisualStyleBackColor = true;
            btnActPGA.Click += btnActPGA_Click;
            // 
            // btnGetDIG_IF_CNTRL
            // 
            btnGetDIG_IF_CNTRL.Location = new Point(1164, 314);
            btnGetDIG_IF_CNTRL.Name = "btnGetDIG_IF_CNTRL";
            btnGetDIG_IF_CNTRL.Size = new Size(123, 41);
            btnGetDIG_IF_CNTRL.TabIndex = 19;
            btnGetDIG_IF_CNTRL.Text = "Get DIG_IF_CNTRL";
            btnGetDIG_IF_CNTRL.UseVisualStyleBackColor = true;
            btnGetDIG_IF_CNTRL.Click += btnGetDIG_IF_CNTRL_Click;
            // 
            // btnDebug
            // 
            btnDebug.Location = new Point(1164, 27);
            btnDebug.Name = "btnDebug";
            btnDebug.Size = new Size(153, 54);
            btnDebug.TabIndex = 20;
            btnDebug.Text = "Debug EVM";
            btnDebug.UseVisualStyleBackColor = true;
            btnDebug.Click += btnDebug_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1341, 738);
            Controls.Add(btnDebug);
            Controls.Add(btnGetDIG_IF_CNTRL);
            Controls.Add(btnActPGA);
            Controls.Add(btnGetMetaData);
            Controls.Add(btnFindDUT);
            Controls.Add(btnInitPGA305);
            Controls.Add(btnInitUSB2ANY);
            Controls.Add(btnConnectSTM32);
            Controls.Add(btnGetHW);
            Controls.Add(comboBox1);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private ListBox listBox1;
        private ComboBox comboBox1;
        private Button btnGetHW;
        private Button btnConnectSTM32;
        private Button btnInitUSB2ANY;
        private Button btnInitPGA305;
        private Button btnFindDUT;
        private Label label1;
        private Button btnGetMetaData;
        private Button btnActPGA;
        private Button btnGetDIG_IF_CNTRL;
        private Button btnDebug;
    }
}
