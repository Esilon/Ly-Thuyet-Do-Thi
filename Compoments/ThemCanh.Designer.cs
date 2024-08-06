namespace Đồ_Thị.Compoments
{
    partial class ThemCanh
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemCanh));
            panel1 = new Panel();
            numericUpDown1 = new NumericUpDown();
            button3 = new Button();
            btn_Plus = new FontAwesome.Sharp.IconButton();
            btn_Minus = new FontAwesome.Sharp.IconButton();
            button5 = new Button();
            label1 = new Label();
            label2 = new Label();
            VertexFirst = new TextBox();
            VertexSecondary = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(numericUpDown1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(btn_Plus);
            panel1.Controls.Add(btn_Minus);
            panel1.Controls.Add(button5);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(110, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(193, 138);
            panel1.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Enabled = false;
            numericUpDown1.Location = new Point(87, 44);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(51, 23);
            numericUpDown1.TabIndex = 10;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            // 
            // button3
            // 
            button3.Location = new Point(11, 102);
            button3.Name = "button3";
            button3.Size = new Size(70, 24);
            button3.TabIndex = 8;
            button3.Text = "Thoát";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnCancel_Click;
            // 
            // btn_Plus
            // 
            btn_Plus.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btn_Plus.IconColor = Color.Black;
            btn_Plus.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btn_Plus.IconSize = 30;
            btn_Plus.Location = new Point(144, 36);
            btn_Plus.Name = "btn_Plus";
            btn_Plus.Size = new Size(45, 34);
            btn_Plus.TabIndex = 7;
            btn_Plus.UseVisualStyleBackColor = true;
            btn_Plus.Click += btn_Plus_Click;
            // 
            // btn_Minus
            // 
            btn_Minus.IconChar = FontAwesome.Sharp.IconChar.Minus;
            btn_Minus.IconColor = Color.Black;
            btn_Minus.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btn_Minus.IconSize = 30;
            btn_Minus.Location = new Point(36, 34);
            btn_Minus.Name = "btn_Minus";
            btn_Minus.Size = new Size(45, 36);
            btn_Minus.TabIndex = 6;
            btn_Minus.UseVisualStyleBackColor = true;
            btn_Minus.Click += btn_Minus_Click;
            // 
            // button5
            // 
            button5.Location = new Point(111, 102);
            button5.Name = "button5";
            button5.Size = new Size(70, 24);
            button5.TabIndex = 3;
            button5.Text = "Tiếp tục";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnAddEdge_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 43);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 4;
            label1.Text = "Đỉnh 1:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 67);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 5;
            label2.Text = "Đỉnh 2:";
            // 
            // VertexFirst
            // 
            VertexFirst.Location = new Point(52, 40);
            VertexFirst.Name = "VertexFirst";
            VertexFirst.ReadOnly = true;
            VertexFirst.Size = new Size(52, 23);
            VertexFirst.TabIndex = 6;
            // 
            // VertexSecondary
            // 
            VertexSecondary.Location = new Point(52, 64);
            VertexSecondary.Name = "VertexSecondary";
            VertexSecondary.ReadOnly = true;
            VertexSecondary.Size = new Size(52, 23);
            VertexSecondary.TabIndex = 7;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(11, 102);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(93, 19);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Có trọng số?";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBoxHasWeight_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(12, 12);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(84, 19);
            checkBox2.TabIndex = 9;
            checkBox2.Text = "Có hướng?";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // ThemCanh
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 138);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(VertexSecondary);
            Controls.Add(VertexFirst);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ThemCanh";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ThemCanh";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Button button5;
        private FontAwesome.Sharp.IconButton btn_Plus;
        private FontAwesome.Sharp.IconButton btn_Minus;
        private Button button3;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private Label label2;
        private TextBox VertexFirst;
        private TextBox VertexSecondary;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
    }
}