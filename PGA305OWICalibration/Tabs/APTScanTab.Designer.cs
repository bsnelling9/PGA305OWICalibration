using PGA305OWICalibration.UIControls;

namespace PGA305OWICalibration.Tabs
{
    partial class APTScanTab : UserControl
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
            tlpCards = new TableLayoutPanel();
            btnConfigureAll = new ATPButton();
            txtBatchStockCode = new TextBox();
            lblStockCodeAll = new Label();
            rbnConfigureSingle = new RadioButton();
            rbnBatchConfigure = new RadioButton();
            btnDisconnect = new ATPButton();
            btnCreateStockCode = new ATPButton();
            SuspendLayout();
            // 
            // tlpCards
            // 
            tlpCards.ColumnCount = 4;
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.Location = new Point(183, 29);
            tlpCards.Name = "tlpCards";
            tlpCards.RowCount = 2;
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCards.Size = new Size(1264, 793);
            tlpCards.TabIndex = 0;
            // 
            // btnConfigureAll
            // 
            btnConfigureAll.BackColor = Color.White;
            btnConfigureAll.BorderColor = Color.Black;
            btnConfigureAll.CornerRadius = 10;
            btnConfigureAll.Cursor = Cursors.Hand;
            btnConfigureAll.FlatStyle = FlatStyle.Flat;
            btnConfigureAll.Font = new Font("Segoe UI", 10F);
            btnConfigureAll.ForeColor = Color.Black;
            btnConfigureAll.Location = new Point(19, 239);
            btnConfigureAll.Name = "btnConfigureAll";
            btnConfigureAll.Size = new Size(112, 45);
            btnConfigureAll.TabIndex = 81;
            btnConfigureAll.Text = "Configure All";
            btnConfigureAll.UseVisualStyleBackColor = false;
            btnConfigureAll.Click += btnConfigureAll_Click;
            // 
            // txtBatchStockCode
            // 
            txtBatchStockCode.Location = new Point(19, 153);
            txtBatchStockCode.Name = "txtBatchStockCode";
            txtBatchStockCode.Size = new Size(144, 23);
            txtBatchStockCode.TabIndex = 82;
            // 
            // lblStockCodeAll
            // 
            lblStockCodeAll.AutoSize = true;
            lblStockCodeAll.Location = new Point(19, 135);
            lblStockCodeAll.Name = "lblStockCodeAll";
            lblStockCodeAll.Size = new Size(100, 15);
            lblStockCodeAll.TabIndex = 83;
            lblStockCodeAll.Text = "Enter Stock Code:";
            // 
            // rbnConfigureSingle
            // 
            rbnConfigureSingle.AutoSize = true;
            rbnConfigureSingle.Location = new Point(23, 72);
            rbnConfigureSingle.Name = "rbnConfigureSingle";
            rbnConfigureSingle.Size = new Size(113, 19);
            rbnConfigureSingle.TabIndex = 84;
            rbnConfigureSingle.TabStop = true;
            rbnConfigureSingle.Text = "Single Configure";
            rbnConfigureSingle.UseVisualStyleBackColor = true;
            rbnConfigureSingle.CheckedChanged += rbnConfigureSingle_CheckedChanged;
            // 
            // rbnBatchConfigure
            // 
            rbnBatchConfigure.AutoSize = true;
            rbnBatchConfigure.Location = new Point(23, 97);
            rbnBatchConfigure.Name = "rbnBatchConfigure";
            rbnBatchConfigure.Size = new Size(111, 19);
            rbnBatchConfigure.TabIndex = 85;
            rbnBatchConfigure.TabStop = true;
            rbnBatchConfigure.Text = "Batch Configure";
            rbnBatchConfigure.UseVisualStyleBackColor = true;
            rbnBatchConfigure.CheckedChanged += rbnBatchConfigure_CheckedChanged;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.White;
            btnDisconnect.BorderColor = Color.Black;
            btnDisconnect.CornerRadius = 10;
            btnDisconnect.Cursor = Cursors.Hand;
            btnDisconnect.FlatStyle = FlatStyle.Flat;
            btnDisconnect.Font = new Font("Segoe UI", 10F);
            btnDisconnect.ForeColor = Color.Black;
            btnDisconnect.Location = new Point(19, 370);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(112, 45);
            btnDisconnect.TabIndex = 86;
            btnDisconnect.Text = "Disconnect All";
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnCreateStockCode
            // 
            btnCreateStockCode.BackColor = Color.White;
            btnCreateStockCode.BorderColor = Color.Black;
            btnCreateStockCode.CornerRadius = 10;
            btnCreateStockCode.Cursor = Cursors.Hand;
            btnCreateStockCode.FlatStyle = FlatStyle.Flat;
            btnCreateStockCode.Font = new Font("Segoe UI", 10F);
            btnCreateStockCode.ForeColor = Color.Black;
            btnCreateStockCode.Location = new Point(19, 188);
            btnCreateStockCode.Name = "btnCreateStockCode";
            btnCreateStockCode.Size = new Size(132, 45);
            btnCreateStockCode.TabIndex = 87;
            btnCreateStockCode.Text = "Create Stock Code";
            btnCreateStockCode.UseVisualStyleBackColor = false;
            btnCreateStockCode.Click += btnCreateStockCode_Click;
            // 
            // APTScanTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCreateStockCode);
            Controls.Add(btnDisconnect);
            Controls.Add(rbnBatchConfigure);
            Controls.Add(rbnConfigureSingle);
            Controls.Add(lblStockCodeAll);
            Controls.Add(txtBatchStockCode);
            Controls.Add(btnConfigureAll);
            Controls.Add(tlpCards);
            Name = "APTScanTab";
            Size = new Size(1504, 837);
            ResumeLayout(false);
            PerformLayout();
        }

        private TableLayoutPanel tlpCards;
        private ATPButton btnConfigureAll;
        private TextBox txtBatchStockCode;
        private Label lblStockCodeAll;
        private RadioButton rbnConfigureSingle;
        private RadioButton rbnBatchConfigure;
        private ATPButton btnDisconnect;
        private ATPButton btnCreateStockCode;
    }
}