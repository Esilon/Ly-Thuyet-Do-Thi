namespace Đồ_Thị.uc
{
    partial class Test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test));
            panelMain = new Panel();
            panelDis = new Panel();
            panelE = new Panel();
            label1 = new Label();
            txtNodeCount = new TextBox();
            paneldraw = new Panel();
            panelOptionButton = new Panel();
            btn_Xoa = new Compoments.Round_Button();
            btn_select = new Compoments.Round_Button();
            btnClear = new Compoments.Round_Button();
            btn_ThemCanh = new Compoments.Round_Button();
            btn_ThemDinh = new Compoments.Round_Button();
            panelMain.SuspendLayout();
            panelDis.SuspendLayout();
            panelE.SuspendLayout();
            panelOptionButton.SuspendLayout();
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
            panelMain.Size = new Size(895, 465);
            panelMain.TabIndex = 0;
            // 
            // panelDis
            // 
            panelDis.Controls.Add(panelE);
            panelDis.Controls.Add(paneldraw);
            panelDis.Dock = DockStyle.Fill;
            panelDis.Location = new Point(0, 0);
            panelDis.Name = "panelDis";
            panelDis.Size = new Size(895, 465);
            panelDis.TabIndex = 4;
            // 
            // panelE
            // 
            panelE.Controls.Add(label1);
            panelE.Controls.Add(txtNodeCount);
            panelE.Dock = DockStyle.Right;
            panelE.Location = new Point(759, 0);
            panelE.Name = "panelE";
            panelE.Size = new Size(136, 465);
            panelE.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(83, 0);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 1;
            label1.Text = "Số node";
            // 
            // txtNodeCount
            // 
            txtNodeCount.Dock = DockStyle.Right;
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
            paneldraw.Size = new Size(895, 465);
            paneldraw.TabIndex = 3;
            paneldraw.Paint += panel1_Paint;
            paneldraw.MouseDown += panel1_MouseDown;
            paneldraw.MouseMove += panel1_MouseMove;
            // 
            // panelOptionButton
            // 
            panelOptionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelOptionButton.BackColor = Color.LightSalmon;
            panelOptionButton.BackgroundImageLayout = ImageLayout.None;
            panelOptionButton.Controls.Add(btn_Xoa);
            panelOptionButton.Controls.Add(btn_select);
            panelOptionButton.Controls.Add(btnClear);
            panelOptionButton.Controls.Add(btn_ThemCanh);
            panelOptionButton.Controls.Add(btn_ThemDinh);
            panelOptionButton.Dock = DockStyle.Bottom;
            panelOptionButton.Location = new Point(0, 404);
            panelOptionButton.Name = "panelOptionButton";
            panelOptionButton.Size = new Size(895, 61);
            panelOptionButton.TabIndex = 1;
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
            btn_Xoa.ForeColor = Color.White;
            btn_Xoa.Image = (Image)resources.GetObject("btn_Xoa.Image");
            btn_Xoa.Location = new Point(326, 12);
            btn_Xoa.Name = "btn_Xoa";
            btn_Xoa.RightToLeft = RightToLeft.No;
            btn_Xoa.Size = new Size(150, 40);
            btn_Xoa.TabIndex = 4;
            btn_Xoa.Text = "Xoá";
            btn_Xoa.TextColor = Color.White;
            btn_Xoa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_Xoa.UseVisualStyleBackColor = false;
            btn_Xoa.Click += btnXoa_Click;
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
            btn_select.ForeColor = Color.White;
            btn_select.Image = (Image)resources.GetObject("btn_select.Image");
            btn_select.Location = new Point(638, 12);
            btn_select.Name = "btn_select";
            btn_select.Size = new Size(150, 40);
            btn_select.TabIndex = 3;
            btn_select.Text = "Chọn";
            btn_select.TextColor = Color.White;
            btn_select.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_select.UseVisualStyleBackColor = false;
            btn_select.Click += btn_select_Click;
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
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(482, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 40);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.TextColor = Color.White;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
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
            btn_ThemCanh.ForeColor = Color.White;
            btn_ThemCanh.Image = (Image)resources.GetObject("btn_ThemCanh.Image");
            btn_ThemCanh.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ThemCanh.Location = new Point(170, 12);
            btn_ThemCanh.Name = "btn_ThemCanh";
            btn_ThemCanh.Size = new Size(150, 40);
            btn_ThemCanh.TabIndex = 1;
            btn_ThemCanh.Text = "Thêm Cạnh";
            btn_ThemCanh.TextColor = Color.White;
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
            btn_ThemDinh.ForeColor = Color.White;
            btn_ThemDinh.Image = (Image)resources.GetObject("btn_ThemDinh.Image");
            btn_ThemDinh.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ThemDinh.Location = new Point(14, 12);
            btn_ThemDinh.Name = "btn_ThemDinh";
            btn_ThemDinh.Size = new Size(150, 40);
            btn_ThemDinh.TabIndex = 0;
            btn_ThemDinh.Text = "Thêm Đỉnh";
            btn_ThemDinh.TextColor = Color.White;
            btn_ThemDinh.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ThemDinh.UseVisualStyleBackColor = false;
            btn_ThemDinh.Click += btnAddVertex_Click;
            // 
            // Test
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelOptionButton);
            Controls.Add(panelMain);
            Name = "Test";
            Size = new Size(895, 465);
            Click += btnAddVertex_Click;
            panelMain.ResumeLayout(false);
            panelDis.ResumeLayout(false);
            panelE.ResumeLayout(false);
            panelE.PerformLayout();
            panelOptionButton.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelMain;
        private Panel panelOptionButton;
        private Compoments.Round_Button btn_ThemCanh;
        private Compoments.Round_Button btn_ThemDinh;
        private Panel panelE;
        private Panel paneldraw;
        private Label label1;
        private TextBox txtNodeCount;
        private Compoments.Round_Button btn_select;
        private Compoments.Round_Button btnClear;
        private Compoments.Round_Button btn_Xoa;
        private Panel panelDis;
    }
}
