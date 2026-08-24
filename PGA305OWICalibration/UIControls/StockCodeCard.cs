using System.ComponentModel;
using System.Diagnostics;

namespace PGA305OWICalibration.UIControls
{
    public partial class StockCodeCard : ChannelCard
    {
        private string _message = string.Empty;
        private string _lastAttempt = string.Empty;
        private bool _selectionMode;

        public StockCodeCard()
        {
            InitializeComponent();
            Click += Card_Click;
            HookClicks(this);
            UpdateDisplay();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SelectionMode
        {
            get => _selectionMode;
            set
            {
                _selectionMode = value;

                if (!value)
                {
                    chkInclude.Checked = false;

                    if (!_deviceConnected
                        && _message.Length == 0
                        && _outputconfig.StockCode.Length == 0)
                    {
                        BorderColor = Color.Gainsboro;
                    }
                }

                UpdateDisplay();
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StockCodeText => txtStockCode.Text.Trim();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Included => chkInclude.Checked;

        public void SetStockCode(string code)
        {
            _message = string.Empty;
            _lastAttempt = string.Empty;
            txtStockCode.Text = code;
            UpdateDisplay();
            Invalidate();
        }

        private void HookClicks(Control root)
        {
            foreach (Control c in root.Controls)
            {
                if (c != chkInclude)
                    c.Click += Card_Click;

                HookClicks(c);
            }
        }

        private void Card_Click(object? sender, EventArgs e)
        {
            if (!_selectionMode || _deviceConnected) return;

            chkInclude.Checked = !chkInclude.Checked;
            UpdateDisplay();
            Invalidate();
        }

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

                int deviceLimit = _outputconfig.MaxPressure;
                bool mismatch = _deviceConnected && !_outputconfig.PressureRangeIsValid;

                bool hasError = _message.Length > 0;

                bool codeChanged = !string.Equals(StockCodeText, _outputconfig.StockCode, StringComparison.OrdinalIgnoreCase);

                bool retryable = !string.Equals(StockCodeText, _lastAttempt, StringComparison.OrdinalIgnoreCase);

                lblChannelNum.Text = ChannelLabel;

                string range = type.Length > 0
                    ? $"{_outputconfig.pMin}-{_outputconfig.pMax} {unit}" : "--";

                if (hasError)
                {
                    lblSummary.Text = _message;
                }
                else if (_deviceConnected)
                {
                    lblSummary.Text = string.Join(Environment.NewLine,
                        $"Stock code: {Or(_outputconfig.StockCode)}",
                        $"Serial Number: {_outputconfig.SerialNumber}",
                        $"Sensor Number: {Or(_outputconfig.SensorSerialNumber)}",
                        $"Pressure code: {Or(_outputconfig.PressureCode)}",
                        $"Output: {_outputconfig.ElectricalOutput} ({type})",
                        $"Pressure: {range}",
                         mismatch
                            ? string.Join(Environment.NewLine, string.Empty,
                                "Error",
                                $"Stock code {Or(_outputconfig.StockCode)} needs {_outputconfig.pMax} {unit}",
                                $"Device {Or(_outputconfig.PressureCode)} max is {deviceLimit} {unit}")
                            : string.Empty);
                }
                else
                {
                    lblSummary.Text = string.Join(Environment.NewLine,
                        $"Stock code: {Or(_outputconfig.StockCode)}",
                        $"Output: {(type.Length > 0 ? $"{_outputconfig.ElectricalOutput} ({type})" : "--")}",
                        $"Pressure: {range}");
                }

                btnConnectDevice.Text = !_deviceConnected ? "Connect" : codeChanged ? "Recheck code" : "Connected";

                bool live = _interactive && !_selectionMode;

                chkInclude.Visible = _selectionMode;
                chkInclude.Enabled = _selectionMode && _interactive && !_deviceConnected;

                txtStockCode.Enabled = live;

                btnConnectDevice.Enabled = live
                    && StockCodeText.Length > 0
                    && retryable
                    && (hasError || !_deviceConnected || codeChanged);

                btnConfigDevice.Enabled = live && _deviceConnected && !mismatch;

                if (mismatch)
                    BorderColor = Color.Red;
                else if (_deviceConnected && !hasError)
                    BorderColor = Color.RoyalBlue;
                else if (_selectionMode && !HasProgress)
                    BorderColor = chkInclude.Checked ? Color.RoyalBlue : Color.Gainsboro;
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

            bool backToLoaded = string.Equals(StockCodeText, _outputconfig.StockCode,
                StringComparison.OrdinalIgnoreCase);

            if (backToLoaded)
            {
                _message = string.Empty;
                _lastAttempt = string.Empty;
                BorderColor = _deviceConnected ? Color.RoyalBlue : Color.Gainsboro;
            }
            else if (!_deviceConnected && _message.Length == 0)
            {
                BorderColor = Color.Gainsboro;
            }

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

        public void ClearSelection()
        {
            chkInclude.Checked = false;
            UpdateDisplay();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (txtStockCode?.Parent == null || _message.Length == 0) return;

            var box = RectangleToClient(txtStockCode.Parent.RectangleToScreen(txtStockCode.Bounds));

            using (var pen = new Pen(Color.DarkOrange, 2))
                e.Graphics.DrawRectangle(pen, Rectangle.Inflate(box, 2, 2));
        }
    }
}