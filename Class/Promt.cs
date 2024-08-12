using System.Globalization;
using System.Text.RegularExpressions;

namespace Đồ_Thị.Class
{
    public static partial class Prompt
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
        public static void DisplayPrimStepsBTTT(string caption, List<Edge> minimumSpanningTree, List<Vertex> vertices, decimal pricePerMeter)
        {
            using Form prompt = new();
            prompt.Width = 400;
            prompt.Height = 500;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            DataGridView dataGridView = new()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "Cạnh";
            dataGridView.Columns[1].Name = "Trọng số (Mét)";
            dataGridView.Columns[2].Name = "Chi phí (VNĐ)";

            int totalWeight = 0; // Tổng trọng số của MST
            decimal totalCost = 0; // Tổng chi phí

            foreach (Edge edge in minimumSpanningTree)
            {
                string edgeDescription = $"{vertices[edge.Vertex1].Value} - {vertices[edge.Vertex2].Value}";
                string weightText = $"{edge.Weight} m";
                string costText = $"{edge.Weight * pricePerMeter:###,###,###} VNĐ"; // Định dạng tiền VNĐ

                dataGridView.Rows.Add(edgeDescription, weightText, costText);
                totalWeight += edge.Weight;
                totalCost += edge.Weight * pricePerMeter;
            }

            // Thêm hàng tổng số tiền
            string totalWeightText = $"{totalWeight} m";
            string totalCostText = $"{totalCost:###,###,###} VNĐ"; // Định dạng tiền VNĐ
            string[] totalRow =
            {
        "Tổng trọng số:",
        totalWeightText,
        totalCostText
    };
            dataGridView.Rows.Add(totalRow);

            prompt.Controls.Add(dataGridView);
            _ = prompt.ShowDialog();
        }

        public static decimal ShowPriceInputDialog()
        {
            using Form priceForm = new();
            priceForm.Width = 300;
            priceForm.Height = 150;
            priceForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            priceForm.Text = "Nhập giá tiền cho mỗi mét";
            priceForm.StartPosition = FormStartPosition.CenterScreen;

            Label label = new()
            {
                Left = 10,
                Top = 20,
                Text = "Giá tiền (VNĐ) mỗi mét:"
            };

            TextBox textBox = new()
            {
                Left = 150,
                Top = 20,
                Width = 100,
                MaxLength = 15 // Điều chỉnh theo nhu cầu
            };

            Button okButton = new()
            {
                Text = "OK",
                Left = 100,
                Width = 100,
                Top = 60,
                DialogResult = DialogResult.OK
            };

            okButton.Click += (sender, e) => priceForm.Close();
            textBox.TextChanged += (sender, e) =>
            {
                // Lấy giá trị không có ký hiệu VNĐ
                string cleanedText = MyRegex().Replace(textBox.Text.Replace(" VNĐ", ""), "");

                if (string.IsNullOrEmpty(cleanedText))
                {
                    // Xóa TextBox nếu không còn số nào
                    textBox.Text = string.Empty;
                    textBox.Select(0, 0);
                }
                else
                {
                    // Định dạng giá trị và thêm ký hiệu VNĐ
                    if (decimal.TryParse(cleanedText, out decimal value))
                    {
                        string formattedText = value.ToString("N0", new CultureInfo("en-US")) + " VNĐ";
                        textBox.Text = formattedText;

                        // Đặt con trỏ ngay sau số tiền
                        int cursorPosition = textBox.Text.Length - " VNĐ".Length;
                        textBox.Select(cursorPosition, 0);
                    }
                }
            };
            priceForm.Controls.Add(label);
            priceForm.Controls.Add(textBox);
            priceForm.Controls.Add(okButton);
            priceForm.AcceptButton = okButton;

            return priceForm.ShowDialog() == DialogResult.OK && decimal.TryParse(textBox.Text.Replace(" VNĐ", ""), out decimal price) ? price : 0;
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

        [GeneratedRegex(@"[^0-9]")]
        private static partial Regex MyRegex();
    }
}
