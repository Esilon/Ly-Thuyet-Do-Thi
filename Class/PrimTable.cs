namespace Đồ_Thị.Class
{
    public class PrimTable(List<Vertex> vertices, List<Edge> edges)
    {
        private readonly List<Vertex> vertices = vertices;
        private readonly List<Edge> edges = edges;

        public static void RunPrimAlgorithm(List<Vertex> vertices, List<Edge> edges, DataGridView dataGridView)
        {
                       var selectedVertices = new HashSet<int> { 0 }; // Bắt đầu từ đỉnh 0
            var mstEdges = new List<Edge>();
            var step = 0;

            // Khởi tạo hàng đầu tiên
            var initialRow = new object[vertices.Count + 3];
            initialRow[0] = "Khởi tạo";
            for (int i = 0; i < vertices.Count; i++)
            {
                if (i == 0)
                    initialRow[i + 1] = $"[0,1]";
                else
                    initialRow[i + 1] = $"[∞,1]";
            }
            initialRow[vertices.Count + 1] = "1"; // VH
            initialRow[vertices.Count + 2] = "∅"; // T
            dataGridView.Rows.Add(initialRow);

            while (selectedVertices.Count < vertices.Count)
            {
                Edge minEdge = null;
                foreach (var edge in edges)
                {
                    if (selectedVertices.Contains(edge.Vertex1) && !selectedVertices.Contains(edge.Vertex2) ||
                        selectedVertices.Contains(edge.Vertex2) && !selectedVertices.Contains(edge.Vertex1))
                    {
                        if (minEdge == null || edge.Weight < minEdge.Weight)
                        {
                            minEdge = edge;
                        }
                    }
                }

                if (minEdge == null) break; // Nếu không có cạnh nào có thể chọn, thoát khỏi vòng lặp

                // Thêm cạnh nhỏ nhất vào cây khung
                mstEdges.Add(minEdge);
                var nextVertex = selectedVertices.Contains(minEdge.Vertex1) ? minEdge.Vertex2 : minEdge.Vertex1;
                selectedVertices.Add(nextVertex);

                // Cập nhật DataGridView
                var row = new object[vertices.Count + 3];
                row[0] = step.ToString();
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (selectedVertices.Contains(i))
                        row[i + 1] = "-";
                    else
                        row[i + 1] = $"[{minEdge.Weight}, {i+1}]";
                }
                row[vertices.Count + 1] = string.Join(",", selectedVertices.Select(v => (v + 1).ToString())); // VH
                row[vertices.Count + 2] = string.Join(",", mstEdges.Select(e => $"({e.Vertex1+1},{e.Vertex2+1})")); // T
                dataGridView.Rows.Add(row);
                step++;
            }
        }
    }
}
