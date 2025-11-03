namespace DoThi.Components
{
    partial class MatrixBlock
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelMatrix = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // panelMatrix
            //
            this.panelMatrix.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMatrix.Location = new System.Drawing.Point(0, 0);
            this.panelMatrix.Name = "panelMatrix";
            this.panelMatrix.Size = new System.Drawing.Size(1037, 537);
            this.panelMatrix.TabIndex = 0;
            //
            // MatrixBlock
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMatrix);
            this.Name = "MatrixBlock";
            this.Size = new System.Drawing.Size(1037, 537);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMatrix;
    }
}
