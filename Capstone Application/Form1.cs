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
        Form2 otherform;
        ToolTip locationTT;
        //bool unpaused;
        public bool counterFormOpen = false;
        bool running = false;
        //bool saveImages = false;
        string imageSaveFolder;
        string countSaveFolder;
        string pathSaveFolder;
        int mouseDownX = 0;
        int mouseDownY = 0;
        int iterationSpeed = 0;
        int editState = 0;
        int? runMax;
        public static RunSettings runSettings;
        public static ControllerScript controllerScript = new ControllerScript();
        Counter counterWindow;
        SaveDataDialog saveDialog;
        System.Timers.Timer text_timer = new System.Timers.Timer();

        public int? RunMax { get => runMax; set => runMax = value; }

        public Form1()
        {
            InitializeComponent();
            innerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            locationTT = new ToolTip();
            text_timer.Interval = 5;
            text_timer.Elapsed += Timer_Tick;
            this.SetDesktopLocation(0, 0);
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            Console.WriteLine("timer active");
            Invoke(new Action(() => UpdateIterationBox()));
        }

        private void newModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "New Model Dialog";
            Form2 newModelDialog = new Form2(name, this, false);
            otherform = newModelDialog;
            newModelDialog.ShowDialog();
        }

        public void OnOtherFormClose()
        {
            otherform.Dispose();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void dansNeighborAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            controllerScript.CreateCA((this));
            controllerScript.StartCA(this);
            UpdateRunBox();
            UpdateImage();
        }

        private void UpdateRunBox()
        {
            runCountBox.Text = controllerScript.caRuns.ToString();
        }

        private void UpdateIterationBox()
        {
            iterationCountBox.Text = controllerScript.iterations.ToString();
            toolStrip1.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void toolStripLabel2_Click(object sender, EventArgs e)
        {
            PauseUnpauseCA();
        }

        public void PauseUnpauseCA()
        {
            if(controllerScript.CreatedCA)
            {
                running = !running;
                if (running == true)
                {
                    backgroundWorker1.RunWorkerAsync();
                    text_timer.Enabled = true;
                    //text_timer.Start();
                }
                else if(running == false)
                {
                    //text_timer.Stop();
                    text_timer.Enabled = false;
                }
                    
            }
        }

        private void RunCA()
        {
            controllerScript.OneIteration(this);
        }

        private void UpdateImage()
        {
            controllerScript.UpdateBoard(this);
            if (counterFormOpen == true)
            {
                counterWindow.UpdateCounts();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RunCA();
            UpdateImage();
            UpdateIterationBox();
            controllerScript.CheckSettings(this);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Ask Dan about this
            while (running == true)
            {
                if(iterationSpeed == 0)
                {
                    RunCA();
                    Invoke(new Action(() => UpdateImage()));
                    controllerScript.CheckSettings(this);
                }
                else if(iterationSpeed > 0)
                {
                    RunCA();
                    Invoke(new Action(() => UpdateImage()));
                    controllerScript.CheckSettings(this);
                    System.Threading.Thread.Sleep(iterationSpeed);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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
            Form2 editModelDialog = new Form2(name, this, true);
            otherform = editModelDialog;
            editModelDialog.Visible = false;
            editModelDialog.ShowDialog(this);
        }

        public void AutoReset()
        {
            controllerScript.ResetGrid(this);
            controllerScript.CreateCA((this));
            controllerScript.StartCA(this);
            controllerScript.CheckMaxRuns(this);
            Invoke(new Action(() => UpdateRunBox()));
            Invoke(new Action(() => UpdateImage()));
        }

        private void editGridButton_Click(object sender, EventArgs e)
        {
            if(running == false)
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
        }

        private void innerPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void innerPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (controllerScript.editModeOn == true)
            {
                System.Drawing.Point point = innerPictureBox.PointToClient(Cursor.Position);
                mouseDownX = point.X;
                mouseDownY = point.Y;
            }
        }

        private void innerPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (controllerScript.editModeOn == true)
            {
                System.Drawing.Point point = innerPictureBox.PointToClient(Cursor.Position);
                int mouseUpX = point.X;
                int mouseUpY = point.Y;

                if (mouseDownX == mouseUpX && mouseDownY == mouseUpY)
                {
                    if(e.Button == MouseButtons.Left)
                    {
                        controllerScript.EditGrid(mouseUpX, mouseUpY, innerPictureBox, 0, editState);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        controllerScript.EditGrid(mouseUpX, mouseUpY, innerPictureBox, 1, editState);
                    }
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
                    if (e.Button == MouseButtons.Left)
                    {
                        controllerScript.EditGrid(rangeX, rangeY, innerPictureBox, 0, editState);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        controllerScript.EditGrid(rangeX, rangeY, innerPictureBox, 1, editState);
                    }
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
                string otherName = "Type " + i.ToString();
                ToolStripTextBox textBox = new ToolStripTextBox(newname);
                ToolStripMenuItem stateSelect = new ToolStripMenuItem(otherName);
                stateSelect.Text = otherName;
                stateSelect.Click += new System.EventHandler(stateSelect_Click);
                this.editGridButton.DropDownItems.Add(stateSelect);
            }
        }

        public void UpdateIterationPauseCell(int states)
        {
            for (int i = 0; i < states; i++)
            {
                string newname = "CellBox" + i.ToString();
                ToolStripTextBox textBox = new ToolStripTextBox(newname);
            }
        }

        private void stateSelect_Click(object sender, EventArgs e)
        {
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            editState = int.Parse(item.Text.Remove(0, 5));
        }

        public void UncheckOtherToolStripMenuItems(ToolStripMenuItem selectedMenuItem)
        {
            selectedMenuItem.Checked = true;

            // Select the other MenuItens from the ParentMenu(OwnerItens) and unchecked this,
            // The current Linq Expression verify if the item is a real ToolStripMenuItem
            // and if the item is a another ToolStripMenuItem to uncheck this.
            foreach (var ltoolStripMenuItem in (from object
                                                    item in selectedMenuItem.Owner.Items
                                                let ltoolStripMenuItem = item as ToolStripMenuItem
                                                where ltoolStripMenuItem != null
                                                where !item.Equals(selectedMenuItem)
                                                select ltoolStripMenuItem))
                (ltoolStripMenuItem).Checked = false;

            // This line is optional, for show the mainMenu after click
            // selectedMenuItem.Owner.Show();
        }

        //string GetCountResetText(string controlName)
        //{
        //    return this.cellCountToolStripMenuItem.DropDownItems[controlName].Text;
        //}

        //string GetCountPauseText(string controlName)
        //{
        //    return this.autoPauseCellCount.DropDownItems[controlName].Text;
        //}

        public void InvokeImageSave(string time, string path)
        {
            Invoke(new Action(() => SaveImages(time, path)));
        }

        public void SaveImages(string time, string path)
        {
            string imageName = time + " Run " + controllerScript.caRuns + " Iteration " + controllerScript.iterations + " Image.bmp";
            string fileName = (path + "/" + imageName);
            innerPictureBox.Image.Save(fileName);
        }

        //public void SaveCounts(string time)
        //{
        //    string countName = time + " Run " + controllerScript.caRuns + " Iteration " + controllerScript.iterations + " Cell Count.txt";
        //    string fileName = (countSaveFolder + "/" + countName);
        //    using (StreamWriter wt = new StreamWriter(fileName))
        //    {

        //        for (int i = 0; i < controllerScript.FullCount.Count; ++i)
        //        {
        //            wt.Write("Iteration: " + (i + 1).ToString());
        //            for (int j = 0; j < controllerScript.FullCount[i].Count; ++j)
        //            {
        //                string cellTypeString = (j + 1).ToString();
        //                wt.Write(" Cell Type " + cellTypeString + ": " + controllerScript.FullCount[i][j]);
        //            }
        //            wt.WriteLine();
        //        }
        //        wt.Close();
        //    }
        //}

        public void AutoPathSave(string time)
        {
            string countName = time + " Run " + controllerScript.caRuns + " Iteration " + controllerScript.iterations + " Agent Paths.txt";
            string fileName = (pathSaveFolder + "/" + countName);
            string agent = "Agent ";
            using (StreamWriter wt = new StreamWriter(fileName))
            {
                int maxLength = (controllerScript.myCA.gridWidth.ToString().Length * 2) + 4;

                if ((controllerScript.myCA.ActiveAgents.Count.ToString().Length) + 6 > maxLength)
                {
                    maxLength = (controllerScript.myCA.ActiveAgents.Count.ToString().Length) + 6;
                }
                for (int i = 0; i < controllerScript.myCA.ActiveAgents.Count; ++i)
                {
                    wt.Write(agent.PadRight(maxLength - (i + 1).ToString().Length) + (i + 1) + "|");
                }
                wt.WriteLine();
                for (int i = 0; i < (controllerScript.iterations + 1); i++)
                {
                    for (int j = 0; j < controllerScript.myCA.ActiveAgents.Count; j++)
                    {
                        wt.Write(controllerScript.myCA.ActiveAgents[j].History[i].ToString().PadRight(maxLength) + "|");
                    }
                    wt.WriteLine();
                }

                wt.Close();
            }
        }

        //public void SaveCount()
        //{
        //    string countName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
        //        DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
        //        controllerScript.caRuns + " Iteration " + controllerScript.iterations;
        //    string fileName = (countSaveFolder + "/" + countName);

        //    using (StreamWriter wt = new StreamWriter(fileName))
        //    {

        //        for (int i = 0; i < controllerScript.FullCount.Count; ++i)
        //        {
        //            wt.Write("Iteration: " + (i + 1).ToString());
        //            for (int j = 0; j < controllerScript.FullCount[i].Count; ++j)
        //            {
        //                string cellTypeString = (j + 1).ToString();
        //                wt.Write(" Cell Type " + cellTypeString + ": " + controllerScript.FullCount[i][j]);
        //            }
        //            wt.WriteLine();
        //        }
        //        wt.Close();
        //    }
        //}

        public void SaveData(string time, bool counts, bool trans, bool cIndex, string path)
        {
            path = path + "/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + " Runs " + controllerScript.caRuns + " Iterations " + controllerScript.iterations + " Data.csv";
            Console.WriteLine("Path: " + path);
            using (StreamWriter wt = new StreamWriter(path))
            {
                //pertinent info - run, iteration, time
                string thing = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + " Runs: " + controllerScript.caRuns + " Iterations: " + controllerScript.iterations;
                wt.Write(thing);
                wt.WriteLine();
                wt.Write("Iteration,");
                if (counts)
                {
                    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                    {
                        string cellTypeString = (j + 1).ToString();
                        wt.Write("Counts Type " + cellTypeString + ",");
                    }
                }
                if (trans)
                {
                    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                    {
                        string cellTypeString = (j + 1).ToString();
                        wt.Write("Transitions Type " + cellTypeString + ",");
                    }
                }
                if (cIndex)
                {
                    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                    {
                        string cellTypeString = (j + 1).ToString();
                        wt.Write("C Index Type " + cellTypeString + ",");
                    }
                }
                wt.WriteLine();
                for (int i = 0; i < controllerScript.iterations; ++i)
                {
                    wt.Write((i + 1).ToString() + ",");
                    wt.WriteLine();
                }
                wt.Close();
            }
            string[] lines = File.ReadAllLines(path);
            int count_val = 0;
            int trans_val = 0;
            int index_val = 0;
            for (int i = 0; i < controllerScript.iterations; ++i)
            {
                int line = i + 2;
                string currentLine = lines[line];
                
                if (counts)
                {
                    List<Tuple<int, List<int>>> local_count = controllerScript.FullCount;
                    if (trans_val <= local_count.Count - 1)
                    {
                        if ((i + 1) == local_count[count_val].Item1)
                        {
                            for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                            {
                                currentLine += local_count[count_val].Item2[j].ToString() + ",";
                            }
                            count_val += 1;
                        }
                        else
                        {
                            for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                            {
                                currentLine += local_count[count_val - 1].Item2[j].ToString() + ",";
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                        {
                            currentLine += local_count[count_val - 1].Item2[j].ToString() + ",";
                        }
                    }
                }
                if (trans)
                {
                    List<Tuple<int, List<int>>> local_trans = controllerScript.FullTransitions;
                    if (trans_val <= local_trans.Count - 1)
                    {
                        if ((i + 1) == local_trans[trans_val].Item1)
                        {
                            for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                            {
                                currentLine += local_trans[trans_val].Item2[j].ToString() + ",";
                            }
                            trans_val += 1;
                        }
                        else
                        {
                            for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                            {
                                currentLine += local_trans[trans_val - 1].Item2[j].ToString() + ",";
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                        {
                            currentLine += local_trans[trans_val - 1].Item2[j].ToString() + ",";
                        }
                    }
                }
                if (cIndex)
                {
                    List<Tuple<int, List<double>>> local_index = controllerScript.FullIndex;
                    if (index_val <= local_index.Count - 1)
                    {
                        if ((i + 1) == local_index[index_val].Item1)
                        {
                            for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                            {
                                currentLine += local_index[index_val].Item2[j].ToString() + ",";
                            }
                            index_val += 1;
                        }
                        else
                        {
                            for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                            {
                                currentLine += local_index[index_val - 1].Item2[j].ToString() + ",";
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                        {
                            currentLine += local_index[index_val - 1].Item2[j].ToString() + ",";
                        }
                    }
                }
                lines[line] = currentLine;
            }
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    writer.Write(lines[i]);
                    writer.WriteLine();
                }
                writer.Close();
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

        private void resetIterationTextBox_Leave(object sender, EventArgs e)
        {
            
        }

        private void iterationCountCountSave_Leave(object sender, EventArgs e)
        {
            
        }

        private void iterationCountImageSave_Leave(object sender, EventArgs e)
        {
            
        }

        private void cellCountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            ////saveFileDialog1.InitialDirectory = Application.StartupPath;
            //saveFileDialog1.Filter = "(*.txt)|*.txt|(*.*)|*.*";
            //saveFileDialog1.FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
            //    DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
            //    controllerScript.caRuns + " Iteration " + controllerScript.iterations + " Cell Count";
            ////saveFileDialog1.RestoreDirectory = true;

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    using (StreamWriter wt = new StreamWriter(saveFileDialog1.FileName))
            //    {
            //        for (int i = 0; i < controllerScript.FullCount.Count; ++i)
            //        {
            //            wt.Write("Iteration: " + (i+1).ToString());
            //            for (int j = 0; j < controllerScript.FullCount[i].Count; ++j)
            //            {
            //                string cellTypeString = (j + 1).ToString();
            //                wt.Write(" Cell Type " + cellTypeString + ": " + controllerScript.FullCount[i][j]);
            //            }
            //            wt.WriteLine();
            //        }
            //        wt.Close();
            //    }
            //}
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.bmp)|*.bmp|(*.jpeg)|*.jpeg|(*.png)|*.png|(*.tiff)|*.tiff";
            sfd.FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
                controllerScript.caRuns + " Iteration " + controllerScript.iterations + " Image";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                innerPictureBox.Image.Save(sfd.FileName);
            }
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    if (controllerScript.myCA.CaType == 1)
            //    {
            //        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //        saveFileDialog1.InitialDirectory = Application.StartupPath;
            //        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //        saveFileDialog1.FilterIndex = 2;
            //        saveFileDialog1.RestoreDirectory = true;

            //        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //        {
            //            string agent = "Agent ";
            //            using (StreamWriter wt = new StreamWriter(saveFileDialog1.FileName))
            //            {
            //                int maxLength = (controllerScript.myCA.gridWidth.ToString().Length * 2) + 4;

            //                if ((controllerScript.myCA.ActiveAgents.Count.ToString().Length) + 6 > maxLength)
            //                {
            //                    maxLength = (controllerScript.myCA.ActiveAgents.Count.ToString().Length) + 6;
            //                }
            //                for (int i = 0; i < controllerScript.myCA.ActiveAgents.Count; ++i)
            //                {
            //                    wt.Write(agent.PadRight(maxLength - (i + 1).ToString().Length) + (i + 1) + "|");
            //                }
            //                wt.WriteLine();
            //                for (int i = 0; i < (controllerScript.iterations + 1); i++)
            //                {
            //                    for (int j = 0; j < controllerScript.myCA.ActiveAgents.Count; j++)
            //                    {
            //                        wt.Write(controllerScript.myCA.ActiveAgents[j].History[i].ToString().PadRight(maxLength) + "|");
            //                    }
            //                    wt.WriteLine();
            //                }

            //                wt.Close();
            //            }
            //        }
            //    }
        }

        private void visualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageTrace showImageTrace = new ImageTrace();
            showImageTrace.ShowDialog();
        }

        private void speedInput_Click(object sender, EventArgs e)
        {

        }

        private void decrease_Click(object sender, EventArgs e)
        {
            int oldNumber = int.Parse(this.speedInput.Text);
            int newNumber = oldNumber - 1;
            if (newNumber < 0)
                newNumber = 0;
            this.speedInput.Text = newNumber.ToString();
        }

        private void increase_Click(object sender, EventArgs e)
        {
            int oldNumber = int.Parse(this.speedInput.Text);
            int newNumber = oldNumber + 1;
            if (newNumber > 999)
                newNumber = 999;
            this.speedInput.Text = newNumber.ToString();
        }

        private void speedInput_TextChanged(object sender, EventArgs e)
        {
            int speed = int.Parse(this.speedInput.Text);
            if (speed == 0)
            {
                iterationSpeed = 0;
            }
            else if (speed > 0)
            {
                double temp = 1 / (double)speed;
                double temp2 = temp * 1000;
                iterationSpeed = (int)temp2;
            }
        }

        private void innerPictureBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void innerPictureBox_MouseLeave(object sender, EventArgs e)
        {
            //base.OnMouseLeave(e);

            locationTT.Hide(innerPictureBox);
        }

        private void innerPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (controllerScript.editModeOn == true)
            {

                //if (this.locationTT == null)
                //    return;
                System.Drawing.Point point = innerPictureBox.PointToClient(Cursor.Position);
                Tuple<int, int> realLocation = controllerScript.TrueLocation(point.X, point.Y, innerPictureBox);
                string location = "[" + realLocation.Item1 + ", " + realLocation.Item2 + "]";
                locationTT.SetToolTip(innerPictureBox, location);
            }
        }

        private void resetCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controllerScript.ResetRuns();
            UpdateRunBox();
        }

        private void runCountMaxRuns_Click(object sender, EventArgs e)
        {

        }

        private void runCountMaxRuns_CheckedChanged(object sender, EventArgs e)
        {
            if (runCountMaxRuns.Checked)
            {
            }
            else
            {
            }
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(toolStripTextBox1.Text, out int result))
            {
            }
            else
            {
            }
        }



        private void autoPauseIterationCount_Click(object sender, EventArgs e)
        {

        }
        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter wt = new StreamWriter(saveFileDialog1.FileName))
                {
                    //pertinent info - run, iteration, time
                    string thing = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + " Runs: " + controllerScript.caRuns + " Iterations: " + controllerScript.iterations;
                    wt.Write(thing);
                    wt.WriteLine();
                    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                    {
                        string cellTypeString = (j + 1).ToString();
                        wt.Write("Type " + cellTypeString);
                    }
                    wt.WriteLine();
                    for (int i = 0; i < controllerScript.iterations; ++i)
                    {
                        wt.Write("Iteration: " + (i + 1).ToString());
                        wt.WriteLine();
                    }
                    wt.Close();
                }
                string[] lines = File.ReadAllLines(saveFileDialog1.FileName);
                for (int i = 0; i < controllerScript.iterations; ++i)
                {
                    int line = i + 2;
                    string currentLine = lines[line];
                    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                    {
                    }
                    lines[line] = currentLine;
                }
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        writer.Write(lines[i]);
                        writer.WriteLine();
                    }
                    writer.Close();
                }
            }
        }

        public void SaveGroupings(List<List<double>> groupings)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.csv)|*.csv";
            sfd.FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Grouping Save";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter wt = new StreamWriter(sfd.FileName))
                {
                    for (int i = 0; i < groupings[0].Count; i++)
                    {
                        wt.Write("State " + (i + 1).ToString() + ",");
                    }
                    wt.WriteLine();
                    for (int i = 0; i < groupings.Count; ++i)
                    {
                        for (int j = 0; j < groupings[i].Count; ++j)
                        {
                            wt.Write(groupings[i][j] + ",");
                        }
                        wt.WriteLine();
                    }
                    wt.Close();
                }
            }
        }

        private void groupingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(controllerScript.AlreadyCA)
            {
                GroupingForm newGrouper = new GroupingForm(this);
                newGrouper.ShowDialog();
            }
        }

        private void runSettingsButton_Click(object sender, EventArgs e)
        {
            saveDialog = new SaveDataDialog();
            saveDialog.Show();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetCA();
        }

        private void ResetCA()
        {
            controllerScript.ResetGrid(this);
            UpdateRunBox();
            UpdateImage();
        }
    }
}
