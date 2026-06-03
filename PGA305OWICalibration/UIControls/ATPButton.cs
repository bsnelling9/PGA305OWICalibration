using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PGA305OWICalibration.UIControls
{
    public class ATPButton : Button
    {
        public int CornerRadius { get; set; } = 10;
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderSize { get; set; } = 2;

        public ATPButton()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 10);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = new Rectangle(
                BorderSize,
                BorderSize,
                Width - BorderSize * 2,
                Height - BorderSize * 2);

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(rect.Right - CornerRadius, rect.Y, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(rect.Right - CornerRadius, rect.Bottom - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            path.CloseFigure();

            using (SolidBrush brush = new SolidBrush(BackColor))
                e.Graphics.FillPath(brush, path);

            using (Pen pen = new Pen(BorderColor, BorderSize))
                e.Graphics.DrawPath(pen, path);

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            this.Region = new Region(path);
        }
    }
}