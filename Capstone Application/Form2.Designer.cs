﻿namespace Capstone_Application
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
            this.templatePanel = new System.Windows.Forms.Panel();
            this.template_reset_checkbox = new System.Windows.Forms.CheckBox();
            this.template_reset_explanation = new System.Windows.Forms.Label();
            this.template_reset_label = new System.Windows.Forms.Label();
            this.templateBox = new System.Windows.Forms.ComboBox();
            this.templateBoxLabel = new System.Windows.Forms.Label();
            this.gridSizeHori = new System.Windows.Forms.TextBox();
            this.gridSizeVert = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stateNumberBox = new System.Windows.Forms.TextBox();
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
            this.tabPage1.Controls.Add(this.templatePanel);
            this.tabPage1.Controls.Add(this.template_reset_checkbox);
            this.tabPage1.Controls.Add(this.template_reset_explanation);
            this.tabPage1.Controls.Add(this.template_reset_label);
            this.tabPage1.Controls.Add(this.templateBox);
            this.tabPage1.Controls.Add(this.templateBoxLabel);
            this.tabPage1.Controls.Add(this.gridSizeHori);
            this.tabPage1.Controls.Add(this.gridSizeVert);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.stateNumberBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(583, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // templatePanel
            // 
            this.templatePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.templatePanel.Location = new System.Drawing.Point(3, 161);
            this.templatePanel.Name = "templatePanel";
            this.templatePanel.Size = new System.Drawing.Size(577, 337);
            this.templatePanel.TabIndex = 14;
            // 
            // template_reset_checkbox
            // 
            this.template_reset_checkbox.AutoSize = true;
            this.template_reset_checkbox.Location = new System.Drawing.Point(253, 34);
            this.template_reset_checkbox.Name = "template_reset_checkbox";
            this.template_reset_checkbox.Size = new System.Drawing.Size(15, 14);
            this.template_reset_checkbox.TabIndex = 2;
            this.template_reset_checkbox.UseVisualStyleBackColor = true;
            this.template_reset_checkbox.CheckedChanged += new System.EventHandler(this.template_reset_checkbox_CheckedChanged);
            // 
            // template_reset_explanation
            // 
            this.template_reset_explanation.Location = new System.Drawing.Point(286, 29);
            this.template_reset_explanation.Name = "template_reset_explanation";
            this.template_reset_explanation.Size = new System.Drawing.Size(291, 40);
            this.template_reset_explanation.TabIndex = 12;
            this.template_reset_explanation.Text = "Some template simulations stop after a certain event occurs. To reset the grid at" +
    " this point, check this box. Make sure to save your data!";
            this.template_reset_explanation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // template_reset_label
            // 
            this.template_reset_label.Location = new System.Drawing.Point(9, 29);
            this.template_reset_label.Name = "template_reset_label";
            this.template_reset_label.Size = new System.Drawing.Size(125, 20);
            this.template_reset_label.TabIndex = 11;
            this.template_reset_label.Text = "Template reset";
            this.template_reset_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // templateBox
            // 
            this.templateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateBox.FormattingEnabled = true;
            this.templateBox.Items.AddRange(new object[] {
            "None",
            "Random Walk",
            "DLA",
            "Isle Royale",
            "Ant Sim",
            "Gas"});
            this.templateBox.Location = new System.Drawing.Point(147, 9);
            this.templateBox.Name = "templateBox";
            this.templateBox.Size = new System.Drawing.Size(121, 21);
            this.templateBox.TabIndex = 1;
            this.templateBox.SelectedIndexChanged += new System.EventHandler(this.templateBox_SelectedIndexChanged);
            // 
            // templateBoxLabel
            // 
            this.templateBoxLabel.Location = new System.Drawing.Point(9, 9);
            this.templateBoxLabel.Name = "templateBoxLabel";
            this.templateBoxLabel.Size = new System.Drawing.Size(125, 20);
            this.templateBoxLabel.TabIndex = 8;
            this.templateBoxLabel.Text = "Template";
            this.templateBoxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridSizeHori
            // 
            this.gridSizeHori.Location = new System.Drawing.Point(147, 115);
            this.gridSizeHori.Name = "gridSizeHori";
            this.gridSizeHori.Size = new System.Drawing.Size(121, 20);
            this.gridSizeHori.TabIndex = 4;
            // 
            // gridSizeVert
            // 
            this.gridSizeVert.Location = new System.Drawing.Point(147, 135);
            this.gridSizeVert.Name = "gridSizeVert";
            this.gridSizeVert.Size = new System.Drawing.Size(121, 20);
            this.gridSizeVert.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Grid Size (Vertical)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Grid Size (Horizontal)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Number of states";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stateNumberBox
            // 
            this.stateNumberBox.Location = new System.Drawing.Point(147, 95);
            this.stateNumberBox.Name = "stateNumberBox";
            this.stateNumberBox.Size = new System.Drawing.Size(121, 20);
            this.stateNumberBox.TabIndex = 3;
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
            this.cancelTab.TabIndex = 98;
            this.cancelTab.Text = "Cancel";
            this.cancelTab.UseVisualStyleBackColor = true;
            this.cancelTab.Click += new System.EventHandler(this.cancelTab_Click);
            // 
            // confirmTab
            // 
            this.confirmTab.Location = new System.Drawing.Point(531, 532);
            this.confirmTab.Name = "confirmTab";
            this.confirmTab.Size = new System.Drawing.Size(60, 28);
            this.confirmTab.TabIndex = 99;
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
            this.previousTab.TabIndex = 96;
            this.previousTab.Text = "Previous Tab";
            this.previousTab.UseVisualStyleBackColor = true;
            this.previousTab.Click += new System.EventHandler(this.previousTab_Click);
            // 
            // nextTab
            // 
            this.nextTab.Location = new System.Drawing.Point(379, 532);
            this.nextTab.Name = "nextTab";
            this.nextTab.Size = new System.Drawing.Size(80, 28);
            this.nextTab.TabIndex = 97;
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings Dialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stateNumberBox;
        private System.Windows.Forms.TextBox gridSizeHori;
        private System.Windows.Forms.TextBox gridSizeVert;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button cancelTab;
        private System.Windows.Forms.Button confirmTab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button previousTab;
        private System.Windows.Forms.Button nextTab;
        internal System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox templateBox;
        private System.Windows.Forms.Label templateBoxLabel;
        private System.Windows.Forms.CheckBox template_reset_checkbox;
        private System.Windows.Forms.Label template_reset_explanation;
        private System.Windows.Forms.Label template_reset_label;
        private System.Windows.Forms.Panel templatePanel;
    }
}