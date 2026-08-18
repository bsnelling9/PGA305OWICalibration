using PGA305OWICalibration.API;
using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305EVM;
using PGA305OWICalibration.PGA305;
using System.Diagnostics;

namespace PGA305OWICalibration
{
    public partial class Form2 : Form
    {
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private PGA305Owi _pga305OWI = null!;
        private ApiClient _api = new ApiClient();
        private PGAOutputConfig _outputconfig = new PGAOutputConfig();

        private string? _sensorSerialNumber;

        public Form2()
        {
            InitializeComponent();
            SetOutputConfigVisible(false);
            btnConfigDevice.Enabled = false;
            gbxConfigOutput.Visible = false;
            gbxConfigPressure.Visible = false;
            lblStep2.Visible = false;
            btnConnectDevice.Visible = false;
            gbxConfigDevice.Visible = false;
            lblJobCode.Visible = false;
            txtJobCode.Visible = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { ParkOwiLines(); } catch (Exception ex) { Debug.WriteLine(ex.Message); }
            base.OnFormClosing(e);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void ParkOwiLines()
        {
            _u2a.GPIO_WritePort(AppConfig.GPIO11, AppConfig.STATE_LOW);
            _u2a.GPIO_WritePort(AppConfig.GPIO10, AppConfig.STATE_LOW);
        }

        private void SetOutputConfigVisible(bool visible)
        {
            gbxConfigOutput.Visible = visible;
            gbxConfigPressure.Visible = visible;
        }

        private void btnHandlePOT_Click(object sender, EventArgs e)
        {
            this.HandlePOT();
        }

        // This method handles the potentiometer configuration for the EVM
        private void HandlePOT()
        {
            int i2cResult = _u2a.I2C_Control(0, 0, 1);
            Debug.WriteLine($"I2C_Control result: {i2cResult}");

            int writeResult = _u2a.I2C_RegisterWrite(AppConfig.DIGIPOT_ADDR, AppConfig.DIGIPOT_REG, AppConfig.DIGIPOT_VALUE);
            Debug.WriteLine($"DigiPot write result: {writeResult}");

            int result = _u2a.I2C_RegisterWrite(AppConfig.TPL0102_ADDR, 0x00, 0x00);
            Debug.WriteLine($"TPL0102 Pot0 write result: {result}");

            result = _u2a.I2C_RegisterWrite(AppConfig.TPL0102_ADDR, 0x01, 0x00);
            Debug.WriteLine($"TPL0102 Pot1 write result: {result}");
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
                listBoxDebug.Items.Add($"Power result: {powerResult}");

                listBoxDebug.Items.Add($"USB2ANY opened. Handle = {_u2a.GetHandle()}");
                _pga305OWI = new PGA305Owi(_u2a);

                this.HandlePOT();

                bool initOk = _pga305OWI.Initialize();
                listBoxDebug.Items.Add($"PGA305 init: {(initOk ? "OK" : "FAILED")}");

                if (initOk)
                {
                    listBoxDebug.Items.Add("Please click Connect to Device.");
                    lblStep2.Visible = true;
                    btnConnectDevice.Visible = true;
                    lblJobCode.Visible = true;
                    txtJobCode.Visible = true;
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

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            try
            {
                bool activate = _pga305OWI.Activate();

                if (!activate)
                {
                    listBoxDebug.Items.Add("Device failed to activate.");
                    SetOutputConfigVisible(false);
                    return;
                }
                _outputconfig.SerialNumber = _pga305OWI.ReadInternalSerialNumber();
                _outputconfig.PressureCode = _pga305OWI.ReadPressureCode();
                _sensorSerialNumber = _pga305OWI.ReadSerialNumber();

                if (_outputconfig.SerialNumber == -1)
                {
                    listBoxDebug.Items.Add("Failed to read internal serial number.");
                    SetOutputConfigVisible(true);
                    return;
                }
                _outputconfig.SetPressureRangeFromCode();
                SetPressureLimits();

                Debug.WriteLine($"Pressure range set to: {_outputconfig.maxPSI} - {_outputconfig.maxBar} {_outputconfig.PressureUnit}");
                                
                listBoxDebug.Items.Add($"Serial number: {_sensorSerialNumber}");
                listBoxDebug.Items.Add($"Pressure code: {_outputconfig.PressureCode}");
                listBoxDebug.Items.Add($"Internal serial number: {_outputconfig.SerialNumber}");

                SetOutputConfigVisible(true);
                SelectVoltageOutput();

            }
            catch (Exception ex)
            {
                listBoxDebug.Items.Add($"Error: {ex.Message}");
                SetOutputConfigVisible(false);
            }
        }

        private void btnNoPChange_Click(object sender, EventArgs e)
        {
            _outputconfig.ResetPressureRange();

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

            lblMinPressure.Visible = true;
            lblMaxPressure.Visible = true;
            numMinPressure.Visible = true;
            numMaxPressure.Visible = true;
            btnConfigDevice.Visible = true;
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

            lblMinPressure.Visible = true;
            lblMaxPressure.Visible = true;
            numMinPressure.Visible = true;
            numMaxPressure.Visible = true;
            btnConfigDevice.Visible = true;
        }

        private void SetVoltageRange(string range)
        {
            _outputconfig.SelectVoltage(range);
            UpdateOutputConfigSummary();
            gbxConfigDevice.Visible = true;
        }

        private void lstVoltageRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVoltageRange.SelectedItem is string range)
                SetVoltageRange(range);
        }

