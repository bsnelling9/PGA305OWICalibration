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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            btnScanHardware = new PGA305OWICalibration.UIControls.ATPButton();
            dgvHardware = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnConnectDevice = new PGA305OWICalibration.UIControls.ATPButton();
            lsbAPT10MetaData = new ListBox();
            btnConfigDevice = new PGA305OWICalibration.UIControls.ATPButton();
            lblOutputMode = new Label();
            lblVoltageRange = new Label();
            lstVoltageRange = new ListBox();
            btnOutputV = new PGA305OWICalibration.UIControls.ATPButton();
            btnOutputRM = new PGA305OWICalibration.UIControls.ATPButton();
            btnOutputC = new PGA305OWICalibration.UIControls.ATPButton();
            btnExit = new PGA305OWICalibration.UIControls.ATPButton();
            label1 = new Label();
            label2 = new Label();
            btnNoPChange = new PGA305OWICalibration.UIControls.ATPButton();
            lblPressureRange = new Label();
            numMinPressure = new NumericUpDown();
            lblMinPressure = new Label();
            numMaxPressure = new NumericUpDown();
            lblMaxPressure = new Label();
            lsbOutputConfig = new ListBox();
            btnUnitBar = new PGA305OWICalibration.UIControls.ATPButton();
            btnUnitPsi = new PGA305OWICalibration.UIControls.ATPButton();
            lblSelectUnit = new Label();
            lblPressureError = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvHardware).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).BeginInit();
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
            btnScanHardware.Location = new Point(12, 33);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvHardware.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvHardware.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvHardware.DefaultCellStyle = dataGridViewCellStyle2;
            dgvHardware.Location = new Point(12, 136);
            dgvHardware.Name = "dgvHardware";
            dgvHardware.ReadOnly = true;
            dgvHardware.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHardware.Size = new Size(422, 152);
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
            btnConnectDevice.Location = new Point(12, 353);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(160, 45);
            btnConnectDevice.TabIndex = 21;
            btnConnectDevice.Text = "Connect to APT10";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
            // 
            // lsbAPT10MetaData
            // 
            lsbAPT10MetaData.FormattingEnabled = true;
            lsbAPT10MetaData.ItemHeight = 15;
            lsbAPT10MetaData.Location = new Point(12, 404);
            lsbAPT10MetaData.Name = "lsbAPT10MetaData";
            lsbAPT10MetaData.Size = new Size(330, 124);
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
            btnConfigDevice.Location = new Point(923, 318);
            btnConfigDevice.Name = "btnConfigDevice";
            btnConfigDevice.Size = new Size(160, 45);
            btnConfigDevice.TabIndex = 42;
            btnConfigDevice.Text = "Configure";
            btnConfigDevice.UseVisualStyleBackColor = false;
            btnConfigDevice.Click += btnConfigDevice_Click;
            // 
            // lblOutputMode
            // 
            lblOutputMode.AutoSize = true;
            lblOutputMode.Font = new Font("Segoe UI", 10F);
            lblOutputMode.Location = new Point(583, 21);
            lblOutputMode.Name = "lblOutputMode";
            lblOutputMode.Size = new Size(184, 19);
            lblOutputMode.TabIndex = 36;
            lblOutputMode.Text = "Select Output Configuration:";
            // 
            // lblVoltageRange
            // 
            lblVoltageRange.AutoSize = true;
            lblVoltageRange.Font = new Font("Segoe UI", 10F);
            lblVoltageRange.Location = new Point(583, 111);
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
            lstVoltageRange.Location = new Point(583, 136);
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
            btnOutputV.Location = new Point(583, 51);
            btnOutputV.Name = "btnOutputV";
            btnOutputV.Size = new Size(160, 45);
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
            btnOutputRM.Location = new Point(753, 51);
            btnOutputRM.Name = "btnOutputRM";
            btnOutputRM.Size = new Size(160, 45);
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
            btnOutputC.Location = new Point(923, 51);
            btnOutputC.Name = "btnOutputC";
            btnOutputC.Size = new Size(160, 45);
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
            label1.Location = new Point(16, 12);
            label1.Name = "label1";
            label1.Size = new Size(228, 19);
            label1.TabIndex = 44;
            label1.Text = "Click Scan Harware Button to begin:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(16, 115);
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
            btnNoPChange.Location = new Point(583, 288);
            btnNoPChange.Name = "btnNoPChange";
            btnNoPChange.Size = new Size(128, 45);
            btnNoPChange.TabIndex = 46;
            btnNoPChange.Text = "No Change";
            btnNoPChange.UseVisualStyleBackColor = false;
            btnNoPChange.Click += btnNoPChange_Click;
            // 
            // lblPressureRange
            // 
            lblPressureRange.AutoSize = true;
            lblPressureRange.Font = new Font("Segoe UI", 10F);
            lblPressureRange.Location = new Point(583, 264);
            lblPressureRange.Name = "lblPressureRange";
            lblPressureRange.Size = new Size(128, 19);
            lblPressureRange.TabIndex = 47;
            lblPressureRange.Text = "Configure Pressure:";
            lblPressureRange.Visible = false;
            // 
            // numMinPressure
            // 
            numMinPressure.Location = new Point(584, 456);
            numMinPressure.Name = "numMinPressure";
            numMinPressure.Size = new Size(82, 23);
            numMinPressure.TabIndex = 48;
            numMinPressure.ValueChanged += numMinPressure_ValueChanged;
            // 
            // lblMinPressure
            // 
            lblMinPressure.AutoSize = true;
            lblMinPressure.Font = new Font("Segoe UI", 10F);
            lblMinPressure.Location = new Point(584, 434);
            lblMinPressure.Name = "lblMinPressure";
            lblMinPressure.Size = new Size(92, 19);
            lblMinPressure.TabIndex = 49;
            lblMinPressure.Text = "Min Pressure:";
            lblMinPressure.Visible = false;
            // 
            // numMaxPressure
            // 
            numMaxPressure.Location = new Point(712, 456);
            numMaxPressure.Name = "numMaxPressure";
            numMaxPressure.Size = new Size(82, 23);
            numMaxPressure.TabIndex = 50;
            numMaxPressure.ValueChanged += numMaxPressure_ValueChanged;
            // 
            // lblMaxPressure
            // 
            lblMaxPressure.AutoSize = true;
            lblMaxPressure.Font = new Font("Segoe UI", 10F);
            lblMaxPressure.Location = new Point(712, 434);
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
            lsbOutputConfig.Location = new Point(903, 147);
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
            btnUnitBar.Location = new Point(584, 366);
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
            btnUnitPsi.Location = new Point(665, 366);
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
            lblSelectUnit.Location = new Point(584, 344);
            lblSelectUnit.Name = "lblSelectUnit";
            lblSelectUnit.Size = new Size(74, 19);
            lblSelectUnit.TabIndex = 55;
            lblSelectUnit.Text = "Select Unit";
            lblSelectUnit.Visible = false;
            // 
            // lblPressureError
            // 
            lblPressureError.AutoSize = true;
            lblPressureError.Font = new Font("Segoe UI", 10F);
            lblPressureError.Location = new Point(583, 492);
            lblPressureError.Name = "lblPressureError";
            lblPressureError.Size = new Size(45, 19);
            lblPressureError.TabIndex = 56;
            lblPressureError.Text = "label3";
            lblPressureError.Click += lblPressureError_Click;
            // 
            // I2COutputConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1261, 674);
            Controls.Add(lblPressureError);
            Controls.Add(lblSelectUnit);
            Controls.Add(btnUnitPsi);
            Controls.Add(btnUnitBar);
            Controls.Add(lsbOutputConfig);
            Controls.Add(lblMaxPressure);
            Controls.Add(numMaxPressure);
            Controls.Add(lblMinPressure);
            Controls.Add(numMinPressure);
            Controls.Add(lblPressureRange);
            Controls.Add(btnNoPChange);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnExit);
            Controls.Add(btnConfigDevice);
            Controls.Add(lblOutputMode);
            Controls.Add(lblVoltageRange);
            Controls.Add(lstVoltageRange);
            Controls.Add(btnOutputV);
            Controls.Add(btnOutputRM);
            Controls.Add(btnOutputC);
            Controls.Add(lsbAPT10MetaData);
            Controls.Add(btnConnectDevice);
            Controls.Add(dgvHardware);
            Controls.Add(btnScanHardware);
            Name = "I2COutputConfigForm";
            Text = "I2COutputConfigForm";
            ((System.ComponentModel.ISupportInitialize)dgvHardware).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).EndInit();
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
        private Label lblOutputMode;
        private Label lblVoltageRange;
        private ListBox lstVoltageRange;
        private UIControls.ATPButton btnOutputV;
        private UIControls.ATPButton btnOutputRM;
        private UIControls.ATPButton btnOutputC;
        private UIControls.ATPButton btnExit;
        private Label label1;
        private Label label2;
        private UIControls.ATPButton btnNoPChange;
        private Label lblPressureRange;
        private NumericUpDown numMinPressure;
        private Label lblMinPressure;
        private NumericUpDown numMaxPressure;
        private Label lblMaxPressure;
        private ListBox lsbOutputConfig;
        private UIControls.ATPButton btnUnitBar;
        private UIControls.ATPButton btnUnitPsi;
        private Label lblSelectUnit;
        private Label lblPressureError;
    }
}