using System.Reflection;

namespace Đồ_Thị.uc
{
    public partial class MatrixBlock : UserControl
    {
        const int OFFSET = 20;
        const int HANDLE_SIZE = 6;

        Point _p;
        Point _pRightBottom;

        int[,]? _adjacencyMatrix;
        int[,]? _weightMatrix;

        float _size;

        private readonly Dictionary<int, string> vertexValues = [];
        Rectangle _rectHandle1, _rectHandle2, _rectHandle3, _rectHandle4;
        int _handleIndex = -1;

        bool _selected = false;
        bool _resizing = false;
        private MatrixType _matrixType;

        public MatrixBlock()
        {
            _rectHandle1 = new Rectangle(0, 0, HANDLE_SIZE, HANDLE_SIZE);
            MinimumSize = new Size(20, 20);
            Size = new Size(160, 160);
            DoubleBuffered = true;
            SetDoubleBufferedPanel();
            MatrixType2 = MatrixType.Adjacency; // Corrected property name to MatrixType
        }
        private void SetDoubleBufferedPanel()
        {
            _ = typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, [true]);
            ResizeRedraw = true;
        }

        public void Reset()
        {
            _adjacencyMatrix = null;
            _weightMatrix = null;
        }

        public MatrixType MatrixType2 // Corrected property name to MatrixType
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

        private void UpdateMatrix()
        {
            int n = GetMatrixSize();
            _size = n > 0 ? (Width - OFFSET) / n : 0;

            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            _selected = true;
            Invalidate();
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _selected = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _p = e.Location;
                Invalidate();

                _resizing = true;
                Rectangle cursor = new(e.Location, new Size(0, 0));
                _pRightBottom = new Point(Right, Bottom);

                if (_rectHandle1.IntersectsWith(cursor)) _handleIndex = 0;
                else if (_rectHandle2.IntersectsWith(cursor)) _handleIndex = 1;
                else if (_rectHandle3.IntersectsWith(cursor)) _handleIndex = 2;
                else if (_rectHandle4.IntersectsWith(cursor)) _handleIndex = 3;
                else
                    _resizing = false;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_resizing)
                {
                    if (_handleIndex == 0)
                    {
                        Cursor = Cursors.SizeNWSE;
                        Location = Parent.PointToClient(PointToScreen(e.Location));
                        Width = _pRightBottom.X - Left;
                        Height = _pRightBottom.Y - Top;
                    }
                    else if (_handleIndex == 1)
                    {
                        Cursor = Cursors.SizeNESW;
                        Width = e.X;
                        Top = Parent.PointToClient(PointToScreen(e.Location)).Y;
                        Height = _pRightBottom.Y - Top;
                    }
                    else if (_handleIndex == 2)
                    {
                        Cursor = Cursors.SizeNWSE;
                        Width = e.X;
                        Height = e.Y;
                    }
                    else if (_handleIndex == 3)
                    {
                        Cursor = Cursors.SizeNESW;
                        Left = Parent.PointToClient(PointToScreen(e.Location)).X;
                        Width = _pRightBottom.X - Left;
                        Height = e.Y;
                    }
                }
                else
                {
                    Cursor = Cursors.SizeAll;
                    Point p = Parent.PointToClient(PointToScreen(e.Location));
                    p.X -= _p.X;
                    p.Y -= _p.Y;
                    Location = p;
                }
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            base.OnMouseUp(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_adjacencyMatrix != null)
                _size = (Width - OFFSET) / _adjacencyMatrix.GetLength(0);

            Height = Width;

            _rectHandle2 = new Rectangle(Width - HANDLE_SIZE - 1, 0, HANDLE_SIZE, HANDLE_SIZE);
            _rectHandle3 = new Rectangle(Width - HANDLE_SIZE - 1, Height - HANDLE_SIZE - 1, HANDLE_SIZE, HANDLE_SIZE);
            _rectHandle4 = new Rectangle(0, Height - HANDLE_SIZE - 1, HANDLE_SIZE, HANDLE_SIZE);

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_adjacencyMatrix == null)
                return;

            DrawMatrix(e.Graphics);
            if (_selected)
            {
                e.Graphics.FillRectangle(Brushes.White, _rectHandle1);
                e.Graphics.FillRectangle(Brushes.White, _rectHandle2);
                e.Graphics.FillRectangle(Brushes.White, _rectHandle3);
                e.Graphics.FillRectangle(Brushes.White, _rectHandle4);

                e.Graphics.DrawRectangle(Pens.Gray, _rectHandle1);
                e.Graphics.DrawRectangle(Pens.Gray, _rectHandle2);
                e.Graphics.DrawRectangle(Pens.Gray, _rectHandle3);
                e.Graphics.DrawRectangle(Pens.Gray, _rectHandle4);
            }
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            base.OnPaint(e);
        }

        private void DrawMatrix(Graphics g)
        {
            if (_adjacencyMatrix == null)
                return;

            int n = GetMatrixSize();

            for (int i = 0; i < n; i++)
            {
                float x = i * _size + OFFSET;
                string vertexValue = GetVertexValue(i);

                DrawVertexLabel(g, vertexValue, x);
                DrawGridLines(g, x);

                for (int j = 0; j < n; j++)
                {
                    string matrixValue = GetMatrixValue(i, j);
                    DrawMatrixValue(g, matrixValue, x, j);
                }
            }
        }

        private void DrawVertexLabel(Graphics g, string vertexValue, float x)
        {
            g.DrawString(vertexValue, Font, Brushes.Blue, new PointF(0, x));
            g.DrawString(vertexValue, Font, Brushes.Blue, new PointF(x, 0));
        }

        private void DrawGridLines(Graphics g, float x)
        {
            g.DrawLine(Pens.Gray, x, 0, x, Height);
            g.DrawLine(Pens.Gray, 0, x, Width, x);
        }

        private void DrawMatrixValue(Graphics g, string matrixValue, float x, int j)
        {
            g.DrawString(matrixValue, Font, Brushes.DarkMagenta, new PointF(x, j * _size + OFFSET));
        }

        private string GetMatrixValue(int i, int j)
        {
            return i == j
                ? "0"
                : _matrixType == MatrixType.Adjacency
                ? (_adjacencyMatrix[i, j] == 1) ? "1" : "-"
                : _matrixType == MatrixType.Weight && _weightMatrix != null
                ? (_weightMatrix[i, j] > 0) ? _weightMatrix[i, j].ToString() : "-"
                : "-";
        }

        public void SetVertexValue(int vertexIndex, string value)
        {
            vertexValues[vertexIndex] = value;
            Invalidate();
        }

        public void ClearVertexValue(int vertexIndex)
        {
            if (vertexValues.ContainsKey(vertexIndex))
            {
                _ = vertexValues.Remove(vertexIndex);
                List<int> keysToShift = vertexValues.Keys.Where(key => key > vertexIndex).ToList();
                foreach (int key in keysToShift)
                {
                    vertexValues[key - 1] = vertexValues[key];
                    _ = vertexValues.Remove(key);
                }
                Invalidate();
            }
        }

        private string GetVertexValue(int vertexIndex)
        {
            return vertexValues.TryGetValue(vertexIndex, out string? value) ? value : (vertexIndex + 1).ToString();
        }

        private int GetMatrixSize()
        {
            return _adjacencyMatrix?.GetLength(0) ?? _weightMatrix?.GetLength(0) ?? 0;
        }

        public enum MatrixType
        {
            Adjacency,
            Weight
        }
    }
}
