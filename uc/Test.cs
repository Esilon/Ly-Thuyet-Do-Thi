
using Đồ_Thị.Class;

namespace Đồ_Thị.uc
{
    public partial class Test : UserControl
    {
        readonly Class.ColorPri ColorPri = new();
        private readonly List<Vertex> vertices = [];
        private readonly List<Edge> edges = [];
        private Point dragOffset;
        private int currentMode = 0;
        private int selectedVertexIndex1 = -1;
        private int selectedVertexIndex2 = -1;
        private int selectedVertexIndex = -1;

        //Đỉnh Setting
        private const int vertexRadius = 10;
        private readonly Brush vertexColor = Brushes.Red;
        private readonly Brush selectedVertexColor = Brushes.Yellow;
        private readonly Brush edgeColor = Brushes.Black;
        private const int selectedVertexRadius = 2;

        //Background
        private readonly Color background = Color.LightGray;

        //Cạnh Setting
        private const int sizePen = 2;
        private const int arrowSize = 20;
        private const int vertexOffset = 0;
        private readonly Pen edgePen = new(Color.Black, sizePen);
        //Trọng số offset
        private const int weightOffset = 8;

        //Test zone
        private readonly int nextVertexNumber = 1;

        public Test()
        {
            InitializeComponent();
            UpdateButtonStates();
        }

        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            currentMode = 1;
            UpdateButtonStates();
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            currentMode = 2;
            UpdateButtonStates();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            currentMode = 0;
            UpdateButtonStates();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            currentMode = 3;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            btn_select.Enabled = currentMode != 0;
            btn_ThemDinh.Enabled = currentMode != 1;
            btn_ThemCanh.Enabled = currentMode != 2;
            btn_Xoa.Enabled = currentMode != 3;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(background);

            DrawEdges(g);
            DrawVertices(g);
        }

        private void DrawEdges(Graphics g)
        {
            foreach (Edge edge in edges)
            {
                Vertex p1 = vertices[edge.Vertex1];
                Vertex p2 = vertices[edge.Vertex2];
                DrawArrow(g, edgePen, p1.Location, p2.Location, edge.Weight, edge.IsDirected);
            }
        }

