
namespace Đồ_Thị.Class
{
    public class Thisisonlytest(List<Vertex> vertices, List<Edge> edges)
    {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>(vertices);
        public List<Edge> Edges { get; set; } = new List<Edge>(edges);

        public void SaveAdjacencyMatrixToTxt(string filePath, int[,] adjacency)
        {
            int[,] adjacencyMatrix = adjacency;
            int n = adjacencyMatrix.GetLength(0);

            using StreamWriter writer = new(filePath);
            // Viết tên đỉnh lên dòng đầu tiên
            writer.Write(" \t");
            for (int i = 0; i < n; i++)
            {
                writer.Write(Vertices[i].Value + "\t");
            }
            writer.WriteLine();

            // Viết ma trận kề với tên đỉnh bên trái
            for (int i = 0; i < n; i++)
            {
                writer.Write(Vertices[i].Value + "\t");
                for (int j = 0; j < n; j++)
                {
                    writer.Write(adjacencyMatrix[i, j] + "\t");
                }
                writer.WriteLine();
            }
        }

        public void SaveWeightMatrixToTxt(string filePath, int[,] weight)
        {
            int[,] weightMatrix = weight;
            int n = weightMatrix.GetLength(0);

            using StreamWriter writer = new(filePath);
            // Viết tên đỉnh lên dòng đầu tiên
            writer.Write(" \t");
            for (int i = 0; i < n; i++)
            {
                writer.Write(Vertices[i].Value + "\t");
            }
            writer.WriteLine();

            // Viết ma trận kề với tên đỉnh bên trái
            for (int i = 0; i < n; i++)
            {
                writer.Write(Vertices[i].Value + "\t");
                for (int j = 0; j < n; j++)
                {
                    writer.Write(weightMatrix[i, j] + "\t");
                }
                writer.WriteLine();
            }
        }

        public void SaveMatrixToFile(int[,] matrix, bool isAdjacencyMatrix)
        {
            using SaveFileDialog saveFileDialog = new()
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                if (isAdjacencyMatrix)
                {
                    SaveAdjacencyMatrixToTxt(filePath, matrix);
                }
                else
                {
                    SaveWeightMatrixToTxt(filePath, matrix);
                }
            }
        }
    }
}