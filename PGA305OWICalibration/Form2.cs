using PGA305OWICalibration.API;
using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using PGA305OWICalibration.PGA305EVM;
using PGA305OWICalibration.UIControls;
using PGA305OWICalibration.Models;
using System.Diagnostics;
using System.Globalization;

namespace PGA305OWICalibration
{
    public partial class Form2 : Form
    {
        private enum ConfigSource { None, StockCode, Manual }
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private PGA305Owi _pga305OWI = null!;
        private ApiClient _api = new ApiClient();
        private PGAOutputConfig _outputconfig = new PGAOutputConfig();
        private readonly StockCodes _stockCodes = new StockCodes();

        private ATPButton? _selectedOutputMode;
        private ConfigSource _source = ConfigSource.None;
        private string? _sensorSerialNumber;
        private bool _deviceConnected;
        private bool _hardwareReady;

        public Form2()
        {
            InitializeComponent();

            lblMinPressure.Visible = false;
            lblMaxPressure.Visible = false;
            numMinPressure.Visible = false;
            numMaxPressure.Visible = false;
            btnConfigDevice.Enabled = false;

            SetDisconnected();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { ParkOwiLines(); } catch (Exception ex) { Debug.WriteLine(ex.Message); }
            base.OnFormClosing(e);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void UpdateVisibility()
        {
            bool ready = _source != ConfigSource.None;
            bool manual = _source == ConfigSource.Manual;
            bool manualVoltage = manual && _selectedOutputMode == btnVoltagePOT;

            // lblStockCode.Visible = _hardwareReady;
            txtStockCode.Visible = _hardwareReady;
            //btnLoadStockCode.Visible = _hardwareReady;

            btnVoltagePOT.Visible = _hardwareReady;
            btnRatioPOT.Visible = _hardwareReady;
            btnCurrentPOT.Visible = _hardwareReady;
            btnNextDevice.Visible = _hardwareReady;

            lblStep2.Visible = ready;
            btnConnectDevice.Visible = ready;
            lblJobCode.Visible = ready;
            txtJobCode.Visible = ready;

            gbxConfigOutput.Visible = _deviceConnected && manualVoltage;
            gbxConfigPressure.Visible = _deviceConnected && manual;
            gbxConfigDevice.Visible = _deviceConnected;
        }

        private void ShowActiveOutputMode()
        {
            SetOutputModeState(btnRatioPOT, _selectedOutputMode == btnRatioPOT);
            SetOutputModeState(btnVoltagePOT, _selectedOutputMode == btnVoltagePOT);
            SetOutputModeState(btnCurrentPOT, _selectedOutputMode == btnCurrentPOT);
        }

        private void SetOutputModeState(ATPButton btn, bool active)
        {
            btn.Enabled = active;
            btn.BorderColor = active ? Color.Green : Color.Black;
        }

        private void ResetOutputModes()
        {
            btnRatioPOT.Enabled = true;
            btnVoltagePOT.Enabled = true;
            btnCurrentPOT.Enabled = true;
            btnRatioPOT.BorderColor = Color.Black;
            btnVoltagePOT.BorderColor = Color.Black;
            btnCurrentPOT.BorderColor = Color.Black;
        }

        private void SetDisconnected()
        {
            _deviceConnected = false;
            UpdateVisibility();
            ResetOutputModes();
        }

        private void btnInitHW_Click(object sender, EventArgs e)
        {
            listBoxDebug.Items.Clear();
            int status = listBoxDebug.Items.Add("Connecting to hardware...");
            listBoxDebug.Refresh();

            try
            {
                listBoxDebug.Items.Clear();
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
                listBoxDebug.Items.Add($"USB2ANY opened. Handle = {_u2a.GetHandle()}");

                _pga305OWI = new PGA305Owi(_u2a);
                ParkOwiLines();

                bool initOk = _pga305OWI.Initialize();

                if (initOk)
                {
                    _hardwareReady = true;
                    listBoxDebug.Items.Add("Enter a stock code, or select the output type.");
                    UpdateVisibility();
                }
                else
                {
                    listBoxDebug.Items.Add("PGA305 init FAILED.");
                }
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
            }
            finally
            {
                listBoxDebug.Items.RemoveAt(status);
            }
        }

        private void ParkOwiLines()
        {
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO11, USB2AnyConfig.STATE_LOW);
            _u2a.GPIO_WritePort(USB2AnyConfig.GPIO10, USB2AnyConfig.STATE_LOW);
        }

