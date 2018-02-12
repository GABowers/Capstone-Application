using System;
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
        bool mousePressed;
        bool counterFormOpen = false;
        bool running = false;
        bool saveImages = false;
        string imageSaveFolder;
        string countSaveFolder;
        int mouseDownX = 0;
        int mouseDownY = 0;
        public static ControllerScript controllerScript = new ControllerScript();
        Counter counterWindow;
        public Form1()
        {
            InitializeComponent();
            innerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void newModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "New Model Dialog";
            Form2 newModelDialog = new Form2(name, this);
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
            UpdateImage();
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
            if (running == true)
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
            if (counterFormOpen == true)
            {
                counterWindow.UpdateCounts();
            }
        }

        private void CheckSettings()
        {
            if (saveImages == true)
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
            //Get rid of this control. We don't need it.
            saveImages = !saveImages;
            if (saveImages == true)
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
            Form2 newModelDialog = new Form2(name, this);
            newModelDialog.mainForm = this;
            newModelDialog.ShowDialog();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            controllerScript.ResetGrid();
            UpdateRunBox();
            UpdateIterationBox();
        }

        public void AutoReset()
        {
            controllerScript.ResetGrid();
            controllerScript.CreateCA();
            controllerScript.StartCA(this);
            UpdateRunBox();
            UpdateIterationBox();
            UpdateImage();
        }

        private void editGridButton_Click(object sender, EventArgs e)
        {
            if (controllerScript.editModeOn == false)
            {
                this.editGridButton.Text = "Editing";
                controllerScript.editModeOn = true;
            }
            else if (controllerScript.editModeOn == true)
            {
                this.editGridButton.Text = "Edit Grid";
                controllerScript.editModeOn = false;
            }
        }

        private void innerPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void innerPictureBox_MouseDown(object sender, EventArgs e)
        {
            if (controllerScript.editModeOn == true)
            {
                System.Drawing.Point point = innerPictureBox.PointToClient(Cursor.Position);
                mouseDownX = point.X;
                mouseDownY = point.Y;
            }
        }

        private void innerPictureBox_MouseUp(object sender, EventArgs e)
        {
            if (controllerScript.editModeOn == true)
            {
                System.Drawing.Point point = innerPictureBox.PointToClient(Cursor.Position);
                int mouseUpX = point.X;
                int mouseUpY = point.Y;
                if (mouseDownX == mouseUpX && mouseDownY == mouseUpY)
                {
                    controllerScript.EditGrid(mouseUpX, mouseUpY, innerPictureBox);
                }
                else
                {
                    int maxX = Math.Max(mouseDownX, mouseUpX);
                    int minX = Math.Min(mouseDownX, mouseUpX);
                    int maxY = Math.Max(mouseDownY, mouseUpY);
                    int minY = Math.Min(mouseDownY, mouseUpY);
                    int distanceX = (maxX - minX);
                    int distanceY = (maxY - minY);
                    int[] rangeX = new int[distanceX + 1];
                    int[] rangeY = new int[distanceY + 1];
                    for (int i = 0; i < (distanceX + 1); i++)
                    {
                        rangeX[i] = (minX + i);
                    }
                    for (int i = 0; i < (distanceY + 1); i++)
                    {
                        rangeY[i] = (minY + i);
                    }
                    controllerScript.EditGrid(rangeX, rangeY, innerPictureBox);
                }
                UpdateImage();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void setAutoResetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cellCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Move this stuff to be performed when a user creates their setup in the separate dialog box
            // We don't want this done every time we click on the menu item.

        }

        public void UpdateIterationResetCell(int states)
        {
            for (int i = 0; i < states; i++)
            {
                string newname = "CellBox" + i.ToString();
                this.cellCountToolStripMenuItem.DropDownItems.Add(new ToolStripTextBox(newname));
            }
        }

        public string GetText(string controlName)
        {
            return this.cellCountToolStripMenuItem.DropDownItems[controlName].Text;
        }

        private void setImageSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(setImageSaveToolStripMenuItem.Checked == true)
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

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if(toolStripMenuItem5.Checked == true)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        countSaveFolder = fbd.SelectedPath;
                    }
                }
            }
        }

        public void SaveImages()
        {
            string imageName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
                (controllerScript.runs + 1) + " Iteration " + controllerScript.iterations;
            string fileName = (imageSaveFolder + "/" + imageName);
            innerPictureBox.Image.Save(fileName);
        }

        public void SaveCount()
        {
            string countName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
                (controllerScript.runs + 1) + " Iteration " + controllerScript.iterations;
            string fileName = (countSaveFolder + "/" + countName);

            using (StreamWriter wt = new StreamWriter(fileName))
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

        private void cellCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the CA has been created yet. If not, obviously don't make this form
            // Form2 newModelDialog = new Form2(name, this);
            counterWindow = new Counter(this);
            counterWindow.Show();
            counterFormOpen = true;
        }
    }
}
