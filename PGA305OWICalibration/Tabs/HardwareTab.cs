using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using System.Diagnostics;
using System.IO.Ports;

namespace PGA305OWICalibration.Tabs
{
    public partial class HardwareTab : UserControl
    {
        private STM32Controller _stm32;
        private USB2AnyDevice _u2a;
        private PGA305Device _pga305;

        public event EventHandler? HardwareReady;
        private const int RowStm32 = 0;
        private const int RowUsb2Any = 1;
        private int channel = 0;

        public HardwareTab(STM32Controller stm32, USB2AnyDevice u2a, PGA305Device pga305)
        {
            InitializeComponent();
            _stm32 = stm32;
            _u2a = u2a;
            _pga305 = pga305;
        }

        private void BtnScanHardware_Click(object sender, EventArgs e)
        {
            dgvHardware.Rows.Clear();

            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    dgvHardware.Rows.Add(port, port, "Found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"COM scan error: {ex.Message}");
            }

            try
            {
                int numFound = _u2a.FindControllers();

                if (numFound > 0)
                {
                    string serial = _u2a.GetSerialNumber(0);
                    dgvHardware.Rows.Add("USB2ANY", serial, "Found");
                }
                else
                {
                    dgvHardware.Rows.Add("USB2ANY", "-", "Not Found");
                }
            }
            catch (Exception ex)
            {
                dgvHardware.Rows.Add("USB2ANY", "-", $"Error: {ex.Message}");
            }
        }

        private void BtnConnectAll_Click(object sender, EventArgs e)
        {
            dgvHardware.Rows.Clear();
            dgvHardware.Rows.Add("STM32 Multiplexer", "-", "Connecting...");
            dgvHardware.Rows.Add("USB2ANY", "-", "Connecting...");

            bool stm32Ok = ConnectStm32();
            bool usbOk = ConnectUsb2Any();

            /*if (stm32Ok && usbOk)
                HardwareReady?.Invoke(this, EventArgs.Empty);*/
        }

        private bool ConnectStm32()
        {
            try
            {
                _stm32.Close();

                if (!_stm32.Open(AppConfig.STM32Port))
                {
                    SetRow(RowStm32, "STM32 Multiplexer", AppConfig.STM32Port, "Failed to open");
                    return false;
                }

                string identity = _stm32.GetIdentity();
                Debug.WriteLine($"STM32 IDN: '{identity}'");

                if (!identity.Contains(AppConfig.DEVICE_IDENTITY))
                {
                    _stm32.Close();
                    SetRow(RowStm32, "STM32 Multiplexer", AppConfig.STM32Port, "Wrong device");
                    return false;
                }

                SetRow(RowStm32, identity, AppConfig.STM32Port, "Connected");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"STM32 error: {ex.Message}");
                SetRow(RowStm32, "STM32 Multiplexer", AppConfig.STM32Port, "Error");
                return false;
            }
        }

        private bool ConnectUsb2Any()
        {
            try
            {
                if (_u2a.FindControllers() == 0)
                {
                    SetRow(RowUsb2Any, "USB2ANY", "-", "Not found");
                    return false;
                }

                string serial = _u2a.GetSerialNumber(0);

                if (!_u2a.Open(""))
                {
                    SetRow(RowUsb2Any, "USB2ANY", serial, "Failed to open");
                    return false;
                }

                _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);

                bool linkOk = _pga305.Initialize();

                SetRow(RowUsb2Any, "USB2ANY", serial, linkOk ? "Connected" : "Failed");
                return linkOk;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"USB2ANY error: {ex.Message}");
                SetRow(RowUsb2Any, "USB2ANY", "-", "Error");
                return false;
            }
        }

        private void SetRow(int index, string device, string id, string status)
        {
            dgvHardware.Rows[index].Cells[0].Value = device;
            dgvHardware.Rows[index].Cells[1].Value = id;
            dgvHardware.Rows[index].Cells[2].Value = status;
        }

        // Below is all for testing will remove at when going into production
        // Only remove these last minute, this is the third time I have added them abck
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            channel = (int)numericUpDown1.Value;
        }

        private void btnSetChannel_Click(object sender, EventArgs e)
        {
            _stm32.SelectChannel((int)numericUpDown1.Value);
        }

        private void btnCompA_Click(object sender, EventArgs e)
        {
            _stm32.SendCommand("cfg52");
        }

        private void btnCompV_Click(object sender, EventArgs e)
        {
            _stm32.SendCommand("cfg51");
        }

        private void btnCompR_Click(object sender, EventArgs e)
        {
            _stm32.SendCommand("cfg53");
        }
        private void btnSETOWI_Click(object sender, EventArgs e)
        {
            _stm32.SendCommand("cfg40");
        }

        private void btnSETMA_Click(object sender, EventArgs e)
        {
            _stm32.SendCommand("cfg20");
        }

        private void btnSETVO_Click(object sender, EventArgs e)
        {
            _stm32.SendCommand("cfg10");
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _pga305.Initialize();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (!_pga305.Activate())
            {
                Debug.WriteLine($"Channel {channel}: PGA305 activate failed");
            }

            var serialNumber = _pga305.ReadInternalSerialNumber();
            Debug.WriteLine($"Serial Number {serialNumber}");
        }

        private void btnGPIOTXLow_Click(object sender, EventArgs e)
        {
            _u2a.GPIO_SetPort(USB2AnyConfig.GPIO11, USB2AnyConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO11, USB2AnyConfig.STATE_LOW);
        }

        private void btnGPIOTXHigh_Click(object sender, EventArgs e)
        {
            _u2a.GPIO_SetPort(USB2AnyConfig.GPIO11, USB2AnyConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO11, USB2AnyConfig.STATE_HIGH);
        }

        private void btnActivateLow_Click(object sender, EventArgs e)
        {
            _u2a.GPIO_SetPort(USB2AnyConfig.GPIO7, USB2AnyConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO7, USB2AnyConfig.STATE_LOW);
        }

        private void btnActivatehigh_Click(object sender, EventArgs e)
        {
            _u2a.GPIO_SetPort(USB2AnyConfig.GPIO7, USB2AnyConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO7, USB2AnyConfig.STATE_HIGH);
        }

        private void btnOWI_TXLow_Click(object sender, EventArgs e)
        {
            _u2a.GPIO_SetPort(USB2AnyConfig.GPIO4, USB2AnyConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO4, USB2AnyConfig.STATE_LOW);
        }

        private void btnOWITXHigh_Click(object sender, EventArgs e)
        {
            _u2a.GPIO_SetPort(USB2AnyConfig.GPIO4, USB2AnyConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO4, USB2AnyConfig.STATE_HIGH);
        }
    }
}