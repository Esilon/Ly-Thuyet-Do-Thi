using DoThi.Class;

namespace DoThi.Components
{
    public partial class AddEdge : Form
    {
        private readonly int _vertexIndex1;
        private readonly int _vertexIndex2;

        public AddEdge(List<Vertex> vertices, int vertexIndex1, int vertexIndex2)
        {
            InitializeComponent();
            _vertexIndex1 = vertexIndex1;
            _vertexIndex2 = vertexIndex2;

            labelVertex1.Text = $"Vertex {vertices[vertexIndex1].Value}";
            labelVertex2.Text = $"Vertex {vertices[vertexIndex2].Value}";
            numericUpDownWeight.Focus();

            checkBoxHasWeight.CheckedChanged += checkBoxHasWeight_CheckedChanged;
            checkBoxHasWeight_CheckedChanged(this, EventArgs.Empty);
        }

        public Edge GetEdge()
        {
            var isDirected = checkBoxIsDirected.Checked;
            var hasWeight = checkBoxHasWeight.Checked;
            var weight = hasWeight ? (int)numericUpDownWeight.Value : 0;

            return new Edge(_vertexIndex1, _vertexIndex2, weight, isDirected);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (numericUpDownWeight.Value < numericUpDownWeight.Maximum)
            {
                numericUpDownWeight.Value++;
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (numericUpDownWeight.Value > numericUpDownWeight.Minimum)
            {
                numericUpDownWeight.Value--;
            }
        }

        private void checkBoxHasWeight_CheckedChanged(object? sender, EventArgs e)
        {
            numericUpDownWeight.Enabled = checkBoxHasWeight.Checked;
        }
    }
}
