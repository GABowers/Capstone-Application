﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class Form1 : Form
    {
        bool running = false;
        bool saveImages = false;
        string imageSaveFolder;
        public static ControllerScript controllerScript = new ControllerScript();
        public Form1()
        {
            InitializeComponent();
        }

        private void newModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "New Model Dialog";
            Form2 newModelDialog = new Form2(name);
            newModelDialog.mainForm = this;
            newModelDialog.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void dansNeighborAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            controllerScript.CreateCA();
            controllerScript.StartCA(this);
            UpdateRunBox();
            UpdateIterationBox();
        }

        private void UpdateRunBox()
        {
            runCountBox.Text = controllerScript.runs.ToString();
        }

        private void UpdateIterationBox()
        {
            iterationCountBox.Text = controllerScript.iterations.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            running = !running;
            if(running == true)
            backgroundWorker1.RunWorkerAsync();
        }

        private void RunCA()
        {
            controllerScript.OneIteration(this);
        }
        
        private void UpdateImage()
        {
            controllerScript.UpdateBoard(this);
            UpdateIterationBox();
        }

        private void CheckSettings()
        {
            if(saveImages == true)
            {
                string filename = imageSaveFolder + "/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
                (controllerScript.runs + 1) + " Iteration " + controllerScript.iterations + ".bmp";
                innerPictureBox.Image.Save(filename);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RunCA();
            CheckSettings();
            UpdateImage();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (running == true)
            {
                RunCA();
                Invoke(new Action(() => UpdateImage()));
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void saveCellCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter wt = new StreamWriter(saveFileDialog1.FileName))
                {

                    for (int i = 0; i < controllerScript.fullCount.Count; ++i)
                    {
                        wt.Write("Iteration: " + i);
                        for (int j = 0; j < controllerScript.fullCount[i].Count; ++j)
                        {
                            string cellTypeString = (j + 1).ToString();
                            wt.Write(" Cell Type " + cellTypeString + ": " + controllerScript.fullCount[i][j]);
                        }
                        wt.WriteLine();
                    }
                    wt.Close();
                }
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.bmp)|*.bmp|(*.jpeg)|*.jpeg|(*.png)|*.png|(*.tiff)|*.tiff";
            sfd.FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
                (controllerScript.runs + 1) + " Iteration " + controllerScript.iterations;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                innerPictureBox.Image.Save(sfd.FileName);
            }
        }

        private void saveAllImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImages = !saveImages;
            if(saveImages == true)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        imageSaveFolder = fbd.SelectedPath;
                    }
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "Capstone Template files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                }
            }
        }

        private void editModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "Edit Model Dialog";
            Form2 newModelDialog = new Form2 (name);
            newModelDialog.mainForm = this;
            newModelDialog.ShowDialog();
        }
    }
}
