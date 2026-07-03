using PGA305OWICalibration.API;
using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using System.Diagnostics;
using System.IO.Ports;

namespace PGA305OWICalibration
{
    public partial class I2COutputConfigForm : Form
    {
        private Stm32I2cController _stm32I2C = new Stm32I2cController();
        private PGA305_I2C _pga305 = null!;
        private ApiClient _api = new ApiClient();
        private string _selectedPort = "";

        private string? _serialNumber;
        private string? _sensorSerialNumber;
        private string? _pressureCode;
        private string? _selectedVoltageRange;
        private string? _selectedOutputConfig;
        private string _pressureUnit = "psi";

        public I2COutputConfigForm()
        {
            InitializeComponent();
            SetVisible(false);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            _stm32I2C.Close();
            this.Close();
        }

        private void SetVisible(bool visible)
        {
            btnConnectDevice.Visible = visible;
            btnOutputV.Visible = visible;
            btnOutputRM.Visible = visible;
            btnOutputC.Visible = visible;
            btnConfigDevice.Visible = visible;
            lblOutputMode.Visible = visible;
            lblVoltageRange.Visible = visible && _selectedOutputConfig == "voltage";
            lstVoltageRange.Visible = visible && _selectedOutputConfig == "voltage";
            lblPressureRange.Visible = visible;
            numMinPressure.Visible = visible;
            numMaxPressure.Visible = visible;
            lblSelectUnit.Visible = visible;
            btnUnitBar.Visible = visible;
            btnUnitPsi.Visible = visible;
            lblPressureError.Visible = visible;
            lblMinPressure.Visible = visible;
            lblMaxPressure.Visible = visible;
            lsbOutputConfig.Visible = visible;
            lsbAPT10MetaData.Visible = visible;
            btnNoPChange.Visible = visible;
        }

        private void btnScanHardware_Click(object sender, EventArgs e)
        {
            dgvHardware.Rows.Clear();

            foreach (string port in SerialPort.GetPortNames())
            {
                string identity = "Unknown";
                try
                {
                    if (_stm32I2C.Open(port))
                        identity = _stm32I2C.GetIdentity() ?? "No response";
                    else
                        identity = "Failed to open";
                }
                catch (Exception ex)
                {
                    identity = $"Error: {ex.Message}";
                }
                finally
                {
                    _stm32I2C.Close();
                }

                dgvHardware.Rows.Add(identity, port, "Not connected");
            }
        }

        private void dgvHardware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string port = dgvHardware.Rows[e.RowIndex].Cells[1].Value.ToString()!;
            bool isConnected = ConnectToPort(port, e.RowIndex);

            if (isConnected)
            {
                btnConnectDevice.Visible = true;
            }
        }

