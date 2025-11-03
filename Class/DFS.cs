namespace DoThi.Class
{
    /// <summary>
    /// Performs a depth-first search (DFS) on a graph represented by an adjacency matrix.
    /// </summary>
    public class DFS
    {
        private readonly int[,] _adjacencyMatrix;
        private bool[]? _visited;
        private List<int> _result;

        public DFS(int[,] adjacencyMatrix)
        {
            _adjacencyMatrix = adjacencyMatrix;
            _result = new List<int>();
        }

        /// <summary>
        /// Performs a depth-first search starting from the specified vertex.
        /// </summary>
        /// <param name="startVertex">The starting vertex for the DFS.</param>
        /// <returns>A list of vertices in the order they were visited.</returns>
        public List<int> Perform(int startVertex)
        {
            int size = _adjacencyMatrix.GetLength(0);
            _visited = new bool[size];
            _result.Clear();
            DfsUtil(startVertex);
            return _result;
        }

        private void DfsUtil(int vertex)
        {
            _visited[vertex] = true;
            _result.Add(vertex);

            for (int i = 0; i < _adjacencyMatrix.GetLength(0); i++)
            {
                if (_adjacencyMatrix[vertex, i] != 0 && !_visited[i])
                {
                    DfsUtil(i);
                }
            }
        }
    }
}
