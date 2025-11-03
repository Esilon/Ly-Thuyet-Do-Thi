using System.Globalization;
using System.Text.RegularExpressions;

namespace DoThi.Class
{
    public static partial class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            using var prompt = new Form
            {
                Width = 180,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var textLabel = new Label { Left = 35, Top = 20, Text = text };
            var inputBox = new TextBox { Left = 20, Top = 40, Width = 120, TextAlign = HorizontalAlignment.Center };
            var confirmation = new Button { Text = caption, Left = 30, Width = 90, Top = 70, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => prompt.Close();
            prompt.Controls.AddRange(new Control[] { textLabel, inputBox, confirmation });
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
        }

        public static string? ChangeValueDialog(string text, string caption, string value)
        {
            using var prompt = new Form
            {
                Width = 260,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            var textLabel = new Label { Left = 50, Top = 20, Text = text };
            var inputBox = new TextBox { Left = 50, Top = 50, Width = 150, Text = value, TextAlign = HorizontalAlignment.Center };
            var confirmation = new Button { Text = caption, Left = 20, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Cancel", Left = 140, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => prompt.Close();
            cancel.Click += (sender, e) => prompt.Close();
            prompt.Controls.AddRange(new Control[] { inputBox, confirmation, cancel, textLabel });
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
        }

        public static (string, bool)? ShowChangeValueDialog(string text, string caption, string value)
        {
            using var prompt = new Form
            {
                Width = 240,
                Height = 190,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            var textLabel = new Label { Left = 65, Top = 20, Text = text, TextAlign = ContentAlignment.MiddleCenter };
            var inputBox = new TextBox { Left = 55, Top = 50, Width = 120, Text = value, TextAlign = HorizontalAlignment.Center };
            var confirmation = new Button { Text = caption, Left = 30, Width = 70, Top = 120, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Cancel", Left = 125, Width = 70, Top = 120, DialogResult = DialogResult.Cancel };
            var directedRadioButton = new RadioButton { Text = "Directed", Left = 20, Top = 80, Checked = true };
            var undirectedRadioButton = new RadioButton { Text = "Undirected", Left = 130, Top = 80 };

            confirmation.Click += (sender, e) => prompt.Close();
            cancel.Click += (sender, e) => prompt.Close();
            prompt.Controls.AddRange(new Control[] { textLabel, inputBox, confirmation, cancel, directedRadioButton, undirectedRadioButton });
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() != DialogResult.OK) return null;

            return (inputBox.Text, directedRadioButton.Checked);
        }

        public static void ShowKruskalSteps(string caption, List<Edge> minimumSpanningTree, List<Vertex> vertices)
        {
            using var prompt = new Form
            {
                Width = 250,
                Height = 400,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            var dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnCount = 2
            };
            dataGridView.Columns[0].Name = "Edge";
            dataGridView.Columns[1].Name = "Weight";

            foreach (var edge in minimumSpanningTree)
            {
                dataGridView.Rows.Add($"{vertices[edge.Vertex1].Value} - {vertices[edge.Vertex2].Value}", edge.Weight.ToString());
            }

            prompt.Controls.Add(dataGridView);
            prompt.ShowDialog();
        }

        public static void ShowPrimSteps(string caption, List<Edge> minimumSpanningTree, List<Vertex> vertices, decimal pricePerMeter)
        {
            using var prompt = new Form
            {
                Width = 400,
                Height = 500,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            var dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnCount = 3
            };
            dataGridView.Columns[0].Name = "Edge";
            dataGridView.Columns[1].Name = "Weight (m)";
            dataGridView.Columns[2].Name = "Cost (VND)";

            int totalWeight = minimumSpanningTree.Sum(edge => edge.Weight);
            decimal totalCost = minimumSpanningTree.Sum(edge => edge.Weight * pricePerMeter);

            foreach (var edge in minimumSpanningTree)
            {
                dataGridView.Rows.Add($"{vertices[edge.Vertex1].Value} - {vertices[edge.Vertex2].Value}", $"{edge.Weight} m", $"{edge.Weight * pricePerMeter:N0} VND");
            }

            dataGridView.Rows.Add("Total:", $"{totalWeight} m", $"{totalCost:N0} VND");

            prompt.Controls.Add(dataGridView);
            prompt.ShowDialog();
        }

        public static decimal ShowPriceInputDialog()
        {
            using var priceForm = new Form
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Enter Price per Meter",
                StartPosition = FormStartPosition.CenterScreen
            };

            var label = new Label { Left = 10, Top = 20, Text = "Price (VND) per meter:" };
            var textBox = new TextBox { Left = 150, Top = 20, Width = 100, MaxLength = 15 };
            var okButton = new Button { Text = "OK", Left = 100, Width = 100, Top = 60, DialogResult = DialogResult.OK };

            okButton.Click += (sender, e) => priceForm.Close();
            textBox.TextChanged += (sender, e) =>
            {
                string cleanedText = NonNumericRegex().Replace(textBox.Text.Replace(" VND", ""), "");
                if (string.IsNullOrEmpty(cleanedText))
                {
                    textBox.Text = string.Empty;
                }
                else if (decimal.TryParse(cleanedText, out decimal value))
                {
                    textBox.Text = $"{value:N0} VND";
                    textBox.Select(textBox.Text.Length - " VND".Length, 0);
                }
            };

            priceForm.Controls.AddRange(new Control[] { label, textBox, okButton });
            priceForm.AcceptButton = okButton;

            if (priceForm.ShowDialog() != DialogResult.OK) return 0;
            return decimal.TryParse(textBox.Text.Replace(" VND", ""), out decimal price) ? price : 0;
        }

        public static void ShowDijkstraSteps(string caption, int[] distances, List<int>[] parents, List<int[]> steps, List<Vertex> vertices)
        {
            using var prompt = new Form
            {
                Width = 1000,
                Height = 600,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            var dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnCount = vertices.Count + 1
            };
            dataGridView.Columns[0].Name = "Step";
            for (int i = 0; i < vertices.Count; i++)
            {
                dataGridView.Columns[i + 1].Name = vertices[i].Value;
            }

            var confirmed = new bool[vertices.Count];
            var starred = new bool[vertices.Count];

            for (int i = 0; i < steps.Length; i++)
            {
                var row = new string[vertices.Count + 1];
                row[0] = (i + 1).ToString();
                for (int j = 0; j < vertices.Count; j++)
                {
                    if (steps[i][j] == int.MaxValue)
                    {
                        row[j + 1] = "(∞, -)";
                    }
                    else if (confirmed[j])
                    {
                        row[j + 1] = "{---}";
                    }
                    else
                    {
                        var parentStr = string.Join(", ", parents[j].Select(p => p == -1 ? "-" : vertices[p].Value));
                        row[j + 1] = $"({steps[i][j]}, {parentStr})";
                        if (distances[j] == steps[i][j] && !starred[j])
                        {
                            row[j + 1] += "*";
                            starred[j] = true;
                        }
                        if (distances[j] == steps[i][j])
                        {
                            confirmed[j] = true;
                        }
                    }
                }
                dataGridView.Rows.Add(row);
            }

            prompt.Controls.Add(dataGridView);
            prompt.ShowDialog();
        }

        [GeneratedRegex(@"[^0-9]")]
        private static partial Regex NonNumericRegex();
    }
}