        private void SelectVoltageOutput(string range = "0-10V")
        {
            lblVoltageRange.Visible = true;
            lstVoltageRange.Visible = true;
            gbxConfigPressure.Visible = true;

            lstVoltageRange.SelectedItem = range;
            SetVoltageRange(range);               
        }

        private void BtnOutputV_Click(object sender, EventArgs e) => SelectVoltageOutput();

        private void BtnOutputRM_Click(object sender, EventArgs e)
        {
            _outputconfig.SelectRatiometric();

            lblVoltageRange.Visible = false;
            lstVoltageRange.Visible = false;

            UpdateOutputConfigSummary();
        }

        private void BtnOutputC_Click(object sender, EventArgs e)
        {
            _outputconfig.SelectCurrent();

            lblVoltageRange.Visible = false;
            lstVoltageRange.Visible = false;

            UpdateOutputConfigSummary();
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
            Debug.WriteLine($"Signal Type: {_outputconfig.SignalType}");
            Debug.WriteLine($"Electrical Output: {_outputconfig.ElectricalOutput}");
            Debug.WriteLine($"V Min: {_outputconfig.vMin}");
            Debug.WriteLine($"V Max: {_outputconfig.vMax}");
            Debug.WriteLine($"P Min: {_outputconfig.pMin}");
            Debug.WriteLine($"P Max: {_outputconfig.pMax}");
            Debug.WriteLine($"Pressure Unit: {_outputconfig.PressureUnit}");
        }

        private async void btnConfigDevice_Click(object sender, EventArgs e)
        {

            if (_outputconfig.SerialNumber == -1)
            {
                listBoxDebug.Items.Add("No serial number — click Read Device first.");
                return;
            }

            if (string.IsNullOrEmpty(_sensorSerialNumber))
            {
                listBoxDebug.Items.Add("No sensor serial number.");
                return;
            }
            try
            {
                LogOutputConfig();

                var result = await _api.ConvertOutput(
                    _outputconfig.SerialNumber, _outputconfig.vMin, _outputconfig.vMax,
                    _outputconfig.pMin, _outputconfig.pMax, _outputconfig.PressureUnit);

                if (result == null)
                {
                    listBoxDebug.Items.Add("convert-output failed.");
                    return;
                }

                if (result.serial_number != _outputconfig.SerialNumber)
                {
                    listBoxDebug.Items.Add("Serial number mismatch — aborting.");
                    return;
                }

                listBoxDebug.Items.Add("Convert output successful. Programming device...");

                if (!_pga305OWI.ProgramDevice(result.coefficients, _outputconfig.SelectedRegisters))
                {
                    listBoxDebug.Items.Add("Device programming FAILED — nothing written to database.");
                    return;
                }
                listBoxDebug.Items.Add("Device programmed and verified.");

                var createTransducer = await _api.CreateTransducer(
                        _outputconfig.JobCode,
                        result.serial_number,
                        _outputconfig.ElectricalOutput,
                        $"{_outputconfig.pMin}-{_outputconfig.pMax} {_outputconfig.PressureUnit}",
                        _outputconfig.SignalType);
                if (!createTransducer)
                {
                    listBoxDebug.Items.Add("Transducer write FAILED — skipping coefficients.");
                    return;
                }

                listBoxDebug.Items.Add("Transducer created in database.");

                var createFinalCoeff = await _api.CreateFinalCoefficients(
                        result.session_id, result.serial_number, _outputconfig.JobCode,
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
                _u2a.GPIO_WritePort(AppConfig.GPIO11, AppConfig.STATE_LOW);
                _u2a.GPIO_WritePort(AppConfig.GPIO10, AppConfig.STATE_LOW);

                MessageBox.Show("ENSURE TO TURN OFF THE POWERSUPPLY FIRST! Fit the next device, then click OK.",
                                "Next Device", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HandlePOT();

                if (!_pga305OWI.Initialize())
                {
                    listBoxDebug.Items.Add("Re-init FAILED.");
                    return;
                }

                ResetForNextDevice();
                listBoxDebug.Items.Add("Ready — click Connect to Device.");
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

            SetOutputConfigVisible(false);
            lblMinPressure.Visible = false;
            lblMaxPressure.Visible = false;
            numMinPressure.Visible = false;
            numMaxPressure.Visible = false;

            btnConfigDevice.Enabled = !string.IsNullOrWhiteSpace(txtJobCode.Text);
        }
    }
}