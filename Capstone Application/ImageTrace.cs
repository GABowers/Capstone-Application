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
        List<CheckBox> checklist = new List<CheckBox>();
        DirectBitmap bmp;
        TraceOption action = TraceOption.Heatmap;

        public ImageTrace()
        {
            InitializeComponent();
            AddAgents();
            bmp = new DirectBitmap(controllerScript.myCA.gridWidth, controllerScript.myCA.gridHeight);
            tracePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            tracePictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        // how to treat heatmap: collapse across agents, or keep separate (colors or something)?

        private void pathTraceRadio_CheckedChanged(object sender, EventArgs e)
        {
            SetAction();
        }

        private void freqTraceRadio_CheckedChanged(object sender, EventArgs e)
        {
            SetAction();
        }

        void SetAction()
        {
            if(pathTraceRadio.Checked)
            {
                action = TraceOption.Trace;
            }
            else if(freqTraceRadio.Checked)
            {
                action = TraceOption.Heatmap;
            }
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
            Console.WriteLine("Checking");
            // handle adding new agents to the current visualization
        }

        void LookForWork()
        {
            switch(action)
            {
                case TraceOption.Heatmap:
                    DoHeatMap();
                    break;
                case TraceOption.Trace:
                    DoTrace();
                    break;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LookForWork();
        }

        void DoHeatMap()
        {
            int currentPercentage = 0;
            //List<int> duplicates = new List<int>();
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            //int numberOfCheckedAgents = 0;
            //int incrementor = 0;
            List<int> agent_locations = new List<int>();
            List<Tuple<int, int>> locations = new List<Tuple<int, int>>();
            for (int i = 0; i < checklist.Count; i++)
            {
                if(checklist[i].Checked)
                {
                    agent_locations.Add(i);
                }
            }

            for (int i = 0; i < agent_locations.Count; i++)
            {
                locations.AddRange(controllerScript.myCA.ActiveAgents[agent_locations[i]].History.Select(x => new Tuple<int, int>(x.Item1, x.Item2)).ToList());
            }

            List<Tuple<int, int>> unique_locations = locations.Distinct().ToList();
            List<int> location_counts = unique_locations.Select(x => locations.Count(y => y.Equals(x))).ToList();
            double max = location_counts.Max();
            List<double> scaled_values = location_counts.Select(x => x / max).ToList();
            for (int i = 0; i < unique_locations.Count(); i++)
            {
                double fraction = scaled_values[i] * 255;
                int rightColor = Convert.ToInt32(fraction);
                Color tempColor = Color.FromArgb(rightColor, rightColor, rightColor);
                bmp.SetPixel(unique_locations[i].Item1, unique_locations[i].Item2, tempColor);
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
            this.tracePictureBox.Image = bmp.Bitmap;
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

        enum TraceOption
        {
            Heatmap,
            Trace
        }
    }
}
