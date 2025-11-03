namespace DoThi.Class
{
    /// <summary>
    /// Provides functionality to export graph matrices to text files.
    /// </summary>
    public class GraphExporter
    {
        private readonly List<Vertex> _vertices;

        public GraphExporter(List<Vertex> vertices)
        {
            _vertices = vertices;
        }

        /// <summary>
        /// Saves a matrix to a text file with vertex labels as headers.
        /// </summary>
        /// <param name="filePath">The path to save the file to.</param>
        /// <param name="matrix">The matrix to save.</param>
        private void SaveMatrixToTxt(string filePath, int[,] matrix)
        {
            int n = matrix.GetLength(0);

            using var writer = new StreamWriter(filePath);
            // Write column headers.
            writer.Write(" \t");
            for (int i = 0; i < n; i++)
            {
                writer.Write(_vertices[i].Value + "\t");
            }
            writer.WriteLine();

            // Write rows with headers.
            for (int i = 0; i < n; i++)
            {
                writer.Write(_vertices[i].Value + "\t");
                for (int j = 0; j < n; j++)
                {
                    writer.Write(matrix[i, j] + "\t");
                }
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Opens a save file dialog and saves the specified matrix to the selected file.
        /// </summary>
        /// <param name="matrix">The matrix to save.</param>
        public void SaveMatrixToFile(int[,] matrix)
        {
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveMatrixToTxt(saveFileDialog.FileName, matrix);
            }
        }
    }
}
