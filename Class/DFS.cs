namespace Đồ_Thị.Class
{
    public class DFS
    {
        private readonly int[,] _adjacencyMatrix;
        private bool[] _visited;
        private List<int> _result;
        public DFS(int[,] adjacencyMatrix)
        {
            _adjacencyMatrix = adjacencyMatrix;
        }

        public List<int> PerformDFS(int startVertex)
        {
            int size = _adjacencyMatrix.GetLength(0);
            _visited = new bool[size];
            _result = [];
            DFSUtil(startVertex);
            return _result;
        }

        private void DFSUtil(int vertex)
        {
            _visited[vertex] = true;
            _result.Add(vertex);

            for (int i = 0; i < _adjacencyMatrix.GetLength(0); i++)
            {
                if (_adjacencyMatrix[vertex, i] != 0 && !_visited[i])
                {
                    DFSUtil(i);
                }
            }
        }
    }
}
