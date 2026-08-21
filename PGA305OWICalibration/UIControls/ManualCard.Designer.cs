namespace PGA305OWICalibration.UIControls
{
    partial class ManualCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlManual = new Panel();
            btnUnitPsi = new ATPButton();
            btnUnitBar = new ATPButton();
            lblMaxPressure = new Label();
            numMaxPressure = new NumericUpDown();
            lblMinPressure = new Label();
            numMinPressure = new NumericUpDown();
            cbxVoltageRange = new ComboBox();
            btnOutputCurrent = new ATPButton();
            btnOutputRM = new ATPButton();
            btnOutputVolt = new ATPButton();
            btnConnectDevice = new ATPButton();
            lblSummary = new Label();
            btnConfigDevice = new ATPButton();
            lblPressureCode = new Label();
            lblSerialNumber = new Label();
            lblChannelNum = new Label();
            tlpCard = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            gbxConfigure = new GroupBox();
            pnlManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).BeginInit();
            tlpCard.SuspendLayout();
            groupBox1.SuspendLayout();
            gbxConfigure.SuspendLayout();
            SuspendLayout();
            // 
            // pnlManual
            // 
            pnlManual.Controls.Add(btnUnitPsi);
            pnlManual.Controls.Add(btnUnitBar);
            pnlManual.Controls.Add(lblMaxPressure);
            pnlManual.Controls.Add(numMaxPressure);
            pnlManual.Controls.Add(lblMinPressure);
            pnlManual.Controls.Add(numMinPressure);
            pnlManual.Controls.Add(cbxVoltageRange);
            pnlManual.Controls.Add(btnOutputCurrent);
            pnlManual.Controls.Add(btnOutputRM);
            pnlManual.Controls.Add(btnOutputVolt);
            pnlManual.Controls.Add(btnConnectDevice);
            pnlManual.Location = new Point(3, 46);
            pnlManual.Name = "pnlManual";
            pnlManual.Size = new Size(300, 221);
            pnlManual.TabIndex = 94;
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
            btnUnitPsi.Location = new Point(9, 114);
            btnUnitPsi.Name = "btnUnitPsi";
            btnUnitPsi.Size = new Size(57, 45);
            btnUnitPsi.TabIndex = 108;
            btnUnitPsi.Text = "psi";
            btnUnitPsi.UseVisualStyleBackColor = false;
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
            btnUnitBar.Location = new Point(96, 114);
            btnUnitBar.Name = "btnUnitBar";
            btnUnitBar.Size = new Size(57, 45);
            btnUnitBar.TabIndex = 107;
            btnUnitBar.Text = "Bar";
            btnUnitBar.UseVisualStyleBackColor = false;
            // 
            // lblMaxPressure
            // 
            lblMaxPressure.AutoSize = true;
            lblMaxPressure.Font = new Font("Segoe UI", 10F);
            lblMaxPressure.Location = new Point(153, 168);
            lblMaxPressure.Name = "lblMaxPressure";
            lblMaxPressure.Size = new Size(94, 19);
            lblMaxPressure.TabIndex = 103;
            lblMaxPressure.Text = "Max Pressure:";
            // 
            // numMaxPressure
            // 
            numMaxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMaxPressure.Location = new Point(153, 190);
            numMaxPressure.Name = "numMaxPressure";
            numMaxPressure.Size = new Size(94, 25);
            numMaxPressure.TabIndex = 102;
            // 
            // lblMinPressure
            // 
            lblMinPressure.AutoSize = true;
            lblMinPressure.Font = new Font("Segoe UI", 10F);
            lblMinPressure.Location = new Point(9, 168);
            lblMinPressure.Name = "lblMinPressure";
            lblMinPressure.Size = new Size(92, 19);
            lblMinPressure.TabIndex = 101;
            lblMinPressure.Text = "Min Pressure:";
            // 
            // numMinPressure
            // 
            numMinPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMinPressure.Location = new Point(9, 190);
            numMinPressure.Name = "numMinPressure";
            numMinPressure.Size = new Size(91, 25);
            numMinPressure.TabIndex = 100;
            // 
            // cbxVoltageRange
            // 
            cbxVoltageRange.FormattingEnabled = true;
            cbxVoltageRange.Location = new Point(127, 76);
            cbxVoltageRange.Name = "cbxVoltageRange";
            cbxVoltageRange.Size = new Size(97, 23);
            cbxVoltageRange.TabIndex = 99;
            // 
            // btnOutputCurrent
            // 
            btnOutputCurrent.BackColor = Color.White;
            btnOutputCurrent.BorderColor = Color.Black;
            btnOutputCurrent.BorderSize = 2;
            btnOutputCurrent.CornerRadius = 10;
            btnOutputCurrent.Cursor = Cursors.Hand;
            btnOutputCurrent.FlatStyle = FlatStyle.Flat;
            btnOutputCurrent.Font = new Font("Segoe UI", 10F);
            btnOutputCurrent.ForeColor = Color.Black;
            btnOutputCurrent.Location = new Point(210, 12);
            btnOutputCurrent.Name = "btnOutputCurrent";
            btnOutputCurrent.Size = new Size(66, 45);
            btnOutputCurrent.TabIndex = 98;
            btnOutputCurrent.Text = "Current";
            btnOutputCurrent.UseVisualStyleBackColor = false;
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
            btnOutputRM.Location = new Point(108, 12);
            btnOutputRM.Name = "btnOutputRM";
            btnOutputRM.Size = new Size(83, 45);
            btnOutputRM.TabIndex = 97;
            btnOutputRM.Text = "Ratiometric";
            btnOutputRM.UseVisualStyleBackColor = false;
            // 
            // btnOutputVolt
            // 
            btnOutputVolt.BackColor = Color.White;
            btnOutputVolt.BorderColor = Color.Black;
            btnOutputVolt.BorderSize = 2;
            btnOutputVolt.CornerRadius = 10;
            btnOutputVolt.Cursor = Cursors.Hand;
            btnOutputVolt.FlatStyle = FlatStyle.Flat;
            btnOutputVolt.Font = new Font("Segoe UI", 10F);
            btnOutputVolt.ForeColor = Color.Black;
            btnOutputVolt.Location = new Point(9, 12);
            btnOutputVolt.Name = "btnOutputVolt";
            btnOutputVolt.Size = new Size(77, 45);
            btnOutputVolt.TabIndex = 96;
            btnOutputVolt.Text = "Voltage";
            btnOutputVolt.UseVisualStyleBackColor = false;
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
            btnConnectDevice.Location = new Point(9, 63);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(83, 45);
            btnConnectDevice.TabIndex = 94;
            btnConnectDevice.Text = "Connect";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
            // 
            // lblSummary
            // 
            lblSummary.Location = new Point(98, 13);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(178, 165);
            lblSummary.TabIndex = 109;
            lblSummary.Text = "label1";
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
            btnConfigDevice.Location = new Point(6, 24);
            btnConfigDevice.Name = "btnConfigDevice";
            btnConfigDevice.Size = new Size(85, 45);
            btnConfigDevice.TabIndex = 104;
            btnConfigDevice.Text = "Configure";
            btnConfigDevice.UseVisualStyleBackColor = false;
            // 
            // lblPressureCode
            // 
            lblPressureCode.AutoSize = true;
            lblPressureCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPressureCode.Location = new Point(210, 17);
            lblPressureCode.Name = "lblPressureCode";
            lblPressureCode.Size = new Size(86, 15);
            lblPressureCode.TabIndex = 106;
            lblPressureCode.Text = "Pressure Code";
            // 
            // lblSerialNumber
            // 
            lblSerialNumber.AutoSize = true;
            lblSerialNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSerialNumber.Location = new Point(108, 17);
            lblSerialNumber.Name = "lblSerialNumber";
            lblSerialNumber.Size = new Size(68, 15);
            lblSerialNumber.TabIndex = 105;
            lblSerialNumber.Text = "Serial Num";
            // 
            // lblChannelNum
            // 
            lblChannelNum.AutoSize = true;
            lblChannelNum.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblChannelNum.Location = new Point(11, 17);
            lblChannelNum.Name = "lblChannelNum";
            lblChannelNum.Size = new Size(51, 15);
            lblChannelNum.TabIndex = 95;
            lblChannelNum.Text = "Channel";
            // 
            // tlpCard
            // 
            tlpCard.ColumnCount = 1;
            tlpCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCard.Controls.Add(groupBox1, 0, 0);
            tlpCard.Controls.Add(pnlManual, 0, 1);
            tlpCard.Controls.Add(gbxConfigure, 0, 3);
            tlpCard.Dock = DockStyle.Fill;
            tlpCard.Location = new Point(12, 12);
            tlpCard.Name = "tlpCard";
            tlpCard.RowCount = 4;
            tlpCard.RowStyles.Add(new RowStyle());
            tlpCard.RowStyles.Add(new RowStyle());
            tlpCard.RowStyles.Add(new RowStyle());
            tlpCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCard.Size = new Size(328, 628);
            tlpCard.TabIndex = 111;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblChannelNum);
            groupBox1.Controls.Add(lblPressureCode);
            groupBox1.Controls.Add(lblSerialNumber);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 37);
            groupBox1.TabIndex = 112;
            groupBox1.TabStop = false;
            // 
            // gbxConfigure
            // 
            gbxConfigure.Controls.Add(btnConfigDevice);
            gbxConfigure.Controls.Add(lblSummary);
            gbxConfigure.Location = new Point(3, 273);
            gbxConfigure.Name = "gbxConfigure";
            gbxConfigure.Size = new Size(311, 215);
            gbxConfigure.TabIndex = 112;
            gbxConfigure.TabStop = false;
            // 
            // ManualCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpCard);
            Name = "ManualCard";
            Padding = new Padding(12);
            Size = new Size(352, 652);
            pnlManual.ResumeLayout(false);
            pnlManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).EndInit();
            tlpCard.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            gbxConfigure.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlManual;
        private Label lblSummary;
        private ATPButton btnUnitPsi;
        private ATPButton btnUnitBar;
        private Label lblPressureCode;
        private Label lblSerialNumber;
        private ATPButton btnConfigDevice;
        private Label lblMaxPressure;
        private NumericUpDown numMaxPressure;
        private Label lblMinPressure;
        private NumericUpDown numMinPressure;
        private ComboBox cbxVoltageRange;
        private ATPButton btnOutputCurrent;
        private ATPButton btnOutputRM;
        private ATPButton btnOutputVolt;
        private Label lblChannelNum;
        private ATPButton btnConnectDevice;
        private TableLayoutPanel tlpCard;
        private GroupBox gbxConfigure;
        private GroupBox groupBox1;
    }
}
