namespace DoThi.Class
{
    public class MovingBall
    {
        private const int Speed = 5;
        private readonly System.Windows.Forms.Timer _timer;
        private int _currentEdgeIndex;
        private float _deltaX;
        private float _deltaY;
        public List<PointF> EdgePath { get; set; }
        private readonly Control _parent;
        private bool _blink;
        private PointF _currentPosition;
        private bool _isCompleted;

        public bool IsRunning { get; private set; }

        public MovingBall(Control parent)
        {
            EdgePath = new List<PointF>();
            _timer = new System.Windows.Forms.Timer { Interval = 10 };
            _parent = parent;
            _timer.Tick += Timer_Tick;
        }

        public void Reset()
        {
            _currentEdgeIndex = 0;
            if (EdgePath.Count > 0)
            {
                _currentPosition = EdgePath[0];
            }
            _isCompleted = false;
        }

        public void Start()
        {
            if (EdgePath.Count <= 1) return;

            Reset();
            PrepareNextSegment();
            _timer.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            _timer.Stop();
            IsRunning = false;
        }

        private void PrepareNextSegment()
        {
            if (_currentEdgeIndex >= EdgePath.Count - 1)
            {
                _isCompleted = true;
                Stop();
                return;
            }

            PointF startPoint = _currentPosition;
            PointF endPoint = EdgePath[_currentEdgeIndex + 1];

            _deltaX = endPoint.X - startPoint.X;
            _deltaY = endPoint.Y - startPoint.Y;

            float length = (float)Math.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);

            if (length > 0)
            {
                _deltaX = _deltaX / length * Speed;
                _deltaY = _deltaY / length * Speed;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_isCompleted)
            {
                Stop();
                return;
            }

            PointF endPoint = EdgePath[_currentEdgeIndex + 1];
            float distance = (float)Math.Sqrt(Math.Pow(endPoint.X - _currentPosition.X, 2) + Math.Pow(endPoint.Y - _currentPosition.Y, 2));

            if (distance < Speed)
            {
                _currentPosition = endPoint;
                _currentEdgeIndex++;
                PrepareNextSegment();
            }
            else
            {
                _currentPosition = new PointF(_currentPosition.X + _deltaX, _currentPosition.Y + _deltaY);
            }

            _parent.Invalidate();
        }

        public void Draw(Graphics g)
        {
            if (EdgePath.Count <= 1) return;

            // Draw the path.
            using (var dashedPen = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                for (int i = 0; i < EdgePath.Count - 1; i++)
                {
                    g.DrawLine(dashedPen, EdgePath[i], EdgePath[i + 1]);
                }
            }

            // Draw the ball.
            if (IsRunning)
            {
                g.FillEllipse(_blink ? Brushes.Red : Brushes.Blue, _currentPosition.X - 5, _currentPosition.Y - 5, 10, 10);
                _blink = !_blink;
            }

            // Draw start and end labels.
            using var font = new Font("Arial", 12);
            using var brush = new SolidBrush(Color.Black);
            g.DrawString("Start", font, brush, EdgePath[0].X + 20, EdgePath[0].Y - 20);
            g.DrawString("End", font, brush, EdgePath[^1].X + 20, EdgePath[^1].Y - 20);
        }
    }
}
