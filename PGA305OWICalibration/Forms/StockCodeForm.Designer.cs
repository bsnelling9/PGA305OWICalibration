namespace PGA305OWICalibration.Forms
{
    partial class StockCodeForm
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
            btnSave = new PGA305OWICalibration.UIControls.ATPButton();
            lblStockCodeAll = new Label();
            txtStockCode = new TextBox();
            btnUnitPsi = new PGA305OWICalibration.UIControls.ATPButton();
            btnUnitBar = new PGA305OWICalibration.UIControls.ATPButton();
            lblMaxPressure = new Label();
            numMaxPressure = new NumericUpDown();
            lblMinPressure = new Label();
            numMinPressure = new NumericUpDown();
            lstVoltageRange = new ListBox();
            lblMinOutput = new Label();
            lblMaxOuput = new Label();
            numMinOutput = new NumericUpDown();
            numMaxOutput = new NumericUpDown();
            btnCurrent = new PGA305OWICalibration.UIControls.ATPButton();
            btnRatiometric = new PGA305OWICalibration.UIControls.ATPButton();
            btnVoltage = new PGA305OWICalibration.UIControls.ATPButton();
            btnClose = new PGA305OWICalibration.UIControls.ATPButton();
            gbxElectricalOutput = new GroupBox();
            gbxPressure = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinOutput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutput).BeginInit();
            gbxElectricalOutput.SuspendLayout();
            gbxPressure.SuspendLayout();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.White;
            btnSave.BorderColor = Color.Black;
            btnSave.BorderSize = 2;
            btnSave.CornerRadius = 10;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.ForeColor = Color.Black;
            btnSave.Location = new Point(656, 393);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(132, 45);
            btnSave.TabIndex = 88;
            btnSave.Text = "Save and Close";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblStockCodeAll
            // 
            lblStockCodeAll.AutoSize = true;
            lblStockCodeAll.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockCodeAll.Location = new Point(315, 28);
            lblStockCodeAll.Name = "lblStockCodeAll";
            lblStockCodeAll.Size = new Size(116, 17);
            lblStockCodeAll.TabIndex = 90;
            lblStockCodeAll.Text = "Enter Stock Code:";
            // 
            // txtStockCode
            // 
            txtStockCode.Location = new Point(315, 46);
            txtStockCode.Name = "txtStockCode";
            txtStockCode.Size = new Size(144, 23);
            txtStockCode.TabIndex = 89;
            txtStockCode.TextChanged += txtStockCode_TextChanged;
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
            btnUnitPsi.Location = new Point(192, 39);
            btnUnitPsi.Name = "btnUnitPsi";
            btnUnitPsi.Size = new Size(57, 45);
            btnUnitPsi.TabIndex = 99;
            btnUnitPsi.Text = "psi";
            btnUnitPsi.UseVisualStyleBackColor = false;
            btnUnitPsi.Click += btnUnitPsi_Click;
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
            btnUnitBar.Location = new Point(45, 39);
            btnUnitBar.Name = "btnUnitBar";
            btnUnitBar.Size = new Size(57, 45);
            btnUnitBar.TabIndex = 98;
            btnUnitBar.Text = "Bar";
            btnUnitBar.UseVisualStyleBackColor = false;
            btnUnitBar.Click += btnUnitBar_Click;
            // 
            // lblMaxPressure
            // 
            lblMaxPressure.AutoSize = true;
            lblMaxPressure.Font = new Font("Segoe UI", 10F);
            lblMaxPressure.Location = new Point(25, 169);
            lblMaxPressure.Name = "lblMaxPressure";
            lblMaxPressure.Size = new Size(94, 19);
            lblMaxPressure.TabIndex = 97;
            lblMaxPressure.Text = "Max Pressure:";
            // 
            // numMaxPressure
            // 
            numMaxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMaxPressure.Location = new Point(187, 167);
            numMaxPressure.Name = "numMaxPressure";
            numMaxPressure.Size = new Size(106, 25);
            numMaxPressure.TabIndex = 96;
            numMaxPressure.ValueChanged += numMaxPressure_ValueChanged;
            // 
            // lblMinPressure
            // 
            lblMinPressure.AutoSize = true;
            lblMinPressure.Font = new Font("Segoe UI", 10F);
            lblMinPressure.Location = new Point(25, 113);
            lblMinPressure.Name = "lblMinPressure";
            lblMinPressure.Size = new Size(92, 19);
            lblMinPressure.TabIndex = 95;
            lblMinPressure.Text = "Min Pressure:";
            // 
            // numMinPressure
            // 
            numMinPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMinPressure.Location = new Point(187, 107);
            numMinPressure.Name = "numMinPressure";
            numMinPressure.Size = new Size(106, 25);
            numMinPressure.TabIndex = 94;
            numMinPressure.ValueChanged += numMinPressure_ValueChanged;
            // 
            // lstVoltageRange
            // 
            lstVoltageRange.Font = new Font("Segoe UI", 10F);
            lstVoltageRange.ItemHeight = 17;
            lstVoltageRange.Items.AddRange(new object[] { "0-10V", "0-5V", "1-5V", "0.5-4.5V", "1-6V" });
            lstVoltageRange.Location = new Point(10, 110);
            lstVoltageRange.Name = "lstVoltageRange";
            lstVoltageRange.Size = new Size(105, 89);
            lstVoltageRange.TabIndex = 102;
            lstVoltageRange.SelectedIndexChanged += lstVoltageRange_SelectedIndexChanged;
            // 
            // lblMinOutput
            // 
            lblMinOutput.AutoSize = true;
            lblMinOutput.Font = new Font("Segoe UI", 10F);
            lblMinOutput.Location = new Point(146, 113);
            lblMinOutput.Name = "lblMinOutput";
            lblMinOutput.Size = new Size(85, 19);
            lblMinOutput.TabIndex = 103;
            lblMinOutput.Text = "Min Output:";
            // 
            // lblMaxOuput
            // 
            lblMaxOuput.AutoSize = true;
            lblMaxOuput.Font = new Font("Segoe UI", 10F);
            lblMaxOuput.Location = new Point(146, 169);
            lblMaxOuput.Name = "lblMaxOuput";
            lblMaxOuput.Size = new Size(87, 19);
            lblMaxOuput.TabIndex = 104;
            lblMaxOuput.Text = "Max Output:";
            // 
            // numMinOutput
            // 
            numMinOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMinOutput.Location = new Point(250, 108);
            numMinOutput.Name = "numMinOutput";
            numMinOutput.Size = new Size(106, 25);
            numMinOutput.TabIndex = 105;
            numMinOutput.ValueChanged += numMinOutput_ValueChanged;
            // 
            // numMaxOutput
            // 
            numMaxOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numMaxOutput.Location = new Point(250, 169);
            numMaxOutput.Name = "numMaxOutput";
            numMaxOutput.Size = new Size(106, 25);
            numMaxOutput.TabIndex = 106;
            numMaxOutput.ValueChanged += numMaxOutput_ValueChanged;
            // 
            // btnCurrent
            // 
            btnCurrent.BackColor = Color.White;
            btnCurrent.BorderColor = Color.Black;
            btnCurrent.BorderSize = 2;
            btnCurrent.CornerRadius = 10;
            btnCurrent.Cursor = Cursors.Hand;
            btnCurrent.FlatStyle = FlatStyle.Flat;
            btnCurrent.Font = new Font("Segoe UI", 10F);
            btnCurrent.ForeColor = Color.Black;
            btnCurrent.Location = new Point(252, 39);
            btnCurrent.Name = "btnCurrent";
            btnCurrent.Size = new Size(104, 45);
            btnCurrent.TabIndex = 109;
            btnCurrent.Text = "Current";
            btnCurrent.UseVisualStyleBackColor = false;
            btnCurrent.Click += btnCurrent_Click;
            // 
            // btnRatiometric
            // 
            btnRatiometric.BackColor = Color.White;
            btnRatiometric.BorderColor = Color.Black;
            btnRatiometric.BorderSize = 2;
            btnRatiometric.CornerRadius = 10;
            btnRatiometric.Cursor = Cursors.Hand;
            btnRatiometric.FlatStyle = FlatStyle.Flat;
            btnRatiometric.Font = new Font("Segoe UI", 10F);
            btnRatiometric.ForeColor = Color.Black;
            btnRatiometric.Location = new Point(123, 39);
            btnRatiometric.Name = "btnRatiometric";
            btnRatiometric.Size = new Size(110, 45);
            btnRatiometric.TabIndex = 108;
            btnRatiometric.Text = "Ratiometric";
            btnRatiometric.UseVisualStyleBackColor = false;
            btnRatiometric.Click += btnRatiometric_Click;
            // 
            // btnVoltage
            // 
            btnVoltage.BackColor = Color.White;
            btnVoltage.BorderColor = Color.Black;
            btnVoltage.BorderSize = 2;
            btnVoltage.CornerRadius = 10;
            btnVoltage.Cursor = Cursors.Hand;
            btnVoltage.FlatStyle = FlatStyle.Flat;
            btnVoltage.Font = new Font("Segoe UI", 10F);
            btnVoltage.ForeColor = Color.Black;
            btnVoltage.Location = new Point(8, 39);
            btnVoltage.Name = "btnVoltage";
            btnVoltage.Size = new Size(107, 45);
            btnVoltage.TabIndex = 107;
            btnVoltage.Text = "Voltage";
            btnVoltage.UseVisualStyleBackColor = false;
            btnVoltage.Click += btnVoltage_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.BorderColor = Color.Black;
            btnClose.BorderSize = 2;
            btnClose.CornerRadius = 10;
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(553, 393);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(97, 45);
            btnClose.TabIndex = 110;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // gbxElectricalOutput
            // 
            gbxElectricalOutput.Controls.Add(btnCurrent);
            gbxElectricalOutput.Controls.Add(btnRatiometric);
            gbxElectricalOutput.Controls.Add(btnVoltage);
            gbxElectricalOutput.Controls.Add(numMaxOutput);
            gbxElectricalOutput.Controls.Add(numMinOutput);
            gbxElectricalOutput.Controls.Add(lblMaxOuput);
            gbxElectricalOutput.Controls.Add(lblMinOutput);
            gbxElectricalOutput.Controls.Add(lstVoltageRange);
            gbxElectricalOutput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxElectricalOutput.Location = new Point(19, 84);
            gbxElectricalOutput.Name = "gbxElectricalOutput";
            gbxElectricalOutput.Size = new Size(372, 231);
            gbxElectricalOutput.TabIndex = 111;
            gbxElectricalOutput.TabStop = false;
            gbxElectricalOutput.Text = "Select Output";
            // 
            // gbxPressure
            // 
            gbxPressure.Controls.Add(btnUnitPsi);
            gbxPressure.Controls.Add(btnUnitBar);
            gbxPressure.Controls.Add(lblMaxPressure);
            gbxPressure.Controls.Add(numMaxPressure);
            gbxPressure.Controls.Add(lblMinPressure);
            gbxPressure.Controls.Add(numMinPressure);
            gbxPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbxPressure.Location = new Point(402, 84);
            gbxPressure.Name = "gbxPressure";
            gbxPressure.Size = new Size(324, 231);
            gbxPressure.TabIndex = 112;
            gbxPressure.TabStop = false;
            gbxPressure.Text = "Select Units and Pressure Range";
            // 
            // StockCodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gbxPressure);
            Controls.Add(gbxElectricalOutput);
            Controls.Add(btnClose);
            Controls.Add(lblStockCodeAll);
            Controls.Add(txtStockCode);
            Controls.Add(btnSave);
            Name = "StockCodeForm";
            Text = "StockCodeForm";
            ((System.ComponentModel.ISupportInitialize)numMaxPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinPressure).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinOutput).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxOutput).EndInit();
            gbxElectricalOutput.ResumeLayout(false);
            gbxElectricalOutput.PerformLayout();
            gbxPressure.ResumeLayout(false);
            gbxPressure.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UIControls.ATPButton btnSave;
        private Label lblStockCodeAll;
        private TextBox txtStockCode;
        private UIControls.ATPButton btnUnitPsi;
        private UIControls.ATPButton btnUnitBar;
        private Label lblMaxPressure;
        private NumericUpDown numMaxPressure;
        private Label lblMinPressure;
        private NumericUpDown numMinPressure;
        private ListBox lstVoltageRange;
        private Label lblMinOutput;
        private Label lblMaxOuput;
        private NumericUpDown numMinOutput;
        private NumericUpDown numMaxOutput;
        private UIControls.ATPButton btnCurrent;
        private UIControls.ATPButton btnRatiometric;
        private UIControls.ATPButton btnVoltage;
        private UIControls.ATPButton btnClose;
        private GroupBox gbxElectricalOutput;
        private GroupBox gbxPressure;
    }
}