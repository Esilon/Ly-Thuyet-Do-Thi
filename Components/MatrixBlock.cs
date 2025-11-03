using System.Reflection;

namespace DoThi.Components
{
    public partial class MatrixBlock : UserControl
    {
        // Constants
        private const int Offset = 20;
        private const int HandleSize = 6;

        // Fields for dragging and resizing
        private Point _dragStartPoint;
        private Point _resizeStartBottomRight;
        private int _handleIndex = -1;
        private bool _isResizing = false;

        // Matrix data
        private int[,]? _adjacencyMatrix;
        private int[,]? _weightMatrix;
        private float _cellSize;
        private readonly Dictionary<int, string> _vertexValues = [];

        // State
        private bool _isSelected = false;
        private MatrixType _matrixType;

        // Resizing handles
        private Rectangle _topLeftHandle, _topRightHandle, _bottomRightHandle, _bottomLeftHandle;

        public MatrixBlock()
        {
            MinimumSize = new Size(20, 20);
            Size = new Size(160, 160);
            DoubleBuffered = true;
            SetDoubleBuffered();
            MatrixType = MatrixType.Adjacency;
        }

        /// <summary>
        /// Enables double buffering to reduce flicker during redraws.
        /// </summary>
        private void SetDoubleBuffered()
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });
            ResizeRedraw = true;
        }

        public void Reset()
        {
            _adjacencyMatrix = null;
            _weightMatrix = null;
            _vertexValues.Clear();
            Invalidate();
        }

        #region Properties

        public MatrixType MatrixType
        {
            get => _matrixType;
            set
            {
                _matrixType = value;
                UpdateMatrix();
            }
        }

        public int[,]? AdjacencyMatrix
        {
            get => _adjacencyMatrix;
            set
            {
                _adjacencyMatrix = value;
                UpdateMatrix();
            }
        }

        public int[,]? WeightMatrix
        {
            get => _weightMatrix;
            set
            {
                _weightMatrix = value;
                UpdateMatrix();
            }
        }

        #endregion

        private void UpdateMatrix()
        {
            int n = GetMatrixSize();
            _cellSize = n > 0 ? (Width - Offset) / (float)n : 0;
            Invalidate();
        }

        #region Mouse Events

        protected override void OnMouseHover(EventArgs e)
        {
            _isSelected = true;
            Invalidate();
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isSelected = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _dragStartPoint = e.Location;
            _resizeStartBottomRight = new Point(Right, Bottom);

            var cursorRect = new Rectangle(e.Location, Size.Empty);
            if (_topLeftHandle.IntersectsWith(cursorRect)) _handleIndex = 0;
            else if (_topRightHandle.IntersectsWith(cursorRect)) _handleIndex = 1;
            else if (_bottomRightHandle.IntersectsWith(cursorRect)) _handleIndex = 2;
            else if (_bottomLeftHandle.IntersectsWith(cursorRect)) _handleIndex = 3;
            else _handleIndex = -1;

            _isResizing = _handleIndex != -1;

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_isResizing)
                {
                    HandleResizing(e);
                }
                else
                {
                    HandleDragging(e);
                }
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        private void HandleDragging(MouseEventArgs e)
        {
            Cursor = Cursors.SizeAll;
            Point p = Parent.PointToClient(PointToScreen(e.Location));
            p.X -= _dragStartPoint.X;
            p.Y -= _dragStartPoint.Y;
            Location = p;
        }

        private void HandleResizing(MouseEventArgs e)
        {
            switch (_handleIndex)
            {
                case 0: // Top-left handle
                    Cursor = Cursors.SizeNWSE;
                    Location = Parent.PointToClient(PointToScreen(e.Location));
                    Width = _resizeStartBottomRight.X - Left;
                    Height = _resizeStartBottomRight.Y - Top;
                    break;
                case 1: // Top-right handle
                    Cursor = Cursors.SizeNESW;
                    Width = e.X;
                    Top = Parent.PointToClient(PointToScreen(e.Location)).Y;
                    Height = _resizeStartBottomRight.Y - Top;
                    break;
                case 2: // Bottom-right handle
                    Cursor = Cursors.SizeNWSE;
                    Width = e.X;
                    Height = e.Y;
                    break;
                case 3: // Bottom-left handle
                    Cursor = Cursors.SizeNESW;
                    Left = Parent.PointToClient(PointToScreen(e.Location)).X;
                    Width = _resizeStartBottomRight.X - Left;
                    Height = e.Y;
                    break;
            }
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            _isResizing = false;
            base.OnMouseUp(e);
        }

        #endregion

        #region Paint and Resize Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int n = GetMatrixSize();
            if (n > 0)
                _cellSize = (Width - Offset) / (float)n;

            Height = Width; // Maintain a square aspect ratio

            // Update handle positions
            _topLeftHandle = new Rectangle(0, 0, HandleSize, HandleSize);
            _topRightHandle = new Rectangle(Width - HandleSize - 1, 0, HandleSize, HandleSize);
            _bottomRightHandle = new Rectangle(Width - HandleSize - 1, Height - HandleSize - 1, HandleSize, HandleSize);
            _bottomLeftHandle = new Rectangle(0, Height - HandleSize - 1, HandleSize, HandleSize);

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GetMatrixSize() == 0) return;

            DrawMatrix(e.Graphics);

            if (_isSelected)
            {
                DrawHandles(e.Graphics);
            }

            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }

        private void DrawHandles(Graphics g)
        {
            g.FillRectangle(Brushes.White, _topLeftHandle);
            g.FillRectangle(Brushes.White, _topRightHandle);
            g.FillRectangle(Brushes.White, _bottomRightHandle);
            g.FillRectangle(Brushes.White, _bottomLeftHandle);

            g.DrawRectangle(Pens.Gray, _topLeftHandle);
            g.DrawRectangle(Pens.Gray, _topRightHandle);
            g.DrawRectangle(Pens.Gray, _bottomRightHandle);
            g.DrawRectangle(Pens.Gray, _bottomLeftHandle);
        }

        private void DrawMatrix(Graphics g)
        {
            int n = GetMatrixSize();
            if (n == 0) return;

            for (int i = 0; i < n; i++)
            {
                float pos = i * _cellSize + Offset;
                string vertexValue = GetVertexValue(i);

                // Draw header labels
                g.DrawString(vertexValue, Font, Brushes.Blue, new PointF(0, pos)); // Row labels
                g.DrawString(vertexValue, Font, Brushes.Blue, new PointF(pos, 0)); // Column labels

                // Draw grid lines
                g.DrawLine(Pens.Gray, pos, 0, pos, Height);
                g.DrawLine(Pens.Gray, 0, pos, Width, pos);

                // Draw matrix values
                for (int j = 0; j < n; j++)
                {
                    string matrixValue = GetMatrixValue(i, j);
                    g.DrawString(matrixValue, Font, Brushes.DarkMagenta, new PointF(pos, j * _cellSize + Offset));
                }
            }
        }

        #endregion

        #region Helper Methods

        private string GetMatrixValue(int i, int j)
        {
            if (i == j) return "0";

            switch (MatrixType)
            {
                case MatrixType.Adjacency:
                    return (_adjacencyMatrix?[i, j] == 1) ? "1" : "-";
                case MatrixType.Weight:
                    int? weight = _weightMatrix?[i, j];
                    return (weight > 0) ? weight.ToString() : "-";
                default:
                    return "-";
            }
        }

        public void SetVertexValue(int vertexIndex, string value)
        {
            _vertexValues[vertexIndex] = value;
            Invalidate();
        }

        public void ClearVertexValue(int vertexIndex)
        {
            if (!_vertexValues.ContainsKey(vertexIndex)) return;

            _vertexValues.Remove(vertexIndex);

            // Re-index subsequent keys to maintain consistency
            var keysToShift = _vertexValues.Keys.Where(key => key > vertexIndex).ToList();
            foreach (int key in keysToShift)
            {
                _vertexValues[key - 1] = _vertexValues[key];
                _vertexValues.Remove(key);
            }
            Invalidate();
        }

        private string GetVertexValue(int vertexIndex)
        {
            return _vertexValues.TryGetValue(vertexIndex, out string? value) ? value : (vertexIndex + 1).ToString();
        }

        private int GetMatrixSize()
        {
            return _adjacencyMatrix?.GetLength(0) ?? _weightMatrix?.GetLength(0) ?? 0;
        }

        #endregion

        public enum MatrixType
        {
            Adjacency,
            Weight
        }
    }
}
