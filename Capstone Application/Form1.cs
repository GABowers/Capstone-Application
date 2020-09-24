using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private bool running;
        bool Running
        {
            get
            {
                return running;
            }
            set
            {
                running = value;
                if(IsHandleCreated)
                {
                    Invoke(new Action(() => UpdateRunning()));
                }
            }
        }
        int mouseDownX = 0;
        int mouseDownY = 0;
        int iterationSpeed = 0;
        int? runMax;
        public static RunSettings runSettings;
        public static ControllerScript controllerScript = new ControllerScript();
        Counter counterWindow;
        EditWindow editWindow;
        public PixelBox innerPictureBox;

        //System.Timers.Timer edit_timer;
        //SaveDataDialog saveDialog;
        System.Timers.Timer text_timer = new System.Timers.Timer();

        public int? RunMax { get => runMax; set => runMax = value; }

        public Form1()
        {
            InitializeComponent();
            Running = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            innerPictureBox = new PixelBox();
            this.innerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerPictureBox.Location = new System.Drawing.Point(0, 0);
            this.innerPictureBox.Name = "pictureBox";
            this.innerPictureBox.Size = new System.Drawing.Size(600, 600);
            this.innerPictureBox.TabIndex = 0;
            this.innerPictureBox.TabStop = false;
            this.panel1.Controls.Add(this.innerPictureBox);

            innerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            innerPictureBox.MouseDown += innerPictureBox_MouseDown;
            innerPictureBox.MouseUp += innerPictureBox_MouseUp;
            locationTT = new ToolTip();
            text_timer.Interval = 5;
            text_timer.Elapsed += Timer_Tick;
        }

        private void UpdateRunning()
        {
            PauseButton.Text = running ? "Pause" : "Unpause";
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if(this != null)
            {
                Invoke(new Action(() => UpdateIterationBox()));
            }
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

        private void dansNeighborAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            StartCA();
        }

        public void StartCA()
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
            // load program settings
        }

        void toolStripLabel2_Click(object sender, EventArgs e)
        {
            PauseUnpauseCA();
        }

        public void PauseUnpauseCA()
        {
            if(controllerScript.CreatedCA)
            {
                Running = !Running;
                if (Running == true)
                {
                    backgroundWorker1.RunWorkerAsync();
                    text_timer.Enabled = true;
                    //text_timer.Start();
                }
                else if(Running == false)
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
            double totalDuration = 0;
            while (Running == true)
            {
                DateTime start = DateTime.Now;
                if(iterationSpeed == 0)
                {
                    RunCA();
                    controllerScript.CheckSettings(this);
                }
                else if(iterationSpeed > 0)
                {
                    DateTime time_start = DateTime.Now;
                    RunCA();
                    controllerScript.CheckSettings(this);
                    DateTime time_end = DateTime.Now;
                    System.Threading.Thread.Sleep(Math.Max((iterationSpeed - Convert.ToInt32((time_end - time_start).TotalMilliseconds)), 0));
                }
                totalDuration += (DateTime.Now - start).TotalSeconds;
                if(totalDuration > (1/60.0))
                {
                    // update
                    Invoke(new Action(() => UpdateImage()));
                    Invoke(new Action(() => UpdateIterationBox()));
                    totalDuration = 0;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                        controllerScript.EditGrid(mouseUpX, mouseUpY, innerPictureBox, 0, editWindow.EditState);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        controllerScript.EditGrid(mouseUpX, mouseUpY, innerPictureBox, 1, editWindow.EditState);
                    }
                }
                else if (mouseDownX != mouseUpX && mouseDownY != mouseUpY)
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
                    Console.WriteLine(distanceX + "," + distanceY);
                    if (e.Button == MouseButtons.Left)
                    {
                        controllerScript.EditGrid(rangeX, rangeY, innerPictureBox, 0, editWindow.EditState);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        controllerScript.EditGrid(rangeX, rangeY, innerPictureBox, 1, editWindow.EditState);
                    }
                }
                UpdateImage();
            }
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

        public void AutoPathSave(string time, string folder_path, bool addToName)
        {
            var paths = controllerScript.Paths;
            paths.AddRange(controllerScript.myCA.GetPaths());
            string countName = time + " Iteration " + controllerScript.iterations + " Agent Paths.csv";
            string fileName = folder_path;
            if (addToName)
            {
                fileName += "/" + countName;
            }
            string agent = "Agent ";
            using (StreamWriter wt = new StreamWriter(fileName))
            {
                wt.Write("Date,Runs,Iterations");
                wt.WriteLine();
                string thing = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + "," + controllerScript.caRuns + "," + controllerScript.iterations;
                wt.Write(thing);
                wt.WriteLine();
                wt.Write("Iteration,");
                List<int> lengths = paths.Select(x => x.Count).ToList();
                int max = lengths.Max();
                for (int i = 0; i < paths.Count; i++)
                {
                    wt.Write(agent + (i + 1).ToString() + "," + ",");
                }
                wt.WriteLine();
                for (int i = 0; i < max; i++)
                {
                    wt.Write(i + ",");
                    for (int j = 0; j < paths.Count; j++)
                    {
                        if(paths[j].Count > i)
                        {
                            wt.Write(paths[j][i].Item1 + "," + paths[j][i].Item2 + ",");
                        }
                        else
                        {
                            wt.Write("," + ",");
                        }
                    }
                    wt.WriteLine();
                }
                wt.Close();
            }
        }

        public void SaveData(string time, bool counts, bool trans, bool cIndex, string path)
        {
            if(string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                path = path + "/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + " Runs " + controllerScript.caRuns + " Iterations " + controllerScript.iterations + " Data.csv";
            }
            //Console.WriteLine("Path: " + path);
            List<int> iterations = new List<int>();
            if(counts)
            {
                iterations.AddRange(controllerScript.FullCount.Select(x => x.Item1));
            }
            if (trans)
            {
                iterations.AddRange(controllerScript.FullTransitions.Select(x => x.Item1));
            }
            if (cIndex)
            {
                iterations.AddRange(controllerScript.FullIndex.Select(x => x.Item1));
            }

            iterations = iterations.Distinct().ToList();
            iterations.Sort();
            using (StreamWriter wt = new StreamWriter(path))
            {
                //pertinent info - run, iteration, time
                wt.Write("Date,Run,Iterations");
                wt.WriteLine();
                string thing = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + "," + controllerScript.caRuns + "," + controllerScript.iterations;
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
                        for (int k = 0; k < controllerScript.amountOfCellTypes; k++)
                        {
                            if(j == k)
                            {
                                continue;
                            }
                            string write = "Transitions " + (j + 1).ToString() + " ->" + (k + 1).ToString();
                            wt.Write(write + ",");
                        }
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
                for (int i = 0; i < iterations.Count; ++i)
                {
                    wt.Write((iterations[i]).ToString() + ",");
                    wt.WriteLine();
                }
                wt.Close();
            }
            string[] lines = File.ReadAllLines(path);
            int count_val = 0;
            int trans_val = 0;
            int index_val = 0;
            for (int i = 0; i < iterations.Count; ++i)
            {
                int line = i + 3;
                string currentLine = lines[line];
                string[] currentLineArray = currentLine.Split(',');
                int it = int.Parse(currentLineArray[0]);
                if (counts)
                {
                    List<Tuple<int, List<int>>> local_count = controllerScript.FullCount;
                    if (count_val <= local_count.Count - 1)
                    {
                        if (local_count[count_val].Item1 == it)
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
                }
                if (trans)
                {
                    List<Tuple<int, List<int>>> local_trans = controllerScript.FullTransitions;
                    if (trans_val <= local_trans.Count - 1)
                    {
                        int localTrans = 0;
                        if (local_trans[trans_val].Item1 == it)
                        {
                            localTrans = trans_val;
                            trans_val += 1;
                        }
                        else
                        {
                            localTrans = trans_val - 1;
                        }
                        int val = 0;
                        for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                        {
                            for (int k = 0; k < controllerScript.amountOfCellTypes; k++)
                            {
                                if(j == k)
                                {
                                    continue;
                                }
                                currentLine += local_trans[localTrans].Item2[val].ToString() + ",";
                                val++;
                            }
                        }
                    }
                }
                if (cIndex)
                {
                    List<Tuple<int, List<double>>> local_index = controllerScript.FullIndex;
                    if (index_val <= local_index.Count- 1)
                    {
                        if (local_index[index_val].Item1 == it)
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

        private void visualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageTrace showImageTrace = new ImageTrace();
            showImageTrace.ShowDialog();
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
            if(int.TryParse(speedInput.Text, out int speed))
            {
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
            controllerScript.ResetRuns(1);
            UpdateRunBox();
        }
        
        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveData(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff"), runSettings.SaveCounts, runSettings.SaveTrans, runSettings.SaveIndex, saveFileDialog1.FileName);
                //using (StreamWriter wt = new StreamWriter(saveFileDialog1.FileName))
                //{
                //    //pertinent info - run, iteration, time
                //    string thing = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + " Runs: " + controllerScript.caRuns + " Iterations: " + controllerScript.iterations;
                //    wt.Write(thing);
                //    wt.WriteLine();
                //    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                //    {
                //        string cellTypeString = (j + 1).ToString();
                //        wt.Write("Type " + cellTypeString);
                //    }
                //    wt.WriteLine();
                //    for (int i = 0; i < controllerScript.iterations; ++i)
                //    {
                //        wt.Write("Iteration: " + (i + 1).ToString());
                //        wt.WriteLine();
                //    }
                //    wt.Close();
                //}
                //string[] lines = File.ReadAllLines(saveFileDialog1.FileName);
                //for (int i = 0; i < controllerScript.iterations; ++i)
                //{
                //    int line = i + 2;
                //    string currentLine = lines[line];
                //    for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                //    {

                //    }
                //    lines[line] = currentLine;
                //}
                //using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName, false))
                //{
                //    for (int i = 0; i < lines.Length; i++)
                //    {
                //        writer.Write(lines[i]);
                //        writer.WriteLine();
                //    }
                //    writer.Close();
                //}
            }
        }

        private void runSettingsButton_Click(object sender, EventArgs e)
        {
            if(runSettings != null)
            {
                if (!Running)
                {
                    SaveDataDialog saveDialog = new SaveDataDialog();
                    saveDialog.Show();
                }

            }
            else
            {
                MessageBox.Show("Create a CA Model first.");
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if(runSettings != null)
            {
                ResetCA();
            }
        }

        public void ResetCA()
        {
            controllerScript.ResetGrid(this);
            controllerScript.CreateCA(this);
            controllerScript.StartCA(this);
            UpdateRunBox();
            UpdateIterationBox();
            UpdateImage();
        }

        public void SaveHist(Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>, List<Tuple<int, int>>> input, string time, string path, List<int> finalIterations, int runs)
        {
            List<Tuple<int, int>> ends = input.Item1;
            List<Tuple<int, int>> x_bins = input.Item2;
            List<Tuple<int, int>> y_bins = input.Item3;
            int max = Math.Max(Math.Max(ends.Count, x_bins.Count), y_bins.Count);
            if (string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                path = path + "/" + time + " Runs " + runs + " Iterations " + controllerScript.iterations + " Final Location Histograms.csv";
            }
            using (StreamWriter wt = new StreamWriter(path))
            {
                //pertinent info - run, iteration, time
                wt.WriteLine("Final Positions Histograms");
                wt.WriteLine("Date,Runs");
                string thing = time + "," + runs;
                wt.WriteLine(thing);
                wt.WriteLine("Final Iteration, Final Position, , X Axis Histogram, , Y Axis Histogram");
                wt.WriteLine("Iteration, Position (X), Position (Y), Position (X), Count (X), Position (Y), Count (Y)");
                for (int i = 0; i < max; i++)
                {
                    string iteration = (finalIterations.Count > i) ? finalIterations[i].ToString() : "";
                    string end_x = (ends.Count > i) ? ends[i].Item1.ToString() : "";
                    string end_y = (ends.Count > i) ? ends[i].Item2.ToString() : "";
                    string bin_x = (x_bins.Count > i) ? x_bins[i].Item1.ToString() + "," + x_bins[i].Item2.ToString() : ",";
                    string bin_y = (y_bins.Count > i) ? y_bins[i].Item1.ToString() + "," + y_bins[i].Item2.ToString() : ",";
                    wt.WriteLine(iteration + "," + end_x + "," + end_y + "," + bin_x + "," + bin_y);
                }
                //for (int i = 0; i < ends.Count; i++)
                //{
                //    wt.Write(finalIterations[i] + "," + ends[i].Item1 + "," + ends[i].Item2);
                //    if (x_num > i)
                //    {
                //        wt.Write("," + x_bins[i].Item1 + "," + x_bins[i].Item2);
                //    }
                //    if (y_num > i)
                //    {
                //        wt.Write("," + y_bins[i].Item1 + "," + y_bins[i].Item2);
                //    }
                //    wt.WriteLine();
                //}
                //if (x_greater > 0)
                //{
                //    for (int i = ends.Count; i < x_bins.Count; i++)
                //    {
                //        wt.Write(",," + y_bins[i].Item1 + "," + y_bins[i].Item2);
                //        if (y_num > i)
                //        {
                //            wt.Write("," + y_bins[i].Item1 + "," + y_bins[i].Item2);
                //        }
                //        wt.WriteLine();
                //    }
                //}
                //if(y_greater > 0)
                //{
                //    for (int i = ends.Count + x_bins.Count; i < y_bins.Count; i++)
                //    {
                //        wt.Write(",," + y_bins[i].Item1 + "," + y_bins[i].Item2);
                //        wt.WriteLine();
                //    }
                //}        
                wt.Close();
            }
        }

        private void traceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageTrace showImageTrace = new ImageTrace();
            showImageTrace.ShowDialog();
        }

        private void editGridButton_Click(object sender, EventArgs e)
        {
            if (controllerScript.AlreadyCA)
            {
                if (Running == false)
                {
                    if (controllerScript.editModeOn == false)
                    {
                        editWindow = new EditWindow(this);
                        editWindow.Show();
                    }
                    else if (controllerScript.editModeOn == true)
                    {
                        editWindow.Close();
                        editWindow.Dispose();
                    }
                }
            }
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog() { Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AutoPathSave(System.DateTime.Now.ToString("o").Replace(':', '.'), ofd.FileName, false);
            }
        }

        private void finalLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog() { Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                controllerScript.GetHist(System.DateTime.Now.ToString("o").Replace(':', '.'), this, ofd.FileName);
            }
        }

        private void resetRunsButton_Click(object sender, EventArgs e)
        {
            if (runSettings != null)
            {
                ResetCA();
                controllerScript.ResetRuns(1);
                UpdateRunBox();
                UpdateIterationBox();
                UpdateImage();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
