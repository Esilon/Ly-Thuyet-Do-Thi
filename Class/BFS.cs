namespace DoThi.Class
{
    public class BFS
    {
        private readonly int[,] _adjacencyMatrix;
        private readonly List<Vertex> _vertices;

        public BFS(int[,] adjacencyMatrix, List<Vertex> vertices)
        {
            _adjacencyMatrix = adjacencyMatrix;
            _vertices = vertices;
        }

        public (List<int>, List<string>) Perform(int startVertex)
        {
            int size = _adjacencyMatrix.GetLength(0);
            var visited = new bool[size];
            var result = new List<int>();
            var adjacencyLog = new List<string>();
            var queue = new Queue<int>();

            visited[startVertex] = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                int vertex = queue.Dequeue();
                result.Add(vertex);
                adjacencyLog.Add($"Considering vertex: {_vertices[vertex].Value}");

                for (int i = 0; i < size; i++)
                {
                    if (_adjacencyMatrix[vertex, i] != 0 && !visited[i])
                    {
                        adjacencyLog.Add($"Vertex {_vertices[vertex].Value} is adjacent to vertex {_vertices[i].Value}");
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }

            return (result, adjacencyLog);
        }
    }
}
