using PGA305OWICalibration.UIControls;
using System.Windows.Forms;
using System.Drawing;

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
            dgvHardware = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnScanHardware = new ATPButton();
            btnConnectAll = new ATPButton();
            ((System.ComponentModel.ISupportInitialize)dgvHardware).BeginInit();
            SuspendLayout();
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
            dgvHardware.Location = new Point(299, 290);
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
            btnScanHardware.Location = new Point(299, 225);
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
            btnConnectAll.Location = new Point(505, 225);
            btnConnectAll.Name = "btnConnectAll";
            btnConnectAll.Size = new Size(160, 45);
            btnConnectAll.TabIndex = 20;
            btnConnectAll.Text = "Connect All";
            btnConnectAll.UseVisualStyleBackColor = false;
            btnConnectAll.Click += BtnConnectAll_Click;
            // 
            // HardwareTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnScanHardware);
            Controls.Add(dgvHardware);
            Controls.Add(btnConnectAll);
            Name = "HardwareTab";
            Size = new Size(1260, 755);
            ((System.ComponentModel.ISupportInitialize)dgvHardware).EndInit();
            ResumeLayout(false);
        }
        private DataGridView dgvHardware;
        private ATPButton btnScanHardware;
        private ATPButton btnConnectAll;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}