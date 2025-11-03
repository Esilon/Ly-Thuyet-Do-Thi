using DoThi.Class;
using Timer = System.Windows.Forms.Timer;

namespace DoThi.Animation
{
    public class KruskalAnimation : UserControl
    {
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;
        private readonly List<Edge> _minimumSpanningTree;
        private int _currentEdgeIndex;
        private readonly Timer _animationTimer;

        // Drawing settings
        private const int VertexRadius = 15;
        private readonly Brush _vertexBrush = Brushes.LightSalmon;
        private readonly Pen _vertexOutlinePen = Pens.Red;
        private readonly Pen _edgePen = new(Color.MediumPurple, 5);
        private const int ArrowSize = 18;
        private static readonly Brush ArrowBrush = Brushes.MediumPurple;
        private readonly Pen _mstEdgePen = new(Color.Green, 5);
        private readonly Brush _mstVertexBrush = Brushes.LightGreen;

        public KruskalAnimation(List<Vertex> vertices, List<Edge> edges, List<Edge> minimumSpanningTree)
        {
            _vertices = vertices;
            _edges = edges;
            _minimumSpanningTree = minimumSpanningTree;
            _currentEdgeIndex = 0;
            DoubleBuffered = true;
            InitializeAnimation();
        }

        private void InitializeAnimation()
        {
            _animationTimer = new Timer { Interval = 1000 };
            _animationTimer.Tick += OnAnimationTick;
            _animationTimer.Start();
        }

        private void OnAnimationTick(object? sender, EventArgs e)
        {
            if (_currentEdgeIndex < _minimumSpanningTree.Count)
            {
                _currentEdgeIndex++;
                Invalidate();
            }
            else
            {
                _animationTimer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.LightGray);

            DrawEdges(g);
            DrawVertices(g);
        }

        private void DrawVertices(Graphics g)
        {
            foreach (var vertex in _vertices)
            {
                var vertexBrush = _vertexBrush;
                if (_minimumSpanningTree.Take(_currentEdgeIndex).Any(e => e.Vertex1 == _vertices.IndexOf(vertex) || e.Vertex2 == _vertices.IndexOf(vertex)))
                {
                    vertexBrush = _mstVertexBrush;
                }

                g.FillEllipse(vertexBrush, vertex.Location.X - VertexRadius, vertex.Location.Y - VertexRadius, 2 * VertexRadius, 2 * VertexRadius);
                g.DrawEllipse(_vertexOutlinePen, vertex.Location.X - VertexRadius, vertex.Location.Y - VertexRadius, 2 * VertexRadius, 2 * VertexRadius);

                var stringSize = g.MeasureString(vertex.Value, Font);
                g.DrawString(vertex.Value, Font, Brushes.Black, vertex.Location.X - stringSize.Width / 2, vertex.Location.Y - stringSize.Height / 2);
            }
        }

        private void DrawEdges(Graphics g)
        {
            foreach (var edge in _edges)
            {
                var p1 = _vertices[edge.Vertex1].Location;
                var p2 = _vertices[edge.Vertex2].Location;
                var pen = _edgePen;

                if (_minimumSpanningTree.Take(_currentEdgeIndex).Contains(edge))
                {
                    pen = _mstEdgePen;
                }

                if (edge.Vertex1 == edge.Vertex2)
                {
                    DrawSelfLoop(g, p1, edge.Weight, edge.IsDirected, pen);
                }
                else
                {
                    DrawArrow(g, p1, p2, edge.Weight, edge.IsDirected, pen);
                }
            }
        }

        private void DrawSelfLoop(Graphics g, PointF location, int weight, bool isDirected, Pen pen)
        {
            int radius = 20;
            var selfLoopRect = new Rectangle((int)(location.X - radius), (int)(location.Y - radius), 2 * radius, 2 * radius);
            g.DrawEllipse(pen, selfLoopRect);

            if (isDirected)
            {
                double angle = -45 * Math.PI / 180;
                float arrowX = location.X + (float)(radius * Math.Cos(angle));
                float arrowY = location.Y + (float)(radius * Math.Sin(angle));
                var arrowHead = new PointF[]
                {
                    new(arrowX, arrowY),
                    new(arrowX - 5, arrowY + 5),
                    new(arrowX + 5, arrowY + 5)
                };
                g.FillPolygon(Brushes.Black, arrowHead);
            }

            using var font = new Font("Arial", 10);
            g.DrawString(weight.ToString(), font, Brushes.Black, new PointF(location.X + radius, location.Y - radius));
        }

        private void DrawArrow(Graphics g, PointF p1, PointF p2, int weight, bool isDirected, Pen pen)
        {
            float angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            var startPoint = new PointF(p2.X - VertexRadius * (float)Math.Cos(angle), p2.Y - VertexRadius * (float)Math.Sin(angle));
            g.DrawLine(pen, p1, startPoint);

            if (isDirected)
            {
                var arrowHead = new PointF[]
                {
                    startPoint,
                    new(startPoint.X - ArrowSize * (float)Math.Cos(angle - Math.PI / 8), startPoint.Y - ArrowSize * (float)Math.Sin(angle - Math.PI / 8)),
                    new(startPoint.X - ArrowSize * (float)Math.Cos(angle + Math.PI / 8), startPoint.Y - ArrowSize * (float)Math.Sin(angle + Math.PI / 8))
                };
                g.FillPolygon(ArrowBrush, arrowHead);
            }

            if (weight == 0) return;
            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;
            var squareRect = new RectangleF(midX - 9, midY - 9, 18, 18);
            g.FillRectangle(Brushes.DarkOrange, squareRect);
            using var font = new Font("Arial", 12);
            var textSize = g.MeasureString(weight.ToString(), font);
            g.DrawString(weight.ToString(), font, Brushes.Black, new PointF(midX - textSize.Width / 2, midY - textSize.Height / 2));
        }
    }
}
