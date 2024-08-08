using Đồ_Thị.Class;
using Đồ_Thị.Compoments;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
namespace Đồ_Thị.uc
{
    public partial class MatrixShow : UserControl
    {
        private readonly MatrixBlock _matrixBlock = new();
        private readonly MovingBall _movingBall;
        private GraphData _graphData;
        private List<Vertex> _vertices = [];
        private List<Edge> _edges = [];
        private int[,]? _adjacencyMatrix;
        private int[,]? _weightMatrix;
        private int _currentMode = 0;
        private int _selectedVertexIndex = -1;
        private Vertex startDinh = null;
        private PointF currentMousePosition;
        // Vertex and edge settings
        private const int _vertexRadius = 14;
        private readonly Brush _vertexColor = Brushes.LightSalmon;
        private readonly Brush _selectedVertexColor = Brushes.Yellow;
        private readonly Pen _defaultVertexOutlineColor = Pens.Red;

        // Edge settings
        private static readonly Brush _edgeWeightColor = Brushes.DarkOrange;
        private static readonly Brush _edgeWeightOutlineColor = Brushes.Black;
        private readonly Pen _edgeLineColor = new(Color.MediumPurple, 5);
        private const int _arrowSize = 18;
        private static readonly Brush _arrowColor = Brushes.MediumPurple;
        private const int _vertexOffset = 0;

        // Background color
        private readonly Color _background = Color.LightGray;

        //Map
        private Point _initialMousePosition;
        private bool _isDraggingMap = false;

        // Test zone
        private float _zoomLevel = 1.0f;
        private PointF _panOffset = new(0, 0);

        private List<Edge> _additionalEllipseEdges;

        public MatrixShow()
        {
            InitializeComponent();
            SetDoubleBufferedPanel();
            ResizeMatrixBlock();
            UpdateButtonStates();
            _movingBall = new MovingBall(paneldraw);
            paneldraw.MouseWheel += new MouseEventHandler(paneldraw_MouseWheel);
            txtZoom.Text = _zoomLevel.ToString();

        }

        private void SetDoubleBufferedPanel()
        {
            _ = typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, paneldraw, [true]);
            this.ResizeRedraw = true;
        }

        private void ResizeMatrixBlock()
        {
            _matrixBlock.Location = new Point(10, 10);
            _matrixBlock.Size = new Size(100, 100);
        }

