namespace Capstone_Application
{
    partial class RandomTemplateUC
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
            this.radio1D = new System.Windows.Forms.RadioButton();
            this.radio2D = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radio1D
            // 
            this.radio1D.AutoSize = true;
            this.radio1D.Location = new System.Drawing.Point(3, 3);
            this.radio1D.Name = "radio1D";
            this.radio1D.Size = new System.Drawing.Size(89, 17);
            this.radio1D.TabIndex = 0;
            this.radio1D.TabStop = true;
            this.radio1D.Text = "1-dimensional";
            this.radio1D.UseVisualStyleBackColor = true;
            // 
            // radio2D
            // 
            this.radio2D.AutoSize = true;
            this.radio2D.Location = new System.Drawing.Point(3, 26);
            this.radio2D.Name = "radio2D";
            this.radio2D.Size = new System.Drawing.Size(89, 17);
            this.radio2D.TabIndex = 1;
            this.radio2D.TabStop = true;
            this.radio2D.Text = "2-dimensional";
            this.radio2D.UseVisualStyleBackColor = true;
            // 
            // RandomTemplateUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radio2D);
            this.Controls.Add(this.radio1D);
            this.Name = "RandomTemplateUC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RadioButton radio1D;
        public System.Windows.Forms.RadioButton radio2D;
    }
}
