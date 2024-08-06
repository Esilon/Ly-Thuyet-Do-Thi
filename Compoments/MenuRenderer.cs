using System.Drawing.Drawing2D;

namespace Đồ_Thị.Compoments
{
    public class MenuRenderer : ToolStripProfessionalRenderer
    {
        // Fields
        private readonly Color primaryColor;
        private readonly Color textColor;
        private readonly int arrowThickness;

        // Constructor
        public MenuRenderer(bool isMainMenu, Color primaryColor, Color textColor)
            : base(new MenuColorTable(isMainMenu, primaryColor))
        {
            this.primaryColor = primaryColor;
            if (isMainMenu)
            {
                arrowThickness = 3;
                this.textColor = textColor == Color.Empty ? Color.Gainsboro : textColor;
            }
            else
            {
                arrowThickness = 2;
                this.textColor = textColor == Color.Empty ? Color.DimGray : textColor;
            }
        }

        // Overrides
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.Item.ForeColor = e.Item.Selected ? Color.White : textColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            // Fields
            Graphics graph = e.Graphics;
            Size arrowSize = new(5, 12);
            Color arrowColor = e.Item.Selected ? Color.White : Color.White;
            Rectangle rect = new(e.ArrowRectangle.Location.X, (e.ArrowRectangle.Height - arrowSize.Height) / 2,
                arrowSize.Width, arrowSize.Height);
            using GraphicsPath path = new();
            using Pen pen = new(arrowColor, arrowThickness);
            // Drawing
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            path.AddLine(rect.Left, rect.Top, rect.Right, rect.Top + rect.Height / 2);
            path.AddLine(rect.Right, rect.Top + rect.Height / 2, rect.Left, rect.Top + rect.Height);
            graph.DrawPath(pen, path);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                Rectangle rect = new(Point.Empty, e.Item.Size);
                using Brush brush = new SolidBrush(primaryColor);
                e.Graphics.FillRectangle(brush, rect);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
    }
}
