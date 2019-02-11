using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        static List<StatePageInfo> statePageInfo;
        public RunSettings runSettings = Form1.runSettings;
        public bool reset_now = false;
        List<NType> nTypes;
        List<GridType> grids;
        public List<Color> colors;
        List<int> cellAmounts;
        List<Tuple<int, List<int>>> fullCount;
        List<Tuple<int, List<int>>> fullTransitions;
        List<Tuple<int, List<double>>> fullIndex;
        List<object> templateObject;

        public bool editModeOn = false;
        bool createdCA = false;
        bool alreadyCA = false;
        //bool running;
        public int iterations = 0;
        public int caRuns = 1;
        //public int neighborhoodType;
        int localGridWidth;
        int localGridHeight;
        public int amountOfCellTypes;
        //double iterationSpeed;
        DirectBitmap bmp;
        public Form1 local_form;

        public MainPageInfo MainPageInfo
        {
            get { return mainPageInfo; }
            set { mainPageInfo = value; }
        }

        public List<Tuple<int, List<int>>> FullCount { get => fullCount; set => fullCount = value; }
        public List<Tuple<int, List<int>>> FullTransitions { get => fullTransitions; set => fullTransitions = value; }
        public List<Tuple<int, List<double>>> FullIndex { get => fullIndex; set => fullIndex = value; }
        public bool CreatedCA { get => createdCA; set => createdCA = value; }
        public bool AlreadyCA { get => alreadyCA; set => alreadyCA = value; }

        static ControllerScript()
        {
            mainPageInfo = new MainPageInfo();
            cellPageController = new StatePageController();
            statePageInfo = new List<StatePageInfo>();
            mainPageController = new MainPageController();
        }

        public void SetMainInfo(TextBox stateNumberBox, TextBox gridSizeHori, TextBox gridSizeVert)
        {
            //May have errors here - previously it was an int? so it checked if a value had been assigned.
            stateNumberBox.Text = mainPageInfo.numStates.ToString();
            gridSizeHori.Text = mainPageInfo.gridWidth.ToString();
            gridSizeVert.Text = mainPageInfo.gridHeight.ToString();
        }

        public void MainPageNext(int numStates, int gridWidth, int gridHeight, Template template)
        {
            UpdateMainValues(numStates, gridWidth, gridHeight, template);
            if (CheckMainPageInfo() == false)
            {
                return;
            }
            if (mainPageInfo.numStates != statePageInfo.Count)
            {
                SetupStateInfo();
            }
        }

        //public void UpdateProbFields(UserControl1 uc, int currentState)
        //{
        //    uc.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        //}

        //public void AdvancedUpdateProbFields(UserControl2 uc, int currentState, int amountOfStates)
        //{
        //    uc.UpdateValues(statePageInfo[(currentState - 1)], currentState, amountOfStates);
        //}

        public StatePageInfo GetStatePage(int currentState)
        {
            return statePageInfo[currentState];
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

        public void SetupStateInfo()
        {
            // Clear out old state pages
            statePageInfo.Clear();
            //int neighbors;
            for (int i = 0; i < mainPageInfo.numStates; ++i)
            {
                // Needs to be different based on which type of neighborhood we're using.
                StatePageInfo current = new StatePageInfo(i + 1);
                //StatePageInfo current = new StatePageInfo(mainPageInfo.numStates);
                statePageInfo.Add(current);
                //Console.WriteLine("New statePage added");
            }
        }

        public void StateInfoDirectEdit(int infoState, NType nType, GridType gridType, Color color,
            List<Tuple<int, int>> startingLocations, int neighbors,  int startingAmount, List<List<List<double>>> probs,
            bool mobile, int mobileN, List<double> moveProbs, bool sticking,List<double> stickingProbs, bool storage,
            bool ai, bool growth, List<Tuple<string, double>> storageObjects)
        {
            statePageInfo[infoState].nType = nType;
            statePageInfo[infoState].gridType = gridType;
            statePageInfo[infoState].color = color;
            statePageInfo[infoState].startingLocations = startingLocations;
            statePageInfo[infoState].neighbors = neighbors;
            statePageInfo[infoState].startingAmount = startingAmount;
            statePageInfo[infoState].probs = probs;
            statePageInfo[infoState].mobile = mobile;
            statePageInfo[infoState].mobileNeighborhood = mobileN;
            statePageInfo[infoState].moveProbs = moveProbs;
            statePageInfo[infoState].sticking = sticking;
            statePageInfo[infoState].stickingProbs = stickingProbs;
            statePageInfo[infoState].storage = storage;
            statePageInfo[infoState].ai = ai;
            statePageInfo[infoState].growth = growth;
            statePageInfo[infoState].storageObjects = storageObjects;
        }

        public void UpdateMainValues(int newnumStates, int newgridWidth, int newgridHeight, Template newTemplate)
        {
            mainPageInfo.numStates = newnumStates;
            mainPageInfo.gridWidth = newgridWidth;
            mainPageInfo.gridHeight = newgridHeight;
            mainPageInfo.template = newTemplate;
        }

        public void UpdateMainTemplateInfo(bool reset)
        {
            mainPageInfo.template_reset = reset;
        }

        //public void UpdateProbValues(UserControl1 newUC, int currentState)
        //{
        //    // Do these need to be here? Seems a little like excessive OOP.
        //    newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        //}

        //public void RetrieveProbValues(UserControl1 newUC, int currentState)
        //{
        //    // Do these need to be here? Seems a little like excessive OOP.
        //    newUC.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        //}

        public void AdvancedUpdateProbValues(UserControl2 newUC, int currentState)
        {
            newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        }

        public void AdvancedRetrieveProbValues(UserControl2 newUC, int currentState)
        {
            newUC.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        }

        //public void Update2ndOrder(_2ndOrderTabs newUC, int currentState)
        //{
        //    newUC.SetValues(statePageInfo[(currentState - 1)], currentState);
        //}

        //public void Retrieve2ndOrder(_2ndOrderTabs newUC, int currentState)
        //{
        //    newUC.UpdateValues(statePageInfo[(currentState - 1)], currentState);
        //}

        public void SetStartingLocations(List<Tuple<int, int>> incomingStartingLocations, int currentState)
        {
            statePageInfo[currentState].startingLocations = new List<Tuple<int, int>>();
            statePageInfo[currentState].startingLocations = incomingStartingLocations;
        }

        // Stuff from other controller here

        public void OneIteration(Form1 currentForm)
        {
            //running = true;
            myCA.OneIteration();
            iterations++;
            // move these to check settings?
            List<int> currentCellCount = new List<int>();
            currentCellCount.AddRange(myCA.StateCount);

            if (CheckCount(new List<int>(currentCellCount)))
            {
                FullCount.Add(new Tuple<int, List<int>>(iterations, new List<int>(currentCellCount)));
            }
            List<int> currentTransitions = new List<int>();
            currentTransitions.AddRange(myCA.Transitions);
            if (CheckTrans(new List<int>(currentTransitions)))
            {
                FullTransitions.Add(new Tuple<int, List<int>>(iterations, new List<int>(currentTransitions)));
            }
            
            List<double> currentIndex = new List<double>();
            currentIndex.AddRange(myCA.CIndexes);
            if (CheckIndex(new List<double>(currentIndex)))
            {
                FullIndex.Add(new Tuple<int, List<double>>(iterations, new List<double>(currentIndex)));
            }
        }

        bool CheckCount(List<int> curCount)
        {
            if (iterations > 1)
            {
                Tuple<int, List<int>> prev = FullCount.Last();
                List<int> prev_list = prev.Item2;
                for (int i = 0; i < curCount.Count; i++)
                {
                    if (curCount[i] == prev_list[i])
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        bool CheckTrans(List<int> curTrans)
        {
            if (iterations > 1)
            {
                Tuple<int, List<int>> prev = FullTransitions.Last();
                List<int> prev_list = prev.Item2;
                for (int i = 0; i < curTrans.Count; i++)
                {
                    if (curTrans[i] == prev_list[i])
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        bool CheckIndex(List<double> curIndex)
        {
            if (iterations > 1)
            {
                Tuple<int, List<double>> prev = FullIndex.Last();
                List<double> prev_list = prev.Item2;
                for (int i = 0; i < curIndex.Count; i++)
                {
                    if (curIndex[i] == prev_list[i])
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateCA(Form1 form)
        {
            local_form = form;
            if (CreatedCA == false)
            {
                iterations = 0;
                editModeOn = false;
                grids = new List<GridType>();
                nTypes = new List<NType>();
                colors = new List<Color>();
                cellAmounts = new List<int>();
                fullCount = new List<Tuple<int, List<int>>>();
                fullTransitions = new List<Tuple<int, List<int>>>();
                fullIndex = new List<Tuple<int, List<double>>>();
                amountOfCellTypes = mainPageInfo.numStates;
                Template template = mainPageInfo.template;
                bool reset = false;
                if(mainPageInfo.template_reset.HasValue)
                {
                    reset = mainPageInfo.template_reset.Value;
                }
                
                for (int i = 0; i < statePageInfo.Count; i++)
                {
                    nTypes.Add(statePageInfo[i].nType.Value);
                }
                
                for (int i = 0; i < statePageInfo.Count; i++)
                {
                    grids.Add(statePageInfo[i].gridType.Value);
                }
                
                localGridWidth = mainPageInfo.gridWidth;
                localGridHeight = mainPageInfo.gridHeight;
                myCA = new CA(this, localGridWidth, localGridHeight, amountOfCellTypes, nTypes, grids, statePageInfo, template, reset);
                for (int h = 0; h < statePageInfo.Count; ++h)
                {
                    cellAmounts.Add(statePageInfo[h].startingAmount.Value);
                    colors.Add(statePageInfo[h].color.Value);
                }
                myCA.InitializeGrid(cellAmounts);
                CreatedCA = true;
            }
        }

        public void Pause()
        {
            local_form.PauseUnpauseCA();
        }

        public void EditCA()
        {
            for (int h = 0; h < statePageInfo.Count; ++h)
            {
                colors[h] = statePageInfo[h].color.Value;
            }
        }

        public void StartCA(Form1 currentForm)
        {
            editModeOn = false;
            if (AlreadyCA == false)
            {
                iterations = 0;
                Console.WriteLine(localGridWidth + "," +  localGridHeight);
                bmp = new 
                    DirectBitmap(localGridWidth, localGridHeight);
                //currentForm.innerPictureBox.Image = bmp.Bitmap;
                UpdateBoard(currentForm);
                AlreadyCA = true;
            }
        }

        public static Color PreMultiplyAlpha(Color pixel)
        {
            return Color.FromArgb(
                pixel.A,
                PreMultiplyAlpha_Component(pixel.R, pixel.A),
                PreMultiplyAlpha_Component(pixel.G, pixel.A),
                PreMultiplyAlpha_Component(pixel.B, pixel.A));
        }

        private static byte PreMultiplyAlpha_Component(byte source, byte alpha)
        {
            return (byte)((float)source * (float)alpha / (float)byte.MaxValue + 0.5f);
        }

        public void UpdateBoard(Form1 currentForm)
        {
            //BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Color tileColor;
            //int bitsPerPixel_int = System.Drawing.Image.GetPixelFormatSize(bmp_data.PixelFormat);
            //byte bitsPerPixel  = Convert.ToByte(bitsPerPixel_int);
            //byte* scan0 = (byte*)bmp_data.Scan0.ToPointer();
            if (iterations == 0)
            {
                for (int i = 0; i < localGridWidth; ++i)
                {
                    for (int j = 0; j < localGridHeight; ++j)
                    {
                        if (myCA.grid[i, j].ContainsAgent == true && (System.Object.ReferenceEquals(myCA.grid[i, j].agent, null) == false))
                        {
                           tileColor  = PreMultiplyAlpha(colors[myCA.GetCellState(i, j)]);
                            bmp.SetPixel(i, j, tileColor);
                        }
                        else
                        {
                            tileColor = PreMultiplyAlpha(Color.Black);
                            bmp.SetPixel(i, j, tileColor);
                        }
                    }
                }
            }
            //Rectangle cur_rest = currentForm.innerPictureBox.ClientRectangle;
            for (int i = 0; i < myCA.ActiveAgents.Count; i++)
            {
                AgentController curAgent = myCA.ActiveAgents[i];
                int oldX;
                int oldY;
                if (curAgent.History.Count > 1)
                {
                    if (curAgent.HistoryChange)
                    {
                        oldX = curAgent.History[myCA.ActiveAgents[i].History.Count - 2].Item1;
                        oldY = curAgent.History[myCA.ActiveAgents[i].History.Count - 2].Item2;
                        if (myCA.grid[oldX, oldY].ContainsAgent == true && (System.Object.ReferenceEquals(myCA.grid[oldX, oldY].agent, null) == false))
                        {
                            tileColor = PreMultiplyAlpha(colors[myCA.grid[oldX, oldY].agent.History[myCA.ActiveAgents[i].History.Count - 2].Item3]);
                            bmp.SetPixel(oldX, oldY, tileColor);
                        }
                        else
                        {
                            tileColor = PreMultiplyAlpha(Color.Black);
                            bmp.SetPixel(oldX, oldY, tileColor);
                        }
                    }
                }

                int newX = curAgent.xLocation;
                int newY = curAgent.yLocation;


                tileColor = PreMultiplyAlpha(colors[curAgent.currentState]);
                bmp.SetPixel(newX, newY, tileColor);

            }
            //if(currentForm.innerPictureBox != null)
            //{
            //    currentForm.innerPictureBox.Dispose();
            //}
            if(currentForm.innerPictureBox.Image != null)
            {
                lock (currentForm.innerPictureBox.Image)
                {
                    currentForm.innerPictureBox.Image = bmp.Bitmap;
                }
            }
            else
            {
                currentForm.innerPictureBox.Image = bmp.Bitmap;
            }
            
        }

        public void ClearGrid()
        {
            // clear grid allows CA settings to persist while removing this specific "run."
            iterations = 0;
            FullCount.Clear();
            FullTransitions.Clear();
            FullIndex.Clear();
            myCA = null;
            AlreadyCA = false;
        }

        public void ResetGrid(Form1 form)
        {
            // this should be for reseting everything, including CA settings
            string time = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");
            CheckFinalDataSave(form, time);
            caRuns++;
            CreatedCA = false;
            editModeOn = false;
            reset_now = false;
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
                        myCA.grid[xProper, yProper].AddAgent(xProper, yProper, new AgentController(xProper, yProper, state));
                        myCA.grid[xProper, yProper].ContainsAgent = true;
                        myCA.grid[xProper, yProper].agent.currentState = state;
                        myCA.grid[xProper, yProper].agent.xLocation = xProper;
                        myCA.grid[xProper, yProper].agent.yLocation = yProper;
                        myCA.AddAgent(myCA.grid[xProper, yProper].agent);
                    }
                }
                UpdateCounter();
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
                                myCA.RemoveAgent(xProper, yProper);
                                myCA.grid[xProper, yProper].agent = null;
                                myCA.grid[xProper, yProper].ContainsAgent = false;
                            }
                            else if (myCA.grid[xProper, yProper].ContainsAgent == false)
                            {
                                myCA.grid[xProper, yProper].AddAgent(xProper, yProper, new AgentController(xProper, yProper, state));
                                myCA.grid[xProper, yProper].ContainsAgent = true;
                                myCA.grid[xProper, yProper].agent.currentState = state;
                                myCA.grid[xProper, yProper].agent.xLocation = xProper;
                                myCA.grid[xProper, yProper].agent.yLocation = yProper;
                                myCA.AddAgent(myCA.grid[xProper, yProper].agent);
                            }
                        }
                    }
                }
                UpdateCounter();
            }
        }

        void UpdateCounter()
        {
            //get new statecount from CA, then change counter box
        }
        
        //public List<double> ReturnConnectivityIndex()
        //{
        //    Console.WriteLine("ReturnConnectivityIndex");
        //    List<double> tempList = new List<double>();
        //    if(createdCA)
        //    {
        //        for (int i = 0; i < amountOfCellTypes; i++)
        //        {
        //            tempList.Add(myCA.GetCIndex(i));
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < amountOfCellTypes; i++)
        //        {
        //            tempList.Add(0);
        //        }
        //    }
        //    return tempList;
        //}

        public void CheckSettings(Form1 form)
        {
            //Get system time here

            string time = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");

            CheckDataSave(form, time);

            //Console.WriteLine("Reset: " + runSettings.AutoReset);
            // Reset CA options
            if (runSettings.ResetIterations.Count > 0 || runSettings.ResetCounts.Count > 0)
            {
                // Iteration-based
                for (int i = 0; i < runSettings.ResetIterations.Count; i++)
                {
                    if (runSettings.ResetIterations[i] == iterations)
                    {
                        //CheckFinalDataSave(form, time);
                        form.AutoReset();
                    }
                }
                // Cell count based
                for(int i = 0; i < runSettings.ResetCounts.Count; i++)
                {
                    if (myCA.StateCount[i] == runSettings.ResetCounts[i])
                    {
                        //CheckFinalDataSave(form, time);
                        form.AutoReset();
                    }
                }
            }

            // spur-of-the-moment reset based on template call
            if(reset_now)
            {
                form.AutoReset();
            }

            // Pause CA options
            if (runSettings.PauseIterations.Count > 0 || runSettings.PauseCounts.Count > 0)
            {
                // Iteration-based
                for (int i = 0; i < runSettings.PauseIterations.Count; i++)
                {
                    if (runSettings.PauseIterations[i] == iterations)
                    {
                        //CheckFinalDataSave(form, time);
                        form.PauseUnpauseCA();
                    }
                }
                // Cell count based
                for (int i = 0; i < runSettings.PauseCounts.Count; i++)
                {
                    if (myCA.StateCount[i] == runSettings.PauseCounts[i])
                    {
                        //CheckFinalDataSave(form, time);
                        form.PauseUnpauseCA();
                    }
                }
            }
        }
        
        // This is for saving the cell count and images upon reseting the grid
        void CheckDataSave(Form1 form, string time)
        {
            bool counts = runSettings.SaveCounts;
            bool trans = runSettings.SaveTrans;
            bool cIndex = runSettings.SaveIndex;
            // This is for saving the cell counts in text file
            if (runSettings.DataIncs.Count > 0)
            {
                // Check if iteration is the same as any in the list. If so, do the count save
                for (int i = 0; i < runSettings.DataIncs.Count; i++)
                {
                    if (runSettings.DataIncs[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        int remainder = iterations % runSettings.DataIncs[i];
                        if (remainder == 0)
                        {
                            // Auto count save
                            form.SaveData(time, counts, trans, cIndex, runSettings.DataPath);
                        }
                    }
                }
            }

            // For image saving
            if (runSettings.ImageIncs.Count > 0)
            {
                // Check if iteration is the same as any in the list. If so, do the image save
                for (int i = 0; i < runSettings.ImageIncs.Count; i++)
                {
                    if (runSettings.ImageIncs[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        int remainder = iterations % runSettings.ImageIncs[i];
                        if (remainder == 0)
                        {
                            // Auto image save
                            form.InvokeImageSave(time, runSettings.ImagePath);
                        }
                    }
                }
            }

            // For path saving
            if (runSettings.PathsIncs.Count > 0)
            {
                // Check if iteration is the same as any in the list. If so, do the image save
                for (int i = 0; i < runSettings.PathsIncs.Count; i++)
                {
                    if (runSettings.PathsIncs[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        int remainder = iterations % runSettings.PathsIncs[i];
                        if (remainder == 0)
                        {
                            // Auto image save
                            form.AutoPathSave(time, runSettings.PathsPath);
                        }
                    }
                }
            }
        }

        void CheckFinalDataSave(Form1 form, string time)
        {
            bool counts = runSettings.SaveCounts;
            bool trans = runSettings.SaveTrans;
            bool cIndex = runSettings.SaveIndex;
            // This is for saving the cell counts in text file
            if (runSettings.DataIncs.Count > 0)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < runSettings.DataIncs.Count; i++)
                {
                    if (runSettings.DataIncs[i] == -1)
                    {
                        Console.WriteLine("We're here because we're here");
                        form.SaveData(time, counts, trans, cIndex, runSettings.DataPath);
                    }
                }
            }

            // For image saving
            if (runSettings.ImageIncs.Count > 0)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < runSettings.ImageIncs.Count; i++)
                {
                    if (runSettings.ImageIncs[i] == -1)
                    {
                        form.InvokeImageSave(time, runSettings.ImagePath);
                    }
                }
            }

            // For path saving
            if (runSettings.PathsIncs.Count > 0)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < runSettings.PathsIncs.Count; i++)
                {
                    if (runSettings.PathsIncs[i] == -1)
                    {
                        form.AutoPathSave(time, runSettings.PathsPath);
                    }
                }
            }

            if(runSettings.TemplateIncs.Count > 0)
            {
                TemplateAdd();
                for (int i = 0; i < runSettings.TemplateIncs.Count; i++)
                {
                    if (caRuns == runSettings.TemplateIncs[i])
                    {
                        TemplateSave(form, time);
                    }
                }
            }
        }

        public void CheckMaxRuns(Form1 form)
        {
            if(form.RunMax.HasValue)
            {
                if(caRuns > form.RunMax)
                {
                    form.PauseUnpauseCA();
                }
            }
        }

        public void ResetTemplate()
        {
            templateObject = new List<object>();
            TemplateInit();
        }

        void TemplateInit()
        {
            switch(mainPageInfo.template)
            {
                case Template.Random_Walk:
                    templateObject.Add(new List<Tuple<int, int, int>>());
                    break;
            }
        }

        void TemplateAdd()
        {
            // do the custom actions of each template
            switch(mainPageInfo.template)
            {
                case Template.Random_Walk:
                    List<Tuple<int, int, int>> prev = (List<Tuple<int, int, int>>)templateObject[0];
                    prev.AddRange(myCA.AddEnds().Select(x => new Tuple<int, int, int>(x.Item1, x.Item2, iterations)));
                    templateObject[0] = prev;
                    break;
            }
        }

        void TemplateSave(Form1 form, string time)
        {
            switch(mainPageInfo.template)
            {
                case Template.Random_Walk:
                    form.TemplateSave(templateObject, MainPageInfo.template, time, runSettings.TemplatePath);
                    break;
            }
        }
    }
}
