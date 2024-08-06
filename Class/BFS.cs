namespace Đồ_Thị.Class
{
    public class BFS(int[,] adjacencyMatrix, List<Vertex> vertices)
    {
        private readonly int[,] _adjacencyMatrix = adjacencyMatrix;
        private readonly List<Vertex> _vertices = vertices;

        public (List<int>, List<string>) PerformBFS(int startVertex)
        {
            int size = _adjacencyMatrix.GetLength(0);
            bool[] visited = new bool[size];
            List<int> result = [];
            List<string> adjacencyLog = [];
            Queue<int> queue = new();

            visited[startVertex] = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                int vertex = queue.Dequeue();
                result.Add(vertex);
                adjacencyLog.Add($"Đỉnh đang xét: {_vertices[vertex].Value}");

                for (int i = 0; i < size; i++)
                {
                    if (_adjacencyMatrix[vertex, i] != 0 && !visited[i])
                    {
                        adjacencyLog.Add($"Đỉnh {_vertices[vertex].Value} kề với đỉnh {_vertices[i].Value}");
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }

            return (result, adjacencyLog);
        }
    }
}
