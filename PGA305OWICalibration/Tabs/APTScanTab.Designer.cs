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
            btnConfigureAll.BorderSize = 2;
            btnConfigureAll.CornerRadius = 10;
            btnConfigureAll.Cursor = Cursors.Hand;
            btnConfigureAll.FlatStyle = FlatStyle.Flat;
            btnConfigureAll.Font = new Font("Segoe UI", 10F);
            btnConfigureAll.ForeColor = Color.Black;
            btnConfigureAll.Location = new Point(19, 164);
            btnConfigureAll.Name = "btnConfigureAll";
            btnConfigureAll.Size = new Size(112, 45);
            btnConfigureAll.TabIndex = 81;
            btnConfigureAll.Text = "Configure All";
            btnConfigureAll.UseVisualStyleBackColor = false;
            btnConfigureAll.Click += btnConfigureAll_Click;
            // 
            // txtBatchStockCode
            // 
            txtBatchStockCode.Location = new Point(19, 135);
            txtBatchStockCode.Name = "txtBatchStockCode";
            txtBatchStockCode.Size = new Size(144, 23);
            txtBatchStockCode.TabIndex = 82;
            // 
            // lblStockCodeAll
            // 
            lblStockCodeAll.AutoSize = true;
            lblStockCodeAll.Location = new Point(19, 117);
            lblStockCodeAll.Name = "lblStockCodeAll";
            lblStockCodeAll.Size = new Size(100, 15);
            lblStockCodeAll.TabIndex = 83;
            lblStockCodeAll.Text = "Enter Stock Code:";
            // 
            // rbnConfigureSingle
            // 
            rbnConfigureSingle.AutoSize = true;
            rbnConfigureSingle.Location = new Point(23, 54);
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
            rbnBatchConfigure.Location = new Point(23, 79);
            rbnBatchConfigure.Name = "rbnBatchConfigure";
            rbnBatchConfigure.Size = new Size(111, 19);
            rbnBatchConfigure.TabIndex = 85;
            rbnBatchConfigure.TabStop = true;
            rbnBatchConfigure.Text = "Batch Configure";
            rbnBatchConfigure.UseVisualStyleBackColor = true;
            rbnBatchConfigure.CheckedChanged += rbnBatchConfigure_CheckedChanged;
            // 
            // APTScanTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
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
    }
}