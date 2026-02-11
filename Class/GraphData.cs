using System.Text.Json;
using System.Text.Json.Serialization;

namespace Đồ_Thị.Class
{
    public class GraphData
    {
        public required List<Vertex> Vertices { get; set; }
        public required List<Edge> Edges { get; set; }
        public required int[,] AdjacencyMatrix { get; set; }
        public required int[,] WeightMatrix { get; set; }

        private static JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new Array2DConverter(), new PointFConverter() }
            };
        }

        #region Save/Load
        public void SaveGraph()
        {
            using SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Graph";
            saveFileDialog.DefaultExt = "graph";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;

                try
                {
                    string json = JsonSerializer.Serialize(this, GetJsonOptions());
                    File.WriteAllText(fileName, json);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Lỗi khi lưu đồ thị: " + ex.Message);
                }
            }
        }

        public static GraphData LoadGraph(string fileName)
        {
            GraphData loadedData;

            try
            {
                string json = File.ReadAllText(fileName);
                loadedData = JsonSerializer.Deserialize<GraphData>(json, GetJsonOptions());
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Lỗi khi tải đồ thị: " + ex.Message);
                return null;
            }

            return loadedData;
        }
        public static GraphData OpenGraphFile()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*",
                Title = "Open Graph File"
            };
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    return LoadGraph(fileName);
                }
                return null;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Error loading graph: " + ex.Message);
                return null;
            }
        }
    }
    #endregion
}
