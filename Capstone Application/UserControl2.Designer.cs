namespace Capstone_Application
{
    partial class UserControl2
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
            this.agentCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorBox = new System.Windows.Forms.Label();
            this.fullPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // agentCount
            // 
            this.agentCount.Location = new System.Drawing.Point(459, 30);
            this.agentCount.Name = "agentCount";
            this.agentCount.Size = new System.Drawing.Size(121, 20);
            this.agentCount.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 21);
            this.label2.TabIndex = 14;
            this.label2.Text = "Starting number of agents of current state (integer)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(450, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "Color of state";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorDialog1
            // 
            this.colorDialog1.SolidColorOnly = true;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.Color.White;
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(459, 3);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(121, 21);
            this.colorBox.TabIndex = 23;
            this.colorBox.Text = "Click Here";
            this.colorBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // fullPanel
            // 
            this.fullPanel.AutoScroll = true;
            this.fullPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fullPanel.Location = new System.Drawing.Point(0, 56);
            this.fullPanel.Name = "fullPanel";
            this.fullPanel.Size = new System.Drawing.Size(584, 445);
            this.fullPanel.TabIndex = 26;
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.fullPanel);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.agentCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(584, 501);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox agentCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label colorBox;
        private System.Windows.Forms.Panel fullPanel;
    }
}
