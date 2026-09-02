using PGA305OWICalibration.PGA305;
using System.ComponentModel;

namespace PGA305OWICalibration.UIControls
{
    public partial class ManualCard : ChannelCard
    {
        public ManualCard()
        {
            InitializeComponent();

            cbxVoltageRange.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxVoltageRange.Items.AddRange(PGAOutputConfig.AvailableVoltageRanges.ToArray());
            cbxVoltageRange.SelectedIndex = 0;

            UpdateDisplay();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedVoltageRange => cbxVoltageRange.SelectedItem as string;

        public override void UpdateDisplay()
        {
            if (cbxVoltageRange == null) return;

            _updating = true;

            string type = _outputconfig.SignalType;
            string unit = _outputconfig.PressureUnit;

            StyleButton(btnOutputRM, type == PGAOutputConfig.Ratiometric);
            StyleButton(btnOutputVolt, type == PGAOutputConfig.Voltage);
            StyleButton(btnOutputCurrent, type == PGAOutputConfig.Current);
            StyleButton(btnUnitPsi, unit == "psi");
            StyleButton(btnUnitBar, unit == "bar");

            cbxVoltageRange.Visible = type == PGAOutputConfig.Voltage;

            lblChannelNum.Text = ChannelLabel;

            lblSerialNumber.Text = _outputconfig.SerialNumber > 0
                ? _outputconfig.SerialNumber.ToString()
                : "--";

            lblPressureCode.Text = _outputconfig.PressureCode.Length > 0
                ? _outputconfig.PressureCode
                : "--";

            int deviceLimit = _outputconfig.MaxPressure;
            bool mismatch = _deviceConnected && !_outputconfig.PressureRangeIsValid;
            int limit = Math.Max(deviceLimit, Math.Max(_outputconfig.pMax, 1));

            numMinPressure.Maximum = limit;
            numMaxPressure.Maximum = limit;
            numMinPressure.Value = Math.Min(_outputconfig.pMin, limit);
            numMaxPressure.Value = Math.Min(_outputconfig.pMax, limit);

            if (mismatch)
                BorderColor = Color.Red;
            else if (_deviceConnected)
                BorderColor = Color.RoyalBlue;

            string range = type.Length > 0
                ? $"{_outputconfig.pMin}-{_outputconfig.pMax} {unit}"
                : "--";

            lblSummary.Text = _deviceConnected
                ? string.Join(Environment.NewLine,
                    $"Serial: {_outputconfig.SerialNumber}",
                    $"Sensor: {_outputconfig.SensorSerialNumber}",
                    $"Pressure code: {_outputconfig.PressureCode}",
                    $"Output: {_outputconfig.ElectricalOutput} ({type})",
                    $"Pressure: {range}",
                    mismatch ? $"MISMATCH: device max is {deviceLimit} {unit}" : string.Empty)
                : string.Join(Environment.NewLine,
                    $"Output: {(type.Length > 0 ? $"{_outputconfig.ElectricalOutput} ({type})" : "--")}",
                    $"Pressure: {range}");

            btnConnectDevice.Text = _deviceConnected ? "Connected" : "Connect";

            btnOutputRM.Enabled = _interactive && !_deviceConnected;
            btnOutputVolt.Enabled = _interactive && !_deviceConnected;
            btnOutputCurrent.Enabled = _interactive && !_deviceConnected;
            cbxVoltageRange.Enabled = _interactive && !_deviceConnected;
            btnUnitPsi.Enabled = _interactive && _deviceConnected;
            btnUnitBar.Enabled = _interactive && _deviceConnected;
            numMinPressure.Enabled = _interactive && _deviceConnected;
            numMaxPressure.Enabled = _interactive && _deviceConnected;
            btnConnectDevice.Enabled = _interactive && type.Length > 0 && !_deviceConnected;
            btnConfigDevice.Enabled = _interactive && _deviceConnected && !mismatch;

            _updating = false;
        }

        private void btnOutputRM_Click(object sender, EventArgs e)
        {
            _outputconfig.SelectRatiometric();
            UpdateDisplay();
        }

        private void btnOutputVolt_Click(object sender, EventArgs e)
        {
            _outputconfig.SelectVoltage(SelectedVoltageRange);
            UpdateDisplay();
        }

        private void btnOutputCurrent_Click(object sender, EventArgs e)
        {
            _outputconfig.SelectCurrent();
            UpdateDisplay();
        }

        private void cbxVoltageRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updating || _outputconfig.SignalType != PGAOutputConfig.Voltage) return;

            _outputconfig.SelectVoltage(SelectedVoltageRange);
            UpdateDisplay();
        }

        private void numMinPressure_ValueChanged(object sender, EventArgs e)
        {
            if (_updating) return;

            _outputconfig.pMin = (int)numMinPressure.Value;
            UpdateDisplay();
        }

        private void numMaxPressure_ValueChanged(object sender, EventArgs e)
        {
            if (_updating) return;

            _outputconfig.pMax = (int)numMaxPressure.Value;
            UpdateDisplay();
        }

        private void btnUnitPsi_Click(object sender, EventArgs e)
        {
            _outputconfig.SetPressureUnit("psi");
            UpdateDisplay();
        }

        private void btnUnitBar_Click(object sender, EventArgs e)
        {
            _outputconfig.SetPressureUnit("bar");
            UpdateDisplay();
        }

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            if (_outputconfig.SignalType.Length == 0) return;

            RaiseConnectRequested();
        }

        private void btnConfigDevice_Click(object sender, EventArgs e)
        {
            if (!_deviceConnected) return;

            RaiseConfigureRequested();
        }
    }
}