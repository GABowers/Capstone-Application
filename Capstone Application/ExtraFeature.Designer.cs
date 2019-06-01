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
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.thresholdBehaviorLabel = new System.Windows.Forms.Label();
            this.stochasticThresholdInput = new System.Windows.Forms.CheckBox();
            this.thresholdParameterLabel = new System.Windows.Forms.Label();
            this.initialLabel = new System.Windows.Forms.Label();
            this.initialInput = new System.Windows.Forms.TextBox();
            this.shadeInput = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(372, 0);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 0;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // nameInput
            // 
            this.nameInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nameInput.Location = new System.Drawing.Point(372, 29);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(75, 20);
            this.nameInput.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(3, 29);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(75, 20);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(3, 55);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(75, 20);
            this.typeLabel.TabIndex = 3;
            this.typeLabel.Text = "Type";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // typeInput
            // 
            this.typeInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.typeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeInput.FormattingEnabled = true;
            this.typeInput.Items.AddRange(new object[] {
            "Double"});
            this.typeInput.Location = new System.Drawing.Point(372, 54);
            this.typeInput.Name = "typeInput";
            this.typeInput.Size = new System.Drawing.Size(75, 21);
            this.typeInput.TabIndex = 4;
            // 
            // behaviorLabel
            // 
            this.behaviorLabel.Location = new System.Drawing.Point(3, 108);
            this.behaviorLabel.Name = "behaviorLabel";
            this.behaviorLabel.Size = new System.Drawing.Size(178, 20);
            this.behaviorLabel.TabIndex = 5;
            this.behaviorLabel.Text = "Iteration Behavior";
            this.behaviorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iterationBehaviorInput
            // 
            this.iterationBehaviorInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iterationBehaviorInput.Location = new System.Drawing.Point(297, 108);
            this.iterationBehaviorInput.Name = "iterationBehaviorInput";
            this.iterationBehaviorInput.Size = new System.Drawing.Size(150, 20);
            this.iterationBehaviorInput.TabIndex = 6;
            this.toolTip1.SetToolTip(this.iterationBehaviorInput, resources.GetString("iterationBehaviorInput.ToolTip"));
            // 
            // thresholdInput
            // 
            this.thresholdInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdInput.Location = new System.Drawing.Point(372, 134);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(75, 20);
            this.thresholdInput.TabIndex = 8;
            this.toolTip1.SetToolTip(this.thresholdInput, "Threshold for action");
            // 
            // thresholdBehaviorInput
            // 
            this.thresholdBehaviorInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdBehaviorInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thresholdBehaviorInput.FormattingEnabled = true;
            this.thresholdBehaviorInput.Items.AddRange(new object[] {
            "Overflow"});
            this.thresholdBehaviorInput.Location = new System.Drawing.Point(326, 160);
            this.thresholdBehaviorInput.Name = "thresholdBehaviorInput";
            this.thresholdBehaviorInput.Size = new System.Drawing.Size(121, 21);
            this.thresholdBehaviorInput.TabIndex = 9;
            this.toolTip1.SetToolTip(this.thresholdBehaviorInput, "What type of action happens when the threshold is reached.");
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.Location = new System.Drawing.Point(3, 134);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(75, 20);
            this.thresholdLabel.TabIndex = 7;
            this.thresholdLabel.Text = "Threshold";
            this.thresholdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // thresholdBehaviorLabel
            // 
            this.thresholdBehaviorLabel.Location = new System.Drawing.Point(3, 160);
            this.thresholdBehaviorLabel.Name = "thresholdBehaviorLabel";
            this.thresholdBehaviorLabel.Size = new System.Drawing.Size(150, 20);
            this.thresholdBehaviorLabel.TabIndex = 10;
            this.thresholdBehaviorLabel.Text = "Threshold Behavior";
            this.thresholdBehaviorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stochasticThresholdInput
            // 
            this.stochasticThresholdInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stochasticThresholdInput.AutoSize = true;
            this.stochasticThresholdInput.Location = new System.Drawing.Point(367, 186);
            this.stochasticThresholdInput.Name = "stochasticThresholdInput";
            this.stochasticThresholdInput.Size = new System.Drawing.Size(76, 17);
            this.stochasticThresholdInput.TabIndex = 11;
            this.stochasticThresholdInput.Text = "Stochastic";
            this.stochasticThresholdInput.UseVisualStyleBackColor = true;
            // 
            // thresholdParameterLabel
            // 
            this.thresholdParameterLabel.Location = new System.Drawing.Point(3, 186);
            this.thresholdParameterLabel.Name = "thresholdParameterLabel";
            this.thresholdParameterLabel.Size = new System.Drawing.Size(190, 20);
            this.thresholdParameterLabel.TabIndex = 12;
            this.thresholdParameterLabel.Text = "Threshold Behavior Parameters";
            this.thresholdParameterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // initialLabel
            // 
            this.initialLabel.Location = new System.Drawing.Point(3, 82);
            this.initialLabel.Name = "initialLabel";
            this.initialLabel.Size = new System.Drawing.Size(150, 20);
            this.initialLabel.TabIndex = 13;
            this.initialLabel.Text = "Initial Value";
            this.initialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // initialInput
            // 
            this.initialInput.Location = new System.Drawing.Point(347, 82);
            this.initialInput.Name = "initialInput";
            this.initialInput.Size = new System.Drawing.Size(100, 20);
            this.initialInput.TabIndex = 14;
            // 
            // shadeInput
            // 
            this.shadeInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shadeInput.AutoSize = true;
            this.shadeInput.Location = new System.Drawing.Point(400, 212);
            this.shadeInput.Name = "shadeInput";
            this.shadeInput.Size = new System.Drawing.Size(15, 14);
            this.shadeInput.TabIndex = 15;
            this.toolTip1.SetToolTip(this.shadeInput, "The agent\'s color with be shaded from dark to light as the value / threshold rati" +
        "o increases");
            this.shadeInput.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Shade agent color by value / threshold";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExtraFeature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shadeInput);
            this.Controls.Add(this.initialInput);
            this.Controls.Add(this.initialLabel);
            this.Controls.Add(this.thresholdParameterLabel);
            this.Controls.Add(this.stochasticThresholdInput);
            this.Controls.Add(this.thresholdBehaviorLabel);
            this.Controls.Add(this.thresholdBehaviorInput);
            this.Controls.Add(this.thresholdInput);
            this.Controls.Add(this.thresholdLabel);
            this.Controls.Add(this.iterationBehaviorInput);
            this.Controls.Add(this.behaviorLabel);
            this.Controls.Add(this.typeInput);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameInput);
            this.Controls.Add(this.removeButton);
            this.Name = "ExtraFeature";
            this.Size = new System.Drawing.Size(450, 259);
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
        private System.Windows.Forms.Label label1;
    }
}
