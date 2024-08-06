using Đồ_Thị.Class;
using Timer = System.Windows.Forms.Timer;

namespace Đồ_Thị.uc
{
    public class DijkstraAnimation : UserControl
    {
        // Fields
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;
        private readonly List<int[]> _steps;
        private readonly int _sourceVertex;
        private readonly int _destinationVertex;
        private int _currentStepIndex;
        private Timer _animationTimer;
        private List<Edge> _shortestPathEdges;

        // Colors and drawing settings
        private readonly Color _background = Color.LightGray;
        private const int _vertexRadius = 15;
        private readonly Brush _vertexColor = Brushes.LightSalmon;
        private readonly Pen _defaultVertexOutlineColor = Pens.Red;
        private readonly Pen _edgeLineColor = new(Color.MediumPurple, 5);
        private const int _arrowSize = 18;
        private static readonly Brush _arrowColor = Brushes.MediumPurple;
        private const int _vertexOffset = 0;
        private readonly Pen _currentEdgeLineColor = new(Color.Green, 5);
        private readonly Brush _currentVertexColor = Brushes.LightGreen;
        // Dijkstra Vis
        private readonly Brush[] _originalVertexColors;
        private readonly Pen[] _originalEdgePens;
        // Zoom and Dragging
        private float _scaleFactor = 1.0f;
        private PointF _translation = PointF.Empty;


        public DijkstraAnimation(List<Vertex> vertices, List<Edge> edges, List<int[]> steps, int sourceVertex, int destinationVertex)
        {
            _vertices = vertices;
            _edges = edges;
            _steps = steps;
            _sourceVertex = sourceVertex;
            _destinationVertex = destinationVertex;
            _currentStepIndex = 0;
            _originalVertexColors = new Brush[_vertices.Count];
            for (int i = 0; i < _vertices.Count; i++)
            {
                _originalVertexColors[i] = _vertexColor;
            }

            _originalEdgePens = new Pen[_edges.Count];
            for (int i = 0; i < _edges.Count; i++)
            {
                _originalEdgePens[i] = _edgeLineColor;
            }

            InitializeAnimation();
            CalculateShortestPath();
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
            if (_currentStepIndex < 1)
            {
                Invalidate();
                _currentStepIndex++;
            }
            else
            {
                _animationTimer.Stop();
            }
        }
        private void CalculateShortestPath()
        {
            _shortestPathEdges = new List<Edge>(); // List to store the edges of the shortest path
            List<int> _parentVertices = new(); // List to store parent vertices for path reconstruction

            // Initialize parent vertices list with -1 (indicating no parent)
            for (int i = 0; i < _vertices.Count; i++)
            {
                _parentVertices.Add(-1);
            }

            // Initialize distances dictionary with maximum value, except for source vertex which is 0
            Dictionary<int, int> distances = new();
            foreach (Vertex vertex in _vertices)
            {
                int a = _vertices.IndexOf(vertex);
                distances[a] = int.MaxValue;
            }
            distances[_sourceVertex] = 0;

            // Priority queue for processing vertices based on shortest distance
            PriorityQueue<int> priorityQueue = new();
            priorityQueue.Enqueue(_sourceVertex, 0);

            // Dijkstra's algorithm main loop
            while (priorityQueue.Count > 0)
            {
                int u = priorityQueue.Dequeue();

                // Iterate through edges connected to vertex u
                foreach (Edge? edge in _edges.Where(e =>
                    e.Vertex1 == u || (!e.IsDirected && e.Vertex2 == u)))
                {
                    int v = (edge.Vertex1 == u) ? edge.Vertex2 : edge.Vertex1; // Determine adjacent vertex
                    int weight = edge.Weight;

                    // Relaxation step: Update distance if a shorter path to v is found through u
                    if (distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight; // Update distance
                        _parentVertices[v] = u; // Update parent vertex for path reconstruction
                        priorityQueue.Enqueue(v, distances[v]); // Enqueue v with updated distance
                    }
                }
            }

            // Path reconstruction from destination to source
            int currentVertex = _destinationVertex;
            while (currentVertex != _sourceVertex && _parentVertices[currentVertex] != -1)
            {
                int parentVertex = _parentVertices[currentVertex];

                // Find edge connecting parentVertex to currentVertex
                Edge shortestPathEdge = _edges.FirstOrDefault(edge =>
                    (edge.Vertex1 == parentVertex && edge.Vertex2 == currentVertex) ||
                    (!edge.IsDirected && edge.Vertex1 == currentVertex && edge.Vertex2 == parentVertex));

                if (shortestPathEdge != null)
                {
                    _shortestPathEdges.Add(shortestPathEdge); // Add edge to shortest path edges list
                }

                currentVertex = parentVertex; // Move to parent vertex
            }
            _shortestPathEdges.Reverse(); // Reverse the list to get the path from source to destination
        }


        private void DrawVertices(Graphics g)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                Brush vertexBrush = _originalVertexColors[i];
                if (_currentStepIndex < _steps.Count && _steps[_currentStepIndex][i] != int.MaxValue)
                {
                    vertexBrush = _currentVertexColor;
                }

