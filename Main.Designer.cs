namespace DoThi
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            panelSideBarMenu = new Panel();
            btnSideBarExit = new FontAwesome.Sharp.IconButton();
            btnSideBar1 = new FontAwesome.Sharp.IconButton();
            btnSideBar2 = new FontAwesome.Sharp.IconButton();
            panelMenuTitle = new Panel();
            btnButtonMenu = new FontAwesome.Sharp.IconButton();
            labelMenuTitle = new Label();
            panelTitleBar = new Panel();
            panelControls = new Panel();
            btnMinimized = new FontAwesome.Sharp.IconButton();
            btnFullScreen = new FontAwesome.Sharp.IconButton();
            btnExit = new FontAwesome.Sharp.IconButton();
            labelTitle = new Label();
            panelMain = new Panel();
            sidebarTransition = new System.Windows.Forms.Timer(components);
            panelSideBarMenu.SuspendLayout();
            panelMenuTitle.SuspendLayout();
            panelTitleBar.SuspendLayout();
            panelControls.SuspendLayout();
            SuspendLayout();
            // 
            // panelSideBarMenu
            // 
            panelSideBarMenu.BackColor = Color.LightSalmon;
            panelSideBarMenu.Controls.Add(btnSideBarExit);
            panelSideBarMenu.Controls.Add(btnSideBar1);
            panelSideBarMenu.Controls.Add(btnSideBar2);
            panelSideBarMenu.Controls.Add(panelMenuTitle);
            panelSideBarMenu.Dock = DockStyle.Left;
            panelSideBarMenu.Location = new Point(0, 0);
            panelSideBarMenu.Name = "panelSideBarMenu";
            panelSideBarMenu.Size = new Size(206, 508);
            panelSideBarMenu.TabIndex = 0;
            // 
            // btnSideBarExit
            // 
            btnSideBarExit.AutoSize = true;
            btnSideBarExit.BackColor = Color.LightSalmon;
            btnSideBarExit.Cursor = Cursors.Hand;
            btnSideBarExit.Dock = DockStyle.Bottom;
            btnSideBarExit.FlatAppearance.BorderSize = 0;
            btnSideBarExit.FlatStyle = FlatStyle.Flat;
            btnSideBarExit.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSideBarExit.ForeColor = Color.White;
            btnSideBarExit.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            btnSideBarExit.IconColor = Color.Black;
            btnSideBarExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSideBarExit.IconSize = 40;
            btnSideBarExit.ImageAlign = ContentAlignment.MiddleLeft;
            btnSideBarExit.Location = new Point(0, 447);
            btnSideBarExit.Margin = new Padding(0);
            btnSideBarExit.Name = "btnSideBarExit";
            btnSideBarExit.Padding = new Padding(10, 0, 0, 0);
            btnSideBarExit.Size = new Size(206, 61);
            btnSideBarExit.TabIndex = 5;
            btnSideBarExit.Tag = "btnSideBarExit";
            btnSideBarExit.Text = "Exit";
            btnSideBarExit.TextAlign = ContentAlignment.MiddleLeft;
            btnSideBarExit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSideBarExit.UseVisualStyleBackColor = false;
            // 
            // btnSideBar1
            // 
            btnSideBar1.AutoSize = true;
            btnSideBar1.BackColor = Color.LightSalmon;
            btnSideBar1.Cursor = Cursors.Hand;
            btnSideBar1.Dock = DockStyle.Top;
            btnSideBar1.FlatAppearance.BorderSize = 0;
            btnSideBar1.FlatStyle = FlatStyle.Flat;
            btnSideBar1.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSideBar1.ForeColor = Color.White;
            btnSideBar1.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            btnSideBar1.IconColor = Color.Black;
            btnSideBar1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSideBar1.IconSize = 40;
            btnSideBar1.ImageAlign = ContentAlignment.MiddleLeft;
            btnSideBar1.Location = new Point(0, 104);
            btnSideBar1.Margin = new Padding(0);
            btnSideBar1.Name = "btnSideBar1";
            btnSideBar1.Padding = new Padding(10, 0, 0, 0);
            btnSideBar1.Size = new Size(206, 46);
            btnSideBar1.TabIndex = 2;
            btnSideBar1.Tag = "btnSideBar1";
            btnSideBar1.Text = "Prim_BTTT";
            btnSideBar1.TextAlign = ContentAlignment.MiddleLeft;
            btnSideBar1.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSideBar1.UseVisualStyleBackColor = false;
            btnSideBar1.Click += btnSideBar2_Click;
            // 
            // btnSideBar2
            // 
            btnSideBar2.AutoSize = true;
            btnSideBar2.BackColor = Color.LightSalmon;
            btnSideBar2.Cursor = Cursors.Hand;
            btnSideBar2.Dock = DockStyle.Top;
            btnSideBar2.FlatAppearance.BorderSize = 0;
            btnSideBar2.FlatStyle = FlatStyle.Flat;
            btnSideBar2.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSideBar2.ForeColor = Color.White;
            btnSideBar2.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
            btnSideBar2.IconColor = Color.Black;
            btnSideBar2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSideBar2.IconSize = 40;
            btnSideBar2.ImageAlign = ContentAlignment.MiddleLeft;
            btnSideBar2.Location = new Point(0, 56);
            btnSideBar2.Margin = new Padding(0);
            btnSideBar2.Name = "btnSideBar2";
            btnSideBar2.Padding = new Padding(10, 0, 0, 0);
            btnSideBar2.Size = new Size(206, 48);
            btnSideBar2.TabIndex = 3;
            btnSideBar2.Tag = "btnSideBar2";
            btnSideBar2.Text = "Matrix";
            btnSideBar2.TextAlign = ContentAlignment.MiddleLeft;
            btnSideBar2.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSideBar2.UseVisualStyleBackColor = false;
            btnSideBar2.Click += btnSideBar1_Click;
            // 
            // panelMenuTitle
            // 
            panelMenuTitle.BackColor = Color.Sienna;
            panelMenuTitle.Controls.Add(btnButtonMenu);
            panelMenuTitle.Controls.Add(labelMenuTitle);
            panelMenuTitle.Dock = DockStyle.Top;
            panelMenuTitle.Location = new Point(0, 0);
            panelMenuTitle.Name = "panelMenuTitle";
            panelMenuTitle.Size = new Size(206, 56);
            panelMenuTitle.TabIndex = 0;
            // 
            // btnButtonMenu
            // 
            btnButtonMenu.AutoSize = true;
            btnButtonMenu.BackColor = Color.Sienna;
            btnButtonMenu.Cursor = Cursors.Hand;
            btnButtonMenu.Dock = DockStyle.Fill;
            btnButtonMenu.FlatAppearance.BorderSize = 0;
            btnButtonMenu.FlatStyle = FlatStyle.Flat;
            btnButtonMenu.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnButtonMenu.ForeColor = Color.White;
            btnButtonMenu.IconChar = FontAwesome.Sharp.IconChar.Navicon;
            btnButtonMenu.IconColor = Color.Black;
            btnButtonMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnButtonMenu.IconSize = 28;
            btnButtonMenu.Location = new Point(0, 0);
            btnButtonMenu.Margin = new Padding(0);
            btnButtonMenu.Name = "btnButtonMenu";
            btnButtonMenu.Size = new Size(50, 56);
            btnButtonMenu.TabIndex = 1;
            btnButtonMenu.UseVisualStyleBackColor = false;
            btnButtonMenu.Click += btnButtonMenu_Click;
            // 
            // labelMenuTitle
            // 
            labelMenuTitle.Dock = DockStyle.Right;
            labelMenuTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelMenuTitle.ForeColor = Color.PeachPuff;
            labelMenuTitle.Location = new Point(50, 0);
            labelMenuTitle.Name = "labelMenuTitle";
            labelMenuTitle.Size = new Size(156, 56);
            labelMenuTitle.TabIndex = 0;
            labelMenuTitle.Text = "GENERAL GRAPH";
            labelMenuTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelMenuTitle.MouseDown += labelMenuTitle_MouseDown;
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.White;
            panelTitleBar.Controls.Add(panelControls);
            panelTitleBar.Controls.Add(labelTitle);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(206, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(947, 56);
            panelTitleBar.TabIndex = 1;
            panelTitleBar.MouseDown += panelTitle_MouseDown;
            // 
            // panelControls
            // 
            panelControls.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelControls.AutoSize = true;
            panelControls.Controls.Add(btnMinimized);
            panelControls.Controls.Add(btnFullScreen);
            panelControls.Controls.Add(btnExit);
            panelControls.Location = new Point(809, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(138, 56);
            panelControls.TabIndex = 1;
            // 
            // btnMinimized
            // 
            btnMinimized.BackColor = Color.LightSalmon;
            btnMinimized.Cursor = Cursors.Hand;
            btnMinimized.FlatAppearance.BorderSize = 0;
            btnMinimized.FlatStyle = FlatStyle.Flat;
            btnMinimized.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMinimized.ForeColor = Color.White;
            btnMinimized.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            btnMinimized.IconColor = Color.White;
            btnMinimized.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnMinimized.IconSize = 25;
            btnMinimized.Location = new Point(6, 0);
            btnMinimized.Margin = new Padding(0);
            btnMinimized.Name = "btnMinimized";
            btnMinimized.Size = new Size(44, 20);
            btnMinimized.TabIndex = 8;
            btnMinimized.UseVisualStyleBackColor = false;
            btnMinimized.Click += btnMinimized_Click;
            // 
            // btnFullScreen
            // 
            btnFullScreen.BackColor = Color.LightSalmon;
            btnFullScreen.Cursor = Cursors.Hand;
            btnFullScreen.FlatAppearance.BorderSize = 0;
            btnFullScreen.FlatStyle = FlatStyle.Flat;
            btnFullScreen.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFullScreen.ForeColor = Color.White;
            btnFullScreen.IconChar = FontAwesome.Sharp.IconChar.Expand;
            btnFullScreen.IconColor = Color.White;
            btnFullScreen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFullScreen.IconSize = 22;
            btnFullScreen.Location = new Point(50, 0);
            btnFullScreen.Margin = new Padding(0);
            btnFullScreen.Name = "btnFullScreen";
            btnFullScreen.Size = new Size(44, 20);
            btnFullScreen.TabIndex = 7;
            btnFullScreen.UseVisualStyleBackColor = false;
            btnFullScreen.Click += btnFullScreen_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Maroon;
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.White;
            btnExit.IconChar = FontAwesome.Sharp.IconChar.X;
            btnExit.IconColor = Color.White;
            btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExit.IconSize = 18;
            btnExit.Location = new Point(94, 0);
            btnExit.Margin = new Padding(0);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(44, 20);
            btnExit.TabIndex = 6;
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // labelTitle
            // 
            labelTitle.Dock = DockStyle.Left;
            labelTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitle.ForeColor = Color.DarkRed;
            labelTitle.Location = new Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(205, 56);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "DASHBOARD";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelTitle.MouseDown += labelTitle_MouseDown;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(206, 56);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(947, 452);
            panelMain.TabIndex = 2;
            // 
            // sidebarTransition
            // 
            sidebarTransition.Interval = 10;
            sidebarTransition.Tick += sidebarTransition_Tick;
            // 
            // Main
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            ClientSize = new Size(1153, 508);
            Controls.Add(panelMain);
            Controls.Add(panelTitleBar);
            Controls.Add(panelSideBarMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GENERAL GRAPH";
            Resize += Form1_Resize;
            panelSideBarMenu.ResumeLayout(false);
            panelSideBarMenu.PerformLayout();
            panelMenuTitle.ResumeLayout(false);
            panelMenuTitle.PerformLayout();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            panelControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSideBarMenu;
        private Panel panelMenuTitle;
        private Label labelMenuTitle;
        private Panel panelTitleBar;
        private Panel panelMain;
        private FontAwesome.Sharp.IconButton btnButtonMenu;
        private Label labelTitle;
        private FontAwesome.Sharp.IconButton btnSideBarExit;
        private FontAwesome.Sharp.IconButton btnSideBar1;
        private FontAwesome.Sharp.IconButton btnSideBar2;
        private FontAwesome.Sharp.IconButton btnExit;
        private FontAwesome.Sharp.IconButton btnMinimized;
        private FontAwesome.Sharp.IconButton btnFullScreen;
        private Panel panelControls;
        private System.Windows.Forms.Timer sidebarTransition;

    }
}
