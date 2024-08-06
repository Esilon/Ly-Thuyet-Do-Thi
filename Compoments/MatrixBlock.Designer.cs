namespace Đồ_Thị.uc
{
    partial class MatrixBlock
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
            MatrixP = new Panel();
            SuspendLayout();
            // 
            // MatrixP
            // 
            MatrixP.BackColor = SystemColors.ControlDark;
            MatrixP.Dock = DockStyle.Fill;
            MatrixP.Location = new Point(0, 0);
            MatrixP.Name = "MatrixP";
            MatrixP.Size = new Size(1037, 537);
            MatrixP.TabIndex = 0;
            // 
            // MatrixBlock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MatrixP);
            Name = "MatrixBlock";
            Size = new Size(1037, 537);
            ResumeLayout(false);
        }

        #endregion

        private Panel MatrixP;
    }
}
