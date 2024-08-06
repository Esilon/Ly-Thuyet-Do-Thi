using Đồ_Thị.Class;

public class Dijkstra
{
    private readonly List<Vertex> _vertices;
    private readonly List<Edge> _edges;
    private readonly int _vertexCount;

    public Dijkstra(List<Vertex> vertices, List<Edge> edges, int vertexCount)
    {
        _vertices = vertices;
        _edges = edges;
        _vertexCount = vertexCount;
    }
    // Tìm các cặp cạnh có đường đi nhỏ nhất tới tất cả các đỉnh khác.
    public (int[] dist, List<int>[] parents, List<int[]> steps) DijkstraShortestPath(int sourceVertex)
    {
        int[] dist = new int[_vertexCount];
        bool[] sptSet = new bool[_vertexCount];
        List<int>[] parents = new List<int>[_vertexCount];
        List<int[]> steps = new();

        for (int i = 0; i < _vertexCount; i++)
        {
            dist[i] = int.MaxValue;
            sptSet[i] = false;
            parents[i] = [];
        }

        dist[sourceVertex] = 0;

        for (int count = 0; count < _vertexCount - 1; count++)
        {
            int u = MinDistance(dist, sptSet);
            sptSet[u] = true;

            foreach (Edge edge in _edges)
            {
                int v = -1;
                if (edge.Vertex1 == u)
                    v = edge.Vertex2;
                else if (!edge.IsDirected && edge.Vertex2 == u)
                    v = edge.Vertex1;

                if (v != -1 && !sptSet[v] && dist[u] != int.MaxValue && dist[u] + edge.Weight < dist[v])
                {
                    dist[v] = dist[u] + edge.Weight;
                    parents[v].Clear();
                    parents[v].Add(u);
                }
                else if (v != -1 && !sptSet[v] && dist[u] + edge.Weight == dist[v])
                {
                    parents[v].Add(u);
                }
            }

            steps.Add((int[])dist.Clone());
        }

        return (dist, parents, steps);
    }




    private int MinDistance(int[] dist, bool[] sptSet)
    {
        int min = int.MaxValue, minIndex = -1;

        for (int v = 0; v < _vertexCount; v++)
        {
            if (!sptSet[v] && dist[v] <= min)
            {
                min = dist[v];
                minIndex = v;
            }
        }

        return minIndex;
    }
    public void DijkstraTest(int startVertex, List<Vertex> vertices, List<Edge> edges)
    {
        int vertexCount = vertices.Count;
        int[] distances = new int[vertexCount];
        List<int>[] parents = new List<int>[vertexCount];
        List<int[]> steps = new();

        for (int i = 0; i < vertexCount; i++)
        {
            distances[i] = int.MaxValue;
            parents[i] = new List<int> { -1 };
        }

        distances[startVertex] = 0;

        bool[] visited = new bool[vertexCount];
        PriorityQueue<int, int> pq = new();

        pq.Enqueue(startVertex, 0);

        while (pq.Count > 0)
        {
            int u = pq.Dequeue();
            if (visited[u])
                continue;

            visited[u] = true;

            foreach (Edge? edge in edges.Where(e => e.Vertex1 == u || (!e.IsDirected && e.Vertex2 == u)))
            {
                int v = (edge.Vertex1 == u) ? edge.Vertex2 : edge.Vertex1;
                int weight = edge.Weight;

                if (!visited[v] && distances[u] + weight < distances[v])
                {
                    distances[v] = distances[u] + weight;
                    parents[v] = [u];
                    pq.Enqueue(v, distances[v]);
                }
                else if (distances[u] + weight == distances[v])
                {
                    parents[v].Add(u);
                }
            }

            int[] stepSnapshot = new int[vertexCount];
            Array.Copy(distances, stepSnapshot, vertexCount);
            steps.Add(stepSnapshot);
        }

        Prompt.DisplayDijkstraSteps("Dijkstra Steps", distances, parents, steps, vertices);
    }
}
