using Đồ_Thị.Class;
using Timer = System.Windows.Forms.Timer;
namespace Đồ_Thị.Animation
{
    public class PrimAnimation : UserControl
    {
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;
        private readonly List<Edge> _minimumSpanningTree;
        private int _currentEdgeIndex;
        private Timer _animationTimer;

        // Background color
        private readonly Color _background = Color.LightGray;
        // Vertex and edge settings
        private const int _vertexRadius = 15;
        private readonly Brush _vertexColor = Brushes.LightSalmon;
        private readonly Pen _defaultVertexOutlineColor = Pens.Red;

        // Edge settings
        private readonly Pen _edgeLineColor = new(Color.MediumPurple, 5);
        private const int _arrowSize = 18;
        private static readonly Brush _arrowColor = Brushes.MediumPurple;
        private const int _vertexOffset = 0;
        //Prim Vis
        private readonly Pen _mstEdgeLineColor = new(Color.Green, 5);
        private readonly Brush _mstVertexColor = Brushes.LightGreen;
        // Zoom and Dragging
        private float _scaleFactor = 1.0f;
        private PointF _translation = PointF.Empty;

        public PrimAnimation(List<Vertex> vertices, List<Edge> edges, List<Edge> minimumSpanningTree)
        {
            _vertices = vertices;
            _edges = edges;
            _minimumSpanningTree = minimumSpanningTree;
            _currentEdgeIndex = 0;

            InitializeAnimation();
        }

        private void InitializeAnimation()
        {
            _animationTimer = new Timer
            {
                Interval = 1000
            };
            _animationTimer.Tick += OnAnimationTick;
            _animationTimer.Start();
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            if (_currentEdgeIndex < _minimumSpanningTree.Count)
            {
                Invalidate();
                _currentEdgeIndex++;
            }
            else
            {
                _animationTimer.Stop();
            }
        }

        private void DrawVertices(Graphics g)
        {
            foreach (Vertex vertex in _vertices)
            {
                Brush vertexBrush = _vertexColor;
                if (_minimumSpanningTree.Any(e => _vertices[e.Vertex1].Location == vertex.Location || _vertices[e.Vertex2].Location == vertex.Location) &&
                    _currentEdgeIndex > _minimumSpanningTree.FindIndex(e => _vertices[e.Vertex1].Location == vertex.Location || _vertices[e.Vertex2].Location == vertex.Location))
                {
                    vertexBrush = _mstVertexColor;
                }

                g.FillEllipse(vertexBrush, vertex.Location.X - _vertexRadius, vertex.Location.Y - _vertexRadius, 2 * _vertexRadius, 2 * _vertexRadius);
                g.DrawEllipse(_defaultVertexOutlineColor, vertex.Location.X - _vertexRadius, vertex.Location.Y - _vertexRadius, 2 * _vertexRadius, 2 * _vertexRadius);
                SizeF stringSize = g.MeasureString(vertex.Value, Font);
                g.DrawString(vertex.Value, Font, Brushes.Black, vertex.Location.X - stringSize.Width / 2, vertex.Location.Y - stringSize.Height / 2);
            }
        }

        private void DrawEdges(Graphics g)
        {
            foreach (Edge edge in _edges)
            {
                PointF p1 = _vertices[edge.Vertex1].Location;
                PointF p2 = _vertices[edge.Vertex2].Location;

                Pen pen = _edgeLineColor;
                if (_minimumSpanningTree.Contains(edge) && _currentEdgeIndex > _minimumSpanningTree.IndexOf(edge))
                {
                    pen = _mstEdgeLineColor;
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
            Rectangle selfLoopRect = new((int)(location.X - radius), (int)(location.Y - radius), 2 * radius, 2 * radius);

            g.DrawEllipse(pen, selfLoopRect);
            if (isDirected)
            {
                DrawSelfLoopArrow(g, location, radius);
            }

            using Font font = new("Arial", 10);
            PointF weightLocation = new(location.X + radius, location.Y - radius);
            g.DrawString(weight.ToString(), font, Brushes.Black, weightLocation);
        }

        private static void DrawSelfLoopArrow(Graphics g, PointF location, int radius)
        {
            double angle = -45;
            double radian = angle * Math.PI / 180;
            float arrowX = location.X + (float)(radius * Math.Cos(radian));
            float arrowY = location.Y + (float)(radius * Math.Sin(radian));

            PointF[] arrowHead =
            {
                new(arrowX, arrowY),
                new(arrowX - 5, arrowY + 5),
                new(arrowX + 5, arrowY + 5)
            };
            g.FillPolygon(Brushes.Black, arrowHead);
        }

        private void DrawArrow(Graphics g, PointF p1, PointF p2, int weight, bool isDirected, Pen pen)
        {
            float angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            PointF startPointF = new(p2.X - (_vertexRadius + _vertexOffset) * (float)Math.Cos(angle),
                                           p2.Y - (_vertexRadius + _vertexOffset) * (float)Math.Sin(angle));

            g.DrawLine(pen, p1, startPointF);

            if (isDirected)
            {
                DrawArrowHead(g, startPointF, angle);
            }

            DrawEdgeWeight(g, p1, p2, weight);
        }

        private static void DrawEdgeWeight(Graphics g, PointF p1, PointF p2, int weight)
        {
            if (weight == 0) return;

            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;
            RectangleF squareRect = new(midX - 9, midY - 9, 18, 18);

            g.FillRectangle(Brushes.Black, squareRect);
            g.FillRectangle(Brushes.DarkOrange, squareRect);

            using Font font = new("Arial", 12);
            SizeF textSize = g.MeasureString(weight.ToString(), font);
            PointF textLocation = new(midX - textSize.Width / 2, midY - textSize.Height / 2);
            g.DrawString(weight.ToString(), font, Brushes.Black, textLocation);
        }

        private static void DrawArrowHead(Graphics g, PointF startPointF, float angle)
        {
            PointF[] arrowHeadPointFs =
            {
                new(startPointF.X, startPointF.Y),
                new(startPointF.X - _arrowSize * (float)Math.Cos(angle - Math.PI / 6), startPointF.Y - _arrowSize * (float)Math.Sin(angle - Math.PI / 6)),
                new(startPointF.X - _arrowSize * (float)Math.Cos(angle + Math.PI / 6), startPointF.Y - _arrowSize * (float)Math.Sin(angle + Math.PI / 6))
            };
            g.FillPolygon(_arrowColor, arrowHeadPointFs);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(_background);
            ApplyTransform(g);
            DrawEdges(g);
            DrawVertices(g);
        }
        public void SetTransform(float scaleFactor, PointF translation)
        {
            _scaleFactor = scaleFactor;
            _translation = translation;
            Invalidate();
        }

        private void ApplyTransform(Graphics g)
        {
            g.ScaleTransform(_scaleFactor, _scaleFactor);
            g.TranslateTransform(_translation.X, _translation.Y);
        }
    }
}