        private void SetPots(byte rloop, byte addVolt)
        {
            _u2a.I2C_Control(0, 0, 1);

            _u2a.I2C_RegisterWrite(EVMConfig.RLOOP_ADDR, EVMConfig.RLOOP_REG, rloop);

            _u2a.I2C_RegisterWrite(EVMConfig.TPL0102_ADDR, EVMConfig.ADDVOLT_REG_WA, addVolt);
            _u2a.I2C_RegisterWrite(EVMConfig.TPL0102_ADDR, EVMConfig.ADDVOLT_REG_WB, addVolt);

            Debug.WriteLine($"Pots set: Rloop 0x{rloop:X2}, AddVolt 0x{addVolt:X2}");
        }

        private bool SetPotsForSignalType(string signalType)
        {
            switch (signalType)
            {
                case PGAOutputConfig.Ratiometric:
                    SetPots(EVMConfig.RLOOP_10R, EVMConfig.ADDVOLT_0V0);
                    return true;

                case PGAOutputConfig.Voltage:
                    SetPots(EVMConfig.RLOOP_22R, EVMConfig.ADDVOLT_0V5);
                    return true;

                case PGAOutputConfig.Current:
                    SetPots(EVMConfig.RLOOP_80R, EVMConfig.ADDVOLT_0V6);

                    return true;

                default:
                    listBoxDebug.Items.Add($"No pot configuration for '{signalType}'.");
                    return false;
            }
        }

        private void ResetConfigForNewSelection()
        {
            _outputconfig = new PGAOutputConfig { JobCode = txtJobCode.Text.Trim() };
            lsbOutputConfig.Items.Clear();
        }

        private async void btnLoadStockCode_Click(object sender, EventArgs e) => await LoadStockCode();
               
        private async Task<bool> LoadStockCode()
        {
            string code = txtStockCode.Text.Trim();

            if (code.Length == 0)
            {
                listBoxDebug.Items.Add("Enter a stock code.");
                return false;
            }

            var spec = await _api.GetStockCode(code);

            if (spec == null)
            {
                listBoxDebug.Items.Add($"Stock code '{code}' not found.");
                return false;
            }

            string type = spec.output_type.ToLowerInvariant() switch
            {
                "ratiometric" => PGAOutputConfig.Ratiometric,
                "voltage" => PGAOutputConfig.Voltage,
                "current" => PGAOutputConfig.Current,
                _ => string.Empty
            };

            if (type.Length == 0)
            {
                listBoxDebug.Items.Add($"'{spec.stock_code}' has unknown output type '{spec.output_type}'.");
                return false;
            }

            ResetConfigForNewSelection();
            _selectedOutputMode = null;
            ResetOutputModes();

            switch (type)
            {
                case PGAOutputConfig.Ratiometric:
                    _outputconfig.SelectRatiometric();
                    break;

                case PGAOutputConfig.Current:
                    _outputconfig.SelectCurrent();
                    break;

                case PGAOutputConfig.Voltage:
                    string range = string.Format(CultureInfo.InvariantCulture, "{0:0.##}-{1:0.##}V", spec.output_min, spec.output_min);
                    try
                    {
                        _outputconfig.SelectVoltage(range);
                    }
                    catch (ArgumentException)
                    {
                        listBoxDebug.Items.Add($"'{spec.stock_code}' asks for {range}, which isn't a configured voltage range.");
                        return false;
                    }
                    break;
            }

            _outputconfig.StockCode = spec.stock_code;
            _outputconfig.PressureUnit = spec.pressure_units;
            _outputconfig.pMin = (int)spec.pressure_min;
            _outputconfig.pMax = (int)spec.pressure_max;

            if (!SetPotsForSignalType(_outputconfig.SignalType))
                return false;

            _source = ConfigSource.StockCode;

            UpdateOutputConfigSummary();
            UpdateVisibility();

            listBoxDebug.Items.Add($"{spec.stock_code}: {_outputconfig.SignalType}, " +
                                   $"{_outputconfig.pMin}-{_outputconfig.pMax} {_outputconfig.PressureUnit}");
            return true;
        }

