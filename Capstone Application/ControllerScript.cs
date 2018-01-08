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
        public int neighborhoodType;
        List<Color> colors = new List<Color>();
        List<float> ratios = new List<float>();
        List<int> cellAmounts = new List<int>();
        public List<List<int>> fullCount = new List<List<int>>();

        bool editModeOn = false;
        bool alreadyCA = false;
        bool running = false;
        public int iterations = 0;
        public int runs = 0;
        int localGridWidth;
        int localGridHeight;
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

        public void AdvancedUpdateProbFields(UserControl2 uc, int currentState, int amountOfStates)
        {
            uc.UpdateValues(statePageInfo[(currentState - 1)], currentState, amountOfStates);
        }

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
                default:
                    neighbors = 0;
                    break;
            }
            for (int i = 0; i < mainPageInfo.numStates; ++i)
            {
                StatePageInfo current = new StatePageInfo(mainPageInfo.numStates, neighbors, i + 1);
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

        // Stuff from other controller here

        public void OneIteration(Form1 currentForm)
        {
            //Console.WriteLine("ControllerScript OneIteration starting");
            running = true;
            myCA.OneIteration();
            iterations++;
            List<int> currentCellCount = new List<int>();
            currentCellCount.AddRange(CA.stateCount);
            fullCount.Add(currentCellCount);
        }

        public void CreateCA()
        {
            editModeOn = false;
            int amountOfCellTypes = mainPageInfo.numStates;
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

            myCA.InitializeGrid(cellAmounts);
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
        
    }
}
