namespace DoThi.Class
{
    /// <summary>
    /// Implements Kruskal's algorithm to find the Minimum Spanning Tree (MST) of a graph.
    /// </summary>
    public class Kruskal
    {
        private readonly List<Edge> _edges;
        private readonly List<Edge> _minimumSpanningTree;
        private readonly int[] _parent;

        public Kruskal(int vertexCount, List<Edge> edges)
        {
            _edges = edges;
            _minimumSpanningTree = new List<Edge>();
            _parent = new int[vertexCount];

            // Initialize each vertex as its own parent.
            for (int i = 0; i < vertexCount; i++)
            {
                _parent[i] = i;
            }
        }

        /// <summary>
        /// Finds the Minimum Spanning Tree (MST) of the graph.
        /// </summary>
        /// <returns>A list of edges that form the MST.</returns>
        public List<Edge> GetMinimumSpanningTree()
        {
            // Sort edges by weight in ascending order.
            _edges.Sort();

            foreach (var edge in _edges)
            {
                int root1 = Find(edge.Vertex1);
                int root2 = Find(edge.Vertex2);

                // If the vertices are in different sets, add the edge to the MST and union the sets.
                if (root1 != root2)
                {
                    _minimumSpanningTree.Add(edge);
                    Union(root1, root2);
                }
            }

            return _minimumSpanningTree;
        }

        // Finds the root of a vertex's set.
        private int Find(int vertex)
        {
            if (_parent[vertex] != vertex)
            {
                // Path compression for optimization.
                _parent[vertex] = Find(_parent[vertex]);
            }
            return _parent[vertex];
        }

        // Merges two sets.
        private void Union(int root1, int root2)
        {
            _parent[root2] = root1;
        }
    }
}
