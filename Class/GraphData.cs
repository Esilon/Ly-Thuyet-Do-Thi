using System.Runtime.Serialization.Formatters.Binary;
namespace Đồ_Thị.Class
{
#pragma warning disable SYSLIB0011
    [Serializable]
    public class GraphData
    {
        public required List<Vertex> Vertices { get; set; }
        public required List<Edge> Edges { get; set; }
        public required int[,] AdjacencyMatrix { get; set; }
        public required int[,] WeightMatrix { get; set; }

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
                    using Stream stream = File.Open(fileName, FileMode.Create);
                    BinaryFormatter bformatter = new();
                    bformatter.Serialize(stream, this);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Lỗi khi lưu đồ thị: " + ex.Message);
                }
            }
        }

        public static GraphData LoadGraph(string fileName)
        {
            GraphData loadedData = null;

            try
            {
                using Stream stream = File.Open(fileName, FileMode.Open);
                BinaryFormatter bformatter = new();
                loadedData = (GraphData)bformatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Lỗi khi tải đồ thị: " + ex.Message);
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
