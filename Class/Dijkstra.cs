namespace DoThi.Class
{
    /// <summary>
    /// Implements Dijkstra's algorithm to find the shortest paths from a source vertex to all other vertices in a graph.
    /// </summary>
    public class Dijkstra
    {
        private readonly List<Edge> _edges;
        private readonly int _vertexCount;

        public Dijkstra(List<Edge> edges, int vertexCount)
        {
            _edges = edges;
            _vertexCount = vertexCount;
        }

        /// <summary>
        /// Finds the shortest paths from a source vertex to all other vertices using Dijkstra's algorithm.
        /// </summary>
        /// <param name="sourceVertex">The starting vertex.</param>
        /// <returns>A tuple containing the distances, parents, and steps of the algorithm.</returns>
        public (int[] dist, List<int>[] parents, List<int[]> steps) FindShortestPath(int sourceVertex)
        {
            var distances = new int[_vertexCount];
            var parents = new List<int>[_vertexCount];
            var visited = new bool[_vertexCount];
            var steps = new List<int[]>();

            for (int i = 0; i < _vertexCount; i++)
            {
                distances[i] = int.MaxValue;
                parents[i] = new List<int>();
            }

            distances[sourceVertex] = 0;
            var priorityQueue = new PriorityQueue<int, int>();
            priorityQueue.Enqueue(sourceVertex, 0);

            while (priorityQueue.Count > 0)
            {
                int u = priorityQueue.Dequeue();

                if (visited[u]) continue;
                visited[u] = true;

                foreach (var edge in _edges.Where(e => e.Vertex1 == u || (!e.IsDirected && e.Vertex2 == u)))
                {
                    int v = (edge.Vertex1 == u) ? edge.Vertex2 : edge.Vertex1;
                    int weight = edge.Weight;

                    if (!visited[v] && distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight;
                        parents[v].Clear();
                        parents[v].Add(u);
                        priorityQueue.Enqueue(v, distances[v]);
                    }
                    else if (!visited[v] && distances[u] + weight == distances[v])
                    {
                        parents[v].Add(u);
                    }
                }
                steps.Add((int[])distances.Clone());
            }

            return (distances, parents, steps);
        }
    }
}
