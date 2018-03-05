namespace Capstone_Application
{
    partial class Form2
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridTypeBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gridSizeHori = new System.Windows.Forms.TextBox();
            this.gridSizeVert = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stateNumberBox = new System.Windows.Forms.TextBox();
            this.neighborTypeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.caType = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cancelTab = new System.Windows.Forms.Button();
            this.confirmTab = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.previousTab = new System.Windows.Forms.Button();
            this.nextTab = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(591, 527);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridTypeBox);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.gridSizeHori);
            this.tabPage1.Controls.Add(this.gridSizeVert);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.stateNumberBox);
            this.tabPage1.Controls.Add(this.neighborTypeBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.caType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(583, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridTypeBox
            // 
            this.gridTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gridTypeBox.FormattingEnabled = true;
            this.gridTypeBox.Items.AddRange(new object[] {
            "Box",
            "Cylinder (Upright)",
            "Cylinder (Sideways)",
            "Torus"});
            this.gridTypeBox.Location = new System.Drawing.Point(147, 48);
            this.gridTypeBox.Name = "gridTypeBox";
            this.gridTypeBox.Size = new System.Drawing.Size(121, 21);
            this.gridTypeBox.TabIndex = 2;
            this.gridTypeBox.SelectedIndexChanged += new System.EventHandler(this.gridType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 21);
            this.label7.TabIndex = 10;
            this.label7.Text = "Grid Type";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridSizeHori
            // 
            this.gridSizeHori.Location = new System.Drawing.Point(147, 89);
            this.gridSizeHori.Name = "gridSizeHori";
            this.gridSizeHori.Size = new System.Drawing.Size(121, 20);
            this.gridSizeHori.TabIndex = 4;
            // 
            // gridSizeVert
            // 
            this.gridSizeVert.Location = new System.Drawing.Point(147, 109);
            this.gridSizeVert.Name = "gridSizeVert";
            this.gridSizeVert.Size = new System.Drawing.Size(121, 20);
            this.gridSizeVert.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Grid Size (Vertical)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Grid Size (Horizontal)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Number of states";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stateNumberBox
            // 
            this.stateNumberBox.Location = new System.Drawing.Point(147, 69);
            this.stateNumberBox.Name = "stateNumberBox";
            this.stateNumberBox.Size = new System.Drawing.Size(121, 20);
            this.stateNumberBox.TabIndex = 3;
            this.stateNumberBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // neighborTypeBox
            // 
            this.neighborTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neighborTypeBox.FormattingEnabled = true;
            this.neighborTypeBox.Items.AddRange(new object[] {
            "None",
            "Von Neumann",
            "Moore",
            "Hybrid",
            "Advanced"});
            this.neighborTypeBox.Location = new System.Drawing.Point(147, 27);
            this.neighborTypeBox.Name = "neighborTypeBox";
            this.neighborTypeBox.Size = new System.Drawing.Size(121, 21);
            this.neighborTypeBox.TabIndex = 1;
            this.neighborTypeBox.SelectedIndexChanged += new System.EventHandler(this.neighborType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Neighborhood Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "CA Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // caType
            // 
            this.caType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.caType.FormattingEnabled = true;
            this.caType.Items.AddRange(new object[] {
            "First Order",
            "Second Order"});
            this.caType.Location = new System.Drawing.Point(147, 6);
            this.caType.Name = "caType";
            this.caType.Size = new System.Drawing.Size(121, 21);
            this.caType.TabIndex = 0;
            this.caType.SelectedIndexChanged += new System.EventHandler(this.caType_SelectedIndexChanged);
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // cancelTab
            // 
            this.cancelTab.Location = new System.Drawing.Point(465, 532);
            this.cancelTab.Name = "cancelTab";
            this.cancelTab.Size = new System.Drawing.Size(60, 28);
            this.cancelTab.TabIndex = 4;
            this.cancelTab.Text = "Cancel";
            this.cancelTab.UseVisualStyleBackColor = true;
            this.cancelTab.Click += new System.EventHandler(this.cancelTab_Click);
            // 
            // confirmTab
            // 
            this.confirmTab.Location = new System.Drawing.Point(531, 532);
            this.confirmTab.Name = "confirmTab";
            this.confirmTab.Size = new System.Drawing.Size(60, 28);
            this.confirmTab.TabIndex = 2;
            this.confirmTab.Text = "Confirm";
            this.confirmTab.UseVisualStyleBackColor = true;
            this.confirmTab.Click += new System.EventHandler(this.confirmTab_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 533);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(287, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tooltip and event notification";
            // 
            // previousTab
            // 
            this.previousTab.Location = new System.Drawing.Point(293, 532);
            this.previousTab.Name = "previousTab";
            this.previousTab.Size = new System.Drawing.Size(80, 28);
            this.previousTab.TabIndex = 3;
            this.previousTab.Text = "Previous Tab";
            this.previousTab.UseVisualStyleBackColor = true;
            this.previousTab.Click += new System.EventHandler(this.previousTab_Click);
            // 
            // nextTab
            // 
            this.nextTab.Location = new System.Drawing.Point(379, 532);
            this.nextTab.Name = "nextTab";
            this.nextTab.Size = new System.Drawing.Size(80, 28);
            this.nextTab.TabIndex = 1;
            this.nextTab.Text = "Next Tab";
            this.nextTab.UseVisualStyleBackColor = true;
            this.nextTab.Click += new System.EventHandler(this.nextTab_Click);
            // 
            // Form2
            // 
            this.AccessibleName = "New Model Dialog";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 566);
            this.Controls.Add(this.nextTab);
            this.Controls.Add(this.previousTab);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.confirmTab);
            this.Controls.Add(this.cancelTab);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form2";
            this.Text = "Settings Dialog";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox caType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stateNumberBox;
        private System.Windows.Forms.ComboBox neighborTypeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gridSizeHori;
        private System.Windows.Forms.TextBox gridSizeVert;
        private System.Windows.Forms.ComboBox gridTypeBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button cancelTab;
        private System.Windows.Forms.Button confirmTab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button previousTab;
        private System.Windows.Forms.Button nextTab;
    }
}