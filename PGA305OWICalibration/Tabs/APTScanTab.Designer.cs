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
            modeButton = new STM32ModeButton();
            SuspendLayout();

            // modeButton
            modeButton.Name = "modeButton";
            modeButton.Location = new System.Drawing.Point(20, 20);
            modeButton.TabStop = false;

            Controls.Add(modeButton);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Name = "APTScanTab";
            Size = new System.Drawing.Size(1725, 1112);

            ResumeLayout(false);
        }

        private STM32ModeButton modeButton;
    }
}