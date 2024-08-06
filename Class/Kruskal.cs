namespace Đồ_Thị.Class
{
    public class Kruskal
    {
        private readonly List<Edge> _edges;
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _minimumSpanningTree;
        private readonly int[] _parent;

        public Kruskal(List<Vertex> vertices, List<Edge> edges)
        {
            _vertices = vertices;
            _edges = edges;
            _minimumSpanningTree = [];
            _parent = new int[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
            {
                _parent[i] = i;
            }
        }

        public List<Edge> GetMinimumSpanningTree()
        {
            _edges.Sort(new EdgeComparer());

            foreach (Edge edge in _edges)
            {
                int root1 = Find(edge.Vertex1);
                int root2 = Find(edge.Vertex2);

                if (root1 != root2)
                {
                    _minimumSpanningTree.Add(edge);
                    Union(root1, root2);
                }
            }

            return _minimumSpanningTree;
        }

        private int Find(int vertex)
        {
            if (_parent[vertex] != vertex)
            {
                _parent[vertex] = Find(_parent[vertex]);
            }
            return _parent[vertex];
        }

        private void Union(int root1, int root2)
        {
            _parent[root2] = root1;
        }

        private class EdgeComparer : IComparer<Edge>
        {
            public int Compare(Edge x, Edge y)
            {
                return x.Weight.CompareTo(y.Weight);
            }
        }
    }
}
