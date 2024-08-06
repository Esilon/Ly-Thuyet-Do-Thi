namespace Đồ_Thị.Class
{
    [Serializable]
    public class Edge : IComparable<Edge>
    {
        public int Vertex1 { get; set; }
        public int Vertex2 { get; set; }
        public int Weight { get; set; }
        public bool IsDirected { get; set; }

        public Edge(int vertex1, int vertex2, int weight, bool isDirected)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
            IsDirected = isDirected;
        }
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
