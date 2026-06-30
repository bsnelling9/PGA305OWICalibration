using PGA305OWICalibration.API;
using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305EVM;
using System.Diagnostics;

namespace PGA305OWICalibration
{
    public partial class Form2 : Form
    {
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private PGA305Owi _pga305OWI = null!;
        private ApiClient _api = new ApiClient();

        //I2C variables for the Digipot used for OWI signal conditioning on the EVM.
        private const ushort DIGIPOT_ADDR = 0x2D;
        private const byte DIGIPOT_REG = 0x00;
        private const byte DIGIPOT_VALUE = 0x19;

        private string? _sensorSerialNumber;
        private string? _pressureCode;
        private string? _selectedVoltageRange;
        private string? _selectedOutputConfig;

        public Form2()
        {
            InitializeComponent();
        }

        private void btnExitDebug_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void u2a_Click(object sender, EventArgs e)
        {
            try
            {
                _u2a.EnableDebugLogging();

                int numFound = _u2a.FindControllers();
                listBoxDebug.Items.Add($"USB2ANY devices found: {numFound}");

                if (numFound == 0)
                {
                    listBoxDebug.Items.Add("No USB2ANY detected.");
                    return;
                }

                string serial = _u2a.GetSerialNumber(0);
                listBoxDebug.Items.Add($"USB2ANY serial: {serial}");

                bool opened = _u2a.Open("");
                if (!opened)
                {
                    listBoxDebug.Items.Add("Failed to open USB2ANY.");
                    return;
                }

                int powerResult = _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);
                listBoxDebug.Items.Add($"Power result: {powerResult}");

                listBoxDebug.Items.Add($"USB2ANY opened. Handle = {_u2a.GetHandle()}");
                _pga305OWI = new PGA305Owi(_u2a);
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnHandlePOT_Click(object sender, EventArgs e)
        {
            int i2cResult = _u2a.I2C_Control(0, 0, 1);
            Debug.WriteLine($"I2C_Control result: {i2cResult}");

            int writeResult = _u2a.I2C_RegisterWrite(DIGIPOT_ADDR, DIGIPOT_REG, DIGIPOT_VALUE);
            Debug.WriteLine($"DigiPot write result: {writeResult}");

            int result = _u2a.I2C_RegisterWrite(0x57, 0x00, 0x00);
            Debug.WriteLine($"TPL0102 Pot0 write result: {result}");

            result = _u2a.I2C_RegisterWrite(0x57, 0x01, 0x00);
            Debug.WriteLine($"TPL0102 Pot1 write result: {result}");
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (_pga305OWI == null)
            {
                listBoxDebug.Items.Add("Please click U2A first.");
                return;
            }

            bool initOk = _pga305OWI.Initialize();
            listBoxDebug.Items.Add($"PGA305 init: {(initOk ? "OK" : "FAILED")}");
            if (initOk)
                listBoxDebug.Items.Add("Ready — click Activate.");
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            _pga305OWI.Activate();
        }

        private void btnReadDevice_Click(object sender, EventArgs e)
        {
            _pressureCode = _pga305OWI.ReadPressureCode();
            _sensorSerialNumber = _pga305OWI.ReadSerialNumber();
            string internalSerialNumber = _pga305OWI.ReadInternalSerialNumber();

            listBoxDebug.Items.Add($"Pressure code: {_pressureCode}");
            listBoxDebug.Items.Add($"Serial number: {_sensorSerialNumber}");
            listBoxDebug.Items.Add($"Internal serial number: {internalSerialNumber}");
        }

        private void BtnOutputV_Click(object sender, EventArgs e)
        {
            _selectedOutputConfig = "voltage";
            lblVoltageRange.Visible = true;
            lstVoltageRange.Visible = true;
        }

        private void BtnOutputRM_Click(object sender, EventArgs e)
        {
            _selectedOutputConfig = "ratiometric";
        }

        private void BtnOutputC_Click(object sender, EventArgs e)
        {
            _selectedOutputConfig = "current";
        }

        private void lstVoltageRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVoltageRange.SelectedItem != null)
            {
                _selectedVoltageRange = lstVoltageRange.SelectedItem.ToString();
                Debug.WriteLine($"Voltage range selected: {_selectedVoltageRange}");
            }
        }

        private (double vMin, double vMax) ParseVoltageRange(string range)
        {
            string trimmed = range.TrimEnd('V');
            var parts = trimmed.Split('-');
            return (double.Parse(parts[0]), double.Parse(parts[1]));
        }

        private async void btnConfigure_Click(object sender, EventArgs e)
        {
            // These are Temporary, just for testing the writing to EEPROM           
            _sensorSerialNumber ??= "1"; 
            string testStockCode = "TEST001";

            if (string.IsNullOrEmpty(_sensorSerialNumber))
            {
                listBoxDebug.Items.Add("No serial number — click Read Device first.");
                return;
            }

            if (string.IsNullOrEmpty(_selectedVoltageRange))
            {
                listBoxDebug.Items.Add("No voltage range selected.");
                return;
            }

            listBoxDebug.Items.Add($"Configuring device for {_selectedVoltageRange}...");

            if (_selectedVoltageRange == "0-10V")
            {
                listBoxDebug.Items.Add("0-10V is native range — no coefficient change needed.");
                return;
            }

            var (vMin, vMax) = ParseVoltageRange(_selectedVoltageRange);

            var result = await _api.ConvertOutput(int.Parse(_sensorSerialNumber), vMin, vMax);
            if (result == null)
            {
                listBoxDebug.Items.Add("convert-output failed.");
                return;
            }

            listBoxDebug.Items.Add($"Got coefficients for session {result.session_id}");
            foreach (var kv in result.coefficients)
                listBoxDebug.Items.Add($"  {kv.Key} = 0x{kv.Value}");

            listBoxDebug.Items.Add("EEPROM write skipped — pending hardware fix.");

            bool transducerWritten = await _api.CreateTransducer(
                testStockCode, result.serial_number, _selectedVoltageRange,
                _pressureCode ?? "100G", _selectedOutputConfig ?? "voltage");

            listBoxDebug.Items.Add(transducerWritten
                ? "transducer written to database."
                : "transducer write FAILED.");

            if (!transducerWritten)
            {
                listBoxDebug.Items.Add("Skipping final-coefficients write since transducer failed.");
                return;
            }

            bool dbWritten = await _api.CreateFinalCoefficients(
                result.session_id, result.serial_number, testStockCode,
                result.coefficients, result.padc_gain, result.tadc_gain,
                result.padc_offset, result.tadc_offset);

            listBoxDebug.Items.Add(dbWritten
                ? "final-coefficients written to database."
                : "final-coefficients write FAILED.");
        }

        //Remove this for final production
        private void btnTestWriteH_Click(object sender, EventArgs e)
        {
            if (_pga305OWI == null)
            {
                listBoxDebug.Items.Add("Please click U2A first.");
                return;
            }

            bool ok = _pga305OWI.TestWriteHCoefficients(0x01);
            listBoxDebug.Items.Add(ok ? "H coefficient test write succeeded." : "H coefficient test write FAILED.");

            foreach (string key in new[] { "h0", "h1", "h2", "h3" })
            {
                byte[] addrs = AppConfig.COEFFICIENT_ADDRESSES[key];
                int lsb = _pga305OWI.ReadRegister(addrs[0]);
                int mid = _pga305OWI.ReadRegister(addrs[1]);
                int msb = _pga305OWI.ReadRegister(addrs[2]);
                listBoxDebug.Items.Add($"{key}: 0x{msb:X2}{mid:X2}{lsb:X2}");
            }
        }
    }
}