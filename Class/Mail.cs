namespace DoThi.Class
{
    /// <summary>
    /// Implements the Chinese Postman Problem algorithm to find the shortest path that traverses every edge of a graph at least once.
    /// </summary>
    public class ChinesePostman
    {
        public readonly List<Vertex> Vertices;
        private readonly List<Edge> _edges;
        private readonly List<Edge> _additionalEdges = new();
        private List<Vertex> _eulerianCycle = new();

        public ChinesePostman(List<Vertex> vertices, List<Edge> edges)
        {
            Vertices = vertices;
            _edges = edges;
        }

        public List<Edge> GetAdditionalEdges() => new List<Edge>(_additionalEdges);

        public Vertex? GetVertexById(int id) => Vertices.FirstOrDefault(v => Vertices.IndexOf(v) == id);

        public List<Vertex>? GetEulerianCycle() => _eulerianCycle.Count > 0 ? _eulerianCycle : null;

        public Edge? GetEdgeBetween(Vertex vertex1, Vertex vertex2)
        {
            if (_edges == null || vertex1 == null || vertex2 == null) return null;
            return _edges.FirstOrDefault(edge =>
                (edge.Vertex1 == Vertices.IndexOf(vertex1) && edge.Vertex2 == Vertices.IndexOf(vertex2)) ||
                (edge.Vertex1 == Vertices.IndexOf(vertex2) && edge.Vertex2 == Vertices.IndexOf(vertex1)));
        }

        public (string, string) Solve(Vertex startVertex)
        {
            // Step 1: Find vertices with odd degrees.
            var oddVertices = FindOddDegreeVertices();

            // Step 2: Compute shortest paths between all pairs of odd vertices.
            var distances = ComputeShortestPaths();

            // Step 3: Find the minimum weight perfect matching for the odd vertices.
            var pairingResults = CalculatePairingDistances(oddVertices, distances);

            // Step 4: Construct the message with the optimal pairings and distances.
            var message = "Optimal pairings and distances:\n";
            foreach (var result in pairingResults)
            {
                message += $"{result.Pairing} -> d({result.Pairing}) = {result.Distance}\n";
            }
            int minDistance = pairingResults.Min(p => p.Distance);
            message += $"Minimum total distance: {minDistance}";

            // Step 5: Add new edges to make the graph Eulerian.
            var optimalPair = pairingResults.First(p => p.Distance == minDistance);
            foreach (var pair in optimalPair.Pairs)
            {
                int v1Index = Vertices.IndexOf(pair.Item1);
                int v2Index = Vertices.IndexOf(pair.Item2);
                var shortestPath = FindShortestPath(v1Index, v2Index, distances);
                _additionalEdges.AddRange(shortestPath.Edges);
                _edges.AddRange(shortestPath.Edges);
            }

            // Step 6: Find the Eulerian cycle.
            _eulerianCycle = FindEulerianCycle(startVertex);

            // Step 7: Construct the Eulerian cycle message.
            var cycleMessage = "Eulerian Cycle: " + string.Join(" -> ", _eulerianCycle.Select(v => v.Value));

            return (message, cycleMessage);
        }

        private List<Vertex> FindOddDegreeVertices()
        {
            return Vertices.Where(v => _edges.Count(e => e.Vertex1 == Vertices.IndexOf(v) || e.Vertex2 == Vertices.IndexOf(v)) % 2 != 0).ToList();
        }

        private int[,] ComputeShortestPaths()
        {
            int n = Vertices.Count;
            var distances = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = i == j ? 0 : int.MaxValue;
                }
            }

            foreach (var edge in _edges)
            {
                distances[edge.Vertex1, edge.Vertex2] = edge.Weight;
                if (!edge.IsDirected)
                {
                    distances[edge.Vertex2, edge.Vertex1] = edge.Weight;
                }
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (distances[i, k] != int.MaxValue && distances[k, j] != int.MaxValue && distances[i, k] + distances[k, j] < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }
            return distances;
        }

        private List<PairingResult> CalculatePairingDistances(List<Vertex> oddVertices, int[,] distances)
        {
            var results = new List<PairingResult>();
            var pairs = GeneratePairs(oddVertices);

            foreach (var pair in pairs)
            {
                int totalDistance = 0;
                var pairing = "{" + string.Join(", ", pair.Select(p => $"({p.Item1.Value}, {p.Item2.Value})")) + "}";
                var newEdges = new List<Edge>();

                foreach (var p in pair)
                {
                    int v1Index = Vertices.IndexOf(p.Item1);
                    int v2Index = Vertices.IndexOf(p.Item2);
                    var (pathDistance, pathEdges) = FindShortestPath(v1Index, v2Index, distances);
                    totalDistance += pathDistance;
                    newEdges.AddRange(pathEdges);
                }
                results.Add(new PairingResult(pairing, totalDistance, pair, newEdges));
            }

            return results;
        }

        private (int TotalDistance, List<Edge> Edges) FindShortestPath(int startIndex, int endIndex, int[,] distances)
        {
            var pathEdges = new List<Edge>();
            int totalDistance = distances[startIndex, endIndex];
            if (totalDistance == int.MaxValue) return (totalDistance, pathEdges);

            var previous = new int[Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++) previous[i] = -1;

            var queue = new Queue<int>();
            queue.Enqueue(startIndex);
            var visited = new bool[Vertices.Count];
            visited[startIndex] = true;

            while (queue.Count > 0)
            {
                int currentIndex = queue.Dequeue();
                if (currentIndex == endIndex) break;

                for (int neighborIndex = 0; neighborIndex < Vertices.Count; neighborIndex++)
                {
                    if (distances[currentIndex, neighborIndex] != int.MaxValue && !visited[neighborIndex])
                    {
                        visited[neighborIndex] = true;
                        queue.Enqueue(neighborIndex);
                        previous[neighborIndex] = currentIndex;
                    }
                }
            }

            var path = new Stack<int>();
            int current = endIndex;
            while (current != -1)
            {
                path.Push(current);
                current = previous[current];
            }

            int previousVertex = -1;
            foreach (var vertex in path)
            {
                if (previousVertex != -1)
                {
                    var edge = _edges.FirstOrDefault(e =>
                        (e.Vertex1 == previousVertex && e.Vertex2 == vertex) ||
                        (e.Vertex2 == previousVertex && e.Vertex1 == vertex));
                    if (edge != null) pathEdges.Add(edge);
                }
                previousVertex = vertex;
            }

            return (totalDistance, pathEdges);
        }

        private List<List<Tuple<Vertex, Vertex>>> GeneratePairs(List<Vertex> vertices)
        {
            var results = new List<List<Tuple<Vertex, Vertex>>>();
            if (vertices.Count % 2 != 0) throw new InvalidOperationException("The number of odd degree vertices must be even.");

            GeneratePairsRecursive(vertices, new List<Tuple<Vertex, Vertex>>(), results);
            return results;
        }

        private void GeneratePairsRecursive(List<Vertex> vertices, List<Tuple<Vertex, Vertex>> currentPairs, List<List<Tuple<Vertex, Vertex>>> results)
        {
            if (vertices.Count == 0)
            {
                results.Add(new List<Tuple<Vertex, Vertex>>(currentPairs));
                return;
            }

            var first = vertices[0];
            var remaining = vertices.Skip(1).ToList();

            for (int i = 0; i < remaining.Count; i++)
            {
                var pair = new Tuple<Vertex, Vertex>(first, remaining[i]);
                currentPairs.Add(pair);
                var newRemaining = remaining.Where((_, index) => index != i).ToList();
                GeneratePairsRecursive(newRemaining, currentPairs, results);
                currentPairs.Remove(pair);
            }
        }

        private List<Vertex> FindEulerianCycle(Vertex startVertex)
        {
            var stack = new Stack<Vertex>();
            var cycle = new List<Vertex>();
            var current = startVertex;
            var tempEdges = new List<Edge>(_edges);

            while (stack.Count > 0 || tempEdges.Any(e => e.Vertex1 == Vertices.IndexOf(current) || e.Vertex2 == Vertices.IndexOf(current)))
            {
                var edge = tempEdges.FirstOrDefault(e => e.Vertex1 == Vertices.IndexOf(current) || e.Vertex2 == Vertices.IndexOf(current));
                if (edge != null)
                {
                    stack.Push(current);
                    tempEdges.Remove(edge);
                    current = edge.Vertex1 == Vertices.IndexOf(current) ? Vertices[edge.Vertex2] : Vertices[edge.Vertex1];
                }
                else
                {
                    cycle.Add(current);
                    current = stack.Count > 0 ? stack.Pop() : startVertex;
                }
            }
            cycle.Add(current);
            cycle.Reverse();
            return cycle;
        }
    }

    public class PairingResult
    {
        public string Pairing { get; }
        public int Distance { get; }
        public List<Tuple<Vertex, Vertex>> Pairs { get; }
        public List<Edge> NewEdges { get; }

        public PairingResult(string pairing, int distance, List<Tuple<Vertex, Vertex>> pairs, List<Edge> newEdges)
        {
            Pairing = pairing;
            Distance = distance;
            Pairs = pairs;
            NewEdges = newEdges;
        }
    }
}
