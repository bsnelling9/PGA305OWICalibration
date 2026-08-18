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
            lblComp = new Label();
            chkVCOMPA0 = new CheckBox();
            chkVCOMPA1 = new CheckBox();
            btnSetCompensation = new ATPButton();
            lblRelay = new Label();
            rdoOWI = new RadioButton();
            rdoVO = new RadioButton();
            rdoMA = new RadioButton();
            btnSetRelay = new ATPButton();
            dgvHardware = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnScanHardware = new ATPButton();
            btnConnectAll = new ATPButton();
            btnConnectDevice = new ATPButton();
            btnInitHW = new ATPButton();
            ((System.ComponentModel.ISupportInitialize)dgvHardware).BeginInit();
            SuspendLayout();
            // 
            // lblComp
            // 
            lblComp.AutoSize = true;
            lblComp.Font = new Font("Segoe UI", 10F);
            lblComp.Location = new Point(372, 254);
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
            chkVCOMPA0.Location = new Point(372, 284);
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
            chkVCOMPA1.Location = new Point(372, 314);
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
            btnSetCompensation.Location = new Point(372, 354);
            btnSetCompensation.Name = "btnSetCompensation";
            btnSetCompensation.Size = new Size(160, 45);
            btnSetCompensation.TabIndex = 10;
            btnSetCompensation.Text = "Set Compensation";
            btnSetCompensation.UseVisualStyleBackColor = false;
            btnSetCompensation.Click += BtnSetCompensation_Click;
            // 
            // lblRelay
            // 
            lblRelay.AutoSize = true;
            lblRelay.Font = new Font("Segoe UI", 10F);
            lblRelay.Location = new Point(372, 424);
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
            rdoOWI.Location = new Point(372, 454);
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
            rdoVO.Location = new Point(432, 454);
            rdoVO.Name = "rdoVO";
            rdoVO.Size = new Size(47, 23);
            rdoVO.TabIndex = 14;
            rdoVO.Text = "VO";
            // 
            // rdoMA
            // 
            rdoMA.AutoSize = true;
            rdoMA.Font = new Font("Segoe UI", 10F);
            rdoMA.Location = new Point(482, 454);
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
            btnSetRelay.Location = new Point(371, 490);
            btnSetRelay.Name = "btnSetRelay";
            btnSetRelay.Size = new Size(160, 45);
            btnSetRelay.TabIndex = 16;
            btnSetRelay.Text = "Set Relay";
            btnSetRelay.UseVisualStyleBackColor = false;
            btnSetRelay.Click += BtnSetRelay_Click;
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
            btnConnectDevice.Location = new Point(371, 665);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(160, 45);
            btnConnectDevice.TabIndex = 41;
            btnConnectDevice.Text = "Connect to Device";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
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
            btnInitHW.Location = new Point(371, 581);
            btnInitHW.Name = "btnInitHW";
            btnInitHW.Size = new Size(160, 45);
            btnInitHW.TabIndex = 40;
            btnInitHW.Text = "Initialize Hardware";
            btnInitHW.UseVisualStyleBackColor = false;
            btnInitHW.Click += btnInitHW_Click;
            // 
            // HardwareTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnConnectDevice);
            Controls.Add(btnInitHW);
            Controls.Add(lblComp);
            Controls.Add(chkVCOMPA0);
            Controls.Add(chkVCOMPA1);
            Controls.Add(btnSetCompensation);
            Controls.Add(lblRelay);
            Controls.Add(rdoOWI);
            Controls.Add(rdoVO);
            Controls.Add(rdoMA);
            Controls.Add(btnSetRelay);
            Controls.Add(btnScanHardware);
            Controls.Add(dgvHardware);
            Controls.Add(btnConnectAll);
            Name = "HardwareTab";
            Size = new Size(1725, 1112);
            ((System.ComponentModel.ISupportInitialize)dgvHardware).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblComp;
        private CheckBox chkVCOMPA0;
        private CheckBox chkVCOMPA1;
        private ATPButton btnSetCompensation;
        private Label lblRelay;
        private RadioButton rdoOWI;
        private RadioButton rdoVO;
        private RadioButton rdoMA;
        private ATPButton btnSetRelay;
        private DataGridView dgvHardware;
        private ATPButton btnScanHardware;
        private ATPButton btnConnectAll;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ATPButton btnConnectDevice;
        private ATPButton btnInitHW;
    }
}