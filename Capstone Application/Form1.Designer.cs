﻿namespace Capstone_Application
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.newModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finalLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dansNeighborAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomWalkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runSettingsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.StartButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PauseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetRunButton = new System.Windows.Forms.ToolStripButton();
            this.resetRunsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.editGridButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.runLabel = new System.Windows.Forms.ToolStripLabel();
            this.runCountBox = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.iterationLabel = new System.Windows.Forms.ToolStripLabel();
            this.iterationCountBox = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.speedLabel = new System.Windows.Forms.ToolStripLabel();
            this.speedInput = new System.Windows.Forms.ToolStripTextBox();
            this.decrease = new System.Windows.Forms.ToolStripButton();
            this.increase = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.saveToolStripMenuItem,
            this.analysisToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.runSettingsButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTemplateToolStripMenuItem,
            this.openTemplateToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItem1.Text = "File";
            // 
            // saveTemplateToolStripMenuItem
            // 
            this.saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem";
            this.saveTemplateToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            this.saveTemplateToolStripMenuItem.Text = "Save Template";
            this.saveTemplateToolStripMenuItem.Click += new System.EventHandler(this.saveTemplateToolStripMenuItem_Click);
            // 
            // openTemplateToolStripMenuItem
            // 
            this.openTemplateToolStripMenuItem.Name = "openTemplateToolStripMenuItem";
            this.openTemplateToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            this.openTemplateToolStripMenuItem.Text = "Open Template";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.ToolTipText = "Exit the program.";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newModelToolStripMenuItem,
            this.editModelToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(42, 24);
            this.toolStripMenuItem2.Text = "CA";
            // 
            // newModelToolStripMenuItem
            // 
            this.newModelToolStripMenuItem.AutoToolTip = true;
            this.newModelToolStripMenuItem.Name = "newModelToolStripMenuItem";
            this.newModelToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.newModelToolStripMenuItem.Text = "New Model";
            this.newModelToolStripMenuItem.ToolTipText = "Define CA type, grid boundaries, agent types and properties.";
            this.newModelToolStripMenuItem.Click += new System.EventHandler(this.newModelToolStripMenuItem_Click);
            // 
            // editModelToolStripMenuItem
            // 
            this.editModelToolStripMenuItem.Name = "editModelToolStripMenuItem";
            this.editModelToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.editModelToolStripMenuItem.Text = "Edit Model";
            this.editModelToolStripMenuItem.Click += new System.EventHandler(this.editModelToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.pathToolStripMenuItem,
            this.visualizationToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.finalLocationsToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // pathToolStripMenuItem
            // 
            this.pathToolStripMenuItem.Name = "pathToolStripMenuItem";
            this.pathToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.pathToolStripMenuItem.Text = "Path";
            this.pathToolStripMenuItem.Click += new System.EventHandler(this.pathToolStripMenuItem_Click);
            // 
            // visualizationToolStripMenuItem
            // 
            this.visualizationToolStripMenuItem.Name = "visualizationToolStripMenuItem";
            this.visualizationToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.visualizationToolStripMenuItem.Text = "Visualization";
            this.visualizationToolStripMenuItem.Click += new System.EventHandler(this.visualizationToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.dataToolStripMenuItem.Text = "Data";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // finalLocationsToolStripMenuItem
            // 
            this.finalLocationsToolStripMenuItem.Name = "finalLocationsToolStripMenuItem";
            this.finalLocationsToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.finalLocationsToolStripMenuItem.Text = "Final Locations";
            this.finalLocationsToolStripMenuItem.Click += new System.EventHandler(this.finalLocationsToolStripMenuItem_Click);
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dansNeighborAnalysisToolStripMenuItem,
            this.randomWalkToolStripMenuItem,
            this.traceToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.analysisToolStripMenuItem.Text = "Analysis";
            // 
            // dansNeighborAnalysisToolStripMenuItem
            // 
            this.dansNeighborAnalysisToolStripMenuItem.Name = "dansNeighborAnalysisToolStripMenuItem";
            this.dansNeighborAnalysisToolStripMenuItem.Size = new System.Drawing.Size(298, 26);
            this.dansNeighborAnalysisToolStripMenuItem.Text = "Dan\'s Neighbor Analysis (DNA)";
            this.dansNeighborAnalysisToolStripMenuItem.Click += new System.EventHandler(this.dansNeighborAnalysisToolStripMenuItem_Click);
            // 
            // randomWalkToolStripMenuItem
            // 
            this.randomWalkToolStripMenuItem.Name = "randomWalkToolStripMenuItem";
            this.randomWalkToolStripMenuItem.Size = new System.Drawing.Size(298, 26);
            this.randomWalkToolStripMenuItem.Text = "Random Walk";
            // 
            // traceToolStripMenuItem
            // 
            this.traceToolStripMenuItem.Name = "traceToolStripMenuItem";
            this.traceToolStripMenuItem.Size = new System.Drawing.Size(298, 26);
            this.traceToolStripMenuItem.Text = "Trace";
            this.traceToolStripMenuItem.Click += new System.EventHandler(this.traceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainWindowToolStripMenuItem,
            this.modelToolStripMenuItem,
            this.cellCounterToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(78, 24);
            this.toolStripMenuItem3.Text = "Window";
            // 
            // mainWindowToolStripMenuItem
            // 
            this.mainWindowToolStripMenuItem.Name = "mainWindowToolStripMenuItem";
            this.mainWindowToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.mainWindowToolStripMenuItem.Text = "Main Window";
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // cellCounterToolStripMenuItem
            // 
            this.cellCounterToolStripMenuItem.Name = "cellCounterToolStripMenuItem";
            this.cellCounterToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.cellCounterToolStripMenuItem.Text = "Run Data";
            this.cellCounterToolStripMenuItem.Click += new System.EventHandler(this.cellCounterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(55, 24);
            this.toolStripMenuItem4.Text = "Help";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.userGuideToolStripMenuItem.Text = "User Guide";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // runSettingsButton
            // 
            this.runSettingsButton.Name = "runSettingsButton";
            this.runSettingsButton.Size = new System.Drawing.Size(105, 24);
            this.runSettingsButton.Text = "Run Settings";
            this.runSettingsButton.Click += new System.EventHandler(this.runSettingsButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartButton,
            this.toolStripSeparator1,
            this.PauseButton,
            this.toolStripSeparator2,
            this.resetRunButton,
            this.resetRunsButton,
            this.toolStripSeparator5,
            this.editGridButton,
            this.toolStripSeparator3,
            this.runLabel,
            this.runCountBox,
            this.toolStripSeparator4,
            this.iterationLabel,
            this.iterationCountBox,
            this.toolStripButton1,
            this.toolStripSeparator6,
            this.speedLabel,
            this.speedInput,
            this.decrease,
            this.increase});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // StartButton
            // 
            this.StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StartButton.Image = ((System.Drawing.Image)(resources.GetObject("StartButton.Image")));
            this.StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(44, 24);
            this.StartButton.Text = "Start";
            this.StartButton.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // PauseButton
            // 
            this.PauseButton.AutoToolTip = false;
            this.PauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PauseButton.Image = ((System.Drawing.Image)(resources.GetObject("PauseButton.Image")));
            this.PauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(70, 24);
            this.PauseButton.Text = "Unpause";
            this.PauseButton.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // resetRunButton
            // 
            this.resetRunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetRunButton.Image = ((System.Drawing.Image)(resources.GetObject("resetRunButton.Image")));
            this.resetRunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetRunButton.Name = "resetRunButton";
            this.resetRunButton.Size = new System.Drawing.Size(78, 24);
            this.resetRunButton.Text = "Run Reset";
            this.resetRunButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // resetRunsButton
            // 
            this.resetRunsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetRunsButton.Image = ((System.Drawing.Image)(resources.GetObject("resetRunsButton.Image")));
            this.resetRunsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetRunsButton.Name = "resetRunsButton";
            this.resetRunsButton.Size = new System.Drawing.Size(76, 24);
            this.resetRunsButton.Text = "Full Reset";
            this.resetRunsButton.Click += new System.EventHandler(this.resetRunsButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // editGridButton
            // 
            this.editGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editGridButton.Name = "editGridButton";
            this.editGridButton.Size = new System.Drawing.Size(71, 24);
            this.editGridButton.Text = "Edit Grid";
            this.editGridButton.ToolTipText = "Edit Grid";
            this.editGridButton.Click += new System.EventHandler(this.editGridButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // runLabel
            // 
            this.runLabel.Name = "runLabel";
            this.runLabel.Size = new System.Drawing.Size(34, 24);
            this.runLabel.Text = "Run";
            // 
            // runCountBox
            // 
            this.runCountBox.Name = "runCountBox";
            this.runCountBox.Size = new System.Drawing.Size(37, 24);
            this.runCountBox.Text = "[##]";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // iterationLabel
            // 
            this.iterationLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.iterationLabel.Image = ((System.Drawing.Image)(resources.GetObject("iterationLabel.Image")));
            this.iterationLabel.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(65, 24);
            this.iterationLabel.Text = "Iteration";
            this.iterationLabel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // iterationCountBox
            // 
            this.iterationCountBox.Name = "iterationCountBox";
            this.iterationCountBox.Size = new System.Drawing.Size(0, 24);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(31, 24);
            this.toolStripButton1.Text = "+1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // speedLabel
            // 
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(51, 24);
            this.speedLabel.Text = "Speed";
            // 
            // speedInput
            // 
            this.speedInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.speedInput.MaxLength = 3;
            this.speedInput.Name = "speedInput";
            this.speedInput.Size = new System.Drawing.Size(25, 27);
            this.speedInput.Text = "0";
            this.speedInput.TextChanged += new System.EventHandler(this.speedInput_TextChanged);
            // 
            // decrease
            // 
            this.decrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.decrease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.decrease.Name = "decrease";
            this.decrease.Size = new System.Drawing.Size(29, 24);
            this.decrease.Text = "-";
            this.decrease.Click += new System.EventHandler(this.decrease_Click);
            // 
            // increase
            // 
            this.increase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.increase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.increase.Name = "increase";
            this.increase.Size = new System.Drawing.Size(29, 24);
            this.increase.Text = "+";
            this.increase.Click += new System.EventHandler(this.increase_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 744);
            this.panel1.TabIndex = 4;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 799);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capstone";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem newModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dansNeighborAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel runCountBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel iterationCountBox;
        private System.Windows.Forms.ToolStripButton StartButton;
        private System.Windows.Forms.ToolStripButton PauseButton;
        private System.Windows.Forms.ToolStripButton resetRunButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cellCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripTextBox speedInput;
        private System.Windows.Forms.ToolStripButton decrease;
        private System.Windows.Forms.ToolStripButton increase;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomWalkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runSettingsButton;
        private System.Windows.Forms.ToolStripLabel iterationLabel;
        private System.Windows.Forms.ToolStripMenuItem traceToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel runLabel;
        private System.Windows.Forms.ToolStripLabel speedLabel;
        public System.Windows.Forms.ToolStripButton editGridButton;
        private System.Windows.Forms.ToolStripMenuItem finalLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton resetRunsButton;
        //private System.Windows.Forms.PictureBox pictureBox;
    }
}

