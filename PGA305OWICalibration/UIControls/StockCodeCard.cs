using System.ComponentModel;
using System.Diagnostics;

namespace PGA305OWICalibration.UIControls
{
    public partial class StockCodeCard : ChannelCard
    {
        private string _message = string.Empty;
        private string _lastAttempt = string.Empty;

        public StockCodeCard()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StockCodeText => txtStockCode.Text.Trim();

        public void ShowMessage(string message)
        {
            _message = message ?? string.Empty;
            UpdateDisplay();
            Invalidate();
        }

        public override void ResetConfig(string jobCode)
        {
            _message = string.Empty;
            _lastAttempt = string.Empty;
            txtStockCode.Clear();
            base.ResetConfig(jobCode);
        }

        public override void UpdateDisplay()
        {
            if (txtStockCode == null) return;

            _updating = true;

            try
            {
                string type = _outputconfig.SignalType;
                string unit = _outputconfig.PressureUnit;

                int deviceLimit = unit == "bar" ? _outputconfig.maxBar : _outputconfig.maxPSI;
                bool mismatch = _deviceConnected && deviceLimit > 0 && _outputconfig.pMax > deviceLimit;

                bool codeChanged = !string.Equals(StockCodeText, _outputconfig.StockCode,
                    StringComparison.OrdinalIgnoreCase);

                bool retryable = !string.Equals(StockCodeText, _lastAttempt,
                    StringComparison.OrdinalIgnoreCase);

                lblChannelNum.Text = ChannelLabel;

                string range = type.Length > 0
                    ? $"{_outputconfig.pMin}-{_outputconfig.pMax} {unit}"
                    : "--";

                if (_message.Length > 0)
                {
                    lblSummary.Text = _message;
                }
                else if (_deviceConnected)
                {
                    lblSummary.Text = string.Join(Environment.NewLine,
                        $"Stock code: {Or(_outputconfig.StockCode)}",
                        $"Serial: {_outputconfig.SerialNumber}",
                        $"Sensor: {Or(_outputconfig.SensorSerialNumber)}",
                        $"Pressure code: {Or(_outputconfig.PressureCode)}",
                        $"Output: {_outputconfig.ElectricalOutput} ({type})",
                        $"Pressure: {range}",
                        mismatch ? $"MISMATCH: device max is {deviceLimit} {unit}" : string.Empty);
                }
                else
                {
                    lblSummary.Text = string.Join(Environment.NewLine,
                        $"Stock code: {Or(_outputconfig.StockCode)}",
                        $"Output: {(type.Length > 0 ? $"{_outputconfig.ElectricalOutput} ({type})" : "--")}",
                        $"Pressure: {range}");
                }

                if (mismatch)
                    BorderColor = Color.Red;
                else if (_deviceConnected)
                    BorderColor = Color.RoyalBlue;

                btnConnectDevice.Text = !_deviceConnected
                    ? "Connect"
                    : codeChanged ? "Recheck code" : "Connected";

                txtStockCode.Enabled = _interactive;
                bool hasError = _message.Length > 0;

                btnConnectDevice.Enabled = _interactive
                   && StockCodeText.Length > 0
                   && retryable
                   && (hasError || !_deviceConnected || codeChanged);

                btnConfigDevice.Enabled = _interactive && _deviceConnected && !mismatch;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StockCodeCard.UpdateDisplay() exception: {ex.Message}");
            }
            finally
            {
                _updating = false;
            }
        }

        private static string Or(string value) => value.Length > 0 ? value : "--";

        private void txtStockCode_TextChanged(object sender, EventArgs e)
        {
            if (_updating) return;

            _message = string.Empty;

            if (!_deviceConnected)
                BorderColor = Color.Gainsboro;

            UpdateDisplay();
            Invalidate();
        }

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            if (StockCodeText.Length == 0) return;

            _lastAttempt = StockCodeText;
            RaiseConnectRequested();
        }

        private void btnConfigDevice_Click(object sender, EventArgs e)
        {
            if (!_deviceConnected) return;

            RaiseConfigureRequested();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (txtStockCode == null || _message.Length == 0) return;

            using (var pen = new Pen(Color.DarkOrange, 2))
                e.Graphics.DrawRectangle(pen, Rectangle.Inflate(txtStockCode.Bounds, 2, 2));
        }
    }
}