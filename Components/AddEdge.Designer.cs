namespace DoThi.Components
{
    partial class AddEdge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEdge));
            this.panelControls = new System.Windows.Forms.Panel();
            this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPlus = new FontAwesome.Sharp.IconButton();
            this.btnMinus = new FontAwesome.Sharp.IconButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelVertex1 = new System.Windows.Forms.TextBox();
            this.labelVertex2 = new System.Windows.Forms.TextBox();
            this.checkBoxHasWeight = new System.Windows.Forms.CheckBox();
            this.checkBoxIsDirected = new System.Windows.Forms.CheckBox();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
            this.SuspendLayout();
            //
            // panelControls
            //
            this.panelControls.Controls.Add(this.numericUpDownWeight);
            this.panelControls.Controls.Add(this.btnCancel);
            this.panelControls.Controls.Add(this.btnPlus);
            this.panelControls.Controls.Add(this.btnMinus);
            this.panelControls.Controls.Add(this.btnAdd);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(110, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(193, 138);
            this.panelControls.TabIndex = 2;
            //
            // numericUpDownWeight
            //
            this.numericUpDownWeight.Enabled = false;
            this.numericUpDownWeight.Location = new System.Drawing.Point(87, 44);
            this.numericUpDownWeight.Name = "numericUpDownWeight";
            this.numericUpDownWeight.Size = new System.Drawing.Size(51, 23);
            this.numericUpDownWeight.TabIndex = 10;
            this.numericUpDownWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(11, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // btnPlus
            //
            this.btnPlus.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnPlus.IconColor = System.Drawing.Color.Black;
            this.btnPlus.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPlus.IconSize = 30;
            this.btnPlus.Location = new System.Drawing.Point(144, 36);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(45, 34);
            this.btnPlus.TabIndex = 7;
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            //
            // btnMinus
            //
            this.btnMinus.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMinus.IconColor = System.Drawing.Color.Black;
            this.btnMinus.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnMinus.IconSize = 30;
            this.btnMinus.Location = new System.Drawing.Point(36, 34);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(45, 36);
            this.btnMinus.TabIndex = 6;
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            //
            // btnAdd
            //
            this.btnAdd.Location = new System.Drawing.Point(111, 102);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 24);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Continue";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vertex 1:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Vertex 2:";
            //
            // labelVertex1
            //
            this.labelVertex1.Location = new System.Drawing.Point(52, 40);
            this.labelVertex1.Name = "labelVertex1";
            this.labelVertex1.ReadOnly = true;
            this.labelVertex1.Size = new System.Drawing.Size(52, 23);
            this.labelVertex1.TabIndex = 6;
            //
            // labelVertex2
            //
            this.labelVertex2.Location = new System.Drawing.Point(52, 64);
            this.labelVertex2.Name = "labelVertex2";
            this.labelVertex2.ReadOnly = true;
            this.labelVertex2.Size = new System.Drawing.Size(52, 23);
            this.labelVertex2.TabIndex = 7;
            //
            // checkBoxHasWeight
            //
            this.checkBoxHasWeight.AutoSize = true;
            this.checkBoxHasWeight.Location = new System.Drawing.Point(11, 102);
            this.checkBoxHasWeight.Name = "checkBoxHasWeight";
            this.checkBoxHasWeight.Size = new System.Drawing.Size(90, 19);
            this.checkBoxHasWeight.TabIndex = 8;
            this.checkBoxHasWeight.Text = "Has Weight?";
            this.checkBoxHasWeight.UseVisualStyleBackColor = true;
            this.checkBoxHasWeight.CheckedChanged += new System.EventHandler(this.checkBoxHasWeight_CheckedChanged);
            //
            // checkBoxIsDirected
            //
            this.checkBoxIsDirected.AutoSize = true;
            this.checkBoxIsDirected.Location = new System.Drawing.Point(12, 12);
            this.checkBoxIsDirected.Name = "checkBoxIsDirected";
            this.checkBoxIsDirected.Size = new System.Drawing.Size(84, 19);
            this.checkBoxIsDirected.TabIndex = 9;
            this.checkBoxIsDirected.Text = "Is Directed?";
            this.checkBoxIsDirected.UseVisualStyleBackColor = true;
            //
            // AddEdge
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 138);
            this.Controls.Add(this.checkBoxIsDirected);
            this.Controls.Add(this.checkBoxHasWeight);
            this.Controls.Add(this.labelVertex2);
            this.Controls.Add(this.labelVertex1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelControls);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEdge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Edge";
            this.panelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Button btnAdd;
        private FontAwesome.Sharp.IconButton btnPlus;
        private FontAwesome.Sharp.IconButton btnMinus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox labelVertex1;
        private System.Windows.Forms.TextBox labelVertex2;
        private System.Windows.Forms.CheckBox checkBoxHasWeight;
        private System.Windows.Forms.CheckBox checkBoxIsDirected;
    }
}
