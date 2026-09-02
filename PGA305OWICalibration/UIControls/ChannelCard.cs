using PGA305OWICalibration.Config;
using PGA305OWICalibration.PGA305;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace PGA305OWICalibration.UIControls
{
    public class ChannelCard : UserControl
    {
        protected static readonly Color SelBack = Color.FromArgb(230, 238, 255);
        protected static readonly Color SelBorder = Color.RoyalBlue;
        protected PGAOutputConfig _outputconfig = new PGAOutputConfig();
        protected bool _deviceConnected;
        protected bool _interactive = true;
        protected bool _updating;

        private int _channel;
        private Color _borderColor = Color.Gainsboro;

        private Panel? _overlay;
        private Label? _overlayText;
        private ATPButton? _overlayButton;
        private readonly List<string> _progress = new List<string>();

        public event EventHandler? ConnectRequested;
        public event EventHandler? ConfigureRequested;
        public event EventHandler? DisconnectRequested;

        protected bool HasProgress => _progress.Count > 0;

        public ChannelCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint
                   | ControlStyles.ResizeRedraw, true);

            BackColor = CardBackColor;

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                BuildOverlay();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PGAOutputConfig OutputConfig => _outputconfig;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DeviceConnected
        {
            get => _deviceConnected;
            set { _deviceConnected = value; UpdateDisplay(); }
        }

        [Category("Channel Card")]
        [DefaultValue(0)]
        [Description("Multiplexer channel 0-7 this card drives.")]
        public int Channel
        {
            get => _channel;
            set { _channel = value; UpdateDisplay(); }
        }

        [Category("Channel Card")]
        [Description("Grey idle, blue connected, green pass, red fail.")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Channel Card")]
        public Color CardBackColor { get; set; } = Color.White;

        [Category("Channel Card")]
        [DefaultValue(10)]
        public int CornerRadius { get; set; } = 10;

        [Category("Channel Card")]
        [DefaultValue(3)]
        public int CardBorderSize { get; set; } = 3;

        protected string ChannelLabel => "Channel " + (_channel + 1);

        // Repaints the card from _outputconfig. Overridden by each card type.
        public virtual void UpdateDisplay() { }

        public virtual void SetInteractive(bool enabled)
        {
            if (_interactive == enabled) return;

            _interactive = enabled;
            UpdateDisplay();
        }

        public virtual void ResetConfig(string jobCode)
        {
            _outputconfig = new PGAOutputConfig { JobCode = jobCode };
            _deviceConnected = false;
            _borderColor = Color.Gainsboro;
            ClearProgress();
            UpdateDisplay();
            Invalidate();
        }

        protected void RaiseConnectRequested()
            => ConnectRequested?.Invoke(this, EventArgs.Empty);

        protected void RaiseConfigureRequested()
            => ConfigureRequested?.Invoke(this, EventArgs.Empty);

        protected void RaiseDisconnectRequested()
            => DisconnectRequested?.Invoke(this, EventArgs.Empty);

        protected static void StyleButton(ATPButton btn, bool selected)
        {
            btn.BackColor = selected ? SelBack : Color.White;
            btn.BorderColor = selected ? SelBorder : Color.Black;
            btn.Invalidate();
        }

        private void BuildOverlay()
        {
            _overlayText = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(16),
                Font = AppConfig.ResultFont,
                BackColor = Color.Transparent
            };

            _overlayButton = new ATPButton
            {
                Text = "Reset",
                BackColor = Color.White,
                BorderColor = Color.Black,
                Visible = false
            };
            _overlayButton.Click += (s, e) => ResetConfig(string.Empty);

            _overlay = new Panel
            {
                Visible = false
            };

            _overlay.Controls.Add(_overlayText);
            _overlay.Controls.Add(_overlayButton);
            Controls.Add(_overlay);
        }

        private void LayoutOverlay()
        {
            if (_overlay == null) return;

            _overlay.BackColor = CardBackColor;

            int inset = CardBorderSize + 1;
            var bounds = new Rectangle(inset, inset, Width - inset * 2, Height - inset * 2);

            if (bounds.Width <= 0 || bounds.Height <= 0) return;

            _overlay.Bounds = bounds;

            using (var path = RoundedRect(
                new Rectangle(0, 0, bounds.Width, bounds.Height),
                Math.Max(1, CornerRadius - inset)))
            {
                _overlay.Region?.Dispose();
                _overlay.Region = new Region(path);
            }

            if (_overlayButton == null) return;

            const int buttonWidth = 125;
            const int buttonHeight = 42;

            _overlayButton.Bounds = new Rectangle(
                (bounds.Width - buttonWidth) / 2,
                (bounds.Height / 2) + 8,
                buttonWidth,
                buttonHeight);

            _overlayButton.Visible = true;

            _overlay.Controls.SetChildIndex(_overlayButton, 0);
        }

        public void ShowResult(string text, bool allowReset = false)
        {
            if (_overlay == null || _overlayText == null) return;

            _progress.Clear();
            _progress.Add(text);

            LayoutOverlay();

            _overlayText.TextAlign = ContentAlignment.MiddleCenter;
            _overlayText.Padding = allowReset
                ? new Padding(16, 16, 16, 64)
                : new Padding(16);
            _overlayText.Text = text;

            if (_overlayButton != null)
            {
                _overlayButton.Visible = allowReset;

                if (allowReset)
                    _overlay.Controls.SetChildIndex(_overlayButton, 0);
            }

            _overlay.Visible = true;
            _overlay.BringToFront();
        }

        public void ShowProgress(string line)
        {
            if (_overlay == null || _overlayText == null) return;

            _progress.Add(line);

            LayoutOverlay();

            _overlayText.TextAlign = ContentAlignment.TopLeft;
            _overlayText.Padding = new Padding(16);
            _overlayText.Text = string.Join(Environment.NewLine, _progress);

            if (_overlayButton != null)
                _overlayButton.Visible = false;

            _overlay.Visible = true;
            _overlay.BringToFront();
        }

        public void ClearProgress()
        {
            if (_overlay == null || _overlayText == null) return;

            _progress.Clear();
            _overlayText.Text = string.Empty;

            if (_overlayButton != null)
                _overlayButton.Visible = false;

            _overlay.Visible = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            using (var path = RoundedRect(new Rectangle(0, 0, Width, Height), CornerRadius))
            {
                Region?.Dispose();
                Region = new Region(path);
            }

            LayoutOverlay();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            rect.Inflate(-(CardBorderSize / 2) - 1, -(CardBorderSize / 2) - 1);

            using (var path = RoundedRect(rect, CornerRadius))
            using (var bg = new SolidBrush(CardBackColor))
            using (var pen = new Pen(_borderColor, CardBorderSize))
            {
                e.Graphics.FillPath(bg, path);
                e.Graphics.DrawPath(pen, path);
            }

            base.OnPaint(e);
        }

        protected static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = Math.Max(1, radius * 2);
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}