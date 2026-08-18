namespace PGA305OWICalibration
{
    partial class I2COutputConfigForm
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            btnScanHardware = new PGA305OWICalibration.UIControls.ATPButton();
            dgvHardware = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnConnectDevice = new PGA305OWICalibration.UIControls.ATPButton();
            lsbAPT10MetaData = new ListBox();
            btnConfigDevice = new PGA305OWICalibration.UIControls.ATPButton();
            lblVoltageRange = new Label();
            lstVoltageRange = new ListBox();
            btnOutputV = new PGA305OWICalibration.UIControls.ATPButton();
            btnOutputRM = new PGA305OWICalibration.UIControls.ATPButton();
            btnOutputC = new PGA305OWICalibration.UIControls.ATPButton();
            btnExit = new PGA305OWICalibration.UIControls.ATPButton();
            label1 = new Label();
            label2 = new Label();
            btnNoPChange = new PGA305OWICalibration.UIControls.ATPButton();
            numMinPressure = new NumericUpDown();
            lblMinPressure = new Label();
            numMaxPressure = new NumericUpDown();
            lblMaxPressure = new Label();
            lsbOutputConfig = new ListBox();
            btnUnitBar = new PGA305OWICalibration.UIControls.ATPButton();
            btnUnitPsi = new PGA305OWICalibration.UIControls.ATPButton();
            lblSelectUnit = new Label();
            lblConfigueSensor = new Label();
            gbxScanHardware = new GroupBox();
            gbxConnectDevice = new GroupBox();
            gbxConfigOutput = new GroupBox();
            gbxConfigPressure = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvHardware).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).BeginInit();
            gbxScanHardware.SuspendLayout();
            gbxConnectDevice.SuspendLayout();
            gbxConfigOutput.SuspendLayout();
            gbxConfigPressure.SuspendLayout();
            SuspendLayout();
            // 
            // btnScanHardware
            // 
            btnScanHardware.BackColor = Color.White;
            btnScanHardware.BorderColor = Color.Black;
            btnScanHardware.BorderSize = 2;
            btnScanHardware.CornerRadius = 10;
            btnScanHardware.Cursor = Cursors.Hand;
            btnScanHardware.FlatStyle = FlatStyle.Flat;
            btnScanHardware.Font = new Font("Segoe UI", 10F);
            btnScanHardware.ForeColor = Color.Black;
            btnScanHardware.Location = new Point(9, 43);
            btnScanHardware.Name = "btnScanHardware";
            btnScanHardware.Size = new Size(160, 45);
            btnScanHardware.TabIndex = 19;
            btnScanHardware.Text = "Scan Hardware";
            btnScanHardware.UseVisualStyleBackColor = false;
            btnScanHardware.Click += btnScanHardware_Click;
            // 
            // dgvHardware
            // 
            dgvHardware.AllowUserToAddRows = false;
            dgvHardware.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvHardware.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHardware.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvHardware.Location = new Point(13, 113);
            dgvHardware.Name = "dgvHardware";
            dgvHardware.ReadOnly = true;
            dgvHardware.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHardware.Size = new Size(497, 152);
            dgvHardware.TabIndex = 20;
            dgvHardware.CellClick += dgvHardware_CellClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Device";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Port";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Status";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
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
            btnConnectDevice.Location = new Point(6, 22);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(160, 45);
            btnConnectDevice.TabIndex = 21;
            btnConnectDevice.Text = "Connect to APT10";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
            // 
            // lsbAPT10MetaData
            // 
            lsbAPT10MetaData.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lsbAPT10MetaData.FormattingEnabled = true;
            lsbAPT10MetaData.ItemHeight = 15;
            lsbAPT10MetaData.Location = new Point(180, 24);
            lsbAPT10MetaData.Name = "lsbAPT10MetaData";
            lsbAPT10MetaData.Size = new Size(330, 214);
            lsbAPT10MetaData.TabIndex = 31;
            lsbAPT10MetaData.SelectedIndexChanged += lsbAPT10MetaData_SelectedIndexChanged;
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
            btnConfigDevice.Location = new Point(1005, 294);
            btnConfigDevice.Name = "btnConfigDevice";
            btnConfigDevice.Size = new Size(160, 45);
            btnConfigDevice.TabIndex = 42;
            btnConfigDevice.Text = "Configure";
            btnConfigDevice.UseVisualStyleBackColor = false;
            btnConfigDevice.Click += btnConfigDevice_Click;
            // 
            // lblVoltageRange
            // 
            lblVoltageRange.AutoSize = true;
            lblVoltageRange.Font = new Font("Segoe UI", 10F);
            lblVoltageRange.Location = new Point(16, 91);
            lblVoltageRange.Name = "lblVoltageRange";
            lblVoltageRange.Size = new Size(139, 19);
            lblVoltageRange.TabIndex = 40;
            lblVoltageRange.Text = "Select Voltage Range:";
            lblVoltageRange.Visible = false;
            // 
            // lstVoltageRange
            // 
            lstVoltageRange.Font = new Font("Segoe UI", 10F);
            lstVoltageRange.ItemHeight = 17;
            lstVoltageRange.Items.AddRange(new object[] { "0-10V", "0-5V", "1-5V", "0.5-4.5V", "1-6V" });
            lstVoltageRange.Location = new Point(15, 113);
            lstVoltageRange.Name = "lstVoltageRange";
            lstVoltageRange.Size = new Size(200, 106);
            lstVoltageRange.TabIndex = 41;
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
            btnOutputV.Location = new Point(15, 35);
            btnOutputV.Name = "btnOutputV";
            btnOutputV.Size = new Size(107, 45);
            btnOutputV.TabIndex = 37;
            btnOutputV.Text = "Voltage";
            btnOutputV.UseVisualStyleBackColor = false;
            btnOutputV.Click += btnOutputV_Click;
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
            btnOutputRM.Location = new Point(143, 36);
            btnOutputRM.Name = "btnOutputRM";
            btnOutputRM.Size = new Size(110, 45);
            btnOutputRM.TabIndex = 38;
            btnOutputRM.Text = "Ratio Metric";
            btnOutputRM.UseVisualStyleBackColor = false;
            btnOutputRM.Click += btnOutputRM_Click;
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
            btnOutputC.Location = new Point(274, 36);
            btnOutputC.Name = "btnOutputC";
            btnOutputC.Size = new Size(104, 45);
            btnOutputC.TabIndex = 39;
            btnOutputC.Text = "Current";
            btnOutputC.UseVisualStyleBackColor = false;
            btnOutputC.Click += btnOutputC_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.White;
            btnExit.BorderColor = Color.Black;
            btnExit.BorderSize = 2;
            btnExit.CornerRadius = 10;
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.ForeColor = Color.Black;
            btnExit.Location = new Point(1136, 617);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(113, 45);
            btnExit.TabIndex = 43;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(9, 21);
            label1.Name = "label1";
            label1.Size = new Size(228, 19);
            label1.TabIndex = 44;
            label1.Text = "Click Scan Harware Button to begin:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(9, 91);
            label2.Name = "label2";
            label2.Size = new Size(303, 19);
            label2.TabIndex = 45;
            label2.Text = "Double click on the row to connect to hardware:";
            // 
            // btnNoPChange
            // 
            btnNoPChange.BackColor = Color.White;
            btnNoPChange.BorderColor = Color.Black;
            btnNoPChange.BorderSize = 2;
            btnNoPChange.CornerRadius = 10;
            btnNoPChange.Cursor = Cursors.Hand;
            btnNoPChange.FlatStyle = FlatStyle.Flat;
            btnNoPChange.Font = new Font("Segoe UI", 10F);
            btnNoPChange.ForeColor = Color.Black;
            btnNoPChange.Location = new Point(18, 37);
            btnNoPChange.Name = "btnNoPChange";
            btnNoPChange.Size = new Size(107, 45);
            btnNoPChange.TabIndex = 46;
            btnNoPChange.Text = "No Change";
            btnNoPChange.UseVisualStyleBackColor = false;
            btnNoPChange.Click += btnNoPChange_Click;
            // 
            // numMinPressure
            // 
            numMinPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMinPressure.Location = new Point(27, 139);
            numMinPressure.Name = "numMinPressure";
            numMinPressure.Size = new Size(82, 25);
            numMinPressure.TabIndex = 48;
            numMinPressure.ValueChanged += numMinPressure_ValueChanged;
            // 
            // lblMinPressure
            // 
            lblMinPressure.AutoSize = true;
            lblMinPressure.Font = new Font("Segoe UI", 10F);
            lblMinPressure.Location = new Point(27, 117);
            lblMinPressure.Name = "lblMinPressure";
            lblMinPressure.Size = new Size(92, 19);
            lblMinPressure.TabIndex = 49;
            lblMinPressure.Text = "Min Pressure:";
            lblMinPressure.Visible = false;
            // 
            // numMaxPressure
            // 
            numMaxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMaxPressure.Location = new Point(155, 139);
            numMaxPressure.Name = "numMaxPressure";
            numMaxPressure.Size = new Size(82, 25);
            numMaxPressure.TabIndex = 50;
            numMaxPressure.ValueChanged += numMaxPressure_ValueChanged;
            // 
            // lblMaxPressure
            // 
            lblMaxPressure.AutoSize = true;
            lblMaxPressure.Font = new Font("Segoe UI", 10F);
            lblMaxPressure.Location = new Point(155, 117);
            lblMaxPressure.Name = "lblMaxPressure";
            lblMaxPressure.Size = new Size(94, 19);
            lblMaxPressure.TabIndex = 51;
            lblMaxPressure.Text = "Max Pressure:";
            lblMaxPressure.Visible = false;
            // 
            // lsbOutputConfig
            // 
            lsbOutputConfig.Font = new Font("Segoe UI", 10F);
            lsbOutputConfig.FormattingEnabled = true;
            lsbOutputConfig.ItemHeight = 17;
            lsbOutputConfig.Location = new Point(991, 143);
            lsbOutputConfig.Name = "lsbOutputConfig";
            lsbOutputConfig.Size = new Size(219, 140);
            lsbOutputConfig.TabIndex = 52;
            lsbOutputConfig.SelectedIndexChanged += lsbOutputConfig_SelectedIndexChanged;
            // 
            // btnUnitBar
            // 
            btnUnitBar.BackColor = Color.White;
            btnUnitBar.BorderColor = Color.Black;
            btnUnitBar.BorderSize = 2;
            btnUnitBar.CornerRadius = 10;
            btnUnitBar.Cursor = Cursors.Hand;
            btnUnitBar.FlatStyle = FlatStyle.Flat;
            btnUnitBar.Font = new Font("Segoe UI", 10F);
            btnUnitBar.ForeColor = Color.Black;
            btnUnitBar.Location = new Point(160, 37);
            btnUnitBar.Name = "btnUnitBar";
            btnUnitBar.Size = new Size(57, 45);
            btnUnitBar.TabIndex = 53;
            btnUnitBar.Text = "Bar";
            btnUnitBar.UseVisualStyleBackColor = false;
            btnUnitBar.Click += btnUnitBar_Click;
            // 
            // btnUnitPsi
            // 
            btnUnitPsi.BackColor = Color.White;
            btnUnitPsi.BorderColor = Color.Black;
            btnUnitPsi.BorderSize = 2;
            btnUnitPsi.CornerRadius = 10;
            btnUnitPsi.Cursor = Cursors.Hand;
            btnUnitPsi.FlatStyle = FlatStyle.Flat;
            btnUnitPsi.Font = new Font("Segoe UI", 10F);
            btnUnitPsi.ForeColor = Color.Black;
            btnUnitPsi.Location = new Point(241, 37);
            btnUnitPsi.Name = "btnUnitPsi";
            btnUnitPsi.Size = new Size(57, 45);
            btnUnitPsi.TabIndex = 54;
            btnUnitPsi.Text = "psi";
            btnUnitPsi.UseVisualStyleBackColor = false;
            btnUnitPsi.Click += btnUnitPsi_Click;
            // 
            // lblSelectUnit
            // 
            lblSelectUnit.AutoSize = true;
            lblSelectUnit.Font = new Font("Segoe UI", 10F);
            lblSelectUnit.Location = new Point(160, 15);
            lblSelectUnit.Name = "lblSelectUnit";
            lblSelectUnit.Size = new Size(77, 19);
            lblSelectUnit.TabIndex = 55;
            lblSelectUnit.Text = "Select Unit:";
            lblSelectUnit.Visible = false;
            // 
            // lblConfigueSensor
            // 
            lblConfigueSensor.AutoSize = true;
            lblConfigueSensor.Font = new Font("Segoe UI", 10F);
            lblConfigueSensor.Location = new Point(991, 121);
            lblConfigueSensor.Name = "lblConfigueSensor";
            lblConfigueSensor.Size = new Size(219, 19);
            lblConfigueSensor.TabIndex = 58;
            lblConfigueSensor.Text = "Confirm and Configure Transducer";
            lblConfigueSensor.Visible = false;
            // 
            // gbxScanHardware
            // 
            gbxScanHardware.Controls.Add(label2);
            gbxScanHardware.Controls.Add(label1);
            gbxScanHardware.Controls.Add(dgvHardware);
            gbxScanHardware.Controls.Add(btnScanHardware);
            gbxScanHardware.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxScanHardware.Location = new Point(16, 12);
            gbxScanHardware.Name = "gbxScanHardware";
            gbxScanHardware.Size = new Size(516, 281);
            gbxScanHardware.TabIndex = 59;
            gbxScanHardware.TabStop = false;
            gbxScanHardware.Text = "Setup Hardware";
            // 
            // gbxConnectDevice
            // 
            gbxConnectDevice.Controls.Add(lsbAPT10MetaData);
            gbxConnectDevice.Controls.Add(btnConnectDevice);
            gbxConnectDevice.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxConnectDevice.Location = new Point(16, 304);
            gbxConnectDevice.Name = "gbxConnectDevice";
            gbxConnectDevice.Size = new Size(517, 244);
            gbxConnectDevice.TabIndex = 60;
            gbxConnectDevice.TabStop = false;
            gbxConnectDevice.Text = "Connect to Device";
            // 
            // gbxConfigOutput
            // 
            gbxConfigOutput.Controls.Add(btnOutputV);
            gbxConfigOutput.Controls.Add(btnOutputRM);
            gbxConfigOutput.Controls.Add(btnOutputC);
            gbxConfigOutput.Controls.Add(lstVoltageRange);
            gbxConfigOutput.Controls.Add(lblVoltageRange);
            gbxConfigOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxConfigOutput.Location = new Point(550, 7);
            gbxConfigOutput.Name = "gbxConfigOutput";
            gbxConfigOutput.Size = new Size(402, 225);
            gbxConfigOutput.TabIndex = 61;
            gbxConfigOutput.TabStop = false;
            gbxConfigOutput.Text = "Configure Output";
            // 
            // gbxConfigPressure
            // 
            gbxConfigPressure.Controls.Add(lblSelectUnit);
            gbxConfigPressure.Controls.Add(btnUnitPsi);
            gbxConfigPressure.Controls.Add(btnUnitBar);
            gbxConfigPressure.Controls.Add(lblMaxPressure);
            gbxConfigPressure.Controls.Add(numMaxPressure);
            gbxConfigPressure.Controls.Add(lblMinPressure);
            gbxConfigPressure.Controls.Add(numMinPressure);
            gbxConfigPressure.Controls.Add(btnNoPChange);
            gbxConfigPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxConfigPressure.Location = new Point(548, 242);
            gbxConfigPressure.Name = "gbxConfigPressure";
            gbxConfigPressure.Size = new Size(406, 189);
            gbxConfigPressure.TabIndex = 62;
            gbxConfigPressure.TabStop = false;
            gbxConfigPressure.Text = "Configure Pressure";
            // 
            // I2COutputConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1261, 674);
            Controls.Add(gbxConfigPressure);
            Controls.Add(gbxConfigOutput);
            Controls.Add(gbxConnectDevice);
            Controls.Add(gbxScanHardware);
            Controls.Add(lblConfigueSensor);
            Controls.Add(lsbOutputConfig);
            Controls.Add(btnExit);
            Controls.Add(btnConfigDevice);
            Name = "I2COutputConfigForm";
            Text = "I2COutputConfigForm";
            ((System.ComponentModel.ISupportInitialize)dgvHardware).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).EndInit();
            gbxScanHardware.ResumeLayout(false);
            gbxScanHardware.PerformLayout();
            gbxConnectDevice.ResumeLayout(false);
            gbxConfigOutput.ResumeLayout(false);
            gbxConfigOutput.PerformLayout();
            gbxConfigPressure.ResumeLayout(false);
            gbxConfigPressure.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UIControls.ATPButton btnScanHardware;
        private DataGridView dgvHardware;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private UIControls.ATPButton btnConnectDevice;
        private ListBox lsbAPT10MetaData;
        private UIControls.ATPButton btnConfigDevice;
        private Label lblVoltageRange;
        private ListBox lstVoltageRange;
        private UIControls.ATPButton btnOutputV;
        private UIControls.ATPButton btnOutputRM;
        private UIControls.ATPButton btnOutputC;
        private UIControls.ATPButton btnExit;
        private Label label1;
        private Label label2;
        private UIControls.ATPButton btnNoPChange;
        private NumericUpDown numMinPressure;
        private Label lblMinPressure;
        private NumericUpDown numMaxPressure;
        private Label lblMaxPressure;
        private ListBox lsbOutputConfig;
        private UIControls.ATPButton btnUnitBar;
        private UIControls.ATPButton btnUnitPsi;
        private Label lblSelectUnit;
        private Label lblConfigueSensor;
        private GroupBox gbxScanHardware;
        private GroupBox gbxConnectDevice;
        private GroupBox gbxConfigOutput;
        private GroupBox gbxConfigPressure;
    }
}