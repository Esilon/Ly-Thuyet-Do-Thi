using System.Runtime.Serialization.Formatters.Binary;

namespace DoThi.Class
{
#pragma warning disable SYSLIB0011
    [Serializable]
    public class GraphData
    {
        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }
        public int[,] AdjacencyMatrix { get; set; }
        public int[,] WeightMatrix { get; set; }

        #region Save/Load

        public void Save()
        {
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*",
                Title = "Save Graph",
                DefaultExt = "graph",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                using Stream stream = File.Open(saveFileDialog.FileName, FileMode.Create);
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving graph: " + ex.Message);
            }
        }

        public static GraphData? Load(string fileName)
        {
            try
            {
                using Stream stream = File.Open(fileName, FileMode.Open);
                var binaryFormatter = new BinaryFormatter();
                return (GraphData)binaryFormatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading graph: " + ex.Message);
                return null;
            }
        }

        public static GraphData? OpenFile()
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*",
                Title = "Open Graph File"
            };

            return openFileDialog.ShowDialog() == DialogResult.OK ? Load(openFileDialog.FileName) : null;
        }

        #endregion
    }
}
