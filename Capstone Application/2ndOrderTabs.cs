using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public partial class _2ndOrderTabs : UserControl
    {
        public _2ndOrderTabs()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void colorBox_Click(object sender, EventArgs e)
        {
            //Show color dialog
            DialogResult result = colorDialog1.ShowDialog();
            //see if user pressed ok
            if (result == DialogResult.OK)
            {
                colorBox.BackColor = colorDialog1.Color;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // subtract 1 by value in "down" box, divide by 3, then put that in all other boxes
            if (double.TryParse(this.walkDownBox.Text, out double result))
            {
                double remaining = 1 - double.Parse(walkDownBox.Text);
                double eachWalk = (remaining / 3);
                this.walkLeftBox.Text = eachWalk.ToString();
                this.walkRightBox.Text = eachWalk.ToString();
                this.walkUpBox.Text = eachWalk.ToString();
            }
        }

        public void SetValues(StatePageInfo info, int currentState)
        {
            info.caType = 1;
            info.color = colorBox.BackColor;
            if (string.IsNullOrWhiteSpace(agentCount.Text))
            {
                info.startingAmount = 0;
            }
            else
            {
                info.startingAmount = int.Parse(agentCount.Text);
            }

            // This will need changed when adding ability to move diagonally

            if (double.TryParse(this.walkUpBox.Text, out double result1))
            {
                info.walkProbs[0] = double.Parse(this.walkUpBox.Text);
            }

            if (double.TryParse(this.walkRightBox.Text, out double result2))
            {
                info.walkProbs[1] = double.Parse(this.walkRightBox.Text);
            }

            if (double.TryParse(this.walkDownBox.Text, out double result3))
            {
                info.walkProbs[2] = double.Parse(this.walkDownBox.Text);
            }

            if (double.TryParse(this.walkLeftBox.Text, out double result4))
            {
                info.walkProbs[3] = double.Parse(this.walkLeftBox.Text);
            }

            if(string.IsNullOrWhiteSpace(this.stickingBox.Text))
            {
                info.sticking = false;
            }
            else
            {
                info.sticking = true;
                if (double.TryParse(this.stickingBox.Text, out double result5))
                {
                    info.stickingProb = double.Parse(this.stickingBox.Text);
                }
            }
        }
    }
}
