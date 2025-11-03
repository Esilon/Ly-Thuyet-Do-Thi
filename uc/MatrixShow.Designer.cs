namespace DoThi.uc
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatrixShow));
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelDisplay = new System.Windows.Forms.Panel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnExportMatrix = new System.Windows.Forms.Button();
            this.labelZoom = new System.Windows.Forms.Label();
            this.txtZoom = new System.Windows.Forms.TextBox();
            this.btnClearMovingBall = new System.Windows.Forms.Button();
            this.groupMatrixType = new System.Windows.Forms.GroupBox();
            this.radioWeightMatrix = new System.Windows.Forms.RadioButton();
            this.radioAdjMatrix = new System.Windows.Forms.RadioButton();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.cbEndVertex = new System.Windows.Forms.ComboBox();
            this.cbStartVertex = new System.Windows.Forms.ComboBox();
            this.labelNodeCount = new System.Windows.Forms.Label();
            this.txtNodeCount = new System.Windows.Forms.TextBox();
            this.panelDraw = new System.Windows.Forms.Panel();
            this.btnGraph = new DoThi.Components.RoundButton();
            this.btnClear = new DoThi.Components.RoundButton();
            this.btnSelect = new DoThi.Components.RoundButton();
            this.btnSearchMenu = new DoThi.Components.RoundButton();
            this.btnDelete = new DoThi.Components.RoundButton();
            this.btnAddEdge = new DoThi.Components.RoundButton();
            this.btnAddVertex = new DoThi.Components.RoundButton();
            this.drpGraph = new DoThi.Components.RJDropdownMenu(this.components);
            this.saveGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drpSearch = new DoThi.Components.RJDropdownMenu(this.components);
            this.dfsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bfsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chinesePostmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kruskalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortestPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dijkstraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMain.SuspendLayout();
            this.panelDisplay.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.groupMatrixType.SuspendLayout();
            this.drpGraph.SuspendLayout();
            this.drpSearch.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.AutoSize = true;
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMain.Controls.Add(this.panelDisplay);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(938, 421);
            this.panelMain.TabIndex = 2;
            //
            // panelDisplay
            //
            this.panelDisplay.Controls.Add(this.panelControls);
            this.panelDisplay.Controls.Add(this.panelDraw);
            this.panelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDisplay.Location = new System.Drawing.Point(0, 0);
            this.panelDisplay.Name = "panelDisplay";
            this.panelDisplay.Size = new System.Drawing.Size(938, 421);
            this.panelDisplay.TabIndex = 4;
            //
            // panelControls
            //
            this.panelControls.Controls.Add(this.btnExportMatrix);
            this.panelControls.Controls.Add(this.labelZoom);
            this.panelControls.Controls.Add(this.txtZoom);
            this.panelControls.Controls.Add(this.btnClearMovingBall);
            this.panelControls.Controls.Add(this.groupMatrixType);
            this.panelControls.Controls.Add(this.labelTo);
            this.panelControls.Controls.Add(this.labelFrom);
            this.panelControls.Controls.Add(this.cbEndVertex);
            this.panelControls.Controls.Add(this.cbStartVertex);
            this.panelControls.Controls.Add(this.labelNodeCount);
            this.panelControls.Controls.Add(this.txtNodeCount);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(801, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(137, 421);
            this.panelControls.TabIndex = 2;
            //
            // btnExportMatrix
            //
            this.btnExportMatrix.Location = new System.Drawing.Point(56, 131);
            this.btnExportMatrix.Name = "btnExportMatrix";
            this.btnExportMatrix.Size = new System.Drawing.Size(75, 23);
            this.btnExportMatrix.TabIndex = 12;
            this.btnExportMatrix.Text = "Export Matrix";
            this.btnExportMatrix.UseVisualStyleBackColor = true;
            this.btnExportMatrix.Click += new System.EventHandler(this.btnExportMatrix_Click);
            //
            // labelZoom
            //
            this.labelZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZoom.AutoSize = true;
            this.labelZoom.Location = new System.Drawing.Point(84, 295);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(59, 15);
            this.labelZoom.TabIndex = 11;
            this.labelZoom.Text = "Zoom Level";
            // 
            // txtZoom
            // 
            this.txtZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZoom.Location = new System.Drawing.Point(3, 295);
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.ReadOnly = true;
            this.txtZoom.Size = new System.Drawing.Size(131, 23);
            this.txtZoom.TabIndex = 10;
            //
            // btnClearMovingBall
            //
            this.btnClearMovingBall.Location = new System.Drawing.Point(56, 102);
            this.btnClearMovingBall.Name = "btnClearMovingBall";
            this.btnClearMovingBall.Size = new System.Drawing.Size(75, 23);
            this.btnClearMovingBall.TabIndex = 9;
            this.btnClearMovingBall.Text = "Clear Draw";
            this.btnClearMovingBall.UseVisualStyleBackColor = true;
            this.btnClearMovingBall.Click += new System.EventHandler(this.btnClearMovingBall_Click);
            //
            // groupMatrixType
            //
            this.groupMatrixType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMatrixType.Controls.Add(this.radioWeightMatrix);
            this.groupMatrixType.Controls.Add(this.radioAdjMatrix);
            this.groupMatrixType.Location = new System.Drawing.Point(0, 223);
            this.groupMatrixType.Name = "groupMatrixType";
            this.groupMatrixType.Size = new System.Drawing.Size(137, 66);
            this.groupMatrixType.TabIndex = 8;
            this.groupMatrixType.TabStop = false;
            this.groupMatrixType.Text = "Matrix Type";
            //
            // radioWeightMatrix
            //
            this.radioWeightMatrix.AutoSize = true;
            this.radioWeightMatrix.Location = new System.Drawing.Point(6, 43);
            this.radioWeightMatrix.Name = "radioWeightMatrix";
            this.radioWeightMatrix.Size = new System.Drawing.Size(98, 19);
            this.radioWeightMatrix.TabIndex = 7;
            this.radioWeightMatrix.Text = "Weight Matrix";
            this.radioWeightMatrix.UseVisualStyleBackColor = true;
            this.radioWeightMatrix.CheckedChanged += new System.EventHandler(this.rbMatrixType_CheckedChanged);
            //
            // radioAdjMatrix
            //
            this.radioAdjMatrix.AutoSize = true;
            this.radioAdjMatrix.Checked = true;
            this.radioAdjMatrix.Location = new System.Drawing.Point(6, 18);
            this.radioAdjMatrix.Name = "radioAdjMatrix";
            this.radioAdjMatrix.Size = new System.Drawing.Size(116, 19);
            this.radioAdjMatrix.TabIndex = 6;
            this.radioAdjMatrix.TabStop = true;
            this.radioAdjMatrix.Text = "Adjacency Matrix";
            this.radioAdjMatrix.UseVisualStyleBackColor = true;
            this.radioAdjMatrix.CheckedChanged += new System.EventHandler(this.rbMatrixType_CheckedChanged);
            //
            // labelTo
            //
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(22, 61);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(22, 15);
            this.labelTo.TabIndex = 5;
            this.labelTo.Text = "To:";
            //
            // labelFrom
            //
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(6, 32);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(38, 15);
            this.labelFrom.TabIndex = 4;
            this.labelFrom.Text = "From:";
            //
            // cbEndVertex
            //
            this.cbEndVertex.FormattingEnabled = true;
            this.cbEndVertex.Location = new System.Drawing.Point(56, 58);
            this.cbEndVertex.Name = "cbEndVertex";
            this.cbEndVertex.Size = new System.Drawing.Size(76, 23);
            this.cbEndVertex.TabIndex = 3;
            //
            // cbStartVertex
            //
            this.cbStartVertex.FormattingEnabled = true;
            this.cbStartVertex.Location = new System.Drawing.Point(56, 29);
            this.cbStartVertex.Name = "cbStartVertex";
            this.cbStartVertex.Size = new System.Drawing.Size(76, 23);
            this.cbStartVertex.TabIndex = 2;
            //
            // labelNodeCount
            //
            this.labelNodeCount.AutoSize = true;
            this.labelNodeCount.Location = new System.Drawing.Point(87, 0);
            this.labelNodeCount.Name = "labelNodeCount";
            this.labelNodeCount.Size = new System.Drawing.Size(71, 15);
            this.labelNodeCount.TabIndex = 1;
            this.labelNodeCount.Text = "Node Count";
            // 
            // txtNodeCount
            // 
            this.txtNodeCount.Location = new System.Drawing.Point(0, 0);
            this.txtNodeCount.Name = "txtNodeCount";
            this.txtNodeCount.ReadOnly = true;
            this.txtNodeCount.Size = new System.Drawing.Size(136, 23);
            this.txtNodeCount.TabIndex = 0;
            //
            // panelDraw
            //
            this.panelDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDraw.Location = new System.Drawing.Point(0, 0);
            this.panelDraw.Name = "panelDraw";
            this.panelDraw.Size = new System.Drawing.Size(938, 421);
            this.panelDraw.TabIndex = 3;
            this.panelDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDraw_Paint);
            this.panelDraw.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseDoubleClick);
            this.panelDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseDown);
            this.panelDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseMove);
            this.panelDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseUp);
            //
            // btnGraph
            //
            this.btnGraph.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnGraph.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnGraph.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGraph.BorderRadius = 20;
            this.btnGraph.BorderSize = 0;
            this.btnGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGraph.FlatAppearance.BorderSize = 0;
            this.btnGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGraph.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGraph.ForeColor = System.Drawing.Color.Transparent;
            this.btnGraph.Image = global::DoThi.Properties.Resources.Save_1;
            this.btnGraph.Location = new System.Drawing.Point(3, 49);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new System.Drawing.Size(150, 40);
            this.btnGraph.TabIndex = 6;
            this.btnGraph.Text = "Graph";
            this.btnGraph.TextColor = System.Drawing.Color.Transparent;
            this.btnGraph.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGraph.UseVisualStyleBackColor = false;
            this.btnGraph.Click += new System.EventHandler(this.btnGraph_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnClear.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnClear.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnClear.BorderRadius = 20;
            this.btnClear.BorderSize = 0;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Transparent;
            this.btnClear.Location = new System.Drawing.Point(627, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 40);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.TextColor = System.Drawing.Color.Transparent;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // btnSelect
            //
            this.btnSelect.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnSelect.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnSelect.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSelect.BorderRadius = 20;
            this.btnSelect.BorderSize = 0;
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.FlatAppearance.BorderSize = 0;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.Transparent;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(3, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(150, 40);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Select";
            this.btnSelect.TextColor = System.Drawing.Color.Transparent;
            this.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            //
            // btnSearchMenu
            //
            this.btnSearchMenu.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnSearchMenu.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnSearchMenu.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSearchMenu.BorderRadius = 20;
            this.btnSearchMenu.BorderSize = 0;
            this.btnSearchMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchMenu.FlatAppearance.BorderSize = 0;
            this.btnSearchMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchMenu.ForeColor = System.Drawing.Color.Transparent;
            this.btnSearchMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchMenu.Image")));
            this.btnSearchMenu.Location = new System.Drawing.Point(783, 3);
            this.btnSearchMenu.Name = "btnSearchMenu";
            this.btnSearchMenu.Size = new System.Drawing.Size(150, 40);
            this.btnSearchMenu.TabIndex = 5;
            this.btnSearchMenu.Text = "Search";
            this.btnSearchMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchMenu.TextColor = System.Drawing.Color.Transparent;
            this.btnSearchMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearchMenu.UseVisualStyleBackColor = false;
            this.btnSearchMenu.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // btnDelete
            //
            this.btnDelete.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnDelete.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnDelete.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDelete.BorderRadius = 20;
            this.btnDelete.BorderSize = 0;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(471, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDelete.Size = new System.Drawing.Size(150, 40);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextColor = System.Drawing.Color.Transparent;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // btnAddEdge
            //
            this.btnAddEdge.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnAddEdge.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnAddEdge.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAddEdge.BorderRadius = 20;
            this.btnAddEdge.BorderSize = 0;
            this.btnAddEdge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddEdge.FlatAppearance.BorderSize = 0;
            this.btnAddEdge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEdge.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEdge.ForeColor = System.Drawing.Color.Transparent;
            this.btnAddEdge.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEdge.Image")));
            this.btnAddEdge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddEdge.Location = new System.Drawing.Point(315, 3);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(150, 40);
            this.btnAddEdge.TabIndex = 1;
            this.btnAddEdge.Text = "Add Edge";
            this.btnAddEdge.TextColor = System.Drawing.Color.Transparent;
            this.btnAddEdge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddEdge.UseVisualStyleBackColor = false;
            this.btnAddEdge.Click += new System.EventHandler(this.btnAddEdge_Click);
            //
            // btnAddVertex
            //
            this.btnAddVertex.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnAddVertex.BackgroundColor = System.Drawing.Color.SaddleBrown;
            this.btnAddVertex.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAddVertex.BorderRadius = 20;
            this.btnAddVertex.BorderSize = 0;
            this.btnAddVertex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddVertex.FlatAppearance.BorderSize = 0;
            this.btnAddVertex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVertex.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVertex.ForeColor = System.Drawing.Color.Transparent;
            this.btnAddVertex.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVertex.Image")));
            this.btnAddVertex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddVertex.Location = new System.Drawing.Point(159, 3);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(150, 40);
            this.btnAddVertex.TabIndex = 0;
            this.btnAddVertex.Text = "Add Vertex";
            this.btnAddVertex.TextColor = System.Drawing.Color.Transparent;
            this.btnAddVertex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddVertex.UseVisualStyleBackColor = false;
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);
            //
            // drpGraph
            //
            this.drpGraph.BackColor = System.Drawing.SystemColors.Control;
            this.drpGraph.IsMainMenu = false;
            this.drpGraph.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGraphToolStripMenuItem,
            this.loadGraphToolStripMenuItem});
            this.drpGraph.MenuItemHeight = 20;
            this.drpGraph.MenuItemTextColor = System.Drawing.Color.White;
            this.drpGraph.Name = "drMenuGraph";
            this.drpGraph.PrimaryColor = System.Drawing.Color.SaddleBrown;
            this.drpGraph.Size = new System.Drawing.Size(134, 48);
            //
            // saveGraphToolStripMenuItem
            //
            this.saveGraphToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.saveGraphToolStripMenuItem.Name = "saveGraphToolStripMenuItem";
            this.saveGraphToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveGraphToolStripMenuItem.Text = "Save Graph";
            this.saveGraphToolStripMenuItem.Click += new System.EventHandler(this.saveGraphToolStripMenuItem_Click);
            //
            // loadGraphToolStripMenuItem
            //
            this.loadGraphToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.loadGraphToolStripMenuItem.Name = "loadGraphToolStripMenuItem";
            this.loadGraphToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadGraphToolStripMenuItem.Text = "Load Graph";
            this.loadGraphToolStripMenuItem.Click += new System.EventHandler(this.loadGraphToolStripMenuItem_Click);
            //
            // drpSearch
            //
            this.drpSearch.IsMainMenu = true;
            this.drpSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dfsToolStripMenuItem,
            this.bfsToolStripMenuItem,
            this.chinesePostmanToolStripMenuItem,
            this.mstToolStripMenuItem,
            this.shortestPathToolStripMenuItem});
            this.drpSearch.MenuItemHeight = 25;
            this.drpSearch.MenuItemTextColor = System.Drawing.Color.Empty;
            this.drpSearch.Name = "drTim";
            this.drpSearch.PrimaryColor = System.Drawing.Color.Empty;
            this.drpSearch.Size = new System.Drawing.Size(183, 114);
            //
            // dfsToolStripMenuItem
            //
            this.dfsToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.dfsToolStripMenuItem.Name = "dfsToolStripMenuItem";
            this.dfsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.dfsToolStripMenuItem.Text = "DFS";
            this.dfsToolStripMenuItem.Click += new System.EventHandler(this.DFS_Click);
            //
            // bfsToolStripMenuItem
            //
            this.bfsToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.bfsToolStripMenuItem.Name = "bfsToolStripMenuItem";
            this.bfsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.bfsToolStripMenuItem.Text = "BFS";
            this.bfsToolStripMenuItem.Click += new System.EventHandler(this.BFS_Click);
            //
            // chinesePostmanToolStripMenuItem
            //
            this.chinesePostmanToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.chinesePostmanToolStripMenuItem.Name = "chinesePostmanToolStripMenuItem";
            this.chinesePostmanToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.chinesePostmanToolStripMenuItem.Text = "Chinese Postman";
            this.chinesePostmanToolStripMenuItem.Click += new System.EventHandler(this.ChinesePostman_Click);
            //
            // mstToolStripMenuItem
            //
            this.mstToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.mstToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kruskalToolStripMenuItem,
            this.primToolStripMenuItem});
            this.mstToolStripMenuItem.Name = "mstToolStripMenuItem";
            this.mstToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.mstToolStripMenuItem.Text = "Minimum Spanning Tree";
            //
            // kruskalToolStripMenuItem
            //
            this.kruskalToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.kruskalToolStripMenuItem.Name = "kruskalToolStripMenuItem";
            this.kruskalToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.kruskalToolStripMenuItem.Text = "Kruskal";
            this.kruskalToolStripMenuItem.Click += new System.EventHandler(this.Kruskal_Click);
            //
            // primToolStripMenuItem
            //
            this.primToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.primToolStripMenuItem.Name = "primToolStripMenuItem";
            this.primToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.primToolStripMenuItem.Text = "Prim";
            this.primToolStripMenuItem.Click += new System.EventHandler(this.Prim_Click);
            //
            // shortestPathToolStripMenuItem
            //
            this.shortestPathToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.shortestPathToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dijkstraToolStripMenuItem});
            this.shortestPathToolStripMenuItem.Name = "shortestPathToolStripMenuItem";
            this.shortestPathToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.shortestPathToolStripMenuItem.Text = "Shortest Path";
            //
            // dijkstraToolStripMenuItem
            //
            this.dijkstraToolStripMenuItem.BackColor = System.Drawing.Color.Sienna;
            this.dijkstraToolStripMenuItem.Name = "dijkstraToolStripMenuItem";
            this.dijkstraToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.dijkstraToolStripMenuItem.Text = "Dijkstra";
            this.dijkstraToolStripMenuItem.Click += new System.EventHandler(this.Dijkstra_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightSalmon;
            this.flowLayoutPanel1.Controls.Add(this.btnSelect);
            this.flowLayoutPanel1.Controls.Add(this.btnAddVertex);
            this.flowLayoutPanel1.Controls.Add(this.btnAddEdge);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnClear);
            this.flowLayoutPanel1.Controls.Add(this.btnSearchMenu);
            this.flowLayoutPanel1.Controls.Add(this.btnGraph);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 421);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(938, 100);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // MatrixShow
            // 
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelMain);
            this.Name = "MatrixShow";
            this.Size = new System.Drawing.Size(938, 521);
            this.panelMain.ResumeLayout(false);
            this.panelDisplay.ResumeLayout(false);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.groupMatrixType.ResumeLayout(false);
            this.groupMatrixType.PerformLayout();
            this.drpGraph.ResumeLayout(false);
            this.drpSearch.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Label labelNodeCount;
        private System.Windows.Forms.TextBox txtNodeCount;
        private System.Windows.Forms.Panel panelDraw;
        private Components.RoundButton btnDelete;
        private Components.RoundButton btnSelect;
        private Components.RoundButton btnClear;
        private Components.RoundButton btnAddEdge;
        private Components.RoundButton btnAddVertex;
        private Components.RoundButton btnSearchMenu;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.ComboBox cbEndVertex;
        private System.Windows.Forms.ComboBox cbStartVertex;
        private Components.RoundButton btnGraph;
        private System.Windows.Forms.GroupBox groupMatrixType;
        private System.Windows.Forms.RadioButton radioWeightMatrix;
        private System.Windows.Forms.RadioButton radioAdjMatrix;
        private System.Windows.Forms.Button btnClearMovingBall;
        private Components.RJDropdownMenu drpGraph;
        private System.Windows.Forms.ToolStripMenuItem saveGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGraphToolStripMenuItem;
        private Components.RJDropdownMenu drpSearch;
        private System.Windows.Forms.ToolStripMenuItem dfsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bfsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chinesePostmanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kruskalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortestPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dijkstraToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.TextBox txtZoom;
        private System.Windows.Forms.Button btnExportMatrix;
    }
}
