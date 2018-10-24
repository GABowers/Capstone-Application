﻿using System;
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
        int mobileN = 0;
        int states;
        int curState;
        int neighbors = 0;
        bool sticking = false;
        bool mobile = false;
        NType nType;
        GridType gType;

        List<List<List<double>>> probs = new List<List<List<double>>>();
        List<double> moveProbs = new List<double>();
        List<double> stickingProbs = new List<double>();
        List<Tuple<int, int>> startingLocations = new List<Tuple<int, int>>();

        public UserControl2(int numStates, int thisState)
        {
            states = numStates;
            curState = thisState;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            mobilityBox.SelectedIndex = 0;
            neighborBox.SelectedIndex = 0;
            edgeBox.SelectedIndex = 0;
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

        void RefreshMobilityButtons()
        {
            mobilityButtonsPanel.Controls.Clear();
            if (mobile)
            {
                int xLabel = 3;
                int xInput = 439;
                int yPos = 5;
                Label mNeighborPickLabel = new Label() { Name = "mNeighborPickLabel", Text = "Set the neighborhood of the mobile agent--or what directions it can move", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                ComboBox mNeighborPick = new ComboBox() { Name = "mNeighborPick", Items = { "von Neumann" }, Location = new System.Drawing.Point(xInput, yPos) };
                mNeighborPick.SelectedIndexChanged += (sender, e) =>
                {
                    switch (mNeighborPick.SelectedIndex)
                    {
                        case 0:
                            mobileN = 0;
                            break;
                        default:
                            mobileN = 0;
                            break;
                    }
                    RefreshMobilityFields();
                };
                mNeighborPick.SelectedIndex = mobileN;
                mobilityButtonsPanel.Controls.Add(mNeighborPickLabel);
                mobilityButtonsPanel.Controls.Add(mNeighborPick);
            }
        }

        void RefreshMobilityFields()
        {
            mobilityInputPanel.Controls.Clear();
            string[] directions = new string[] { "left", "up", "right", "down", "up-left", "up-right", "down-right", "down-left"};
            if (mobile == true)
            {
                int xLabel = 3;
                int xInput = 439;
                int yPos = 5;
                List<double> stickingProbsTemp = new List<double>();
                for (int i = 0; i < states; i++)
                {
                    stickingProbsTemp.Add(0);
                    string stickState = (i + 1).ToString();
                    Label mStickyLabel = new Label() { Name = $"mStickyLabel{i}", Text = $"Sticking probability: the agent's propensity for sticking to other agents of type {stickState}.", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                    TextBox mStickyPick = new TextBox() { Name = $"mStickyPick{i}", Location = new System.Drawing.Point(xInput, yPos), Size = new Size(121, 20) };
                    mStickyPick.TextChanged += (sender, e) =>
                    {
                        int stick_loc = int.Parse(mStickyPick.Name.Remove(0, 11));
                        if (double.TryParse(mStickyPick.Text, out double result))
                        {
                            stickingProbs[stick_loc] = result;
                        }
                        else
                        {
                            stickingProbs[stick_loc] = 0;
                        }
                    };
                    yPos += 26;
                    mobilityInputPanel.Controls.Add(mStickyLabel);
                    mobilityInputPanel.Controls.Add(mStickyPick);
                }

                for (int i = 0; i < stickingProbs.Count; i++)
                {
                    try
                    {
                        stickingProbs[i] = stickingProbsTemp[i];
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                stickingProbs = stickingProbsTemp;
                int loop;
                switch(mobileN)
                {
                    case 0:
                        loop = 4;
                        break;
                    default:
                        loop = 4;
                        break;
                }
                int yPosCalc = 0;
                List<double> moveProbsTemp = new List<double>();
                for (int i = 0; i < loop; i++)
                {
                    moveProbsTemp.Add(0);
                    string moveDir = directions[i];
                    Label mMoveLabel = new Label() { Name = $"mMoveLabel{i}", Text = $"Probability of moving {moveDir}.", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                    TextBox mMovePick = new TextBox() { Name = $"mMovePick{i}", Location = new System.Drawing.Point(xInput, yPos), Size = new Size(121, 20) };
                    mMovePick.TextChanged += (sender, e) =>
                    {
                        int move_loc = int.Parse(mMovePick.Name.Remove(0, 9));
                        if (double.TryParse(mMovePick.Text, out double result))
                        {
                            moveProbs[move_loc] = result;
                        }
                        else
                        {
                            moveProbs[move_loc] = 0;
                        }
                    };
                    if(i == 3)
                    {
                        yPosCalc = yPos;
                    }
                    yPos += 26;
                    mobilityInputPanel.Controls.Add(mMoveLabel);
                    mobilityInputPanel.Controls.Add(mMovePick);
                }
                Button calc = new Button() { Name = "calcButton", Location = new System.Drawing.Point(338, yPosCalc), Size = new Size(95, 21), Text = "Calculate Others" };
                calc.Click += (sender, e) =>
                {
                    TextBox down = (TextBox)mobilityInputPanel.Controls["mMovePick3"];
                    if(double.TryParse(down.Text, out double result))
                    {
                        double rem = 1 - result;
                        double div = rem / (loop - 1);
                        for (int i = 0; i < loop; i++)
                        {
                            if(i == 3)
                            {
                                continue;
                            }
                            else
                            {
                                string name = "mMovePick" + i;
                                TextBox cur = (TextBox)mobilityInputPanel.Controls[name];
                                cur.Text = div.ToString();
                            }
                        }
                    }
                };
                mobilityInputPanel.Controls.Add(calc);
                for (int i = 0; i < moveProbs.Count; i++)
                {
                    try
                    {
                        moveProbs[i] = moveProbsTemp[i];
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                moveProbs = moveProbsTemp;
            }
        }

        void RefreshNeighborFields()
        {
            neighborhoodPanel.Controls.Clear();
            int xLabel = 3;
            int xInput = 439;
            int yPos = 5;
            List<List<List<double>>> tempProbs = new List<List<List<double>>>();
            for (int i = 0; i < states; i++)
            {
                tempProbs.Add(new List<List<double>>());
                for (int j = 0; j < states; j++)
                {
                    tempProbs[i].Add(new List<double>());
                    if (neighbors == -1)
                    {
                        // do advanced neighbor stuff
                    }
                    else
                    {
                        for (int l = 0; l < neighbors + 1; l++)
                        {
                            tempProbs[i][j].Add(0);
                            // add labels and text fields
                        }
                    }
                }
            }
            string fromState = (curState + 1).ToString();
            for (int i = 0; i < states; i++)
            {
                if(i == curState)
                {
                    continue;
                }
                else
                {
                    string toState = (i + 1).ToString();
                    for (int j = 0; j < states; j++)
                    {
                        string neighborState = (j + 1).ToString();
                        if (neighbors == -1)
                        {
                            // do advanced neighbor stuff
                        }
                        else
                        {
                            for (int k = 0; k < neighbors + 1; k++)
                            {
                                string neighbors = k.ToString();
                                Label neighborsLabel = new Label() { Name = $"mNeighborLabel{i}.{j}.{k}", Text = $"Probability of change from state {fromState} to state {toState}, with {k} neighbors of state {neighborState}.", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                                TextBox neighborsPick = new TextBox() { Name = $"mNeighborPick{i}.{j}.{k}", Location = new System.Drawing.Point(xInput, yPos), Size = new Size(121, 20) };
                                neighborsPick.TextChanged += (sender, e) =>
                                {
                                    string basic = neighborsPick.Name.Remove(0, 13);
                                    int first = int.Parse(basic.Remove(1));
                                    string next = basic.Remove(0, 2);
                                    int second = int.Parse(next.Remove(1));
                                    string final = next.Remove(0, 2);
                                    int third = int.Parse(final);
                                    if (double.TryParse(neighborsPick.Text, out double result))
                                    {
                                        probs[first][second][third] = result;
                                    }
                                    else
                                    {
                                        probs[first][second][third] = 0;
                                    }
                                };
                                yPos += 26;
                                neighborhoodPanel.Controls.Add(neighborsLabel);
                                neighborhoodPanel.Controls.Add(neighborsPick);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < probs.Count; i++)
            {
                for (int j = 0; j < probs[i].Count; j++)
                {
                    for (int k = 0; k < probs[i][j].Count; k++)
                    {
                        try
                        {
                            tempProbs[i][j][k] = probs[i][j][k];
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }

            probs = tempProbs;
            // populate fields with prob data
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
                this.inputPanel.Controls.Add(nextStatePanel);
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
                    To_State_Panel currentStatePanel = this.inputPanel.Controls.Find(stateName, true).FirstOrDefault() as To_State_Panel;
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
            info.nType = nType;
            info.gridType = gType;
            info.mobile = mobile;
            info.color = colorBox.BackColor;
            info.mobileNeighborhood = mobileN;
            info.startingLocations = startingLocations;
            info.neighbors = neighbors;
            // Maybe check that the number is valid first?
            if(int.TryParse(agentCount.Text, out int result))
            {
                info.startingAmount = result;
            }
            else
            {
                info.startingAmount = 0;
            }
            info.probs = probs;
            info.stickingProbs = stickingProbs;
            info.sticking = sticking;
            info.moveProbs = moveProbs;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void agentCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void neighborBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(neighborBox.SelectedIndex)
            {
                case 0:
                    neighbors = 0;
                    nType = NType.None;
                    break;
                case 1:
                    neighbors = 4;
                    nType = NType.VonNeumann;
                    break;
                case 2:
                    neighbors = 8;
                    nType = NType.Moore;
                    break;
                case 3:
                    neighbors = 12;
                    nType = NType.Hybrid;
                    break;
                case 4:
                    neighbors = -1;
                    nType = NType.Advanced;
                    break;
                default:
                    neighbors = 0;
                    nType = NType.None;
                    break;
            }
            RefreshNeighborFields();
        }

        private void mobilityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(mobilityBox.SelectedIndex)
            {
                case 0:
                    mobile = false;
                    break;
                case 1:
                    mobile = true;
                    break;
                default:
                    mobile = false;
                    break;
            }
            RefreshMobilityButtons();
            RefreshMobilityFields();
        }

        private void edgeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (edgeBox.SelectedIndex)
            {
                case 0:
                    gType = GridType.Box;
                    break;
                case 1:
                    gType = GridType.CylinderH;
                    break;
                case 2:
                    gType = GridType.CylinderW;
                    break;
                case 3:
                    gType = GridType.Torus;
                    break;
                default:
                    gType = GridType.Box;
                    break;
            }
        }
    }
}
