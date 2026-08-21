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
            txtStockCodeAll = new TextBox();
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
            btnConfigureAll.Location = new Point(19, 92);
            btnConfigureAll.Name = "btnConfigureAll";
            btnConfigureAll.Size = new Size(112, 45);
            btnConfigureAll.TabIndex = 81;
            btnConfigureAll.Text = "Configure All";
            btnConfigureAll.UseVisualStyleBackColor = false;
            // 
            // txtStockCodeAll
            // 
            txtStockCodeAll.Location = new Point(20, 43);
            txtStockCodeAll.Name = "txtStockCodeAll";
            txtStockCodeAll.Size = new Size(144, 23);
            txtStockCodeAll.TabIndex = 82;
            // 
            // APTScanTab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtStockCodeAll);
            Controls.Add(btnConfigureAll);
            Controls.Add(tlpCards);
            Name = "APTScanTab";
            Size = new Size(1450, 837);
            ResumeLayout(false);
            PerformLayout();
        }

        private TableLayoutPanel tlpCards;
        private ATPButton btnConfigureAll;
        private TextBox txtStockCodeAll;
    }
}