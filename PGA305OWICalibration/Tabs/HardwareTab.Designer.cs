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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dgvHardware = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnScanHardware = new ATPButton();
            btnConnectAll = new ATPButton();
            lblHardware = new Label();
            numericUpDown1 = new NumericUpDown();
            lblChannel = new Label();
            btnSetChannel = new Button();
            btnSETOWI = new Button();
            btnCompA = new Button();
            btnCompV = new Button();
            btnCompR = new Button();
            btnSETMA = new Button();
            btnSETVO = new Button();
            btnInit = new Button();
            btnActivate = new Button();
            btnGPIOTXLow = new Button();
            btnGPIOTXHigh = new Button();
            btnActivateLow = new Button();
            btnActivatehigh = new Button();
            btnOWITXHigh = new Button();
            btnOWI_TXLow = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHardware).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // dgvHardware
            // 
            dgvHardware.AllowUserToAddRows = false;
            dgvHardware.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvHardware.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHardware.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvHardware.DefaultCellStyle = dataGridViewCellStyle4;
            dgvHardware.Location = new Point(129, 205);
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
            btnScanHardware.Location = new Point(129, 140);
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
            btnConnectAll.Location = new Point(129, 424);
            btnConnectAll.Name = "btnConnectAll";
            btnConnectAll.Size = new Size(160, 45);
            btnConnectAll.TabIndex = 20;
            btnConnectAll.Text = "Connect All";
            btnConnectAll.UseVisualStyleBackColor = false;
            btnConnectAll.Click += BtnConnectAll_Click;
            // 
            // lblHardware
            // 
            lblHardware.AutoSize = true;
            lblHardware.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHardware.Location = new Point(54, 63);
            lblHardware.Name = "lblHardware";
            lblHardware.Size = new Size(372, 47);
            lblHardware.TabIndex = 21;
            lblHardware.Text = "Connect to Hardware";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(995, 87);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(91, 23);
            numericUpDown1.TabIndex = 22;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // lblChannel
            // 
            lblChannel.AutoSize = true;
            lblChannel.Location = new Point(995, 69);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(88, 15);
            lblChannel.TabIndex = 23;
            lblChannel.Text = "Select Channel:";
            // 
            // btnSetChannel
            // 
            btnSetChannel.Location = new Point(995, 116);
            btnSetChannel.Name = "btnSetChannel";
            btnSetChannel.Size = new Size(123, 45);
            btnSetChannel.TabIndex = 24;
            btnSetChannel.Text = "Set Channel";
            btnSetChannel.UseVisualStyleBackColor = true;
            btnSetChannel.Click += btnSetChannel_Click;
            // 
            // btnSETOWI
            // 
            btnSETOWI.Location = new Point(897, 250);
            btnSETOWI.Name = "btnSETOWI";
            btnSETOWI.Size = new Size(93, 38);
            btnSETOWI.TabIndex = 25;
            btnSETOWI.Text = "SETOWI";
            btnSETOWI.UseVisualStyleBackColor = true;
            btnSETOWI.Click += btnSETOWI_Click;
            // 
            // btnCompA
            // 
            btnCompA.Location = new Point(897, 187);
            btnCompA.Name = "btnCompA";
            btnCompA.Size = new Size(93, 36);
            btnCompA.TabIndex = 26;
            btnCompA.Text = "Current Comp";
            btnCompA.UseVisualStyleBackColor = true;
            btnCompA.Click += btnCompA_Click;
            // 
            // btnCompV
            // 
            btnCompV.Location = new Point(1012, 187);
            btnCompV.Name = "btnCompV";
            btnCompV.Size = new Size(93, 36);
            btnCompV.TabIndex = 27;
            btnCompV.Text = "Voltage Comp";
            btnCompV.UseVisualStyleBackColor = true;
            btnCompV.Click += btnCompV_Click;
            // 
            // btnCompR
            // 
            btnCompR.Location = new Point(1111, 187);
            btnCompR.Name = "btnCompR";
            btnCompR.Size = new Size(93, 36);
            btnCompR.TabIndex = 28;
            btnCompR.Text = "Ratio Comp";
            btnCompR.UseVisualStyleBackColor = true;
            btnCompR.Click += btnCompR_Click;
            // 
            // btnSETMA
            // 
            btnSETMA.Location = new Point(1012, 250);
            btnSETMA.Name = "btnSETMA";
            btnSETMA.Size = new Size(93, 38);
            btnSETMA.TabIndex = 29;
            btnSETMA.Text = "SETMA";
            btnSETMA.UseVisualStyleBackColor = true;
            btnSETMA.Click += btnSETMA_Click;
            // 
            // btnSETVO
            // 
            btnSETVO.Location = new Point(1111, 250);
            btnSETVO.Name = "btnSETVO";
            btnSETVO.Size = new Size(93, 38);
            btnSETVO.TabIndex = 30;
            btnSETVO.Text = "SETVO";
            btnSETVO.UseVisualStyleBackColor = true;
            btnSETVO.Click += btnSETVO_Click;
            // 
            // btnInit
            // 
            btnInit.Location = new Point(897, 356);
            btnInit.Name = "btnInit";
            btnInit.Size = new Size(93, 38);
            btnInit.TabIndex = 31;
            btnInit.Text = "Init";
            btnInit.UseVisualStyleBackColor = true;
            btnInit.Click += btnInit_Click;
            // 
            // btnActivate
            // 
            btnActivate.Location = new Point(1012, 356);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(93, 38);
            btnActivate.TabIndex = 32;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += btnActivate_Click;
            // 
            // btnGPIOTXLow
            // 
            btnGPIOTXLow.Location = new Point(465, 488);
            btnGPIOTXLow.Name = "btnGPIOTXLow";
            btnGPIOTXLow.Size = new Size(125, 38);
            btnGPIOTXLow.TabIndex = 33;
            btnGPIOTXLow.Text = "GPIO_OWI_TX Low";
            btnGPIOTXLow.UseVisualStyleBackColor = true;
            btnGPIOTXLow.Click += btnGPIOTXLow_Click;
            // 
            // btnGPIOTXHigh
            // 
            btnGPIOTXHigh.Location = new Point(633, 488);
            btnGPIOTXHigh.Name = "btnGPIOTXHigh";
            btnGPIOTXHigh.Size = new Size(125, 38);
            btnGPIOTXHigh.TabIndex = 34;
            btnGPIOTXHigh.Text = "GPIO_OWI_TX High";
            btnGPIOTXHigh.UseVisualStyleBackColor = true;
            btnGPIOTXHigh.Click += btnGPIOTXHigh_Click;
            // 
            // btnActivateLow
            // 
            btnActivateLow.Location = new Point(465, 562);
            btnActivateLow.Name = "btnActivateLow";
            btnActivateLow.Size = new Size(125, 38);
            btnActivateLow.TabIndex = 35;
            btnActivateLow.Text = "Actvate Pin Low";
            btnActivateLow.UseVisualStyleBackColor = true;
            btnActivateLow.Click += btnActivateLow_Click;
            // 
            // btnActivatehigh
            // 
            btnActivatehigh.Location = new Point(633, 562);
            btnActivatehigh.Name = "btnActivatehigh";
            btnActivatehigh.Size = new Size(125, 38);
            btnActivatehigh.TabIndex = 36;
            btnActivatehigh.Text = "Activate Pin High";
            btnActivatehigh.UseVisualStyleBackColor = true;
            btnActivatehigh.Click += btnActivatehigh_Click;
            // 
            // btnOWITXHigh
            // 
            btnOWITXHigh.Location = new Point(633, 640);
            btnOWITXHigh.Name = "btnOWITXHigh";
            btnOWITXHigh.Size = new Size(125, 38);
            btnOWITXHigh.TabIndex = 38;
            btnOWITXHigh.Text = "OWI_TX High";
            btnOWITXHigh.UseVisualStyleBackColor = true;
            btnOWITXHigh.Click += btnOWITXHigh_Click;
            // 
            // btnOWI_TXLow
            // 
            btnOWI_TXLow.Location = new Point(465, 640);
            btnOWI_TXLow.Name = "btnOWI_TXLow";
            btnOWI_TXLow.Size = new Size(125, 38);
            btnOWI_TXLow.TabIndex = 37;
            btnOWI_TXLow.Text = "OWI_TX Low";
            btnOWI_TXLow.UseVisualStyleBackColor = true;
            btnOWI_TXLow.Click += btnOWI_TXLow_Click;
            // 
            // HardwareTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnOWITXHigh);
            Controls.Add(btnOWI_TXLow);
            Controls.Add(btnActivatehigh);
            Controls.Add(btnActivateLow);
            Controls.Add(btnGPIOTXHigh);
            Controls.Add(btnGPIOTXLow);
            Controls.Add(btnActivate);
            Controls.Add(btnInit);
            Controls.Add(btnSETVO);
            Controls.Add(btnSETMA);
            Controls.Add(btnCompR);
            Controls.Add(btnCompV);
            Controls.Add(btnCompA);
            Controls.Add(btnSETOWI);
            Controls.Add(btnSetChannel);
            Controls.Add(lblChannel);
            Controls.Add(numericUpDown1);
            Controls.Add(lblHardware);
            Controls.Add(btnScanHardware);
            Controls.Add(dgvHardware);
            Controls.Add(btnConnectAll);
            Name = "HardwareTab";
            Size = new Size(1260, 755);
            ((System.ComponentModel.ISupportInitialize)dgvHardware).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private DataGridView dgvHardware;
        private ATPButton btnScanHardware;
        private ATPButton btnConnectAll;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Label lblHardware;
        private NumericUpDown numericUpDown1;
        private Label lblChannel;
        private Button btnSetChannel;
        private Button btnSETOWI;
        private Button btnCompA;
        private Button btnCompV;
        private Button btnCompR;
        private Button btnSETMA;
        private Button btnSETVO;
        private Button btnInit;
        private Button btnActivate;
        private Button btnGPIOTXLow;
        private Button btnGPIOTXHigh;
        private Button btnActivateLow;
        private Button btnActivatehigh;
        private Button btnOWITXHigh;
        private Button btnOWI_TXLow;
    }
}