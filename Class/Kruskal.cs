namespace Đồ_Thị.Class
{
    public class Kruskal
    {
        // Danh sách các cạnh của đồ thị
        private readonly List<Edge> _edges;
        
        // Danh sách lưu trữ các cạnh của cây bao trùm nhỏ nhất (MST)
        private readonly List<Edge> _minimumSpanningTree;
        
        // Mảng cha dùng cho cấu trúc dữ liệu union-find
        private readonly int[] _parent;

        // Hàm khởi tạo
        public Kruskal(List<Vertex> vertices, List<Edge> edges)
        {
            _edges = edges; // Gán danh sách các cạnh
            _minimumSpanningTree = new List<Edge>(); // Khởi tạo danh sách rỗng cho MST
            _parent = new int[vertices.Count]; // Khởi tạo mảng cha với kích thước bằng số đỉnh
            
            // Khởi tạo mảng cha, mỗi đỉnh là cha của chính nó
            for (int i = 0; i < vertices.Count; i++)
            {
                _parent[i] = i;
            }
        }

        // Hàm tính toán và trả về cây bao trùm nhỏ nhất (MST)
        public List<Edge> GetMinimumSpanningTree()
        {
            // Sắp xếp các cạnh theo trọng số tăng dần
            _edges.Sort(new EdgeComparer());

            // Duyệt qua từng cạnh đã sắp xếp
            foreach (Edge edge in _edges)
            {
                // Tìm gốc của hai đỉnh đầu mút của cạnh
                int root1 = Find(edge.Vertex1);
                int root2 = Find(edge.Vertex2);

                // Nếu hai đỉnh thuộc các nhóm khác nhau
                if (root1 != root2)
                {
                    // Thêm cạnh vào MST
                    _minimumSpanningTree.Add(edge);
                    
                    // Hợp nhất hai nhóm
                    Union(root1, root2);
                }
            }

            // Trả về danh sách các cạnh của MST
            return _minimumSpanningTree;
        }

        // Hàm tìm gốc của đỉnh
        private int Find(int vertex)
        {
            // Nếu đỉnh không phải là gốc của nhóm
            if (_parent[vertex] != vertex)
            {
                // Cập nhật cha của đỉnh theo đường dẫn, và thực hiện nén đường
                _parent[vertex] = Find(_parent[vertex]);
            }
            // Trả về gốc của nhóm
            return _parent[vertex];
        }

        // Hàm hợp nhất hai nhóm
        private void Union(int root1, int root2)
        {
            // Gán gốc của nhóm thứ hai làm cha của nhóm thứ nhất
            _parent[root2] = root1;
        }

        // Lớp so sánh các cạnh theo trọng số
        private class EdgeComparer : IComparer<Edge>
        {
            // So sánh hai cạnh dựa trên trọng số của chúng
            public int Compare(Edge x, Edge y)
            {
                return x.Weight.CompareTo(y.Weight);
            }
        }
    }
}
