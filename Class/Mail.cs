namespace Đồ_Thị.Class
{
    public class Mail(List<Vertex> vertices, List<Edge> edges)
    {
        private List<Vertex> vertices = vertices;
        private List<Edge> edges = edges;
        private List<Edge> additionalEllipseEdges = []; // Biến mới để lưu các cạnh ellipse
        // Phương pháp chính để giải bài toán Người đưa thư Trung Hoa
        public List<Edge> GetAddEdge()
        {
            return additionalEllipseEdges;
        }
        public (string, string) SolveChinesePostmanProblem(Vertex startVertex)
        {
            // Bước 1: Tìm các đỉnh có bậc lẻ
            List<Vertex> oddVertices = FindOddDegreeVertices();

            // Bước 2: Tính khoảng cách ngắn nhất giữa các đỉnh
            int[,] distances = ComputeShortestPaths();

            // Bước 3: Tìm các cặp tối ưu cho các đỉnh bậc lẻ và hiển thị kết quả
            var pairingResults = CalculatePairingDistances(oddVertices, distances);

            // Hiển thị kết quả các phân hoạch và khoảng cách
            string message = "Các cặp tối ưu và khoảng cách:\n";
            foreach (var result in pairingResults)
            {
                message += $"{result.Pairing} -> d({result.Pairing}) = {result.Distance}\n";
            }
            int minDistance = pairingResults.Min(p => p.Distance);
            message += $"Tổng khoảng cách nhỏ nhất: {minDistance}";

            // Bước 5: Thêm các cạnh mới để làm đồ thị thành Eulerian
            var optimalPair = pairingResults.First(p => p.Distance == minDistance);

            foreach (var pair in optimalPair.Pairs)
            {
                int v1Index = vertices.IndexOf(pair.Item1);
                int v2Index = vertices.IndexOf(pair.Item2);
                int distance = distances[v1Index, v2Index];
                var shortestPath = FindShortestPath(v1Index, v2Index, distances);
                additionalEllipseEdges.AddRange(shortestPath.Edges);
                foreach (var edge in shortestPath.Edges)
                {
                    edges.Add(edge);
                }
            }

            // Tìm chu trình Eulerian từ một đỉnh bất kỳ
            List<Vertex> eulerianCycle = FindEulerianCycle(startVertex);

            // Hiển thị chu trình Eulerian
            string cycleMessage = "Chu trình Eulerian: ";
            foreach (var vertex in eulerianCycle)
            {
                cycleMessage += vertex.Value + " -> ";
            }
            cycleMessage = cycleMessage.TrimEnd(' ', '-', '>');

            return (message, cycleMessage);
        }

        // Phương pháp tìm các đỉnh có bậc lẻ trong đồ thị
        private List<Vertex> FindOddDegreeVertices()
        {
            List<Vertex> oddVertices = [];
            foreach (var vertex in vertices)
            {
                int degree = edges.Count(e => e.Vertex1 == vertices.IndexOf(vertex) || e.Vertex2 == vertices.IndexOf(vertex));
                if (degree % 2 != 0)
                {
                    oddVertices.Add(vertex);
                }
            }
            return oddVertices;
        }

        // Phương pháp tính khoảng cách ngắn nhất giữa các đỉnh
        private int[,] ComputeShortestPaths()
        {
            int n = vertices.Count;
            int[,] distances = new int[n, n];

            bool isWeighted = edges.Any(e => e.Weight != 0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = i == j ? 0 : int.MaxValue;
                }
            }

            if (isWeighted) // Dùng thuật toán Dijkstra
            {
                for (int i = 0; i < n; i++)
                {
                    Dijkstra(i, distances);
                }
            }
            else // Dùng thuật toán BFS
            {
                for (int i = 0; i < n; i++)
                {
                    BFS(i, distances);
                }
            }

            return distances;
        }

        private void Dijkstra(int startIndex, int[,] distances)
        {
            int n = vertices.Count;
            bool[] visited = new bool[n];
            int[] minDistances = new int[n];

            for (int i = 0; i < n; i++)
            {
                minDistances[i] = int.MaxValue;
            }
            minDistances[startIndex] = 0;

            var priorityQueue = new SortedSet<(int Distance, int Vertex)>
            {
                (0, startIndex)
            };

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentIndex) = priorityQueue.First();
                priorityQueue.Remove(priorityQueue.First());

                if (visited[currentIndex])
                {
                    continue;
                }
                visited[currentIndex] = true;

                foreach (var edge in edges.Where(e => e.Vertex1 == currentIndex || e.Vertex2 == currentIndex))
                {
                    int neighborIndex = edge.Vertex1 == currentIndex ? edge.Vertex2 : edge.Vertex1;

                    if (visited[neighborIndex])
                    {
                        continue;
                    }

                    int newDistance = currentDistance + edge.Weight;
                    if (newDistance < minDistances[neighborIndex])
                    {
                        priorityQueue.Remove((minDistances[neighborIndex], neighborIndex));
                        minDistances[neighborIndex] = newDistance;
                        priorityQueue.Add((newDistance, neighborIndex));
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                distances[startIndex, i] = minDistances[i];
            }
        }

        private void BFS(int startIndex, int[,] distances)
        {
            int n = vertices.Count;
            bool[] visited = new bool[n];
            Queue<(int Vertex, int Distance)> queue = new Queue<(int Vertex, int Distance)>();

            queue.Enqueue((startIndex, 0));
            visited[startIndex] = true;

            while (queue.Count > 0)
            {
                var (currentIndex, currentDistance) = queue.Dequeue();

                foreach (var edge in edges.Where(e => e.Vertex1 == currentIndex || e.Vertex2 == currentIndex))
                {
                    int neighborIndex = edge.Vertex1 == currentIndex ? edge.Vertex2 : edge.Vertex1;

                    if (visited[neighborIndex])
                    {
                        continue;
                    }

                    visited[neighborIndex] = true;
                    distances[startIndex, neighborIndex] = currentDistance + 1;
                    queue.Enqueue((neighborIndex, currentDistance + 1));
                }
            }
        }

        // Phương pháp sinh các cặp đỉnh lẻ và tính phân hoạch khoảng cách giữa các cặp đỉnh lẻ
        private List<PairingResult> CalculatePairingDistances(List<Vertex> oddVertices, int[,] distances)
        {
            var results = new List<PairingResult>();
            var pairs = GeneratePairs(oddVertices);

            foreach (var pair in pairs)
            {
                int totalDistance = 0;
                string pairing = "{";
                List<Edge> newEdges = new List<Edge>();

                foreach (var p in pair)
                {
                    int v1Index = vertices.IndexOf(p.Item1);
                    int v2Index = vertices.IndexOf(p.Item2);

                    var (TotalDistance, Edges) = FindShortestPath(v1Index, v2Index, distances);
                    totalDistance += TotalDistance;
                    newEdges.AddRange(Edges);

                    pairing += $"({p.Item1.Value}, {p.Item2.Value}), ";
                }
                pairing = pairing.TrimEnd(',', ' ') + "}";

                results.Add(new PairingResult(pairing, totalDistance, pair, newEdges));
            }

            return results;
        }
        // Tìm đỉnh tới đỉnh đích thông qua từng đỉnh trung gian với khoảng cách nhỏ nhất
        private (int TotalDistance, List<Edge> Edges) FindShortestPath(int startIndex, int endIndex, int[,] distances)
        {
            List<Edge> pathEdges = new List<Edge>();
            int totalDistance = distances[startIndex, endIndex];

            if (totalDistance == int.MaxValue)
            {
                // Không có đường đi giữa các đỉnh
                return (totalDistance, pathEdges);
            }

            // Khởi tạo đồ thị phụ để theo dõi các cạnh trong đường đi ngắn nhất
            Dictionary<int, List<Edge>> adjList = new Dictionary<int, List<Edge>>();
            foreach (var edge in edges)
            {
                if (!adjList.ContainsKey(edge.Vertex1))
                    adjList[edge.Vertex1] = new List<Edge>();
                if (!adjList.ContainsKey(edge.Vertex2))
                    adjList[edge.Vertex2] = new List<Edge>();

                adjList[edge.Vertex1].Add(edge);
                adjList[edge.Vertex2].Add(edge);
            }

            // Khởi tạo mảng để theo dõi đường đi từ đỉnh bắt đầu
            int[] previous = new int[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
                previous[i] = -1;

            // Khởi tạo hàng đợi và mảng khoảng cách
            var queue = new Queue<int>();
            queue.Enqueue(startIndex);
            bool[] visited = new bool[vertices.Count];
            visited[startIndex] = true;

            // Khởi tạo mảng khoảng cách
            int[] minDistances = new int[vertices.Count];
            Array.Fill(minDistances, int.MaxValue);
            minDistances[startIndex] = 0;

            while (queue.Count > 0)
            {
                int currentIndex = queue.Dequeue();

                if (currentIndex == endIndex)
                    break;

                foreach (var edge in adjList[currentIndex])
                {
                    int neighborIndex = edge.Vertex1 == currentIndex ? edge.Vertex2 : edge.Vertex1;

                    if (!visited[neighborIndex])
                    {
                        visited[neighborIndex] = true;
                        queue.Enqueue(neighborIndex);
                        minDistances[neighborIndex] = minDistances[currentIndex] + edge.Weight;
                        previous[neighborIndex] = currentIndex;
                    }
                }
            }

            // Xây dựng danh sách các cạnh của đường đi ngắn nhất
            Stack<int> path = new Stack<int>();
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
                    var edge = edges.FirstOrDefault(e =>
                        (e.Vertex1 == previousVertex && e.Vertex2 == vertex) ||
                        (e.Vertex2 == previousVertex && e.Vertex1 == vertex));

                    if (edge != null)
                    {
                        pathEdges.Add(edge);
                    }
                }
                previousVertex = vertex;
            }

            return (totalDistance, pathEdges);
        }



        // Phương pháp sinh các cặp đỉnh lẻ
        private List<List<Tuple<Vertex, Vertex>>> GeneratePairs(List<Vertex> vertices)
        {
            var results = new List<List<Tuple<Vertex, Vertex>>>();

            if (vertices.Count % 2 != 0)
            {
                throw new InvalidOperationException("Số lượng đỉnh bậc lẻ phải là số chẵn.");
            }

            GeneratePairsRecursive(vertices, [], results);

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

        // Phương pháp tìm chu trình Eulerian
        private List<Vertex> FindEulerianCycle(Vertex startVertex)
        {
            var stack = new Stack<Vertex>();
            var cycle = new List<Vertex>();
            var current = startVertex;
            var newEdges = new List<Edge>(edges);

            while (stack.Count > 0 || newEdges.Count > 0)
            {
                var edge = newEdges.FirstOrDefault(e => e.Vertex1 == vertices.IndexOf(current) || e.Vertex2 == vertices.IndexOf(current));
                if (edge != null)
                {
                    stack.Push(current);
                    newEdges.Remove(edge);

                    current = edge.Vertex1 == vertices.IndexOf(current) ? vertices.Find(v => vertices.IndexOf(v) == edge.Vertex2) : vertices.Find(v => vertices.IndexOf(v) == edge.Vertex1);
                }
                else
                {
                    cycle.Add(current);
                    current = stack.Count > 0 ? stack.Pop() : startVertex;
                }
            }

            cycle.Add(current);
            return cycle;
        }

    }
    public class PairingResult(string pairing, int distance, List<Tuple<Vertex, Vertex>> pairs, List<Edge> newEdges)
    {
        public string Pairing { get; } = pairing;
        public int Distance { get; } = distance;
        public List<Tuple<Vertex, Vertex>> Pairs { get; } = pairs;
        public List<Edge> NewEdges { get; } = newEdges;
    }
}
