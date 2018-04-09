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
            this.randWalkLabelUp = new System.Windows.Forms.Label();
            this.randWalkBoxUp = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mobileNeighborHood = new System.Windows.Forms.ComboBox();
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
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 21);
            this.label2.TabIndex = 25;
            this.label2.Text = "Starting number of agents of current state (integer)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // agentCount
            // 
            this.agentCount.Location = new System.Drawing.Point(459, 57);
            this.agentCount.Name = "agentCount";
            this.agentCount.Size = new System.Drawing.Size(121, 20);
            this.agentCount.TabIndex = 26;
            // 
            // randWalkLabelUp
            // 
            this.randWalkLabelUp.Location = new System.Drawing.Point(3, 84);
            this.randWalkLabelUp.Name = "randWalkLabelUp";
            this.randWalkLabelUp.Size = new System.Drawing.Size(125, 21);
            this.randWalkLabelUp.TabIndex = 27;
            this.randWalkLabelUp.Text = "Random walk (up)";
            this.randWalkLabelUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.randWalkLabelUp.Click += new System.EventHandler(this.label3_Click);
            // 
            // randWalkBoxUp
            // 
            this.randWalkBoxUp.Location = new System.Drawing.Point(459, 84);
            this.randWalkBoxUp.Name = "randWalkBoxUp";
            this.randWalkBoxUp.Size = new System.Drawing.Size(121, 20);
            this.randWalkBoxUp.TabIndex = 33;
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(358, 138);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(95, 21);
            this.calculateButton.TabIndex = 37;
            this.calculateButton.Text = "Calculate Others";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(450, 21);
            this.label5.TabIndex = 40;
            this.label5.Text = "Neighborhood Type";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mobileNeighborHood
            // 
            this.mobileNeighborHood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobileNeighborHood.FormattingEnabled = true;
            this.mobileNeighborHood.Items.AddRange(new object[] {
            "Von Neumann",
            "Moore"});
            this.mobileNeighborHood.Location = new System.Drawing.Point(459, 30);
            this.mobileNeighborHood.Name = "mobileNeighborHood";
            this.mobileNeighborHood.Size = new System.Drawing.Size(121, 21);
            this.mobileNeighborHood.TabIndex = 41;
            this.mobileNeighborHood.SelectedValueChanged += new System.EventHandler(this.mobileNeighborHood_SelectedValueChanged);
            // 
            // _2ndOrderTabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.mobileNeighborHood);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.randWalkBoxUp);
            this.Controls.Add(this.randWalkLabelUp);
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
        private System.Windows.Forms.Label randWalkLabelUp;
        private System.Windows.Forms.TextBox randWalkBoxUp;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mobileNeighborHood;
    }
}
