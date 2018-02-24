namespace Capstone_Application
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
            this.saveCellCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTransitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dansNeighborAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.runCountBox = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.iterationCountBox = new System.Windows.Forms.ToolStripLabel();
            this.innerPictureBox = new Capstone_Application.PictureBoxWithInterpolationMode();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveImageTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripButton();
            this.editGridButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.runCountMaxRuns = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.iterationCountCountSave = new System.Windows.Forms.ToolStripTextBox();
            this.setImageSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iterationCountImageSave = new System.Windows.Forms.ToolStripTextBox();
            this.setAutoResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iterationCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetIterationTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.cellCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.analysisToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(505, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTemplateToolStripMenuItem,
            this.openTemplateToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // saveTemplateToolStripMenuItem
            // 
            this.saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem";
            this.saveTemplateToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveTemplateToolStripMenuItem.Text = "Save Template";
            this.saveTemplateToolStripMenuItem.Click += new System.EventHandler(this.saveTemplateToolStripMenuItem_Click);
            // 
            // openTemplateToolStripMenuItem
            // 
            this.openTemplateToolStripMenuItem.Name = "openTemplateToolStripMenuItem";
            this.openTemplateToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openTemplateToolStripMenuItem.Text = "Open Template";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.ToolTipText = "Exit the program.";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newModelToolStripMenuItem,
            this.editModelToolStripMenuItem,
            this.saveCellCountToolStripMenuItem,
            this.saveTransitionsToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.saveMovementToolStripMenuItem,
            this.saveImageTraceToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem2.Text = "CA";
            // 
            // newModelToolStripMenuItem
            // 
            this.newModelToolStripMenuItem.AutoToolTip = true;
            this.newModelToolStripMenuItem.Name = "newModelToolStripMenuItem";
            this.newModelToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.newModelToolStripMenuItem.Text = "New Model";
            this.newModelToolStripMenuItem.ToolTipText = "Define CA type, grid boundaries, agent types and properties.";
            this.newModelToolStripMenuItem.Click += new System.EventHandler(this.newModelToolStripMenuItem_Click);
            // 
            // editModelToolStripMenuItem
            // 
            this.editModelToolStripMenuItem.Name = "editModelToolStripMenuItem";
            this.editModelToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.editModelToolStripMenuItem.Text = "Edit Model";
            this.editModelToolStripMenuItem.Click += new System.EventHandler(this.editModelToolStripMenuItem_Click);
            // 
            // saveCellCountToolStripMenuItem
            // 
            this.saveCellCountToolStripMenuItem.Name = "saveCellCountToolStripMenuItem";
            this.saveCellCountToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveCellCountToolStripMenuItem.Text = "Save Cell Count";
            this.saveCellCountToolStripMenuItem.Click += new System.EventHandler(this.saveCellCountToolStripMenuItem_Click);
            // 
            // saveTransitionsToolStripMenuItem
            // 
            this.saveTransitionsToolStripMenuItem.AutoToolTip = true;
            this.saveTransitionsToolStripMenuItem.CheckOnClick = true;
            this.saveTransitionsToolStripMenuItem.Name = "saveTransitionsToolStripMenuItem";
            this.saveTransitionsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveTransitionsToolStripMenuItem.Text = "Save Transitions";
            this.saveTransitionsToolStripMenuItem.ToolTipText = "Images will be saved in a \"Y-M-D H-M-S Run # Iteration #\" format.";
            this.saveTransitionsToolStripMenuItem.Click += new System.EventHandler(this.saveAllImagesToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // saveMovementToolStripMenuItem
            // 
            this.saveMovementToolStripMenuItem.Name = "saveMovementToolStripMenuItem";
            this.saveMovementToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveMovementToolStripMenuItem.Text = "Save Movement";
            this.saveMovementToolStripMenuItem.Click += new System.EventHandler(this.saveMovementToolStripMenuItem_Click);
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dansNeighborAnalysisToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.analysisToolStripMenuItem.Text = "Analysis";
            // 
            // dansNeighborAnalysisToolStripMenuItem
            // 
            this.dansNeighborAnalysisToolStripMenuItem.Name = "dansNeighborAnalysisToolStripMenuItem";
            this.dansNeighborAnalysisToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.dansNeighborAnalysisToolStripMenuItem.Text = "Dan\'s Neighbor Analysis (DNA)";
            this.dansNeighborAnalysisToolStripMenuItem.Click += new System.EventHandler(this.dansNeighborAnalysisToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainWindowToolStripMenuItem,
            this.modelToolStripMenuItem,
            this.cellCounterToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(63, 20);
            this.toolStripMenuItem3.Text = "Window";
            // 
            // mainWindowToolStripMenuItem
            // 
            this.mainWindowToolStripMenuItem.Name = "mainWindowToolStripMenuItem";
            this.mainWindowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.mainWindowToolStripMenuItem.Text = "Main Window";
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // cellCounterToolStripMenuItem
            // 
            this.cellCounterToolStripMenuItem.Name = "cellCounterToolStripMenuItem";
            this.cellCounterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.cellCounterToolStripMenuItem.Text = "Run Data";
            this.cellCounterToolStripMenuItem.Click += new System.EventHandler(this.cellCounterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem4.Text = "Help";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.userGuideToolStripMenuItem.Text = "User Guide";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolStripSeparator5,
            this.editGridButton,
            this.toolStripSeparator3,
            this.toolStripDropDownButton2,
            this.runCountBox,
            this.toolStripSeparator4,
            this.toolStripDropDownButton1,
            this.iterationCountBox,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(505, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // runCountBox
            // 
            this.runCountBox.Name = "runCountBox";
            this.runCountBox.Size = new System.Drawing.Size(59, 22);
            this.runCountBox.Text = "[Number]";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // iterationCountBox
            // 
            this.iterationCountBox.Name = "iterationCountBox";
            this.iterationCountBox.Size = new System.Drawing.Size(59, 22);
            this.iterationCountBox.Text = "[Number]";
            // 
            // innerPictureBox
            // 
            this.innerPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.innerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerPictureBox.ErrorImage = null;
            this.innerPictureBox.InitialImage = null;
            this.innerPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.innerPictureBox.Location = new System.Drawing.Point(0, 0);
            this.innerPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.innerPictureBox.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.innerPictureBox.Name = "innerPictureBox";
            this.innerPictureBox.Size = new System.Drawing.Size(505, 505);
            this.innerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.innerPictureBox.TabIndex = 3;
            this.innerPictureBox.TabStop = false;
            this.innerPictureBox.Click += new System.EventHandler(this.innerPictureBox_Click);
            this.innerPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.innerPictureBox_MouseDown);
            this.innerPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.innerPictureBox_MouseUp);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.innerPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 505);
            this.panel1.TabIndex = 4;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // saveImageTraceToolStripMenuItem
            // 
            this.saveImageTraceToolStripMenuItem.Name = "saveImageTraceToolStripMenuItem";
            this.saveImageTraceToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveImageTraceToolStripMenuItem.Text = "Save Visualization";
            this.saveImageTraceToolStripMenuItem.Click += new System.EventHandler(this.saveImageTraceToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel1.Text = "Start CA";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.AutoToolTip = false;
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel2.Image")));
            this.toolStripLabel2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel2.Text = "Pause";
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel3.Image")));
            this.toolStripLabel3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel3.Text = "Reset CA";
            this.toolStripLabel3.Click += new System.EventHandler(this.toolStripLabel3_Click);
            // 
            // editGridButton
            // 
            this.editGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editGridButton.Image = ((System.Drawing.Image)(resources.GetObject("editGridButton.Image")));
            this.editGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editGridButton.Name = "editGridButton";
            this.editGridButton.Size = new System.Drawing.Size(56, 22);
            this.editGridButton.Text = "Edit Grid";
            this.editGridButton.ToolTipText = "Edit Grid";
            this.editGridButton.Click += new System.EventHandler(this.editGridButton_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runCountMaxRuns});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(41, 22);
            this.toolStripDropDownButton2.Text = "Run";
            // 
            // runCountMaxRuns
            // 
            this.runCountMaxRuns.CheckOnClick = true;
            this.runCountMaxRuns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.runCountMaxRuns.Name = "runCountMaxRuns";
            this.runCountMaxRuns.Size = new System.Drawing.Size(173, 22);
            this.runCountMaxRuns.Text = "Set Max Auto Runs";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.setImageSaveToolStripMenuItem,
            this.setAutoResetToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(64, 22);
            this.toolStripDropDownButton1.Text = "Iteration";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.AutoToolTip = true;
            this.toolStripMenuItem5.CheckOnClick = true;
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iterationCountCountSave});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItem5.Text = "Set Count Save";
            this.toolStripMenuItem5.ToolTipText = resources.GetString("toolStripMenuItem5.ToolTipText");
            this.toolStripMenuItem5.CheckedChanged += new System.EventHandler(this.toolStripMenuItem5_CheckedChanged);
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // iterationCountCountSave
            // 
            this.iterationCountCountSave.Name = "iterationCountCountSave";
            this.iterationCountCountSave.Size = new System.Drawing.Size(100, 23);
            this.iterationCountCountSave.Leave += new System.EventHandler(this.iterationCountCountSave_Leave);
            this.iterationCountCountSave.TextChanged += new System.EventHandler(this.iterationCountCountSave_TextChanged);
            // 
            // setImageSaveToolStripMenuItem
            // 
            this.setImageSaveToolStripMenuItem.CheckOnClick = true;
            this.setImageSaveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iterationCountImageSave});
            this.setImageSaveToolStripMenuItem.Name = "setImageSaveToolStripMenuItem";
            this.setImageSaveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.setImageSaveToolStripMenuItem.Text = "Set Image Save";
            this.setImageSaveToolStripMenuItem.CheckedChanged += new System.EventHandler(this.setImageSaveToolStripMenuItem_CheckedChanged);
            this.setImageSaveToolStripMenuItem.Click += new System.EventHandler(this.setImageSaveToolStripMenuItem_Click);
            // 
            // iterationCountImageSave
            // 
            this.iterationCountImageSave.Name = "iterationCountImageSave";
            this.iterationCountImageSave.Size = new System.Drawing.Size(100, 23);
            this.iterationCountImageSave.Leave += new System.EventHandler(this.iterationCountImageSave_Leave);
            this.iterationCountImageSave.TextChanged += new System.EventHandler(this.iterationCountImageSave_TextChanged);
            // 
            // setAutoResetToolStripMenuItem
            // 
            this.setAutoResetToolStripMenuItem.CheckOnClick = true;
            this.setAutoResetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iterationCountToolStripMenuItem,
            this.cellCountToolStripMenuItem});
            this.setAutoResetToolStripMenuItem.Name = "setAutoResetToolStripMenuItem";
            this.setAutoResetToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.setAutoResetToolStripMenuItem.Text = "Set Auto Reset";
            this.setAutoResetToolStripMenuItem.CheckedChanged += new System.EventHandler(this.setAutoResetToolStripMenuItem_CheckedChanged);
            this.setAutoResetToolStripMenuItem.Click += new System.EventHandler(this.setAutoResetToolStripMenuItem_Click);
            // 
            // iterationCountToolStripMenuItem
            // 
            this.iterationCountToolStripMenuItem.CheckOnClick = true;
            this.iterationCountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetIterationTextBox});
            this.iterationCountToolStripMenuItem.Name = "iterationCountToolStripMenuItem";
            this.iterationCountToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.iterationCountToolStripMenuItem.Text = "Iteration Count";
            this.iterationCountToolStripMenuItem.CheckedChanged += new System.EventHandler(this.iterationCountToolStripMenuItem_CheckedChanged);
            // 
            // resetIterationTextBox
            // 
            this.resetIterationTextBox.Name = "resetIterationTextBox";
            this.resetIterationTextBox.Size = new System.Drawing.Size(100, 23);
            this.resetIterationTextBox.Leave += new System.EventHandler(this.resetIterationTextBox_Leave);
            this.resetIterationTextBox.TextChanged += new System.EventHandler(this.resetIterationTextBox_TextChanged);
            // 
            // cellCountToolStripMenuItem
            // 
            this.cellCountToolStripMenuItem.CheckOnClick = true;
            this.cellCountToolStripMenuItem.Name = "cellCountToolStripMenuItem";
            this.cellCountToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.cellCountToolStripMenuItem.Text = "Cell Count";
            this.cellCountToolStripMenuItem.CheckedChanged += new System.EventHandler(this.cellCountToolStripMenuItem_CheckedChanged);
            this.cellCountToolStripMenuItem.Click += new System.EventHandler(this.cellCountToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(25, 22);
            this.toolStripButton1.Text = "+1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 554);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Capstone";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem saveCellCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTransitionsToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        public System.Windows.Forms.ToolStripMenuItem runCountMaxRuns;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel runCountBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem setImageSaveToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem setAutoResetToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel iterationCountBox;
        private System.Windows.Forms.ToolStripButton toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripLabel3;
        public PictureBoxWithInterpolationMode innerPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton editGridButton;
        public System.Windows.Forms.ToolStripMenuItem iterationCountToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cellCountToolStripMenuItem;
        public System.Windows.Forms.ToolStripTextBox resetIterationTextBox;
        public System.Windows.Forms.ToolStripTextBox iterationCountCountSave;
        public System.Windows.Forms.ToolStripTextBox iterationCountImageSave;
        private System.Windows.Forms.ToolStripMenuItem cellCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMovementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageTraceToolStripMenuItem;
    }
}

