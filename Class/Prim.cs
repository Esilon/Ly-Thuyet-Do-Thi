using Đồ_Thị.Class;
namespace Đồ_Thị.uc
{
    public class Prim
    {
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;
        private readonly List<Edge> _minimumSpanningTree = [];
        private readonly bool[] _visited;

        public Prim(List<Vertex> vertices, List<Edge> edges)
        {
            _vertices = vertices;
            _edges = edges;
            _visited = new bool[vertices.Count];
        }

        public List<Edge> GetMinimumSpanningTree(int startVertex)
        {
            SortedSet<Edge> priorityQueue = new(Comparer<Edge>.Create((x, y) =>
            {
                return x.Weight == y.Weight
                    ? x.Vertex1.CompareTo(y.Vertex1)
                    : x.Weight.CompareTo(y.Weight);
            }));

            Visit(startVertex, priorityQueue);

            while (priorityQueue.Count > 0)
            {
                Edge? edge = priorityQueue.Min;
                _ = priorityQueue.Remove(edge);

                if (_visited[edge.Vertex1] && _visited[edge.Vertex2])
                    continue;

                _minimumSpanningTree.Add(edge);

                if (!_visited[edge.Vertex1])
                    Visit(edge.Vertex1, priorityQueue);

                if (!_visited[edge.Vertex2])
                    Visit(edge.Vertex2, priorityQueue);
            }

            return _minimumSpanningTree;
        }

        private void Visit(int vertex, SortedSet<Edge> priorityQueue)
        {
            _visited[vertex] = true;

            foreach (Edge edge in _edges)
            {
                if (edge.Vertex1 == vertex && !_visited[edge.Vertex2])
                    _ = priorityQueue.Add(edge);

                if (edge.Vertex2 == vertex && !_visited[edge.Vertex1])
                    _ = priorityQueue.Add(edge);
            }
        }
    }
}
