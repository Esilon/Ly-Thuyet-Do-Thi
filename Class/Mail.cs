namespace Đồ_Thị.Class
{
    public class Mail(List<Vertex> vertices, List<Edge> edges)
    {
        private readonly List<Vertex> vertices = vertices;
        private readonly List<Edge> edges = edges;

        // Phương pháp chính để giải bài toán Người đưa thư Trung Hoa
        public void SolveChinesePostmanProblem(Vertex startVertex)
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

            MessageBox.Show(message, "Kết quả các cặp tối ưu");

            // Bước 5: Thêm các cạnh mới để làm đồ thị thành Eulerian
            var optimalPair = pairingResults.First(p => p.Distance == minDistance);
            foreach (var pair in optimalPair.Pairs)
            {
                int v1Index = vertices.IndexOf(pair.Item1);
                int v2Index = vertices.IndexOf(pair.Item2);
                int distance = distances[v1Index, v2Index];
                edges.Add(new Edge(v1Index, v2Index, distance, false));
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

            MessageBox.Show(cycleMessage, "Chu trình Eulerian");
        }

        //  Phương pháp tìm các đỉnh có bậc lẻ trong đồ thị
        private List<Vertex> FindOddDegreeVertices()
        {
            // Tìm các đỉnh có bậc lẻ trong đồ thị
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

            // Kiểm tra đồ thị có trọng số hay không
            bool isWeighted = edges.Any(e => e.Weight != 0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = i == j ? 0 : int.MaxValue;
                }
            }

            if (isWeighted)
            {
                // Sử dụng Dijkstra cho đồ thị có trọng số
                for (int i = 0; i < n; i++)
                {
                    Dijkstra(i, distances);
                }
            }
            else
            {
                // Sử dụng BFS cho đồ thị không trọng số
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

                foreach (var edge in edges.Where(e => e.Vertex1 == vertices[currentIndex].GetHashCode() || e.Vertex2 == vertices[currentIndex].GetHashCode()))
                {
                    int neighborIndex = edge.Vertex1 == vertices[currentIndex].GetHashCode()
                        ? vertices.FindIndex(v => v.GetHashCode() == edge.Vertex2)
                        : vertices.FindIndex(v => v.GetHashCode() == edge.Vertex1);

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
            Queue<(int Vertex, int Distance)> queue = new();

            queue.Enqueue((startIndex, 0));
            visited[startIndex] = true;

            while (queue.Count > 0)
            {
                var (currentIndex, currentDistance) = queue.Dequeue();

                foreach (var edge in edges.Where(e => e.Vertex1 == vertices.IndexOf(vertices[currentIndex]) || e.Vertex2 == vertices.IndexOf(vertices[currentIndex])))
                {
                    int neighborIndex = edge.Vertex1 == vertices.IndexOf(vertices[currentIndex])
                        ? vertices.FindIndex(v => vertices.IndexOf(v) == edge.Vertex2)
                        : vertices.FindIndex(v => vertices.IndexOf(v) == edge.Vertex1);

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
                foreach (var p in pair)
                {
                    int v1Index = vertices.IndexOf(p.Item1);
                    int v2Index = vertices.IndexOf(p.Item2);
                    int distance = distances[v1Index, v2Index];
                    totalDistance += distance;
                    pairing += $"({p.Item1.Value}, {p.Item2.Value}), ";
                }
                pairing = pairing.TrimEnd(',', ' ') + "}";
                results.Add(new PairingResult(pairing, totalDistance, pair));
            }

            return results;
        }

        // Phương pháp sinh các cặp đỉnh lẻ
        private List<List<Tuple<Vertex, Vertex>>> GeneratePairs(List<Vertex> vertices)
        {
            var results = new List<List<Tuple<Vertex, Vertex>>>();

            // Sinh tất cả các cặp có thể từ các đỉnh bậc lẻ
            if (vertices.Count % 2 != 0)
            {
                throw new InvalidOperationException("Số lượng đỉnh bậc lẻ phải là số chẵn.");
            }

            GeneratePairsRecursive(vertices, [], results);
            return results;
        }

        private static void GeneratePairsRecursive(List<Vertex> vertices, List<Tuple<Vertex, Vertex>> current, List<List<Tuple<Vertex, Vertex>>> results)
        {
            if (vertices.Count == 0)
            {
                results.Add(new List<Tuple<Vertex, Vertex>>(current));
                return;
            }

            Vertex first = vertices[0];
            for (int i = 1; i < vertices.Count; i++)
            {
                var pair = Tuple.Create(first, vertices[i]);
                current.Add(pair);

                var remaining = vertices.Where((v, index) => index != 0 && index != i).ToList();
                GeneratePairsRecursive(remaining, current, results);

                current.Remove(pair);
            }
        }

        private class PairingResult(string pairing, int distance, List<Tuple<Vertex, Vertex>> pairs)
        {
            public string Pairing { get; } = pairing;
            public int Distance { get; } = distance;
            public List<Tuple<Vertex, Vertex>> Pairs { get; } = pairs;
        }
        private List<Vertex> FindEulerianCycle(Vertex startVertex)
        {
            // Thuật toán Hierholzer để tìm chu trình Eulerian
            List<Vertex> cycle = [];
            Stack<Vertex> stack = new();
            Dictionary<Vertex, List<Edge>> adjList = [];

            // Tạo danh sách kề cho mỗi đỉnh
            foreach (var vertex in vertices)
            {
                adjList[vertex] = [];
            }

            // Thêm các cạnh vào danh sách kề của mỗi đỉnh
            foreach (var edge in edges)
            {
                Vertex v1 = vertices.First(v => vertices.IndexOf(v) == edge.Vertex1);
                Vertex v2 = vertices.First(v => vertices.IndexOf(v) == edge.Vertex2);
                adjList[v1].Add(edge);
                if (!edge.IsDirected)
                {
                    adjList[v2].Add(edge);
                }
            }

            stack.Push(startVertex);
            while (stack.Count > 0)
            {
                Vertex v = stack.Peek();
                if (adjList[v].Count > 0)
                {
                    Edge edge = adjList[v][0];
                    adjList[v].Remove(edge);

                    Vertex nextVertex = vertices.FirstOrDefault(ver => vertices.IndexOf(ver) == (vertices.IndexOf(v) == edge.Vertex1 ? edge.Vertex2 : edge.Vertex1));
                    if (nextVertex != null)
                    {
                        stack.Push(nextVertex);
                    }
                }
                else
                {
                    cycle.Add(v);
                    stack.Pop();
                }
            }

            cycle.Reverse(); // Đảo ngược chu trình để có chu trình đúng thứ tự
            return cycle;
        }

    }
}
