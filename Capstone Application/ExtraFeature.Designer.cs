namespace Capstone_Application
{
    partial class ExtraFeature
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraFeature));
            this.removeButton = new System.Windows.Forms.Button();
            this.nameInput = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeInput = new System.Windows.Forms.ComboBox();
            this.behaviorLabel = new System.Windows.Forms.Label();
            this.iterationBehaviorInput = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.thresholdBehaviorInput = new System.Windows.Forms.ComboBox();
            this.shadeInput = new System.Windows.Forms.CheckBox();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.thresholdBehaviorLabel = new System.Windows.Forms.Label();
            this.stochasticThresholdInput = new System.Windows.Forms.CheckBox();
            this.thresholdParameterLabel = new System.Windows.Forms.Label();
            this.initialLabel = new System.Windows.Forms.Label();
            this.initialInput = new System.Windows.Forms.TextBox();
            this.shadeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.thresholdTypeInput = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // removeButton
            // 
            this.removeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeButton.Location = new System.Drawing.Point(237, 3);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(228, 24);
            this.removeButton.TabIndex = 0;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // nameInput
            // 
            this.nameInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameInput.Location = new System.Drawing.Point(237, 33);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(228, 20);
            this.nameInput.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Location = new System.Drawing.Point(3, 30);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(228, 30);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // typeLabel
            // 
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeLabel.Location = new System.Drawing.Point(3, 60);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(228, 30);
            this.typeLabel.TabIndex = 3;
            this.typeLabel.Text = "Type";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // typeInput
            // 
            this.typeInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeInput.FormattingEnabled = true;
            this.typeInput.Items.AddRange(new object[] {
            "Double"});
            this.typeInput.Location = new System.Drawing.Point(237, 63);
            this.typeInput.Name = "typeInput";
            this.typeInput.Size = new System.Drawing.Size(228, 21);
            this.typeInput.TabIndex = 4;
            // 
            // behaviorLabel
            // 
            this.behaviorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.behaviorLabel.Location = new System.Drawing.Point(3, 120);
            this.behaviorLabel.Name = "behaviorLabel";
            this.behaviorLabel.Size = new System.Drawing.Size(228, 30);
            this.behaviorLabel.TabIndex = 5;
            this.behaviorLabel.Text = "Iteration Behavior";
            this.behaviorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iterationBehaviorInput
            // 
            this.iterationBehaviorInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iterationBehaviorInput.Location = new System.Drawing.Point(237, 123);
            this.iterationBehaviorInput.Name = "iterationBehaviorInput";
            this.iterationBehaviorInput.Size = new System.Drawing.Size(228, 20);
            this.iterationBehaviorInput.TabIndex = 6;
            this.toolTip1.SetToolTip(this.iterationBehaviorInput, resources.GetString("iterationBehaviorInput.ToolTip"));
            // 
            // thresholdInput
            // 
            this.thresholdInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thresholdInput.Location = new System.Drawing.Point(117, 3);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(108, 20);
            this.thresholdInput.TabIndex = 8;
            this.toolTip1.SetToolTip(this.thresholdInput, "Threshold for action");
            // 
            // thresholdBehaviorInput
            // 
            this.thresholdBehaviorInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thresholdBehaviorInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thresholdBehaviorInput.FormattingEnabled = true;
            this.thresholdBehaviorInput.Location = new System.Drawing.Point(237, 183);
            this.thresholdBehaviorInput.Name = "thresholdBehaviorInput";
            this.thresholdBehaviorInput.Size = new System.Drawing.Size(228, 21);
            this.thresholdBehaviorInput.TabIndex = 9;
            this.toolTip1.SetToolTip(this.thresholdBehaviorInput, "What type of action happens when the threshold is reached.");
            // 
            // shadeInput
            // 
            this.shadeInput.AutoSize = true;
            this.shadeInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadeInput.Location = new System.Drawing.Point(237, 243);
            this.shadeInput.Name = "shadeInput";
            this.shadeInput.Size = new System.Drawing.Size(228, 24);
            this.shadeInput.TabIndex = 15;
            this.toolTip1.SetToolTip(this.shadeInput, "The agent\'s color with be shaded from dark to light as the value / threshold rati" +
        "o increases");
            this.shadeInput.UseVisualStyleBackColor = true;
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thresholdLabel.Location = new System.Drawing.Point(3, 150);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(228, 30);
            this.thresholdLabel.TabIndex = 7;
            this.thresholdLabel.Text = "Threshold";
            this.thresholdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // thresholdBehaviorLabel
            // 
            this.thresholdBehaviorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thresholdBehaviorLabel.Location = new System.Drawing.Point(3, 180);
            this.thresholdBehaviorLabel.Name = "thresholdBehaviorLabel";
            this.thresholdBehaviorLabel.Size = new System.Drawing.Size(228, 30);
            this.thresholdBehaviorLabel.TabIndex = 10;
            this.thresholdBehaviorLabel.Text = "Threshold Behavior";
            this.thresholdBehaviorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stochasticThresholdInput
            // 
            this.stochasticThresholdInput.AutoSize = true;
            this.stochasticThresholdInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stochasticThresholdInput.Location = new System.Drawing.Point(237, 213);
            this.stochasticThresholdInput.Name = "stochasticThresholdInput";
            this.stochasticThresholdInput.Size = new System.Drawing.Size(228, 24);
            this.stochasticThresholdInput.TabIndex = 11;
            this.stochasticThresholdInput.Text = "Stochastic";
            this.stochasticThresholdInput.UseVisualStyleBackColor = true;
            // 
            // thresholdParameterLabel
            // 
            this.thresholdParameterLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thresholdParameterLabel.Location = new System.Drawing.Point(3, 210);
            this.thresholdParameterLabel.Name = "thresholdParameterLabel";
            this.thresholdParameterLabel.Size = new System.Drawing.Size(228, 30);
            this.thresholdParameterLabel.TabIndex = 12;
            this.thresholdParameterLabel.Text = "Threshold Behavior Parameters";
            this.thresholdParameterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // initialLabel
            // 
            this.initialLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.initialLabel.Location = new System.Drawing.Point(3, 90);
            this.initialLabel.Name = "initialLabel";
            this.initialLabel.Size = new System.Drawing.Size(228, 30);
            this.initialLabel.TabIndex = 13;
            this.initialLabel.Text = "Initial Value";
            this.initialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // initialInput
            // 
            this.initialInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.initialInput.Location = new System.Drawing.Point(237, 93);
            this.initialInput.Name = "initialInput";
            this.initialInput.Size = new System.Drawing.Size(228, 20);
            this.initialInput.TabIndex = 14;
            // 
            // shadeLabel
            // 
            this.shadeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadeLabel.Location = new System.Drawing.Point(3, 240);
            this.shadeLabel.Name = "shadeLabel";
            this.shadeLabel.Size = new System.Drawing.Size(228, 30);
            this.shadeLabel.TabIndex = 16;
            this.shadeLabel.Text = "Shade agent color by value / threshold";
            this.shadeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.thresholdBehaviorInput, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.shadeInput, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.shadeLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.stochasticThresholdInput, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.thresholdParameterLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.thresholdBehaviorLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.thresholdLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.iterationBehaviorInput, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.behaviorLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.initialInput, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.initialLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.typeInput, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.typeLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nameInput, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.removeButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 270);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.thresholdInput, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.thresholdTypeInput, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(237, 153);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(228, 24);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // thresholdTypeInput
            // 
            this.thresholdTypeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thresholdTypeInput.FormattingEnabled = true;
            this.thresholdTypeInput.Items.AddRange(new object[] {
            "x <threshold",
            "x <= threshold",
            "x == threshold",
            "x >= threshold",
            "x > threshold"});
            this.thresholdTypeInput.Location = new System.Drawing.Point(3, 3);
            this.thresholdTypeInput.Name = "thresholdTypeInput";
            this.thresholdTypeInput.Size = new System.Drawing.Size(108, 21);
            this.thresholdTypeInput.TabIndex = 9;
            // 
            // ExtraFeature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExtraFeature";
            this.Size = new System.Drawing.Size(468, 270);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox nameInput;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox typeInput;
        private System.Windows.Forms.Label behaviorLabel;
        private System.Windows.Forms.TextBox iterationBehaviorInput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.TextBox thresholdInput;
        private System.Windows.Forms.ComboBox thresholdBehaviorInput;
        private System.Windows.Forms.Label thresholdBehaviorLabel;
        private System.Windows.Forms.CheckBox stochasticThresholdInput;
        private System.Windows.Forms.Label thresholdParameterLabel;
        public System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label initialLabel;
        private System.Windows.Forms.TextBox initialInput;
        private System.Windows.Forms.CheckBox shadeInput;
        private System.Windows.Forms.Label shadeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox thresholdTypeInput;
    }
}
