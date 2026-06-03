using PGA305OWICalibration.UIControls;
using System.Windows.Forms;
using System.Drawing;
using PGA305OWICalibration.UIControls;

namespace PGA305OWICalibration.Tabs
{
    partial class HardwareTab : UserControl
    {
        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblPorts = new Label();
            cmbPorts = new ComboBox();
            btnGetPorts = new ATPButton();
            btnConnectSTM32 = new ATPButton();
            lblSTM32Status = new Label();
            btnConnectUSB2ANY = new ATPButton();
            lblUSB2ANYStatus = new Label();
            lblComp = new Label();
            chkVCOMPA0 = new CheckBox();
            chkVCOMPA1 = new CheckBox();
            btnSetCompensation = new ATPButton();
            lblCompensationStatus = new Label();
            lblRelay = new Label();
            rdoOWI = new RadioButton();
            rdoVO = new RadioButton();
            rdoMA = new RadioButton();
            btnSetRelay = new ATPButton();
            lblRelayStatus = new Label();
            dgvHardware = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnScanHardware = new ATPButton();
            btnConnectAll = new ATPButton();
            btnOutputV = new ATPButton();
            btnOutputRM = new ATPButton();
            btnOutputC = new ATPButton();
            lblOutputMode = new Label();
            lblVoltageRange = new Label();
            lstVoltageRange = new ListBox();
            ((System.ComponentModel.ISupportInitialize)dgvHardware).BeginInit();
            SuspendLayout();
            // 
            // lblPorts
            // 
            lblPorts.AutoSize = true;
            lblPorts.Font = new Font("Segoe UI", 10F);
            lblPorts.Location = new Point(866, 75);
            lblPorts.Name = "lblPorts";
            lblPorts.Size = new Size(74, 19);
            lblPorts.TabIndex = 0;
            lblPorts.Text = "COM Port:";
            // 
            // cmbPorts
            // 
            cmbPorts.Font = new Font("Segoe UI", 10F);
            cmbPorts.Location = new Point(866, 97);
            cmbPorts.Name = "cmbPorts";
            cmbPorts.Size = new Size(150, 25);
            cmbPorts.TabIndex = 1;
            // 
            // btnGetPorts
            // 
            btnGetPorts.BackColor = Color.White;
            btnGetPorts.BorderColor = Color.Black;
            btnGetPorts.BorderSize = 2;
            btnGetPorts.CornerRadius = 10;
            btnGetPorts.Cursor = Cursors.Hand;
            btnGetPorts.FlatStyle = FlatStyle.Flat;
            btnGetPorts.Font = new Font("Segoe UI", 10F);
            btnGetPorts.ForeColor = Color.Black;
            btnGetPorts.Location = new Point(861, 18);
            btnGetPorts.Name = "btnGetPorts";
            btnGetPorts.Size = new Size(160, 45);
            btnGetPorts.TabIndex = 2;
            btnGetPorts.Text = "Scan Ports";
            btnGetPorts.UseVisualStyleBackColor = false;
            btnGetPorts.Click += BtnGetPorts_Click;
            // 
            // btnConnectSTM32
            // 
            btnConnectSTM32.BackColor = Color.White;
            btnConnectSTM32.BorderColor = Color.Black;
            btnConnectSTM32.BorderSize = 2;
            btnConnectSTM32.CornerRadius = 10;
            btnConnectSTM32.Cursor = Cursors.Hand;
            btnConnectSTM32.FlatStyle = FlatStyle.Flat;
            btnConnectSTM32.Font = new Font("Segoe UI", 10F);
            btnConnectSTM32.ForeColor = Color.Black;
            btnConnectSTM32.Location = new Point(862, 128);
            btnConnectSTM32.Name = "btnConnectSTM32";
            btnConnectSTM32.Size = new Size(160, 45);
            btnConnectSTM32.TabIndex = 3;
            btnConnectSTM32.Text = "Connect STM32";
            btnConnectSTM32.UseVisualStyleBackColor = false;
            btnConnectSTM32.Click += BtnConnectSTM32_Click;
            // 
            // lblSTM32Status
            // 
            lblSTM32Status.AutoSize = true;
            lblSTM32Status.Font = new Font("Segoe UI", 10F);
            lblSTM32Status.ForeColor = Color.Red;
            lblSTM32Status.Location = new Point(860, 176);
            lblSTM32Status.Name = "lblSTM32Status";
            lblSTM32Status.Size = new Size(152, 19);
            lblSTM32Status.TabIndex = 4;
            lblSTM32Status.Text = "STM32: Not Connected";
            // 
            // btnConnectUSB2ANY
            // 
            btnConnectUSB2ANY.BackColor = Color.White;
            btnConnectUSB2ANY.BorderColor = Color.Black;
            btnConnectUSB2ANY.BorderSize = 2;
            btnConnectUSB2ANY.CornerRadius = 10;
            btnConnectUSB2ANY.Cursor = Cursors.Hand;
            btnConnectUSB2ANY.FlatStyle = FlatStyle.Flat;
            btnConnectUSB2ANY.Font = new Font("Segoe UI", 10F);
            btnConnectUSB2ANY.ForeColor = Color.Black;
            btnConnectUSB2ANY.Location = new Point(862, 218);
            btnConnectUSB2ANY.Name = "btnConnectUSB2ANY";
            btnConnectUSB2ANY.Size = new Size(160, 45);
            btnConnectUSB2ANY.TabIndex = 5;
            btnConnectUSB2ANY.Text = "Connect USB2ANY";
            btnConnectUSB2ANY.UseVisualStyleBackColor = false;
            btnConnectUSB2ANY.Click += BtnConnectUSB2ANY_Click;
            // 
            // lblUSB2ANYStatus
            // 
            lblUSB2ANYStatus.AutoSize = true;
            lblUSB2ANYStatus.Font = new Font("Segoe UI", 10F);
            lblUSB2ANYStatus.ForeColor = Color.Red;
            lblUSB2ANYStatus.Location = new Point(863, 272);
            lblUSB2ANYStatus.Name = "lblUSB2ANYStatus";
            lblUSB2ANYStatus.Size = new Size(169, 19);
            lblUSB2ANYStatus.TabIndex = 6;
            lblUSB2ANYStatus.Text = "USB2ANY: Not Connected";
            // 
            // lblComp
            // 
            lblComp.AutoSize = true;
            lblComp.Font = new Font("Segoe UI", 10F);
            lblComp.Location = new Point(823, 353);
            lblComp.Name = "lblComp";
            lblComp.Size = new Size(142, 19);
            lblComp.TabIndex = 7;
            lblComp.Text = "Voltage Comparators:";
            // 
            // chkVCOMPA0
            // 
            chkVCOMPA0.AutoSize = true;
            chkVCOMPA0.Checked = true;
            chkVCOMPA0.CheckState = CheckState.Checked;
            chkVCOMPA0.Font = new Font("Segoe UI", 10F);
            chkVCOMPA0.Location = new Point(823, 383);
            chkVCOMPA0.Name = "chkVCOMPA0";
            chkVCOMPA0.Size = new Size(94, 23);
            chkVCOMPA0.TabIndex = 8;
            chkVCOMPA0.Text = "VCOMPA0";
            // 
            // chkVCOMPA1
            // 
            chkVCOMPA1.AutoSize = true;
            chkVCOMPA1.Checked = true;
            chkVCOMPA1.CheckState = CheckState.Checked;
            chkVCOMPA1.Font = new Font("Segoe UI", 10F);
            chkVCOMPA1.Location = new Point(823, 413);
            chkVCOMPA1.Name = "chkVCOMPA1";
            chkVCOMPA1.Size = new Size(94, 23);
            chkVCOMPA1.TabIndex = 9;
            chkVCOMPA1.Text = "VCOMPA1";
            // 
            // btnSetCompensation
            // 
            btnSetCompensation.BackColor = Color.White;
            btnSetCompensation.BorderColor = Color.Black;
            btnSetCompensation.BorderSize = 2;
            btnSetCompensation.CornerRadius = 10;
            btnSetCompensation.Cursor = Cursors.Hand;
            btnSetCompensation.FlatStyle = FlatStyle.Flat;
            btnSetCompensation.Font = new Font("Segoe UI", 10F);
            btnSetCompensation.ForeColor = Color.Black;
            btnSetCompensation.Location = new Point(823, 453);
            btnSetCompensation.Name = "btnSetCompensation";
            btnSetCompensation.Size = new Size(160, 45);
            btnSetCompensation.TabIndex = 10;
            btnSetCompensation.Text = "Set Compensation";
            btnSetCompensation.UseVisualStyleBackColor = false;
            btnSetCompensation.Click += BtnSetCompensation_Click;
            // 
            // lblCompensationStatus
            // 
            lblCompensationStatus.AutoSize = true;
            lblCompensationStatus.Font = new Font("Segoe UI", 10F);
            lblCompensationStatus.Location = new Point(189, 498);
            lblCompensationStatus.Name = "lblCompensationStatus";
            lblCompensationStatus.Size = new Size(0, 19);
            lblCompensationStatus.TabIndex = 11;
            // 
            // lblRelay
            // 
            lblRelay.AutoSize = true;
            lblRelay.Font = new Font("Segoe UI", 10F);
            lblRelay.Location = new Point(823, 523);
            lblRelay.Name = "lblRelay";
            lblRelay.Size = new Size(84, 19);
            lblRelay.TabIndex = 12;
            lblRelay.Text = "Relay Mode:";
            // 
            // rdoOWI
            // 
            rdoOWI.AutoSize = true;
            rdoOWI.Checked = true;
            rdoOWI.Font = new Font("Segoe UI", 10F);
            rdoOWI.Location = new Point(823, 553);
            rdoOWI.Name = "rdoOWI";
            rdoOWI.Size = new Size(55, 23);
            rdoOWI.TabIndex = 13;
            rdoOWI.TabStop = true;
            rdoOWI.Text = "OWI";
            // 
            // rdoVO
            // 
            rdoVO.AutoSize = true;
            rdoVO.Font = new Font("Segoe UI", 10F);
            rdoVO.Location = new Point(883, 553);
            rdoVO.Name = "rdoVO";
            rdoVO.Size = new Size(47, 23);
            rdoVO.TabIndex = 14;
            rdoVO.Text = "VO";
            // 
            // rdoMA
            // 
            rdoMA.AutoSize = true;
            rdoMA.Font = new Font("Segoe UI", 10F);
            rdoMA.Location = new Point(933, 553);
            rdoMA.Name = "rdoMA";
            rdoMA.Size = new Size(49, 23);
            rdoMA.TabIndex = 15;
            rdoMA.Text = "MA";
            // 
            // btnSetRelay
            // 
            btnSetRelay.BackColor = Color.White;
            btnSetRelay.BorderColor = Color.Black;
            btnSetRelay.BorderSize = 2;
            btnSetRelay.CornerRadius = 10;
            btnSetRelay.Cursor = Cursors.Hand;
            btnSetRelay.FlatStyle = FlatStyle.Flat;
            btnSetRelay.Font = new Font("Segoe UI", 10F);
            btnSetRelay.ForeColor = Color.Black;
            btnSetRelay.Location = new Point(823, 593);
            btnSetRelay.Name = "btnSetRelay";
            btnSetRelay.Size = new Size(160, 45);
            btnSetRelay.TabIndex = 16;
            btnSetRelay.Text = "Set Relay";
            btnSetRelay.UseVisualStyleBackColor = false;
            btnSetRelay.Click += BtnSetRelay_Click;
            // 
            // lblRelayStatus
            // 
            lblRelayStatus.AutoSize = true;
            lblRelayStatus.Font = new Font("Segoe UI", 10F);
            lblRelayStatus.Location = new Point(189, 638);
            lblRelayStatus.Name = "lblRelayStatus";
            lblRelayStatus.Size = new Size(0, 19);
            lblRelayStatus.TabIndex = 17;
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
            dgvHardware.Location = new Point(173, 19);
            dgvHardware.Name = "dgvHardware";
            dgvHardware.ReadOnly = true;
            dgvHardware.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHardware.Size = new Size(600, 200);
            dgvHardware.TabIndex = 19;
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
            btnScanHardware.Location = new Point(7, 25);
            btnScanHardware.Name = "btnScanHardware";
            btnScanHardware.Size = new Size(160, 45);
            btnScanHardware.TabIndex = 18;
            btnScanHardware.Text = "Scan Hardware";
            btnScanHardware.UseVisualStyleBackColor = false;
            btnScanHardware.Click += BtnScanHardware_Click;
            // 
            // btnConnectAll
            // 
            btnConnectAll.BackColor = Color.White;
            btnConnectAll.BorderColor = Color.Black;
            btnConnectAll.BorderSize = 2;
            btnConnectAll.CornerRadius = 10;
            btnConnectAll.Cursor = Cursors.Hand;
            btnConnectAll.FlatStyle = FlatStyle.Flat;
            btnConnectAll.Font = new Font("Segoe UI", 10F);
            btnConnectAll.ForeColor = Color.Black;
            btnConnectAll.Location = new Point(173, 234);
            btnConnectAll.Name = "btnConnectAll";
            btnConnectAll.Size = new Size(160, 45);
            btnConnectAll.TabIndex = 20;
            btnConnectAll.Text = "Connect All";
            btnConnectAll.UseVisualStyleBackColor = false;
            btnConnectAll.Click += BtnConnectAll_Click;
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
            btnOutputV.Location = new Point(20, 339);
            btnOutputV.Name = "btnOutputV";
            btnOutputV.Size = new Size(160, 45);
            btnOutputV.TabIndex = 22;
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
            btnOutputRM.Location = new Point(190, 339);
            btnOutputRM.Name = "btnOutputRM";
            btnOutputRM.Size = new Size(160, 45);
            btnOutputRM.TabIndex = 23;
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
            btnOutputC.Location = new Point(360, 339);
            btnOutputC.Name = "btnOutputC";
            btnOutputC.Size = new Size(160, 45);
            btnOutputC.TabIndex = 24;
            btnOutputC.Text = "Current";
            btnOutputC.UseVisualStyleBackColor = false;
            btnOutputC.Click += BtnOutputC_Click;
            // 
            // lblOutputMode
            // 
            lblOutputMode.AutoSize = true;
            lblOutputMode.Font = new Font("Segoe UI", 10F);
            lblOutputMode.Location = new Point(20, 309);
            lblOutputMode.Name = "lblOutputMode";
            lblOutputMode.Size = new Size(184, 19);
            lblOutputMode.TabIndex = 21;
            lblOutputMode.Text = "Select Output Configuration:";
            // 
            // lblVoltageRange
            // 
            lblVoltageRange.AutoSize = true;
            lblVoltageRange.Font = new Font("Segoe UI", 10F);
            lblVoltageRange.Location = new Point(19, 400);
            lblVoltageRange.Name = "lblVoltageRange";
            lblVoltageRange.Size = new Size(100, 23);
            lblVoltageRange.TabIndex = 0;
            lblVoltageRange.Text = "Select Voltage Range:";
            // 
            // lstVoltageRange
            // 
            lstVoltageRange.Font = new Font("Segoe UI", 10F);
            lstVoltageRange.Items.AddRange(new object[] { "0-10V", "0-5V", "1-5V", "0.5-4.5V", "1-6V" });
            lstVoltageRange.Location = new Point(19, 425);
            lstVoltageRange.Name = "lstVoltageRange";
            lstVoltageRange.Size = new Size(200, 110);
            lstVoltageRange.TabIndex = 0;
            lstVoltageRange.Click += LstVoltageRange_Click;
            // 
            // HardwareTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblPorts);
            Controls.Add(cmbPorts);
            Controls.Add(btnGetPorts);
            Controls.Add(btnConnectSTM32);
            Controls.Add(lblSTM32Status);
            Controls.Add(btnConnectUSB2ANY);
            Controls.Add(lblUSB2ANYStatus);
            Controls.Add(lblComp);
            Controls.Add(chkVCOMPA0);
            Controls.Add(chkVCOMPA1);
            Controls.Add(btnSetCompensation);
            Controls.Add(lblCompensationStatus);
            Controls.Add(lblRelay);
            Controls.Add(rdoOWI);
            Controls.Add(rdoVO);
            Controls.Add(rdoMA);
            Controls.Add(btnSetRelay);
            Controls.Add(lblRelayStatus);
            Controls.Add(btnScanHardware);
            Controls.Add(dgvHardware);
            Controls.Add(btnConnectAll);
            Controls.Add(lblOutputMode);
            Controls.Add(btnOutputV);
            Controls.Add(btnOutputRM);
            Controls.Add(btnOutputC);
            Controls.Add(lblVoltageRange);
            Controls.Add(lstVoltageRange);
            Name = "HardwareTab";
            Size = new Size(1725, 1112);
            ((System.ComponentModel.ISupportInitialize)dgvHardware).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblPorts;
        private ComboBox cmbPorts;
        private ATPButton btnGetPorts;
        private ATPButton btnConnectSTM32;
        private Label lblSTM32Status;
        private ATPButton btnConnectUSB2ANY;
        private Label lblUSB2ANYStatus;
        private Label lblComp;
        private CheckBox chkVCOMPA0;
        private CheckBox chkVCOMPA1;
        private ATPButton btnSetCompensation;
        private Label lblCompensationStatus;
        private Label lblRelay;
        private RadioButton rdoOWI;
        private RadioButton rdoVO;
        private RadioButton rdoMA;
        private ATPButton btnSetRelay;
        private Label lblRelayStatus;
        private DataGridView dgvHardware;
        private ATPButton btnScanHardware;
        private ATPButton btnConnectAll;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ATPButton btnOutputV;
        private ATPButton btnOutputRM;
        private ATPButton btnOutputC;
        private Label lblOutputMode;
        private Label lblVoltageRange;
        private ListBox lstVoltageRange;
    }
}