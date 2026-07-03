using PGA305OWICalibration.UIControls;

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
            u2a = new Button();
            btnActivate = new Button();
            button10 = new Button();
            listBoxDebug = new ListBox();
            btnHandlePOT = new Button();
            btnInit = new Button();
            btnReadDevice = new Button();
            lblOutputMode = new Label();
            lblVoltageRange = new Label();
            lstVoltageRange = new ListBox();
            btnOutputV = new ATPButton();
            btnOutputRM = new ATPButton();
            btnOutputC = new ATPButton();
            btnTestWriteH = new Button();
            btnConfigDevice = new ATPButton();
            btnInitHW = new ATPButton();
            btnClose = new ATPButton();
            btnConnectDevice = new ATPButton();
            SuspendLayout();
            // 
            // u2a
            // 
            u2a.Location = new Point(34, 389);
            u2a.Name = "u2a";
            u2a.Size = new Size(119, 48);
            u2a.TabIndex = 2;
            u2a.Text = "Connect to USB2ANY";
            u2a.UseVisualStyleBackColor = true;
            u2a.Click += u2a_Click;
            // 
            // btnActivate
            // 
            btnActivate.Location = new Point(35, 543);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(118, 39);
            btnActivate.TabIndex = 9;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += btnActivate_Click;
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
            listBoxDebug.Location = new Point(35, 31);
            listBoxDebug.Name = "listBoxDebug";
            listBoxDebug.Size = new Size(349, 139);
            listBoxDebug.TabIndex = 21;
            // 
            // btnHandlePOT
            // 
            btnHandlePOT.Location = new Point(35, 443);
            btnHandlePOT.Name = "btnHandlePOT";
            btnHandlePOT.Size = new Size(118, 38);
            btnHandlePOT.TabIndex = 22;
            btnHandlePOT.Text = "Handle POT";
            btnHandlePOT.UseVisualStyleBackColor = true;
            btnHandlePOT.Click += btnHandlePOT_Click;
            // 
            // btnInit
            // 
            btnInit.Location = new Point(35, 499);
            btnInit.Name = "btnInit";
            btnInit.Size = new Size(118, 38);
            btnInit.TabIndex = 23;
            btnInit.Text = "Initialize";
            btnInit.UseVisualStyleBackColor = true;
            btnInit.Click += btnInit_Click;
            // 
            // btnReadDevice
            // 
            btnReadDevice.Location = new Point(35, 588);
            btnReadDevice.Name = "btnReadDevice";
            btnReadDevice.Size = new Size(118, 44);
            btnReadDevice.TabIndex = 24;
            btnReadDevice.Text = "Read Device";
            btnReadDevice.UseVisualStyleBackColor = true;
            btnReadDevice.Click += btnReadDevice_Click;
            // 
            // lblOutputMode
            // 
            lblOutputMode.AutoSize = true;
            lblOutputMode.Font = new Font("Segoe UI", 10F);
            lblOutputMode.Location = new Point(574, 31);
            lblOutputMode.Name = "lblOutputMode";
            lblOutputMode.Size = new Size(184, 19);
            lblOutputMode.TabIndex = 25;
            lblOutputMode.Text = "Select Output Configuration:";
            // 
            // lblVoltageRange
            // 
            lblVoltageRange.AutoSize = true;
            lblVoltageRange.Font = new Font("Segoe UI", 10F);
            lblVoltageRange.Location = new Point(574, 121);
            lblVoltageRange.Name = "lblVoltageRange";
            lblVoltageRange.Size = new Size(139, 19);
            lblVoltageRange.TabIndex = 29;
            lblVoltageRange.Text = "Select Voltage Range:";
            lblVoltageRange.Visible = false;
            // 
            // lstVoltageRange
            // 
            lstVoltageRange.Font = new Font("Segoe UI", 10F);
            lstVoltageRange.ItemHeight = 17;
            lstVoltageRange.Items.AddRange(new object[] { "0-10V", "0-5V", "1-5V", "0.5-4.5V", "1-6V" });
            lstVoltageRange.Location = new Point(574, 146);
            lstVoltageRange.Name = "lstVoltageRange";
            lstVoltageRange.Size = new Size(200, 106);
            lstVoltageRange.TabIndex = 30;
            lstVoltageRange.Visible = false;
            lstVoltageRange.SelectedIndexChanged += lstVoltageRange_SelectedIndexChanged;
            // 
            // btnOutputV
            // 
            btnOutputV.BackColor = Color.White;
            btnOutputV.BorderColor = Color.Black;
            btnOutputV.BorderSize = 2;
            btnOutputV.CornerRadius = 10;
            btnOutputV.Cursor = Cursors.Hand;
            btnOutputV.FlatStyle = FlatStyle.Flat;
            btnOutputV.Font = new Font("Segoe UI", 10F);
            btnOutputV.ForeColor = Color.Black;
            btnOutputV.Location = new Point(574, 61);
            btnOutputV.Name = "btnOutputV";
            btnOutputV.Size = new Size(160, 45);
            btnOutputV.TabIndex = 26;
            btnOutputV.Text = "Voltage";
            btnOutputV.UseVisualStyleBackColor = false;
            btnOutputV.Click += BtnOutputV_Click;
            // 
            // btnOutputRM
            // 
            btnOutputRM.BackColor = Color.White;
            btnOutputRM.BorderColor = Color.Black;
            btnOutputRM.BorderSize = 2;
            btnOutputRM.CornerRadius = 10;
            btnOutputRM.Cursor = Cursors.Hand;
            btnOutputRM.FlatStyle = FlatStyle.Flat;
            btnOutputRM.Font = new Font("Segoe UI", 10F);
            btnOutputRM.ForeColor = Color.Black;
            btnOutputRM.Location = new Point(744, 61);
            btnOutputRM.Name = "btnOutputRM";
            btnOutputRM.Size = new Size(160, 45);
            btnOutputRM.TabIndex = 27;
            btnOutputRM.Text = "Ratio Metric";
            btnOutputRM.UseVisualStyleBackColor = false;
            btnOutputRM.Click += BtnOutputRM_Click;
            // 
            // btnOutputC
            // 
            btnOutputC.BackColor = Color.White;
            btnOutputC.BorderColor = Color.Black;
            btnOutputC.BorderSize = 2;
            btnOutputC.CornerRadius = 10;
            btnOutputC.Cursor = Cursors.Hand;
            btnOutputC.FlatStyle = FlatStyle.Flat;
            btnOutputC.Font = new Font("Segoe UI", 10F);
            btnOutputC.ForeColor = Color.Black;
            btnOutputC.Location = new Point(914, 61);
            btnOutputC.Name = "btnOutputC";
            btnOutputC.Size = new Size(160, 45);
            btnOutputC.TabIndex = 28;
            btnOutputC.Text = "Current";
            btnOutputC.UseVisualStyleBackColor = false;
            btnOutputC.Click += BtnOutputC_Click;
            // 
            // btnTestWriteH
            // 
            btnTestWriteH.Location = new Point(39, 638);
            btnTestWriteH.Name = "btnTestWriteH";
            btnTestWriteH.Size = new Size(115, 40);
            btnTestWriteH.TabIndex = 32;
            btnTestWriteH.Text = "Test Write h";
            btnTestWriteH.UseVisualStyleBackColor = true;
            btnTestWriteH.Click += btnTestWriteH_Click;
            // 
            // btnConfigDevice
            // 
            btnConfigDevice.BackColor = Color.White;
            btnConfigDevice.BorderColor = Color.Black;
            btnConfigDevice.BorderSize = 2;
            btnConfigDevice.CornerRadius = 10;
            btnConfigDevice.Cursor = Cursors.Hand;
            btnConfigDevice.FlatStyle = FlatStyle.Flat;
            btnConfigDevice.Font = new Font("Segoe UI", 10F);
            btnConfigDevice.ForeColor = Color.Black;
            btnConfigDevice.Location = new Point(574, 274);
            btnConfigDevice.Name = "btnConfigDevice";
            btnConfigDevice.Size = new Size(160, 45);
            btnConfigDevice.TabIndex = 35;
            btnConfigDevice.Text = "Configure";
            btnConfigDevice.UseVisualStyleBackColor = false;
            btnConfigDevice.Click += btnConfigDevice_Click;
            // 
            // btnInitHW
            // 
            btnInitHW.BackColor = Color.White;
            btnInitHW.BorderColor = Color.Black;
            btnInitHW.BorderSize = 2;
            btnInitHW.CornerRadius = 10;
            btnInitHW.Cursor = Cursors.Hand;
            btnInitHW.FlatStyle = FlatStyle.Flat;
            btnInitHW.Font = new Font("Segoe UI", 10F);
            btnInitHW.ForeColor = Color.Black;
            btnInitHW.Location = new Point(395, 61);
            btnInitHW.Name = "btnInitHW";
            btnInitHW.Size = new Size(160, 45);
            btnInitHW.TabIndex = 36;
            btnInitHW.Text = "Initialize Hardware";
            btnInitHW.UseVisualStyleBackColor = false;
            btnInitHW.Click += btnInitHW_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.BorderColor = Color.Black;
            btnClose.BorderSize = 2;
            btnClose.CornerRadius = 10;
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(977, 635);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(113, 45);
            btnClose.TabIndex = 38;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnConnectDevice
            // 
            btnConnectDevice.BackColor = Color.White;
            btnConnectDevice.BorderColor = Color.Black;
            btnConnectDevice.BorderSize = 2;
            btnConnectDevice.CornerRadius = 10;
            btnConnectDevice.Cursor = Cursors.Hand;
            btnConnectDevice.FlatStyle = FlatStyle.Flat;
            btnConnectDevice.Font = new Font("Segoe UI", 10F);
            btnConnectDevice.ForeColor = Color.Black;
            btnConnectDevice.Location = new Point(395, 137);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(160, 45);
            btnConnectDevice.TabIndex = 39;
            btnConnectDevice.Text = "Connect to Device";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 692);
            Controls.Add(btnConnectDevice);
            Controls.Add(btnClose);
            Controls.Add(btnInitHW);
            Controls.Add(btnConfigDevice);
            Controls.Add(btnTestWriteH);
            Controls.Add(btnReadDevice);
            Controls.Add(btnInit);
            Controls.Add(btnHandlePOT);
            Controls.Add(listBoxDebug);
            Controls.Add(button10);
            Controls.Add(u2a);
            Controls.Add(btnActivate);
            Controls.Add(lblOutputMode);
            Controls.Add(lblVoltageRange);
            Controls.Add(lstVoltageRange);
            Controls.Add(btnOutputV);
            Controls.Add(btnOutputRM);
            Controls.Add(btnOutputC);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button u2a;
        private Button btnActivate;
        private Button button10;
        private ListBox listBoxDebug;
        private Button btnHandlePOT;
        private Button btnInit;
        private Button btnReadDevice;
        private Label lblOutputMode;
        private ATPButton btnOutputV;
        private ATPButton btnOutputRM;
        private ATPButton btnOutputC;
        private Label lblVoltageRange;
        private ListBox lstVoltageRange;
        private Button btnTestWriteH;
        private ATPButton btnConfigDevice;
        private ATPButton btnInitHW;
        private ATPButton btnClose;
        private ATPButton btnConnectDevice;
    }
}