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
        bool generateColors = true;
        //https://stackoverflow.com/questions/470690/how-to-automatically-generate-n-distinct-colors
        List<Color> colors = new List<Color>
        {
            UIntToColor(0xFFFFB300), //Vivid Yellow
            UIntToColor(0xFF803E75), //Strong Purple
            UIntToColor(0xFFFF6800), //Vivid Orange
            UIntToColor(0xFFA6BDD7), //Very Light Blue
            UIntToColor(0xFFC10020), //Vivid Red
            UIntToColor(0xFFCEA262), //Grayish Yellow
            UIntToColor(0xFF817066), //Medium Gray

            //The following will not be good for people with defective color vision
            UIntToColor(0xFF007D34), //Vivid Green
            UIntToColor(0xFFF6768E), //Strong Purplish Pink
            UIntToColor(0xFF00538A), //Strong Blue
            UIntToColor(0xFFFF7A5C), //Strong Yellowish Pink
            UIntToColor(0xFF53377A), //Strong Violet
            UIntToColor(0xFFFF8E00), //Vivid Orange Yellow
            UIntToColor(0xFFB32851), //Strong Purplish Red
            UIntToColor(0xFFF4C800), //Vivid Greenish Yellow
            UIntToColor(0xFF7F180D), //Strong Reddish Brown
            UIntToColor(0xFF93AA00), //Vivid Yellowish Green
            UIntToColor(0xFF593315), //Deep Yellowish Brown
            UIntToColor(0xFFF13A13), //Vivid Reddish Orange
            UIntToColor(0xFF232C16), //Dark Olive Green
        };
        private static readonly List<Color> _boyntonOptimized = new List<Color>
        {
            Color.FromArgb(0, 0, 255),      //Blue
            Color.FromArgb(255, 0, 0),      //Red
            Color.FromArgb(0, 255, 0),      //Green
            Color.FromArgb(255, 255, 0),    //Yellow
            Color.FromArgb(255, 0, 255),    //Magenta
            Color.FromArgb(255, 128, 128),  //Pink
            Color.FromArgb(128, 128, 128),  //Gray
            Color.FromArgb(128, 0, 0),      //Brown
            Color.FromArgb(255, 128, 0),    //Orange
        };
        List<CheckBox> checklist = new List<CheckBox>();
        DirectBitmap bmp;
        TraceOption action = TraceOption.Heatmap;
        struct HSV { public float h; public float s; public float v; };

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
            int agentCount = controllerScript.Paths.Count + controllerScript.myCA.ActiveAgents.Count;
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
            //List<int> duplicates = new List<int>();
            for (int i = 0; i < controllerScript.myCA.gridWidth; i++)
            {
                for (int j = 0; j < controllerScript.myCA.gridHeight; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }
            List<int> agent_locations = new List<int>();
            List<Tuple<int, int, int>> locations = new List<Tuple<int, int, int>>();
            for (int i = 0; i < checklist.Count; i++)
            {
                if(checklist[i].Checked)
                {
                    agent_locations.Add(i);
                }
            }
            List<List<Tuple<int, int, int>>> all_agents = controllerScript.Paths;
            all_agents.AddRange(controllerScript.myCA.GetPaths());
            for (int i = 0; i < agent_locations.Count; i++)
            {
                locations.AddRange(all_agents[i]);
            }
            List<int> states = locations.Select(x => x.Item3).ToList();
            
            List<int> unique_states = states.Distinct().ToList();
            List<Tuple<int, int>> already_colored = new List<Tuple<int, int>>();
            for (int i = 0; i < unique_states.Count; i++)
            {
                List<Tuple<int, int, int>> unique_locations = locations.Where(x => x.Item3.Equals(i)).Select(y => new Tuple<int, int, int>(y.Item1, y.Item2, i)).Distinct().ToList();
                List<int> location_counts = unique_locations.Select(x => locations.Count(y => y.Equals(x))).ToList();
                double max = location_counts.Max();
                List<double> scaled_values = location_counts.Select(x => x / max).ToList();
                for (int j = 0; j < unique_locations.Count(); j++)
                {
                    double fraction = scaled_values[j] * 255;
                    int rightColor = Convert.ToInt32(fraction);
                    Color tempColor = Color.FromArgb(rightColor, rightColor, rightColor);
                    Tuple<int, int> cur_loc = new Tuple<int, int>(unique_locations[j].Item1, unique_locations[j].Item2);
                    if (already_colored.Contains(cur_loc))
                    {
                        Blend(cur_loc, tempColor);
                    }
                    else
                    {
                        bmp.SetPixel(unique_locations[j].Item1, unique_locations[j].Item2, tempColor);
                        already_colored.Add(new Tuple<int, int>(unique_locations[j].Item1, unique_locations[j].Item2));
                    }
                    
                }
            }
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
            List<int> agent_locations = new List<int>();
            for (int i = 0; i < checklist.Count; i++)
            {
                if (checklist[i].Checked)
                {
                    agent_locations.Add(i);
                }
            }
            List<Tuple<int, int>> already_colored = new List<Tuple<int, int>>();
            for (int i = 0; i < agent_locations.Count; i++)
            {
                List<Tuple<int, int, int>> history = controllerScript.Paths[agent_locations[i]];
                List<Tuple<int, int, int>> unique_history = history.Distinct().ToList();
                List<Tuple<int, int>> unique_history_locations = unique_history.Select(x => new Tuple<int, int>(x.Item1, x.Item2)).ToList();
                for (int j = 0; j < unique_history.Count; j++)
                {
                    Tuple<int, int> cur_loc = new Tuple<int, int>(unique_history[j].Item1, unique_history[j].Item2);
                    double alpha = Convert.ToDouble(j + 1) / unique_history.Count();
                    Color cur_color = generateColors? _boyntonOptimized[i] : controllerScript.colors[unique_history[j].Item3];
                    Color new_color = Color.FromArgb((int)(cur_color.R * alpha), (int)(cur_color.G * alpha), (int)(cur_color.B * alpha));
                    if (already_colored.Contains(cur_loc))
                    {
                        Blend(cur_loc, new_color);
                    }
                    else
                    {
                        bmp.SetPixel(cur_loc.Item1, cur_loc.Item2, new_color);
                        already_colored.Add(new Tuple<int, int>(unique_history[j].Item1, unique_history[j].Item2));
                    }
                }
            }
            UpdateImage();
        }

        float getBrightness(Color c)
        {
            return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f;
        }

        void Blend(Tuple<int, int> loc, Color next)
        {
            // convert to HSL, combine H
            Color cur_color = bmp.GetPixel(loc.Item1, loc.Item2);
            HSV cur_hsv = new HSV() { h = cur_color.GetHue(), s = cur_color.GetSaturation(), v = getBrightness(cur_color) };
            HSV new_hsv = new HSV() { h = next.GetHue(), s = next.GetSaturation(), v = getBrightness(next) };
            HSV combined = new HSV() { h = (cur_hsv.h + new_hsv.h)/2.0f, s = (cur_hsv.s + new_hsv.s) / 2.0f , v = (cur_hsv.v + new_hsv.v) / 2.0f };
            Color new_color = ColorFromHSL(combined);
            bmp.SetPixel(loc.Item1, loc.Item2, new_color);
        }

        // HSV stuff from https://stackoverflow.com/questions/28759764/c-sharp-sethue-or-alternatively-convert-hsl-to-rgb-and-set-rgb

        Color ColorFromHSL(HSV hsl)
        {
            if(hsl.s == 0)
            {
                int L = (int)hsl.v;
                return Color.FromArgb(255, L, L, L);
            }

            double min, max, h;
            h = hsl.h / 360.0;
            max = hsl.v < 0.5 ? hsl.v * (1 + hsl.s) : (hsl.v + hsl.s) - (hsl.v * hsl.s);
            min = (hsl.v * 2.0) - max;

            Color c = Color.FromArgb(255, (int)(255 * RGBChannelFromHue(min, max, h + 1 / 3.1)), (int)(255 * RGBChannelFromHue(min, max, h)), (int)(255 * RGBChannelFromHue(min, max, h - 1 / 3.1)));
            return c;
        }

        double RGBChannelFromHue(double m1, double m2, double h)
        {
            h = (h + 1.0) % 1.0;
            if (h < 0)
            {
                h += 1;
            }
            if (h * 6 < 1)
            {
                return m1 + (m2 - m1) * 6 * h;
            }
            else if(h * 2 < 1)
            {
                return m2;
            }
            else if(h * 3 < 2)
            {
                return m1 + (m2 - m1) * 6 * (2.0 / 3.0 - h);
            }
            else
            {
                return m1;
            }
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
                controllerScript.caRuns + " Heatmap-Trace.bmp";
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

        static public Color UIntToColor(uint color)
        {
            var a = (byte)(color >> 24);
            var r = (byte)(color >> 16);
            var g = (byte)(color >> 8);
            var b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }

        enum TraceOption
        {
            Heatmap,
            Trace
        }
    }
}
