namespace Đồ_Thị.uc
{
    partial class MatrixShow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatrixShow));
            panelMain = new Panel();
            panelDis = new Panel();
            panelE = new Panel();
            btn_totxtmatrix = new Button();
            label4 = new Label();
            txtZoom = new TextBox();
            btn_ClearMovingBall = new Button();
            MatranGroup = new GroupBox();
            radio_WeightMatrix = new RadioButton();
            radio_AdjMatrix = new RadioButton();
            label3 = new Label();
            label2 = new Label();
            cb_Second = new ComboBox();
            cb_First = new ComboBox();
            label1 = new Label();
            txtNodeCount = new TextBox();
            paneldraw = new Panel();
            btn_Graph = new Compoments.Round_Button();
            btnClear = new Compoments.Round_Button();
            btn_select = new Compoments.Round_Button();
            btn_SearchMenu = new Compoments.Round_Button();
            btn_Xoa = new Compoments.Round_Button();
            btn_ThemCanh = new Compoments.Round_Button();
            btn_ThemDinh = new Compoments.Round_Button();
            drMenuGraph = new Compoments.RJDropdownMenu(components);
            SaveGraph = new ToolStripMenuItem();
            LoadGraph = new ToolStripMenuItem();
            drTim = new Compoments.RJDropdownMenu(components);
            DFS = new ToolStripMenuItem();
            BFS = new ToolStripMenuItem();
            MailMan = new ToolStripMenuItem();
            MSTree = new ToolStripMenuItem();
            Kruskal = new ToolStripMenuItem();
            Prim = new ToolStripMenuItem();
            SPaths = new ToolStripMenuItem();
            Dijkstra = new ToolStripMenuItem();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panelMain.SuspendLayout();
            panelDis.SuspendLayout();
            panelE.SuspendLayout();
            MatranGroup.SuspendLayout();
            drMenuGraph.SuspendLayout();
            drTim.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoSize = true;
            panelMain.BackColor = SystemColors.ControlLightLight;
            panelMain.Controls.Add(panelDis);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(938, 521);
            panelMain.TabIndex = 2;
            // 
            // panelDis
            // 
            panelDis.Controls.Add(panelE);
            panelDis.Controls.Add(paneldraw);
            panelDis.Dock = DockStyle.Fill;
            panelDis.Location = new Point(0, 0);
            panelDis.Name = "panelDis";
            panelDis.Size = new Size(938, 521);
            panelDis.TabIndex = 4;
            // 
            // panelE
            // 
            panelE.Controls.Add(btn_totxtmatrix);
            panelE.Controls.Add(label4);
            panelE.Controls.Add(txtZoom);
            panelE.Controls.Add(btn_ClearMovingBall);
            panelE.Controls.Add(MatranGroup);
            panelE.Controls.Add(label3);
            panelE.Controls.Add(label2);
            panelE.Controls.Add(cb_Second);
            panelE.Controls.Add(cb_First);
            panelE.Controls.Add(label1);
            panelE.Controls.Add(txtNodeCount);
            panelE.Dock = DockStyle.Right;
            panelE.Location = new Point(801, 0);
            panelE.Name = "panelE";
            panelE.Size = new Size(137, 521);
            panelE.TabIndex = 2;
            // 
            // btn_totxtmatrix
            // 
            btn_totxtmatrix.Location = new Point(56, 131);
            btn_totxtmatrix.Name = "btn_totxtmatrix";
            btn_totxtmatrix.Size = new Size(75, 23);
            btn_totxtmatrix.TabIndex = 12;
            btn_totxtmatrix.Text = "totxtMatrix";
            btn_totxtmatrix.UseVisualStyleBackColor = true;
            btn_totxtmatrix.Click += btn_totxtmatrix_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(84, 395);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 11;
            label4.Text = "Zome LVL";
            // 
            // txtZoom
            // 
            txtZoom.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtZoom.Location = new Point(3, 395);
            txtZoom.Name = "txtZoom";
            txtZoom.ReadOnly = true;
            txtZoom.Size = new Size(131, 23);
            txtZoom.TabIndex = 10;
            // 
            // btn_ClearMovingBall
            // 
            btn_ClearMovingBall.Location = new Point(56, 102);
            btn_ClearMovingBall.Name = "btn_ClearMovingBall";
            btn_ClearMovingBall.Size = new Size(75, 23);
            btn_ClearMovingBall.TabIndex = 9;
            btn_ClearMovingBall.Text = "Clear Draw";
            btn_ClearMovingBall.UseVisualStyleBackColor = true;
            btn_ClearMovingBall.Click += btn_ClearMovingBall_Click;
            // 
            // MatranGroup
            // 
            MatranGroup.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            MatranGroup.Controls.Add(radio_WeightMatrix);
            MatranGroup.Controls.Add(radio_AdjMatrix);
            MatranGroup.Location = new Point(0, 323);
            MatranGroup.Name = "MatranGroup";
            MatranGroup.Size = new Size(137, 66);
            MatranGroup.TabIndex = 8;
            MatranGroup.TabStop = false;
            MatranGroup.Text = "Ma trận";
            // 
            // radio_WeightMatrix
            // 
            radio_WeightMatrix.AutoSize = true;
            radio_WeightMatrix.Location = new Point(6, 43);
            radio_WeightMatrix.Name = "radio_WeightMatrix";
            radio_WeightMatrix.Size = new Size(113, 19);
            radio_WeightMatrix.TabIndex = 7;
            radio_WeightMatrix.Text = "Ma trận trọng số";
            radio_WeightMatrix.UseVisualStyleBackColor = true;
            radio_WeightMatrix.CheckedChanged += rbMatrixType_CheckedChanged;
            // 
            // radio_AdjMatrix
            // 
            radio_AdjMatrix.AutoSize = true;
            radio_AdjMatrix.Checked = true;
            radio_AdjMatrix.Location = new Point(6, 18);
            radio_AdjMatrix.Name = "radio_AdjMatrix";
            radio_AdjMatrix.Size = new Size(81, 19);
            radio_AdjMatrix.TabIndex = 6;
            radio_AdjMatrix.TabStop = true;
            radio_AdjMatrix.Text = "Ma trận kề";
            radio_AdjMatrix.UseVisualStyleBackColor = true;
            radio_AdjMatrix.CheckedChanged += rbMatrixType_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 61);
            label3.Name = "label3";
            label3.Size = new Size(22, 15);
            label3.TabIndex = 5;
            label3.Text = "To:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 32);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 4;
            label2.Text = "From:";
            // 
            // cb_Second
            // 
            cb_Second.FormattingEnabled = true;
            cb_Second.Location = new Point(56, 58);
            cb_Second.Name = "cb_Second";
            cb_Second.Size = new Size(76, 23);
            cb_Second.TabIndex = 3;
            // 
            // cb_First
            // 
            cb_First.FormattingEnabled = true;
            cb_First.Location = new Point(56, 29);
            cb_First.Name = "cb_First";
            cb_First.Size = new Size(76, 23);
            cb_First.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 0);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 1;
            label1.Text = "Số node";
            // 
            // txtNodeCount
            // 
            txtNodeCount.Location = new Point(0, 0);
            txtNodeCount.Name = "txtNodeCount";
            txtNodeCount.ReadOnly = true;
            txtNodeCount.Size = new Size(136, 23);
            txtNodeCount.TabIndex = 0;
            // 
            // paneldraw
            // 
            paneldraw.Dock = DockStyle.Fill;
            paneldraw.Location = new Point(0, 0);
            paneldraw.Name = "paneldraw";
            paneldraw.Size = new Size(938, 521);
            paneldraw.TabIndex = 3;
            paneldraw.Paint += paneldraw_Paint;
            paneldraw.MouseDoubleClick += paneldraw_MouseDoubleClick;
            paneldraw.MouseDown += paneldraw_MouseDown;
            paneldraw.MouseMove += paneldraw_MouseMove;
            paneldraw.MouseUp += paneldraw_MouseUp;
            // 
            // btn_Graph
            // 
            btn_Graph.BackColor = Color.SaddleBrown;
            btn_Graph.BackgroundColor = Color.SaddleBrown;
            btn_Graph.BorderColor = Color.PaleVioletRed;
            btn_Graph.BorderRadius = 20;
            btn_Graph.BorderSize = 0;
            btn_Graph.Cursor = Cursors.Hand;
            btn_Graph.FlatAppearance.BorderSize = 0;
            btn_Graph.FlatStyle = FlatStyle.Flat;
            btn_Graph.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Graph.ForeColor = Color.Transparent;
            btn_Graph.Image = Đồ_thị.Properties.Resources.Save_1;
            btn_Graph.Location = new Point(3, 49);
            btn_Graph.Name = "btn_Graph";
            btn_Graph.Size = new Size(150, 40);
            btn_Graph.TabIndex = 6;
            btn_Graph.Text = "Graph";
            btn_Graph.TextColor = Color.Transparent;
            btn_Graph.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_Graph.UseVisualStyleBackColor = false;
            btn_Graph.Click += btn_SaveGraph_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.SaddleBrown;
            btnClear.BackgroundColor = Color.SaddleBrown;
            btnClear.BorderColor = Color.PaleVioletRed;
            btnClear.BorderRadius = 20;
            btnClear.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.Transparent;
            btnClear.Location = new Point(627, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 40);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.TextColor = Color.Transparent;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btn_select
            // 
            btn_select.BackColor = Color.SaddleBrown;
            btn_select.BackgroundColor = Color.SaddleBrown;
            btn_select.BorderColor = Color.PaleVioletRed;
            btn_select.BorderRadius = 20;
            btn_select.BorderSize = 0;
            btn_select.Cursor = Cursors.Hand;
            btn_select.FlatAppearance.BorderSize = 0;
            btn_select.FlatStyle = FlatStyle.Flat;
            btn_select.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_select.ForeColor = Color.Transparent;
            btn_select.Image = (Image)resources.GetObject("btn_select.Image");
            btn_select.Location = new Point(3, 3);
            btn_select.Name = "btn_select";
            btn_select.Size = new Size(150, 40);
            btn_select.TabIndex = 3;
            btn_select.Text = "Chọn";
            btn_select.TextColor = Color.Transparent;
            btn_select.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_select.UseVisualStyleBackColor = false;
            btn_select.Click += btnSelect_Click;
            // 
            // btn_SearchMenu
            // 
            btn_SearchMenu.BackColor = Color.SaddleBrown;
            btn_SearchMenu.BackgroundColor = Color.SaddleBrown;
            btn_SearchMenu.BorderColor = Color.PaleVioletRed;
            btn_SearchMenu.BorderRadius = 20;
            btn_SearchMenu.BorderSize = 0;
            btn_SearchMenu.Cursor = Cursors.Hand;
            btn_SearchMenu.FlatAppearance.BorderSize = 0;
            btn_SearchMenu.FlatStyle = FlatStyle.Flat;
            btn_SearchMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_SearchMenu.ForeColor = Color.Transparent;
            btn_SearchMenu.Image = (Image)resources.GetObject("btn_SearchMenu.Image");
            btn_SearchMenu.Location = new Point(783, 3);
            btn_SearchMenu.Name = "btn_SearchMenu";
            btn_SearchMenu.Size = new Size(150, 40);
            btn_SearchMenu.TabIndex = 5;
            btn_SearchMenu.Text = "Tìm";
            btn_SearchMenu.TextAlign = ContentAlignment.MiddleRight;
            btn_SearchMenu.TextColor = Color.Transparent;
            btn_SearchMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_SearchMenu.UseVisualStyleBackColor = false;
            btn_SearchMenu.Click += btn_Search_Click;
            // 
            // btn_Xoa
            // 
            btn_Xoa.BackColor = Color.SaddleBrown;
            btn_Xoa.BackgroundColor = Color.SaddleBrown;
            btn_Xoa.BorderColor = Color.PaleVioletRed;
            btn_Xoa.BorderRadius = 20;
            btn_Xoa.BorderSize = 0;
            btn_Xoa.Cursor = Cursors.Hand;
            btn_Xoa.FlatAppearance.BorderSize = 0;
            btn_Xoa.FlatStyle = FlatStyle.Flat;
            btn_Xoa.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Xoa.ForeColor = Color.Transparent;
            btn_Xoa.Image = (Image)resources.GetObject("btn_Xoa.Image");
            btn_Xoa.Location = new Point(471, 3);
            btn_Xoa.Name = "btn_Xoa";
            btn_Xoa.RightToLeft = RightToLeft.No;
            btn_Xoa.Size = new Size(150, 40);
            btn_Xoa.TabIndex = 4;
            btn_Xoa.Text = "Xoá";
            btn_Xoa.TextColor = Color.Transparent;
            btn_Xoa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_Xoa.UseVisualStyleBackColor = false;
            btn_Xoa.Click += btnXoa_Click;
            // 
            // btn_ThemCanh
            // 
            btn_ThemCanh.BackColor = Color.SaddleBrown;
            btn_ThemCanh.BackgroundColor = Color.SaddleBrown;
            btn_ThemCanh.BorderColor = Color.PaleVioletRed;
            btn_ThemCanh.BorderRadius = 20;
            btn_ThemCanh.BorderSize = 0;
            btn_ThemCanh.Cursor = Cursors.Hand;
            btn_ThemCanh.FlatAppearance.BorderSize = 0;
            btn_ThemCanh.FlatStyle = FlatStyle.Flat;
            btn_ThemCanh.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ThemCanh.ForeColor = Color.Transparent;
            btn_ThemCanh.Image = (Image)resources.GetObject("btn_ThemCanh.Image");
            btn_ThemCanh.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ThemCanh.Location = new Point(315, 3);
            btn_ThemCanh.Name = "btn_ThemCanh";
            btn_ThemCanh.Size = new Size(150, 40);
            btn_ThemCanh.TabIndex = 1;
            btn_ThemCanh.Text = "Thêm Cạnh";
            btn_ThemCanh.TextColor = Color.Transparent;
            btn_ThemCanh.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ThemCanh.UseVisualStyleBackColor = false;
            btn_ThemCanh.Click += btnAddEdge_Click;
            // 
            // btn_ThemDinh
            // 
            btn_ThemDinh.BackColor = Color.SaddleBrown;
            btn_ThemDinh.BackgroundColor = Color.SaddleBrown;
            btn_ThemDinh.BorderColor = Color.PaleVioletRed;
            btn_ThemDinh.BorderRadius = 20;
            btn_ThemDinh.BorderSize = 0;
            btn_ThemDinh.Cursor = Cursors.Hand;
            btn_ThemDinh.FlatAppearance.BorderSize = 0;
            btn_ThemDinh.FlatStyle = FlatStyle.Flat;
            btn_ThemDinh.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ThemDinh.ForeColor = Color.Transparent;
            btn_ThemDinh.Image = (Image)resources.GetObject("btn_ThemDinh.Image");
            btn_ThemDinh.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ThemDinh.Location = new Point(159, 3);
            btn_ThemDinh.Name = "btn_ThemDinh";
            btn_ThemDinh.Size = new Size(150, 40);
            btn_ThemDinh.TabIndex = 0;
            btn_ThemDinh.Text = "Thêm Đỉnh";
            btn_ThemDinh.TextColor = Color.Transparent;
            btn_ThemDinh.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ThemDinh.UseVisualStyleBackColor = false;
            btn_ThemDinh.Click += btnAddVertex_Click;
            // 
            // drMenuGraph
            // 
            drMenuGraph.BackColor = SystemColors.Control;
            drMenuGraph.IsMainMenu = false;
            drMenuGraph.Items.AddRange(new ToolStripItem[] { SaveGraph, LoadGraph });
            drMenuGraph.MenuItemHeight = 20;
            drMenuGraph.MenuItemTextColor = Color.White;
            drMenuGraph.Name = "drMenuGraph";
            drMenuGraph.PrimaryColor = Color.SaddleBrown;
            drMenuGraph.Size = new Size(134, 48);
            // 
            // SaveGraph
            // 
            SaveGraph.BackColor = Color.Sienna;
            SaveGraph.Name = "SaveGraph";
            SaveGraph.Size = new Size(133, 22);
            SaveGraph.Text = "Save Graph";
            SaveGraph.Click += saveGraphToolStripMenuItem_Click;
            // 
            // LoadGraph
            // 
            LoadGraph.BackColor = Color.Sienna;
            LoadGraph.Name = "LoadGraph";
            LoadGraph.Size = new Size(133, 22);
            LoadGraph.Text = "LoadGraph";
            LoadGraph.Click += loadGraphToolStripMenuItem_Click;
            // 
            // drTim
            // 
            drTim.IsMainMenu = true;
            drTim.Items.AddRange(new ToolStripItem[] { DFS, BFS, MailMan, MSTree, SPaths });
            drTim.MenuItemHeight = 25;
            drTim.MenuItemTextColor = Color.Empty;
            drTim.Name = "drTim";
            drTim.PrimaryColor = Color.Empty;
            drTim.Size = new Size(183, 114);
            // 
            // DFS
            // 
            DFS.BackColor = Color.Sienna;
            DFS.Name = "DFS";
            DFS.Size = new Size(182, 22);
            DFS.Text = "DFS";
            DFS.Click += DFS_Click;
            // 
            // BFS
            // 
            BFS.BackColor = Color.Sienna;
            BFS.Name = "BFS";
            BFS.Size = new Size(182, 22);
            BFS.Text = "BFS";
            BFS.Click += BFS_Click;
            // 
            // MailMan
            // 
            MailMan.BackColor = Color.Sienna;
            MailMan.Name = "MailMan";
            MailMan.Size = new Size(182, 22);
            MailMan.Text = "MailMan";
            MailMan.Click += MailMan_Click;
            // 
            // MSTree
            // 
            MSTree.BackColor = Color.Sienna;
            MSTree.DropDownItems.AddRange(new ToolStripItem[] { Kruskal, Prim });
            MSTree.Name = "MSTree";
            MSTree.Size = new Size(182, 22);
            MSTree.Text = "Cây khung nhỏ nhất";
            // 
            // Kruskal
            // 
            Kruskal.BackColor = Color.Sienna;
            Kruskal.Name = "Kruskal";
            Kruskal.Size = new Size(112, 22);
            Kruskal.Text = "Kruskal";
            Kruskal.Click += Kruskal_Click;
            // 
            // Prim
            // 
            Prim.BackColor = Color.Sienna;
            Prim.Name = "Prim";
            Prim.Size = new Size(112, 22);
            Prim.Text = "Prim";
            Prim.Click += Prim_Click;
            // 
            // SPaths
            // 
            SPaths.BackColor = Color.Sienna;
            SPaths.DropDownItems.AddRange(new ToolStripItem[] { Dijkstra });
            SPaths.Name = "SPaths";
            SPaths.Size = new Size(182, 22);
            SPaths.Text = "Đường đi ngắn nhất";
            // 
            // Dijkstra
            // 
            Dijkstra.BackColor = Color.Sienna;
            Dijkstra.Name = "Dijkstra";
            Dijkstra.Size = new Size(113, 22);
            Dijkstra.Text = "Dijkstra";
            Dijkstra.Click += Dijkstra_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.LightSalmon;
            flowLayoutPanel1.Controls.Add(btn_select);
            flowLayoutPanel1.Controls.Add(btn_ThemDinh);
            flowLayoutPanel1.Controls.Add(btn_ThemCanh);
            flowLayoutPanel1.Controls.Add(btn_Xoa);
            flowLayoutPanel1.Controls.Add(btnClear);
            flowLayoutPanel1.Controls.Add(btn_SearchMenu);
            flowLayoutPanel1.Controls.Add(btn_Graph);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.Location = new Point(0, 421);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(938, 100);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // MatrixShow
            // 
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panelMain);
            Name = "MatrixShow";
            Size = new Size(938, 521);
            panelMain.ResumeLayout(false);
            panelDis.ResumeLayout(false);
            panelE.ResumeLayout(false);
            panelE.PerformLayout();
            MatranGroup.ResumeLayout(false);
            MatranGroup.PerformLayout();
            drMenuGraph.ResumeLayout(false);
            drTim.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelMain;
        private Panel panelDis;
        private Panel panelE;
        private Label label1;
        private TextBox txtNodeCount;
        private Panel paneldraw;
        private Compoments.Round_Button btn_Xoa;
        private Compoments.Round_Button btn_select;
        private Compoments.Round_Button btnClear;
        private Compoments.Round_Button btn_ThemCanh;
        private Compoments.Round_Button btn_ThemDinh;
        private Compoments.Round_Button btn_SearchMenu;
        private Label label3;
        private Label label2;
        private ComboBox cb_Second;
        private ComboBox cb_First;
        private Compoments.Round_Button btn_Graph;
        private GroupBox MatranGroup;
        private RadioButton radio_WeightMatrix;
        private RadioButton radio_AdjMatrix;
        private Button btn_ClearMovingBall;
        private Compoments.RJDropdownMenu drMenuGraph;
        private ToolStripMenuItem SaveGraph;
        private ToolStripMenuItem LoadGraph;
        private Compoments.RJDropdownMenu drTim;
        private ToolStripMenuItem DFS;
        private ToolStripMenuItem BFS;
        private ToolStripMenuItem MailMan;
        private ToolStripMenuItem MSTree;
        private ToolStripMenuItem Kruskal;
        private ToolStripMenuItem Prim;
        private ToolStripMenuItem SPaths;
        private ToolStripMenuItem Dijkstra;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label4;
        private TextBox txtZoom;
        private Button btn_totxtmatrix;
    }
}
