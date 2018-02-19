namespace Capstone_Application
{
    partial class _2ndOrderTabs
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
            this.label1 = new System.Windows.Forms.Label();
            this.colorBox = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.agentCount = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.walkUpBox = new System.Windows.Forms.TextBox();
            this.walkRightBox = new System.Windows.Forms.TextBox();
            this.walkDownBox = new System.Windows.Forms.TextBox();
            this.walkLeftBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(450, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "Color of state";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.Color.White;
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(459, 3);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(121, 21);
            this.colorBox.TabIndex = 24;
            this.colorBox.Text = "Click Here";
            this.colorBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 21);
            this.label2.TabIndex = 25;
            this.label2.Text = "Starting number of agents of current state (integer)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // agentCount
            // 
            this.agentCount.Location = new System.Drawing.Point(459, 30);
            this.agentCount.Name = "agentCount";
            this.agentCount.Size = new System.Drawing.Size(121, 20);
            this.agentCount.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 21);
            this.label3.TabIndex = 27;
            this.label3.Text = "Random walk (up)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 21);
            this.label6.TabIndex = 30;
            this.label6.Text = "Random walk (right)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 21);
            this.label7.TabIndex = 31;
            this.label7.Text = "Random walk (down) (gravity)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 21);
            this.label8.TabIndex = 32;
            this.label8.Text = "Random walk (left)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // walkUpBox
            // 
            this.walkUpBox.Location = new System.Drawing.Point(459, 57);
            this.walkUpBox.Name = "walkUpBox";
            this.walkUpBox.Size = new System.Drawing.Size(121, 20);
            this.walkUpBox.TabIndex = 33;
            // 
            // walkRightBox
            // 
            this.walkRightBox.Location = new System.Drawing.Point(459, 84);
            this.walkRightBox.Name = "walkRightBox";
            this.walkRightBox.Size = new System.Drawing.Size(121, 20);
            this.walkRightBox.TabIndex = 34;
            // 
            // walkDownBox
            // 
            this.walkDownBox.Location = new System.Drawing.Point(459, 111);
            this.walkDownBox.Name = "walkDownBox";
            this.walkDownBox.Size = new System.Drawing.Size(121, 20);
            this.walkDownBox.TabIndex = 35;
            // 
            // walkLeftBox
            // 
            this.walkLeftBox.Location = new System.Drawing.Point(459, 138);
            this.walkLeftBox.Name = "walkLeftBox";
            this.walkLeftBox.Size = new System.Drawing.Size(121, 20);
            this.walkLeftBox.TabIndex = 36;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(358, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 21);
            this.button1.TabIndex = 37;
            this.button1.Text = "Calculate Others";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _2ndOrderTabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.walkLeftBox);
            this.Controls.Add(this.walkDownBox);
            this.Controls.Add(this.walkRightBox);
            this.Controls.Add(this.walkUpBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.agentCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.label1);
            this.Name = "_2ndOrderTabs";
            this.Size = new System.Drawing.Size(584, 501);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label colorBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox agentCount;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox walkUpBox;
        private System.Windows.Forms.TextBox walkRightBox;
        private System.Windows.Forms.TextBox walkDownBox;
        private System.Windows.Forms.TextBox walkLeftBox;
        private System.Windows.Forms.Button button1;
    }
}
