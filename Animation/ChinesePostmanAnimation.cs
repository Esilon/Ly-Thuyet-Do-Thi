using DoThi.Class;
using Timer = System.Windows.Forms.Timer;

namespace DoThi.Animation
{
    public class ChinesePostmanAnimation
    {
        private readonly ChinesePostman _chinesePostman;
        private readonly Panel _panel;
        private readonly List<PointF> _animationPath;
        private int _currentFrameIndex = 0;
        private readonly Timer _timer;

        public ChinesePostmanAnimation(ChinesePostman chinesePostman, Panel panel)
        {
            _chinesePostman = chinesePostman;
            _panel = panel;
            _animationPath = new List<PointF>();
            CreateAnimationPath();

            _timer = new Timer { Interval = 100 };
            _timer.Tick += OnTick;
        }

        public void StartAnimation()
        {
            _currentFrameIndex = 0;
            _timer.Start();
        }

        private void CreateAnimationPath()
        {
            var eulerianCycle = _chinesePostman.GetEulerianCycle();
            if (eulerianCycle == null || eulerianCycle.Count < 2) return;

            for (int i = 0; i < eulerianCycle.Count - 1; i++)
            {
                var currentVertex = eulerianCycle[i];
                var nextVertex = eulerianCycle[i + 1];
                var edge = _chinesePostman.GetEdgeBetween(currentVertex, nextVertex);
                if (edge == null) continue;

                var startPoint = currentVertex.Location;
                var endPoint = nextVertex.Location;

                // Add intermediate points for a smoother animation.
                for (float t = 0; t <= 1; t += 0.1f)
                {
                    _animationPath.Add(Lerp(startPoint, endPoint, t));
                }
            }
        }

        private static PointF Lerp(PointF start, PointF end, float t)
        {
            return new PointF(start.X + t * (end.X - start.X), start.Y + t * (end.Y - start.Y));
        }

        private void OnTick(object? sender, EventArgs e)
        {
            if (_currentFrameIndex < _animationPath.Count)
            {
                _currentFrameIndex++;
                _panel.Invalidate();
            }
            else
            {
                _timer.Stop();
            }
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (_animationPath.Count <= 0 || _currentFrameIndex >= _animationPath.Count) return;

            // Draw the current position of the "postman".
            var currentPosition = _animationPath[_currentFrameIndex];
            g.FillEllipse(Brushes.Blue, currentPosition.X - 5, currentPosition.Y - 5, 10, 10);
        }
    }
}
