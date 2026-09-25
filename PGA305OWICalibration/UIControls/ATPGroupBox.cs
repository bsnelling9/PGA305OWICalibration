using System.Drawing.Drawing2D;

namespace PGA305OWICalibration.UIControls
{
    public class ATPGroupBox : Panel
    {
        private int _cornerRadius = 10;
        private Color _borderColor = Color.FromArgb(214, 217, 224);

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

        public ATPGroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                    | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.FromArgb(247, 248, 250);
            ForeColor = Color.FromArgb(107, 114, 128);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        }

        private GraphicsPath CreatePath(float inset)
        {
            RectangleF rect = new RectangleF(inset, inset, Math.Max(1, Width - inset * 2), Math.Max(1, Height - inset * 2));
            float d = Math.Min(_cornerRadius, Math.Min(rect.Width, rect.Height));

            GraphicsPath path = new GraphicsPath();
            if (d <= 0) { path.AddRectangle(rect); return path; }

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            using (GraphicsPath path = CreatePath(0))
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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            using (SolidBrush brush = new SolidBrush(BackColor))
                pevent.Graphics.FillRectangle(brush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (GraphicsPath path = CreatePath(2f))
            {
                var oldMode = e.Graphics.SmoothingMode;
                e.Graphics.SmoothingMode = SmoothingMode.None;
                using (Pen pen = new Pen(_borderColor, 2f))
                    e.Graphics.DrawPath(pen, path);
                e.Graphics.SmoothingMode = oldMode;
            }

            TextRenderer.DrawText(e.Graphics, Text.ToUpper(), Font,
                new Rectangle(16, 14, Width - 32, 20), ForeColor,
                TextFormatFlags.Left | TextFormatFlags.NoPadding);
        }
    }
}