namespace Capstone_Application
{
    partial class To_State_Panel
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
            this.toStatePanel = new System.Windows.Forms.Panel();
            this.toStateLabel = new System.Windows.Forms.Label();
            this.toStatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toStatePanel
            // 
            this.toStatePanel.AutoScroll = true;
            this.toStatePanel.AutoSize = true;
            this.toStatePanel.Controls.Add(this.toStateLabel);
            this.toStatePanel.Location = new System.Drawing.Point(0, 0);
            this.toStatePanel.Name = "toStatePanel";
            this.toStatePanel.Size = new System.Drawing.Size(580, 20);
            this.toStatePanel.TabIndex = 26;
            // 
            // toStateLabel
            // 
            this.toStateLabel.AutoSize = true;
            this.toStateLabel.Location = new System.Drawing.Point(5, 5);
            this.toStateLabel.Name = "toStateLabel";
            this.toStateLabel.Size = new System.Drawing.Size(145, 13);
            this.toStateLabel.TabIndex = 5;
            this.toStateLabel.Text = "To state X, with neighbors of:";
            // 
            // To_State_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.toStatePanel);
            this.Name = "To_State_Panel";
            this.Size = new System.Drawing.Size(583, 23);
            this.toStatePanel.ResumeLayout(false);
            this.toStatePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel toStatePanel;
        internal System.Windows.Forms.Label toStateLabel;
    }
}
