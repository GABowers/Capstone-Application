namespace Capstone_Application
{
    partial class GasTemplateUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GasTemplateUC));
            this.gasLabel = new System.Windows.Forms.Label();
            this.moleculeInput = new System.Windows.Forms.TextBox();
            this.gasTemp = new System.Windows.Forms.Label();
            this.tempInput = new System.Windows.Forms.TextBox();
            this.templateExplanation = new System.Windows.Forms.Label();
            this.aRingInput = new System.Windows.Forms.TextBox();
            this.naRingInput = new System.Windows.Forms.TextBox();
            this.bondInput = new System.Windows.Forms.TextBox();
            this.bondLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pressureLabel = new System.Windows.Forms.Label();
            this.resLabel = new System.Windows.Forms.Label();
            this.resInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // gasLabel
            // 
            this.gasLabel.AutoSize = true;
            this.gasLabel.Location = new System.Drawing.Point(6, 46);
            this.gasLabel.Name = "gasLabel";
            this.gasLabel.Size = new System.Drawing.Size(131, 13);
            this.gasLabel.TabIndex = 0;
            this.gasLabel.Text = "Molecule (Ex. H2, N2, He)";
            // 
            // moleculeInput
            // 
            this.moleculeInput.Location = new System.Drawing.Point(472, 43);
            this.moleculeInput.Name = "moleculeInput";
            this.moleculeInput.Size = new System.Drawing.Size(100, 20);
            this.moleculeInput.TabIndex = 1;
            this.moleculeInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.moleculeInput.TextChanged += new System.EventHandler(this.moleculeInput_TextChanged);
            // 
            // gasTemp
            // 
            this.gasTemp.AutoSize = true;
            this.gasTemp.Location = new System.Drawing.Point(6, 150);
            this.gasTemp.Name = "gasTemp";
            this.gasTemp.Size = new System.Drawing.Size(141, 13);
            this.gasTemp.TabIndex = 2;
            this.gasTemp.Text = "Temperature of system (in C)";
            // 
            // tempInput
            // 
            this.tempInput.Location = new System.Drawing.Point(472, 147);
            this.tempInput.Name = "tempInput";
            this.tempInput.Size = new System.Drawing.Size(100, 20);
            this.tempInput.TabIndex = 5;
            this.tempInput.Text = "25";
            this.tempInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tempInput.TextChanged += new System.EventHandler(this.tempInput_TextChanged);
            // 
            // templateExplanation
            // 
            this.templateExplanation.Location = new System.Drawing.Point(6, 0);
            this.templateExplanation.Name = "templateExplanation";
            this.templateExplanation.Size = new System.Drawing.Size(566, 40);
            this.templateExplanation.TabIndex = 6;
            this.templateExplanation.Text = resources.GetString("templateExplanation.Text");
            // 
            // aRingInput
            // 
            this.aRingInput.Location = new System.Drawing.Point(472, 95);
            this.aRingInput.Name = "aRingInput";
            this.aRingInput.Size = new System.Drawing.Size(100, 20);
            this.aRingInput.TabIndex = 3;
            this.aRingInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.aRingInput.TextChanged += new System.EventHandler(this.aRingInput_TextChanged);
            // 
            // naRingInput
            // 
            this.naRingInput.Location = new System.Drawing.Point(472, 121);
            this.naRingInput.Name = "naRingInput";
            this.naRingInput.Size = new System.Drawing.Size(100, 20);
            this.naRingInput.TabIndex = 4;
            this.naRingInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.naRingInput.TextChanged += new System.EventHandler(this.naRingInput_TextChanged);
            // 
            // bondInput
            // 
            this.bondInput.Location = new System.Drawing.Point(472, 69);
            this.bondInput.Name = "bondInput";
            this.bondInput.Size = new System.Drawing.Size(100, 20);
            this.bondInput.TabIndex = 2;
            this.bondInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bondInput.TextChanged += new System.EventHandler(this.bondInput_TextChanged);
            // 
            // bondLabel
            // 
            this.bondLabel.AutoSize = true;
            this.bondLabel.Location = new System.Drawing.Point(6, 72);
            this.bondLabel.Name = "bondLabel";
            this.bondLabel.Size = new System.Drawing.Size(324, 13);
            this.bondLabel.TabIndex = 10;
            this.bondLabel.Text = "Number of bonds in molecule (single, double, triple all count as one)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Number of non-aromatic rings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Number of aromatic rings";
            // 
            // pressureLabel
            // 
            this.pressureLabel.Location = new System.Drawing.Point(6, 196);
            this.pressureLabel.Name = "pressureLabel";
            this.pressureLabel.Size = new System.Drawing.Size(566, 20);
            this.pressureLabel.TabIndex = 13;
            // 
            // resLabel
            // 
            this.resLabel.AutoSize = true;
            this.resLabel.Location = new System.Drawing.Point(6, 176);
            this.resLabel.Name = "resLabel";
            this.resLabel.Size = new System.Drawing.Size(419, 13);
            this.resLabel.TabIndex = 14;
            this.resLabel.Text = "Resolution (grid size for concentration/pressure calculations - 5 = 5x5 squares o" +
    "ver grid)";
            // 
            // resInput
            // 
            this.resInput.Location = new System.Drawing.Point(472, 173);
            this.resInput.Name = "resInput";
            this.resInput.Size = new System.Drawing.Size(100, 20);
            this.resInput.TabIndex = 15;
            this.resInput.Text = "5";
            this.resInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.resInput.TextChanged += new System.EventHandler(this.resInput_TextChanged);
            // 
            // GasTemplateUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resInput);
            this.Controls.Add(this.resLabel);
            this.Controls.Add(this.pressureLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bondLabel);
            this.Controls.Add(this.bondInput);
            this.Controls.Add(this.naRingInput);
            this.Controls.Add(this.aRingInput);
            this.Controls.Add(this.templateExplanation);
            this.Controls.Add(this.tempInput);
            this.Controls.Add(this.gasTemp);
            this.Controls.Add(this.moleculeInput);
            this.Controls.Add(this.gasLabel);
            this.Name = "GasTemplateUC";
            this.Size = new System.Drawing.Size(575, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gasLabel;
        private System.Windows.Forms.TextBox moleculeInput;
        private System.Windows.Forms.Label gasTemp;
        private System.Windows.Forms.TextBox tempInput;
        private System.Windows.Forms.Label templateExplanation;
        private System.Windows.Forms.TextBox aRingInput;
        private System.Windows.Forms.TextBox naRingInput;
        private System.Windows.Forms.TextBox bondInput;
        private System.Windows.Forms.Label bondLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pressureLabel;
        private System.Windows.Forms.Label resLabel;
        private System.Windows.Forms.TextBox resInput;
    }
}
