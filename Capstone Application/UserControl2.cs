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
    public partial class UserControl2 : UserControl
    {
        int neighborStateXPosition = 0;
        
        int toStateXPosition = 0;
        int toStateYPosition = 0;
        public UserControl2()
        {
            InitializeComponent();
            Console.WriteLine(toStateYPosition);
            this.Dock = DockStyle.Fill;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        public void UpdateProbFields(int currentState, int amountOfStates)
        {
            for (int otherState = 0; otherState < amountOfStates; otherState++)
            {
                int neighborStateYPosition = 25;
                int currentOtherState = otherState + 1;

                //unlikely but possible - situations where one would set this to a value
                //rather than keeping blank vVv   Just consider it...
                if (currentOtherState == currentState)
                    continue;

                To_State_Panel nextStatePanel = new To_State_Panel();
                nextStatePanel.toStateLabel.Name = "StatePanel" + otherState;
                nextStatePanel.toStateLabel.Text = "To State " + currentOtherState + ", with neighbors of:";
                this.fullPanel.Controls.Add(nextStatePanel);
                nextStatePanel.Location = new System.Drawing.Point(toStateXPosition, toStateYPosition);

                for (int neighborState = 0; neighborState < amountOfStates; neighborState++)
                {
                    int currentNeighborState = neighborState + 1;
                    Neighbor_State_Entry nextNeighborPanel = new Neighbor_State_Entry();
                    nextNeighborPanel.neighborStateLabel.Name = "NeighborPanel" + neighborState;
                    nextNeighborPanel.neighborStateLabel.Text = "State " + currentNeighborState;
                    nextStatePanel.Controls.Add(nextNeighborPanel);
                    nextNeighborPanel.Location = new System.Drawing.Point(neighborStateXPosition, neighborStateYPosition);
                    Console.WriteLine("Position: " + nextNeighborPanel.Location.Y);
                    neighborStateYPosition += (nextNeighborPanel.Size.Height + 5);
                    Console.WriteLine("Position: " + nextNeighborPanel.Location.Y);
                }

                toStateYPosition += (nextStatePanel.Size.Height + 5);
            }
        }

        public void UpdateValues(StatePageInfo info, int currentState, int amountOfStates)
        {
            colorBox.BackColor = info.color;
            if (info.startingAmount.HasValue)
                agentCount.Text = info.startingAmount.ToString();

            for (int otherState = 0; otherState < amountOfStates; otherState++)
            {
                int currentOtherState = otherState + 1;

                //unlikely but possible - situations where one would set this to a value
                //rather than keeping blank vVv   Just consider it...
                if (currentOtherState == currentState)
                    continue;

                string stateName = "StatePanel" + otherState;

                for (int neighborState = 0; neighborState < amountOfStates; neighborState++)
                {
                    string neighborName = "NeighborPanel" + neighborState;
                    Neighbor_State_Entry currentNeighbor = this.Controls[stateName].Controls.Cast<Neighbor_State_Entry>().Where(c => c.Name == ("NeighborPanel" + neighborState)).FirstOrDefault();
                    int columns = currentNeighbor.dataGridView1.ColumnCount;
                    int rows = currentNeighbor.dataGridView1.RowCount;
                    double[,] probArray = new double[rows, columns];
                    for(int i = 0; i < rows; i++)
                    {
                        for(int j = 0; j < columns; j++)
                        {
                            probArray[i, j] = Double.Parse(currentNeighbor.dataGridView1[i, j].ToString());
                        }
                    }
                }
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
            info.startingAmount = int.Parse(agentCount.Text);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
