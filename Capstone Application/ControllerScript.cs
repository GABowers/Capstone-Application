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
        static System.Collections.Concurrent.ConcurrentBag<Tuple<int, int, Color>> pixelChanges;
        static MainPageController mainPageController;
        static StatePageController cellPageController;
        static MainPageInfo mainPageInfo;
        public CA myCA;
        static List<StatePageInfo> statePageInfo;
        public RunSettings runSettings = Form1.runSettings;
        public bool reset_now = false;
        bool reshade = false;
        List<NType> nTypes;
        public List<Color> colors;
        List<int> cellAmounts;
        List<List<Tuple<int, int, int>>> paths = new List<List<Tuple<int, int, int>>>();
        //List<object> templateObject;

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

        public List<Tuple<int, List<int>>> FullCount { get; private set; }
        public List<Tuple<int,Dictionary<Tuple<int, int>, int>>> FullTransitions {get; private set;}
        public List<Tuple<int, List<double>>> FullIndex { get; private set; }
        public bool CreatedCA { get => createdCA; set => createdCA = value; }
        public bool AlreadyCA { get => alreadyCA; set => alreadyCA = value; }
        public List<List<Tuple<int, int, int>>> Paths { get => paths; set => paths = value; }

        static ControllerScript()
        {
            mainPageInfo = new MainPageInfo();
            cellPageController = new StatePageController();
            statePageInfo = new List<StatePageInfo>();
            mainPageController = new MainPageController();
            pixelChanges = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, int, Color>>();
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
            var colors = mainPageInfo.numStates > 0 ? GetColorRange(mainPageInfo.numStates) : null;
            //int neighbors;
            for (int i = 0; i < mainPageInfo.numStates; i++)
            {
                // Needs to be different based on which type of neighborhood we're using.
                StatePageInfo current = mainPageInfo.numStates > 0 ? new StatePageInfo(i + 1, colors[i]) : new StatePageInfo(i + 1);
                //StatePageInfo current = new StatePageInfo(mainPageInfo.numStates);
                statePageInfo.Add(current);
            }
        }

        Color[] GetColorRange(int total)
        {
            List<Color> colors = new List<Color>();
            for (int i = 0; i < total; i++)
            {
                float progress = (float)i / total;
                colors.Add(Rainbow(progress));
            }
            return colors.ToArray();
        }

        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }

        Color ToRGB(double h, double s, double l)
        {
            byte r, g, b;
            if(s == 0)
            {
                r = (byte)Math.Round(l * 255d);
                g = (byte)Math.Round(l * 255d);
                b = (byte)Math.Round(l * 255d);
            }
            else
            {
                double t1, t2;
                double th = h / 6.0d;
                if(l < 0.5d)
                {
                    t2 = l * (1d + s);
                }
                else
                {
                    t2 = (l + s) - (l * s);
                }
                t1 = 2d * l - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);
                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                r = (byte)Math.Round(tr * 255d);
                g = (byte)Math.Round(tg * 255d);
                b = (byte)Math.Round(tb * 255d);
            }
            return Color.FromArgb(r, g, b);
        }

        private static double ColorCalc(double c, double t1, double t2)
        {
            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }

        public void StateInfoDirectEdit(int infoState, NType nType, GridType gridType, Color color,
            List<Tuple<int, int>> startingLocations, int neighbors,  int startingAmount, List<List<List<double>>> probs,
            bool mobile, MoveType mobileN, List<double> moveProbs, bool sticking,List<double> stickingProbs, bool storage,
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
            if(alreadyCA)
            {
                //running = true;
                myCA.OneIteration();
                iterations++;
                // move these to check settings?
                if(runSettings.SaveCounts)
                {
                    FullCount.Add(new Tuple<int, List<int>>(iterations, myCA.StateCount.Values.ToList()));
                }
                if (runSettings.SaveTrans)
                {
                    FullTransitions.Add(new Tuple<int, Dictionary<Tuple<int, int>, int>>(iterations, new Dictionary<Tuple<int, int>, int>(myCA.Transitions)));
                }

                if(runSettings.SaveIndex)
                {
                    myCA.UpdateCIndex();
                    List<double> currentIndex = new List<double>();
                    currentIndex.AddRange(myCA.CIndexes);
                    FullIndex.Add(new Tuple<int, List<double>>(iterations, myCA.CIndexes));
                }
            }
        }

        public void CreateCA(Form1 form)
        {
            local_form = form;
            if(statePageInfo.Any(x => x.containerSettings != null))
            {
                reshade = statePageInfo.Where(x => x.containerSettings != null).Any(xyz => xyz.containerSettings.Any(abc => abc.Shade == true) == true);
            }
            else
            {
                reshade = false;
            }
            iterations = 0;
            editModeOn = false;
            nTypes = new List<NType>();
            colors = new List<Color>();
            cellAmounts = Enumerable.Repeat(0, statePageInfo.Count).ToList();
            FullCount = new List<Tuple<int, List<int>>>();
            FullTransitions = new List<Tuple<int, Dictionary<Tuple<int, int>, int>>>();
            FullIndex = new List<Tuple<int, List<double>>>();
            amountOfCellTypes = mainPageInfo.numStates;
            Template template = mainPageInfo.template;
            bool reset = false;
            if(mainPageInfo.template_reset.HasValue)
            {
                reset = mainPageInfo.template_reset.Value;
            }
                
            for (int i = 0; i < statePageInfo.Count; i++)
            {
                nTypes.Add(statePageInfo[i].nType);
            }
                                
            localGridWidth = mainPageInfo.gridWidth;
            localGridHeight = mainPageInfo.gridHeight;
            myCA = new CA(this, localGridWidth, localGridHeight, amountOfCellTypes, nTypes, statePageInfo, template, reset);
            for (int h = 0; h < statePageInfo.Count; ++h)
            {
                cellAmounts[h] = statePageInfo[h].startingAmount.Value;
                colors.Add(statePageInfo[h].color.Value);
            }
            myCA.InitializeGrid(cellAmounts);
            FullCount.Add(new Tuple<int, List<int>>(0, myCA.StateCount.Values.ToList()));
            FullTransitions.Add(new Tuple<int, Dictionary<Tuple<int, int>, int>>(0, new Dictionary<Tuple<int, int>, int>(myCA.Transitions)));
            FullIndex.Add(new Tuple<int, List<double>>(0, myCA.CIndexes));
            CreatedCA = true;
        }

        public void Pause()
        {
            local_form.PauseCA();
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
                bmp = new DirectBitmap(localGridWidth, localGridHeight);
                //rect = new Rectangle(0, 0, bmp.Bitmap.Width, bmp.Bitmap.Height);
                //currentForm.innerPictureBox.Image = bmp.Bitmap;
                //UpdateBoard(currentForm);
                AlreadyCA = true;
            }
            UpdateBoard(currentForm);
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
            if(alreadyCA)
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                //var data = bmp.Bitmap.LockBits(rect, ImageLockMode.ReadWrite, bmp.Bitmap.PixelFormat);
                //var depth = Bitmap.GetPixelFormatSize(data.PixelFormat);
                //var buffer = new byte[data.Width * data.Height * depth];
                //pixelChanges = new System.Collections.Concurrent.ConcurrentBag<Tuple<int, int, Color>>();
                //if (iterations == 0)
                {
                    Parallel.For(0, CA.backup.Length, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (i) =>
                    {
                        int x = i / myCA.gridWidth;
                        int y = i % myCA.gridWidth;
                        if (CA.backup[i] < 0)
                        {
                            bmp.SetPixel(x, y, Color.Black);
                        }
                        else
                        {
                            bmp.SetPixel(x, y, colors[CA.backup[i]]);
                        }
                    });
                    //CA.backup.AsParallel().ForAll((color, index) =>
                    //{
                    //    int x = i / myCA.gridWidth;
                    //    int y = i % myCA.gridWidth;
                    //    if (CA.backup[i] < 0)
                    //    {
                    //        bmp.SetPixel(x, y, Color.Black);
                    //    }
                    //    else
                    //    {
                    //        bmp.SetPixel(x, y, colors[CA.backup[i]]);
                    //    }
                    //});
                }
                UpdateImage(currentForm, bmp.Bitmap);
            }
        }

        void UpdateImage(Form1 currentForm, Bitmap bmp)
        {
            try
            {
                currentForm.innerPictureBox.Image = bmp;
            }
            catch(InvalidOperationException)
            {
                UpdateImage(currentForm, bmp);
            }
        }

        public void ClearGrid()
        {
            // clear grid allows CA settings to persist while removing this specific "run."
            iterations = 0;
            if(!runSettings.MetaStore)
            {
                FullCount.Clear();
                FullTransitions.Clear();
                FullIndex.Clear();
                Paths.Clear();
            }
            myCA = null;
            AlreadyCA = false;
        }

        public void ResetGrid(Form1 form)
        {
            if(alreadyCA)
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
        }

        public void ResetRuns(int val)
        {
            caRuns = val;
            paths = new List<List<Tuple<int, int, int>>>();
        }

        public Tuple<int, int> TrueLocation(int xValue, int yValue, PixelBox container)
        {
            Double tempX = Convert.ToDouble(xValue);
            Double tempY = Convert.ToDouble(yValue);
            int xProper = Convert.ToInt32((tempX / container.Size.Width) * myCA.gridWidth);
            int yProper = Convert.ToInt32((tempY / container.Size.Height) * myCA.gridHeight);
            return new Tuple<int, int>(xProper, yProper);
        }

        public void EditGrid(int xValue, int yValue, PixelBox container, int buttonPressed, int state)
        {
            if(editModeOn == true)
            {
                Double tempX = Convert.ToDouble(xValue);
                Double tempY = Convert.ToDouble(yValue);
                Edit(tempX, tempY, container, buttonPressed, state);
            }
        }

        void Edit(double tempX, double tempY, PixelBox container, int buttonPressed, int state)
        {
            int xProper = Convert.ToInt32((tempX / container.Size.Width) * myCA.gridWidth);
            int yProper = Convert.ToInt32((tempY / container.Size.Height) * myCA.gridHeight);
            if (xProper == -1)
            {
                xProper = 0;
            }
            if (xProper == myCA.gridWidth)
            {
                xProper = myCA.gridWidth - 1;
            }
            if (yProper == -1)
            {
                yProper = 0;
            }
            if (yProper == myCA.gridHeight)
            {
                yProper = myCA.gridHeight - 1;
            }
            if ((xProper >= 0 && xProper < myCA.gridWidth) && (yProper >= 0 && yProper < myCA.gridHeight))
            {
                var pos = (xProper * myCA.gridWidth) + yProper;
                if (buttonPressed == 0) // adds or changes to current
                {
                    if (myCA.grid[pos].ContainsAgent == true)
                    {
                        myCA.grid[pos].Agent.currentState = state;
                        //if (myCA.grid[xProper, yProper].agent.currentState > (mainPageInfo.numStates - 1))
                        //{
                        //    myCA.grid[xProper, yProper].agent.currentState = (myCA.grid[xProper, yProper].agent.currentState - mainPageInfo.numStates);
                        //}
                    }
                    else
                    {
                        myCA.grid[pos].AddAgent(new AgentController(xProper, yProper, state, myCA, myCA.grid[pos]));
                        myCA.AddAgent(myCA.grid[pos].Agent);
                    }
                }
                if (buttonPressed == 1) // removes agents
                {
                    if (myCA.grid[pos].ContainsAgent == true)
                    {
                        myCA.grid[pos].RemoveAgent();
                        myCA.RemoveAgent(xProper, yProper);
                    }
                }
                myCA.Backup();
                UpdateCounter();
                if(local_form != null)
                {
                    UpdateBoard(local_form);
                }
            }
        }

        public void EditGrid(int[] rangeX, int[] rangeY, PixelBox container, int buttonPressed, int state)
        {
            if (editModeOn == true)
            {
                var xcx = statePageInfo[state].moveProbs;
                //myCA. check if can move
                for (int i = 0; i < rangeX.Length; i++)
                {
                    for (int j = 0; j < rangeY.Length; j++)
                    {
                        Double tempX = Convert.ToDouble(rangeX[i]);
                        Double tempY = Convert.ToDouble(rangeY[j]);
                        Edit(tempX, tempY, container, buttonPressed, state);
                    }
                }
            }
        }

        void UpdateCounter()
        {
            //get new statecount from CA, then change counter box
        }
        
        //public List<double> ReturnConnectivityIndex()
        //{
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
            if(alreadyCA)
            {
                //Get system time here

                string time = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");

                CheckDataSave(form, time);

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
                    for (int i = 0; i < runSettings.ResetCounts.Count; i++)
                    {
                        if (myCA.StateCount[i] == runSettings.ResetCounts[i])
                        {
                            //CheckFinalDataSave(form, time);
                            form.AutoReset();
                        }
                    }
                }

                // spur-of-the-moment reset based on template call
                if (reset_now)
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
                            form.PauseCA();
                        }
                    }
                    // Cell count based
                    for (int i = 0; i < runSettings.PauseCounts.Count; i++)
                    {
                        if (myCA.StateCount[i] == runSettings.PauseCounts[i])
                        {
                            //CheckFinalDataSave(form, time);
                            form.PauseCA();
                        }
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
                            form.AutoPathSave(time, runSettings.PathsPath, true);
                        }
                    }
                }
            }

            if (runSettings.HistIncs.Count > 0)
            {
                // Check if iteration is the same as any in the list. If so, do the count save
                for (int i = 0; i < runSettings.HistIncs.Count; i++)
                {
                    if (runSettings.HistIncs[i] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        int remainder = iterations % runSettings.HistIncs[i];
                        if (remainder == 0 && runSettings.HistRunIncs.Any(x => caRuns % x == 0))
                        {
                            GetHist(time, form, runSettings.HistPath);
                        }
                    }
                }
            }
        }

        public void GetHist(string time, Form1 form, string filePath)
        {
            Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>, List<Tuple<int, int>>> output = Analysis.FinalLocationHistogram(paths, new Tuple<int, int>(localGridWidth, localGridHeight));
            form.SaveHist(output, time, filePath, paths.Select(x => x.Count() - 1).ToList(), paths.Count);
        }

        public void UpdatePaths()
        {
            if (runSettings.SavePaths)
            {
                paths.AddRange(myCA.GetPaths());
            }
        }

        void CheckFinalDataSave(Form1 form, string time)
        {
            bool counts = runSettings.SaveCounts;
            bool trans = runSettings.SaveTrans;
            bool cIndex = runSettings.SaveIndex;

            if (runSettings.SavePaths)
            {
                paths.AddRange(myCA.GetPaths());
            }

            // This is for saving the cell counts in text file
            if (runSettings.DataIncs.Count > 0)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < runSettings.DataIncs.Count; i++)
                {
                    if (runSettings.DataIncs[i] == -1)
                    {
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
                        form.AutoPathSave(time, runSettings.PathsPath, true);
                    }
                }
            }

            if (runSettings.HistIncs.Count > 0)
            {
                // Check for code denoting save at reset. If so, do the image save
                for (int i = 0; i < runSettings.HistIncs.Count; i++)
                {
                    if (runSettings.HistIncs[i] == -1 && runSettings.HistRunIncs.Any(x => caRuns % x == 0))
                    {
                        GetHist(time, form, runSettings.HistPath);
                    }
                }
            }

            //if(runSettings.TemplateIncs.Count > 0)
            //{
            //    // This action should happen every time we reach a run with (run % templateInc) == 0
            //    TemplateAdd();
            //    for (int i = 0; i < runSettings.TemplateIncs.Count; i++)
            //    {
            //        if (caRuns % runSettings.TemplateIncs[i] == 0)
            //        {
            //            TemplateSave(form, time);
            //        }
            //    }
            //}
        }

        public void CheckMaxRuns(Form1 form)
        {
            if(runSettings.PauseRuns != null)
            {
                for (int i = 0; i < runSettings.PauseRuns.Count; i++)
                {
                    if (caRuns > runSettings.PauseRuns[i])
                    {
                        //CheckFinalDataSave(form, time);
                        form.PauseCA();
                    }
                }
            }
        }

        //public void ResetTemplate()
        //{
        //    templateObject = new List<object>();
        //    TemplateInit();
        //}

        //void TemplateInit()
        //{
        //    switch(mainPageInfo.template)
        //    {
        //        case Template.Random_Walk:
        //            templateObject.Add(new List<Tuple<int, int, int>>());
        //            break;
        //    }
        //}

        //void TemplateAdd()
        //{
        //    // do the custom actions of each template
        //    switch(mainPageInfo.template)
        //    {
        //        case Template.Random_Walk:
        //            List<Tuple<int, int, int>> prev = (List<Tuple<int, int, int>>)templateObject[0];
        //            prev.AddRange(myCA.AddEnds().Select(x => new Tuple<int, int, int>(x.Item1, x.Item2, iterations)));
        //            templateObject[0] = prev;
        //            break;
        //    }
        //}

        //void TemplateSave(Form1 form, string time)
        //{
        //    switch(mainPageInfo.template)
        //    {
        //        case Template.Random_Walk:
        //            Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> output = Analysis.FinalLocationHistogram(templateObject, new Tuple<int, int>(localGridWidth, localGridHeight));
        //            form.TemplateSave((object)output, mainPageInfo.template, time, runSettings.TemplatePath);
        //            break;
        //    }
        //}
    }
}
