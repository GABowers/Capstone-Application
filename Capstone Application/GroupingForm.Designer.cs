namespace Capstone_Application
{
    partial class GroupingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.runGetAllGroups = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Return the grouping (connectivity + clustering) of all cell states as a csv file," +
    " running as many times as indicated in the input field.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(318, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // runGetAllGroups
            // 
            this.runGetAllGroups.Location = new System.Drawing.Point(318, 35);
            this.runGetAllGroups.Name = "runGetAllGroups";
            this.runGetAllGroups.Size = new System.Drawing.Size(100, 20);
            this.runGetAllGroups.TabIndex = 2;
            this.runGetAllGroups.Text = "Run";
            this.runGetAllGroups.UseVisualStyleBackColor = true;
            this.runGetAllGroups.Click += new System.EventHandler(this.runGetAllGroups_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(399, 35);
            this.progressBar1.TabIndex = 3;
            // 
            // GroupingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 112);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.runGetAllGroups);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "GroupingForm";
            this.Text = "GroupingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button runGetAllGroups;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}