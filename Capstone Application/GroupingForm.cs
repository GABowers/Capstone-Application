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
    public partial class GroupingForm : Form
    {
        ControllerScript controllerScript = Form1.controllerScript;
        Form1 mainform;
        int timeElapsed = 0;
        double timeRemaining = 0;
        System.Timers.Timer timer1, timer2;
        public GroupingForm(Form1 form)
        {
            InitializeComponent();
            //timer1.Stop();
            //timer2.Stop();
            timer1 = new System.Timers.Timer(1000);
            timer2 = new System.Timers.Timer(1000);
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Tick);
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer1.Stop();
            timer2.Stop();
            mainform = form;
        }

        int NumRuns { get; set; } = 0;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(textBox1.Text, out int result))
            {
                NumRuns = result;
            }
        }

        private void runGetAllGroups_Click(object sender, EventArgs e)
        {
            newBW.RunWorkerAsync();
            UpdateText();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // increments time elapsed
            // update UI
            timeElapsed++;
            this.Invoke(new System.Action(() =>
            {
                UpdateText();
            }));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // update ui
            timeRemaining += 1;
            this.Invoke(new System.Action(() =>
            {
                UpdateText();
            }));
        }

        private void UpdateText()
        {
            TimeSpan time1 = TimeSpan.FromSeconds(timeElapsed);
            TimeSpan time2 = TimeSpan.FromSeconds(timeRemaining);
            string elapsedString = time1.ToString(@"mm\:ss");
            string remainingString = time2.ToString(@"mm\:ss"); ;
            infoLabel.Text = progressBar1.Value.ToString() + "% Time elapsed: " + elapsedString + " Est. remaining: " + remainingString;
        }

        private void newBW_DoWork_1(object sender, DoWorkEventArgs e)
        {
            double inc = (double)100 / NumRuns;
            double trueTotal = 0;
            List<List<double>> groupings = new List<List<double>>();
            DateTime time = DateTime.Now;
            for (int i = 0; i < NumRuns; i++)
            {
                if(i == 1)
                {
                    this.Invoke(new System.Action(() =>
                    {
                        StartTimer();
                    }));
                }
                if (i > 0)
                {
                    int iterations = i;
                    int remIt = 100 - i;
                    DateTime now = DateTime.Now;
                    TimeSpan sub = time - now;
                    double dif = sub.TotalSeconds;
                    double per = dif / iterations;
                    timeRemaining = per * remIt;
                }
                groupings.Add(new List<double>());
                for (int j = 0; j < controllerScript.amountOfCellTypes; j++)
                {
                    groupings[i].Add(controllerScript.myCA.GetCIndex(j));
                }
                controllerScript.ResetGrid(mainform);
                controllerScript.CreateCA(mainform);
                controllerScript.StartCA(mainform);
                //this.Invoke(new System.Action(() =>
                //{
                //    mainform.AutoReset();
                //}));
                trueTotal += inc;
                newBW.ReportProgress((int)trueTotal);
            }

            // think about making this parallel like before
            this.Invoke(new System.Action(() =>
            {
                mainform.SaveGroupings(groupings);
            }));
            groupings.Clear();
        }

        void StartTimer()
        {
            timer2.Start();
        }

        private void newBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void newBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
