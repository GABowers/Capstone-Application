using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    public class ControllerScript
    {
        static MainPageController mainPageController;
        static StatePageController cellPageController;
        static MainPageInfo mainPageInfo;
        public CA myCA;
        static  List<StatePageInfo> statePageInfo;
        public GridType gType;
        NType nType;
        List<Color> colors = new List<Color>();
        List<float> ratios = new List<float>();
        List<int> cellAmounts = new List<int>();
        public List<List<int>> fullCount = new List<List<int>>();
        public List<List<int>> fullTransitions = new List<List<int>>();

        public bool editModeOn = false;
        public bool createdCA = false;
        bool alreadyCA = false;
        bool running = false;
        public int iterations = 0;
        public int caRuns = 1;
        public int neighborhoodType;
        int localGridWidth;
        int localGridHeight;
        public int amountOfCellTypes;
        double iterationSpeed = 0;
        Bitmap bmp;

        public MainPageInfo MainPageInfo
        {
            get { return mainPageInfo; }
            set { mainPageInfo = value; }
        }

        static ControllerScript()
        {
            mainPageInfo = new MainPageInfo();
            cellPageController = new StatePageController();
            statePageInfo = new List<StatePageInfo>();
            mainPageController = new MainPageController();
        }

        public void SetMainInfo(ComboBox caType, ComboBox neighborType, ComboBox gridType, TextBox stateNumberBox, TextBox gridSizeHori, TextBox gridSizeVert)
        {
            caType.SelectedIndex = mainPageInfo.caType;
            gridType.SelectedIndex = (int)mainPageInfo.gridType;
            neighborType.SelectedIndex = (int)mainPageInfo.nType;
            //May have errors here - previously it was an int? so it checked if a value had been assigned.
            stateNumberBox.Text = mainPageInfo.numStates.ToString();
            gridSizeHori.Text = mainPageInfo.gridWidth.ToString();
            gridSizeVert.Text = mainPageInfo.gridHeight.ToString();
        }

        public void MainPageNext(GridType gridType, NType nType, int numStates, int gridWidth, int gridHeight, int caType)
        {
            UpdateMainValues(gridType, nType, numStates, gridWidth, gridHeight, caType);
            if (CheckMainPageInfo() == false)
            {
                return;
            }
            if (mainPageInfo.numStates != statePageInfo.Count)
            {
                SetupStateInfo();
            }
        }

        public void UpdateProbFields(UserControl1 uc, int currentState)
        {
            uc.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        }

        //public void AdvancedUpdateProbFields(UserControl2 uc, int currentState, int amountOfStates)
        //{
        //    uc.UpdateValues(statePageInfo[(currentState - 1)], currentState, amountOfStates);
        //}

        private bool CheckMainPageInfo()
        {
            bool isOK = true;
            if (mainPageInfo.numStates <= 0)
                isOK = false;
            if (mainPageInfo.gridWidth <= 0)
                isOK = false;
            if (mainPageInfo.gridHeight <= 0)
                isOK = false;
            return isOK;
        }

        private void SetupStateInfo()
        {
            // Clear out old state pages
            statePageInfo.Clear();
            int neighbors;
            switch (mainPageInfo.nType)
            {
                case NType.None:
                    neighbors = 0;
                    break;
                case NType.VonNeumann:
                    neighbors = 4;
                    break;
                case NType.Moore:
                    neighbors = 8;
                    break;
                case NType.Hybrid:
                    neighbors = 12;
                    break;
                case NType.Advanced:
                    neighbors = 0;
                    break;
                default:
                    neighbors = 0;
                    break;
            }
            for (int i = 0; i < mainPageInfo.numStates; ++i)
            {
                // Needs to be different based on which type of neighborhood we're using.

                StatePageInfo current = new StatePageInfo(mainPageInfo.numStates, neighbors, i + 1, mainPageInfo.nType, mainPageInfo.caType);
                //StatePageInfo current = new StatePageInfo(mainPageInfo.numStates);
                statePageInfo.Add(current);
                //Console.WriteLine("New statePage added");
            }
        }

        public void UpdateMainValues(GridType newgridType, NType newnType, int newnumStates, int newgridWidth, int newgridHeight, int caType)
        {
            mainPageInfo.gridType = newgridType;
            mainPageInfo.nType = newnType;
            mainPageInfo.numStates = newnumStates;
            mainPageInfo.gridWidth = newgridWidth;
            mainPageInfo.gridHeight = newgridHeight;
            mainPageInfo.caType = caType;
        }

        public void UpdateProbValues(UserControl1 newUC, int currentState)
        {
            // Do these need to be here? Seems a little like excessive OOP.
            newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void RetrieveProbValues(UserControl1 newUC, int currentState)
        {
            // Do these need to be here? Seems a little like excessive OOP.
            newUC.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void AdvancedUpdateProbValues(UserControl2 newUC, int currentState)
        {
            newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void AdvancedRetrieveProbValues(UserControl2 newUC, int currentState)
        {
            newUC.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void Update2ndOrder(_2ndOrderTabs newUC, int currentState)
        {
            newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void Retrieve2ndOrder(_2ndOrderTabs newUC, int currentState)
        {
            newUC.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        }

        // Stuff from other controller here

        public void OneIteration(Form1 currentForm)
        {
            running = true;
            myCA.OneIteration();
            iterations++;
            List<int> currentCellCount = new List<int>();
            List<int> currentTransitions = new List<int>();
            currentCellCount.AddRange(myCA.stateCount);
            currentTransitions.AddRange(myCA.transitions);
            fullCount.Add(currentCellCount);
            fullTransitions.Add(currentTransitions);
        }

        public void CreateCA()
        {
            if (createdCA == false)
            {
                iterations = 0;
                editModeOn = false;
                amountOfCellTypes = mainPageInfo.numStates;
                nType = mainPageInfo.nType;
                localGridWidth = mainPageInfo.gridWidth;
                localGridHeight = mainPageInfo.gridHeight;
                gType = mainPageInfo.gridType;
                myCA = new CA(localGridWidth, localGridHeight, amountOfCellTypes, nType, mainPageInfo.caType, gType);
                for (int h = 0; h < statePageInfo.Count; ++h)
                {
                    ratios.Add(statePageInfo[h].startingAmount.Value);
                    cellAmounts.Add(statePageInfo[h].startingAmount.Value);
                    colors.Add(statePageInfo[h].color);
                    if (statePageInfo[h].caType == 0)
                    {
                        if (statePageInfo[h].nType == NType.Advanced)
                        {
                            for (int i = 0; i < statePageInfo[h].advProbs.GetLength(0); i++)
                            {
                                for (int j = 0; j < (statePageInfo[h].advProbs.GetLength(1)); j++)
                                {
                                    myCA.CreateStateArray(h, i, j, statePageInfo[h].advProbs[i, j].GetLength(0), statePageInfo[h].advProbs[i, j].GetLength(1));
                                    for (int k = 0; k < statePageInfo[h].advProbs[i, j].GetLength(0); k++)
                                    {
                                        for (int l = 0; l < statePageInfo[h].advProbs[i, j].GetLength(1); l++)
                                        {
                                            myCA.SetStateInfo(h, i, j, k, l, statePageInfo[h].advProbs[i, j][k, l]);
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            for (int i = 0; i < statePageInfo[h].probs.GetLength(0); ++i)
                            {
                                for (int j = 0; j < (statePageInfo[h].probs.GetLength(1)); ++j)
                                {
                                    for (int k = 0; k < statePageInfo[h].probs.GetLength(2); ++k)
                                    {
                                        myCA.SetStateInfo(h, i, j, k, statePageInfo[h].probs[i, j, k]);
                                    }
                                }
                            }
                        }
                    }
                    else if(statePageInfo[h].caType == 1)
                    {
                            myCA.Set2ndOrder(h, statePageInfo[h].walkProbs, statePageInfo[h].stickingProbs, statePageInfo[h].sticking, statePageInfo[h].mobileNeighborhood);
                    }
                }
                myCA.InitializeGrid(cellAmounts);
                createdCA = true;
            }
        }

        public void EditCA()
        {
            for (int h = 0; h < statePageInfo.Count; ++h)
            {
                colors[h] = statePageInfo[h].color;
                if (statePageInfo[h].caType == 0)
                {
                    if (statePageInfo[h].nType == NType.Advanced)
                    {
                        for (int i = 0; i < statePageInfo[h].advProbs.GetLength(0); i++)
                        {
                            for (int j = 0; j < (statePageInfo[h].advProbs.GetLength(1)); j++)
                            {
                                myCA.CreateStateArray(h, i, j, statePageInfo[h].advProbs[i, j].GetLength(0), statePageInfo[h].advProbs[i, j].GetLength(1));
                                for (int k = 0; k < statePageInfo[h].advProbs[i, j].GetLength(0); k++)
                                {
                                    for (int l = 0; l < statePageInfo[h].advProbs[i, j].GetLength(1); l++)
                                    {
                                        myCA.SetStateInfo(h, i, j, k, l, statePageInfo[h].advProbs[i, j][k, l]);
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < statePageInfo[h].probs.GetLength(0); ++i)
                        {
                            for (int j = 0; j < (statePageInfo[h].probs.GetLength(1)); ++j)
                            {
                                for (int k = 0; k < statePageInfo[h].probs.GetLength(2); ++k)
                                {
                                    myCA.SetStateInfo(h, i, j, k, statePageInfo[h].probs[i, j, k]);
                                }
                            }
                        }
                    }
                }
                else if (statePageInfo[h].caType == 1)
                {
                    myCA.Set2ndOrder(h, statePageInfo[h].walkProbs, statePageInfo[h].stickingProbs, statePageInfo[h].sticking, statePageInfo[h].mobileNeighborhood);
                }
            }
        }

        public void StartCA(Form1 currentForm)
        {
            editModeOn = false;
            if (alreadyCA == false)
            {
                iterations = 0;
                bmp = new Bitmap(localGridWidth, localGridHeight);
                UpdateBoard(currentForm);
                alreadyCA = true;
            }
        }

        public void UpdateBoard(Form1 currentForm)
        {
            if (myCA.CaType == 1 && iterations > 0)
            {
                Color tileColor;
                for (int i = 0; i < myCA.ActiveAgents.Count; i++)
                {
                    int oldX = myCA.ActiveAgents[i].History[myCA.ActiveAgents[i].History.Count - 2].Item1;
                    int oldY = myCA.ActiveAgents[i].History[myCA.ActiveAgents[i].History.Count - 2].Item2;
                    int newX = myCA.ActiveAgents[i].History[myCA.ActiveAgents[i].History.Count - 1].Item1;
                    int newY = myCA.ActiveAgents[i].History[myCA.ActiveAgents[i].History.Count - 1].Item2;

                    //Console.WriteLine("Prev: " + oldX + "," + oldY + " Cur: " + newX + "," + newY);

                    if (myCA.grid[oldX, oldY].ContainsAgent == true && (System.Object.ReferenceEquals(myCA.grid[oldX, oldY].agent, null) == false))
                    {
                        tileColor = colors[myCA.GetCellState(oldX, oldY)];
                        bmp.SetPixel(oldX, oldY, tileColor);
                    }
                    else
                    {
                        tileColor = Color.Black;
                        bmp.SetPixel(oldX, oldY, tileColor);
                    }
                    if (myCA.grid[newX, newY].ContainsAgent == true && (System.Object.ReferenceEquals(myCA.grid[newX, newY].agent, null) == false))
                    {
                        tileColor = colors[myCA.GetCellState(newX, newY)];
                        bmp.SetPixel(newX, newY, tileColor);
                    }
                    else
                    {
                        tileColor = Color.Black;
                        bmp.SetPixel(newX, newY, tileColor);
                    }
                }
            }
            else
            {
                Color tileColor;
                for (int i = 0; i < localGridWidth; ++i)
                {
                    for (int j = 0; j < localGridHeight; ++j)
                    {
                        if (myCA.grid[i, j].ContainsAgent == true && (System.Object.ReferenceEquals(myCA.grid[i, j].agent, null) == false))
                        {
                            tileColor = colors[myCA.GetCellState(i, j)];
                            bmp.SetPixel(i, j, tileColor);
                        }
                        else
                        {
                            tileColor = Color.Black;
                            bmp.SetPixel(i, j, tileColor);
                        }
                    }
                }
            }
            
            currentForm.innerPictureBox.Image = bmp;
        }

        public void ClearGrid()
        {
            //Destroy(tex);
            //Destroy(board);
            //Destroy(sr);
            iterations = 0;
            ratios.Clear();
            colors.Clear();
            fullCount.Clear();
            fullTransitions.Clear();
            //imageList.Clear();
            //probabilities.Clear();
            myCA = null;
            alreadyCA = false;
        }

        public void ResetGrid(Form1 form)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");
            CheckFinalDataSave(form, time);
            caRuns++;
            createdCA = false;
            editModeOn = false;
            ClearGrid();
        }

        public void ResetRuns()
        {
            caRuns = 1;
        }

        public Tuple<int, int> TrueLocation(int xValue, int yValue, PictureBoxWithInterpolationMode container)
        {
            Double tempX = Convert.ToDouble(xValue);
            Double tempY = Convert.ToDouble(yValue);
            int xProper = Convert.ToInt32((tempX / container.Size.Width) * myCA.gridWidth);
            int yProper = Convert.ToInt32((tempY / container.Size.Height) * myCA.gridHeight);
            return new Tuple<int, int>(xProper, yProper);
        }

        public void EditGrid(int xValue, int yValue, PictureBoxWithInterpolationMode container, int buttonPressed, int state)
        {
            if(editModeOn == true)
            {
                Double tempX = Convert.ToDouble(xValue);
                Double tempY = Convert.ToDouble(yValue);
                int xProper = Convert.ToInt32((tempX / container.Size.Width) * myCA.gridWidth);
                int yProper = Convert.ToInt32((tempY / container.Size.Height) * myCA.gridHeight);
                if(buttonPressed == 0)
                {
                    if (myCA.grid[xProper, yProper].ContainsAgent == true)
                    {
                        myCA.grid[xProper, yProper].agent.currentState = state;
                        //if (myCA.grid[xProper, yProper].agent.currentState > (mainPageInfo.numStates - 1))
                        //{
                        //    myCA.grid[xProper, yProper].agent.currentState = (myCA.grid[xProper, yProper].agent.currentState - mainPageInfo.numStates);
                        //}
                    }
                }
                if (buttonPressed == 1)
                {
                    if (myCA.grid[xProper, yProper].ContainsAgent == true)
                    {
                        myCA.grid[xProper, yProper].ContainsAgent = false;
                        myCA.grid[xProper, yProper].agent = null;
                        myCA.RemoveAgent(xProper, yProper);
                    }
                    else if(myCA.grid[xProper, yProper].ContainsAgent == false)
                    {
                        myCA.grid[xProper, yProper].AddAgent(xProper, yProper, new AgentController(xProper, yProper));
                        myCA.grid[xProper, yProper].ContainsAgent = true;
                        myCA.grid[xProper, yProper].agent.currentState = state;
                        myCA.grid[xProper, yProper].agent.xLocation = xProper;
                        myCA.grid[xProper, yProper].agent.yLocation = yProper;
                        myCA.AddAgent(myCA.grid[xProper, yProper].agent);
                    }
                }
            }
        }

        public void EditGrid(int[] rangeX, int[] rangeY, PictureBoxWithInterpolationMode container, int buttonPressed, int state)
        {
            if (editModeOn == true)
            {
                for (int i = 0; i < rangeX.Length; i++)
                {
                    for (int j = 0; j < rangeY.Length; j++)
                    {
                        Double tempX = Convert.ToDouble(rangeX[i]);
                        Double tempY = Convert.ToDouble(rangeY[j]);
                        int xProper = Convert.ToInt32((tempX / container.Size.Width) * myCA.gridWidth);
                        int yProper = Convert.ToInt32((tempY / container.Size.Height) * myCA.gridHeight);
                        if (buttonPressed == 0)
                        {
                            if (myCA.grid[xProper, yProper].ContainsAgent == true)
                            {
                                myCA.grid[xProper, yProper].agent.currentState = state;
                                //if (myCA.grid[xProper, yProper].agent.currentState > (mainPageInfo.numStates - 1))
                                //{
                                //    myCA.grid[xProper, yProper].agent.currentState = (myCA.grid[xProper, yProper].agent.currentState - mainPageInfo.numStates);
                                //}
                            }
                        }
                        if (buttonPressed == 1)
                        {
                            if (myCA.grid[xProper, yProper].ContainsAgent == true)
                            {
                                myCA.grid[xProper, yProper].ContainsAgent = false;
                                myCA.grid[xProper, yProper].agent = null;
                                myCA.RemoveAgent(xProper, yProper);
                            }
                            else if (myCA.grid[xProper, yProper].ContainsAgent == false)
                            {
                                myCA.grid[xProper, yProper].AddAgent(xProper, yProper, new AgentController(xProper, yProper));
                                myCA.grid[xProper, yProper].ContainsAgent = true;
                                myCA.grid[xProper, yProper].agent.currentState = state;
                                myCA.grid[xProper, yProper].agent.xLocation = xProper;
                                myCA.grid[xProper, yProper].agent.yLocation = yProper;
                                myCA.AddAgent(myCA.grid[xProper, yProper].agent);
                            }
                        }
                    }
                }
            }
        }

        public double ReturnConnectivityIndex(int state)
        {
            if(createdCA)
            {
                return myCA.GetCIndex(state);
            }
            else
            {
                return 0;
            }
        }

        public void CheckSettings(Form1 form)
        {
            //Get system time here

            string time = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");

            CheckDataSave(form, time);

            // Reset CA options
            if (form.settingsScript.AutoReset)
            {
                // Iteration-based
                if(form.settingsScript.IterationReset)
                {
                    if(form.settingsScript.IterationResetValue == iterations)
                        {
                            //CheckFinalDataSave(form, time);
                            form.AutoReset();
                        }
                }
                // Cell count based
                if(form.settingsScript.CountReset)
                {
                    for(int i = 0; i < form.settingsScript.CountResetValues.Count; i++)
                    {
                        if(myCA.stateCount[i] == form.settingsScript.CountResetValues[i])
                        {
                            //CheckFinalDataSave(form, time);
                            form.AutoReset();
                        }
                    }
                }
            }

            // Pause CA options
            if (form.settingsScript.AutoPause)
            {
                // Iteration-based
                if (form.settingsScript.IterationPause)
                {
                    if (form.settingsScript.IterationPauseValue == iterations)
                    {
                        //CheckFinalDataSave(form, time);
                        form.PauseUnpauseCA();
                    }
                }
                // Cell count based
                if (form.settingsScript.CountPause)
                {
                    for (int i = 0; i < form.settingsScript.CountPauseValues.Count; i++)
                    {
                        if (myCA.stateCount[i] == form.settingsScript.CountPauseValues[i])
                        {
                            //CheckFinalDataSave(form, time);
                            form.PauseUnpauseCA();
                        }
                    }
                }
            }
        }
        
        // This is for saving the cell count and images upon reseting the grid
        void CheckDataSave(Form1 form, string time)
        {
            // This is for saving the cell counts in text file
            if (form.settingsScript.CountSave)
            {
                // Check if iteration is the same as any in the list. If so, do the image save
                for (int i = 0; i < form.settingsScript.CountSaveValues.Count; i++)
                {
                    if (form.settingsScript.CountSaveValues[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        //int tempInt = form.settingsScript.CountSaveValues[i];
                        int remainder = iterations % form.settingsScript.CountSaveValues[i];
                        if (remainder == 0)
                        {
                            // Auto count save
                            form.SaveCounts(time);
                        }
                    }
                }
            }

            // For image saving
            if (form.settingsScript.ImageSave)
            {
                // Check if iteration is the same as any in the list. If so, do the image save
                for (int i = 0; i < form.settingsScript.ImageSaveValues.Count; i++)
                {
                    if (form.settingsScript.ImageSaveValues[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        //int tempInt = form.settingsScript.ImageSaveValues[i];
                        int remainder = iterations % form.settingsScript.ImageSaveValues[i];
                        if (remainder == 0)
                        {
                            // Auto image save
                            form.InvokeImageSave(time);
                        }
                    }
                }
            }

            // For path saving
            if (form.settingsScript.PathSave)
            {
                // Check if iteration is the same as any in the list. If so, do the image save
                for (int i = 0; i < form.settingsScript.PathSaveValues.Count; i++)
                {
                    if (form.settingsScript.PathSaveValues[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        //int tempInt = form.settingsScript.PathSaveValues[i];
                        int remainder = iterations % form.settingsScript.PathSaveValues[i];
                        if (remainder == 0)
                        {
                            // Auto image save
                            form.AutoPathSave(time);
                        }
                    }
                }
            }
        }

        void CheckFinalDataSave(Form1 form, string time)
        {
            // This is for saving the cell counts in text file
            if (form.settingsScript.CountSave)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < form.settingsScript.CountSaveValues.Count; i++)
                {
                    if (form.settingsScript.CountSaveValues[i] == -1)
                    {
                        form.SaveCounts(time);
                    }
                }
            }

            // For image saving
            if (form.settingsScript.ImageSave)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < form.settingsScript.ImageSaveValues.Count; i++)
                {
                    if (form.settingsScript.ImageSaveValues[i] == -1)
                    {
                        form.InvokeImageSave(time);
                    }
                }
            }

            // For image saving
            if (form.settingsScript.PathSave)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < form.settingsScript.PathSaveValues.Count; i++)
                {
                    if (form.settingsScript.PathSaveValues[i] == -1)
                    {
                        form.AutoPathSave(time);
                    }
                }
            }
        }

        public void CheckMaxRuns(Form1 form)
        {
            if(form.settingsScript.RunMax)
            {
                if(caRuns > form.settingsScript.RunMaxValue)
                {
                    form.PauseUnpauseCA();
                }
            }
        }
    }
}
