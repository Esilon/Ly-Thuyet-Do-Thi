namespace Đồ_Thị.Class
{
    [Serializable]
    public class Edge(int vertex1, int vertex2, int weight, bool isDirected) : IComparable<Edge>
    {
        public int Vertex1 { get; set; } = vertex1;
        public int Vertex2 { get; set; } = vertex2;
        public int Weight { get; set; } = weight;
        public bool IsDirected { get; set; } = isDirected;

        public int CompareTo(Edge other)
        {
            return Weight.CompareTo(other.Weight);
        }
        public class EdgeComparer : IComparer<Edge>
        {
            public int Compare(Edge x, Edge y)
            {
                return x.Weight.CompareTo(y.Weight);
            }
        }
    }
}