        private void SelectManual(ATPButton button, Action apply)
        {
            ResetConfigForNewSelection();
            txtStockCode.Clear();
            _selectedOutputMode = button;

            apply();

            if (!SetPotsForSignalType(_outputconfig.SignalType))
                return;

            _source = ConfigSource.Manual;

            UpdateOutputConfigSummary();
            UpdateVisibility();
        }

        private void btnRatioPOT_Click(object sender, EventArgs e)
            => SelectManual(btnRatioPOT, () => _outputconfig.SelectRatiometric());

        private void btnCurrentPOT_Click(object sender, EventArgs e)
            => SelectManual(btnCurrentPOT, () => _outputconfig.SelectCurrent());

        private void btnVoltagePOT_Click(object sender, EventArgs e)
            => SelectManual(btnVoltagePOT, () =>
            {
                lstVoltageRange.SelectedItem = "0-10V";
                _outputconfig.SelectVoltage("0-10V");
            });

        private async void btnConnectDevice_Click(object sender, EventArgs e)
        {
            if (_source == ConfigSource.None)
            {
                listBoxDebug.Items.Add("Load a stock code or select the output type before connecting.");
                return;
            }

            btnConnectDevice.Enabled = false;

            try
            {
                bool ok = await Task.Run(() => ConnectDevice());

                if (!ok)
                {
                    listBoxDebug.Items.Add("Device failed to activate.");
                    SetDisconnected();
                    return;
                }

                if (_source == ConfigSource.StockCode)
                {
                    if (!_outputconfig.PressureRangeIsValid)
                    {
                        listBoxDebug.Items.Add($"MISMATCH: {_outputconfig.pMax} {_outputconfig.PressureUnit} " +
                                               $"exceeds device code {_outputconfig.PressureCode}.");
                        SetDisconnected();
                        return;
                    }
                }
                else
                {
                    SetPressureLimits();
                }

                UpdateOutputConfigSummary();

                listBoxDebug.Items.Add($"Serial number: {_sensorSerialNumber}");
                listBoxDebug.Items.Add($"Pressure code: {_outputconfig.PressureCode}");
                listBoxDebug.Items.Add($"Internal serial number: {_outputconfig.SerialNumber}");

                _deviceConnected = true;
                UpdateVisibility();
                ShowActiveOutputMode();
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
                SetDisconnected();
            }
            finally
            {
                btnConnectDevice.Enabled = true;
            }
        }

        private bool ConnectDevice()
        {
            if (!_pga305OWI.Activate())
                return false;

            _outputconfig.SerialNumber = _pga305OWI.ReadInternalSerialNumber();

            if (_outputconfig.SerialNumber <= 0)
                return false;

            _outputconfig.PressureCode = _pga305OWI.ReadPressureCode();
            _sensorSerialNumber = _pga305OWI.ReadSerialNumber();
            _outputconfig.SetPressureRangeFromCode();

            return true;
        }

        private void btnNoPChange_Click(object sender, EventArgs e)
        {
            _outputconfig.SetPressureRangeFromCode();
            numMinPressure.Value = _outputconfig.pMin;
            numMaxPressure.Value = _outputconfig.maxPSI;
            UpdateOutputConfigSummary();
        }

        private void UpdateOutputConfigSummary()
        {
            lsbOutputConfig.Items.Clear();
            lsbOutputConfig.Items.Add($"Output Configuration: {_outputconfig.SignalType}");
            lsbOutputConfig.Items.Add($"Electrical Output: {_outputconfig.ElectricalOutput}");
            lsbOutputConfig.Items.Add($"Pressure Range: {_outputconfig.pMin}-{_outputconfig.pMax} {_outputconfig.PressureUnit.ToUpper()}");
        }

