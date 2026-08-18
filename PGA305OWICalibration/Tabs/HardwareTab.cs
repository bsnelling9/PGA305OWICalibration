using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using System.Diagnostics;
using System.IO.Ports;


// To DO
// Remove all voltage selection from this tab
// This is for the hardware only
// The output should be moved the the ATP TAB

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
                bool ok = _stm32.ConfigureVoltageComparators(
                    vcompa0High: chkVCOMPA0.Checked,
                    vcompa1High: chkVCOMPA1.Checked);

            }
            catch (Exception ex)
            {

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

        private void BtnConnectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvHardware.Rows)
            {
                string port = row.Cells[1].Value?.ToString() ?? "";

                if (!port.StartsWith("COM")) continue;

                _stm32.Close();
                bool connected = _stm32.Open(port);
                if (!connected)
                {
                    row.Cells[2].Value = "Failed to Open";
                    continue;
                }

                string identity = _stm32.GetIdentity();
                if (identity.Contains("PGA305"))
                {
                    row.Cells[0].Value = identity;
                    row.Cells[2].Value = "Connected";
                    break;
                }
                else
                {
                    _stm32.Close();
                    row.Cells[0].Value = identity.Length > 0 ? identity : "Unknown";
                    row.Cells[2].Value = "Not STM32";
                }
            }

            try
            {
                bool opened = _u2a.Open("");
             

                foreach (DataGridViewRow row in dgvHardware.Rows)
                {
                    if (row.Cells[0].Value?.ToString() == "USB2ANY")
                    {
                        row.Cells[0].Value = opened ? $"USB2ANY ({_u2a.GetSerialNumber(0)})" : "USB2ANY";
                        row.Cells[2].Value = opened ? "Connected" : "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }


        private void btnInitHW_Click(object sender, EventArgs e)
        {
            bool initOk = _pga305.Initialize();
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