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
            button10 = new Button();
            listBoxDebug = new ListBox();
            btnInitHW = new ATPButton();
            btnClose = new ATPButton();
            btnConnectDevice = new ATPButton();
            label1 = new Label();
            lblStep2 = new Label();
            gbxConfigPressure = new GroupBox();
            btnConfirmPressure = new ATPButton();
            lblSelectUnit = new Label();
            btnUnitPsi = new ATPButton();
            btnUnitBar = new ATPButton();
            lblMaxPressure = new Label();
            numMaxPressure = new NumericUpDown();
            lblMinPressure = new Label();
            numMinPressure = new NumericUpDown();
            btnNoPChange = new ATPButton();
            gbxConfigOutput = new GroupBox();
            lstVoltageRange = new ListBox();
            lsbOutputConfig = new ListBox();
            btnConfigDevice = new ATPButton();
            txtJobCode = new TextBox();
            lblJobCode = new Label();
            btnNextDevice = new ATPButton();
            gbxConfigDevice = new GroupBox();
            btnVoltagePOT = new ATPButton();
            btnRatioPOT = new ATPButton();
            btnCurrentPOT = new ATPButton();
            txtStockCode = new TextBox();
            lblEnterStockCode = new Label();
            btnLoadStockCode = new ATPButton();
            gbxConfigPressure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).BeginInit();
            gbxConfigOutput.SuspendLayout();
            gbxConfigDevice.SuspendLayout();
            SuspendLayout();
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
            listBoxDebug.Location = new Point(12, 27);
            listBoxDebug.Name = "listBoxDebug";
            listBoxDebug.Size = new Size(269, 304);
            listBoxDebug.TabIndex = 21;
            // 
            // btnInitHW
            // 
            btnInitHW.BackColor = Color.White;
            btnInitHW.BorderColor = Color.Black;
            btnInitHW.CornerRadius = 10;
            btnInitHW.Cursor = Cursors.Hand;
            btnInitHW.FlatStyle = FlatStyle.Flat;
            btnInitHW.Font = new Font("Segoe UI", 10F);
            btnInitHW.ForeColor = Color.Black;
            btnInitHW.Location = new Point(342, 28);
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
            btnClose.CornerRadius = 10;
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(977, 581);
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
            btnConnectDevice.CornerRadius = 10;
            btnConnectDevice.Cursor = Cursors.Hand;
            btnConnectDevice.FlatStyle = FlatStyle.Flat;
            btnConnectDevice.Font = new Font("Segoe UI", 10F);
            btnConnectDevice.ForeColor = Color.Black;
            btnConnectDevice.Location = new Point(999, 27);
            btnConnectDevice.Name = "btnConnectDevice";
            btnConnectDevice.Size = new Size(160, 45);
            btnConnectDevice.TabIndex = 39;
            btnConnectDevice.Text = "Connect to Device";
            btnConnectDevice.UseVisualStyleBackColor = false;
            btnConnectDevice.Click += btnConnectDevice_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(342, 10);
            label1.Name = "label1";
            label1.Size = new Size(50, 17);
            label1.TabIndex = 40;
            label1.Text = "Step 1:";
            // 
            // lblStep2
            // 
            lblStep2.AutoSize = true;
            lblStep2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep2.Location = new Point(999, 9);
            lblStep2.Name = "lblStep2";
            lblStep2.Size = new Size(50, 17);
            lblStep2.TabIndex = 41;
            lblStep2.Text = "Step 2:";
            // 
            // gbxConfigPressure
            // 
            gbxConfigPressure.Controls.Add(btnConfirmPressure);
            gbxConfigPressure.Controls.Add(lblSelectUnit);
            gbxConfigPressure.Controls.Add(btnUnitPsi);
            gbxConfigPressure.Controls.Add(btnUnitBar);
            gbxConfigPressure.Controls.Add(lblMaxPressure);
            gbxConfigPressure.Controls.Add(numMaxPressure);
            gbxConfigPressure.Controls.Add(lblMinPressure);
            gbxConfigPressure.Controls.Add(numMinPressure);
            gbxConfigPressure.Controls.Add(btnNoPChange);
            gbxConfigPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxConfigPressure.Location = new Point(717, 150);
            gbxConfigPressure.Name = "gbxConfigPressure";
            gbxConfigPressure.Size = new Size(340, 214);
            gbxConfigPressure.TabIndex = 64;
            gbxConfigPressure.TabStop = false;
            gbxConfigPressure.Text = "Step 5: Configure Pressure";
            // 
            // btnConfirmPressure
            // 
            btnConfirmPressure.BackColor = Color.White;
            btnConfirmPressure.BorderColor = Color.Black;
            btnConfirmPressure.CornerRadius = 10;
            btnConfirmPressure.Cursor = Cursors.Hand;
            btnConfirmPressure.FlatStyle = FlatStyle.Flat;
            btnConfirmPressure.Font = new Font("Segoe UI", 10F);
            btnConfirmPressure.ForeColor = Color.Black;
            btnConfirmPressure.Location = new Point(23, 157);
            btnConfirmPressure.Name = "btnConfirmPressure";
            btnConfirmPressure.Size = new Size(107, 45);
            btnConfirmPressure.TabIndex = 56;
            btnConfirmPressure.Text = "Update";
            btnConfirmPressure.UseVisualStyleBackColor = false;
            btnConfirmPressure.Click += btnConfirmPressure_Click;
            // 
            // lblSelectUnit
            // 
            lblSelectUnit.AutoSize = true;
            lblSelectUnit.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectUnit.Location = new Point(15, 21);
            lblSelectUnit.Name = "lblSelectUnit";
            lblSelectUnit.Size = new Size(78, 17);
            lblSelectUnit.TabIndex = 55;
            lblSelectUnit.Text = "Select Unit:";
            // 
            // btnUnitPsi
            // 
            btnUnitPsi.BackColor = Color.White;
            btnUnitPsi.BorderColor = Color.Black;
            btnUnitPsi.CornerRadius = 10;
            btnUnitPsi.Cursor = Cursors.Hand;
            btnUnitPsi.FlatStyle = FlatStyle.Flat;
            btnUnitPsi.Font = new Font("Segoe UI", 10F);
            btnUnitPsi.ForeColor = Color.Black;
            btnUnitPsi.Location = new Point(91, 41);
            btnUnitPsi.Name = "btnUnitPsi";
            btnUnitPsi.Size = new Size(57, 45);
            btnUnitPsi.TabIndex = 54;
            btnUnitPsi.Text = "psi";
            btnUnitPsi.UseVisualStyleBackColor = false;
            btnUnitPsi.Click += btnUnitPsi_Click;
            // 
            // btnUnitBar
            // 
            btnUnitBar.BackColor = Color.White;
            btnUnitBar.BorderColor = Color.Black;
            btnUnitBar.CornerRadius = 10;
            btnUnitBar.Cursor = Cursors.Hand;
            btnUnitBar.FlatStyle = FlatStyle.Flat;
            btnUnitBar.Font = new Font("Segoe UI", 10F);
            btnUnitBar.ForeColor = Color.Black;
            btnUnitBar.Location = new Point(16, 41);
            btnUnitBar.Name = "btnUnitBar";
            btnUnitBar.Size = new Size(57, 45);
            btnUnitBar.TabIndex = 53;
            btnUnitBar.Text = "Bar";
            btnUnitBar.UseVisualStyleBackColor = false;
            btnUnitBar.Click += btnUnitBar_Click;
            // 
            // lblMaxPressure
            // 
            lblMaxPressure.AutoSize = true;
            lblMaxPressure.Font = new Font("Segoe UI", 10F);
            lblMaxPressure.Location = new Point(166, 91);
            lblMaxPressure.Name = "lblMaxPressure";
            lblMaxPressure.Size = new Size(94, 19);
            lblMaxPressure.TabIndex = 51;
            lblMaxPressure.Text = "Max Pressure:";
            // 
            // numMaxPressure
            // 
            numMaxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMaxPressure.Location = new Point(166, 113);
            numMaxPressure.Name = "numMaxPressure";
            numMaxPressure.Size = new Size(101, 25);
            numMaxPressure.TabIndex = 50;
            numMaxPressure.ValueChanged += numMaxPressure_ValueChanged;
            // 
            // lblMinPressure
            // 
            lblMinPressure.AutoSize = true;
            lblMinPressure.Font = new Font("Segoe UI", 10F);
            lblMinPressure.Location = new Point(22, 91);
            lblMinPressure.Name = "lblMinPressure";
            lblMinPressure.Size = new Size(92, 19);
            lblMinPressure.TabIndex = 49;
            lblMinPressure.Text = "Min Pressure:";
            // 
            // numMinPressure
            // 
            numMinPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMinPressure.Location = new Point(23, 113);
            numMinPressure.Name = "numMinPressure";
            numMinPressure.Size = new Size(106, 25);
            numMinPressure.TabIndex = 48;
            numMinPressure.ValueChanged += numMinPressure_ValueChanged;
            // 
            // btnNoPChange
            // 
            btnNoPChange.BackColor = Color.White;
            btnNoPChange.BorderColor = Color.Black;
            btnNoPChange.CornerRadius = 10;
            btnNoPChange.Cursor = Cursors.Hand;
            btnNoPChange.FlatStyle = FlatStyle.Flat;
            btnNoPChange.Font = new Font("Segoe UI", 10F);
            btnNoPChange.ForeColor = Color.Black;
            btnNoPChange.Location = new Point(213, 157);
            btnNoPChange.Name = "btnNoPChange";
            btnNoPChange.Size = new Size(107, 45);
            btnNoPChange.TabIndex = 46;
            btnNoPChange.Text = "No Change";
            btnNoPChange.UseVisualStyleBackColor = false;
            btnNoPChange.Click += btnNoPChange_Click;
            // 
            // gbxConfigOutput
            // 
            gbxConfigOutput.Controls.Add(lstVoltageRange);
            gbxConfigOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxConfigOutput.Location = new Point(309, 150);
            gbxConfigOutput.Name = "gbxConfigOutput";
            gbxConfigOutput.Size = new Size(388, 214);
            gbxConfigOutput.TabIndex = 63;
            gbxConfigOutput.TabStop = false;
            gbxConfigOutput.Text = "Step 4: Select Voltage Range";
            // 
            // lstVoltageRange
            // 
            lstVoltageRange.Font = new Font("Segoe UI", 10F);
            lstVoltageRange.ItemHeight = 17;
            lstVoltageRange.Items.AddRange(new object[] { "0-10V", "0-5V", "1-5V", "0.5-4.5V", "1-6V" });
            lstVoltageRange.Location = new Point(94, 41);
            lstVoltageRange.Name = "lstVoltageRange";
            lstVoltageRange.Size = new Size(105, 89);
            lstVoltageRange.TabIndex = 41;
            lstVoltageRange.SelectedIndexChanged += lstVoltageRange_SelectedIndexChanged;
            // 
            // lsbOutputConfig
            // 
            lsbOutputConfig.Font = new Font("Segoe UI", 10F);
            lsbOutputConfig.FormattingEnabled = true;
            lsbOutputConfig.ItemHeight = 17;
            lsbOutputConfig.Location = new Point(16, 36);
            lsbOutputConfig.Name = "lsbOutputConfig";
            lsbOutputConfig.Size = new Size(263, 72);
            lsbOutputConfig.TabIndex = 66;
            // 
            // btnConfigDevice
            // 
            btnConfigDevice.BackColor = Color.White;
            btnConfigDevice.BorderColor = Color.Black;
            btnConfigDevice.CornerRadius = 10;
            btnConfigDevice.Cursor = Cursors.Hand;
            btnConfigDevice.FlatStyle = FlatStyle.Flat;
            btnConfigDevice.Font = new Font("Segoe UI", 10F);
            btnConfigDevice.ForeColor = Color.Black;
            btnConfigDevice.Location = new Point(223, 151);
            btnConfigDevice.Name = "btnConfigDevice";
            btnConfigDevice.Size = new Size(160, 45);
            btnConfigDevice.TabIndex = 65;
            btnConfigDevice.Text = "Configure";
            btnConfigDevice.UseVisualStyleBackColor = false;
            btnConfigDevice.Click += btnConfigDevice_Click;
            // 
            // txtJobCode
            // 
            txtJobCode.Location = new Point(763, 470);
            txtJobCode.Name = "txtJobCode";
            txtJobCode.Size = new Size(215, 23);
            txtJobCode.TabIndex = 68;
            txtJobCode.TextChanged += txtJobCode_TextChanged;
            // 
            // lblJobCode
            // 
            lblJobCode.AutoSize = true;
            lblJobCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJobCode.Location = new Point(763, 440);
            lblJobCode.Name = "lblJobCode";
            lblJobCode.Size = new Size(147, 17);
            lblJobCode.TabIndex = 69;
            lblJobCode.Text = "Step 3: Enter Job Code";
            // 
            // btnNextDevice
            // 
            btnNextDevice.BackColor = Color.White;
            btnNextDevice.BorderColor = Color.Black;
            btnNextDevice.CornerRadius = 10;
            btnNextDevice.Cursor = Cursors.Hand;
            btnNextDevice.FlatStyle = FlatStyle.Flat;
            btnNextDevice.Font = new Font("Segoe UI", 10F);
            btnNextDevice.ForeColor = Color.Black;
            btnNextDevice.Location = new Point(854, 531);
            btnNextDevice.Name = "btnNextDevice";
            btnNextDevice.Size = new Size(160, 45);
            btnNextDevice.TabIndex = 70;
            btnNextDevice.Text = "Next Device";
            btnNextDevice.UseVisualStyleBackColor = false;
            btnNextDevice.Click += btnNextDevice_Click;
            // 
            // gbxConfigDevice
            // 
            gbxConfigDevice.Controls.Add(lsbOutputConfig);
            gbxConfigDevice.Controls.Add(btnConfigDevice);
            gbxConfigDevice.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxConfigDevice.Location = new Point(308, 384);
            gbxConfigDevice.Name = "gbxConfigDevice";
            gbxConfigDevice.Size = new Size(389, 202);
            gbxConfigDevice.TabIndex = 71;
            gbxConfigDevice.TabStop = false;
            gbxConfigDevice.Text = "Step 6: Confirm and Configure Device";
            // 
            // btnVoltagePOT
            // 
            btnVoltagePOT.BackColor = Color.White;
            btnVoltagePOT.BorderColor = Color.Black;
            btnVoltagePOT.CornerRadius = 10;
            btnVoltagePOT.Cursor = Cursors.Hand;
            btnVoltagePOT.FlatStyle = FlatStyle.Flat;
            btnVoltagePOT.Font = new Font("Segoe UI", 10F);
            btnVoltagePOT.ForeColor = Color.Black;
            btnVoltagePOT.Location = new Point(541, 90);
            btnVoltagePOT.Name = "btnVoltagePOT";
            btnVoltagePOT.Size = new Size(107, 45);
            btnVoltagePOT.TabIndex = 72;
            btnVoltagePOT.Text = "Voltage";
            btnVoltagePOT.UseVisualStyleBackColor = false;
            btnVoltagePOT.Click += btnVoltagePOT_Click;
            // 
            // btnRatioPOT
            // 
            btnRatioPOT.BackColor = Color.White;
            btnRatioPOT.BorderColor = Color.Black;
            btnRatioPOT.CornerRadius = 10;
            btnRatioPOT.Cursor = Cursors.Hand;
            btnRatioPOT.FlatStyle = FlatStyle.Flat;
            btnRatioPOT.Font = new Font("Segoe UI", 10F);
            btnRatioPOT.ForeColor = Color.Black;
            btnRatioPOT.Location = new Point(654, 90);
            btnRatioPOT.Name = "btnRatioPOT";
            btnRatioPOT.Size = new Size(110, 45);
            btnRatioPOT.TabIndex = 73;
            btnRatioPOT.Text = "Ratiometric";
            btnRatioPOT.UseVisualStyleBackColor = false;
            btnRatioPOT.Click += btnRatioPOT_Click;
            // 
            // btnCurrentPOT
            // 
            btnCurrentPOT.BackColor = Color.White;
            btnCurrentPOT.BorderColor = Color.Black;
            btnCurrentPOT.CornerRadius = 10;
            btnCurrentPOT.Cursor = Cursors.Hand;
            btnCurrentPOT.FlatStyle = FlatStyle.Flat;
            btnCurrentPOT.Font = new Font("Segoe UI", 10F);
            btnCurrentPOT.ForeColor = Color.Black;
            btnCurrentPOT.Location = new Point(770, 90);
            btnCurrentPOT.Name = "btnCurrentPOT";
            btnCurrentPOT.Size = new Size(104, 45);
            btnCurrentPOT.TabIndex = 74;
            btnCurrentPOT.Text = "Current";
            btnCurrentPOT.UseVisualStyleBackColor = false;
            btnCurrentPOT.Click += btnCurrentPOT_Click;
            // 
            // txtStockCode
            // 
            txtStockCode.Font = new Font("Segoe UI", 10F);
            txtStockCode.Location = new Point(633, 39);
            txtStockCode.Name = "txtStockCode";
            txtStockCode.Size = new Size(140, 25);
            txtStockCode.TabIndex = 75;
            // 
            // lblEnterStockCode
            // 
            lblEnterStockCode.AutoSize = true;
            lblEnterStockCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEnterStockCode.Location = new Point(559, 19);
            lblEnterStockCode.Name = "lblEnterStockCode";
            lblEnterStockCode.Size = new Size(116, 17);
            lblEnterStockCode.TabIndex = 76;
            lblEnterStockCode.Text = "Enter Stock Code:";
            // 
            // btnLoadStockCode
            // 
            btnLoadStockCode.BackColor = Color.White;
            btnLoadStockCode.BorderColor = Color.Black;
            btnLoadStockCode.CornerRadius = 10;
            btnLoadStockCode.Cursor = Cursors.Hand;
            btnLoadStockCode.FlatStyle = FlatStyle.Flat;
            btnLoadStockCode.Font = new Font("Segoe UI", 10F);
            btnLoadStockCode.ForeColor = Color.Black;
            btnLoadStockCode.Location = new Point(787, 28);
            btnLoadStockCode.Name = "btnLoadStockCode";
            btnLoadStockCode.Size = new Size(123, 45);
            btnLoadStockCode.TabIndex = 77;
            btnLoadStockCode.Text = "Load Stock Code";
            btnLoadStockCode.UseVisualStyleBackColor = false;
            btnLoadStockCode.Click += btnLoadStockCode_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1201, 654);
            Controls.Add(btnLoadStockCode);
            Controls.Add(lblEnterStockCode);
            Controls.Add(txtStockCode);
            Controls.Add(btnCurrentPOT);
            Controls.Add(btnRatioPOT);
            Controls.Add(btnVoltagePOT);
            Controls.Add(txtJobCode);
            Controls.Add(lblJobCode);
            Controls.Add(btnNextDevice);
            Controls.Add(gbxConfigDevice);
            Controls.Add(gbxConfigPressure);
            Controls.Add(gbxConfigOutput);
            Controls.Add(lblStep2);
            Controls.Add(label1);
            Controls.Add(btnConnectDevice);
            Controls.Add(btnClose);
            Controls.Add(btnInitHW);
            Controls.Add(listBoxDebug);
            Controls.Add(button10);
            Name = "Form2";
            Text = "Form2";
            gbxConfigPressure.ResumeLayout(false);
            gbxConfigPressure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).EndInit();
            gbxConfigOutput.ResumeLayout(false);
            gbxConfigDevice.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button10;
        private ListBox listBoxDebug;
        private ATPButton btnInitHW;
        private ATPButton btnClose;
        private ATPButton btnConnectDevice;
        private Label label1;
        private Label lblStep2;
        private GroupBox gbxConfigPressure;
        private Label lblSelectUnit;
        private ATPButton btnUnitPsi;
        private ATPButton btnUnitBar;
        private Label lblMaxPressure;
        private NumericUpDown numMaxPressure;
        private Label lblMinPressure;
        private NumericUpDown numMinPressure;
        private ATPButton btnNoPChange;
        private GroupBox gbxConfigOutput;
        private ListBox lstVoltageRange;
        private ListBox lsbOutputConfig;
        private ATPButton btnConfigDevice;
        private ATPButton btnConfirmPressure;
        private TextBox txtJobCode;
        private Label lblJobCode;
        private ATPButton btnNextDevice;
        private GroupBox gbxConfigDevice;
        private ATPButton btnVoltagePOT;
        private ATPButton btnRatioPOT;
        private ATPButton btnCurrentPOT;
        private TextBox txtStockCode;
        private Label lblEnterStockCode;
        private ATPButton btnLoadStockCode;
    }
}