                g.FillEllipse(vertexBrush, _vertices[i].Location.X - _vertexRadius, _vertices[i].Location.Y - _vertexRadius, 2 * _vertexRadius, 2 * _vertexRadius);
                g.DrawEllipse(_defaultVertexOutlineColor, _vertices[i].Location.X - _vertexRadius, _vertices[i].Location.Y - _vertexRadius, 2 * _vertexRadius, 2 * _vertexRadius);
                SizeF stringSize = g.MeasureString(_vertices[i].Value, Font);
                g.DrawString(_vertices[i].Value, Font, Brushes.Black, _vertices[i].Location.X - stringSize.Width / 2, _vertices[i].Location.Y - stringSize.Height / 2);
            }
        }

        private void DrawEdges(Graphics g)
        {
            foreach (Edge edge in _edges)
            {
                int edgeIndex = _edges.IndexOf(edge);
                Pen pen = (edgeIndex != -1) ? _originalEdgePens[edgeIndex] : _edgeLineColor;
                PointF p1 = _vertices[edge.Vertex1].Location;
                PointF p2 = _vertices[edge.Vertex2].Location;

                if (_currentStepIndex < _steps.Count &&
                    ((_steps[_currentStepIndex][edge.Vertex1] != int.MaxValue && _steps[_currentStepIndex][edge.Vertex2] != int.MaxValue) ||
                    (!_edges.Exists(e => e.Vertex1 == edge.Vertex1 && e.Vertex2 == edge.Vertex2))))
                {
                    pen = _currentEdgeLineColor;
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

        private void DrawSelfLoopArrow(Graphics g, PointF location, int radius)
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

        private void DrawArrowHead(Graphics g, PointF startPointF, float angle)
        {
            PointF[] arrowHead =
            {
                startPointF,
                new(startPointF.X - _arrowSize * (float)Math.Cos(angle - Math.PI / 8),
                           startPointF.Y - _arrowSize * (float)Math.Sin(angle - Math.PI / 8)),
                new(startPointF.X - _arrowSize * (float)Math.Cos(angle + Math.PI / 8),
                           startPointF.Y - _arrowSize * (float)Math.Sin(angle + Math.PI / 8))
            };

            g.FillPolygon(_arrowColor, arrowHead);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(_background);
            ApplyTransform(g);
            DrawEdges(g);
            DrawVertices(g);
            if (_shortestPathEdges != null && _shortestPathEdges.Count > 0)
            {
                foreach (Edge edge in _shortestPathEdges)
                {
                    int vertex1 = edge.Vertex1;
                    int vertex2 = edge.Vertex2;

                    _originalVertexColors[vertex1] = _currentVertexColor;
                    _originalVertexColors[vertex2] = _currentVertexColor;

                    int edgeIndex = _edges.IndexOf(edge);
                    if (edgeIndex != -1)
                    {
                        _originalEdgePens[edgeIndex] = _currentEdgeLineColor;
                    }
                }
            }
            foreach (Edge edge in _edges)
            {
                int edgeIndex = _edges.IndexOf(edge);
                Pen pen = (edgeIndex != -1) ? _originalEdgePens[edgeIndex] : _edgeLineColor;
                PointF p1 = _vertices[edge.Vertex1].Location;
                PointF p2 = _vertices[edge.Vertex2].Location;

                if (edge.Vertex1 == edge.Vertex2)
                {
                    DrawSelfLoop(g, p1, edge.Weight, edge.IsDirected, pen);
                }
                else
                {
                    DrawArrow(g, p1, p2, edge.Weight, edge.IsDirected, pen);
                }
            }
            for (int i = 0; i < _vertices.Count; i++)
            {
                Brush vertexBrush = _originalVertexColors[i];
                g.FillEllipse(vertexBrush, _vertices[i].Location.X - _vertexRadius, _vertices[i].Location.Y - _vertexRadius, 2 * _vertexRadius, 2 * _vertexRadius);
                g.DrawEllipse(_defaultVertexOutlineColor, _vertices[i].Location.X - _vertexRadius, _vertices[i].Location.Y - _vertexRadius, 2 * _vertexRadius, 2 * _vertexRadius);
                SizeF stringSize = g.MeasureString(_vertices[i].Value, Font);
                g.DrawString(_vertices[i].Value, Font, Brushes.Black, _vertices[i].Location.X - stringSize.Width / 2, _vertices[i].Location.Y - stringSize.Height / 2);
            }
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
    public class PriorityQueue<T>
    {
        private readonly SortedDictionary<int, Queue<T>> _sortedItems = [];
        public void Enqueue(T item, int priority)
        {
            if (!_sortedItems.ContainsKey(priority))
            {
                _sortedItems[priority] = new Queue<T>();
            }
            _sortedItems[priority].Enqueue(item);
        }
        public T Dequeue()
        {
            int key = _sortedItems.Keys.First();
            Queue<T> queue = _sortedItems[key];
            T? item = queue.Dequeue();
            if (queue.Count == 0)
            {
                _ = _sortedItems.Remove(key);
            }

            return item;
        }
        public bool IsEmpty => !_sortedItems.Any();
        public int Count => _sortedItems.Values.Sum(queue => queue.Count);
    }
}
