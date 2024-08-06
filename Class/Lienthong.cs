namespace Đồ_Thị.Class
{

    internal class Lienthong(List<Vertex> vertices, List<Edge> edges)
    {
        private readonly List<Vertex> _vertices = vertices;
        private readonly List<Edge> _edges = edges;

        public bool CheckLienThong()
        {
            if (_vertices.Count == 0)
                return false;

            HashSet<int> visited = [];
            Stack<int> stack = new();
            stack.Push(0);

            while (stack.Count > 0)
            {
                int current = stack.Pop();

                if (!visited.Contains(current))
                {
                    visited.Add(current);

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

        private List<int> GetNeighbors(int vertexIndex)
        {
            List<int> neighbors = [];

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
