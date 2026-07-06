using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using PGA305OWICalibration.PGA305EVM;
using PGA305OWICalibration.Tabs;

namespace PGA305OWICalibration
{
    public partial class Form1 : Form
    {
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private STM32Controller _stm32 = new STM32Controller();
        private PGA305Device _pga305 = null!;

        public Form1()
        {
            InitializeComponent();

            _pga305 = new PGA305Device(_u2a);

            HardwareTab hardwareTab = new HardwareTab(_stm32, _u2a);
            hardwareTab.Dock = DockStyle.Fill;
            this.hardwareTab.Controls.Add(hardwareTab);

            APTScanTab aptScanTab = new APTScanTab(_stm32, _pga305);
            aptScanTab.Dock = DockStyle.Fill;
            this.deviceTab.Controls.Add(aptScanTab);
        }


        private void btnDebug_Click(object sender, EventArgs e)
        {
            Form2 debugForm = new Form2();
            debugForm.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void btnConfigI2C_Click(object sender, EventArgs e)
        {
            I2COutputConfigForm outputForm = new I2COutputConfigForm();
            outputForm.Show();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (_u2a.GetHandle() > 0)
                {
                    _u2a.GPIO_WritePort(PGA305Owi.GPIO7, PGA305Owi.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Owi.GPIO10, PGA305Owi.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Owi.GPIO11, PGA305Owi.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Device.GPIO7, PGA305Device.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Device.GPIO10, PGA305Device.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Device.GPIO11, PGA305Device.STATE_LOW);
                    _u2a.OneWire_SetOutput(0);
                    _u2a.OneWire_SetMode(0);
                    Thread.Sleep(10);
                }
            }
            catch { }

            _stm32.Close();
            _u2a.Close();
            base.OnFormClosing(e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = hardwareTab;
        }
    }
}