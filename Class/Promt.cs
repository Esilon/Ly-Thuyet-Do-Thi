namespace Đồ_Thị.Class
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new()
            {
                Width = 180,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };
            prompt.StartPosition = FormStartPosition.CenterParent;
            prompt.MinimizeBox = false;
            Label textLabel = new() { Left = 35, Top = 20, Text = text };
            TextBox inputBox = new() { Left = 20, Top = 40, Width = 120, TextAlign = System.Windows.Forms.HorizontalAlignment.Center };
            Button confirmation = new() { Text = caption, Left = 30, Width = 90, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
        }
        public static string? ChangeValueDialog(string text, string caption, string value)
        {
            Form prompt = new()
            {
                Width = 260,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new() { Left = 50, Top = 20, Text = text };
            TextBox inputBox = new() { Left = 50, Top = 50, Width = 150, Text = value, TextAlign = System.Windows.Forms.HorizontalAlignment.Center };
            Button confirmation = new() { Text = caption, Left = 20, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new() { Text = "Thoát", Left = 140, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
        }
        public static (string, bool)? ChangeValueDialog(string text, string caption, string value, bool allowEdgeTypeSelection)
        {
            Form prompt = new()
            {
                Width = 240,
                Height = 190,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new() { Left = 65, Top = 20, Text = text, TextAlign = ContentAlignment.MiddleCenter };
            TextBox inputBox = new() { Left = 55, Top = 50, Width = 120, Text = value, TextAlign = System.Windows.Forms.HorizontalAlignment.Center };

            Button confirmation = new() { Text = caption, Left = 30, Width = 70, Top = 120, DialogResult = DialogResult.OK };
            Button cancel = new() { Text = "Thoát", Left = 125, Width = 70, Top = 120, DialogResult = DialogResult.Cancel };

            RadioButton directedRadioButton = new() { Text = "Có hướng", Left = 20, Top = 80 };
            RadioButton undirectedRadioButton = new() { Text = "Vô hướng", Left = 130, Top = 80 };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(directedRadioButton);
            prompt.Controls.Add(undirectedRadioButton);
            prompt.AcceptButton = confirmation;
            if (allowEdgeTypeSelection)
                directedRadioButton.Checked = true;
            else
                undirectedRadioButton.Checked = true;

            DialogResult result = prompt.ShowDialog();

            if (result == DialogResult.OK)
            {
                bool isDirected = directedRadioButton.Checked;
                return (inputBox.Text, isDirected);
            }

            return null;
        }
        public static void DisplayKruskalSteps(string caption, List<Edge> minimumSpanningTree, List<Vertex> vertices)
        {
            using Form prompt = new();
            prompt.Width = 250;
            prompt.Height = 400;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            DataGridView dataGridView = new()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            int verticesCount = vertices.Count;
            dataGridView.ColumnCount = 2;
            dataGridView.Columns[0].Name = "Cạnh";
            dataGridView.Columns[1].Name = "Trọng số";

            int stepNumber = 0;
            foreach (Edge edge in minimumSpanningTree)
            {
                string[] row = [$"{vertices[edge.Vertex1].Value} - {vertices[edge.Vertex2].Value}", edge.Weight.ToString()];
                _ = dataGridView.Rows.Add(row);
                stepNumber++;
            }

            prompt.Controls.Add(dataGridView);
            _ = prompt.ShowDialog();
        }
        public static void DisplayPrimSteps(string caption, List<Edge> minimumSpanningTree, List<Vertex> vertices)
        {
            using Form prompt = new();
            prompt.Width = 1200;
            prompt.Height = 800;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            DataGridView dataGridView = new()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dataGridView.Columns.Add("Step", "Bước lặp");
            for (int i = 0; i < vertices.Count; i++)
            {
                dataGridView.Columns.Add($"Vertex{i + 1}","Đỉnh"+ vertices[i].Value);
            }
            dataGridView.Columns.Add("VH", "VH"); // Các đỉnh đã chọn
            dataGridView.Columns.Add("T", "T"); // Các cạnh trong cây khung
            prompt.Controls.Add(dataGridView);
            PrimTable.RunPrimAlgorithm(vertices, minimumSpanningTree, dataGridView);
            prompt.ShowDialog();
        }
        public static void DisplayDijkstraSteps(string caption, int[] distances, List<int>[] parents, List<int[]> steps, List<Vertex> vertices)
        {
            using Form prompt = new();
            prompt.Width = 1000;
            prompt.Height = 600;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            DataGridView dataGridView = new()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            int verticesCount = vertices.Count;
            dataGridView.ColumnCount = verticesCount + 1;
            dataGridView.Columns[0].Name = "Bước";

            for (int i = 1; i <= verticesCount; i++)
            {
                dataGridView.Columns[i].Name = vertices[i - 1].Value;
            }

            bool[] isConfirmed = new bool[verticesCount];
            bool[] isStarred = new bool[verticesCount];

            int stepNumber = 0;
            foreach (int[] step in steps)
            {
                string[] row = new string[verticesCount + 1];
                row[0] = (stepNumber + 1).ToString();

                for (int i = 0; i < verticesCount; i++)
                {
                    if (step[i] == int.MaxValue)
                    {
                        row[i + 1] = "(∞, -)";
                    }
                    else
                    {
                        if (isConfirmed[i])
                        {
                            row[i + 1] = "{---}";
                        }
                        else
                        {
                            List<string> parentStrList = [];
                            foreach (int parent in parents[i])
                            {
                                string parentStr = parent == -1 ? "-" : vertices[parent].Value;
                                parentStrList.Add($"({step[i]}, {parentStr})");
                            }
                            string parentStrConcat = string.Join(", ", parentStrList);

                            row[i + 1] = parentStrConcat;

                            if (distances[i] == step[i] && !isStarred[i])
                            {
                                row[i + 1] += "*";
                                isStarred[i] = true;
                            }

                            if (distances[i] == step[i])
                            {
                                isConfirmed[i] = true;
                            }
                        }
                    }
                }
                _ = dataGridView.Rows.Add(row);
                stepNumber++;
            }

            prompt.Controls.Add(dataGridView);
            _ = prompt.ShowDialog();
        }

    }
}
