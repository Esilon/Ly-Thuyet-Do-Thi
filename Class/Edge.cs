namespace DoThi.Class
{
    [Serializable]
    public class Edge : IComparable<Edge>
    {
        public int Vertex1 { get; }
        public int Vertex2 { get; }
        public int Weight { get; }
        public bool IsDirected { get; }

        public Edge(int vertex1, int vertex2, int weight, bool isDirected)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
            IsDirected = isDirected;
        }

        public int CompareTo(Edge? other)
        {
            if (other == null) return 1;
            return Weight.CompareTo(other.Weight);
        }
    }
}
