using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class ImageTrace : Form
    {
        ControllerScript controllerScript = Form1.controllerScript;
        int startX = 4;
        int startY = 7;
        double proportion = 100;
        string done = "Done";
        List<CheckBox> checklist = new List<CheckBox>();
        Bitmap bmp;

        public ImageTrace()
        {
            InitializeComponent();
            AddAgents();
            bmp = new Bitmap(controllerScript.myCA.gridWidth, controllerScript.myCA.gridHeight);
            tracePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void pathTraceRadio_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.pathTraceRadio.Checked)
            //{
            //    DoTrace();
            //}
        }

        private void freqTraceRadio_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.freqTraceRadio.Checked)
            //{
            //    DoHeatMap();
            //}
        }

        void AddAgents()
        {
            int agentCount = controllerScript.myCA.ActiveAgents.Count;
            for(int i = 0; i < agentCount; i++)
            {
                CheckBox newBox = new CheckBox();
                newBox.Name = "agent" + i.ToString();
                newBox.Text = "Agent " + (i+1).ToString();
                newBox.CheckedChanged += new System.EventHandler(checkBox_CheckedChanged);
                this.panel1.Controls.Add(newBox);
                checklist.Add(newBox);
                newBox.SetBounds(startX, startY, 63, 17);
                startY = startY + 23;
            }
        }

        void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.freqTraceRadio.Checked)
            //{
            //    DoHeatMap();
            //}
            //else if (this.pathTraceRadio.Checked)
            //{
            //    DoTrace();
            //}
        }

        void LookForWork()
        {
            if (this.freqTraceRadio.Checked)
            {
                DoHeatMap();
            }
            else if (this.pathTraceRadio.Checked)
            {
                DoTrace();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LookForWork();
        }

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    Console.WriteLine("progress: " + e.ProgressPercentage);
        //    this.progressBar1.Value = e.ProgressPercentage;
        //    //Invoke(new Action(() => UpdateText(e.ProgressPercentage)));
        //}

        void DoHeatMap()
        {
            int currentPercentage = 0;
            List<int> duplicates = new List<int>();
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            int numberOfCheckedAgents = 0;
            int incrementor = 0;
            for (int i = 0; i < checklist.Count; i++)
            {
                numberOfCheckedAgents++;
            }

            for (int i = 0; i < checklist.Count; i++)
            {
                if(checklist[i].Checked)
                {
                    int pathLength = controllerScript.myCA.ActiveAgents[i].History.Count;
                    int maxProgress = numberOfCheckedAgents * ((pathLength * pathLength) + pathLength);
                    int maxDupes = 0;
                    List<int> dupeLocations = new List<int>();
                    List<int> dupeTempLocations = new List<int>();
                    for (int j = 0; j < pathLength; j++)
                    {
                        if (dupeLocations.Contains(j))
                        {
                            int location = dupeLocations.IndexOf(j);
                            duplicates.Add(duplicates[dupeTempLocations[location]]);
                            continue;
                        }
                        int tempDupe = 0;
                        int x = controllerScript.myCA.ActiveAgents[i].History[j].Item1;
                        int y = controllerScript.myCA.ActiveAgents[i].History[j].Item2;
                        Tuple<int, int> tempTuple = new Tuple<int, int>(x, y);

                        for(int k = 0; k < pathLength; k++)
                        {
                            int percentage = (int)((((incrementor * (pathLength * pathLength)) + (j * pathLength) + (k + 1)) / (double)maxProgress) * 100);
                            if (percentage > currentPercentage)
                            {
                                currentPercentage = percentage;
                                backgroundWorker1.ReportProgress(percentage);
                            }
                            if (controllerScript.myCA.ActiveAgents[i].History[k].Equals(tempTuple))
                            {
                                dupeLocations.Add(k);
                                dupeTempLocations.Add(j);
                                tempDupe++;
                            }
                        }
                        duplicates.Add(tempDupe);
                        if (tempDupe > maxDupes)
                        {
                            maxDupes = tempDupe;
                        }
                    }

                    for (int j = 0; j < pathLength; j++)
                    {
                        int percentage = (int)((((incrementor+1) * (pathLength * pathLength) + (j + 1))/ (double)maxProgress) * 100);
                        if (percentage > currentPercentage)
                        {
                            currentPercentage = percentage;
                            backgroundWorker1.ReportProgress(percentage);
                        }
                        double fraction = (duplicates[j] / (double)maxDupes) * 255;
                        int rightColor = Convert.ToInt32(fraction);
                        Color tempColor = Color.FromArgb(rightColor, rightColor, rightColor);
                        bmp.SetPixel(controllerScript.myCA.ActiveAgents[i].History[j].Item1, controllerScript.myCA.ActiveAgents[i].History[j].Item2, tempColor);
                    }
                    incrementor++;
                }
            }
            UpdateImage();
        }

        void DoTrace()
        {
            int currentPercentage = 0;
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            int numberOfCheckedAgents = 0;
            int incrementor = 0;
            for (int i = 0; i < checklist.Count; i++)
            {
                numberOfCheckedAgents++;
            }
                for (int i = 0; i < checklist.Count; i++)
            {
                if (checklist[i].Checked)
                {
                    int pathLength = controllerScript.myCA.ActiveAgents[i].History.Count;
                    int maxProgress = numberOfCheckedAgents * pathLength;
                    for(int j = 0; j < pathLength; j++)
                    {
                        int percentage = (int)((((incrementor * pathLength) + (j + 1)) / (double)maxProgress) * 100);
                        if(percentage > currentPercentage)
                        {
                            currentPercentage = percentage;
                            backgroundWorker1.ReportProgress(percentage);
                        }
                        double fraction = ((j + 1) / (double)pathLength) * 255;
                        int rightColor = Convert.ToInt32(fraction);
                        Color tempColor = Color.FromArgb(rightColor, rightColor, rightColor);
                        bmp.SetPixel(controllerScript.myCA.ActiveAgents[i].History[j].Item1, controllerScript.myCA.ActiveAgents[i].History[j].Item2, tempColor);
                    }
                    incrementor++;
                }
            }
            UpdateImage();
        }

        void UpdateImage()
        {
            this.tracePictureBox.Image = bmp;
        }

        void CreateImage()
        {
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            UpdateImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.bmp)|*.bmp|(*.jpeg)|*.jpeg|(*.png)|*.png|(*.tiff)|*.tiff";
            sfd.FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + " Run " +
                controllerScript.caRuns + " Heatmap/Trace.bmp";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tracePictureBox.Image.Save(sfd.FileName);
            }
        }

        private void ImageTrace_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
    }
}
