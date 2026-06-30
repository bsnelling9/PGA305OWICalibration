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
            btnExitDebug = new Button();
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
            btnConfigure = new Button();
            btnTestWriteH = new Button();
            SuspendLayout();
            // 
            // btnExitDebug
            // 
            btnExitDebug.Location = new Point(994, 639);
            btnExitDebug.Name = "btnExitDebug";
            btnExitDebug.Size = new Size(85, 41);
            btnExitDebug.TabIndex = 0;
            btnExitDebug.Text = "Exit";
            btnExitDebug.UseVisualStyleBackColor = true;
            btnExitDebug.Click += btnExitDebug_Click;
            // 
            // u2a
            // 
            u2a.Location = new Point(411, 34);
            u2a.Name = "u2a";
            u2a.Size = new Size(119, 48);
            u2a.TabIndex = 2;
            u2a.Text = "Connect to USB2ANY";
            u2a.UseVisualStyleBackColor = true;
            u2a.Click += u2a_Click;
            // 
            // btnActivate
            // 
            btnActivate.Location = new Point(411, 250);
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
            btnHandlePOT.Location = new Point(411, 111);
            btnHandlePOT.Name = "btnHandlePOT";
            btnHandlePOT.Size = new Size(118, 38);
            btnHandlePOT.TabIndex = 22;
            btnHandlePOT.Text = "Handle POT";
            btnHandlePOT.UseVisualStyleBackColor = true;
            btnHandlePOT.Click += btnHandlePOT_Click;
            // 
            // btnInit
            // 
            btnInit.Location = new Point(411, 177);
            btnInit.Name = "btnInit";
            btnInit.Size = new Size(118, 38);
            btnInit.TabIndex = 23;
            btnInit.Text = "Initialize";
            btnInit.UseVisualStyleBackColor = true;
            btnInit.Click += btnInit_Click;
            // 
            // btnReadDevice
            // 
            btnReadDevice.Location = new Point(411, 328);
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
            // btnConfigure
            // 
            btnConfigure.Location = new Point(675, 283);
            btnConfigure.Name = "btnConfigure";
            btnConfigure.Size = new Size(99, 42);
            btnConfigure.TabIndex = 31;
            btnConfigure.Text = "Configure";
            btnConfigure.UseVisualStyleBackColor = true;
            btnConfigure.Click += btnConfigure_Click;
            // 
            // btnTestWriteH
            // 
            btnTestWriteH.Location = new Point(413, 388);
            btnTestWriteH.Name = "btnTestWriteH";
            btnTestWriteH.Size = new Size(115, 40);
            btnTestWriteH.TabIndex = 32;
            btnTestWriteH.Text = "Test Write h";
            btnTestWriteH.UseVisualStyleBackColor = true;
            btnTestWriteH.Click += btnTestWriteH_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1377, 692);
            Controls.Add(btnTestWriteH);
            Controls.Add(btnConfigure);
            Controls.Add(btnReadDevice);
            Controls.Add(btnInit);
            Controls.Add(btnHandlePOT);
            Controls.Add(listBoxDebug);
            Controls.Add(button10);
            Controls.Add(btnExitDebug);
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

        private Button btnExitDebug;
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
        private Button btnConfigure;
        private Button btnTestWriteH;
    }
}