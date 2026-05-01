using System.Windows.Forms;
using System.Drawing;
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
            modeButton.Location = new Point(20, 20);
            modeButton.Name = "modeButton";

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "APTScanTab";
            Size = new Size(1043, 746);

            Controls.Add(modeButton);

            ResumeLayout(false);
        }

        private STM32ModeButton modeButton;
    }
}