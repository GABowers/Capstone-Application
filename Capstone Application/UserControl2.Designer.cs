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
            this.numberLabel = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorBox = new System.Windows.Forms.Label();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.mobilityLabel = new System.Windows.Forms.Label();
            this.neighborLabel = new System.Windows.Forms.Label();
            this.edgeLabel = new System.Windows.Forms.Label();
            this.neighborBox = new System.Windows.Forms.ComboBox();
            this.mobilityBox = new System.Windows.Forms.ComboBox();
            this.edgeBox = new System.Windows.Forms.ComboBox();
            this.mobilityPanel = new System.Windows.Forms.Panel();
            this.neighborhoodPanel = new System.Windows.Forms.Panel();
            this.mobilityButtonsPanel = new System.Windows.Forms.Panel();
            this.mobilityInputPanel = new System.Windows.Forms.Panel();
            this.inputPanel.SuspendLayout();
            this.mobilityPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // agentCount
            // 
            this.agentCount.Location = new System.Drawing.Point(439, 29);
            this.agentCount.Name = "agentCount";
            this.agentCount.Size = new System.Drawing.Size(121, 20);
            this.agentCount.TabIndex = 21;
            this.agentCount.TextChanged += new System.EventHandler(this.agentCount_TextChanged);
            // 
            // numberLabel
            // 
            this.numberLabel.Location = new System.Drawing.Point(3, 29);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(450, 21);
            this.numberLabel.TabIndex = 14;
            this.numberLabel.Text = "Starting number of agents of current state (integer)";
            this.numberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorLabel
            // 
            this.colorLabel.Location = new System.Drawing.Point(3, 3);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(450, 21);
            this.colorLabel.TabIndex = 13;
            this.colorLabel.Text = "Color of state";
            this.colorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorDialog1
            // 
            this.colorDialog1.SolidColorOnly = true;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.Color.White;
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(439, 3);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(121, 21);
            this.colorBox.TabIndex = 23;
            this.colorBox.Text = "Click Here";
            this.colorBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // inputPanel
            // 
            this.inputPanel.AutoScroll = true;
            this.inputPanel.AutoSize = true;
            this.inputPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inputPanel.Controls.Add(this.neighborhoodPanel);
            this.inputPanel.Controls.Add(this.mobilityPanel);
            this.inputPanel.Location = new System.Drawing.Point(0, 128);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(0, 0);
            this.inputPanel.TabIndex = 26;
            // 
            // mobilityLabel
            // 
            this.mobilityLabel.Location = new System.Drawing.Point(3, 55);
            this.mobilityLabel.Name = "mobilityLabel";
            this.mobilityLabel.Size = new System.Drawing.Size(450, 21);
            this.mobilityLabel.TabIndex = 27;
            this.mobilityLabel.Text = "Mobility - define whether or not an agent moves";
            this.mobilityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neighborLabel
            // 
            this.neighborLabel.Location = new System.Drawing.Point(3, 81);
            this.neighborLabel.Name = "neighborLabel";
            this.neighborLabel.Size = new System.Drawing.Size(450, 21);
            this.neighborLabel.TabIndex = 28;
            this.neighborLabel.Text = "Neighborhood - set which local positions the agent looks to for state changes";
            this.neighborLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edgeLabel
            // 
            this.edgeLabel.Location = new System.Drawing.Point(3, 107);
            this.edgeLabel.Name = "edgeLabel";
            this.edgeLabel.Size = new System.Drawing.Size(450, 21);
            this.edgeLabel.TabIndex = 29;
            this.edgeLabel.Text = "Edge behavior - allow or disallow vertical or horizontal movement to the grid\'s o" +
    "pposite side";
            this.edgeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neighborBox
            // 
            this.neighborBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neighborBox.FormattingEnabled = true;
            this.neighborBox.Items.AddRange(new object[] {
            "None",
            "Von Neumann",
            "Moore",
            "Hybrid",
            "Advanced"});
            this.neighborBox.Location = new System.Drawing.Point(439, 81);
            this.neighborBox.Name = "neighborBox";
            this.neighborBox.Size = new System.Drawing.Size(121, 21);
            this.neighborBox.TabIndex = 30;
            this.neighborBox.SelectedIndexChanged += new System.EventHandler(this.neighborBox_SelectedIndexChanged);
            // 
            // mobilityBox
            // 
            this.mobilityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobilityBox.FormattingEnabled = true;
            this.mobilityBox.Items.AddRange(new object[] {
            "Immobile",
            "Mobile"});
            this.mobilityBox.Location = new System.Drawing.Point(439, 55);
            this.mobilityBox.Name = "mobilityBox";
            this.mobilityBox.Size = new System.Drawing.Size(121, 21);
            this.mobilityBox.TabIndex = 31;
            this.mobilityBox.SelectedIndexChanged += new System.EventHandler(this.mobilityBox_SelectedIndexChanged);
            // 
            // edgeBox
            // 
            this.edgeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edgeBox.FormattingEnabled = true;
            this.edgeBox.Items.AddRange(new object[] {
            "None",
            "Horizontal",
            "Vertical",
            "Both"});
            this.edgeBox.Location = new System.Drawing.Point(439, 107);
            this.edgeBox.Name = "edgeBox";
            this.edgeBox.Size = new System.Drawing.Size(121, 21);
            this.edgeBox.TabIndex = 32;
            this.edgeBox.SelectedIndexChanged += new System.EventHandler(this.edgeBox_SelectedIndexChanged);
            // 
            // mobilityPanel
            // 
            this.mobilityPanel.AutoSize = true;
            this.mobilityPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mobilityPanel.Controls.Add(this.mobilityInputPanel);
            this.mobilityPanel.Controls.Add(this.mobilityButtonsPanel);
            this.mobilityPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mobilityPanel.Location = new System.Drawing.Point(0, 0);
            this.mobilityPanel.Name = "mobilityPanel";
            this.mobilityPanel.Size = new System.Drawing.Size(0, 0);
            this.mobilityPanel.TabIndex = 0;
            // 
            // neighborhoodPanel
            // 
            this.neighborhoodPanel.AutoSize = true;
            this.neighborhoodPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.neighborhoodPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neighborhoodPanel.Location = new System.Drawing.Point(0, 0);
            this.neighborhoodPanel.Name = "neighborhoodPanel";
            this.neighborhoodPanel.Size = new System.Drawing.Size(0, 0);
            this.neighborhoodPanel.TabIndex = 1;
            // 
            // mobilityButtonsPanel
            // 
            this.mobilityButtonsPanel.AutoSize = true;
            this.mobilityButtonsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mobilityButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mobilityButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.mobilityButtonsPanel.Name = "mobilityButtonsPanel";
            this.mobilityButtonsPanel.Size = new System.Drawing.Size(0, 0);
            this.mobilityButtonsPanel.TabIndex = 0;
            // 
            // mobilityInputPanel
            // 
            this.mobilityInputPanel.AutoSize = true;
            this.mobilityInputPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mobilityInputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mobilityInputPanel.Location = new System.Drawing.Point(0, 0);
            this.mobilityInputPanel.Name = "mobilityInputPanel";
            this.mobilityInputPanel.Size = new System.Drawing.Size(0, 0);
            this.mobilityInputPanel.TabIndex = 1;
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.edgeBox);
            this.Controls.Add(this.mobilityBox);
            this.Controls.Add(this.neighborBox);
            this.Controls.Add(this.edgeLabel);
            this.Controls.Add(this.neighborLabel);
            this.Controls.Add(this.mobilityLabel);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.agentCount);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.colorLabel);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(584, 501);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.mobilityPanel.ResumeLayout(false);
            this.mobilityPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox agentCount;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label colorBox;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Label mobilityLabel;
        private System.Windows.Forms.Label neighborLabel;
        private System.Windows.Forms.Label edgeLabel;
        private System.Windows.Forms.ComboBox neighborBox;
        private System.Windows.Forms.ComboBox mobilityBox;
        private System.Windows.Forms.ComboBox edgeBox;
        private System.Windows.Forms.Panel neighborhoodPanel;
        private System.Windows.Forms.Panel mobilityPanel;
        private System.Windows.Forms.Panel mobilityInputPanel;
        private System.Windows.Forms.Panel mobilityButtonsPanel;
    }
}
