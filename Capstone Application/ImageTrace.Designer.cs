namespace Capstone_Application
{
    partial class ImageTrace
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
            this.tracePictureBox = new Capstone_Application.PictureBoxWithInterpolationMode();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pathTraceRadio = new System.Windows.Forms.RadioButton();
            this.freqTraceRadio = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tracePictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tracePictureBox
            // 
            this.tracePictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tracePictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.tracePictureBox.Location = new System.Drawing.Point(0, 30);
            this.tracePictureBox.Name = "tracePictureBox";
            this.tracePictureBox.Size = new System.Drawing.Size(500, 500);
            this.tracePictureBox.TabIndex = 0;
            this.tracePictureBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.pathTraceRadio);
            this.groupBox1.Controls.Add(this.freqTraceRadio);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 30);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(501, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pathTraceRadio
            // 
            this.pathTraceRadio.AutoSize = true;
            this.pathTraceRadio.Location = new System.Drawing.Point(119, 8);
            this.pathTraceRadio.Name = "pathTraceRadio";
            this.pathTraceRadio.Size = new System.Drawing.Size(78, 17);
            this.pathTraceRadio.TabIndex = 1;
            this.pathTraceRadio.TabStop = true;
            this.pathTraceRadio.Text = "Path Trace";
            this.pathTraceRadio.UseVisualStyleBackColor = true;
            this.pathTraceRadio.CheckedChanged += new System.EventHandler(this.pathTraceRadio_CheckedChanged);
            // 
            // freqTraceRadio
            // 
            this.freqTraceRadio.AutoSize = true;
            this.freqTraceRadio.Location = new System.Drawing.Point(7, 8);
            this.freqTraceRadio.Name = "freqTraceRadio";
            this.freqTraceRadio.Size = new System.Drawing.Size(106, 17);
            this.freqTraceRadio.TabIndex = 0;
            this.freqTraceRadio.TabStop = true;
            this.freqTraceRadio.Text = "Frequency Trace";
            this.freqTraceRadio.UseVisualStyleBackColor = true;
            this.freqTraceRadio.CheckedChanged += new System.EventHandler(this.freqTraceRadio_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(501, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(69, 500);
            this.panel1.TabIndex = 2;
            // 
            // ImageTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 530);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tracePictureBox);
            this.Name = "ImageTrace";
            this.Text = "ImageTrace";
            ((System.ComponentModel.ISupportInitialize)(this.tracePictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBoxWithInterpolationMode tracePictureBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton pathTraceRadio;
        private System.Windows.Forms.RadioButton freqTraceRadio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}