using DoThi.Animation;
using DoThi.Class;
using DoThi.Components;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace DoThi.uc
{
    public partial class MatrixShow : UserControl
    {
        private readonly MatrixBlock _matrixBlock = new();
        private readonly MovingBall _movingBall;
        private GraphData _graphData;
        private List<Vertex> _vertices = new();
        private List<Edge> _edges = new();
        private int[,]? _adjacencyMatrix;
        private int[,]? _weightMatrix;
        private int _currentMode = 0; // 0: Select, 1: Add Vertex, 2: Add Edge, 3: Delete
        private int _selectedVertexIndex = -1;
        private Vertex? _startVertex = null;
        private PointF _currentMousePosition;

        // Drawing settings
        private const int VertexRadius = 14;
        private readonly Brush _vertexBrush = Brushes.LightSalmon;
        private readonly Brush _selectedVertexBrush = Brushes.Yellow;
        private readonly Pen _vertexOutlinePen = Pens.Red;
        private readonly Pen _edgePen = new(Color.MediumPurple, 5);
        private const int ArrowSize = 18;
        private static readonly Brush ArrowBrush = Brushes.MediumPurple;

        // Pan and Zoom
        private float _zoomLevel = 1.0f;
        private PointF _panOffset = PointF.Empty;
        private Point _initialMousePosition;
        private bool _isDraggingMap = false;

        public MatrixShow()
        {
            InitializeComponent();
            SetDoubleBufferedPanel();
            _movingBall = new MovingBall(panelDraw);
            panelDraw.MouseWheel += OnPanelDrawMouseWheel;
            txtZoom.Text = _zoomLevel.ToString();
        }

        private void SetDoubleBufferedPanel()
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panelDraw, new object[] { true });
            ResizeRedraw = true;
        }

        private void ShowOrHideMatrixBlock()
        {
            if (_vertices.Count > 0 && !panelDraw.Controls.Contains(_matrixBlock))
            {
                panelDraw.Controls.Add(_matrixBlock);
            }
            else if (_vertices.Count == 0 && panelDraw.Controls.Contains(_matrixBlock))
            {
                panelDraw.Controls.Remove(_matrixBlock);
            }
            panelDraw.Invalidate();
        }

        #region UI Event Handlers

        private void btnAddVertex_Click(object sender, EventArgs e) => SetMode(1);
        private void btnAddEdge_Click(object sender, EventArgs e) => SetMode(2);
        private void btnSelect_Click(object sender, EventArgs e) => SetMode(0);
        private void btnDelete_Click(object sender, EventArgs e) => SetMode(3);

        private void btnClearMovingBall_Click(object sender, EventArgs e)
        {
            _movingBall.Stop();
            _movingBall.EdgePath.Clear();
            panelDraw.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearData();
        private void btnSearch_Click(object sender, EventArgs e) => drpSearch.Show(btnSearchMenu, btnSearchMenu.Width, 0);
        private void btnGraph_Click(object sender, EventArgs e) => drpGraph.Show(btnGraph, btnGraph.Width, 0);

        private void rbMatrixType_CheckedChanged(object sender, EventArgs e)
        {
            _matrixBlock.MatrixType = radioWeightMatrix.Checked ? MatrixBlock.MatrixType.Weight : MatrixBlock.MatrixType.Adjacency;
        }

        private void saveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _graphData = new GraphData
            {
                Vertices = _vertices,
                Edges = _edges,
                AdjacencyMatrix = _adjacencyMatrix,
                WeightMatrix = _weightMatrix
            };
            _graphData.Save();
        }

        private void loadGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearData();
            var loadedGraph = GraphData.OpenFile();
            if (loadedGraph == null) return;

            MessageBox.Show("Graph loaded successfully.");
            _vertices = loadedGraph.Vertices;
            _edges = loadedGraph.Edges;
            _adjacencyMatrix = loadedGraph.AdjacencyMatrix;
            _weightMatrix = loadedGraph.WeightMatrix;

            panelDraw.Invalidate();
            ShowOrHideMatrixBlock();
            UpdateMatrices();
        }

        private void btnExportMatrix_Click(object sender, EventArgs e)
        {
            if (_vertices.Count < 2)
            {
                MessageBox.Show("The graph must have at least 2 vertices.");
                return;
            }

            var exporter = new GraphExporter(_vertices);
            var matrixToSave = radioAdjMatrix.Checked ? _adjacencyMatrix : _weightMatrix;
            if (matrixToSave != null)
            {
                exporter.SaveMatrixToFile(matrixToSave);
            }
        }

        #endregion

        private void SetMode(int mode)
        {
            _currentMode = mode;
            btnSelect.Enabled = _currentMode != 0;
            btnAddVertex.Enabled = _currentMode != 1;
            btnAddEdge.Enabled = _currentMode != 2;
            btnDelete.Enabled = _currentMode != 3;
        }

        private void ClearData()
        {
            _vertices.Clear();
            _edges.Clear();
            _adjacencyMatrix = new int[0, 0];
            _weightMatrix = new int[0, 0];
            _matrixBlock.Reset();
            panelDraw.Invalidate();
            ShowOrHideMatrixBlock();
        }

        private void UpdateMatrices()
        {
            int size = _vertices.Count;
            _adjacencyMatrix = new int[size, size];
            _weightMatrix = new int[size, size];

            foreach (var edge in _edges)
            {
                _adjacencyMatrix[edge.Vertex1, edge.Vertex2] = 1;
                _weightMatrix[edge.Vertex1, edge.Vertex2] = edge.Weight;
                if (!edge.IsDirected)
                {
                    _adjacencyMatrix[edge.Vertex2, edge.Vertex1] = 1;
                    _weightMatrix[edge.Vertex2, edge.Vertex1] = edge.Weight;
                }
            }

            _matrixBlock.AdjacencyMatrix = _adjacencyMatrix;
            _matrixBlock.WeightMatrix = _weightMatrix;
            for (int i = 0; i < _vertices.Count; i++)
            {
                _matrixBlock.SetVertexValue(i, _vertices[i].Value);
            }

            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            cbStartVertex.Items.Clear();
            cbEndVertex.Items.Clear();
            foreach (var vertex in _vertices)
            {
                cbStartVertex.Items.Add(vertex.Value);
                cbEndVertex.Items.Add(vertex.Value);
            }
        }

        #region Panel Drawing and Mouse Events

        private void panelDraw_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.LightGray);
            g.TranslateTransform(_panOffset.X, _panOffset.Y);
            g.ScaleTransform(_zoomLevel, _zoomLevel);

            DrawEdges(g);
            DrawVertices(g);
            _movingBall.Draw(g);

            if (_currentMode == 2 && _startVertex != null)
            {
                g.DrawLine(Pens.Gray, _startVertex.Location, _currentMousePosition);
            }
        }

        private void OnPanelDrawMouseWheel(object? sender, MouseEventArgs e)
        {
            float zoomFactor = e.Delta < 0 ? 1.0f / 1.1f : 1.1f;
            _zoomLevel *= zoomFactor;
            _panOffset.X = e.X - (e.X - _panOffset.X) * zoomFactor;
            _panOffset.Y = e.Y - (e.Y - _panOffset.Y) * zoomFactor;
            txtZoom.Text = _zoomLevel.ToString("F2");
            panelDraw.Invalidate();
        }

        private void panelDraw_MouseMove(object sender, MouseEventArgs e)
        {
            var transformedLocation = TransformMouseCoordinates(e.Location);
            panelDraw.Cursor = GetVertexAt(transformedLocation) != -1 || IsCursorOnEdge(transformedLocation) ? Cursors.Hand : Cursors.Default;

            if (e.Button == MouseButtons.Left && _currentMode == 0 && GetVertexAt(transformedLocation) != -1)
            {
                _vertices[GetVertexAt(transformedLocation)].Location = transformedLocation;
                panelDraw.Invalidate();
            }
            else if (e.Button == MouseButtons.Right && _isDraggingMap)
            {
                _panOffset.X += (e.X - _initialMousePosition.X);
                _panOffset.Y += (e.Y - _initialMousePosition.Y);
                _initialMousePosition = e.Location;
                panelDraw.Invalidate();
            }

            if (_currentMode == 2 && _startVertex != null)
            {
                _currentMousePosition = transformedLocation;
                panelDraw.Invalidate();
            }
        }

        private void panelDraw_MouseDown(object sender, MouseEventArgs e)
        {
            var transformedLocation = TransformMouseCoordinates(e.Location);

            if (e.Button == MouseButtons.Right)
            {
                _initialMousePosition = e.Location;
                _isDraggingMap = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                switch (_currentMode)
                {
                    case 0: // Select
                        _selectedVertexIndex = GetVertexAt(transformedLocation);
                        panelDraw.Invalidate();
                        break;
                    case 1: // Add Vertex
                        AddVertex(transformedLocation);
                        break;
                    case 2: // Add Edge
                        _startVertex = _vertices.FirstOrDefault(v => IsPointOnVertex(transformedLocation, v.Location, VertexRadius));
                        break;
                    case 3: // Delete
                        DeleteVertexOrEdge(transformedLocation);
                        break;
                }
            }
        }

        private void panelDraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isDraggingMap = false;
            }
            else if (_currentMode == 2 && _startVertex != null)
            {
                var endVertex = _vertices.FirstOrDefault(v => IsPointOnVertex(TransformMouseCoordinates(e.Location), v.Location, VertexRadius));
                if (endVertex != null && _startVertex != endVertex)
                {
                    CreateEdge(endVertex);
                }
                _startVertex = null;
                UpdateMatrices();
                panelDraw.Invalidate();
            }
        }

        private void panelDraw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var transformedLocation = TransformMouseCoordinates(e.Location);
            int clickedVertexIndex = GetVertexAt(transformedLocation);

            if (clickedVertexIndex != -1)
            {
                EditVertex(clickedVertexIndex);
            }
            else
            {
                EditEdge(transformedLocation);
            }
        }

        #endregion

        #region Graph Manipulation

        private void AddVertex(PointF location)
        {
            string vertexValue;
            do
            {
                vertexValue = Prompt.ShowDialog("Enter vertex name:", "Add Vertex");
                if (string.IsNullOrEmpty(vertexValue)) return;
                if (string.IsNullOrWhiteSpace(vertexValue))
                {
                    MessageBox.Show("Vertex name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_vertices.Any(v => v.Value == vertexValue))
                {
                    MessageBox.Show("Vertex name already exists. Please enter a different name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    break;
                }
            } while (true);

            _vertices.Add(new Vertex(location, vertexValue));
            _matrixBlock.SetVertexValue(_vertices.Count - 1, vertexValue);
            UpdateMatrices();
            panelDraw.Invalidate();
            ShowOrHideMatrixBlock();
        }

        private void EditVertex(int vertexIndex)
        {
            string? newValue = Prompt.ChangeValueDialog("Enter new vertex value:", "Edit Vertex", _vertices[vertexIndex].Value);
            if (string.IsNullOrEmpty(newValue)) return;

            _vertices[vertexIndex] = new Vertex(_vertices[vertexIndex].Location, newValue);
            _matrixBlock.SetVertexValue(vertexIndex, newValue);
            panelDraw.Invalidate();
            UpdateMatrices();
        }

        private void CreateEdge(Vertex endVertex)
        {
            int startVertexIndex = _vertices.IndexOf(_startVertex!);
            int endVertexIndex = _vertices.IndexOf(endVertex);

            if (_edges.Any(edge => (edge.Vertex1 == startVertexIndex && edge.Vertex2 == endVertexIndex) || (edge.Vertex1 == endVertexIndex && edge.Vertex2 == startVertexIndex)))
            {
                MessageBox.Show("An edge between these two vertices already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var addEdgeForm = new AddEdge(_vertices, startVertexIndex, endVertexIndex);
            if (addEdgeForm.ShowDialog() == DialogResult.OK)
            {
                _edges.Add(addEdgeForm.GetEdge());
                panelDraw.Invalidate();
            }
        }

        private void EditEdge(PointF clickLocation)
        {
            foreach (var edge in _edges)
            {
                var p1 = _vertices[edge.Vertex1].Location;
                var p2 = _vertices[edge.Vertex2].Location;
                if (!IsPointOnEdge(clickLocation, p1, p2) && !IsCursorNearWeight(clickLocation, p1, p2)) continue;

                var result = Prompt.ShowChangeValueDialog("Enter new edge value:", "Edit Edge", edge.Weight.ToString());
                if (result == null) break;

                if (int.TryParse(result.Value.Item1, out int newWeight))
                {
                    var newEdge = new Edge(edge.Vertex1, edge.Vertex2, newWeight, result.Value.Item2);
                    _edges.Remove(edge);
                    _edges.Add(newEdge);
                    panelDraw.Invalidate();
                    UpdateMatrices();
                }
                else
                {
                    MessageBox.Show("Invalid weight value. Please enter a number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                break;
            }
        }

        private void DeleteVertexOrEdge(PointF clickLocation)
        {
            int clickedVertexIndex = GetVertexAt(clickLocation);
            if (clickedVertexIndex != -1)
            {
                _vertices.RemoveAt(clickedVertexIndex);
                _edges.RemoveAll(edge => edge.Vertex1 == clickedVertexIndex || edge.Vertex2 == clickedVertexIndex);
                foreach (var edge in _edges)
                {
                    if (edge.Vertex1 > clickedVertexIndex) _edges[_edges.IndexOf(edge)] = new Edge(edge.Vertex1 - 1, edge.Vertex2, edge.Weight, edge.IsDirected);
                    if (edge.Vertex2 > clickedVertexIndex) _edges[_edges.IndexOf(edge)] = new Edge(edge.Vertex1, edge.Vertex2 - 1, edge.Weight, edge.IsDirected);
                }
            }
            else
            {
                _edges.RemoveAll(edge => IsPointOnEdge(clickLocation, _vertices[edge.Vertex1].Location, _vertices[edge.Vertex2].Location) || IsCursorNearWeight(clickLocation, _vertices[edge.Vertex1].Location, _vertices[edge.Vertex2].Location));
            }
            UpdateMatrices();
            panelDraw.Invalidate();
        }

        #endregion

        #region Drawing

        private void DrawVertices(Graphics g)
        {
            foreach (var vertex in _vertices)
            {
                var vertexBrush = _vertices.IndexOf(vertex) == _selectedVertexIndex ? _selectedVertexBrush : _vertexBrush;
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
                if (edge.Vertex1 == edge.Vertex2)
                {
                    DrawSelfLoop(g, p1, edge.Weight, edge.IsDirected, _edgePen);
                }
                else
                {
                    DrawArrow(g, p1, p2, edge.Weight, edge.IsDirected, _edgePen);
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

        #endregion

        #region Helper Methods

        private int GetVertexAt(PointF location)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                if (IsPointOnVertex(location, _vertices[i].Location, VertexRadius))
                {
                    return i;
                }
            }
            return -1;
        }

        private static bool IsPointOnVertex(PointF point, PointF circleCenter, int radius) =>
            Math.Pow(point.X - circleCenter.X, 2) + Math.Pow(point.Y - circleCenter.Y, 2) <= Math.Pow(radius, 2);

        private bool IsPointOnEdge(PointF point, PointF p1, PointF p2)
        {
            const double tolerance = 3.0;
            double distance = DistancePointToLineSegment(point, p1, p2);
            return distance <= tolerance;
        }

        private bool IsCursorOnEdge(PointF cursorLocation) =>
            _edges.Any(edge => IsPointOnEdge(cursorLocation, _vertices[edge.Vertex1].Location, _vertices[edge.Vertex2].Location));

        private static bool IsCursorNearWeight(PointF cursorLocation, PointF p1, PointF p2)
        {
            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;
            float dx = cursorLocation.X - midX;
            float dy = cursorLocation.Y - midY;
            return Math.Sqrt(dx * dx + dy * dy) <= 16.0f;
        }

        private PointF TransformMouseCoordinates(PointF mouseLocation) =>
            new((mouseLocation.X - _panOffset.X) / _zoomLevel, (mouseLocation.Y - _panOffset.Y) / _zoomLevel);

        private static double DistancePointToLineSegment(PointF point, PointF p1, PointF p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if (dx == 0 && dy == 0) return Math.Sqrt(Math.Pow(point.X - p1.X, 2) + Math.Pow(point.Y - p1.Y, 2));

            float t = ((point.X - p1.X) * dx + (point.Y - p1.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Max(0, Math.Min(1, t));

            return Math.Sqrt(Math.Pow(point.X - (p1.X + t * dx), 2) + Math.Pow(point.Y - (p1.Y + t * dy), 2));
        }

        #endregion

        #region Algorithms

        private void DFS_Click(object sender, EventArgs e)
        {
            if (cbStartVertex.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a starting vertex for DFS.");
                return;
            }

            var startVertexIndex = _vertices.FindIndex(v => v.Value == cbStartVertex.SelectedItem.ToString());
            var connectivity = new GraphConnectivity(_vertices, _edges);
            if (!connectivity.IsConnected())
            {
                MessageBox.Show("The graph is not connected.");
                return;
            }

            var dfs = new DFS(_adjacencyMatrix!);
            var result = dfs.Perform(startVertexIndex);
            var dfsPath = result.Select(vertexIndex => _vertices[vertexIndex].Location).ToList();

            _movingBall.EdgePath = dfsPath;
            _movingBall.Start();
            MessageBox.Show($"DFS traversal starting from {_vertices[startVertexIndex].Value}: \n" + string.Join(" -> ", result.Select(v => _vertices[v].Value)));
            btnClearMovingBall.PerformClick();
        }

        private void BFS_Click(object sender, EventArgs e)
        {
            if (cbStartVertex.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a starting vertex for BFS.");
                return;
            }

            var startVertexIndex = _vertices.FindIndex(v => v.Value == cbStartVertex.SelectedItem.ToString());
            var connectivity = new GraphConnectivity(_vertices, _edges);
            if (!connectivity.IsConnected())
            {
                MessageBox.Show("The graph is not connected.");
                return;
            }

            var bfs = new BFS(_adjacencyMatrix!, _vertices);
            var (result, _) = bfs.Perform(startVertexIndex);
            var bfsPath = result.Select(vertexIndex => _vertices[vertexIndex].Location).ToList();

            _movingBall.EdgePath = bfsPath;
            _movingBall.Start();
            MessageBox.Show($"BFS traversal starting from {_vertices[startVertexIndex].Value}: \n" + string.Join(" -> ", result.Select(v => _vertices[v].Value)));
            btnClearMovingBall.PerformClick();
        }

        private void ChinesePostman_Click(object sender, EventArgs e)
        {
            if (cbStartVertex.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a starting vertex for the Chinese Postman algorithm.");
                return;
            }

            var startVertex = _vertices.First(v => v.Value == cbStartVertex.SelectedItem.ToString());
            var connectivity = new GraphConnectivity(_vertices, _edges);
            if (!connectivity.IsConnected())
            {
                MessageBox.Show("The graph is not connected.");
                return;
            }

            var postman = new ChinesePostman(_vertices, new List<Edge>(_edges));
            var (pairingMessage, cycleMessage) = postman.Solve(startVertex);

            MessageBox.Show(pairingMessage, "Optimal Pairings");
            var animator = new ChinesePostmanAnimation(postman, panelDraw);
            panelDraw.Paint += animator.OnPaint;
            animator.StartAnimation();
            MessageBox.Show(cycleMessage, "Eulerian Cycle");
            panelDraw.Paint -= animator.OnPaint;
            panelDraw.Invalidate();
        }

        private void Kruskal_Click(object sender, EventArgs e)
        {
            var connectivity = new GraphConnectivity(_vertices, _edges);
            if (!connectivity.IsConnected())
            {
                MessageBox.Show("The graph is not connected.");
                return;
            }

            var kruskal = new Kruskal(_vertices.Count, _edges);
            var mst = kruskal.GetMinimumSpanningTree();

            if (mst.Count > 0)
            {
                panelDraw.Controls.Clear();
                var kruskalAnimation = new KruskalAnimation(_vertices, _edges, mst);
                panelDraw.Controls.Add(kruskalAnimation);
                kruskalAnimation.Dock = DockStyle.Fill;
                panelDraw.Invalidate();

                var sb = new StringBuilder();
                sb.AppendLine("Edges of the Minimum Spanning Tree:");
                foreach (var edge in mst)
                {
                    sb.AppendLine($"{_vertices[edge.Vertex1].Value} - {_vertices[edge.Vertex2].Value} (Weight: {edge.Weight})");
                }
                sb.AppendLine($"\nTotal weight of the MST: {mst.Sum(e => e.Weight)}");

                Prompt.ShowKruskalSteps("Kruskal Steps", mst, _vertices);
                MessageBox.Show(sb.ToString(), "Minimum Spanning Tree Information");

                kruskalAnimation.Dispose();
                ShowOrHideMatrixBlock();
                UpdateMatrices();
            }
            else
            {
                MessageBox.Show("No edges found in the Minimum Spanning Tree.", "Information");
            }
        }

        private void Prim_Click(object sender, EventArgs e)
        {
            if (cbStartVertex.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a starting vertex for Prim's algorithm.");
                return;
            }

            var startVertexIndex = _vertices.FindIndex(v => v.Value == cbStartVertex.SelectedItem.ToString());
            var connectivity = new GraphConnectivity(_vertices, _edges);
            if (!connectivity.IsConnected())
            {
                MessageBox.Show("The graph is not connected.");
                return;
            }

            var prim = new Prim(_vertices.Count, _edges);
            var mst = prim.GetMinimumSpanningTree(startVertexIndex);

            if (mst.Count > 0)
            {
                panelDraw.Controls.Clear();
                var primAnimation = new PrimAnimation(_vertices, _edges, mst);
                panelDraw.Controls.Add(primAnimation);
                primAnimation.Dock = DockStyle.Fill;
                panelDraw.Invalidate();

                var sb = new StringBuilder();
                sb.AppendLine("Edges of the Minimum Spanning Tree:");
                foreach (var edge in mst)
                {
                    sb.AppendLine($"{_vertices[edge.Vertex1].Value} - {_vertices[edge.Vertex2].Value} (Weight: {edge.Weight})");
                }
                sb.AppendLine($"\nTotal weight of the MST: {mst.Sum(e => e.Weight)}");

                MessageBox.Show(sb.ToString(), "Minimum Spanning Tree Information");

                primAnimation.Dispose();
                ShowOrHideMatrixBlock();
                UpdateMatrices();
            }
            else
            {
                MessageBox.Show("No edges found in the Minimum Spanning Tree.", "Information");
            }
        }

        private void Dijkstra_Click(object sender, EventArgs e)
        {
            if (cbStartVertex.SelectedIndex == -1 || cbEndVertex.SelectedIndex == -1)
            {
                MessageBox.Show("Please select both a start and end vertex.");
                return;
            }

            var startVertexIndex = _vertices.FindIndex(v => v.Value == cbStartVertex.SelectedItem.ToString());
            var endVertexIndex = _vertices.FindIndex(v => v.Value == cbEndVertex.SelectedItem.ToString());
            var connectivity = new GraphConnectivity(_vertices, _edges);
            if (!connectivity.IsConnected())
            {
                MessageBox.Show("The graph is not connected.");
                return;
            }

            var dijkstra = new Dijkstra(_edges, _vertices.Count);
            var (distances, parents, steps) = dijkstra.FindShortestPath(startVertexIndex);

            if (steps.Count > 0)
            {
                Prompt.ShowDijkstraSteps("Dijkstra Steps", distances, parents, steps.ToArray(), _vertices);

                var path = new List<string>();
                int totalWeight = 0;
                int current = endVertexIndex;
                while (current != startVertexIndex && current != -1)
                {
                    int parent = parents[current].FirstOrDefault(-1);
                    if (parent == -1) break;

                    var edge = _edges.First(e => (e.Vertex1 == current && e.Vertex2 == parent) || (e.Vertex1 == parent && e.Vertex2 == current));
                    totalWeight += edge.Weight;
                    path.Insert(0, $"{_vertices[parent].Value} -> {_vertices[current].Value} (Weight: {edge.Weight})");
                    current = parent;
                }

                var sb = new StringBuilder();
                sb.AppendLine($"Shortest path from {_vertices[startVertexIndex].Value} to {_vertices[endVertexIndex].Value}:");
                sb.AppendLine(string.Join("\n", path));
                sb.AppendLine($"Total weight: {totalWeight}");

                var dijkstraAnimation = new DijkstraAnimation(_vertices, _edges, steps, startVertexIndex);
                panelDraw.Controls.Add(dijkstraAnimation);
                dijkstraAnimation.Dock = DockStyle.Fill;
                MessageBox.Show(sb.ToString(), "Shortest Path Information");
                dijkstraAnimation.Dispose();
                ShowOrHideMatrixBlock();
                UpdateMatrices();
            }
            else
            {
                MessageBox.Show("No path found from the starting vertex.", "Information");
            }
        }

        #endregion
    }
}
