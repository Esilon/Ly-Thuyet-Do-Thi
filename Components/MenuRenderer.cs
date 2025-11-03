using System.Drawing.Drawing2D;

namespace DoThi.Components
{
    public class MenuRenderer : ToolStripProfessionalRenderer
    {
        // Fields
        private readonly Color _primaryColor;
        private readonly Color _textColor;
        private readonly int _arrowThickness;

        // Constructor
        public MenuRenderer(bool isMainMenu, Color primaryColor, Color textColor)
            : base(new Menu-bảng_màu(isMainMenu, primaryColor))
        {
            _primaryColor = primaryColor;
            if (isMainMenu)
            {
                _arrowThickness = 3;
                _textColor = textColor == Color.Empty ? Color.Gainsboro : textColor;
            }
            else
            {
                _arrowThickness = 2;
                _textColor = textColor == Color.Empty ? Color.DimGray : textColor;
            }
        }

        // Overrides
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.Item.ForeColor = e.Item.Selected ? Color.White : _textColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var graph = e.Graphics;
            var arrowSize = new Size(5, 12);
            var arrowColor = e.Item.Selected ? Color.White : _primaryColor;
            var rect = new Rectangle(e.ArrowRectangle.Location.X, (e.ArrowRectangle.Height - arrowSize.Height) / 2, arrowSize.Width, arrowSize.Height);

            using var path = new GraphicsPath();
            using var pen = new Pen(arrowColor, _arrowThickness);

            graph.SmoothingMode = SmoothingMode.AntiAlias;
            path.AddLine(rect.Left, rect.Top, rect.Right, rect.Top + rect.Height / 2);
            path.AddLine(rect.Right, rect.Top + rect.Height / 2, rect.Left, rect.Top + rect.Height);
            graph.DrawPath(pen, path);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                var rect = new Rectangle(Point.Empty, e.Item.Size);
                using var brush = new SolidBrush(_primaryColor);
                e.Graphics.FillRectangle(brush, rect);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
    }
}
