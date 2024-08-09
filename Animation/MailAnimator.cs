using Đồ_Thị.Class;
using Timer = System.Windows.Forms.Timer;
namespace Đồ_Thị.Animation
{
    public class AnimationFrame
    {
        public PointF PositionOnEdge { get; set; } // Vị trí hiện tại trên cạnh
        public Edge CurrentEdge { get; set; }
        public int Step { get; set; }
    }

    public class MailAnimator
    {
        private readonly Mail _mail;
        private readonly Panel _panel;
        private readonly List<AnimationFrame> _frames;
        private int _currentFrameIndex = 0;
        private Timer _timer;


        public MailAnimator(Mail mail, Panel panel)
        {
            _mail = mail;
            _panel = panel;
            _frames = new List<AnimationFrame>();
            CreateAnimationFrames();
        }
        public void StartAnimation()
        {
            _timer = new Timer
            {
                Interval = 100 // Khoảng thời gian giữa các khung hình (100ms)
            };
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void CreateAnimationFrames()
        {
            var eulerianCycle = _mail.GetEulerianCycle();

            if (eulerianCycle == null || eulerianCycle.Count < 2)
            {
                // Không đủ đỉnh để tạo animation
                return;
            }

            // Tạo khung hình di chuyển giữa các đỉnh theo thứ tự tuyến tính
            for (int i = 0; i < eulerianCycle.Count - 1; i++)
            {
                var currentVertex = eulerianCycle[i];
                var nextVertex = eulerianCycle[i + 1];

                // Xác định cạnh giữa currentVertex và nextVertex
                var edge = _mail.GetEdgeBetween(currentVertex, nextVertex);

                if (edge == null)
                {
                    continue; // Bỏ qua nếu không tìm thấy cạnh
                }

                // Lấy điểm bắt đầu và kết thúc trên cạnh
                var startPoint = GetPointOnEdge(edge, 0);
                var endPoint = GetPointOnEdge(edge, 1);

                // Xác định hướng di chuyển trên cạnh
                if (currentVertex == _mail.vertices[edge.Vertex1])
                {
                    // Đang di chuyển từ Vertex1 đến Vertex2
                    AddFramesForEdge(startPoint, endPoint, edge);
                }
                else
                {
                    // Đang di chuyển từ Vertex2 đến Vertex1
                    AddFramesForEdge(endPoint, startPoint, edge);
                }
            }
        }

        private void AddFramesForEdge(PointF startPoint, PointF endPoint, Edge edge)
        {
            // Di chuyển từ startPoint đến endPoint
            for (float t = 0; t <= 1; t += 0.1f) // Di chuyển từ 0 đến 1 với bước 0.1
            {
                var positionOnEdge = Lerp(startPoint, endPoint, t);
                _frames.Add(new AnimationFrame
                {
                    PositionOnEdge = positionOnEdge,
                    CurrentEdge = edge,
                    Step = _frames.Count
                });
            }
        }

        // Hàm nội suy tuyến tính
        private PointF Lerp(PointF start, PointF end, float t)
        {
            return new PointF
            (
                start.X + t * (end.X - start.X),
                start.Y + t * (end.Y - start.Y)
            );
        }

        // Lấy điểm trên cạnh theo tỷ lệ
        private PointF GetPointOnEdge(Edge edge, float t)
        {
            var start = _mail.vertices[edge.Vertex1].Location;
            var end =_mail.vertices[edge.Vertex2].Location;
            return Lerp(start, end, t);
        }


        private void OnTick(object sender, EventArgs e)
        {
            if (_currentFrameIndex >= 0 && _currentFrameIndex < _frames.Count)
            {
                var frame = _frames[_currentFrameIndex];
                _panel.Invalidate(); // Gọi để vẽ lại Panel
                _currentFrameIndex++;
            }
            else
            {
                _timer.Stop(); // Dừng animation khi kết thúc
            }
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (_frames.Count > 0)
            {
                if (_currentFrameIndex >= 0 && _currentFrameIndex < _frames.Count)
                {
                    var frame = _frames[_currentFrameIndex];
                    if (frame.CurrentEdge != null)
                    {
                        // Vẽ cạnh hiện tại
                        var vertex1 = _mail.GetVertexById(frame.CurrentEdge.Vertex1);
                        var vertex2 = _mail.GetVertexById(frame.CurrentEdge.Vertex2);
                        g.DrawLine(Pens.Red, vertex1.Location, vertex2.Location);

                        // Vẽ điểm trên cạnh
                        var pointOnEdge = frame.PositionOnEdge;
                        g.FillEllipse(Brushes.Blue, pointOnEdge.X - 5, pointOnEdge.Y - 5, 10, 10); // Vẽ điểm di chuyển (đường kính 10)
                    }
                }
            }
        }

    }
}
