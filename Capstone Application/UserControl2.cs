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
        ExtraPanel e;
        StatePageInfo parent;
        int neighborStateXPosition = 0;
        Color color;
        int toStateXPosition = 0;
        int toStateYPosition = 0;
        MoveType mobileN = MoveType.None;
        int states;
        int curState;
        int neighbors = 0;
        int startingAmount = 0;
        bool sticking = false;
        bool mobile = false;
        bool storage = false;
        bool extra = false; // think about what type of search the ai will have - what range?
        bool growth = false;
        NType nType;
        GridType gType;

        List<List<List<double>>> probs = new List<List<List<double>>>();
        List<double> moveProbs = new List<double>();
        List<double> stickingProbs = new List<double>();
        List<Tuple<int, int>> startingLocations = new List<Tuple<int, int>>();
        List<Tuple<string, double>> storageObjects = new List<Tuple<string, double>>();

        public UserControl2(StatePageInfo info, int numStates, int thisState)
        {
            parent = info;
            states = numStates;
            curState = thisState;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            extraBox.SelectedIndex = 0;
            SetSettings();
            SetUI();
        }

        void SetSettings()
        {
            nType = parent.nType;
            if (parent.gridType.HasValue)
            {
                gType = parent.gridType.Value;
            }            
            if (parent.probs != null)
            {
                probs = parent.probs;
            }
            if (parent.moveProbs != null)
            {
                moveProbs = parent.moveProbs;
            }
            if (parent.stickingProbs != null)
            {
                stickingProbs = parent.stickingProbs;
            }
            if (parent.sticking.HasValue)
            {
                sticking = parent.sticking.Value;
            }
            if (parent.mobile.HasValue)
            {
                mobile = parent.mobile.Value;
            }
            if (parent.storage.HasValue)
            {
                storage = parent.storage.Value;
            }
            //if (parent.ai.HasValue)
            //{
            //    ai = parent.ai.Value;
            //}
            if (parent.growth.HasValue)
            {
                growth = parent.growth.Value;
            }
            if (parent.mobileNeighborhood.HasValue)
            {
                mobileN = parent.mobileNeighborhood.Value;
            }
            if (parent.startingLocations != null)
            {
                startingLocations = parent.startingLocations;
            }
            if (parent.storageObjects != null)
            {
                storageObjects = parent.storageObjects;
            }
            if (parent.neighbors.HasValue)
            {
                neighbors = parent.neighbors.Value;
            }
            if (parent.color.HasValue)
            {
                color = parent.color.Value;
            }
            if (parent.startingAmount.HasValue)
            {
                startingAmount = parent.startingAmount.Value;
            }
        }

        void SetUI()
        {
            switch(neighbors)
            {
                case 0:
                    neighborBox.SelectedIndex = 0;
                    break;
                case 4:
                    neighborBox.SelectedIndex = 1;
                    break;
                case 8:
                    neighborBox.SelectedIndex = 2;
                    break;
                case 12:
                    neighborBox.SelectedIndex = 3;
                    break;
                case -1:
                    neighborBox.SelectedIndex = 4;
                    break;
                default:
                    neighborBox.SelectedIndex = 0;
                    break;
            }
            switch(mobile)
            {
                case false:
                    mobilityBox.SelectedIndex = 0;
                    break;
                case true:
                    mobilityBox.SelectedIndex = 1;
                    break;
            }
            switch(gType)
            {
                case GridType.Box:
                    edgeBox.SelectedIndex = 0;
                    break;
                case GridType.CylinderH:
                    edgeBox.SelectedIndex = 1;
                    break;
                case GridType.CylinderW:
                    edgeBox.SelectedIndex = 2;
                    break;
                case GridType.Torus:
                    edgeBox.SelectedIndex = 3;
                    break;
            }
            agentCount.Text = startingAmount.ToString();
            colorBox.BackColor = color;
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
                var moveNeighbors = Enum.GetNames(typeof(MoveType));
                ComboBox mNeighborPick = new ComboBox() { Name = "mNeighborPick", Location = new System.Drawing.Point(xInput, yPos) };
                mNeighborPick.Items.AddRange(moveNeighbors);
                mNeighborPick.DropDownStyle = ComboBoxStyle.DropDownList;
                mNeighborPick.SelectedIndexChanged += (sender, e) =>
                {
                    mobileN = (MoveType)(mNeighborPick.SelectedIndex);
                    RefreshMobilityFields();
                };
                
                mobilityButtonsPanel.Controls.Add(mNeighborPickLabel);
                mobilityButtonsPanel.Controls.Add(mNeighborPick);
                //this.PerformLayout();
                //Console.WriteLine("What the ever-loving F is going on here.");
                //for (int i = 0; i < mobilityButtonsPanel.Controls.Count; i++)
                //{
                //    var control = mobilityButtonsPanel.Controls[i];
                //    Console.WriteLine("Control: " + control.Name + " Width: " + control.Width + " Height: " + control.Height);
                //}
                //Console.WriteLine("Panel: " + mobilityButtonsPanel.Name + " Width: " + mobilityButtonsPanel.Width + " Height: " + mobilityButtonsPanel.Height);
                //mobilityButtonsPanel.Update();
                mNeighborPick.SelectedIndex = 0;
            }
        }

        void RefreshMobilityFields()
        {
            mobilityInputPanel.Controls.Clear();
            string[] directions = new string[] { "up", "right", "down", "left", "up-left", "up-right", "down-right", "down-left"};
            if (mobile == true)
            {
                int xLabel = 3;
                int xInput = 439;
                int yPos = 5;
                List<double> stickingProbsTemp = new List<double>();
                for (int i = 0; i < states; i++)
                {
                    stickingProbsTemp.Add(0);
                }
                for (int i = 0; i < stickingProbs.Count; i++)
                {
                    try
                    {
                        stickingProbsTemp[i] = stickingProbs[i];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + System.Environment.NewLine + e.StackTrace);
                    }
                }
                stickingProbs = stickingProbsTemp;
                for (int i = 0; i < states; i++)
                {
                    //stickingProbsTemp.Add(0);
                    string stickState = (i + 1).ToString();
                    Label mStickyLabel = new Label() { Name = $"mStickyLabel{i}", Text = $"Sticking probability: the agent's propensity for sticking to other agents of type {stickState}.", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                    TextBox mStickyPick = new TextBox() { Name = $"mStickyPick{i}", Location = new System.Drawing.Point(xInput, yPos), Size = new Size(121, 20), Text = stickingProbs[i].ToString() };
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
                }
                for (int i = 0; i < moveProbsTemp.Count; i++)
                {
                    try
                    {
                        moveProbsTemp[i] = moveProbs[i];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + System.Environment.NewLine + e.StackTrace);
                    }
                }
                moveProbs = moveProbsTemp;
                for (int i = 0; i < loop; i++)
                {
                    moveProbsTemp.Add(0);
                    string moveDir = directions[i];
                    Label mMoveLabel = new Label() { Name = $"mMoveLabel{i}", Text = $"Probability of moving {moveDir}.", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                    TextBox mMovePick = new TextBox() { Name = $"mMovePick{i}", Location = new System.Drawing.Point(xInput, yPos), Size = new Size(121, 20), Text = moveProbs[i].ToString() };
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
                Button calc = new Button() { Name = "calcButton", Location = new System.Drawing.Point(338, yPosCalc - 26), Size = new Size(95, 21), Text = "Calculate Others" };
                calc.Click += (sender, e) =>
                {
                    TextBox down = (TextBox)mobilityInputPanel.Controls["mMovePick2"];
                    if(double.TryParse(down.Text, out double result))
                    {
                        double rem = 1 - result;
                        double div = rem / (loop - 1);
                        for (int i = 0; i < loop; i++)
                        {
                            if(i == 2)
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
            // pull from real probs if any old
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
                            // do nothing
                            //throw;
                        }
                    }
                }
            }
            probs = tempProbs;
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
                                int _i = i;
                                int _j = j;
                                int _k = k;
                                //Console.WriteLine("Positions: {0}, {1}", xLabel, yPos);
                                string neighbors = k.ToString();
                                Label neighborsLabel = new Label() { Name = $"mNeighborLabel{i}.{j}.{k}", Text = $"Probability of change from state {fromState} to state {toState}, with {k} neighbors of state {neighborState}.", Location = new System.Drawing.Point(xLabel, yPos), AutoSize = true };
                                TextBox neighborsPick = new TextBox() { Name = $"mNeighborPick{i}.{j}.{k}", Location = new System.Drawing.Point(xInput, yPos), Size = new Size(121, 20), Text = probs[i][j][k].ToString() };
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
                                        probs[_i][_j][_k] = result;
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
            // populate fields with prob data
        }

        void RefreshExtraButtons()
        {
            if(extra)
            {
                //Console.WriteLine(extraPanel.Size.Width + ", " + extraPanel.Size.Height);
                extraPanel.Controls.Clear();
                e = new ExtraPanel
                {
                    Location = new System.Drawing.Point(0, 0)
                };
                extraPanel.Controls.Add(e);
                //Console.WriteLine(extraPanel.Size.Width + ", " + extraPanel.Size.Height);
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
                Console.WriteLine("Positions: {0}, {1}", toStateXPosition, toStateYPosition);
            }
        }

        public void UpdateValues(StatePageInfo info, int currentState)
        {
            colorBox.BackColor = info.color.Value;
            agentCount.Text = info.startingAmount.ToString();
            probs = info.probs;
            stickingProbs = info.stickingProbs;
            sticking = info.sticking.Value;
            moveProbs = info.moveProbs;
            storage = info.storage.Value;
            //ai = info.ai.Value;
            growth = info.growth.Value;
            storageObjects = info.storageObjects;
            startingLocations = info.startingLocations;
            mobileN = info.mobileNeighborhood.Value;
            switch (info.nType)
            {
                case NType.None:
                    neighborBox.SelectedIndex = 0;
                    break;
                case NType.VonNeumann:
                    neighborBox.SelectedIndex = 1;
                    break;
                case NType.Moore:
                    neighborBox.SelectedIndex = 2;
                    break;
                case NType.Hybrid:
                    neighborBox.SelectedIndex = 3;
                    break;
                case NType.Advanced:
                    neighborBox.SelectedIndex = 4;
                    break;
            }
            switch(info.gridType)
            {
                case GridType.Box:
                    edgeBox.SelectedIndex = 0;
                    break;
                case GridType.CylinderH:
                    edgeBox.SelectedIndex = 1;
                    break;
                case GridType.CylinderW:
                    edgeBox.SelectedIndex = 2;
                    break;
                case GridType.Torus:
                    edgeBox.SelectedIndex = 3;
                    break;
            }
            switch(info.mobile)
            {
                case false:
                    mobilityBox.SelectedIndex = 0;
                    break;
                case true:
                    mobilityBox.SelectedIndex = 1;
                    break;
            }
            RefreshNeighborFields();
            RefreshMobilityButtons();
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
            info.storage = storage;
            //info.ai = ai;
            info.growth = growth;
            info.storageObjects = storageObjects;
            if(extra)
            {
                info.containerSettings = e.Retrieve();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void agentCount_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(agentCount.Text, out int result))
            {
                startingAmount = result;
            }
            else
            {
                startingAmount = 0;
            }
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
            //RefreshMobilityFields();
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

        private void colorBox_BackColorChanged(object sender, EventArgs e)
        {
            color = colorBox.BackColor;
        }

        private void extraBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(extraBox.SelectedIndex)
            {
                case 0:
                    extra = false;
                    break;
                case 1:
                    extra = true;
                    break;
            }
            RefreshExtraButtons();
        }
    }
}
