namespace DoThi.Class
{
    /// <summary>
    /// Determines if a graph is connected using a depth-first search (DFS) approach.
    /// </summary>
    public class GraphConnectivity
    {
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;

        public GraphConnectivity(List<Vertex> vertices, List<Edge> edges)
        {
            _vertices = vertices;
            _edges = edges;
        }

        /// <summary>
        /// Checks if the graph is connected.
        /// </summary>
        /// <returns>True if the graph is connected, false otherwise.</returns>
        public bool IsConnected()
        {
            if (_vertices.Count == 0)
                return true; // An empty graph is considered connected.

            var visited = new HashSet<int>();
            var stack = new Stack<int>();
            stack.Push(0); // Start DFS from the first vertex.

            while (stack.Count > 0)
            {
                int current = stack.Pop();

                if (visited.Add(current))
                {
                    foreach (var neighbor in GetNeighbors(current))
                    {
                        if (!visited.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }

            return visited.Count == _vertices.Count;
        }

        private IEnumerable<int> GetNeighbors(int vertexIndex)
        {
            var neighbors = new List<int>();
            foreach (var edge in _edges)
            {
                if (edge.Vertex1 == vertexIndex)
                {
                    neighbors.Add(edge.Vertex2);
                }
                else if (edge.Vertex2 == vertexIndex)
                {
                    neighbors.Add(edge.Vertex1);
                }
            }
            return neighbors;
        }
    }
}