        private void DrawVertices(Graphics g)
        {
            foreach (Vertex vertex in vertices)
            {
                g.FillEllipse(vertexColor, vertex.Location.X - vertexRadius, vertex.Location.Y - vertexRadius, vertexRadius * 2, vertexRadius * 2);

                if (vertices.IndexOf(vertex) == selectedVertexIndex)
                {
                    g.FillEllipse(selectedVertexColor, vertex.Location.X - (vertexRadius + selectedVertexRadius), vertex.Location.Y - (vertexRadius + selectedVertexRadius), (vertexRadius + selectedVertexRadius) * 2, (vertexRadius + selectedVertexRadius) * 2);
                    g.FillEllipse(vertexColor, vertex.Location.X - vertexRadius, vertex.Location.Y - vertexRadius, vertexRadius * 2, vertexRadius * 2);
                }

                // Vẽ giá trị của đỉnh
                g.DrawString(vertex.Value, Font, edgeColor, vertex.Location.X, vertex.Location.Y);
            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int hoveredVertexIndex = GetVertexIndexAtPoint(e.Location);
            paneldraw.Cursor = hoveredVertexIndex == -1 ? Cursors.Default : Cursors.Hand;

            if (e.Button == MouseButtons.Left)
            {
                if (currentMode == 0 && hoveredVertexIndex != -1)
                {
                    selectedVertexIndex1 = hoveredVertexIndex;
                    vertices[selectedVertexIndex1].Location = new Point(e.X + dragOffset.X, e.Y + dragOffset.Y);
                    paneldraw.Invalidate();
                }
                else if (currentMode == 2)
                {
                    paneldraw.Invalidate();
                }
            }
            NodeCount();
        }

        private void NodeCount()
        {
            txtNodeCount.Text = vertices.Count.ToString();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (currentMode)
            {
                case 0:
                    HandleSelectVertex(e);
                    break;
                case 1:
                    AddVertex(e);
                    break;
                case 2:
                    AddEdge(e);
                    break;
                case 3:
                    RemoveVertex(e);
                    break;
            }
        }

        private void HandleSelectVertex(MouseEventArgs e)
        {
            int clickedVertexIndex = GetVertexIndexAtPoint(e.Location);

            if (selectedVertexIndex != -1)
            {
                paneldraw.Invalidate();
            }
            selectedVertexIndex = clickedVertexIndex;
            paneldraw.Invalidate();
        }

        private void AddVertex(MouseEventArgs e)
        {
            int x = Math.Max(vertexRadius, Math.Min(paneldraw.Width - vertexRadius, e.X));
            int y = Math.Max(vertexRadius, Math.Min(paneldraw.Height - vertexRadius, e.Y));

            // Hiển thị hộp thoại để nhập giá trị của đỉnh
            string vertexValue = Microsoft.VisualBasic.Interaction.InputBox("Nhập giá trị của đỉnh:", "Thêm đỉnh", nextVertexNumber.ToString());

            Vertex newVertex = new(new Point(x, y), vertexValue);
            vertices.Add(newVertex);
            NodeCount();
            paneldraw.Invalidate();
        }


        private void AddEdge(MouseEventArgs e)
        {
            int clickedVertexIndex = GetVertexIndexAtPoint(e.Location);

            if (selectedVertexIndex1 == -1)
            {
                selectedVertexIndex1 = clickedVertexIndex;
                if (selectedVertexIndex1 != -1)
                {
                    _ = MessageBox.Show($"Đỉnh {selectedVertexIndex1 + 1} đã được chọn. Vui lòng chọn đỉnh thứ hai.");
                }
            }
            else if (selectedVertexIndex2 == -1)
            {
                selectedVertexIndex2 = clickedVertexIndex;
                if (selectedVertexIndex2 != -1 && selectedVertexIndex1 != selectedVertexIndex2)
                {
                    using Compoments.ThemCanh edgeForm = new(vertices, selectedVertexIndex1, selectedVertexIndex2);
                    if (edgeForm.ShowDialog() == DialogResult.OK)
                    {
                        Edge newEdge = edgeForm.GetEdge();
                        edges.Add(newEdge);
                        paneldraw.Invalidate();
                    }
                }
                else if (selectedVertexIndex2 == selectedVertexIndex1)
                {
                    _ = MessageBox.Show("Vui lòng chọn một đỉnh khác.");
                }

                selectedVertexIndex1 = -1;
                selectedVertexIndex2 = -1;
            }
        }


        private void RemoveVertex(MouseEventArgs e)
        {
            int clickedVertexIndex = GetVertexIndexAtPoint(e.Location);

            if (clickedVertexIndex != -1)
            {
                vertices.RemoveAt(clickedVertexIndex);
                _ = edges.RemoveAll(edge => edge.Vertex1 == clickedVertexIndex || edge.Vertex2 == clickedVertexIndex);
                NodeCount();
                paneldraw.Invalidate();
            }
        }


        private int GetVertexIndexAtPoint(PointF location)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (IsPointInCircle(location, vertices[i].Location, vertexRadius))
                {
                    return i;
                }
            }
            return -1;
        }


        private static bool IsPointInCircle(PointF point, PointF circleCenter, int radius)
        {
            return (Math.Pow(point.X - circleCenter.X, 2) + Math.Pow(point.Y - circleCenter.Y, 2)) <= Math.Pow(radius, 2);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            vertices.Clear();
            edges.Clear();
            paneldraw.Invalidate();
        }

        private void DrawArrow(Graphics g, Pen pen, PointF p1, PointF p2, int weight, bool isDirected)
        {
            float angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);

            float startX = p2.X - (vertexRadius + vertexOffset) * (float)Math.Cos(angle);
            float startY = p2.Y - (vertexRadius + vertexOffset) * (float)Math.Sin(angle);
            PointF startPoint = new(startX, startY);

            g.DrawLine(pen, p1, startPoint);

            if (isDirected)
            {
                PointF[] arrowHeadPoints =
                [
                    new PointF(startX, startY),
                    new PointF(startX - arrowSize * (float)Math.Cos(angle - Math.PI / 6), startY - arrowSize * (float)Math.Sin(angle - Math.PI / 6)),
                    new PointF(startX - arrowSize * (float)Math.Cos(angle + Math.PI / 6), startY - arrowSize * (float)Math.Sin(angle + Math.PI / 6)),
                ];
                g.FillPolygon(Brushes.Black, arrowHeadPoints);
            }

            DrawEdgeWeight(g, p1, p2, weight, angle);
        }

        private void DrawEdgeWeight(Graphics g, PointF p1, PointF p2, int weight, float angle)
        {
            float midX = (p1.X + p2.X) / 2;
            float midY = (p1.Y + p2.Y) / 2;
            float offsetX = weightOffset * (float)Math.Cos(angle + Math.PI / 2);
            float offsetY = weightOffset * (float)Math.Sin(angle + Math.PI / 2);
            PointF textLocation = new(midX + offsetX, midY + offsetY);

            SizeF textSize = g.MeasureString(weight.ToString(), Font);
            if (Math.Abs(p1.X - p2.X) < textSize.Width || Math.Abs(p1.Y - p2.Y) < textSize.Height)
            {
                offsetX *= 2;
                offsetY *= 2;
                textLocation = new PointF(midX + offsetX, midY + offsetY);
            }

            g.DrawString(weight.ToString(), Font, edgeColor, textLocation);
        }
    }

}
