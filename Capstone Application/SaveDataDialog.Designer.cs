namespace Capstone_Application
{
    partial class SaveDataDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveDataDialog));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Count");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Transitions");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("C Index");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Data", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Image");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Paths");
            this.outerPanel = new System.Windows.Forms.Panel();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.messageNextButton = new System.Windows.Forms.Button();
            this.messagePrevButton = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.saveOptions = new System.Windows.Forms.TreeView();
            this.saveTabsControl = new System.Windows.Forms.TabControl();
            this.dataTab = new System.Windows.Forms.TabPage();
            this.dataIncInput = new System.Windows.Forms.TextBox();
            this.dataIncLabel = new System.Windows.Forms.Label();
            this.dataIntroLabel = new System.Windows.Forms.Label();
            this.saveDataFolderInput = new System.Windows.Forms.TextBox();
            this.saveFolderLabel = new System.Windows.Forms.Label();
            this.imageTab = new System.Windows.Forms.TabPage();
            this.imageIncInput = new System.Windows.Forms.TextBox();
            this.imageIncLabel = new System.Windows.Forms.Label();
            this.imageIntroLabel = new System.Windows.Forms.Label();
            this.saveImageFolderInput = new System.Windows.Forms.TextBox();
            this.saveImageLabel = new System.Windows.Forms.Label();
            this.pathTab = new System.Windows.Forms.TabPage();
            this.pathsIncInput = new System.Windows.Forms.TextBox();
            this.pathsIncLabel = new System.Windows.Forms.Label();
            this.pathsIntroLabel = new System.Windows.Forms.Label();
            this.savePathsFolderInput = new System.Windows.Forms.TextBox();
            this.savePathsLabel = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.resetTabControl = new System.Windows.Forms.TabControl();
            this.resetTab = new System.Windows.Forms.TabPage();
            this.resetAgentPanel = new System.Windows.Forms.Panel();
            this.resetAgentLabel = new System.Windows.Forms.Label();
            this.resetIterationLabel = new System.Windows.Forms.Label();
            this.resetIterationInput = new System.Windows.Forms.TextBox();
            this.resetIntroLabel = new System.Windows.Forms.Label();
            this.pauseTabControl = new System.Windows.Forms.TabControl();
            this.pauseTab = new System.Windows.Forms.TabPage();
            this.pauseAgentPanel = new System.Windows.Forms.Panel();
            this.pauseAgentLabel = new System.Windows.Forms.Label();
            this.pauseIterationLabel = new System.Windows.Forms.Label();
            this.pauseIterationInput = new System.Windows.Forms.TextBox();
            this.pauseIntroLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.templateTab = new System.Windows.Forms.TabPage();
            this.tempTabExplain = new System.Windows.Forms.Label();
            this.outerPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.saveTabsControl.SuspendLayout();
            this.dataTab.SuspendLayout();
            this.imageTab.SuspendLayout();
            this.pathTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.resetTabControl.SuspendLayout();
            this.resetTab.SuspendLayout();
            this.pauseTabControl.SuspendLayout();
            this.pauseTab.SuspendLayout();
            this.templateTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // outerPanel
            // 
            this.outerPanel.AutoSize = true;
            this.outerPanel.Controls.Add(this.controlsPanel);
            this.outerPanel.Controls.Add(this.splitContainer1);
            this.outerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerPanel.Location = new System.Drawing.Point(0, 0);
            this.outerPanel.Name = "outerPanel";
            this.outerPanel.Size = new System.Drawing.Size(450, 480);
            this.outerPanel.TabIndex = 0;
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.messageNextButton);
            this.controlsPanel.Controls.Add(this.messagePrevButton);
            this.controlsPanel.Controls.Add(this.messageBox);
            this.controlsPanel.Controls.Add(this.loadButton);
            this.controlsPanel.Controls.Add(this.saveButton);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(450, 30);
            this.controlsPanel.TabIndex = 1;
            // 
            // messageNextButton
            // 
            this.messageNextButton.Image = ((System.Drawing.Image)(resources.GetObject("messageNextButton.Image")));
            this.messageNextButton.Location = new System.Drawing.Point(422, 4);
            this.messageNextButton.Name = "messageNextButton";
            this.messageNextButton.Size = new System.Drawing.Size(25, 22);
            this.messageNextButton.TabIndex = 4;
            this.messageNextButton.UseVisualStyleBackColor = true;
            this.messageNextButton.Click += new System.EventHandler(this.messageNextButton_Click);
            // 
            // messagePrevButton
            // 
            this.messagePrevButton.Image = ((System.Drawing.Image)(resources.GetObject("messagePrevButton.Image")));
            this.messagePrevButton.Location = new System.Drawing.Point(395, 4);
            this.messagePrevButton.Name = "messagePrevButton";
            this.messagePrevButton.Size = new System.Drawing.Size(25, 22);
            this.messagePrevButton.TabIndex = 3;
            this.messagePrevButton.UseVisualStyleBackColor = true;
            this.messagePrevButton.Click += new System.EventHandler(this.messagePrevButton_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(119, 4);
            this.messageBox.Margin = new System.Windows.Forms.Padding(3);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(272, 22);
            this.messageBox.TabIndex = 2;
            this.messageBox.Text = "This space intentionally left blank.";
            this.messageBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(63, 4);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(50, 22);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(7, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(50, 22);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(450, 450);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.saveOptions);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.saveTabsControl);
            this.splitContainer2.Size = new System.Drawing.Size(450, 223);
            this.splitContainer2.SplitterDistance = 150;
            this.splitContainer2.TabIndex = 0;
            // 
            // saveOptions
            // 
            this.saveOptions.CheckBoxes = true;
            this.saveOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveOptions.Location = new System.Drawing.Point(0, 0);
            this.saveOptions.Name = "saveOptions";
            treeNode1.Name = "countChild";
            treeNode1.Text = "Count";
            treeNode2.Name = "transitionsChild";
            treeNode2.Text = "Transitions";
            treeNode3.Name = "cIndexChild";
            treeNode3.Text = "C Index";
            treeNode4.Name = "dataRoot";
            treeNode4.Text = "Data";
            treeNode5.Name = "imageRoot";
            treeNode5.Text = "Image";
            treeNode6.Name = "pathsRoot";
            treeNode6.Text = "Paths";
            this.saveOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.saveOptions.Size = new System.Drawing.Size(150, 223);
            this.saveOptions.TabIndex = 0;
            this.saveOptions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.saveOptions_AfterCheck);
            // 
            // saveTabsControl
            // 
            this.saveTabsControl.Controls.Add(this.dataTab);
            this.saveTabsControl.Controls.Add(this.imageTab);
            this.saveTabsControl.Controls.Add(this.pathTab);
            this.saveTabsControl.Controls.Add(this.templateTab);
            this.saveTabsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveTabsControl.Location = new System.Drawing.Point(0, 0);
            this.saveTabsControl.Name = "saveTabsControl";
            this.saveTabsControl.SelectedIndex = 0;
            this.saveTabsControl.Size = new System.Drawing.Size(296, 223);
            this.saveTabsControl.TabIndex = 0;
            // 
            // dataTab
            // 
            this.dataTab.Controls.Add(this.dataIncInput);
            this.dataTab.Controls.Add(this.dataIncLabel);
            this.dataTab.Controls.Add(this.dataIntroLabel);
            this.dataTab.Controls.Add(this.saveDataFolderInput);
            this.dataTab.Controls.Add(this.saveFolderLabel);
            this.dataTab.Location = new System.Drawing.Point(4, 22);
            this.dataTab.Name = "dataTab";
            this.dataTab.Padding = new System.Windows.Forms.Padding(3);
            this.dataTab.Size = new System.Drawing.Size(288, 197);
            this.dataTab.TabIndex = 0;
            this.dataTab.Text = "Save Data";
            this.dataTab.UseVisualStyleBackColor = true;
            // 
            // dataIncInput
            // 
            this.dataIncInput.Location = new System.Drawing.Point(6, 160);
            this.dataIncInput.Name = "dataIncInput";
            this.dataIncInput.Size = new System.Drawing.Size(121, 20);
            this.dataIncInput.TabIndex = 4;
            this.dataIncInput.TextChanged += new System.EventHandler(this.dataIncInput_TextChanged);
            this.dataIncInput.Leave += new System.EventHandler(this.dataIncInput_Leave);
            // 
            // dataIncLabel
            // 
            this.dataIncLabel.Location = new System.Drawing.Point(3, 90);
            this.dataIncLabel.Name = "dataIncLabel";
            this.dataIncLabel.Size = new System.Drawing.Size(282, 65);
            this.dataIncLabel.TabIndex = 3;
            this.dataIncLabel.Text = resources.GetString("dataIncLabel.Text");
            // 
            // dataIntroLabel
            // 
            this.dataIntroLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.dataIntroLabel.Name = "dataIntroLabel";
            this.dataIntroLabel.Size = new System.Drawing.Size(282, 39);
            this.dataIntroLabel.TabIndex = 2;
            this.dataIntroLabel.Text = "Save csv file with various data about each iteration up to the present, including" +
    " agent counts, transitions, and the C Index. Works best at the end of the run.";
            // 
            // saveDataFolderInput
            // 
            this.saveDataFolderInput.Location = new System.Drawing.Point(6, 64);
            this.saveDataFolderInput.Name = "saveDataFolderInput";
            this.saveDataFolderInput.Size = new System.Drawing.Size(242, 20);
            this.saveDataFolderInput.TabIndex = 1;
            this.saveDataFolderInput.TextChanged += new System.EventHandler(this.saveDataFolderInput_TextChanged);
            // 
            // saveFolderLabel
            // 
            this.saveFolderLabel.AutoSize = true;
            this.saveFolderLabel.Location = new System.Drawing.Point(3, 47);
            this.saveFolderLabel.Name = "saveFolderLabel";
            this.saveFolderLabel.Size = new System.Drawing.Size(267, 13);
            this.saveFolderLabel.TabIndex = 0;
            this.saveFolderLabel.Text = "Designate path, or click the button to the right to do so.";
            // 
            // imageTab
            // 
            this.imageTab.Controls.Add(this.imageIncInput);
            this.imageTab.Controls.Add(this.imageIncLabel);
            this.imageTab.Controls.Add(this.imageIntroLabel);
            this.imageTab.Controls.Add(this.saveImageFolderInput);
            this.imageTab.Controls.Add(this.saveImageLabel);
            this.imageTab.Location = new System.Drawing.Point(4, 22);
            this.imageTab.Name = "imageTab";
            this.imageTab.Padding = new System.Windows.Forms.Padding(3);
            this.imageTab.Size = new System.Drawing.Size(288, 197);
            this.imageTab.TabIndex = 1;
            this.imageTab.Text = "Save Image";
            this.imageTab.UseVisualStyleBackColor = true;
            // 
            // imageIncInput
            // 
            this.imageIncInput.Location = new System.Drawing.Point(6, 160);
            this.imageIncInput.Name = "imageIncInput";
            this.imageIncInput.Size = new System.Drawing.Size(121, 20);
            this.imageIncInput.TabIndex = 9;
            this.imageIncInput.TextChanged += new System.EventHandler(this.imageIncInput_TextChanged);
            // 
            // imageIncLabel
            // 
            this.imageIncLabel.Location = new System.Drawing.Point(3, 90);
            this.imageIncLabel.Name = "imageIncLabel";
            this.imageIncLabel.Size = new System.Drawing.Size(282, 65);
            this.imageIncLabel.TabIndex = 8;
            this.imageIncLabel.Text = resources.GetString("imageIncLabel.Text");
            // 
            // imageIntroLabel
            // 
            this.imageIntroLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.imageIntroLabel.Name = "imageIntroLabel";
            this.imageIntroLabel.Size = new System.Drawing.Size(282, 39);
            this.imageIntroLabel.TabIndex = 7;
            this.imageIntroLabel.Text = "Save bitmap with snapshot of the grid at that moment in time.";
            // 
            // saveImageFolderInput
            // 
            this.saveImageFolderInput.Location = new System.Drawing.Point(6, 64);
            this.saveImageFolderInput.Name = "saveImageFolderInput";
            this.saveImageFolderInput.Size = new System.Drawing.Size(242, 20);
            this.saveImageFolderInput.TabIndex = 6;
            this.saveImageFolderInput.TextChanged += new System.EventHandler(this.saveImageFolderInput_TextChanged);
            // 
            // saveImageLabel
            // 
            this.saveImageLabel.AutoSize = true;
            this.saveImageLabel.Location = new System.Drawing.Point(3, 47);
            this.saveImageLabel.Name = "saveImageLabel";
            this.saveImageLabel.Size = new System.Drawing.Size(267, 13);
            this.saveImageLabel.TabIndex = 5;
            this.saveImageLabel.Text = "Designate path, or click the button to the right to do so.";
            // 
            // pathTab
            // 
            this.pathTab.Controls.Add(this.pathsIncInput);
            this.pathTab.Controls.Add(this.pathsIncLabel);
            this.pathTab.Controls.Add(this.pathsIntroLabel);
            this.pathTab.Controls.Add(this.savePathsFolderInput);
            this.pathTab.Controls.Add(this.savePathsLabel);
            this.pathTab.Location = new System.Drawing.Point(4, 22);
            this.pathTab.Name = "pathTab";
            this.pathTab.Padding = new System.Windows.Forms.Padding(3);
            this.pathTab.Size = new System.Drawing.Size(288, 197);
            this.pathTab.TabIndex = 2;
            this.pathTab.Text = "Save Paths";
            this.pathTab.UseVisualStyleBackColor = true;
            // 
            // pathsIncInput
            // 
            this.pathsIncInput.Location = new System.Drawing.Point(6, 160);
            this.pathsIncInput.Name = "pathsIncInput";
            this.pathsIncInput.Size = new System.Drawing.Size(121, 20);
            this.pathsIncInput.TabIndex = 14;
            this.pathsIncInput.TextChanged += new System.EventHandler(this.pathsIncInput_TextChanged);
            // 
            // pathsIncLabel
            // 
            this.pathsIncLabel.Location = new System.Drawing.Point(3, 90);
            this.pathsIncLabel.Name = "pathsIncLabel";
            this.pathsIncLabel.Size = new System.Drawing.Size(282, 65);
            this.pathsIncLabel.TabIndex = 13;
            this.pathsIncLabel.Text = resources.GetString("pathsIncLabel.Text");
            // 
            // pathsIntroLabel
            // 
            this.pathsIntroLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pathsIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.pathsIntroLabel.Name = "pathsIntroLabel";
            this.pathsIntroLabel.Size = new System.Drawing.Size(282, 39);
            this.pathsIntroLabel.TabIndex = 12;
            this.pathsIntroLabel.Text = "Save x and y coordinates of each mobile agent, per iteration.";
            // 
            // savePathsFolderInput
            // 
            this.savePathsFolderInput.Location = new System.Drawing.Point(6, 64);
            this.savePathsFolderInput.Name = "savePathsFolderInput";
            this.savePathsFolderInput.Size = new System.Drawing.Size(242, 20);
            this.savePathsFolderInput.TabIndex = 11;
            this.savePathsFolderInput.TextChanged += new System.EventHandler(this.savePathsFolderInput_TextChanged);
            // 
            // savePathsLabel
            // 
            this.savePathsLabel.AutoSize = true;
            this.savePathsLabel.Location = new System.Drawing.Point(3, 47);
            this.savePathsLabel.Name = "savePathsLabel";
            this.savePathsLabel.Size = new System.Drawing.Size(267, 13);
            this.savePathsLabel.TabIndex = 10;
            this.savePathsLabel.Text = "Designate path, or click the button to the right to do so.";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.resetTabControl);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pauseTabControl);
            this.splitContainer3.Size = new System.Drawing.Size(450, 223);
            this.splitContainer3.SplitterDistance = 223;
            this.splitContainer3.TabIndex = 0;
            // 
            // resetTabControl
            // 
            this.resetTabControl.Controls.Add(this.resetTab);
            this.resetTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetTabControl.Location = new System.Drawing.Point(0, 0);
            this.resetTabControl.Name = "resetTabControl";
            this.resetTabControl.SelectedIndex = 0;
            this.resetTabControl.Size = new System.Drawing.Size(223, 223);
            this.resetTabControl.TabIndex = 0;
            // 
            // resetTab
            // 
            this.resetTab.Controls.Add(this.resetAgentPanel);
            this.resetTab.Controls.Add(this.resetAgentLabel);
            this.resetTab.Controls.Add(this.resetIterationLabel);
            this.resetTab.Controls.Add(this.resetIterationInput);
            this.resetTab.Controls.Add(this.resetIntroLabel);
            this.resetTab.Location = new System.Drawing.Point(4, 22);
            this.resetTab.Name = "resetTab";
            this.resetTab.Padding = new System.Windows.Forms.Padding(3);
            this.resetTab.Size = new System.Drawing.Size(215, 197);
            this.resetTab.TabIndex = 0;
            this.resetTab.Text = "Auto Reset";
            this.resetTab.UseVisualStyleBackColor = true;
            // 
            // resetAgentPanel
            // 
            this.resetAgentPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resetAgentPanel.Location = new System.Drawing.Point(3, 106);
            this.resetAgentPanel.Name = "resetAgentPanel";
            this.resetAgentPanel.Size = new System.Drawing.Size(209, 88);
            this.resetAgentPanel.TabIndex = 4;
            // 
            // resetAgentLabel
            // 
            this.resetAgentLabel.AutoSize = true;
            this.resetAgentLabel.Location = new System.Drawing.Point(3, 90);
            this.resetAgentLabel.Name = "resetAgentLabel";
            this.resetAgentLabel.Size = new System.Drawing.Size(66, 13);
            this.resetAgentLabel.TabIndex = 3;
            this.resetAgentLabel.Text = "Agent Count";
            // 
            // resetIterationLabel
            // 
            this.resetIterationLabel.AutoSize = true;
            this.resetIterationLabel.Location = new System.Drawing.Point(3, 46);
            this.resetIterationLabel.Name = "resetIterationLabel";
            this.resetIterationLabel.Size = new System.Drawing.Size(45, 13);
            this.resetIterationLabel.TabIndex = 2;
            this.resetIterationLabel.Text = "Iteration";
            // 
            // resetIterationInput
            // 
            this.resetIterationInput.BackColor = System.Drawing.SystemColors.Window;
            this.resetIterationInput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.resetIterationInput.Location = new System.Drawing.Point(6, 63);
            this.resetIterationInput.Name = "resetIterationInput";
            this.resetIterationInput.Size = new System.Drawing.Size(100, 20);
            this.resetIterationInput.TabIndex = 1;
            this.resetIterationInput.TextChanged += new System.EventHandler(this.resetIterationInput_TextChanged);
            // 
            // resetIntroLabel
            // 
            this.resetIntroLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.resetIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.resetIntroLabel.Name = "resetIntroLabel";
            this.resetIntroLabel.Size = new System.Drawing.Size(209, 39);
            this.resetIntroLabel.TabIndex = 0;
            this.resetIntroLabel.Text = "Auto-reset the CA upon either threshoold type below. If there are multiple inputs" +
    ", the first threshold crossed will cause a reset.";
            // 
            // pauseTabControl
            // 
            this.pauseTabControl.Controls.Add(this.pauseTab);
            this.pauseTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pauseTabControl.Location = new System.Drawing.Point(0, 0);
            this.pauseTabControl.Name = "pauseTabControl";
            this.pauseTabControl.SelectedIndex = 0;
            this.pauseTabControl.Size = new System.Drawing.Size(223, 223);
            this.pauseTabControl.TabIndex = 0;
            // 
            // pauseTab
            // 
            this.pauseTab.Controls.Add(this.pauseAgentPanel);
            this.pauseTab.Controls.Add(this.pauseAgentLabel);
            this.pauseTab.Controls.Add(this.pauseIterationLabel);
            this.pauseTab.Controls.Add(this.pauseIterationInput);
            this.pauseTab.Controls.Add(this.pauseIntroLabel);
            this.pauseTab.Location = new System.Drawing.Point(4, 22);
            this.pauseTab.Name = "pauseTab";
            this.pauseTab.Padding = new System.Windows.Forms.Padding(3);
            this.pauseTab.Size = new System.Drawing.Size(215, 197);
            this.pauseTab.TabIndex = 0;
            this.pauseTab.Text = "Auto Pause";
            this.pauseTab.UseVisualStyleBackColor = true;
            // 
            // pauseAgentPanel
            // 
            this.pauseAgentPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pauseAgentPanel.Location = new System.Drawing.Point(3, 106);
            this.pauseAgentPanel.Name = "pauseAgentPanel";
            this.pauseAgentPanel.Size = new System.Drawing.Size(209, 88);
            this.pauseAgentPanel.TabIndex = 9;
            // 
            // pauseAgentLabel
            // 
            this.pauseAgentLabel.AutoSize = true;
            this.pauseAgentLabel.Location = new System.Drawing.Point(3, 90);
            this.pauseAgentLabel.Name = "pauseAgentLabel";
            this.pauseAgentLabel.Size = new System.Drawing.Size(66, 13);
            this.pauseAgentLabel.TabIndex = 8;
            this.pauseAgentLabel.Text = "Agent Count";
            // 
            // pauseIterationLabel
            // 
            this.pauseIterationLabel.AutoSize = true;
            this.pauseIterationLabel.Location = new System.Drawing.Point(3, 46);
            this.pauseIterationLabel.Name = "pauseIterationLabel";
            this.pauseIterationLabel.Size = new System.Drawing.Size(45, 13);
            this.pauseIterationLabel.TabIndex = 7;
            this.pauseIterationLabel.Text = "Iteration";
            // 
            // pauseIterationInput
            // 
            this.pauseIterationInput.Location = new System.Drawing.Point(6, 63);
            this.pauseIterationInput.Name = "pauseIterationInput";
            this.pauseIterationInput.Size = new System.Drawing.Size(100, 20);
            this.pauseIterationInput.TabIndex = 6;
            this.pauseIterationInput.TextChanged += new System.EventHandler(this.pauseIterationInput_TextChanged);
            // 
            // pauseIntroLabel
            // 
            this.pauseIntroLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pauseIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.pauseIntroLabel.Name = "pauseIntroLabel";
            this.pauseIntroLabel.Size = new System.Drawing.Size(209, 39);
            this.pauseIntroLabel.TabIndex = 5;
            this.pauseIntroLabel.Text = "Auto-pause the CA upon either threshoold type below. If there are multiple inputs" +
    ", the first threshold crossed will cause a pause.";
            // 
            // templateTab
            // 
            this.templateTab.Controls.Add(this.tempTabExplain);
            this.templateTab.Location = new System.Drawing.Point(4, 22);
            this.templateTab.Name = "templateTab";
            this.templateTab.Padding = new System.Windows.Forms.Padding(3);
            this.templateTab.Size = new System.Drawing.Size(288, 197);
            this.templateTab.TabIndex = 3;
            this.templateTab.Text = "Template";
            this.templateTab.UseVisualStyleBackColor = true;
            // 
            // tempTabExplain
            // 
            this.tempTabExplain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tempTabExplain.Location = new System.Drawing.Point(3, 3);
            this.tempTabExplain.Name = "tempTabExplain";
            this.tempTabExplain.Size = new System.Drawing.Size(282, 39);
            this.tempTabExplain.TabIndex = 3;
            this.tempTabExplain.Text = "Set save options for a specific template being used.";
            // 
            // SaveDataDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 480);
            this.Controls.Add(this.outerPanel);
            this.Name = "SaveDataDialog";
            this.Text = "SaveDataDialog";
            this.outerPanel.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.saveTabsControl.ResumeLayout(false);
            this.dataTab.ResumeLayout(false);
            this.dataTab.PerformLayout();
            this.imageTab.ResumeLayout(false);
            this.imageTab.PerformLayout();
            this.pathTab.ResumeLayout(false);
            this.pathTab.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.resetTabControl.ResumeLayout(false);
            this.resetTab.ResumeLayout(false);
            this.resetTab.PerformLayout();
            this.pauseTabControl.ResumeLayout(false);
            this.pauseTab.ResumeLayout(false);
            this.pauseTab.PerformLayout();
            this.templateTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel outerPanel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Label messageBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView saveOptions;
        private System.Windows.Forms.TabControl saveTabsControl;
        private System.Windows.Forms.TabPage dataTab;
        private System.Windows.Forms.TextBox dataIncInput;
        private System.Windows.Forms.Label dataIncLabel;
        private System.Windows.Forms.Label dataIntroLabel;
        private System.Windows.Forms.TextBox saveDataFolderInput;
        private System.Windows.Forms.Label saveFolderLabel;
        private System.Windows.Forms.TabPage imageTab;
        private System.Windows.Forms.TextBox imageIncInput;
        private System.Windows.Forms.Label imageIncLabel;
        private System.Windows.Forms.Label imageIntroLabel;
        private System.Windows.Forms.TextBox saveImageFolderInput;
        private System.Windows.Forms.Label saveImageLabel;
        private System.Windows.Forms.TabPage pathTab;
        private System.Windows.Forms.TextBox pathsIncInput;
        private System.Windows.Forms.Label pathsIncLabel;
        private System.Windows.Forms.Label pathsIntroLabel;
        private System.Windows.Forms.TextBox savePathsFolderInput;
        private System.Windows.Forms.Label savePathsLabel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl resetTabControl;
        private System.Windows.Forms.TabPage resetTab;
        private System.Windows.Forms.Panel resetAgentPanel;
        private System.Windows.Forms.Label resetAgentLabel;
        private System.Windows.Forms.Label resetIterationLabel;
        private System.Windows.Forms.TextBox resetIterationInput;
        private System.Windows.Forms.Label resetIntroLabel;
        private System.Windows.Forms.TabControl pauseTabControl;
        private System.Windows.Forms.TabPage pauseTab;
        private System.Windows.Forms.Panel pauseAgentPanel;
        private System.Windows.Forms.Label pauseAgentLabel;
        private System.Windows.Forms.Label pauseIterationLabel;
        private System.Windows.Forms.TextBox pauseIterationInput;
        private System.Windows.Forms.Label pauseIntroLabel;
        private System.Windows.Forms.Button messageNextButton;
        private System.Windows.Forms.Button messagePrevButton;
        private System.Windows.Forms.TabPage templateTab;
        private System.Windows.Forms.Label tempTabExplain;
    }
}