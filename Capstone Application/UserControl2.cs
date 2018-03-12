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
                nextStatePanel.Name = "StatePanel" + otherState;
                nextStatePanel.toStateLabel.Text = "To State " + currentOtherState + ", with neighbors of:";
                this.fullPanel.Controls.Add(nextStatePanel);
                nextStatePanel.Location = new System.Drawing.Point(toStateXPosition, toStateYPosition);

                for (int neighborState = 0; neighborState < amountOfStates; neighborState++)
                {
                    int currentNeighborState = neighborState + 1;
                    Neighbor_State_Entry nextNeighborPanel = new Neighbor_State_Entry();
                    nextNeighborPanel.AddInitialCell();
                    nextNeighborPanel.Name = "NeighborPanel" + neighborState;
                    nextNeighborPanel.neighborStateLabel.Text = "State " + currentNeighborState;
                    nextStatePanel.Controls.Add(nextNeighborPanel);
                    nextNeighborPanel.Location = new System.Drawing.Point(neighborStateXPosition, neighborStateYPosition);
                    neighborStateYPosition += (nextNeighborPanel.Size.Height + 5);
                }

                toStateYPosition += (nextStatePanel.Size.Height + 5);
            }
        }

        public void UpdateValues(StatePageInfo info, int currentState)
        {
            colorBox.BackColor = info.color;
            agentCount.Text = info.startingAmount.ToString();

            for (int otherState = 0; otherState < info.advProbs.GetLength(0); otherState++)
            {
                int currentOtherState = otherState + 1;

                // unlikely but possible - situations where one would set this to a value
                // (prob of staying the same is currently automatically assigned as the remainder
                // of however much of 0-1 is used)
                // rather than keeping blank vVv   Just consider it...
                if (currentOtherState == currentState)
                {
                    for (int neighborState = 0; neighborState < info.advProbs.GetLength(1); neighborState++)
                    {
                        info.advProbs[otherState, neighborState] = new double[1, 1];
                    }
                    continue;
                }

                string stateName = "StatePanel" + otherState;
                for (int neighborState = 0; neighborState < info.advProbs.GetLength(1); neighborState++)
                {
                    string neighborName = "NeighborPanel" + neighborState;
                    To_State_Panel currentStatePanel = this.fullPanel.Controls.Find(stateName, true).FirstOrDefault() as To_State_Panel;
                    Neighbor_State_Entry currentNeighbor = currentStatePanel.Controls.Find(neighborName, true).FirstOrDefault() as Neighbor_State_Entry;
                    int columns = currentNeighbor.dataGridView1.ColumnCount;
                    int rows = currentNeighbor.dataGridView1.RowCount;
                    info.advProbs[otherState, neighborState] = new double[rows, columns];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            currentNeighbor.dataGridView1[i, j].Value = info.advProbs[otherState, neighborState][i, j];
                        }
                    }
                }
            }
        }

        public void SetValues(StatePageInfo info, int currentState)
        {
            info.color = colorBox.BackColor;

            // Maybe check that the number is valid first?
            if(string.IsNullOrWhiteSpace(agentCount.Text))
            {
                info.startingAmount = 0;
            }
            else
            {
                info.startingAmount = int.Parse(agentCount.Text);
            }
            for (int otherState = 0; otherState < info.advProbs.GetLength(0); otherState++)
            {
                int currentOtherState = otherState + 1;

                // unlikely but possible - situations where one would set this to a value
                // (prob of staying the same is currently automatically assigned as the remainder
                // of however much of 0-1 is used)
                // rather than keeping blank vVv   Just consider it...
                if (currentOtherState == currentState)
                {
                    for (int neighborState = 0; neighborState < info.advProbs.GetLength(1); neighborState++)
                    {
                        info.advProbs[otherState, neighborState] = new double[1, 1];
                    }
                    continue;
                }

                string stateName = "StatePanel" + otherState;
                for (int neighborState = 0; neighborState < info.advProbs.GetLength(1); neighborState++)
                {
                    string neighborName = "NeighborPanel" + neighborState;
                    To_State_Panel currentStatePanel = this.fullPanel.Controls.Find(stateName, true).FirstOrDefault() as To_State_Panel;
                    Neighbor_State_Entry currentNeighbor = currentStatePanel.Controls.Find(neighborName, true).FirstOrDefault() as Neighbor_State_Entry;
                    int columns = currentNeighbor.dataGridView1.ColumnCount;
                    int rows = currentNeighbor.dataGridView1.RowCount;
                    info.advProbs[otherState, neighborState] = new double[rows, columns];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            if (currentNeighbor.dataGridView1[i, j].Value == null || string.IsNullOrWhiteSpace(currentNeighbor.dataGridView1[i, j].Value.ToString()))
                            {
                                info.advProbs[otherState, neighborState][i, j] = 0;
                            }
                            else
                            {
                                info.advProbs[otherState, neighborState][i, j] = Double.Parse(currentNeighbor.dataGridView1[i, j].Value.ToString());
                            }
                        }
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void agentCount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