        private void btnUnitBar_Click(object sender, EventArgs e)
        {
            _outputconfig.SetPressureUnit("bar");
            numMinPressure.Minimum = 0;
            numMinPressure.Maximum = _outputconfig.maxBar;
            numMaxPressure.Minimum = 0;
            numMaxPressure.Maximum = _outputconfig.maxBar;
            numMinPressure.Value = 0;
            numMaxPressure.Value = _outputconfig.maxBar;
            _outputconfig.pMin = 0;
            _outputconfig.pMax = _outputconfig.maxBar;
            UpdateOutputConfigSummary();
        }

        private void btnUnitPsi_Click(object sender, EventArgs e)
        {
            _outputconfig.SetPressureUnit("psi");
            numMinPressure.Minimum = 0;
            numMinPressure.Maximum = _outputconfig.maxPSI;
            numMaxPressure.Minimum = 0;
            numMaxPressure.Maximum = _outputconfig.maxPSI;
            numMinPressure.Value = 0;
            numMaxPressure.Value = _outputconfig.maxPSI;
            _outputconfig.pMin = 0;
            _outputconfig.pMax = _outputconfig.maxPSI;
            UpdateOutputConfigSummary();
        }

        private void SetVoltageRange(string range)
        {
            _outputconfig.SelectVoltage(range);
            UpdateOutputConfigSummary();
        }

