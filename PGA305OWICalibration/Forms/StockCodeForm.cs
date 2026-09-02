using PGA305OWICalibration.API;
using PGA305OWICalibration.Models;


namespace PGA305OWICalibration.Forms
{
    public partial class StockCodeForm : Form
    {
        private readonly ApiClient _api;

        private string _outputType = "";
        private string _pressureUnits = "";
        private string _pressureReference = "gauge";   

        //This should be a config
        private static readonly (double Min, double Max)[] VoltageRanges =
        {
            (0, 10), (1, 5), (0, 5), (1, 6), (0.5, 4.5)
        };

        private static readonly Color ActiveBorder = Color.FromArgb(0, 120, 215);
        private static readonly Color InactiveBorder = Color.Black;  

        public StockCode? Created { get; private set; }

        public StockCodeForm(ApiClient api)
        {
            InitializeComponent();
            _api = api;

            ConfigureControls();
            HandlePressureVisible(false);
            RefreshToggles();
            UpdateSaveEnabled();
            lstVoltageRangeVisible(false);
            HandleVoltageVisible(false);
        }

        private void ConfigureControls()
        {
            foreach (var n in new[] { numMinOutput, numMaxOutput })
            {
                n.DecimalPlaces = 3;
                n.Minimum = -1000;
                n.Maximum = 1000;
                n.Increment = 0.1m;
            }

            foreach (var n in new[] { numMinPressure, numMaxPressure })
            {
                n.DecimalPlaces = 3;
                n.Minimum = -100000;
                n.Maximum = 100000;
                n.Increment = 1m;
            }

            lstVoltageRange.Items.Clear();
            foreach (var r in VoltageRanges)
                lstVoltageRange.Items.Add($"{r.Min}-{r.Max} V");
            lstVoltageRange.Items.Add("Custom");

            lstVoltageRange.Enabled = false;
            SetOutputNumericsEnabled(false);
        }

        private void lstVoltageRangeVisible (bool visible) => lstVoltageRange.Visible = visible;

        private void RefreshToggles()
        {
            btnRatiometric.BorderColor = Border(_outputType == "ratiometric");
            btnCurrent.BorderColor = Border(_outputType == "current");
            btnVoltage.BorderColor = Border(_outputType == "voltage");
            btnUnitPsi.BorderColor = Border(_pressureUnits == "psi");
            btnUnitBar.BorderColor = Border(_pressureUnits == "bar");
        }

        private static Color Border(bool active) => active ? ActiveBorder : InactiveBorder;

        private void HandlePressureVisible(bool visible)
        {
            numMinPressure.Visible = visible;
            numMaxPressure.Visible = visible;
            lblMinPressure.Visible = visible;
            lblMaxPressure.Visible = visible;
        }

        private void HandleVoltageVisible(bool visible)
        {
            lblMinOutput.Visible = visible;
            lblMaxOuput.Visible = visible;
            numMinOutput.Visible = visible;
            numMaxOutput.Visible = visible;
        }


        private void btnRatiometric_Click(object sender, EventArgs e)
        {
            lstVoltageRangeVisible(false);
            HandleVoltageVisible(true);
            SetOutputType("ratiometric");
        }

        private void btnCurrent_Click(object sender, EventArgs e)
        {
            lstVoltageRangeVisible(false);
            HandleVoltageVisible(true);
            SetOutputType("current"); 
        }

        private void btnVoltage_Click(object sender, EventArgs e)
        {
            lstVoltageRangeVisible(false);
            HandleVoltageVisible(true);
            SetOutputType("voltage");
        }

        private void SetOutputType(string type)
        {
            _outputType = type;

            switch (type)
            {
                case "ratiometric":
                    lstVoltageRange.Enabled = false;
                    lstVoltageRange.ClearSelected();
                    SetOutput(0.5m, 4.5m);
                    SetOutputNumericsEnabled(false);
                    break;

                case "current":
                    lstVoltageRange.Enabled = false;
                    lstVoltageRange.ClearSelected();
                    SetOutput(4m, 20m);
                    SetOutputNumericsEnabled(false);
                    break;

                case "voltage":
                    lstVoltageRange.Enabled = true;
                    lstVoltageRange.SelectedIndex = 0;
                    break;
            }

            RefreshToggles();
            UpdateSaveEnabled();
        }

        private void lstVoltageRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lstVoltageRange.SelectedIndex;
            if (i < 0) return;

            if (i == VoltageRanges.Length)      
            {
                SetOutputNumericsEnabled(true);
                return;
            }

            var r = VoltageRanges[i];
            SetOutput((decimal)r.Min, (decimal)r.Max);
            SetOutputNumericsEnabled(false);
        }

        private void SetOutput(decimal min, decimal max)
        {
            numMinOutput.Value = min;
            numMaxOutput.Value = max;
        }

        private void SetOutputNumericsEnabled(bool enabled)
        {
            numMinOutput.Enabled = enabled;
            numMaxOutput.Enabled = enabled;
        }


        private void btnUnitPsi_Click(object sender, EventArgs e) => SetPressureUnits("psi");
        private void btnUnitBar_Click(object sender, EventArgs e) => SetPressureUnits("bar");

        private void SetPressureUnits(string units)
        {
            _pressureUnits = units;
            HandlePressureVisible(true);
            RefreshToggles();
            UpdateSaveEnabled();
        }


        private void UpdateSaveEnabled() =>
            btnSave.Enabled =
                txtStockCode.Text.Trim().Length > 0 &&
                _outputType.Length > 0 &&
                _pressureUnits.Length > 0;

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string stockCode = txtStockCode.Text.Trim().ToUpperInvariant();

            if (numMaxOutput.Value <= numMinOutput.Value)
            {
                Warn("Output max must be greater than output min.");
                return;
            }

            if (numMaxPressure.Value <= numMinPressure.Value)
            {
                Warn("Pressure max must be greater than pressure min.");
                return;
            }

            var existing = await _api.GetStockCode(stockCode);
            if (existing != null)
            {
                Warn($"{stockCode} already exists.");
                return;
            }

            var code = new StockCode
            {
                stock_code = stockCode,
                output_type = _outputType,
                output_min = (double)numMinOutput.Value,
                output_max = (double)numMaxOutput.Value,
                pressure_reference = _pressureReference,
                pressure_units = _pressureUnits,
                pressure_min = (double)numMinPressure.Value,
                pressure_max = (double)numMaxPressure.Value
            };

            btnSave.Enabled = false;
            try
            {
                if (!await _api.CreateStockCode(code))
                {
                    MessageBox.Show($"Could not save {stockCode}. Check the API is running.",
                        "Stock code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Created = code;
                DialogResult = DialogResult.OK;
                Close();
            }
            finally
            {
                UpdateSaveEnabled();
            }
        }

        private static void Warn(string message)
            => MessageBox.Show(message, "Stock code", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void txtStockCode_TextChanged(object sender, EventArgs e) => UpdateSaveEnabled();

        // Designer-wired stubs. To delete these, remove the matching += lines in
        // StockCodeForm.Designer.cs first or the designer will put them back.
        private void numMinOutput_ValueChanged(object sender, EventArgs e) { }
        private void numMaxOutput_ValueChanged(object sender, EventArgs e) { }
        private void numMinPressure_ValueChanged(object sender, EventArgs e) { }
        private void numMaxPressure_ValueChanged(object sender, EventArgs e) { }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}