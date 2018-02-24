using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class ImageTrace : Form
    {
        ControllerScript controllerScript = Form1.controllerScript;
        int startX = 4;
        int startY = 7;
        string loading = "Loading...";
        string done = "Done";
        List<CheckBox> checklist = new List<CheckBox>();
        Bitmap bmp;

        public ImageTrace()
        {
            InitializeComponent();
            AddAgents();
            bmp = new Bitmap(controllerScript.myCA.gridWidth, controllerScript.myCA.gridHeight);
            tracePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void pathTraceRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(this.pathTraceRadio.Checked)
            {
                this.statusLabel.Text = loading;
                DoTrace();
            }
        }

        private void freqTraceRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(this.freqTraceRadio.Checked)
            {
                this.statusLabel.Text = loading;
                DoHeatMap();
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
            this.statusLabel.Text = loading;
            if (this.freqTraceRadio.Checked)
            {
                DoHeatMap();
            }
            else if(this.pathTraceRadio.Checked)
            {
                DoTrace();
            }
        }

        void DoHeatMap()
        {
            DateTime time = DateTime.Now;
            
            List<int> duplicates = new List<int>();
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            
            for (int i = 0; i < checklist.Count; i++)
            {
                if(checklist[i].Checked)
                {
                    int pathLength = controllerScript.myCA.ActiveAgents[i].History.Count;
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
                            if(controllerScript.myCA.ActiveAgents[i].History[k].Equals(tempTuple))
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
                        
                        double fraction = (duplicates[j] / (double)maxDupes) * 255;
                        int rightColor = Convert.ToInt32(fraction);
                        Color tempColor = Color.FromArgb(rightColor, rightColor, rightColor);
                        bmp.SetPixel(controllerScript.myCA.ActiveAgents[i].History[j].Item1, controllerScript.myCA.ActiveAgents[i].History[j].Item2, tempColor);
                    }
                }
            }
            TimeSpan full = time - DateTime.Now;
            Console.WriteLine(full);
            UpdateImage();
        }

        void DoTrace()
        {
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            for (int i = 0; i < checklist.Count; i++)
            {
                if (checklist[i].Checked)
                {
                    int pathLength = controllerScript.myCA.ActiveAgents[i].History.Count;
                    for(int j = 0; j < pathLength; j++)
                    {
                        double fraction = ((j + 1) / (double)pathLength) * 255;
                        int rightColor = Convert.ToInt32(fraction);
                        Color tempColor = Color.FromArgb(rightColor, rightColor, rightColor);
                        bmp.SetPixel(controllerScript.myCA.ActiveAgents[i].History[j].Item1, controllerScript.myCA.ActiveAgents[i].History[j].Item2, tempColor);
                    }
                }
            }
            UpdateImage();
        }

        void UpdateImage()
        {
            this.tracePictureBox.Image = bmp;
            this.statusLabel.Text = done;
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
    }
}
