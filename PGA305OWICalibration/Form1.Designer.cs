using PGA305OWICalibration.UIControls;

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
            tabControl1 = new TabControl();
            TabMainPage = new TabPage();
            btnDebug = new ATPButton();
            listBox1 = new ListBox();
            btnActPGA = new ATPButton();
            btnGetMetaData = new ATPButton();
            btnFindDUT = new ATPButton();
            btnInitPGA305 = new ATPButton();
            btnInitUSB2ANY = new ATPButton();
            btnConnectSTM32 = new ATPButton();
            btnGetHW = new ATPButton();
            comboBox1 = new ComboBox();
            hardwareTab = new TabPage();
            deviceTab = new TabPage();
            tabPage4 = new TabPage();
            tabControl1.SuspendLayout();
            TabMainPage.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(TabMainPage);
            tabControl1.Controls.Add(hardwareTab);
            tabControl1.Controls.Add(deviceTab);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1685, 738);
            tabControl1.TabIndex = 21;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // TabMainPage
            // 
            TabMainPage.Controls.Add(btnDebug);
            TabMainPage.Controls.Add(listBox1);
            TabMainPage.Controls.Add(btnActPGA);
            TabMainPage.Controls.Add(btnGetMetaData);
            TabMainPage.Controls.Add(btnFindDUT);
            TabMainPage.Controls.Add(btnInitPGA305);
            TabMainPage.Controls.Add(btnInitUSB2ANY);
            TabMainPage.Controls.Add(btnConnectSTM32);
            TabMainPage.Controls.Add(btnGetHW);
            TabMainPage.Controls.Add(comboBox1);
            TabMainPage.ForeColor = SystemColors.ControlText;
            TabMainPage.Location = new Point(4, 24);
            TabMainPage.Name = "TabMainPage";
            TabMainPage.Padding = new Padding(3);
            TabMainPage.Size = new Size(1677, 710);
            TabMainPage.TabIndex = 0;
            TabMainPage.Text = "Main";
            TabMainPage.UseVisualStyleBackColor = true;
            // 
            // btnDebug
            // 
            btnDebug.BackColor = Color.White;
            btnDebug.BorderColor = Color.Black;
            btnDebug.BorderSize = 2;
            btnDebug.CornerRadius = 10;
            btnDebug.FlatStyle = FlatStyle.Flat;
            btnDebug.Font = new Font("Segoe UI", 10F);
            btnDebug.ForeColor = Color.Black;
            btnDebug.Location = new Point(1478, 6);
            btnDebug.Name = "btnDebug";
            btnDebug.Size = new Size(150, 50);
            btnDebug.TabIndex = 40;
            btnDebug.Text = "Debug EVM";
            btnDebug.UseVisualStyleBackColor = true;
            btnDebug.Click += btnDebug_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(397, 133);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(739, 319);
            listBox1.TabIndex = 30;
            // 
            // btnActPGA
            // 
            btnActPGA.BackColor = Color.White;
            btnActPGA.BorderColor = Color.Black;
            btnActPGA.BorderSize = 2;
            btnActPGA.CornerRadius = 10;
            btnActPGA.FlatStyle = FlatStyle.Flat;
            btnActPGA.Font = new Font("Segoe UI", 10F);
            btnActPGA.ForeColor = Color.Black;
            btnActPGA.Location = new Point(30, 457);
            btnActPGA.Name = "btnActPGA";
            btnActPGA.Size = new Size(150, 50);
            btnActPGA.TabIndex = 29;
            btnActPGA.Text = "Activate";
            btnActPGA.UseVisualStyleBackColor = true;
            btnActPGA.Click += btnActPGA_Click;
            // 
            // btnGetMetaData
            // 
            btnGetMetaData.BackColor = Color.White;
            btnGetMetaData.BorderColor = Color.Black;
            btnGetMetaData.BorderSize = 2;
            btnGetMetaData.CornerRadius = 10;
            btnGetMetaData.FlatStyle = FlatStyle.Flat;
            btnGetMetaData.Font = new Font("Segoe UI", 10F);
            btnGetMetaData.ForeColor = Color.Black;
            btnGetMetaData.Location = new Point(30, 538);
            btnGetMetaData.Name = "btnGetMetaData";
            btnGetMetaData.Size = new Size(150, 50);
            btnGetMetaData.TabIndex = 28;
            btnGetMetaData.Text = "Get Meta Data";
            btnGetMetaData.UseVisualStyleBackColor = true;
            btnGetMetaData.Click += btnGetMetaData_Click;
            // 
            // btnFindDUT
            // 
            btnFindDUT.BackColor = Color.White;
            btnFindDUT.BorderColor = Color.Black;
            btnFindDUT.BorderSize = 2;
            btnFindDUT.CornerRadius = 10;
            btnFindDUT.FlatStyle = FlatStyle.Flat;
            btnFindDUT.Font = new Font("Segoe UI", 10F);
            btnFindDUT.ForeColor = Color.Black;
            btnFindDUT.Location = new Point(30, 295);
            btnFindDUT.Name = "btnFindDUT";
            btnFindDUT.Size = new Size(150, 50);
            btnFindDUT.TabIndex = 27;
            btnFindDUT.Text = "Find DUT";
            btnFindDUT.UseVisualStyleBackColor = true;
            btnFindDUT.Click += btnFindDUT_Click;
            // 
            // btnInitPGA305
            // 
            btnInitPGA305.BackColor = Color.White;
            btnInitPGA305.BorderColor = Color.Black;
            btnInitPGA305.BorderSize = 2;
            btnInitPGA305.CornerRadius = 10;
            btnInitPGA305.FlatStyle = FlatStyle.Flat;
            btnInitPGA305.Font = new Font("Segoe UI", 10F);
            btnInitPGA305.ForeColor = Color.Black;
            btnInitPGA305.Location = new Point(30, 376);
            btnInitPGA305.Name = "btnInitPGA305";
            btnInitPGA305.Size = new Size(150, 50);
            btnInitPGA305.TabIndex = 26;
            btnInitPGA305.Text = "Initialize PGA305";
            btnInitPGA305.UseVisualStyleBackColor = true;
            btnInitPGA305.Click += btnInitPGA305_Click;
            // 
            // btnInitUSB2ANY
            // 
            btnInitUSB2ANY.BackColor = Color.White;
            btnInitUSB2ANY.BorderColor = Color.Black;
            btnInitUSB2ANY.BorderSize = 2;
            btnInitUSB2ANY.CornerRadius = 10;
            btnInitUSB2ANY.FlatStyle = FlatStyle.Flat;
            btnInitUSB2ANY.Font = new Font("Segoe UI", 10F);
            btnInitUSB2ANY.ForeColor = Color.Black;
            btnInitUSB2ANY.Location = new Point(30, 214);
            btnInitUSB2ANY.Name = "btnInitUSB2ANY";
            btnInitUSB2ANY.Size = new Size(150, 50);
            btnInitUSB2ANY.TabIndex = 25;
            btnInitUSB2ANY.Text = "Connect USB2ANY";
            btnInitUSB2ANY.UseVisualStyleBackColor = true;
            btnInitUSB2ANY.Click += btnInitUSB2ANY_Click;
            // 
            // btnConnectSTM32
            // 
            btnConnectSTM32.BackColor = Color.White;
            btnConnectSTM32.BorderColor = Color.Black;
            btnConnectSTM32.BorderSize = 2;
            btnConnectSTM32.CornerRadius = 10;
            btnConnectSTM32.FlatStyle = FlatStyle.Flat;
            btnConnectSTM32.Font = new Font("Segoe UI", 10F);
            btnConnectSTM32.ForeColor = Color.Black;
            btnConnectSTM32.Location = new Point(30, 133);
            btnConnectSTM32.Name = "btnConnectSTM32";
            btnConnectSTM32.Size = new Size(150, 50);
            btnConnectSTM32.TabIndex = 24;
            btnConnectSTM32.Text = "Connect to STM32";
            btnConnectSTM32.UseVisualStyleBackColor = true;
            btnConnectSTM32.Click += btnConnectSTM32_Click;
            // 
            // btnGetHW
            // 
            btnGetHW.BackColor = Color.White;
            btnGetHW.BorderColor = Color.Black;
            btnGetHW.BorderSize = 2;
            btnGetHW.CornerRadius = 10;
            btnGetHW.FlatStyle = FlatStyle.Flat;
            btnGetHW.Font = new Font("Segoe UI", 10F);
            btnGetHW.ForeColor = Color.Black;
            btnGetHW.Location = new Point(33, 61);
            btnGetHW.Name = "btnGetHW";
            btnGetHW.Size = new Size(150, 50);
            btnGetHW.TabIndex = 23;
            btnGetHW.Text = "Get STM32";
            btnGetHW.UseVisualStyleBackColor = true;
            btnGetHW.Click += btnGetHW_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(409, 61);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(269, 23);
            comboBox1.TabIndex = 22;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // hardwareTab
            // 
            hardwareTab.Location = new Point(4, 24);
            hardwareTab.Name = "hardwareTab";
            hardwareTab.Padding = new Padding(3);
            hardwareTab.Size = new Size(1677, 710);
            hardwareTab.TabIndex = 1;
            hardwareTab.Text = "Hardware";
            hardwareTab.UseVisualStyleBackColor = true;
            // 
            // deviceTab
            // 
            deviceTab.Location = new Point(4, 24);
            deviceTab.Name = "deviceTab";
            deviceTab.Padding = new Padding(3);
            deviceTab.Size = new Size(1677, 710);
            deviceTab.TabIndex = 2;
            deviceTab.Text = "Device";
            deviceTab.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1677, 710);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1685, 738);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "PGA305 OWI Calibration";
            tabControl1.ResumeLayout(false);
            TabMainPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TabControl tabControl1;
        private TabPage TabMainPage;
        private TabPage hardwareTab;
        private TabPage deviceTab;
        private TabPage tabPage4;
        private ATPButton btnActPGA;
        private ATPButton btnGetMetaData;
        private ATPButton btnFindDUT;
        private ATPButton btnInitPGA305;
        private ATPButton btnInitUSB2ANY;
        private ATPButton btnConnectSTM32;
        private ATPButton btnGetHW;
        private ComboBox comboBox1;
        private ATPButton btnDebug;
        private ListBox listBox1;
    }
}
