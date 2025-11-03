namespace DoThi.Class
{
    /// <summary>
    /// Implements Prim's algorithm to find the Minimum Spanning Tree (MST) of a graph.
    /// </summary>
    public class Prim
    {
        private readonly List<Edge> _edges;
        private readonly int _vertexCount;

        public Prim(int vertexCount, List<Edge> edges)
        {
            _vertexCount = vertexCount;
            _edges = edges;
        }

        /// <summary>
        /// Finds the Minimum Spanning Tree (MST) of the graph using Prim's algorithm.
        /// </summary>
        /// <param name="startVertex">The vertex to start the algorithm from.</param>
        /// <returns>A list of edges that form the MST.</returns>
        public List<Edge> GetMinimumSpanningTree(int startVertex)
        {
            var minimumSpanningTree = new List<Edge>();
            var visited = new bool[_vertexCount];
            var priorityQueue = new PriorityQueue<Edge, int>();

            Visit(startVertex, visited, priorityQueue);

            while (priorityQueue.Count > 0)
            {
                var edge = priorityQueue.Dequeue();

                if (visited[edge.Vertex1] && visited[edge.Vertex2])
                    continue;

                minimumSpanningTree.Add(edge);

                if (!visited[edge.Vertex1])
                    Visit(edge.Vertex1, visited, priorityQueue);

                if (!visited[edge.Vertex2])
                    Visit(edge.Vertex2, visited, priorityQueue);
            }

            return minimumSpanningTree;
        }

        private void Visit(int vertex, bool[] visited, PriorityQueue<Edge, int> priorityQueue)
        {
            visited[vertex] = true;

            foreach (var edge in _edges.Where(e => e.Vertex1 == vertex || e.Vertex2 == vertex))
            {
                if (edge.Vertex1 == vertex && !visited[edge.Vertex2])
                    priorityQueue.Enqueue(edge, edge.Weight);

                if (edge.Vertex2 == vertex && !visited[edge.Vertex1])
                    priorityQueue.Enqueue(edge, edge.Weight);
            }
        }
    }
}