        private void ShowOrHideMatrixBlock()
        {
            if (_vertices.Count > 0)
            {
                paneldraw.Controls.Add(_matrixBlock);
            }
            else
            {
                paneldraw.Controls.Clear();
            }
            paneldraw.Invalidate();
        }
        #region Button
        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            _currentMode = 1;
            UpdateButtonStates();
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            _currentMode = 2;
            UpdateButtonStates();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _currentMode = 0;
            UpdateButtonStates();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _currentMode = 3;
            UpdateButtonStates();
        }

        private void btn_ClearMovingBall_Click(object sender, EventArgs e)
        {
            _movingBall.EdgePath = [new Point(-1000, -1000)];
            _movingBall.Stop();
            paneldraw.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void UpdateButtonStates()
        {
            btn_select.Enabled = _currentMode != 0;
            btn_ThemDinh.Enabled = _currentMode != 1;
            btn_ThemCanh.Enabled = _currentMode != 2;
            btn_Xoa.Enabled = _currentMode != 3;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            drTim.Show(btn_SearchMenu, btn_SearchMenu.Width, 0);
        }

        private void btn_SaveGraph_Click(object sender, EventArgs e)
        {
            drMenuGraph.Show(btn_Graph, btn_Graph.Width, 0);
        }
        #endregion
        private void rbMatrixType_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_WeightMatrix.Checked)
            {
                _matrixBlock.MatrixType2 = MatrixBlock.MatrixType.Weight;
            }
            else if (radio_AdjMatrix.Checked)
            {
                _matrixBlock.MatrixType2 = MatrixBlock.MatrixType.Adjacency;
            }
        }
        private void ClearData()
        {
            _vertices.Clear();
            _edges.Clear();
            _additionalEllipseEdges.Clear();
            _adjacencyMatrix = new int[0, 0];
            _weightMatrix = new int[0, 0];
            _matrixBlock.AdjacencyMatrix = _adjacencyMatrix;
            _matrixBlock.WeightMatrix = _weightMatrix;
            paneldraw.Invalidate();
            ShowOrHideMatrixBlock();
        }
        private void UpdateMatrix()
        {
            int size = _vertices.Count;
            _adjacencyMatrix = new int[size, size];
            _weightMatrix = new int[size, size];

            foreach (Edge edge in _edges)
            {
                UpdateMatricesForEdge(edge);
            }

            _matrixBlock.AdjacencyMatrix = _adjacencyMatrix;
            _matrixBlock.WeightMatrix = _weightMatrix;

            for (int i = 0; i < _vertices.Count; i++)
            {
                _matrixBlock.SetVertexValue(i, _vertices[i].Value);
            }

            PopulateComboBox();
        }

        private void UpdateMatricesForEdge(Edge edge)
        {
            _adjacencyMatrix[edge.Vertex1, edge.Vertex2] = 1;
            _weightMatrix[edge.Vertex1, edge.Vertex2] = edge.Weight;

            if (!edge.IsDirected)
            {
                _adjacencyMatrix[edge.Vertex2, edge.Vertex1] = 1;
                _weightMatrix[edge.Vertex2, edge.Vertex1] = edge.Weight;
            }
        }
        private void PopulateComboBox()
        {
            cb_First.Items.Clear();
            cb_Second.Items.Clear();

            foreach (Vertex vertex in _vertices)
            {
                cb_First.Items.Add(vertex.Value);
                cb_Second.Items.Add(vertex.Value);
            }
        }

        private void NodeCount()
        {
            txtNodeCount.Text = _vertices.Count.ToString();
        }

        private void paneldraw_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(_background);
            g.TranslateTransform(_panOffset.X, _panOffset.Y);
            g.ScaleTransform(_zoomLevel, _zoomLevel);
            DrawEdges(g);
            if (_additionalEllipseEdges != null && _vertices.Count > 0)
            {
                foreach (var edges in _additionalEllipseEdges)
                {
                    DrawEllipseEdge(g, edges);
                }
            }
            DrawVertices(g);

            if (_vertices.Count > 0 && _edges.Count > 0)
                _movingBall.Draw(g);

            if (_currentMode == 2 && startDinh != null)
            {
                g.DrawLine(Pens.Gray, startDinh.Location, currentMousePosition);
            }
        }


        private void paneldraw_MouseWheel(object sender, MouseEventArgs e)
        {
            AdjustZoom(e);
            paneldraw.Invalidate();
        }
        private void AdjustZoom(MouseEventArgs e)
        {
            float zoomFactor = e.Delta < 0 ? 1.0f / 1.1f : 1.1f;

            _zoomLevel *= zoomFactor;
            Point mousePos = e.Location;
            _panOffset.X = mousePos.X - (mousePos.X - _panOffset.X) * zoomFactor;
            _panOffset.Y = mousePos.Y - (mousePos.Y - _panOffset.Y) * zoomFactor;
            txtZoom.Text = _zoomLevel.ToString();
        }

        private void paneldraw_MouseMove(object sender, MouseEventArgs e)
        {
            PointF transformedLocation = TransformMouseCoordinates(e.Location);
            UpdateCursor(transformedLocation);

            if (e.Button == MouseButtons.Left && IsCursorOnVertex(transformedLocation) != -1 && _currentMode == 0)
            {
                MoveVertex(transformedLocation);
            }
            else if (e.Button == MouseButtons.Right && _isDraggingMap)
            {
                DragMap(e);
            }

            if (_currentMode == 2 && startDinh != null)
            {
                currentMousePosition = transformedLocation;
                paneldraw.Invalidate();
            }

            NodeCount();
        }
        private void UpdateCursor(PointF location)
        {
            paneldraw.Cursor = IsCursorOnVertex(location) != -1 || IsCursorOnEdge(location) ? Cursors.Hand : Cursors.Default;
        }

        private void MoveVertex(PointF location)
        {
            _vertices[IsCursorOnVertex(location)].Location = location;
            paneldraw.Invalidate();
        }

        private void DragMap(MouseEventArgs e)
        {
            float xOffset = (e.X - _initialMousePosition.X) / _zoomLevel;
            float yOffset = (e.Y - _initialMousePosition.Y) / _zoomLevel;
            _panOffset = new PointF(_panOffset.X + xOffset, _panOffset.Y + yOffset);
            _initialMousePosition = e.Location;
            paneldraw.Invalidate();
        }
        private void paneldraw_MouseDown(object sender, MouseEventArgs e)
        {
            PointF transformedLocation = TransformMouseCoordinates(e.Location);

            if (e.Button == MouseButtons.Right)
            {
                StartDragging(e);
            }
            else if (e.Button == MouseButtons.Left)
            {
                HandleLeftMouseDown(e, transformedLocation);
            }
        }
        private void StartDragging(MouseEventArgs e)
        {
            _initialMousePosition = e.Location;
            _isDraggingMap = true;
        }

        private void HandleLeftMouseDown(MouseEventArgs e, PointF transformedLocation)
        {
            switch (_currentMode)
            {
                case 0:
                    HandleSelectVertex(e);
                    break;
                case 1:
                    AddVertex(e);
                    break;
                case 2:
                    startDinh = _vertices.FirstOrDefault(d => Distance(d.Location, transformedLocation) < 10);
                    break;
                case 3:
                    RemoveVertexOrEdge(e.Location);
                    break;
            }
        }
        private double Distance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        private void paneldraw_MouseUp(object sender, MouseEventArgs e)
        {
            PointF transformedLocation = TransformMouseCoordinates(e.Location);

            if (e.Button == MouseButtons.Right && _currentMode == 0)
                _isDraggingMap = false;

            paneldraw.Invalidate();

            if (_currentMode == 2 && startDinh != null)
            {
                HandleEdgeCreation(transformedLocation);
            }
        }

        private void HandleEdgeCreation(PointF location)
        {
            Vertex endDinh = _vertices.FirstOrDefault(d => Distance(d.Location, location) < 10);

            if (endDinh != null && startDinh != endDinh)
            {
                CreateEdge(endDinh);
            }

            startDinh = null;
            UpdateMatrix();
        }

        private void CreateEdge(Vertex endDinh)
        {
            int dinh1Index = _vertices.IndexOf(startDinh);
            int dinh2Index = _vertices.IndexOf(endDinh);
            bool edgeExists = _edges.Any(edge => (edge.Vertex1 == dinh1Index && edge.Vertex2 == dinh2Index) || (edge.Vertex1 == dinh2Index && edge.Vertex2 == dinh1Index));

            if (edgeExists)
            {
                MessageBox.Show("Đã tồn tại cạnh giữa hai đỉnh này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ThemCanh formThemCanh = new(_vertices, dinh1Index, dinh2Index);
                if (formThemCanh.ShowDialog() == DialogResult.OK)
                {
                    Edge newCanh = formThemCanh.GetEdge();
                    _edges.Add(newCanh);
                    paneldraw.Invalidate();
                }
            }
        }


        private void paneldraw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointF transformedLocation = TransformMouseCoordinates(e.Location);
                int clickedVertexIndex = IsCursorOnVertex(transformedLocation);

                if (clickedVertexIndex != -1)
                {
                    HandleVertexDoubleClick(clickedVertexIndex);
                }
                else
                {
                    HandleEdgeDoubleClick(transformedLocation);
                }
            }
        }


        private void HandleVertexDoubleClick(int clickedVertexIndex)
        {
            string? newValue = Prompt.ChangeValueDialog("Nhập giá trị mới cho đỉnh", "Sửa đỉnh", _vertices[clickedVertexIndex].Value);

            if (!string.IsNullOrEmpty(newValue))
            {
                _vertices[clickedVertexIndex].Value = newValue;
                _matrixBlock.SetVertexValue(clickedVertexIndex, newValue);
                paneldraw.Invalidate();
                UpdateMatrix();
                ShowOrHideMatrixBlock();
            }
        }


        private void HandleEdgeDoubleClick(PointF clickLocation)
        {
            foreach (Edge edge in _edges)
            {
                PointF p1 = _vertices[edge.Vertex1].Location;
                PointF p2 = _vertices[edge.Vertex2].Location;

                if (IsPointOnEdge(clickLocation, p1, p2) || IsCursorNearWeight(clickLocation, p1, p2))
                {
                    HandleEdgeWeightChange(edge);
                    break;
                }
            }
        }

        private void HandleEdgeWeightChange(Edge edge)
        {
            while (true)
            {
                (string, bool)? result = Prompt.ChangeValueDialog("Nhập giá trị mới cho cạnh", "Sửa cạnh", edge.Weight.ToString(), edge.IsDirected);

                if (result == null)
                {
                    break;
                }

                if (TryUpdateEdgeWeight(edge, result.Value.Item1, result.Value.Item2))
                {
                    break;
                }
                else
                {
                    MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập lại.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        private bool TryUpdateEdgeWeight(Edge edge, string newWeightStr, bool isDirected)
        {
            if (int.TryParse(newWeightStr, out int newWeight))
            {
                edge.Weight = newWeight;
                edge.IsDirected = isDirected;

                // If the edge is not directed, remove any reverse edge
                if (!edge.IsDirected)
                {
                    _edges.RemoveAll(e => e.Vertex1 == edge.Vertex2 && e.Vertex2 == edge.Vertex1);
                }
                else
                {
                    // For directed edges, ensure reverse edge exists and update its weight if necessary
                    Edge reverseEdge = _edges.FirstOrDefault(e => e.Vertex1 == edge.Vertex2 && e.Vertex2 == edge.Vertex1);
                    if (reverseEdge != null)
                    {
                        reverseEdge.Weight = newWeight;
                        reverseEdge.IsDirected = isDirected;
                    }
                }

                paneldraw.Invalidate();
                UpdateMatrix();
                return true;
            }
            return false;
        }

        private PointF TransformMouseCoordinates(PointF mouseLocation)
        {
            return new PointF(
                (mouseLocation.X - _panOffset.X) / _zoomLevel,
                (mouseLocation.Y - _panOffset.Y) / _zoomLevel
            );
        }
        private void HandleSelectVertex(MouseEventArgs e)
        {
            PointF transformedLocation = TransformMouseCoordinates(e.Location);
            _selectedVertexIndex = IsCursorOnVertex(transformedLocation);
            paneldraw.Invalidate();
        }

        private int IsCursorOnVertex(PointF location)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                if (IsPointOnVertex(location, _vertices[i].Location, _vertexRadius))
                {
                    return i;
                }
            }
            return -1;
        }


        private static bool IsPointOnVertex(PointF point, PointF circleCenter, int radius)
        {
            return (Math.Pow(point.X - circleCenter.X, 2) + Math.Pow(point.Y - circleCenter.Y, 2)) <= Math.Pow(radius, 2);
        }

        private bool IsPointOnEdge(PointF point, PointF p1, PointF p2)
        {
            const double tolerance = 3.0; // Adjust this value as needed
            double distance = DistancePointToLineSegment(point, p1, p2);
            return distance <= tolerance;
        }

        private void AddVertex(MouseEventArgs e)
        {
            // Adjust for pan offset and zoom level
            float zoomFactor = _zoomLevel;


            // Calculate mouse position in the zoomed and panned coordinate system
            int x = (int)((e.X - _panOffset.X) / zoomFactor);
            int y = (int)((e.Y - _panOffset.Y) / zoomFactor);

            // Ensure x and y are within bounds
            x = Math.Clamp(x, _vertexRadius, paneldraw.Width - _vertexRadius);
            y = Math.Clamp(y, _vertexRadius, paneldraw.Height - _vertexRadius);

            string vertexValue;
            bool isValidInput = false;
            do
            {
                vertexValue = Prompt.ShowDialog("Nhập tên đỉnh:", "Thêm đỉnh");

                if (vertexValue == "")
                    return;

                if (string.IsNullOrWhiteSpace(vertexValue))
                {
                    _ = MessageBox.Show("Tên Đỉnh Không Được Trống.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else if (_vertices.Any(v => v.Value == vertexValue))
                {
                    _ = MessageBox.Show("Tên đỉnh đã tồn tại. Vui lòng nhập tên khác.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    isValidInput = true;
                }

            } while (!isValidInput);

            _vertices.Add(new Vertex(new Point(x, y), vertexValue));
            _matrixBlock.SetVertexValue(_vertices.Count - 1, vertexValue);
            UpdateMatrix();
            paneldraw.Invalidate();
            ShowOrHideMatrixBlock();
        }
        private void RemoveVertexOrEdge(PointF clickLocation)
        {
            PointF transformedLocation = TransformMouseCoordinates(clickLocation);
            int clickedVertexIndex = IsCursorOnVertex(transformedLocation);
            if (clickedVertexIndex != -1)
            {
                RemoveVertex(clickedVertexIndex);
                return;
            }
            RemoveEdge(transformedLocation);
            UpdateMatrix();
            paneldraw.Invalidate();
        }
        private void RemoveVertex(int vertexIndex)
        {
            _vertices.RemoveAt(vertexIndex);
            _edges.RemoveAll(edge => edge.Vertex1 == vertexIndex || edge.Vertex2 == vertexIndex);

            // Update edge indices
            foreach (Edge edge in _edges)
            {
                if (edge.Vertex1 > vertexIndex)
                {
                    edge.Vertex1--;
                }
                if (edge.Vertex2 > vertexIndex)
                {
                    edge.Vertex2--;
                }
            }

            UpdateMatrix();
            paneldraw.Invalidate();
        }
        private void RemoveEdge(PointF clickLocation)
        {
            List<Edge> edgesToRemove = _edges.Where(edge =>
            {
                PointF p1 = _vertices[edge.Vertex1].Location;
                PointF p2 = _vertices[edge.Vertex2].Location;
                return IsPointOnEdge(clickLocation, p1, p2) || IsCursorNearWeight(clickLocation, p1, p2);
            }).ToList();

            if (edgesToRemove.Count > 0)
            {
                foreach (Edge edge in edgesToRemove)
                {
                    _edges.Remove(edge);
                }
            }

            UpdateMatrix();
            paneldraw.Invalidate();
        }
        private bool IsCursorOnEdge(PointF cursorLocation)
        {
            const double tolerance = 5.0; // Adjust this value as needed
            foreach (Edge edge in _edges)
            {
                PointF p1 = _vertices[edge.Vertex1].Location;
                PointF p2 = _vertices[edge.Vertex2].Location;
                double distance = DistancePointToLineSegment(cursorLocation, p1, p2);

                if (distance < tolerance)
                {
                    return true;
                }
            }
            return false;
        }


        private static bool IsCursorNearWeight(PointF cursorLocation, PointF p1, PointF p2)
        {
            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;

            float dx = cursorLocation.X - midX;
            float dy = cursorLocation.Y - midY;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            const float selectionRadius = 16.0f;
            return distance <= selectionRadius;
        }

        private void DrawVertices(Graphics g)
        {
            foreach (Vertex vertex in _vertices)
            {
                Brush vertexBrush = _vertices.IndexOf(vertex) == _selectedVertexIndex ? _selectedVertexColor : _vertexColor;
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
                if (edge.Vertex1 == edge.Vertex2)
                {
                    DrawSelfLoop(g, p1, edge.Weight, edge.IsDirected); // Sử dụng màu đen cho các cạnh chính
                }
                else
                {
                    DrawArrow(g, p1, p2, edge.Weight, edge.IsDirected); // Sử dụng màu đen cho các cạnh chính
                }
            }
        }

        private void DrawArrow(Graphics g, PointF p1, PointF p2, int weight, bool isDirected)
        {
            float angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            PointF startPoint = new(p2.X - (_vertexRadius + _vertexOffset) * (float)Math.Cos(angle),
                                    p2.Y - (_vertexRadius + _vertexOffset) * (float)Math.Sin(angle));

            g.DrawLine(_edgeLineColor, p1, startPoint);

            if (isDirected)
            {
                DrawArrowHead(g, startPoint, angle);
            }

            DrawEdgeWeight(g, p1, p2, weight);
        }
        private static void DrawArrowHead(Graphics g, PointF startPoint, float angle)
        {
            PointF[] arrowHeadPoints =
            {
                new(startPoint.X, startPoint.Y),
                new(startPoint.X - _arrowSize * (float)Math.Cos(angle - Math.PI / 6), startPoint.Y - _arrowSize * (float)Math.Sin(angle - Math.PI / 6)),
                new(startPoint.X - _arrowSize * (float)Math.Cos(angle + Math.PI / 6), startPoint.Y - _arrowSize * (float)Math.Sin(angle + Math.PI / 6))
            };
            g.FillPolygon(_arrowColor, arrowHeadPoints);
        }

        private static void DrawEdgeWeight(Graphics g, PointF p1, PointF p2, int weight)
        {
            if (weight == 0) return;

            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;
            RectangleF squareRect = new(midX - 9, midY - 9, 18, 18);

            g.FillRectangle(_edgeWeightOutlineColor, squareRect);
            g.FillRectangle(_edgeWeightColor, squareRect);

            using Font font = new("Arial", 12);
            SizeF textSize = g.MeasureString(weight.ToString(), font);
            PointF textLocation = new(midX - textSize.Width / 2, midY - textSize.Height / 2);
            g.DrawString(weight.ToString(), font, Brushes.Black, textLocation);
        }

        private void DrawSelfLoop(Graphics g, PointF location, int weight, bool isDirected)
        {
            float radius = 20;
            Rectangle selfLoopRect = new((int)(location.X - radius), (int)(location.Y - radius), (int)(2 * radius), (int)(2 * radius));
            g.DrawEllipse(_edgeLineColor, selfLoopRect);
            if (isDirected)
            {
                DrawSelfLoopArrow(g, location, radius);
            }

            using Font font = new("Arial", 10);
            PointF weightLocation = new(location.X + radius, location.Y - radius);
            g.DrawString(weight.ToString(), font, Brushes.Black, weightLocation);
        }

        private static void DrawSelfLoopArrow(Graphics g, PointF location, float radius)
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
        private double DistancePointToLineSegment(PointF point, PointF p1, PointF p2)
        {
            float A = point.X - p1.X;
            float B = point.Y - p1.Y;
            float C = p2.X - p1.X;
            float D = p2.Y - p1.Y;

            float dot = A * C + B * D;
            float lenSq = C * C + D * D;
            float param = (lenSq != 0) ? dot / lenSq : -1;

            float xx, yy;

            if (param < 0)
            {
                xx = p1.X;
                yy = p1.Y;
            }
            else if (param > 1)
            {
                xx = p2.X;
                yy = p2.Y;
            }
            else
            {
                xx = p1.X + param * C;
                yy = p1.Y + param * D;
            }

            float dx = point.X - xx;
            float dy = point.Y - yy;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public void DrawEllipseEdge(Graphics graphics, Edge edge)
        {
            // Tính toán điểm bắt đầu và điểm kết thúc của ellipse
            PointF start = _vertices[edge.Vertex1].Location;
            PointF end = _vertices[edge.Vertex2].Location;
            // Tính toán điểm điều khiển của đường cong Bézier
            PointF control1 = new PointF(start.X + (end.X - start.X) / 3, start.Y - (end.Y - start.Y) / 3);
            PointF control2 = new PointF(end.X - (end.X - start.X) / 3, end.Y + (end.Y - start.Y) / 3);

            // Vẽ đường cong
            using (Pen pen = new Pen(Color.Black, 2)) // Bạn có thể thay đổi màu và độ dày của đường viền
            {
                graphics.DrawBezier(pen, start, control1, control2, end);
            }
        }
        #region Save and Load Graph
        private void saveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _graphData = new GraphData
            {
                Vertices = _vertices,
                Edges = _edges,
                AdjacencyMatrix = _adjacencyMatrix,
                WeightMatrix = _weightMatrix
            };
            _graphData.SaveGraph();
        }

        private void loadGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearData();
            GraphData loadedGraph = GraphData.OpenGraphFile();

            if (loadedGraph != null)
            {
                _ = MessageBox.Show("Load Thành Công.");
                _vertices = loadedGraph.Vertices;
                _edges = loadedGraph.Edges;
                _adjacencyMatrix = loadedGraph.AdjacencyMatrix;
                _weightMatrix = loadedGraph.WeightMatrix;
            }
            paneldraw.Invalidate();
            ShowOrHideMatrixBlock();
            UpdateMatrix();
        }
        #endregion

        #region Nút kích hoạt thuật toán
        private void DFS_Click(object sender, EventArgs e)
        {
            if (cb_First != null && cb_First.SelectedIndex != -1)
            {
                string? selectedVertexValue = cb_First.SelectedItem as string;
                int startVertexIndex = _vertices.FindIndex(v => v.Value == selectedVertexValue);
                var lienthong = new Lienthong(_vertices, _edges);
                bool CheckLienThong = lienthong.CheckLienThong();
                if (!CheckLienThong)
                {
                    _ = MessageBox.Show("Đồ thị không liên thông");
                    return;
                }
                if (startVertexIndex != -1)
                {
                    DFS dfs = new(_adjacencyMatrix);
                    List<int> result = dfs.PerformDFS(startVertexIndex);
                    List<PointF> dfsPath = [];
                    foreach (int vertexIndex in result)
                    {
                        PointF vertexLocation = _vertices[vertexIndex].Location;
                        dfsPath.Add(vertexLocation);
                    }
                    if (dfsPath.Count > 0)
                        dfsPath.Add(dfsPath[^1]);
                    _movingBall.EdgePath = dfsPath;
                    _movingBall.Start();
                    _ = MessageBox.Show($"Duyệt DFS bắt đầu từ đỉnh {selectedVertexValue}: \n" + string.Join(" -> ", result.Select(v => _vertices[v].Value)));
                    btn_ClearMovingBall.PerformClick();
                    ShowOrHideMatrixBlock();
                    UpdateMatrix();
                }
                else
                {
                    _ = MessageBox.Show("Không tìm thấy đỉnh phù hợp. Vui lòng chọn một đỉnh khác.");
                }
            }
            else
            {
                _ = MessageBox.Show("Vui lòng chọn một đỉnh để bắt đầu duyệt DFS.");
                _ = cb_First!.Focus();
                paneldraw.Invalidate();
            }
        }

        private void BFS_Click(object sender, EventArgs e)
        {
            if (cb_First != null && cb_First.SelectedIndex != -1)
            {
                string? selectedVertexValue = cb_First.SelectedItem as string;
                int startVertexIndex = _vertices.FindIndex(v => v.Value == selectedVertexValue);
                var lienthong = new Lienthong(_vertices, _edges);
                bool CheckLienThong = lienthong.CheckLienThong();
                if (!CheckLienThong)
                {
                    _ = MessageBox.Show("Đồ thị không liên thông");
                    return;
                }
                if (startVertexIndex != -1)
                {
                    BFS bfs = new(_adjacencyMatrix, _vertices);
                    (List<int> result, List<string> adjacencyLog) = bfs.PerformBFS(startVertexIndex);
                    List<PointF> bfsPath = [];

                    foreach (int vertexIndex in result)
                    {
                        PointF vertexLocation = _vertices[vertexIndex].Location;
                        bfsPath.Add(vertexLocation);
                    }

                    if (bfsPath.Count > 0)
                        bfsPath.Add(bfsPath[bfsPath.Count - 1]);
                    _movingBall.EdgePath = bfsPath;
                    _movingBall.Start();
                    _ = MessageBox.Show($"Duyệt BFS bắt đầu từ đỉnh {selectedVertexValue}: \n" + string.Join(" -> ", result.Select(v => _vertices[v].Value)));
                    btn_ClearMovingBall.PerformClick();
                    ShowOrHideMatrixBlock();
                    UpdateMatrix();
                }
                else
                {
                    _ = MessageBox.Show("Không tìm thấy đỉnh phù hợp. Vui lòng chọn một đỉnh khác.");
                }
            }
            else
            {
                _ = MessageBox.Show("Vui lòng chọn một đỉnh để bắt đầu duyệt BFS.");
                _ = cb_First!.Focus();
                paneldraw.Invalidate();
            }
        }

        private void MailMan_Click(object sender, EventArgs e)
        {
            string? startVertex;
            Vertex startVertexClass;
            var oldedge = this._edges.ToList();
            if (cb_First != null && cb_First.SelectedIndex != -1)
            {
                startVertex = cb_First.SelectedItem as string;
                startVertexClass = _vertices.FirstOrDefault(v => v.Value == startVertex);
            }
            else
            {
                _ = MessageBox.Show("Vui lòng chọn một đỉnh để bắt đầu duyệt MailMan.");
                _ = cb_First!.Focus();
                paneldraw.Invalidate();
                return;
            }
            var lienthong = new Lienthong(_vertices, _edges);
            bool isConnected = lienthong.CheckLienThong();
            if (!isConnected)
            {
                MessageBox.Show("Đồ thị không liên thông");
                return;
            }

            var mailMan = new Mail(_vertices, _edges);
            string a, b;
            (a, b) = mailMan.SolveChinesePostmanProblem(startVertexClass);
            this._additionalEllipseEdges = mailMan.GetAddEdge();
            paneldraw.Invalidate();
            MessageBox.Show(a, "Kết quả các cặp tối ưu");
            MessageBox.Show(b, "Chu trình Eulerian");
            this._edges = [.. oldedge];
            paneldraw.Invalidate();
        }
        private void Kruskal_Click(object sender, EventArgs e)
        {
            var lienthong = new Lienthong(_vertices, _edges);
            bool CheckLienThong = lienthong.CheckLienThong();
            if (!CheckLienThong)
            {
                _ = MessageBox.Show("Đồ thị không liên thông");
                return;
            }
            Kruskal kruskalAlgorithm = new(_vertices, _edges);
            List<Edge> minimumSpanningTree = kruskalAlgorithm.GetMinimumSpanningTree();

            if (minimumSpanningTree.Count > 0)
            {
                foreach (Control control in paneldraw.Controls)
                {
                    if (control != _matrixBlock)
                    {
                        control.Dispose();
                    }
                }
                paneldraw.Controls.Clear();

                KruskalAnimation kruskalAnimation = new(_vertices, _edges, minimumSpanningTree);
                kruskalAnimation.SetTransform(_zoomLevel, _panOffset);
                paneldraw.Controls.Add(kruskalAnimation);
                kruskalAnimation.Dock = DockStyle.Fill;
                paneldraw.Invalidate();

                StringBuilder sb = new();
                _ = sb.AppendLine("Các cạnh của cây bao phủ nhỏ nhất:");
                HashSet<int> verticesInMST = [];

                foreach (Edge edge in minimumSpanningTree)
                {
                    string vertex1Value = _vertices[edge.Vertex1].Value;
                    string vertex2Value = _vertices[edge.Vertex2].Value;
                    _ = sb.AppendLine($"{vertex1Value} - {vertex2Value} (Trọng số: {edge.Weight})");

                    _ = verticesInMST.Add(edge.Vertex1);
                    _ = verticesInMST.Add(edge.Vertex2);
                }

                int totalWeight = minimumSpanningTree.Sum(e => e.Weight);
                _ = sb.AppendLine("\nTổng trọng số của cây bao phủ nhỏ nhất: " + totalWeight);
                Prompt.DisplayKruskalSteps("Cặp đỉnh kruskal", minimumSpanningTree, _vertices);
                _ = MessageBox.Show(sb.ToString(), "Thông tin cây bao phủ nhỏ nhất");
                kruskalAnimation.Dispose();
                ResizeMatrixBlock();
                ShowOrHideMatrixBlock();
                UpdateMatrix();
            }
            else
            {
                _ = MessageBox.Show("Không tìm thấy cạnh nào trong cây bao phủ nhỏ nhất.", "Thông báo");
            }
        }


        private void Prim_Click(object sender, EventArgs e)
        {
            if (cb_First != null && cb_First.SelectedIndex != -1)
            {
                string? startVertex = cb_First.SelectedItem as string;
                int startVertexIndex = _vertices.FindIndex(v => v.Value == startVertex);
                var lienthong = new Lienthong(_vertices, _edges);
                bool CheckLienThong = lienthong.CheckLienThong();
                if (!CheckLienThong)
                {
                    _ = MessageBox.Show("Đồ thị không liên thông");
                    return;
                }
                if (startVertexIndex != -1)
                {
                    Prim primAlgorithm = new(_vertices, _edges);
                    List<Edge> minimumSpanningTree = primAlgorithm.GetMinimumSpanningTree(startVertexIndex);
                    if (minimumSpanningTree.Count > 0)
                    {

                        foreach (Control control in paneldraw.Controls)
                        {
                            if (control != _matrixBlock)
                            {
                                control.Dispose();
                            }
                        }
                        paneldraw.Controls.Clear();

                        PrimAnimation _primAnimation = new(_vertices, _edges, minimumSpanningTree);
                        _primAnimation.SetTransform(_zoomLevel, _panOffset);
                        paneldraw.Controls.Add(_primAnimation);
                        _primAnimation.Dock = DockStyle.Fill;
                        paneldraw.Invalidate();
                        StringBuilder sb = new();
                        _ = sb.AppendLine("Các cạnh của cây bao phủ nhỏ nhất:");
                        HashSet<int> verticesInMST = [];
                        // Lấy cặp cạnh trong cây bao phủ nhỏ nhất và hiển thị
                        foreach (Edge edge in minimumSpanningTree)
                        {
                            string vertex1Value = _vertices[edge.Vertex1].Value;
                            string vertex2Value = _vertices[edge.Vertex2].Value;
                            _ = sb.AppendLine($"{vertex1Value} - {vertex2Value} (Trọng số: {edge.Weight})");

                            _ = verticesInMST.Add(edge.Vertex1);
                            _ = verticesInMST.Add(edge.Vertex2);
                        }
                        int totalWeight = minimumSpanningTree.Sum(e => e.Weight);
                        _ = sb.AppendLine("\nTổng trọng số của cây bao phủ nhỏ nhất: " + totalWeight);

                        Prompt.DisplayPrimSteps("Prim's Algorithm Steps", _edges, _vertices);

                        _ = MessageBox.Show(sb.ToString(), "Thông tin cây bao phủ nhỏ nhất");
                        _primAnimation.Dispose();
                        ResizeMatrixBlock();
                        ShowOrHideMatrixBlock();
                        UpdateMatrix();
                    }
                    else
                    {
                        _ = MessageBox.Show("Không tìm thấy cạnh nào trong cây bao phủ nhỏ nhất.", "Thông báo");
                    }
                }
                else
                {
                    _ = MessageBox.Show("Đỉnh bắt đầu không hợp lệ.", "Thông báo");
                }
            }
            else
            {
                _ = MessageBox.Show("Vui lòng chọn một đỉnh để bắt đầu thuật toán Prim.");
                _ = cb_First.Focus();
                paneldraw.Invalidate();
            }
        }

        private void Dijkstra_Click(object sender, EventArgs e)
        {
            if (cb_First.SelectedItem != null && cb_Second.SelectedItem != null)
            {
                string startVertexValue = cb_First.SelectedItem.ToString();
                string endVertexValue = cb_Second.SelectedItem.ToString();
                int startVertexIndex = _vertices.FindIndex(v => v.Value == startVertexValue);
                int endVertexIndex = _vertices.FindIndex(v => v.Value == endVertexValue);
                var lienthong = new Lienthong(_vertices, _edges);
                bool CheckLienThong = lienthong.CheckLienThong();
                if (!CheckLienThong)
                {
                    _ = MessageBox.Show("Đồ thị không liên thông");
                    return;
                }
                if (startVertexIndex != -1 && endVertexIndex != -1)
                {
                    Dijkstra dijkstraAlgorithm = new(_vertices, _edges, _vertices.Count);
                    (int[] distances, List<int>[] parents, List<int[]> steps) = dijkstraAlgorithm.DijkstraShortestPath(startVertexIndex);

                    if (steps.Count > 0)
                    {
                        Prompt.DisplayDijkstraSteps("Dijkstra", distances, parents, steps, _vertices);
                        foreach (Control control in paneldraw.Controls)
                        {
                            if (control != _matrixBlock)
                            {
                                control.Dispose();
                            }
                        }
                        paneldraw.Controls.Clear();
                        List<PointF> dijkstraPath = [];
                        int currentVertexIndex = endVertexIndex;
                        int totalWeight = 0;
                        List<string> pathWithWeights = [];
                        while (currentVertexIndex != startVertexIndex && currentVertexIndex != -1)
                        {
                            PointF vertexLocation = _vertices[currentVertexIndex].Location;
                            dijkstraPath.Add(vertexLocation);

                            int previousVertexIndex = parents[currentVertexIndex].FirstOrDefault();
                            if (previousVertexIndex != -1)
                            {
                                Edge? edge = _edges.FirstOrDefault(e => (e.Vertex1 == currentVertexIndex && e.Vertex2 == previousVertexIndex) ||
                                                                         (e.Vertex1 == previousVertexIndex && e.Vertex2 == currentVertexIndex));
                                if (edge != null)
                                {
                                    totalWeight += edge.Weight;
                                    pathWithWeights.Insert(0, $"{_vertices[previousVertexIndex].Value} --> {_vertices[currentVertexIndex].Value} (Trọng số: {edge.Weight})");
                                }
                            }

                            currentVertexIndex = previousVertexIndex;
                        }

                        dijkstraPath.Add(_vertices[startVertexIndex].Location);
                        dijkstraPath.Reverse();
                        StringBuilder sb = new();
                        _ = sb.AppendLine($"Đường đi ngắn nhất từ {startVertexValue} đến {endVertexValue}:");
                        _ = sb.AppendLine();
                        foreach (string path in pathWithWeights)
                        {
                            _ = sb.AppendLine(path);
                        }
                        _ = sb.AppendLine($"Tổng trọng số: {totalWeight}");
                        DijkstraAnimation dijkstraAnimation = new(_vertices, _edges, steps, startVertexIndex, endVertexIndex);
                        dijkstraAnimation.SetTransform(_zoomLevel, _panOffset);
                        paneldraw.Controls.Add(dijkstraAnimation);
                        dijkstraAnimation.Dock = DockStyle.Fill;
                        _ = MessageBox.Show(sb.ToString(), "Thông tin đường đi ngắn nhất");
                        paneldraw.Invalidate();
                        dijkstraAnimation.Dispose();
                        ResizeMatrixBlock();
                        ShowOrHideMatrixBlock();
                        UpdateMatrix();
                    }
                    else
                    {
                        _ = MessageBox.Show("Không tìm thấy đường đi nào từ đỉnh bắt đầu.", "Thông báo");
                    }
                }
                else
                {
                    _ = MessageBox.Show("Không tìm thấy đỉnh phù hợp. Vui lòng chọn một đỉnh khác.", "Thông báo");
                }
            }
            else
            {
                _ = MessageBox.Show("Vui lòng chọn đỉnh xuất phát và đỉnh đích để tìm đường đi ngắn nhất.", "Thông báo");
            }
        }

        #endregion

        private void btn_totxtmatrix_Click(object sender, EventArgs e)
        {
            if (_vertices.Count <= 1)
            {
                _ = MessageBox.Show("Đồ thị phải có ít nhất 2 đỉnh.", "Thông báo");
                return;
            }
            bool a;
            int[,] b;
            Thisisonlytest thisisonlytest = new(_vertices, _edges);
            if (radio_AdjMatrix.Checked)
            {
                a = true;
                if (_adjacencyMatrix == null)
                    return;
                b = _adjacencyMatrix;
            }
            else
            {
                a = false;
                if (_weightMatrix == null)
                    return;
                b = _weightMatrix;
            }
            thisisonlytest.SaveMatrixToFile(b, a);
        }
    }
}