        private bool ConnectToPort(string port, int rowIndex)
        {
            try
            {
                bool connected = _stm32I2C.Open(port);

                if (!connected)
                {
                    dgvHardware.Rows[rowIndex].Cells[2].Value = "Failed to open";
                    return false;
                }

                string identity = _stm32I2C.GetIdentity();

                if (string.IsNullOrEmpty(identity))
                {
                    dgvHardware.Rows[rowIndex].Cells[2].Value = "No response";
                    return false;
                }

                _selectedPort = port;
                dgvHardware.Rows[rowIndex].Cells[0].Value = identity;
                dgvHardware.Rows[rowIndex].Cells[2].Value = "Connected";
                return true;
            }
            catch (Exception ex)
            {
                dgvHardware.Rows[rowIndex].Cells[2].Value = $"Error: {ex.Message}";
                return false;
            }
        }

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPort))
            {
                MessageBox.Show("Scan and connect to a device first.");
                return;
            }

            _pga305 = new PGA305_I2C(_stm32I2C);

            if (!_pga305.EnterCommandMode(channel: 0))
            {
                MessageBox.Show("Could not enter command mode. Try power cycling the board.");
                return;
            }

            _serialNumber = _pga305.ReadSerialNumber();
            _sensorSerialNumber = _pga305.ReadSensorSerialNumber();
            _pressureCode = _pga305.ReadPressureCode();

            lsbAPT10MetaData.Items.Clear();
            lsbAPT10MetaData.Items.Add($"Serial Number: {_serialNumber ?? "Read failed"}");
            lsbAPT10MetaData.Items.Add($"Sensor Serial Number: {_sensorSerialNumber ?? "Read failed"}");
            lsbAPT10MetaData.Items.Add($"Pressure Code: {_pressureCode ?? "Read failed"}");

            btnOutputV.Visible = true;
            btnOutputRM.Visible = true;
            btnOutputC.Visible = true;
            lblOutputMode.Visible = true;
            lsbAPT10MetaData.Visible = true;
            lsbOutputConfig.Visible = true;

            _selectedOutputConfig = "Voltage";
            _selectedVoltageRange = "0-10V";

            _pressureUnit = "bar";
            SetPressureControlMaximums();
            numMinPressure.Value = 0;
            numMaxPressure.Value = numMinPressure.Maximum;

            ValidatePressureRange();
            UpdateOutputConfigSummary();
        }

        private async void btnConfigDevice_Click(object sender, EventArgs e)
        {
            // These are Temporary, just for testing the writing to EEPROM
            // 
            string testSerialNumber = "2";
            string testStockCode = "TEST002";

            if (lblPressureError.Visible)
            {
                MessageBox.Show("Fix the pressure range before configuring.");
                return;
            }

            if (string.IsNullOrEmpty(_selectedVoltageRange))
            {
                lsbAPT10MetaData.Items.Add("No voltage range selected.");
                return;
            }

            lsbAPT10MetaData.Items.Add($"Configuring device for {_selectedVoltageRange}...");

            if (_selectedVoltageRange == "0-10V")
            {
                lsbAPT10MetaData.Items.Add("0-10V is native range — no coefficient change needed.");
                return;
            }

            var (vMin, vMax) = ParseVoltageRange(_selectedVoltageRange);
            double pMin = (double)numMinPressure.Value;
            double pMax = (double)numMaxPressure.Value;

            var result = await _api.ConvertOutput(
                int.Parse(testSerialNumber), vMin, vMax, pMin, pMax, _pressureUnit);

            if (result == null)
            {
                lsbAPT10MetaData.Items.Add("convert-output failed.");
                return;
            }

            lsbAPT10MetaData.Items.Add($"Got coefficients for session {result.session_id}, serial {result.serial_number}");
            Debug.WriteLine($"Got coefficients for session {result.session_id}, serial {result.serial_number}");
            foreach (var kv in result.coefficients)
                lsbAPT10MetaData.Items.Add($"  {kv.Key} = 0x{kv.Value}");

            lsbAPT10MetaData.Items.Add("EEPROM write skipped — pending hardware fix.");

            bool transducerWritten = await _api.CreateTransducer(
                testStockCode, result.serial_number, _selectedVoltageRange,
                _pressureCode ?? "100G", _selectedOutputConfig ?? "voltage");

            lsbAPT10MetaData.Items.Add(transducerWritten
                ? "transducer written to database."
                : "transducer write FAILED.");

            if (!transducerWritten)
            {
                lsbAPT10MetaData.Items.Add("Skipping final-coefficients write since transducer failed.");
                return;
            }

            bool dbWritten = await _api.CreateFinalCoefficients(
                result.session_id, result.serial_number, testStockCode,
                result.coefficients, result.padc_gain, result.tadc_gain,
                result.padc_offset, result.tadc_offset);

            lsbAPT10MetaData.Items.Add(dbWritten
                ? "final-coefficients written to database."
                : "final-coefficients write FAILED.");
        }

        private void btnOutputV_Click(object sender, EventArgs e)
        {
            _selectedOutputConfig = "Voltage";
            lblVoltageRange.Visible = true;
            lstVoltageRange.Visible = true;
            UpdateOutputConfigSummary();
        }

        private void btnOutputRM_Click(object sender, EventArgs e)
        {
            _selectedOutputConfig = "ratiometric";
            lblVoltageRange.Visible = false;
            lstVoltageRange.Visible = false;
            UpdateOutputConfigSummary();
        }

        private void btnOutputC_Click(object sender, EventArgs e)
        {
            _selectedOutputConfig = "current";
            lblVoltageRange.Visible = false;
            lstVoltageRange.Visible = false;
            UpdateOutputConfigSummary();
        }

        private (double vMin, double vMax) ParseVoltageRange(string range)
        {
            string trimmed = range.TrimEnd('V');
            var parts = trimmed.Split('-');
            return (double.Parse(parts[0]), double.Parse(parts[1]));
        }

        private void lstVoltageRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVoltageRange.SelectedItem != null)
            {
                _selectedVoltageRange = lstVoltageRange.SelectedItem.ToString();
                UpdateOutputConfigSummary();
                lblPressureRange.Visible = true;
                lblSelectUnit.Visible = true;
                btnUnitBar.Visible = true;
                btnUnitPsi.Visible = true;
                btnNoPChange.Visible = true;
            }
        }

        private void btnNoPChange_Click(object sender, EventArgs e)
        {
            lsbOutputConfig.Items.Add("Pressure range: no change");
            btnConfigDevice.Visible = true;
        }

        private void numMaxPressure_ValueChanged(object sender, EventArgs e)
        {
            if (numMaxPressure.Value > numMaxPressure.Maximum)
                numMaxPressure.Value = numMaxPressure.Maximum;

            ValidatePressureRange();
            UpdateOutputConfigSummary();
        }

        private void numMinPressure_ValueChanged(object sender, EventArgs e)
        {
            if (numMinPressure.Value > numMinPressure.Maximum)
                numMinPressure.Value = numMinPressure.Maximum;

            ValidatePressureRange();
            UpdateOutputConfigSummary();
        }

        private void SetPressureControlMaximums()
        {
            double? maxAllowed = GetMaxPressureInSelectedUnit();
            if (maxAllowed == null) return;

            numMinPressure.Maximum = (decimal)maxAllowed;
            numMaxPressure.Maximum = (decimal)maxAllowed;
        }

        private void btnUnitBar_Click(object sender, EventArgs e)
        {
            _pressureUnit = "bar";
            SetPressureControlMaximums();
            ValidatePressureRange();
            UpdateOutputConfigSummary();
            lblMinPressure.Visible = true;
            lblMaxPressure.Visible = true;
            numMinPressure.Visible = true;
            numMaxPressure.Visible = true;
            btnConfigDevice.Visible = true;
        }

        private void btnUnitPsi_Click(object sender, EventArgs e)
        {
            _pressureUnit = "psi";
            SetPressureControlMaximums();
            ValidatePressureRange();
            UpdateOutputConfigSummary();
            lblMinPressure.Visible = true;
            lblMaxPressure.Visible = true;
            numMinPressure.Visible = true;
            numMaxPressure.Visible = true;
            btnConfigDevice.Visible = true;
        }

        private double? GetSensorMaxBar()
        {
            if (string.IsNullOrEmpty(_pressureCode)) return null;
            string digits = new string(_pressureCode.TakeWhile(char.IsDigit).ToArray());
            return double.TryParse(digits, out double bar) ? bar : null;
        }

        // TODO: this rounds the bar->PSI conversion to the nearest 50 for a clean customer-facing number
        // (e.g. 100 bar = 1450.4 PSI -> displayed/enforced as 1500 PSI).
        // This will need to be fixed later based on pressure code or something
        // Maybe all the sensors should be calibrated in bar? then converted, but there is conversion errors I have to consider
        private double? GetMaxPressureInSelectedUnit()
        {
            double? maxBar = GetSensorMaxBar();
            if (maxBar == null) return null;

            if (_pressureUnit == "bar")
                return maxBar;

            double maxPsi = maxBar.Value * 14.5038;
            return Math.Round(maxPsi / 50) * 50;
        }

        private void ValidatePressureRange()
        {
            double? maxAllowed = GetMaxPressureInSelectedUnit();

            if (maxAllowed == null)
            {
                lblPressureError.Visible = false;
                return;
            }

            bool overLimit = numMaxPressure.Value > (decimal)maxAllowed || numMinPressure.Value > (decimal)maxAllowed;

            if (overLimit)
            {
                lblPressureError.Text = $"Pressure entered too high — max is {maxAllowed} {_pressureUnit.ToUpper()}.";
                lblPressureError.ForeColor = Color.Red;
                lblPressureError.Visible = true;
            }
        }

        private void UpdateOutputConfigSummary()
        {
            lsbOutputConfig.Items.Clear();
            lsbOutputConfig.Items.Add($"Output mode: {_selectedOutputConfig ?? "Not selected"}");

            if (_selectedOutputConfig == "Voltage")
                lsbOutputConfig.Items.Add($"Voltage range: {_selectedVoltageRange ?? "Not selected"}");

            lsbOutputConfig.Items.Add($"Pressure range: {numMinPressure.Value} - {numMaxPressure.Value} {_pressureUnit.ToUpper()}");
        }
        private void lsbAPT10MetaData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lsbOutputConfig_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblPressureError_Click(object sender, EventArgs e)
        {

        }
    }
}