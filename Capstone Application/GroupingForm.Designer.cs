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
            this.cancelGroupings = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.newBW = new System.ComponentModel.BackgroundWorker();
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
            this.textBox1.Location = new System.Drawing.Point(322, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // runGetAllGroups
            // 
            this.runGetAllGroups.Location = new System.Drawing.Point(322, 34);
            this.runGetAllGroups.Margin = new System.Windows.Forms.Padding(0);
            this.runGetAllGroups.Name = "runGetAllGroups";
            this.runGetAllGroups.Size = new System.Drawing.Size(100, 20);
            this.runGetAllGroups.TabIndex = 2;
            this.runGetAllGroups.Text = "Run";
            this.runGetAllGroups.UseVisualStyleBackColor = true;
            this.runGetAllGroups.Click += new System.EventHandler(this.runGetAllGroups_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 82);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 20);
            this.progressBar1.TabIndex = 3;
            // 
            // cancelGroupings
            // 
            this.cancelGroupings.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelGroupings.Location = new System.Drawing.Point(322, 57);
            this.cancelGroupings.Margin = new System.Windows.Forms.Padding(0);
            this.cancelGroupings.Name = "cancelGroupings";
            this.cancelGroupings.Size = new System.Drawing.Size(100, 20);
            this.cancelGroupings.TabIndex = 4;
            this.cancelGroupings.Text = "Cancel";
            this.cancelGroupings.UseVisualStyleBackColor = true;
            // 
            // infoLabel
            // 
            this.infoLabel.Location = new System.Drawing.Point(12, 57);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(300, 20);
            this.infoLabel.TabIndex = 5;
            this.infoLabel.Text = "%";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // newBW
            // 
            this.newBW.WorkerReportsProgress = true;
            this.newBW.WorkerSupportsCancellation = true;
            this.newBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.newBW_DoWork_1);
            this.newBW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.newBW_ProgressChanged);
            this.newBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.newBW_RunWorkerCompleted);
            // 
            // GroupingForm
            // 
            this.AcceptButton = this.runGetAllGroups;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelGroupings;
            this.ClientSize = new System.Drawing.Size(434, 112);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.cancelGroupings);
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
        private System.Windows.Forms.Button cancelGroupings;
        private System.Windows.Forms.Label infoLabel;
        private System.ComponentModel.BackgroundWorker newBW;
    }
}