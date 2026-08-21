using System.Drawing.Drawing2D;

namespace PGA305OWICalibration.UIControls
{
    public class ATPButton : Button
    {
        private int _cornerRadius = 10;
        private Color _borderColor = Color.Black;
        private int _borderSize = 2;

        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; UpdateRegion(); Invalidate(); }
        }

        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; UpdateRegion(); Invalidate(); }
        }

        public ATPButton()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 10);
            Cursor = Cursors.Hand;
        }

        private GraphicsPath CreatePath()
        {
            Rectangle rect = new Rectangle(
                _borderSize,
                _borderSize,
                Width - _borderSize * 2,
                Height - _borderSize * 2);

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, _cornerRadius, _cornerRadius, 180, 90);
            path.AddArc(rect.Right - _cornerRadius, rect.Y, _cornerRadius, _cornerRadius, 270, 90);
            path.AddArc(rect.Right - _cornerRadius, rect.Bottom - _cornerRadius, _cornerRadius, _cornerRadius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - _cornerRadius, _cornerRadius, _cornerRadius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            using (GraphicsPath path = CreatePath())
            {
                Region old = Region;
                Region = new Region(path);
                old?.Dispose();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color back = Enabled ? BackColor : SystemColors.Control;
            Color border = Enabled ? _borderColor : SystemColors.ControlDark;
            Color text = Enabled ? ForeColor : SystemColors.GrayText;

            using (GraphicsPath path = CreatePath())
            {
                using (SolidBrush brush = new SolidBrush(back))
                    e.Graphics.FillPath(brush, path);

                using (Pen pen = new Pen(border, _borderSize))
                    e.Graphics.DrawPath(pen, path);
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, text,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}