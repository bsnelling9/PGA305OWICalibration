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

        private int? _serialNumber;
        private string? _sensorSerialNumber;
        private string? _pressureCode;

        public HardwareTab(STM32Controller stm32, USB2AnyDevice u2a, PGA305Device pga305)
        {
            InitializeComponent();
            _stm32 = stm32;
            _u2a = u2a;
            _pga305 = pga305;
        }

        private void BtnSetCompensation_Click(object sender, EventArgs e)
        {
            try
            {
                int powerResult = _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);
                Debug.WriteLine($"Power result: {powerResult}");

                bool ok = _stm32.ConfigureVoltageComparators(
                    vcompa0High: chkVCOMPA0.Checked,
                    vcompa1High: chkVCOMPA1.Checked);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Set Compensation error: {ex.Message}");
            }
        }

        private void BtnSetRelay_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = _stm32.ConfigureRelays(
                    owiRelayClosed: rdoOWI.Checked,
                    maRelayClosed: rdoMA.Checked,
                    voRelayClosed: rdoOWI.Checked || rdoVO.Checked);

                string mode = rdoOWI.Checked ? "OWI" : rdoVO.Checked ? "VO" : "MA";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Set Relay error: {ex.Message}");
            }
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

        private const int RowStm32 = 0;
        private const int RowUsb2Any = 1;

        private void BtnConnectAll_Click(object sender, EventArgs e)
        {
            dgvHardware.Rows.Clear();
            dgvHardware.Rows.Add("STM32 Multiplexer", "-", "Connecting...");
            dgvHardware.Rows.Add("USB2ANY", "-", "Connecting...");

            ConnectStm32();
            ConnectUsb2Any();
        }

        private const string Stm32Port = "COM15";

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

        private void btnInitHW_Click(object sender, EventArgs e)
        {
            bool initOk = _pga305.Initialize();
        }

        private void btnPinHigh_Click(object sender, EventArgs e)
        {
            bool ok = _stm32.SelectChannel(0);
            if (!ok)
            {
                Debug.WriteLine("Failed to select channel 0");
            }

            _pga305.ActivatePinHigh();
        }

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = _stm32.SelectChannel(0);
                if (!ok)
                {
                    Debug.WriteLine("Failed to select channel 0");
                }

                bool activate = _pga305.Activate();

                if (!activate)
                {
                    Debug.WriteLine("Device failed to activate.");
                    return;
                }

                _serialNumber = _pga305.ReadInternalSerialNumber();
                _pressureCode = _pga305.ReadPressureCode();
                _sensorSerialNumber = _pga305.ReadSerialNumber();


                if (!_serialNumber.HasValue)
                {
                    Debug.WriteLine("Failed to read internal serial number.");
                    return;
                }

                Debug.WriteLine($"Pressure code: {_pressureCode}");
                Debug.WriteLine($"Serial number: {_sensorSerialNumber}");
                Debug.WriteLine($"Internal serial number: {_serialNumber}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}