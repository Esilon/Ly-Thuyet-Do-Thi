namespace Đồ_Thị.Class
{
    class MovingBall
    {
        private const int SPEED = 5;
        private readonly System.Windows.Forms.Timer timer;
        private int currentEdgeIndex;
        private float deltaX;
        private float deltaY;
        public List<PointF> EdgePath;
        private readonly Control parent;
        private bool blink;
        private PointF currentPosition;
        private bool isCompleted;

        public bool IsRunning { get; private set; }

        public MovingBall(Control parent)
        {
            EdgePath = [];
            timer = new System.Windows.Forms.Timer
            {
                Interval = 10
            };
            this.parent = parent;
            timer.Tick += Timer_Tick;
        }

        public void Reset()
        {
            currentEdgeIndex = 0;
            if (EdgePath.Count > 0)
            {
                currentPosition = EdgePath[0];
            }
            isCompleted = false;
        }

        public void Start()
        {
            if (EdgePath.Count > 1)
            {
                currentEdgeIndex = 0;
                currentPosition = EdgePath[0];
                Prepare();
                timer.Start();
                IsRunning = true;
                isCompleted = false;
            }
        }

        public void Stop()
        {
            timer.Stop();
            IsRunning = false;
        }

        private void Prepare()
        {
            if (currentEdgeIndex < EdgePath.Count - 1)
            {
                PointF startPoint = currentPosition;
                PointF endPoint = EdgePath[currentEdgeIndex + 1];

                deltaX = endPoint.X - startPoint.X;
                deltaY = endPoint.Y - startPoint.Y;

                float length = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                if (length > 0)
                {
                    deltaX = deltaX / length * SPEED;
                    deltaY = deltaY / length * SPEED;
                }
            }
            else
            {
                // Đã đến cuối đường đi
                isCompleted = true;
                timer.Stop();
                IsRunning = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentEdgeIndex < EdgePath.Count - 1)
            {
                PointF endPoint = EdgePath[currentEdgeIndex + 1];

                float distance = (float)Math.Sqrt(Math.Pow(endPoint.X - currentPosition.X, 2) + Math.Pow(endPoint.Y - currentPosition.Y, 2));

                if (distance < SPEED)
                {
                    currentPosition = endPoint;
                    currentEdgeIndex++;
                    if (currentEdgeIndex >= EdgePath.Count - 1)
                    {
                        // Nếu đã đến cuối cùng của đường đi, có thể quay lại đầu
                        if (isCompleted)
                        {
                            currentEdgeIndex = 0;
                            isCompleted = false; // Đặt lại để có thể tái sử dụng
                        }
                    }
                    Prepare();
                }
                else
                {
                    currentPosition = new PointF(currentPosition.X + deltaX, currentPosition.Y + deltaY);
                }

                parent.Invalidate(); // Yêu cầu vẽ lại Panel để cập nhật đường đi mới
            }
        }

        public void Draw(Graphics g)
        {
            // Vẽ lại đường đi từ EdgePath
            using (Pen dashedPen = new(Color.Red, 2))
            {
                dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                for (int i = 0; i < EdgePath.Count - 1; i++)
                {
                    g.DrawLine(dashedPen, EdgePath[i], EdgePath[i + 1]);
                }
            }

            // Vẽ quả bóng
            if (EdgePath.Count > 0)
            {
                if (IsRunning)
                {
                    if (blink)
                        g.FillEllipse(Brushes.Red, currentPosition.X - 5, currentPosition.Y - 5, 10, 10);
                    else
                        g.FillEllipse(Brushes.Blue, currentPosition.X - 5, currentPosition.Y - 5, 10, 10);

                    blink = !blink;
                }
            }

            // Vẽ chú thích đầu và cuối đường đi
            for (int i = 0; i < EdgePath.Count; i++)
            {
                if (i == 0 && EdgePath.Count > 1)
                {
                    using Font font = new("Arial", 12);
                    using Brush brush = new SolidBrush(Color.Black);
                    g.DrawString("Đầu", font, brush, EdgePath[i].X + 20, EdgePath[i].Y - 20);
                }
                else if (i == EdgePath.Count - 1 && EdgePath.Count > 1)
                {
                    using Font font = new("Arial", 12);
                    using Brush brush = new SolidBrush(Color.Black);
                    g.DrawString("Cuối", font, brush, EdgePath[i].X + 20, EdgePath[i].Y - 20);
                }
            }
        }
    }
}