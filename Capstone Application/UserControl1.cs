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
    public partial class UserControl1 : UserControl
    {
        int xPosition;
        int yPosition;
        int otherXPosition;
        int otherYPosition;
        public UserControl1()
        {
            InitializeComponent();
            xPosition = probTextField.Location.X;
            yPosition = probTextField.Location.Y;
            otherXPosition = probInputField.Location.X;
            otherYPosition = probInputField.Location.Y;
            this.Dock = DockStyle.Fill;
            this.Controls.Remove(probTextField);
            this.Controls.Remove(probInputField);
            //Console.WriteLine(xPosition);
            //Console.WriteLine(yPosition);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //Show color dialog
            DialogResult result = colorDialog1.ShowDialog();
            //see if user pressed ok
            if (result == DialogResult.OK)
            {
                colorBox.BackColor = colorDialog1.Color;
            }
        }

        public void UpdateProbFields(int currentState, int amountOfStates, int neighborCount)
        {
            for (int i = 0; i < amountOfStates; i++)
            {
                int otherState = i + 1;
                if (otherState == currentState)
                    continue;
                for (int neighborState = 0; neighborState < amountOfStates; neighborState++)
                {
                    int currentNeighborState = neighborState + 1;
                    for (int neighbors = 0; neighbors < (neighborCount + 1); neighbors++)
                    {
                        TextBox textBox = new TextBox { Location = new System.Drawing.Point(otherXPosition, otherYPosition), Name = (currentState + "." + i + "." + neighborState + "." + neighbors), Height = 20, Width = 121, };
                        Label qweLabel = new Label { Location = new System.Drawing.Point(xPosition, yPosition), AutoSize = false, Height = 25, Width = 450, Text = string.Format("Probability of change from State {0} to State {1}, with {2} neighbors of type {3} (0-1 value)", currentState, otherState, neighbors, currentNeighborState) };
                        Controls.Add(qweLabel);
                        Controls.Add(textBox);
                        yPosition += 30;
                        otherYPosition += 30;
                    }
                }
            }
        }

        public void UpdateValues(StatePageInfo info, int currentState)
        {
            colorBox.BackColor = info.color;
            if (info.startingAmount.HasValue)
                agentCount.Text = info.startingAmount.ToString();
            for (int i = 0; i < info.probs.GetLength(0); i++)
            {
                int otherState = i + 1;
                if (otherState == currentState)
                    continue;
                for (int neighborState = 0; neighborState < info.probs.GetLength(1); neighborState++)
                {
                    int currentNeighborState = neighborState + 1;
                    for (int neighbors = 0; neighbors < info.probs.GetLength(2); neighbors++)
                    {
                        string name = currentState + "." + i + "." + neighborState + "." + neighbors;
                        //if (info.probs[i, neighborState, neighbors] != null)
                            this.Controls[name].Text = info.probs[i, neighborState, neighbors].ToString();
                    }
                }
            }
        }

        public void SetValues(StatePageInfo info, int currentState)
        {
            info.color = colorBox.BackColor;
            //Maybe check that the number is valid first?
            if(int.TryParse(agentCount.Text, out int result))
            {
                info.startingAmount = int.Parse(agentCount.Text);
            }
            else
            {
                info.startingAmount = 0;
            }
            for (int i = 0; i < info.probs.GetLength(0); i++)
            {
                int otherState = i + 1;
                if (otherState == currentState)
                    continue;
                for (int neighborState = 0; neighborState < info.probs.GetLength(1); neighborState++)
                {
                    int currentNeighborState = neighborState + 1;
                    for (int neighbors = 0; neighbors < info.probs.GetLength(2); neighbors++)
                    {
                        string name = currentState + "." + i + "." + neighborState + "." + neighbors;
                        if (string.IsNullOrWhiteSpace(this.Controls[name].Text))
                        {
                            info.probs[i, neighborState, neighbors] = 0;
                        }
                        else
                        {
                            info.probs[i, neighborState, neighbors] = float.Parse(this.Controls[name].Text);
                        }
                    }
                }
            }
        }
    }
}
