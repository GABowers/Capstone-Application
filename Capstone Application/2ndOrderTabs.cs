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
        int xPosition;
        int yPosition;
        int otherXPosition;
        int originalY;
        int state;
        int numStates;
        //int otherYPosition;
        //int randWalkLabelXPos;
        //int randWalkLabelYPos;
        //int randWalkBoxXPos;
        //int randWalkBoxYPos;
        List<string> neighborhoodList = new List<string>();
        public _2ndOrderTabs(int currentState, int amountOfStates)
        {
            InitializeComponent();
            state = currentState;
            numStates = amountOfStates;
            xPosition = randWalkLabelUp.Location.X;
            yPosition = randWalkLabelUp.Location.Y;
            originalY = yPosition;
            otherXPosition = randWalkBoxUp.Location.X;
            //otherYPosition = stickProbTextBox.Location.Y;

            this.Controls.Remove(randWalkLabelUp);
            this.Controls.Remove(randWalkBoxUp);
            this.Dock = DockStyle.Fill;
            mobileNeighborHood.SelectedIndex = 0;
        }

        private void PropagateFields()
        {
            PopulateList();
            AddRands();
            AddCalculateButton();
            UpdateStickFields();
        }

        private void PopulateList()
        {
            neighborhoodList.Clear();
            switch(mobileNeighborHood.SelectedIndex)
            {
                case 0:
                    neighborhoodList.Add("up");
                    neighborhoodList.Add("right");
                    neighborhoodList.Add("down");
                    neighborhoodList.Add("left");
                    break;
                case 1:
                    neighborhoodList.Add("up");
                    neighborhoodList.Add("up-right");
                    neighborhoodList.Add("right");
                    neighborhoodList.Add("right-down");
                    neighborhoodList.Add("down");
                    neighborhoodList.Add("down-left");
                    neighborhoodList.Add("left");
                    neighborhoodList.Add("left-up");
                    break;
            }
        }

        private void AddRands()
        {
            for (int i = 0; i < neighborhoodList.Count; i++)
            {
                TextBox textBox = new TextBox { Location = new System.Drawing.Point(otherXPosition, yPosition), Name = ("RandWalkBox" + neighborhoodList[i]), Height = 20, Width = 121, };
                Label qweLabel = new Label { Location = new System.Drawing.Point(xPosition, yPosition), Name = ("RandWalkLabel" + neighborhoodList[i]), AutoSize = false, Height = 25, Width = 438, Text = string.Format("Random walk ({0})", neighborhoodList[i]) };
                Controls.Add(qweLabel);
                Controls.Add(textBox);
                yPosition += 30;
            }
        }

        private void AddCalculateButton()
        {
            TextBox downProb = this.Controls["RandWalkBoxdown"] as TextBox;
            int downBoxY = downProb.Location.Y;
            calculateButton.Location = new System.Drawing.Point(otherXPosition - 101, downBoxY);
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
            TextBox downProb = this.Controls["RandWalkBoxdown"] as TextBox;
            if (double.TryParse(downProb.Text, out double result))
            {
                double remaining = 1 - double.Parse(downProb.Text);
                int otherDirections = neighborhoodList.Count - 1;
                double eachWalk = (remaining / otherDirections);
                for (int i = 0; i < neighborhoodList.Count; i++)
                {
                    if(neighborhoodList[i] == "down")
                    {
                        continue;
                    }
                    TextBox tempBox = this.Controls["RandWalkBox" + neighborhoodList[i]] as TextBox;
                    tempBox.Text = eachWalk.ToString();
                }
            }
        }

        public void UpdateValues(StatePageInfo info, int currentState)
        {
            colorBox.BackColor = info.color;

            mobileNeighborHood.SelectedIndex = info.mobileNeighborhood;

            agentCount.Text = info.startingAmount.ToString();

            for (int i = 0; i < neighborhoodList.Count; i++)
            {
                TextBox tempBox = this.Controls["RandWalkBox" + neighborhoodList[i]] as TextBox;
                tempBox.Text = info.walkProbs[i].ToString(); ;
            }

            //FIX
                for (int i = 0; i < info.probs.GetLength(0); i++)
                {
                    string name = currentState + "." + i;
                    //info.stickingProbs[i] = double.Parse(this.Controls[name].Text);
                    this.Controls[name].Text = info.stickingProbs[i].ToString();
                }
        }

        private void UpdateStickFields()
        {
            for (int i = 0; i < numStates; i++)
            {
                int otherState = i + 1;
                TextBox textBox = new TextBox { Location = new System.Drawing.Point(otherXPosition, yPosition), Name = (state + "." + otherState), Height = 20, Width = 121, };
                //Console.WriteLine("Adding: " + (state + "." + otherState));
                Label qweLabel = new Label { Location = new System.Drawing.Point(xPosition, yPosition), Name = ("StickLabel" + otherState), AutoSize = false, Height = 25, Width = 438, Text = string.Format("Sticking probability (to state {0})", otherState) };
                Controls.Add(qweLabel);
                Controls.Add(textBox);
                yPosition += 30;
                }
        }

        public void SetValues(StatePageInfo info, int currentState)
        {
            info.caType = 1;
            info.color = colorBox.BackColor;
            info.mobileNeighborhood = this.mobileNeighborHood.SelectedIndex;
            if (string.IsNullOrWhiteSpace(agentCount.Text))
            {
                info.startingAmount = 0;
            }
            else
            {
                info.startingAmount = int.Parse(agentCount.Text);
            }

            info.SetWalkProbs(neighborhoodList.Count);

            for (int i = 0; i < neighborhoodList.Count; i++)
            {
                TextBox tempBox = this.Controls["RandWalkBox" + neighborhoodList[i]] as TextBox;
                if (double.TryParse(tempBox.Text, out double result))
                {
                    info.walkProbs[i] = double.Parse(tempBox.Text);
                }
                else
                {
                    info.walkProbs[i] = 0;
                }
            }

            for (int i = 0; i < info.probs.GetLength(0); i++)
            {
                string name = currentState + "." + (i+1).ToString();
                TextBox currentControl = this.Controls[name] as TextBox;
                if (string.IsNullOrWhiteSpace(currentControl.Text))
                {
                    info.stickingProbs.Add(0);
                }
                else
                {
                    if (double.TryParse(currentControl.Text, out double result5))
                    {
                        info.sticking = true;
                        info.stickingProbs.Add(double.Parse(currentControl.Text));
                    }
                }
            }
        }

        private void mobileNeighborHood_SelectedValueChanged(object sender, EventArgs e)
        {
            
            yPosition = originalY;
            for (int i = 0; i < neighborhoodList.Count; i++)
            {
                TextBox tempBox = this.Controls[("RandWalkBox" + neighborhoodList[i])] as TextBox;
                Label tempLabel = this.Controls[("RandWalkLabel" + neighborhoodList[i])] as Label;
                this.Controls.Remove(tempBox);
                this.Controls.Remove(tempLabel);
            }
            for (int i = 0; i < numStates; i++)
            {
                //Console.WriteLine("Firing!");
                TextBox tempBox = this.Controls[(state + "." + (i + 1))] as TextBox;
                //Console.WriteLine("Searching: " + (state + "." + (i + 1)));
                Label tempLabel = this.Controls[("StickLabel" + (i + 1))] as Label;
                this.Controls.Remove(tempBox);
                this.Controls.Remove(tempLabel);
            }
            PropagateFields();
        }
    }
}
