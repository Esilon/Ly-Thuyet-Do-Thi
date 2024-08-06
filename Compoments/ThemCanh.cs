using Đồ_Thị.Class;
namespace Đồ_Thị.Compoments
{
    public partial class ThemCanh : Form
    {

        private readonly int _vertexIndex1;
        private readonly int _vertexIndex2;

        public ThemCanh(List<Vertex> vertices, int vertexIndex1, int vertexIndex2)
        {
            InitializeComponent();
            _vertexIndex1 = vertexIndex1;
            _vertexIndex2 = vertexIndex2;

            VertexFirst.Text = $"Đỉnh {vertices[vertexIndex1].Value}";
            VertexSecondary.Text = $"Đỉnh {vertices[vertexIndex2].Value}";
            _ = numericUpDown1.Focus();

            checkBox1.CheckedChanged += checkBoxHasWeight_CheckedChanged;
            checkBoxHasWeight_CheckedChanged(this, EventArgs.Empty);
        }

        public Edge GetEdge()
        {
            bool isDirected = checkBox2.Checked;
            bool hasWeight = checkBox1.Checked;
            int weight = hasWeight ? (int)numericUpDown1.Value : 0;

            return new Edge(_vertexIndex1, _vertexIndex2, weight, isDirected);
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            _ = GetEdge();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_Plus_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value++;
        }

        private void btn_Minus_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value == 0)
            { }    
            else
              numericUpDown1.Value--;
        }

        private void checkBoxHasWeight_CheckedChanged(object? sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox1.Checked;
        }
    }
}
