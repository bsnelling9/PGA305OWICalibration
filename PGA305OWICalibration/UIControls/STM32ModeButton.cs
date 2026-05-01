using System.Drawing;
using System.Windows.Forms;

namespace PGA305OWICalibration.UIControls
{
    public enum STM32Mode
    {
        NotConnected,
        OWI,
        Measure
    }

    public class STM32ModeButton : ATPButton
    {
        public STM32ModeButton()
        {
            Width = 160;
            Height = 45;
            Enabled = false;
            Cursor = Cursors.Default;
            SetMode(STM32Mode.NotConnected);
        }

        public void SetMode(STM32Mode mode)
        {
            switch (mode)
            {
                case STM32Mode.NotConnected:
                    Text = "Not Connected";
                    BorderColor = Color.Red;
                    break;
                case STM32Mode.OWI:
                    Text = "OWI Active";
                    BorderColor = Color.Green;
                    break;
                case STM32Mode.Measure:
                    Text = "Measure Active";
                    BorderColor = Color.RoyalBlue;
                    break;
            }
            Invalidate();
        }
    }
}