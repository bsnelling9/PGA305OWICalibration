namespace PGA305OWICalibration.Forms
{
    partial class StockCodeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
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
            pnlCard = new PGA305OWICalibration.UIControls.ATPGroupBox();
            lblTitle = new Label();
            gbxStockCode = new PGA305OWICalibration.UIControls.ATPGroupBox();
            lblStockCodeHeader = new Label();
            txtStockCode = new TextBox();
            gbxOutput = new PGA305OWICalibration.UIControls.ATPGroupBox();
            lblOutputHeader = new Label();
            lblOutputType = new Label();
            btnRatiometric = new PGA305OWICalibration.UIControls.ATPButton();
            btnCurrent = new PGA305OWICalibration.UIControls.ATPButton();
            btnVoltage = new PGA305OWICalibration.UIControls.ATPButton();
            lblMinOutput = new Label();
            numMinOutput = new NumericUpDown();
            lblMaxOuput = new Label();
            numMaxOutput = new NumericUpDown();
            lstVoltageRange = new ListBox();
            gbxPressure = new PGA305OWICalibration.UIControls.ATPGroupBox();
            lblPressureHeader = new Label();
            lblUnits = new Label();
            btnUnitPsi = new PGA305OWICalibration.UIControls.ATPButton();
            btnUnitBar = new PGA305OWICalibration.UIControls.ATPButton();
            lblMinPressure = new Label();
            numMinPressure = new NumericUpDown();
            lblMaxPressure = new Label();
            numMaxPressure = new NumericUpDown();
            btnClose = new PGA305OWICalibration.UIControls.ATPButton();
            btnSave = new PGA305OWICalibration.UIControls.ATPButton();
            pnlCard.SuspendLayout();
            gbxStockCode.SuspendLayout();
            gbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinOutput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutput).BeginInit();
            gbxPressure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).BeginInit();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.BorderColor = Color.FromArgb(214, 217, 224);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(gbxStockCode);
            pnlCard.Controls.Add(gbxOutput);
            pnlCard.Controls.Add(gbxPressure);
            pnlCard.Controls.Add(btnClose);
            pnlCard.Controls.Add(btnSave);
            pnlCard.CornerRadius = 10;
            pnlCard.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            pnlCard.ForeColor = Color.FromArgb(107, 114, 128);
            pnlCard.Location = new Point(20, 20);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(500, 820);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(24, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(266, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Create Stock Code";
            // 
            // gbxStockCode
            // 
            gbxStockCode.BackColor = Color.FromArgb(247, 248, 250);
            gbxStockCode.BorderColor = Color.FromArgb(214, 217, 224);
            gbxStockCode.Controls.Add(lblStockCodeHeader);
            gbxStockCode.Controls.Add(txtStockCode);
            gbxStockCode.CornerRadius = 10;
            gbxStockCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            gbxStockCode.ForeColor = Color.FromArgb(107, 114, 128);
            gbxStockCode.Location = new Point(28, 84);
            gbxStockCode.Name = "gbxStockCode";
            gbxStockCode.Size = new Size(444, 92);
            gbxStockCode.TabIndex = 1;
            // 
            // lblStockCodeHeader
            // 
            lblStockCodeHeader.AutoSize = true;
            lblStockCodeHeader.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblStockCodeHeader.Location = new Point(16, 14);
            lblStockCodeHeader.Name = "lblStockCodeHeader";
            lblStockCodeHeader.Size = new Size(88, 17);
            lblStockCodeHeader.TabIndex = 0;
            lblStockCodeHeader.Text = "STOCK CODE";
            // 
            // txtStockCode
            // 
            txtStockCode.BackColor = Color.White;
            txtStockCode.BorderStyle = BorderStyle.FixedSingle;
            txtStockCode.CharacterCasing = CharacterCasing.Upper;
            txtStockCode.Font = new Font("Segoe UI", 10F);
            txtStockCode.ForeColor = Color.FromArgb(26, 29, 36);
            txtStockCode.Location = new Point(16, 44);
            txtStockCode.Name = "txtStockCode";
            txtStockCode.PlaceholderText = "e.g. A106985";
            txtStockCode.Size = new Size(412, 25);
            txtStockCode.TabIndex = 1;
            txtStockCode.TextChanged += txtStockCode_TextChanged;
            // 
            // gbxOutput
            // 
            gbxOutput.BackColor = Color.FromArgb(247, 248, 250);
            gbxOutput.BorderColor = Color.FromArgb(214, 217, 224);
            gbxOutput.Controls.Add(lblOutputHeader);
            gbxOutput.Controls.Add(lblOutputType);
            gbxOutput.Controls.Add(btnRatiometric);
            gbxOutput.Controls.Add(btnCurrent);
            gbxOutput.Controls.Add(btnVoltage);
            gbxOutput.Controls.Add(lblMinOutput);
            gbxOutput.Controls.Add(numMinOutput);
            gbxOutput.Controls.Add(lblMaxOuput);
            gbxOutput.Controls.Add(numMaxOutput);
            gbxOutput.Controls.Add(lstVoltageRange);
            gbxOutput.CornerRadius = 10;
            gbxOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            gbxOutput.ForeColor = Color.FromArgb(107, 114, 128);
            gbxOutput.Location = new Point(28, 192);
            gbxOutput.Name = "gbxOutput";
            gbxOutput.Size = new Size(444, 307);
            gbxOutput.TabIndex = 2;
            // 
            // lblOutputHeader
            // 
            lblOutputHeader.AutoSize = true;
            lblOutputHeader.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblOutputHeader.Location = new Point(16, 14);
            lblOutputHeader.Name = "lblOutputHeader";
            lblOutputHeader.Size = new Size(60, 17);
            lblOutputHeader.TabIndex = 0;
            lblOutputHeader.Text = "OUTPUT";
            // 
            // lblOutputType
            // 
            lblOutputType.AutoSize = true;
            lblOutputType.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblOutputType.ForeColor = Color.FromArgb(26, 29, 36);
            lblOutputType.Location = new Point(16, 40);
            lblOutputType.Name = "lblOutputType";
            lblOutputType.Size = new Size(85, 17);
            lblOutputType.TabIndex = 1;
            lblOutputType.Text = "Output Type";
            // 
            // btnRatiometric
            // 
            btnRatiometric.BackColor = Color.White;
            btnRatiometric.BorderColor = Color.FromArgb(214, 217, 224);
            btnRatiometric.BorderSize = 2;
            btnRatiometric.CornerRadius = 10;
            btnRatiometric.FlatAppearance.BorderSize = 0;
            btnRatiometric.FlatStyle = FlatStyle.Flat;
            btnRatiometric.Font = new Font("Segoe UI", 10F);
            btnRatiometric.ForeColor = Color.FromArgb(26, 29, 36);
            btnRatiometric.Location = new Point(16, 64);
            btnRatiometric.Name = "btnRatiometric";
            btnRatiometric.Size = new Size(130, 36);
            btnRatiometric.TabIndex = 2;
            btnRatiometric.Text = "Ratiometric";
            btnRatiometric.UseVisualStyleBackColor = false;
            btnRatiometric.Click += btnRatiometric_Click;
            // 
            // btnCurrent
            // 
            btnCurrent.BackColor = Color.White;
            btnCurrent.BorderColor = Color.FromArgb(214, 217, 224);
            btnCurrent.BorderSize = 2;
            btnCurrent.CornerRadius = 10;
            btnCurrent.FlatAppearance.BorderSize = 0;
            btnCurrent.FlatStyle = FlatStyle.Flat;
            btnCurrent.Font = new Font("Segoe UI", 10F);
            btnCurrent.ForeColor = Color.FromArgb(26, 29, 36);
            btnCurrent.Location = new Point(157, 64);
            btnCurrent.Name = "btnCurrent";
            btnCurrent.Size = new Size(130, 36);
            btnCurrent.TabIndex = 3;
            btnCurrent.Text = "Current";
            btnCurrent.UseVisualStyleBackColor = false;
            btnCurrent.Click += btnCurrent_Click;
            // 
            // btnVoltage
            // 
            btnVoltage.BackColor = Color.White;
            btnVoltage.BorderColor = Color.FromArgb(214, 217, 224);
            btnVoltage.BorderSize = 2;
            btnVoltage.CornerRadius = 10;
            btnVoltage.FlatAppearance.BorderSize = 0;
            btnVoltage.FlatStyle = FlatStyle.Flat;
            btnVoltage.Font = new Font("Segoe UI", 10F);
            btnVoltage.ForeColor = Color.FromArgb(26, 29, 36);
            btnVoltage.Location = new Point(298, 64);
            btnVoltage.Name = "btnVoltage";
            btnVoltage.Size = new Size(130, 36);
            btnVoltage.TabIndex = 4;
            btnVoltage.Text = "Voltage";
            btnVoltage.UseVisualStyleBackColor = false;
            btnVoltage.Click += btnVoltage_Click;
            // 
            // lblMinOutput
            // 
            lblMinOutput.AutoSize = true;
            lblMinOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblMinOutput.ForeColor = Color.FromArgb(26, 29, 36);
            lblMinOutput.Location = new Point(16, 112);
            lblMinOutput.Name = "lblMinOutput";
            lblMinOutput.Size = new Size(80, 17);
            lblMinOutput.TabIndex = 5;
            lblMinOutput.Text = "Output Min";
            // 
            // numMinOutput
            // 
            numMinOutput.BackColor = Color.White;
            numMinOutput.BorderStyle = BorderStyle.FixedSingle;
            numMinOutput.Font = new Font("Segoe UI", 10F);
            numMinOutput.ForeColor = Color.FromArgb(26, 29, 36);
            numMinOutput.Location = new Point(16, 134);
            numMinOutput.Name = "numMinOutput";
            numMinOutput.Size = new Size(198, 25);
            numMinOutput.TabIndex = 6;
            // 
            // lblMaxOuput
            // 
            lblMaxOuput.AutoSize = true;
            lblMaxOuput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblMaxOuput.ForeColor = Color.FromArgb(26, 29, 36);
            lblMaxOuput.Location = new Point(230, 112);
            lblMaxOuput.Name = "lblMaxOuput";
            lblMaxOuput.Size = new Size(82, 17);
            lblMaxOuput.TabIndex = 7;
            lblMaxOuput.Text = "Output Max";
            // 
            // numMaxOutput
            // 
            numMaxOutput.BackColor = Color.White;
            numMaxOutput.BorderStyle = BorderStyle.FixedSingle;
            numMaxOutput.Font = new Font("Segoe UI", 10F);
            numMaxOutput.ForeColor = Color.FromArgb(26, 29, 36);
            numMaxOutput.Location = new Point(230, 134);
            numMaxOutput.Name = "numMaxOutput";
            numMaxOutput.Size = new Size(198, 25);
            numMaxOutput.TabIndex = 8;
            // 
            // lstVoltageRange
            // 
            lstVoltageRange.BackColor = Color.White;
            lstVoltageRange.BorderStyle = BorderStyle.FixedSingle;
            lstVoltageRange.ColumnWidth = 136;
            lstVoltageRange.Font = new Font("Segoe UI", 10F);
            lstVoltageRange.ForeColor = Color.FromArgb(26, 29, 36);
            lstVoltageRange.FormattingEnabled = true;
            lstVoltageRange.IntegralHeight = false;
            lstVoltageRange.ItemHeight = 17;
            lstVoltageRange.Location = new Point(16, 172);
            lstVoltageRange.MultiColumn = true;
            lstVoltageRange.Name = "lstVoltageRange";
            lstVoltageRange.Size = new Size(412, 121);
            lstVoltageRange.TabIndex = 9;
            lstVoltageRange.SelectedIndexChanged += lstVoltageRange_SelectedIndexChanged;
            // 
            // gbxPressure
            // 
            gbxPressure.BackColor = Color.FromArgb(247, 248, 250);
            gbxPressure.BorderColor = Color.FromArgb(214, 217, 224);
            gbxPressure.Controls.Add(lblPressureHeader);
            gbxPressure.Controls.Add(lblUnits);
            gbxPressure.Controls.Add(btnUnitPsi);
            gbxPressure.Controls.Add(btnUnitBar);
            gbxPressure.Controls.Add(lblMinPressure);
            gbxPressure.Controls.Add(numMinPressure);
            gbxPressure.Controls.Add(lblMaxPressure);
            gbxPressure.Controls.Add(numMaxPressure);
            gbxPressure.CornerRadius = 10;
            gbxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            gbxPressure.ForeColor = Color.FromArgb(107, 114, 128);
            gbxPressure.Location = new Point(28, 516);
            gbxPressure.Name = "gbxPressure";
            gbxPressure.Size = new Size(444, 176);
            gbxPressure.TabIndex = 3;
            // 
            // lblPressureHeader
            // 
            lblPressureHeader.AutoSize = true;
            lblPressureHeader.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblPressureHeader.Location = new Point(16, 14);
            lblPressureHeader.Name = "lblPressureHeader";
            lblPressureHeader.Size = new Size(69, 17);
            lblPressureHeader.TabIndex = 0;
            lblPressureHeader.Text = "PRESSURE";
            // 
            // lblUnits
            // 
            lblUnits.AutoSize = true;
            lblUnits.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblUnits.ForeColor = Color.FromArgb(26, 29, 36);
            lblUnits.Location = new Point(16, 40);
            lblUnits.Name = "lblUnits";
            lblUnits.Size = new Size(40, 17);
            lblUnits.TabIndex = 1;
            lblUnits.Text = "Units";
            // 
            // btnUnitPsi
            // 
            btnUnitPsi.BackColor = Color.White;
            btnUnitPsi.BorderColor = Color.FromArgb(214, 217, 224);
            btnUnitPsi.BorderSize = 2;
            btnUnitPsi.CornerRadius = 10;
            btnUnitPsi.FlatAppearance.BorderSize = 0;
            btnUnitPsi.FlatStyle = FlatStyle.Flat;
            btnUnitPsi.Font = new Font("Segoe UI", 10F);
            btnUnitPsi.ForeColor = Color.FromArgb(26, 29, 36);
            btnUnitPsi.Location = new Point(16, 64);
            btnUnitPsi.Name = "btnUnitPsi";
            btnUnitPsi.Size = new Size(130, 36);
            btnUnitPsi.TabIndex = 2;
            btnUnitPsi.Text = "psi";
            btnUnitPsi.UseVisualStyleBackColor = false;
            btnUnitPsi.Click += btnUnitPsi_Click;
            // 
            // btnUnitBar
            // 
            btnUnitBar.BackColor = Color.White;
            btnUnitBar.BorderColor = Color.FromArgb(214, 217, 224);
            btnUnitBar.BorderSize = 2;
            btnUnitBar.CornerRadius = 10;
            btnUnitBar.FlatAppearance.BorderSize = 0;
            btnUnitBar.FlatStyle = FlatStyle.Flat;
            btnUnitBar.Font = new Font("Segoe UI", 10F);
            btnUnitBar.ForeColor = Color.FromArgb(26, 29, 36);
            btnUnitBar.Location = new Point(298, 64);
            btnUnitBar.Name = "btnUnitBar";
            btnUnitBar.Size = new Size(130, 36);
            btnUnitBar.TabIndex = 3;
            btnUnitBar.Text = "bar";
            btnUnitBar.UseVisualStyleBackColor = false;
            btnUnitBar.Click += btnUnitBar_Click;
            // 
            // lblMinPressure
            // 
            lblMinPressure.AutoSize = true;
            lblMinPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblMinPressure.ForeColor = Color.FromArgb(26, 29, 36);
            lblMinPressure.Location = new Point(16, 112);
            lblMinPressure.Name = "lblMinPressure";
            lblMinPressure.Size = new Size(88, 17);
            lblMinPressure.TabIndex = 4;
            lblMinPressure.Text = "Pressure Min";
            // 
            // numMinPressure
            // 
            numMinPressure.BackColor = Color.White;
            numMinPressure.BorderStyle = BorderStyle.FixedSingle;
            numMinPressure.Font = new Font("Segoe UI", 10F);
            numMinPressure.ForeColor = Color.FromArgb(26, 29, 36);
            numMinPressure.Location = new Point(16, 134);
            numMinPressure.Name = "numMinPressure";
            numMinPressure.Size = new Size(198, 25);
            numMinPressure.TabIndex = 5;
            // 
            // lblMaxPressure
            // 
            lblMaxPressure.AutoSize = true;
            lblMaxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblMaxPressure.ForeColor = Color.FromArgb(26, 29, 36);
            lblMaxPressure.Location = new Point(230, 112);
            lblMaxPressure.Name = "lblMaxPressure";
            lblMaxPressure.Size = new Size(90, 17);
            lblMaxPressure.TabIndex = 6;
            lblMaxPressure.Text = "Pressure Max";
            // 
            // numMaxPressure
            // 
            numMaxPressure.BackColor = Color.White;
            numMaxPressure.BorderStyle = BorderStyle.FixedSingle;
            numMaxPressure.Font = new Font("Segoe UI", 10F);
            numMaxPressure.ForeColor = Color.FromArgb(26, 29, 36);
            numMaxPressure.Location = new Point(230, 134);
            numMaxPressure.Name = "numMaxPressure";
            numMaxPressure.Size = new Size(198, 25);
            numMaxPressure.TabIndex = 7;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.BorderColor = Color.Black;
            btnClose.BorderSize = 2;
            btnClose.CornerRadius = 10;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 11F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(24, 738);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(140, 50);
            btnClose.TabIndex = 5;
            btnClose.Text = "Cancel";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(36, 84, 216);
            btnSave.BorderColor = Color.FromArgb(36, 84, 216);
            btnSave.BorderSize = 2;
            btnSave.CornerRadius = 10;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(184, 738);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(288, 50);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save Stock Code";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // StockCodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            CancelButton = btnClose;
            ClientSize = new Size(540, 852);
            Controls.Add(pnlCard);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StockCodeForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create Stock Code";
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            gbxStockCode.ResumeLayout(false);
            gbxStockCode.PerformLayout();
            gbxOutput.ResumeLayout(false);
            gbxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinOutput).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutput).EndInit();
            gbxPressure.ResumeLayout(false);
            gbxPressure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private UIControls.ATPGroupBox pnlCard;
        private Label lblTitle;
        private UIControls.ATPGroupBox gbxStockCode;
        private Label lblStockCodeHeader;
        private TextBox txtStockCode;
        private UIControls.ATPGroupBox gbxOutput;
        private Label lblOutputHeader;
        private Label lblOutputType;
        private UIControls.ATPButton btnRatiometric;
        private UIControls.ATPButton btnCurrent;
        private UIControls.ATPButton btnVoltage;
        private Label lblMinOutput;
        private NumericUpDown numMinOutput;
        private Label lblMaxOuput;
        private NumericUpDown numMaxOutput;
        private ListBox lstVoltageRange;
        private UIControls.ATPGroupBox gbxPressure;
        private Label lblPressureHeader;
        private Label lblUnits;
        private UIControls.ATPButton btnUnitPsi;
        private UIControls.ATPButton btnUnitBar;
        private Label lblMinPressure;
        private NumericUpDown numMinPressure;
        private Label lblMaxPressure;
        private NumericUpDown numMaxPressure;
        private UIControls.ATPButton btnClose;
        private UIControls.ATPButton btnSave;
    }
}