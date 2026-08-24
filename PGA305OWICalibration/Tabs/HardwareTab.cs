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

        // This will need to go into the appsettingsJSON or the hardware.
        private const string Stm32Port = "COM15";

        private const int RowStm32 = 0;
        private const int RowUsb2Any = 1;

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

            ConnectStm32();
            ConnectUsb2Any();
        }       

        private void ConnectStm32()
        {
            _stm32.Close();

            if (!_stm32.Open(Stm32Port))
            {
                SetRow(RowStm32, "STM32 Multiplexer", Stm32Port, "Failed to open");
                return;
            }

            string identity = _stm32.GetIdentity();

            if (!identity.Contains("PGA305"))
            {
                _stm32.Close();
                SetRow(RowStm32, "STM32 Multiplexer", Stm32Port, "Wrong device");
                return;
            }

            SetRow(RowStm32, identity, Stm32Port, "Connected");
        }

        private void ConnectUsb2Any()
        {
            try
            {
                if (_u2a.FindControllers() == 0)
                {
                    SetRow(RowUsb2Any, "USB2ANY", "-", "Not found");
                    return;
                }

                string serial = _u2a.GetSerialNumber(0);

                if (!_u2a.Open(""))
                {
                    SetRow(RowUsb2Any, "USB2ANY", serial, "Failed to open");
                    return;
                }

                _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);

                bool linkOk = _pga305.Initialize();

                SetRow(RowUsb2Any, "USB2ANY", serial, linkOk ? "Connected" : "Failed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"USB2ANY error: {ex.Message}");
                SetRow(RowUsb2Any, "USB2ANY", "-", "Error");
            }
        }

        private void SetRow(int index, string device, string id, string status)
        {
            dgvHardware.Rows[index].Cells[0].Value = device;
            dgvHardware.Rows[index].Cells[1].Value = id;
            dgvHardware.Rows[index].Cells[2].Value = status;
        }         
    }
}