        private void lstVoltageRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVoltageRange.SelectedItem is string range)
                SetVoltageRange(range);
        }

        private void btnConfirmPressure_Click(object sender, EventArgs e)
        {
            UpdateOutputConfigSummary();
        }

        private void numMinPressure_ValueChanged(object sender, EventArgs e)
        {
            _outputconfig.pMin = (int)numMinPressure.Value;
        }

        private void numMaxPressure_ValueChanged(object sender, EventArgs e)
        {
            _outputconfig.pMax = (int)numMaxPressure.Value;
        }

        private void txtJobCode_TextChanged(object sender, EventArgs e)
        {
            _outputconfig.JobCode = txtJobCode.Text.Trim();
            btnConfigDevice.Enabled = !string.IsNullOrWhiteSpace(txtJobCode.Text);
        }

        private void LogOutputConfig()
        {
            Debug.WriteLine("=== Convert Output ===");
            Debug.WriteLine($"Serial Number: {_outputconfig.SerialNumber}");
            Debug.WriteLine($"Stock Code: {_outputconfig.StockCode}");
            Debug.WriteLine($"Signal Type: {_outputconfig.SignalType}");
            Debug.WriteLine($"Electrical Output: {_outputconfig.ElectricalOutput}");
            Debug.WriteLine($"V Min: {_outputconfig.outputMin}");
            Debug.WriteLine($"V Max: {_outputconfig.outputMax}");
            Debug.WriteLine($"P Min: {_outputconfig.pMin}");
            Debug.WriteLine($"P Max: {_outputconfig.pMax}");
            Debug.WriteLine($"Pressure Unit: {_outputconfig.PressureUnit}");
        }

        private async void btnConfigDevice_Click(object sender, EventArgs e)
        {
            if (_outputconfig.SerialNumber == -1)
            {
                listBoxDebug.Items.Add("No serial number - click Connect to Device first.");
                return;
            }

            if (string.IsNullOrEmpty(_sensorSerialNumber))
            {
                listBoxDebug.Items.Add("No sensor serial number.");
                return;
            }

            string dbCode = _source == ConfigSource.StockCode
                ? _outputconfig.StockCode
                : _outputconfig.JobCode;

            if (string.IsNullOrWhiteSpace(dbCode))
            {
                listBoxDebug.Items.Add("No stock code or job code - refusing to write blank rows.");
                return;
            }

            try
            {
                LogOutputConfig();

                var result = await _api.ConvertOutput(
                    _outputconfig.SerialNumber, _outputconfig.SignalType, _outputconfig.outputMin, _outputconfig.outputMax,
                    _outputconfig.pMin, _outputconfig.pMax, _outputconfig.PressureUnit);

                if (result == null)
                {
                    listBoxDebug.Items.Add("convert-output failed.");
                    return;
                }

                if (result.serial_number != _outputconfig.SerialNumber)
                {
                    listBoxDebug.Items.Add("Serial number mismatch - aborting.");
                    return;
                }

                listBoxDebug.Items.Add("Convert output successful. Programming device...");

                if (!_pga305OWI.ProgramDevice(result.coefficients, _outputconfig.SelectedRegisters))
                {
                    listBoxDebug.Items.Add("Device programming FAILED - nothing written to database.");
                    return;
                }

                listBoxDebug.Items.Add("Device programmed and verified.");

                var createTransducer = await _api.CreateTransducer(
                        dbCode,
                        result.serial_number,
                        _outputconfig.ElectricalOutput,
                        $"{_outputconfig.pMin}-{_outputconfig.pMax} {_outputconfig.PressureUnit}",
                        _outputconfig.SignalType);

                if (!createTransducer)
                {
                    listBoxDebug.Items.Add("Transducer write FAILED - skipping coefficients.");
                    return;
                }

                listBoxDebug.Items.Add("Transducer created in database.");

                var createFinalCoeff = await _api.CreateFinalCoefficients(
                        result.session_id, result.serial_number, dbCode,
                        result.coefficients, result.padc_gain, result.tadc_gain,
                        result.padc_offset, result.tadc_offset);

                if (!createFinalCoeff)
                {
                    listBoxDebug.Items.Add("Final coefficients write FAILED.");
                    return;
                }

                listBoxDebug.Items.Add("Final coefficients written to database.");
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
                Debug.WriteLine(ex);
            }
            finally
            {
                btnConfigDevice.Enabled = true;
            }
        }

        private void SetPressureLimits()
        {
            if (_outputconfig.PressureUnit == "psiG")
            {
                numMinPressure.Minimum = 0;
                numMinPressure.Maximum = _outputconfig.maxPSI;
                numMaxPressure.Minimum = 0;
                numMaxPressure.Maximum = _outputconfig.maxPSI;
                numMinPressure.Value = 0;
                numMaxPressure.Value = _outputconfig.maxPSI;
            }
            else if (_outputconfig.PressureUnit == "bar")
            {
                numMinPressure.Minimum = 0;
                numMinPressure.Maximum = _outputconfig.maxBar;
                numMaxPressure.Minimum = 0;
                numMaxPressure.Maximum = _outputconfig.maxBar;
                numMinPressure.Value = 0;
                numMaxPressure.Value = _outputconfig.maxBar;
            }
        }

        private void btnNextDevice_Click(object sender, EventArgs e)
        {
            try
            {
                // put the OWI lines back to idle before the swap
                _u2a.GPIO_WritePort(USB2AnyConfig.GPIO11, USB2AnyConfig.STATE_LOW);
                _u2a.GPIO_WritePort(USB2AnyConfig.GPIO10, USB2AnyConfig.STATE_LOW);

                MessageBox.Show("ENSURE TO TURN OFF THE POWERSUPPLY FIRST! Fit the next device, then click OK.",
                                "Next Device", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!_pga305OWI.Initialize())
                {
                    listBoxDebug.Items.Add("Re-init FAILED.");
                    return;
                }

                ResetForNextDevice();
                listBoxDebug.Items.Add("Ready - load the stock code, then click Connect to Device.");
            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
            }
        }

        private void ResetForNextDevice()
        {
            _outputconfig = new PGAOutputConfig
            {
                JobCode = txtJobCode.Text.Trim()
            };

            _sensorSerialNumber = null;
            lsbOutputConfig.Items.Clear();
            listBoxDebug.Items.Clear();

            _selectedOutputMode = null;
            _source = ConfigSource.None;

            SetDisconnected();

            lblMinPressure.Visible = false;
            lblMaxPressure.Visible = false;
            numMinPressure.Visible = false;
            numMaxPressure.Visible = false;

            btnConfigDevice.Enabled = !string.IsNullOrWhiteSpace(txtJobCode.Text);
        }
    }
}