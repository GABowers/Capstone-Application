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
        public List<int> currentCellCount = new List<int>();

        public bool editModeOn = false;
        public bool createdCA = false;
        bool alreadyCA = false;
        bool running = false;
        public int iterations = 0;
        public int runs = 0;
        public int neighborhoodType;
        int localGridWidth;
        int localGridHeight;
        public int amountOfCellTypes;
        string filename;
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

        public void SetMainInfo(ComboBox neighborType, ComboBox gridType, TextBox stateNumberBox, TextBox gridSizeHori, TextBox gridSizeVert)
        {
            gridType.SelectedIndex = (int)mainPageInfo.gridType;
            neighborType.SelectedIndex = (int)mainPageInfo.nType;
            //May have errors here - previously it was an int? so it checked if a value had been assigned.
            stateNumberBox.Text = mainPageInfo.numStates.ToString();
            gridSizeHori.Text = mainPageInfo.gridWidth.ToString();
            gridSizeVert.Text = mainPageInfo.gridHeight.ToString();
        }

        public void MainPageNext(GridType gridType, NType nType, int numStates, int gridWidth, int gridHeight)
        {
            UpdateMainValues(gridType, nType, numStates, gridWidth, gridHeight);
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

                StatePageInfo current = new StatePageInfo(mainPageInfo.numStates, neighbors, i + 1, mainPageInfo.nType);
                //StatePageInfo current = new StatePageInfo(mainPageInfo.numStates);
                statePageInfo.Add(current);
                //Console.WriteLine("New statePage added");
            }
        }

        public void UpdateMainValues(GridType newgridType, NType newnType, int newnumStates, int newgridWidth, int newgridHeight)
        {
            mainPageInfo.gridType = newgridType;
            mainPageInfo.nType = newnType;
            mainPageInfo.numStates = newnumStates;
            mainPageInfo.gridWidth = newgridWidth;
            mainPageInfo.gridHeight = newgridHeight;
        }

        public void UpdateProbValues(UserControl1 newUC, int currentState)
        {
            newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void AdvancedUpdateProbValues(UserControl2 newUC, int currentState)
        {
            newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        }

        // Stuff from other controller here

        public void OneIteration(Form1 currentForm)
        {
            running = true;
            myCA.OneIteration();
            iterations++;
            //List<int> currentCellCount = new List<int>();
            currentCellCount.Clear();
            currentCellCount.AddRange(CA.stateCount);
            fullCount.Add(currentCellCount);
            CheckSettings(currentForm);
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
                myCA = new CA(localGridWidth, localGridHeight, amountOfCellTypes, nType, gType);
                for (int h = 0; h < statePageInfo.Count; ++h)
                {
                    ratios.Add(statePageInfo[h].startingAmount.Value);
                    cellAmounts.Add(statePageInfo[h].startingAmount.Value);
                    colors.Add(statePageInfo[h].color);
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
                myCA.InitializeGrid(cellAmounts);
                createdCA = true;
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
            Color tileColor;
            for (int i = 0; i < localGridWidth; ++i)
            {
                for (int j = 0; j < localGridHeight; ++j)
                {
                    if (myCA.grid[i, j].containsAgent == true && (System.Object.ReferenceEquals(myCA.grid[i, j].agent, null) == false))
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
            currentForm.innerPictureBox.Image = bmp;
        }

        public void ClearGrid()
        {
            //Destroy(tex);
            //Destroy(board);
            //Destroy(sr);
            iterations = 0;
            //ratios.Clear();
            //colors.Clear();
            fullCount.Clear();
            //imageList.Clear();
            //probabilities.Clear();
            myCA = null;
            alreadyCA = false;
        }

        public void ResetGrid()
        {
            createdCA = false;
            editModeOn = false;
            if (running == false)
            {
                runs++;
                ClearGrid();
                //CreateCA(mainPageInfo);
            }
        }

        public void EditGrid(int xValue, int yValue, PictureBoxWithInterpolationMode container)
        {
            if(editModeOn == true)
            {
                Double tempX = Convert.ToDouble(xValue);
                Double tempY = Convert.ToDouble(yValue);
                int xProper = Convert.ToInt32((tempX / container.Size.Width) * myCA.gridWidth);
                int yProper = Convert.ToInt32((tempY / container.Size.Height) * myCA.gridHeight);
                if (myCA.grid[xProper, yProper].containsAgent == true)
                {
                    myCA.grid[xProper, yProper].agent.currentState += 1;
                    if(myCA.grid[xProper, yProper].agent.currentState > (mainPageInfo.numStates - 1))
                    {
                        myCA.grid[xProper, yProper].agent.currentState = (myCA.grid[xProper, yProper].agent.currentState - mainPageInfo.numStates);
                    }
                }
            }
        }

        public void EditGrid(int[] rangeX, int[] rangeY, PictureBoxWithInterpolationMode container)
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
                        if (myCA.grid[xProper, yProper].containsAgent == true)
                        {
                            myCA.grid[xProper, yProper].agent.currentState += 1;
                            if (myCA.grid[xProper, yProper].agent.currentState > (mainPageInfo.numStates - 1))
                            {
                                myCA.grid[xProper, yProper].agent.currentState = (myCA.grid[xProper, yProper].agent.currentState - mainPageInfo.numStates);
                            }
                        }
                    }
                }
            }
        }

        public void CheckSettings(Form1 form)
        {
            // This is for saving the cell counts in text file
            if(form.toolStripMenuItem5.Checked == true)
            {

            }

            // For image saving
            if(form.setImageSaveToolStripMenuItem.Checked == true)
            {
                if(string.IsNullOrWhiteSpace(form.iterationCountImageSave.Text))
                {

                }
                else
                {
                    List<string> imageValueStrings = new List<string>();
                    if (form.iterationCountImageSave.Text.Contains(","))
                    {
                        string[] imageValues = form.iterationCountImageSave.Text.Split(',');
                        for (int i = 0; i < imageValues.Length; i++)
                        {
                            imageValueStrings.Add(imageValues[i]);
                        }
                    }
                    else
                    {
                        imageValueStrings.Add(form.iterationCountImageSave.Text);
                    }
                    // Check if iteration is the same as any in the lest. If so, do the image save
                }
            }

            // Reset CA options
            if(form.setAutoResetToolStripMenuItem.Checked == true)
            {
                // Iteration-based

                if(form.iterationCountToolStripMenuItem.Checked == true)
                {
                    if (string.IsNullOrWhiteSpace(form.resetIterationTextBox.Text))
                    {
                    }
                    else
                    {
                        if(int.Parse(form.resetIterationTextBox.Text) == iterations)
                        {
                            // Reset procedure
                            CheckDataSave(form);
                            form.AutoReset();
                        }
                    }
                }

                // Cell count based
                if(form.cellCountToolStripMenuItem.Checked == true)
                {
                    List<int> cellCounts = new List<int>();
                    for(int i = 0; i <  amountOfCellTypes; i++)
                    {
                        string currentName = "CellBox" + i.ToString();
                        string boxText = form.GetText(currentName);
                        if (string.IsNullOrWhiteSpace(boxText))
                        {
                            cellCounts.Add(-1);
                        }
                        else
                        {
                            cellCounts.Add(int.Parse(boxText));
                        }
                    }

                    for(int i = 0; i < cellCounts.Count; i++)
                    {
                        if(CA.stateCount[i] == cellCounts[i])
                        {
                            // Reset procedure
                            CheckDataSave(form);
                            form.AutoReset();
                        }
                    }
                }
            }
        }
        
        // This is for saving the cell count and images upon reseting the grid
        void CheckDataSave(Form1 form)
        {
            // This is for saving the cell counts in text file
            if (form.toolStripMenuItem5.Checked == true)
            {

            }

            // For image saving
            if (form.setImageSaveToolStripMenuItem.Checked == true)
            {

            }
        }
    }
}
