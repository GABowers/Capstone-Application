namespace Capstone_Application
{
    partial class ExtraPanel
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
            this.addLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addLabel
            // 
            this.addLabel.Location = new System.Drawing.Point(3, 3);
            this.addLabel.Name = "addLabel";
            this.addLabel.Size = new System.Drawing.Size(549, 23);
            this.addLabel.TabIndex = 0;
            this.addLabel.Text = "Add extra elements to the agent and define their behavior.";
            this.addLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(450, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // ExtraPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.addLabel);
            this.Name = "ExtraPanel";
            this.Size = new System.Drawing.Size(555, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label addLabel;
        private System.Windows.Forms.Button addButton;
    }
}
