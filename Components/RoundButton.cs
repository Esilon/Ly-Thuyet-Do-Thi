using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace DoThi.Components
{
    public class RoundButton : Button
    {
        private int _borderSize = 0;
        private int _borderRadius = 20;
        private Color _borderColor = Color.PaleVioletRed;

        // Properties
        [Category("Round Button")]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }

        [Category("Round Button")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = (value <= Height) ? value : Height;
                Invalidate();
            }
        }

        [Category("Round Button")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Round Button")]
        public Color BackgroundColor
        {
            get => BackColor;
            set => BackColor = value;
        }

        [Category("Round Button")]
        public Color TextColor
        {
            get => ForeColor;
            set => ForeColor = value;
        }

        // Constructor
        public RoundButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(150, 40);
            BackColor = Color.MediumSlateBlue;
            ForeColor = Color.White;
            Resize += Button_Resize;
        }

        private void Button_Resize(object? sender, EventArgs e)
        {
            if (_borderRadius > Height)
                BorderRadius = Height;
        }

        // Methods
        private static GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            var path = new GraphicsPath();
            var curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            var rectSurface = ClientRectangle;
            var rectBorder = Rectangle.Inflate(rectSurface, -_borderSize, -_borderSize);
            var smoothSize = _borderSize > 0 ? _borderSize : 2;

            if (_borderRadius > 2) // Rounded button
            {
                using var pathSurface = GetFigurePath(rectSurface, _borderRadius);
                using var pathBorder = GetFigurePath(rectBorder, _borderRadius - _borderSize);
                using var penSurface = new Pen(Parent.BackColor, smoothSize);
                using var penBorder = new Pen(_borderColor, _borderSize);

                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Region = new Region(pathSurface);
                pevent.Graphics.DrawPath(penSurface, pathSurface);

                if (_borderSize >= 1)
                {
                    pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else // Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                Region = new Region(rectSurface);
                if (_borderSize >= 1)
                {
                    using var penBorder = new Pen(_borderColor, _borderSize);
                    penBorder.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += Container_BackColorChanged;
        }

        private void Container_BackColorChanged(object? sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
