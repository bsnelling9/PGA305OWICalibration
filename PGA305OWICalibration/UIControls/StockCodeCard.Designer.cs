namespace PGA305OWICalibration.UIControls
{
    partial class StockCodeCard
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblChannelNum = new Label();
            lblEnterStockCode = new Label();
            txtStockCode = new TextBox();
            btnConnectDevice = new ATPButton();
            btnConfigDevice = new ATPButton();
            lblSummary = new Label();
            lblConfigure = new Label();
            lblConnectDevice = new Label();
            chkInclude = new CheckBox();
            SuspendLayout();
            // 
            // lblChannelNum
            // 
            lblChannelNum.AutoSize = true;
            lblChannelNum.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChannelNum.Location = new Point(16, 9);
            lblChannelNum.Name = "lblChannelNum";
            lblChannelNum.Size = new Size(62, 19);
            lblChannelNum.TabIndex = 0;
            lblChannelNum.Text = "Channel";
            // 
            // lblEnterStockCode
            // 
            lblEnterStockCode.AutoSize = true;
            lblEnterStockCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEnterStockCode.Location = new Point(16, 37);
            lblEnterStockCode.Name = "lblEnterStockCode";
            lblEnterStockCode.Size = new Size(116, 17);
            lblEnterStockCode.TabIndex = 1;
            lblEnterStockCode.Text = "Enter Stock Code:";
            // 
            // txtStockCode
            // 
            txtStockCode.Font = new Font("Segoe UI", 10F);
            txtStockCode.Location = new Point(153, 34);
            txtStockCode.Name = "txtStockCode";
            txtStockCode.Size = new Size(140, 25);
            txtStockCode.TabIndex = 2;
            txtStockCode.TextChanged += txtStockCode_TextChanged;
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
            btnConnectDevice.Location = new Point(16, 87);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(140, 42);
            btnConnectDevice.TabIndex = 4;
            btnConnectDevice.Text = "Connect";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
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
            btnConfigDevice.Location = new Point(16, 324);
            btnConfigDevice.Name = "btnConfigDevice";
            btnConfigDevice.Size = new Size(140, 42);
            btnConfigDevice.TabIndex = 5;
            btnConfigDevice.Text = "Configure";
            btnConfigDevice.UseVisualStyleBackColor = false;
            btnConfigDevice.Click += btnConfigDevice_Click;
            // 
            // lblSummary
            // 
            lblSummary.Location = new Point(16, 144);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(277, 150);
            lblSummary.TabIndex = 6;
            // 
            // lblConfigure
            // 
            lblConfigure.AutoSize = true;
            lblConfigure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConfigure.Location = new Point(16, 304);
            lblConfigure.Name = "lblConfigure";
            lblConfigure.Size = new Size(199, 17);
            lblConfigure.TabIndex = 7;
            lblConfigure.Text = "Confirm and Configure Device:";
            // 
            // lblConnectDevice
            // 
            lblConnectDevice.AutoSize = true;
            lblConnectDevice.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConnectDevice.Location = new Point(16, 67);
            lblConnectDevice.Name = "lblConnectDevice";
            lblConnectDevice.Size = new Size(124, 17);
            lblConnectDevice.TabIndex = 8;
            lblConnectDevice.Text = "Connect to Device:";
            // 
            // chkInclude
            // 
            chkInclude.AutoSize = true;
            chkInclude.Location = new Point(228, 9);
            chkInclude.Name = "chkInclude";
            chkInclude.Size = new Size(65, 19);
            chkInclude.TabIndex = 9;
            chkInclude.Text = "Include";
            chkInclude.UseVisualStyleBackColor = true;
            // 
            // StockCodeCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chkInclude);
            Controls.Add(lblConnectDevice);
            Controls.Add(lblConfigure);
            Controls.Add(lblChannelNum);
            Controls.Add(lblEnterStockCode);
            Controls.Add(txtStockCode);
            Controls.Add(btnConnectDevice);
            Controls.Add(btnConfigDevice);
            Controls.Add(lblSummary);
            Name = "StockCodeCard";
            Size = new Size(325, 410);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblChannelNum;
        private Label lblEnterStockCode;
        private TextBox txtStockCode;
        private ATPButton btnConnectDevice;
        private ATPButton btnConfigDevice;
        private Label lblSummary;
        private Label lblConfigure;
        private Label lblConnectDevice;
        private CheckBox chkInclude;
    }
}